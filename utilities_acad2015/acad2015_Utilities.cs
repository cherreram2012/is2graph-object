using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace Utilities_acad2015
{
	public enum AcadColor { Red = 1, Yellow, LightGreen, Cian}

	public class AcadUtilities 
  {
    //------------------------------------------------------------------------//
    //
    public static void Zoom(Point3d pMin, Point3d pMax, Point3d pCenter, double dFactor)
    {
      // Get de current document and database
      Document acDoc = Application.DocumentManager.MdiActiveDocument;
      Database acCurDb = acDoc.Database;

			int nCurVport = System.Convert.ToInt32(Application.GetSystemVariable("CVPORT"));

      // Get the extents of the current space no points
      // or only a center point is provided.
      //Check to see if Model space is current
      if (acCurDb.TileMode)
      {
        if (pMin.Equals(new Point3d()) && pMax.Equals(new Point3d()))
        {
          pMin = acCurDb.Extmin;
          pMax = acCurDb.Extmax;
        }
      }
      else
      {
        // Check to see if Paper space is current
        if (nCurVport == 1)
        {
          // Get the extents of Papers space
          if (pMin.Equals(new Point3d()) && pMax.Equals(new Point3d()))
          {
            pMin = acCurDb.Pextmin;
            pMax = acCurDb.Pextmax;
          }
        }
        else
        {
          // Get the extents of Model space
          if (pMin.Equals(new Point3d()) && pMax.Equals(new Point3d()))
          {
            pMin = acCurDb.Extmin;
            pMax = acCurDb.Extmax;
          }
        }
      }

      // Start transation
      using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
      {
        // Get the current view
        using (ViewTableRecord acView = acDoc.Editor.GetCurrentView())
        {         
          // Translate WCS coordinates to DCS
          Matrix3d matWCS2DCS;
          matWCS2DCS = Matrix3d.PlaneToWorld(acView.ViewDirection);
          matWCS2DCS = Matrix3d.Displacement(acView.Target - Point3d.Origin) * matWCS2DCS;
          matWCS2DCS = Matrix3d.Rotation(-acView.ViewTwist, acView.ViewDirection, acView.Target) * matWCS2DCS;

          // If a center point specified, define the min and max 
          // point of extents for Center and Scale modes
          if (!pCenter.DistanceTo(Point3d.Origin).Equals(0))
          {
            pMin = new Point3d(pCenter.X - (acView.Width / 2),
                               pCenter.Y - (acView.Height / 2), 0);

            pMax = new Point3d((acView.Width / 2) + pCenter.X,
                               (acView.Height / 2) + pCenter.Y, 0);
          }

          // Create an extents object using a line
	        Line acLine = new Line(pMin, pMax);
	        if (acLine.Bounds != null)
	        {
		        Extents3d eExtents = new Extents3d(acLine.Bounds.Value.MinPoint, acLine.Bounds.Value.MaxPoint);

		        // Calculate the ratio between the width and height of the current View
		        double dViewRatio = (acView.Width/acView.Height);

		        // Transform teh extents of the view
		        matWCS2DCS = matWCS2DCS.Inverse();
		        eExtents.TransformBy(matWCS2DCS);

		        double dWidth;
		        double dHeight;
		        Point2d pNewCentPt;

		        // Check see if a center point was provided (Center an dScale modes)
		        if (!pCenter.DistanceTo(Point3d.Origin).Equals(0.0))
		        {
			        dWidth = acView.Width;
			        dHeight = acView.Height;

			        if (dFactor.Equals(0.0))
				        pCenter = pCenter.TransformBy(matWCS2DCS);

			        pNewCentPt = new Point2d(pCenter.X, pCenter.Y);
		        }
		        else // Working in window, Extents and Limits mode
		        {
			        // Calculate the new width and height of current view
			        dWidth = eExtents.MaxPoint.X - eExtents.MinPoint.X;
			        dHeight = eExtents.MaxPoint.Y - eExtents.MinPoint.Y;

			        //Get te center of the view
			        pNewCentPt = new Point2d(((eExtents.MaxPoint.X + eExtents.MinPoint.X)*0.5),
				        ((eExtents.MaxPoint.Y + eExtents.MinPoint.Y)*0.5));
		        }

		        // Check to see if the new width fits in current window
		        if (dWidth > (dHeight*dViewRatio)) dHeight = dWidth/dViewRatio;

		        // Resize and scale the view
		        if (!dFactor.Equals(0.0))
		        {
			        acView.Height = dHeight*dFactor;
			        acView.Width = dWidth*dFactor;
		        }

		        // Set teh center of the view
		        acView.CenterPoint = pNewCentPt;

		        // Set the current view
		        acDoc.Editor.SetCurrentView(acView);
	        }
        }

        acTrans.Commit();
      }
    }

		//------------------------------------------------------------------------//
    //
	  public static void RotateUCS ()
	  {
	  
	  }

		//------------------------------------------------------------------------//
		//
		public static void MoveUCS()
		{

		}

		//------------------------------------------------------------------------//
		//
	  public static void AddCenterAxis (Point3d pC, double width, double height, int color, int scale)
	  {
		  Document acDoc = Application.DocumentManager.MdiActiveDocument;
		  Database acCurDb = acDoc.Database;

		  // Start a transaction
		  using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
		  {
			  // Open the Block table for read
			  BlockTable acBlkTbl;
			  acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

			  // Open the Block table record Model space for write
			  BlockTableRecord acBlkTblRec;
			  acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

			  if (acBlkTblRec == null) return;

			  // Open the Linetype table for read
				const string sLineTypName = "Center";

				LinetypeTable acLineTypTbl;
			  acLineTypTbl = acTrans.GetObject(acCurDb.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
			  
			  if (acLineTypTbl != null)
			  {
				  if (acLineTypTbl.Has(sLineTypName) == false)
				  {
					  acCurDb.LoadLineTypeFile(sLineTypName, "acad.lin");
				  }
			  }

			  // Dibujo los Ejes de Centro
			  Point3d Ox1, Ox2, Oy1, Oy2;
				Ox1 = new Point3d(pC.X - width, pC.Y, 0.0);
				Ox2 = new Point3d(pC.X + width, pC.Y, 0.0);
				Oy1 = new Point3d(pC.X, pC.Y - height, 0.0);
				Oy2 = new Point3d(pC.X, pC.Y + height, 0.0);
			  Line ejeX = new Line(Ox1, Ox2);
			  Line ejeY = new Line(Oy1, Oy2);

			  ejeX.ColorIndex = color;
			  ejeX.LinetypeScale = scale;
			  ejeX.Linetype = sLineTypName;
			  ejeY.ColorIndex = color;
			  ejeY.LinetypeScale = scale;
			  ejeY.Linetype = sLineTypName;

			  acBlkTblRec.AppendEntity(ejeX);
			  acTrans.AddNewlyCreatedDBObject(ejeX, true);

			  acBlkTblRec.AppendEntity(ejeY);
			  acTrans.AddNewlyCreatedDBObject(ejeY, true);
			  
				acTrans.Commit();
		  }
	  }

		//------------------------------------------------------------------------//
		//
	  public static void AddEntityToModel (Entity entity, int color = 255)
	  {
			Document acDoc = Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;

		  using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
		  {
			  BlockTable acBlkTbl;
			  acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

			  BlockTableRecord acBlkTblRec;
			  acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

			  if (acBlkTblRec == null) return;

			  entity.ColorIndex = color;
				acBlkTblRec.AppendEntity(entity);
				acTrans.AddNewlyCreatedDBObject(entity, true);

				acTrans.Commit();
		  }
	  }

		//------------------------------------------------------------------------//
		//
	  public static void AddPinToModel (DBPoint P, int mode, double size, int color = 255)
	  {
			Document acDoc = Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;

			using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
			{
				BlockTable acBlkTbl;
				acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

				BlockTableRecord acBlkTblRec;
				acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

				if (acBlkTblRec == null) return;

				acCurDb.Pdmode = mode;
				acCurDb.Pdsize = size;

				P.ColorIndex = color;
				acBlkTblRec.AppendEntity(P);
				acTrans.AddNewlyCreatedDBObject(P, true);

				acTrans.Commit();
			}
	  }
  }
}
