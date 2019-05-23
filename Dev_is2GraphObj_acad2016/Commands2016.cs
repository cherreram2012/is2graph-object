using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using is2GraphObject;
using Utilities_acad2016;
using EntityExtension_acad2016;
using Translator_is2graph_acad;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;
using SegmentType = is2GraphObject.SegmentType;

namespace Dev_is2GraphObj
{
  public class Commands2016
  {	  
		#region - Command: RotarPunto -
		[CommandMethod("RotarPunto")]
		public static void RotarPunto()
		{
			Document acDoc = Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;

			using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
			{
				BlockTable acBlkTbl;
				acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

				BlockTableRecord acBlkTblRec;
				acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

				//--- Session donde se dibuja ---//
				/* DIBUJO CIRCULO GRANDE */
				double r = 20.0;
				Circle C1 = new Circle(new Point3d(0, 0, 0), Vector3d.ZAxis, r);
				C1.SetDatabaseDefaults();
				acBlkTblRec.AppendEntity(C1);
				acTrans.AddNewlyCreatedDBObject(C1, true);

				/* UBICO PUNTO MARRON */
				PointType P1 = is2GraphObj.PolarPoint(new PointType(), 35.0, r);
				DBPoint Pin1 = new DBPoint(is2GraphTranslator.ToAcad_3dG(P1));
				Pin1.ColorIndex = 22;		// MARRON
				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 0.6;
				acBlkTblRec.AppendEntity(Pin1);
				acTrans.AddNewlyCreatedDBObject(Pin1, true);

				/* DIBUJO CIRCULO MAS PEQUEÑO */
				Point3d P00 = is2GraphTranslator.ToAcad_3d(is2GraphObj.PolarPoint(new PointType(), 20.0, r - 5));
				Circle C2 = new Circle(P00, Vector3d.ZAxis, is2GraphObj.PointPointDistance(is2GraphTranslator.Tois2Graph(P00), P1));
				C2.SetDatabaseDefaults();
				acBlkTblRec.AppendEntity(C2);
				acTrans.AddNewlyCreatedDBObject(C2, true);

				//------------------------
				/* UBICO PUNTO AMARILLO */
				//------------------------
				PointType P2 = is2GraphObj.RotatePoint(P1, 10.0);
				DBPoint Pin2 = new DBPoint(is2GraphTranslator.ToAcad_3dG(P2));
				Pin2.ColorIndex = 40;		// AMARILLO
			  acBlkTblRec.AppendEntity(Pin2);
				acTrans.AddNewlyCreatedDBObject(Pin2, true);

				/* UBICO PUNTO VERDE CLARO */
				PointType P3 = is2GraphObj.RotatePoint(P2, -60.0);
				DBPoint Pin3 = new DBPoint(is2GraphTranslator.ToAcad_3dG(P3));
				Pin3.ColorIndex = 90;		// VERDE CLARO
				acBlkTblRec.AppendEntity(Pin3);
				acTrans.AddNewlyCreatedDBObject(Pin3, true);

				/* UBICO PUNTO AZUL */
				PointType P0 = new PointType(r, 0.0);
				//PointType P2 = new PolarPointType(new PointType(), );
				PointType P4 = is2GraphObj.RotatePoint(P0, 340);
				DBPoint Pin4 = new DBPoint(is2GraphTranslator.ToAcad_3dG(P4));
				Pin4.ColorIndex = 160;		// AZUL
				acBlkTblRec.AppendEntity(Pin4);
				acTrans.AddNewlyCreatedDBObject(Pin4, true);

				/* UBICO PUNTO CYAN */
				PointType P5 = is2GraphObj.RotatePoint(P1, 60.0, is2GraphTranslator.Tois2Graph(P00));
				DBPoint Pin5 = new DBPoint(is2GraphTranslator.ToAcad_3dG(P5));
				Pin5.ColorIndex = 4;		// CYAN
				acBlkTblRec.AppendEntity(Pin5);
				acTrans.AddNewlyCreatedDBObject(Pin5, true);

				/* UBICO PUNTO MAGENTA */
				PointType P6 = is2GraphObj.RotatePoint(P1, -75.0, is2GraphTranslator.Tois2Graph(P00));
				DBPoint Pin6 = new DBPoint(is2GraphTranslator.ToAcad_3dG(P6));
				Pin6.ColorIndex = 6;		// MAGENTA
				acBlkTblRec.AppendEntity(Pin6);
				acTrans.AddNewlyCreatedDBObject(Pin6, true);

				/* UBICO PUNTO ROJO */
				PointType P7 = is2GraphObj.RotatePoint(P3, 75.0);
				DBPoint Pin7 = new DBPoint(is2GraphTranslator.ToAcad_3dG(P7));
				Pin7.ColorIndex = 10;		// ROJO
				acBlkTblRec.AppendEntity(Pin7);
				acTrans.AddNewlyCreatedDBObject(Pin7, true);

				acTrans.Commit();
			}

			acDoc.SendStringToExecute("zoom e ", true, false, false);
			//acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: RotarSegmento -
		[CommandMethod("RotarSegmento")]
		public static void RotarSegmento()
		{
			Document acDoc = Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;

			using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
			{
				BlockTable acBlkTbl;
				acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

				BlockTableRecord acBlkTblRec;
				acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

				//--- Session donde se dibuja ---//
				/* UBICO PUNTO MARRON EN EL ORIGEN DE COORDENADA */
				DBPoint Pin1 = new DBPoint();
				Pin1.ColorIndex = 22;		// MARRON
				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 0.6;				
				acBlkTblRec.AppendEntity(Pin1);
				acTrans.AddNewlyCreatedDBObject(Pin1, true);

				/* UBICO PUNTO AMARILLO */
				Point3d P00 = new Point3d(15, 10, 0);
				DBPoint Pin2 = new DBPoint(P00);
				Pin2.ColorIndex = 40;		// AMARILLO
				acBlkTblRec.AppendEntity(Pin2);
				acTrans.AddNewlyCreatedDBObject(Pin2, true);

				/* DIBUJO LINEA BLANCA */
				SegmentType L00 = new SegmentType(new PointType(45, 20, 0), new PointType(70, 45, 0));
				Line L1 = is2GraphTranslator.ToAcad(L00);
				L1.SetDatabaseDefaults();
				acBlkTblRec.AppendEntity(L1);
				acTrans.AddNewlyCreatedDBObject(L1, true);

				//-- DIBUJO LINEA AZUL CLARO ---//
				/*SegmentType L00 = is2GraphObj.RotateSegment(is2GraphTranslator.Tois2Graph(S1), 35, is2GraphTranslator.Tois2Graph(P00));
				Line L3 = is2GraphTranslator.ToAcad(L00);
				L3.ColorIndex = 151;	// AZUL CLARO
				acBlkTblRec.AppendEntity(L3);
				acTrans.AddNewlyCreatedDBObject(L3, true);

				//--- DIBUJO LINEA MAGENTA ---//
				L00 = is2GraphObj.RotateSegment(is2GraphTranslator.Tois2Graph(S1), -60, is2GraphTranslator.Tois2Graph(P00));
				Line L3 = is2GraphTranslator.ToAcad(L00);
				L3.ColorIndex = 6;	// MAGENTA
				acBlkTblRec.AppendEntity(L3);
				acTrans.AddNewlyCreatedDBObject(L3, true);*/
				//-----------------------------------------------------------------------------------------

				/*//-- DIBUJO LINEA VERDE ---//
				SegmentType L = is2GraphObj.RotateSegment(is2GraphTranslator.Tois2Graph(S1), 55, L00.StartPoint);
				Line L3 = is2GraphTranslator.ToAcad(L);
				L3.ColorIndex = 3;	// VERDE
				acBlkTblRec.AppendEntity(L3);
				acTrans.AddNewlyCreatedDBObject(L3, true);

				//-- DIBUJO LINEA NARANJA ---//
				L = is2GraphObj.RotateSegment(is2GraphTranslator.Tois2Graph(S1), -75, L00.StartPoint);
				Line L4 = is2GraphTranslator.ToAcad(L);
				L4.ColorIndex = 30;	// NARANJA
				acBlkTblRec.AppendEntity(L4);
				acTrans.AddNewlyCreatedDBObject(L4, true);

				//-- DIBUJO LINEA ROJA ---//
				L = is2GraphObj.RotateSegment(is2GraphTranslator.Tois2Graph(S1), 370, L00.StartPoint);
				Line L5 = is2GraphTranslator.ToAcad(L);
				L5.ColorIndex = 10;	// ROJA
				acBlkTblRec.AppendEntity(L5);
				acTrans.AddNewlyCreatedDBObject(L5, true);*/
				//------------------------------------------------------------------------------------------

				//-- DIBUJO LINEA VERDE ---//
				/*SegmentType L = is2GraphObj.RotateSegment(is2GraphTranslator.Tois2Graph(S1), 55, L00.EndPoint);
				Line L3 = is2GraphTranslator.ToAcad(L);
				L3.ColorIndex = 3;	// VERDE
				acBlkTblRec.AppendEntity(L3);
				acTrans.AddNewlyCreatedDBObject(L3, true);

				//-- DIBUJO LINEA NARANJA ---//
				L = is2GraphObj.RotateSegment(is2GraphTranslator.Tois2Graph(S1), -75, L00.EndPoint);
				Line L4 = is2GraphTranslator.ToAcad(L);
				L4.ColorIndex = 30;	// NARANJA
				acBlkTblRec.AppendEntity(L4);
				acTrans.AddNewlyCreatedDBObject(L4, true);

				//-- DIBUJO LINEA ROJA ---//
				L = is2GraphObj.RotateSegment(is2GraphTranslator.Tois2Graph(S1), -370, L00.EndPoint);
				Line L5 = is2GraphTranslator.ToAcad(L);
				L5.ColorIndex = 10;	// ROJA
				acBlkTblRec.AppendEntity(L5);
				acTrans.AddNewlyCreatedDBObject(L5, true);*/
				//------------------------------------------------------------------------------------------

				SegmentType L = is2GraphObj.RotateSegment(is2GraphTranslator.Tois2Graph(L1), 55, L00.MidPoint);
				Line L3 = is2GraphTranslator.ToAcad(L);
				L3.ColorIndex = 3;	// VERDE
				acBlkTblRec.AppendEntity(L3);
				acTrans.AddNewlyCreatedDBObject(L3, true);

				//-- DIBUJO LINEA NARANJA ---//
				L = is2GraphObj.RotateSegment(is2GraphTranslator.Tois2Graph(L1), -75, L00.MidPoint);
				Line L4 = is2GraphTranslator.ToAcad(L);
				L4.ColorIndex = 30;	// NARANJA
				acBlkTblRec.AppendEntity(L4);
				acTrans.AddNewlyCreatedDBObject(L4, true);

				//-- DIBUJO LINEA ROJA ---//
				L = is2GraphObj.RotateSegment(is2GraphTranslator.Tois2Graph(L1), -370, L00.MidPoint);
				Line L5 = is2GraphTranslator.ToAcad(L);
				L5.ColorIndex = 10;	// ROJA
				acBlkTblRec.AppendEntity(L5);
				acTrans.AddNewlyCreatedDBObject(L5, true);

				acTrans.Commit();
			}

			acDoc.SendStringToExecute("zoom e ", true, false, false);
			//acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: Bisel -
		[CommandMethod("Bisel")]
		public static void Bisel()
		{
			Document acDoc = Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;

			using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
			{
				BlockTable acBlkTbl;
				acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

				BlockTableRecord acBlkTblRec;
				acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

				Point3d acP1, acP2, acP3;
				PointType P1, P2, P3, P4, P5, P6;
				SegmentType L00, L11;

				L00 = new SegmentType(new PointType(45, 20, 0), new PointType(70, 45, 0));
				L11 = new SegmentType(new PointType(65, -20, 0), new PointType(70, 35, 0));

				//L00 = new SegmentType(new PointType(45, 20, 0), new PointType(70, 45, 0));
				//L11 = new SegmentType(new PointType(70, 45, 0), new PointType(110, 45, 0));

				const double d1 = 10;
				const double d2 = 10;
				//is2GraphObj.SegmentSegmentChamfer(L00, L11, d1, d2, out P1, out P2);
				
				//--- Session donde se dibuja ---//
				if (acBlkTblRec == null) return;

				//acP1 = is2GraphTranslator.ToAcad_3d(P1);
				//acP2 = is2GraphTranslator.ToAcad_3d(P2);
				
				// DIBUJO LINEA #1
				Line S1 = is2GraphTranslator.ToAcad(L00);
				S1.SetDatabaseDefaults();
				acBlkTblRec.AppendEntity(S1);
				acTrans.AddNewlyCreatedDBObject(S1, true);

				// DIBUJO LINEA #2
				Line S2 = is2GraphTranslator.ToAcad(L11);
				S2.SetDatabaseDefaults();
				acBlkTblRec.AppendEntity(S2);
				acTrans.AddNewlyCreatedDBObject(S2, true);

				// UBICO PIN ROJO EN EL PUNTO #1 
				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 0.6;
				//acP1 = is2GraphTranslator.ToAcad_3d(P1);
				//DBPoint Pin1 = new DBPoint(acP1);
				//Pin1.ColorIndex = 1;		// ROJO
				//acBlkTblRec.AppendEntity(Pin1);
				//acTrans.AddNewlyCreatedDBObject(Pin1, true);

				// UBICO PIN ROJO EN EL PUNTO #2
				//acP2 = is2GraphTranslator.ToAcad_3d(P2);
				//DBPoint Pin2 = new DBPoint(acP2);
				//Pin2.ColorIndex = 1;		// ROJO
				//acBlkTblRec.AppendEntity(Pin2);
				//acTrans.AddNewlyCreatedDBObject(Pin2, true);
				
				acTrans.Commit();
			}

			acDoc.SendStringToExecute("zoom e ", true, false, false);
			//acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: Angulo_EntreLineas -
		[CommandMethod("Angulo_EntreLineas")]
    public static void AnguloEntreLineas()
    {
      Document acDoc = Application.DocumentManager.MdiActiveDocument;
      Database acCurDb = acDoc.Database;

      // Create radius TypedValue array to define the filter criteria
      TypedValue[] acTypValAr = new TypedValue[1];
      acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "LINE"), 0);

      // Assign the filter criteria to radius SelectionFilter object
      SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);

      // Request for objects to be selected in the drawing area
      PromptSelectionResult acSSPrompt;
      acSSPrompt = acDoc.Editor.GetSelection(acSelFtr);

      //----
      /*PromptSelectionResult pSelecRes;
      PromptSelectionOptions pSelecOpts = new PromptSelectionOptions();

      // Prompt for the start point
      pSelecOpts.SetKeywords("[lolo]/[pepe]", "lolo pepe");
      pSelecRes = acDocEd.GetSelection(pSelecOpts, acSelFtr);
      Point3d ptStart = pSelecRes.Value;*/

      // If the prompt status is OK, objects were selected
      if (acSSPrompt.Status == PromptStatus.OK)
      {
        SelectionSet acSSet = acSSPrompt.Value;

        //if (acSSet.Count == 2)
        {
          int i = 0;

          using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
          {
            BlockTable acBlkTbl;
            acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

            BlockTableRecord acBlkTblRec;
            acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

            //-----
            PointType is2P1;
            SegmentType S1, S2;

            S1 = new SegmentType();
            S2 = new SegmentType();

            // Step through the objects in the selection set
            foreach (SelectedObject acSSObj in acSSet)
            {
              // Check to make sure radius valid SelectedObject object was returned
              if (acSSObj != null)
              {
                // Open the selected object for write
                //Entity acEnt = acTrans.GetObject(acSSObj.ObjectId, OpenMode.ForWrite) as Entity;
                Line acEnt = acTrans.GetObject(acSSObj.ObjectId, OpenMode.ForRead) as Line;

                if (acEnt != null)
                {
                  // Change the object's color to Green
                  //acEnt.ColorIndex = 3;

                  switch (i)
                  {
                    case 0:
                      S1 = is2GraphTranslator.Tois2Graph(acEnt);
                      break;

                    case 1:
                      S2 = is2GraphTranslator.Tois2Graph(acEnt);
                      break;
                  }
                }
              }

              i++;
            }

            int angle = Convert.ToInt16(is2GraphObj.SegmentSegmentAngle(S1, S2));

            /*acCurDb.Pdmode = 34;
            acCurDb.Pdsize = 4.0;
            DBPoint Pin1 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P1));
            Pin1.ColorIndex = 3;
            acBlkTblRec.AppendEntity(Pin1);
            acTrans.AddNewlyCreatedDBObject(Pin1, true);*/
 
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("Ángulo: " + angle.ToString() + "º");

            // Save the new object to the database
            acTrans.Commit();
          }
        }
        //else
        //{
        //}

        //Application.ShowAlertDialog("Number of objects selected: " + acSSet.Count.ToString());
      }
      else
      {
        Autodesk.AutoCAD.ApplicationServices.Core.Application.ShowAlertDialog("Number of objects selected: 0");
      }
    }
		#endregion

		#region - Command: Segment_Segment_Intersect -
		[CommandMethod("Segment_Segment_Intersect")]
		public static void Segment_Segment_Intersect ()
    {
      Document acDoc = Application.DocumentManager.MdiActiveDocument;
      Database acCurDb = acDoc.Database;

      // Create radius TypedValue array to define the filter criteria
      TypedValue[] acTypValAr = new TypedValue[1];
      acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "LINE"), 0);

      // Assign the filter criteria to radius SelectionFilter object
      SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);

      // Request for objects to be selected in the drawing area
      PromptSelectionResult acSSPrompt;
      acSSPrompt = acDoc.Editor.GetSelection(acSelFtr);

      //----
      /*PromptSelectionResult pSelecRes;
      PromptSelectionOptions pSelecOpts = new PromptSelectionOptions();

      // Prompt for the start point
      pSelecOpts.SetKeywords("[lolo]/[pepe]", "lolo pepe");
      pSelecRes = acDocEd.GetSelection(pSelecOpts, acSelFtr);
      Point3d ptStart = pSelecRes.Value;*/

      // If the prompt status is OK, objects were selected
      if (acSSPrompt.Status == PromptStatus.OK)
      {
        SelectionSet acSSet = acSSPrompt.Value;

        //if (acSSet.Count == 2)
        {
          int i = 0;

          using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
          {
            BlockTable acBlkTbl;
            acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

            BlockTableRecord acBlkTblRec;
            acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

            //-----
            PointType is2P1;
            SegmentType S1, S2;

            S1 = new SegmentType();
            S2 = new SegmentType();

            // Step through the objects in the selection set
            foreach (SelectedObject acSSObj in acSSet)
            {
              // Check to make sure radius valid SelectedObject object was returned
              if (acSSObj != null)
              {
                // Open the selected object for write
                //Entity acEnt = acTrans.GetObject(acSSObj.ObjectId, OpenMode.ForWrite) as Entity;
                Line acEnt = acTrans.GetObject(acSSObj.ObjectId, OpenMode.ForRead) as Line;

                if (acEnt != null)
                {
                  // Change the object's color to Green
                  //acEnt.ColorIndex = 3;

                  switch (i)
                  {
                    case 0:
                      S1 = is2GraphTranslator.Tois2Graph(acEnt);
                    break;

                    case 1:
                      S2 = is2GraphTranslator.Tois2Graph(acEnt);
                    break;
                  }
                }
              }

              i++;
            }

            is2GraphObj.SegmentsApparentIntersect(S1, S2, out is2P1);

            acCurDb.Pdmode = 34;
            acCurDb.Pdsize = 0.3;
            DBPoint Pin = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P1));
            Pin.ColorIndex = 3;
            acBlkTblRec.AppendEntity(Pin);
            acTrans.AddNewlyCreatedDBObject(Pin, true);

            // Save the new object to the database
            acTrans.Commit();
          }
        }
        //else
        //{
        //}

        //Application.ShowAlertDialog("Number of objects selected: " + acSSet.Count.ToString());
      }
      else
      {
        Application.ShowAlertDialog("Number of objects selected: 0");
      }
    }
		#endregion

		#region - Command: Circle_Circle_Intersect -
		[CommandMethod("Circle_Circle_Intersect")]
		public static void CircleCircleIntersect()
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

				PointType is2P1, is2P2;
				CircleType is2C1, is2C2;

				var entities = UserSelectEntity("CIRCLE", 2, OpenMode.ForRead);
				is2C1 = is2GraphTranslator.Tois2Graph(entities[0] as Circle);
				is2C2 = is2GraphTranslator.Tois2Graph(entities[1] as Circle);

				byte intersec = is2GraphObj.CircleCircleIntersect(is2C1, is2C2, out is2P1, out is2P2);

				if (intersec != 0)
				{
					Line L = new Line(is2GraphTranslator.ToAcad_3d(is2P1), is2GraphTranslator.ToAcad_3d(is2P2));
					L.SetDatabaseDefaults();
					L.ColorIndex = 3;
					acBlkTblRec.AppendEntity(L);
					acTrans.AddNewlyCreatedDBObject(L, true);

					acCurDb.Pdmode = 34;
					acCurDb.Pdsize = 0.9;
					DBPoint Pin1 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P1));
					Pin1.ColorIndex = 1; // verde
					acBlkTblRec.AppendEntity(Pin1);
					acTrans.AddNewlyCreatedDBObject(Pin1, true);

					DBPoint Pin2 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P2));
					Pin2.ColorIndex = 1; // verde
					acBlkTblRec.AppendEntity(Pin2);
					acTrans.AddNewlyCreatedDBObject(Pin2, true);
				}

				CircleCircleRelation relation = is2GraphObj.CircleCircleRelationShip(is2C1, is2C2);
				string msg = "";
				switch (relation)
				{
					case CircleCircleRelation.Secant:
						msg = "Circulos Secantes. Se intersectan en 2 puntos.";
						break;
					case CircleCircleRelation.Tangent_In:
						msg = "Circulos Tangentes Interior. Se intersectan en 1 puntos";
						break;
					case CircleCircleRelation.Tangent_Out:
						msg = "Circulos Tangentes Exterior. Se intersectan en 1 puntos";
						break;
					case CircleCircleRelation.Concentric:
						msg = "Circulos Concentricos. No se Intersectan.";
						break;
					case CircleCircleRelation.Interior:
						msg = "Circulos Interiores. No se Intersectan.";
						break;
					case CircleCircleRelation.Exterior:
						msg = "Circulos Exteriores. No se Intersectan.";
						break;
					case CircleCircleRelation.Equal:
						msg = "Los Circulos son Iguales.";
						break;
				}
				Application.ShowAlertDialog(msg);

				// Save the new object to the database
				acTrans.Commit();
			}
    }
		#endregion

		#region - Command: Circle_Line_Intersect -
		[CommandMethod("Circle_Line_Intersect")]
		public static void CircleLineIntersect()
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
			
				LineType L;
				SegmentType S;
				CircleType C;

				PointType is2P1, is2P2;

				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione el Circulo");
				var selCircle = UserSelectEntity("CIRCLE", 1, OpenMode.ForRead);
				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione ahora la Recta");
				var seltLine = UserSelectEntity("LINE", 1, OpenMode.ForRead);

				C = is2GraphTranslator.Tois2Graph(selCircle[0] as Circle);
				S = is2GraphTranslator.Tois2Graph(seltLine[0] as Line);
				L = new LineType(S.StartPoint, S.Angle);

				/*Dictionary <string, int> dicSeleccion = new Dictionary <string, int>();
				dicSeleccion.Add("CIRCLE", 1);
				dicSeleccion.Add("LINE", 1);
				var selections = UserSelectEntity(dicSeleccion);

				C = is2GraphTranslator.Tois2Graph(selections[0] as Circle);
				C = is2GraphTranslator.Tois2Graph(selections[1] as Line);
				L = new LineType(C.StartPoint, C.Angle);*/


				PointType is2PP;
				LineType normalLine;
				
				normalLine = is2GraphObj.PerperdicularLineAt(L, C.Center);
				is2GraphObj.LineLineIntersect(L, normalLine, out is2PP);

				DBPoint Pin = new DBPoint(is2GraphTranslator.ToAcad_3d(is2PP));
				Pin.ColorIndex = 2; // amarillo
				acBlkTblRec.AppendEntity(Pin);
				acTrans.AddNewlyCreatedDBObject(Pin, true);

				Line normal_L = new Line(is2GraphTranslator.ToAcad_3d(C.Center), is2GraphTranslator.ToAcad_3d(is2PP));
				normal_L.SetDatabaseDefaults();
				normal_L.ColorIndex = 2;
				acBlkTblRec.AppendEntity(normal_L);
				acTrans.AddNewlyCreatedDBObject(normal_L, true);

				byte intersec = is2GraphObj.CircleLineIntersect(C, L, out is2P1, out is2P2);

				if (intersec != 0)
				{
					Line acL = new Line(is2GraphTranslator.ToAcad_3d(is2P1), is2GraphTranslator.ToAcad_3d(is2P2));
					acL.SetDatabaseDefaults();
					acL.ColorIndex = 3;
					acBlkTblRec.AppendEntity(acL);
					acTrans.AddNewlyCreatedDBObject(acL, true);

					acCurDb.Pdmode = 34;
					acCurDb.Pdsize = 0.3;
					DBPoint Pin1 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P1));
					Pin1.ColorIndex = 1; // rojo
					acBlkTblRec.AppendEntity(Pin1);
					acTrans.AddNewlyCreatedDBObject(Pin1, true);

					DBPoint Pin2 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P2));
					Pin2.ColorIndex = 1; // rojo
					acBlkTblRec.AppendEntity(Pin2);
					acTrans.AddNewlyCreatedDBObject(Pin2, true);
				}

				string msg;
				if (intersec == 0)
					msg = "La linea es EXTERIOR a la circunferencia. No la intersecta";
				else if (intersec == 1)
					msg = "La linea es TANGENTE a la circunferencia. La intersecta en 1 punto";
				else
					msg = "La linea es SECANTE a la circunferencia. La intersecta en 2 puntos";

				Application.ShowAlertDialog(msg);

				acTrans.Commit();
			}

			acDoc.SendStringToExecute("zoom e ", true, false, false);
			//acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: Arc_Line_Intersect -
		[CommandMethod("Arc_Line_Intersect")]
		public static void ArcLineIntersect()
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

				LineType L;
				SegmentType S;
				ArcType A;

				PointType is2P1, is2P2;

				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione el Arc");
				var selArc = UserSelectEntity("ARC", 1, OpenMode.ForRead);
				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione ahora la Recta");
				var seltLine = UserSelectEntity("LINE", 1, OpenMode.ForRead);

				A = is2GraphTranslator.Tois2Graph(selArc[0] as Arc);
				S = is2GraphTranslator.Tois2Graph(seltLine[0] as Line);
				L = new LineType(S.StartPoint, S.Angle);

				int intersec = is2GraphObj.ArcLineIntersect(A, L, out is2P1, out is2P2);

				if (intersec != 0)
				{
					Line acL = new Line(is2GraphTranslator.ToAcad_3d(is2P1), is2GraphTranslator.ToAcad_3d(is2P2));
					acL.SetDatabaseDefaults();
					acL.ColorIndex = 3; // verde
					acBlkTblRec.AppendEntity(acL);
					acTrans.AddNewlyCreatedDBObject(acL, true);

					acCurDb.Pdmode = 34;
					acCurDb.Pdsize = 0.9;
					DBPoint Pin1 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P1));
					Pin1.ColorIndex = 2; // amarillo
					acBlkTblRec.AppendEntity(Pin1);
					acTrans.AddNewlyCreatedDBObject(Pin1, true);

					DBPoint Pin2 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P2));
					Pin2.ColorIndex = 2; // amarillo
					acBlkTblRec.AppendEntity(Pin2);
					acTrans.AddNewlyCreatedDBObject(Pin2, true);
				}

				ArcEx Arc = is2GraphTranslatorEx.ToAcad(A, Vector3d.ZAxis);
				Arc.ColorIndex = 1;
				acBlkTblRec.AppendEntity(Arc);
				acTrans.AddNewlyCreatedDBObject(Arc, true);

				acTrans.Commit();
			}

			acDoc.SendStringToExecute("zoom e ", true, false, false);
			//acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: Arc_Segment_Intersect -
		[CommandMethod("Arc_Segment_Intersect")]
		public static void ArcSegmentIntersect ()
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

				LineType L;
				SegmentType S;
				ArcType A;

				PointType is2P1, is2P2;

				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione el Arc");
				var selArc = UserSelectEntity("ARC", 1, OpenMode.ForRead);
				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione ahora la Recta");
				var seltLine = UserSelectEntity("LINE", 1, OpenMode.ForRead);

				A = is2GraphTranslator.Tois2Graph(selArc[0] as Arc);
				S = is2GraphTranslator.Tois2Graph(seltLine[0] as Line);

				int intersec = is2GraphObj.ArcSegmentIntersect(A, S, out is2P1, out is2P2);

				if (intersec != 0)
				{
					Line acL = new Line(is2GraphTranslator.ToAcad_3d(is2P1), is2GraphTranslator.ToAcad_3d(is2P2));
					acL.SetDatabaseDefaults();
					acL.ColorIndex = 3; // verde
					acBlkTblRec.AppendEntity(acL);
					acTrans.AddNewlyCreatedDBObject(acL, true);

					acCurDb.Pdmode = 34;
					acCurDb.Pdsize = 0.9;
					DBPoint Pin1 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P1));
					Pin1.ColorIndex = 2; // amarillo
					acBlkTblRec.AppendEntity(Pin1);
					acTrans.AddNewlyCreatedDBObject(Pin1, true);

					DBPoint Pin2 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P2));
					Pin2.ColorIndex = 2; // amarillo
					acBlkTblRec.AppendEntity(Pin2);
					acTrans.AddNewlyCreatedDBObject(Pin2, true);
				}

				ArcEx Arc = is2GraphTranslatorEx.ToAcad(A, Vector3d.ZAxis);
				Arc.ColorIndex = 1;
				acBlkTblRec.AppendEntity(Arc);
				acTrans.AddNewlyCreatedDBObject(Arc, true);

				acTrans.Commit();
			}

			acDoc.SendStringToExecute("zoom e ", true, false, false);
			//acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: Arc_Arc_Intersect -
		[CommandMethod("Arc_Arc_Intersect")]
		public static void ArcArcIntersect()
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

				PointType is2P1, is2P2;
				ArcType is2C1, is2C2;

				var entities = UserSelectEntity("ARC", 2, OpenMode.ForRead);
				is2C1 = is2GraphTranslator.Tois2Graph(entities[0] as Arc);
				is2C2 = is2GraphTranslator.Tois2Graph(entities[1] as Arc);

				byte intersec = is2GraphObj.ArcArcIntersect(is2C1, is2C2, out is2P1, out is2P2);

				if (intersec != 0)
				{
					Line L = new Line(is2GraphTranslator.ToAcad_3d(is2P1), is2GraphTranslator.ToAcad_3d(is2P2));
					L.SetDatabaseDefaults();
					L.ColorIndex = 3;
					acBlkTblRec.AppendEntity(L);
					acTrans.AddNewlyCreatedDBObject(L, true);

					acCurDb.Pdmode = 34;
					acCurDb.Pdsize = 0.9;
					DBPoint Pin1 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P1));
					Pin1.ColorIndex = 1; // verde
					acBlkTblRec.AppendEntity(Pin1);
					acTrans.AddNewlyCreatedDBObject(Pin1, true);

					DBPoint Pin2 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P2));
					Pin2.ColorIndex = 1; // verde
					acBlkTblRec.AppendEntity(Pin2);
					acTrans.AddNewlyCreatedDBObject(Pin2, true);
				}

				// Save the new object to the database
				acTrans.Commit();
			}
		}
		#endregion

		#region - Command: Arc_Circle_Intersect -
		[CommandMethod("Arc_Circle_Intersect")]
		public static void ArcCircleIntersect()
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

				ArcType A;
				CircleType C;
				
				PointType is2P1, is2P2;

				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione el Arc");
				var selArc = UserSelectEntity("ARC", 1, OpenMode.ForRead);
				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione ahora el Circle");
				var seltCircle = UserSelectEntity("CIRCLE", 1, OpenMode.ForRead);

				A = is2GraphTranslator.Tois2Graph(selArc[0] as Arc);
				C = is2GraphTranslator.Tois2Graph(seltCircle[0] as Circle); 

				byte intersec = is2GraphObj.ArcCircleIntersect(A, C, out is2P1, out is2P2);

				if (intersec != 0)
				{
					Line acL = new Line(is2GraphTranslator.ToAcad_3d(is2P1), is2GraphTranslator.ToAcad_3d(is2P2));
					acL.SetDatabaseDefaults();
					acL.ColorIndex = 3; // verde
					acBlkTblRec.AppendEntity(acL);
					acTrans.AddNewlyCreatedDBObject(acL, true);

					acCurDb.Pdmode = 34;
					acCurDb.Pdsize = 0.9;
					DBPoint Pin1 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P1));
					Pin1.ColorIndex = 2; // amarillo
					acBlkTblRec.AppendEntity(Pin1);
					acTrans.AddNewlyCreatedDBObject(Pin1, true);

					DBPoint Pin2 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P2));
					Pin2.ColorIndex = 2; // amarillo
					acBlkTblRec.AppendEntity(Pin2);
					acTrans.AddNewlyCreatedDBObject(Pin2, true);
				}

				ArcEx Arc = is2GraphTranslatorEx.ToAcad(A, Vector3d.ZAxis);
				Arc.ColorIndex = 1;
				acBlkTblRec.AppendEntity(Arc);
				acTrans.AddNewlyCreatedDBObject(Arc, true);

				acTrans.Commit();
			}

			acDoc.SendStringToExecute("zoom e ", true, false, false);
			//acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: Segment_Segment_Fillet -
		[CommandMethod("Segment_Segment_Fillet")]
		public static void Segment_Segment_Fillet ()
		{
			Document acDoc = Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;

			using (var acTrans = acCurDb.TransactionManager.StartTransaction())
			{
				BlockTable acBlkTbl;
				acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

				BlockTableRecord acBlkTblRec;
				acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

				if (acBlkTblRec == null) return;

				PointType P1, P2, Pc;
				SegmentType L00, L11;

				acDoc.Editor.WriteMessage("Seleccione 2 Lineas");
				var seltLine = UserSelectEntity("LINE", 2, OpenMode.ForRead);

				PromptDoubleOptions pOptions = new PromptDoubleOptions("");
				pOptions.Message = "\nDefina el valor del radio para el Fillet: ";
				pOptions.DefaultValue = 10;
				var pRes = acDoc.Editor.GetDouble(pOptions);

				var optRight = new PromptIntegerOptions("Defina si Right or Left al Segmento #1: [1]Right, [0]Left");
				optRight.DefaultValue = 1;
				PromptIntegerResult resRight = acDoc.Editor.GetInteger(optRight);

				var optLeft = new PromptIntegerOptions("Defina si Right or Left al Segmento #2: [1]Right, [0]Left");
				optLeft.DefaultValue = 1;
				PromptIntegerResult resLeft = acDoc.Editor.GetInteger(optLeft);

				bool S1_right = (resRight.Value == 1);
				bool S2_right = (resLeft.Value == 1);

				L00 = is2GraphTranslator.Tois2Graph(seltLine[0] as Line);
				L11 = is2GraphTranslator.Tois2Graph(seltLine[1] as Line);

				is2GraphObj.SegmentSegmentFillet(L00, L11, pRes.Value, out P1, out P2, out Pc, S1_right, S2_right);
				ArcType is2Arc = is2GraphObj.SegmentSegmentFillet(L00, L11, pRes.Value, S1_right, S2_right);

				Point3d acP1, acP2, acPc;
				acP1 = is2GraphTranslator.ToAcad_3d(P1);
				acP2 = is2GraphTranslator.ToAcad_3d(P2);
				acPc = is2GraphTranslator.ToAcad_3d(Pc);

				// UBICO PIN AMARILLO EN EL PUNTO #1 
				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 0.6;
				DBPoint Pin1 = new DBPoint(acP1);
				Pin1.ColorIndex = 2;		// AMARILLO
				acBlkTblRec.AppendEntity(Pin1);
				acTrans.AddNewlyCreatedDBObject(Pin1, true);

				// UBICO PIN VERDE EN EL PUNTO #2
				DBPoint Pin2 = new DBPoint(acP2);
				Pin2.ColorIndex = 3;		// VERDE
				acBlkTblRec.AppendEntity(Pin2);
				acTrans.AddNewlyCreatedDBObject(Pin2, true);

				// UBICO PIN ROJO EN EL PUNTO #3
				DBPoint Pin3 = new DBPoint(acPc);
				Pin3.ColorIndex = 1;		// ROJO
				acBlkTblRec.AppendEntity(Pin3);
				acTrans.AddNewlyCreatedDBObject(Pin3, true);

				// Creo el Arco
				Arc A = is2GraphTranslatorEx.ToAcad(is2Arc, Vector3d.ZAxis);
				A.ColorIndex = 255;
				acBlkTblRec.AppendEntity(A);
				acTrans.AddNewlyCreatedDBObject(A, true);

				acTrans.Commit();
			}

			acDoc.SendStringToExecute("zoom e ", true, false, false);
			//acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: Segment_Segment_Fillet_Save -
		[CommandMethod("Segment_Segment_Fillet_Save")]
		public static void Segment_Segment_Fillet_Save()
		{
			Document acDoc = Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;

			using (var acTrans = acCurDb.TransactionManager.StartTransaction())
			{
				BlockTable acBlkTbl;
				acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

				BlockTableRecord acBlkTblRec;
				acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

				if (acBlkTblRec == null) return;

				PointType P1, P2, Pc;
				SegmentType L00, L11;

				acDoc.Editor.WriteMessage("Seleccione 2 Lineas");
				var seltLine = UserSelectEntity("LINE", 2, OpenMode.ForRead);

				PromptDoubleOptions pOptions = new PromptDoubleOptions("");
				pOptions.Message = "\nDefina el valor del radio para el Fillet: ";
				pOptions.DefaultValue = 10;
				var pRes = acDoc.Editor.GetDouble(pOptions);

				var optRight = new PromptIntegerOptions("Defina si Right or Left al Segmento #1: [1]Right, [0]Left");
				optRight.DefaultValue = 1;
				PromptIntegerResult resRight = acDoc.Editor.GetInteger(optRight);

				var optLeft = new PromptIntegerOptions("Defina si Right or Left al Segmento #2: [1]Right, [0]Left");
				optLeft.DefaultValue = 1;
				PromptIntegerResult resLeft = acDoc.Editor.GetInteger(optLeft);

				bool S1_right = (resRight.Value == 1);
				bool S2_right = (resLeft.Value == 1);

				L00 = is2GraphTranslator.Tois2Graph(seltLine[0] as Line);
				L11 = is2GraphTranslator.Tois2Graph(seltLine[1] as Line);

				is2GraphObj.SegmentSegmentFillet(L00, L11, pRes.Value, out P1, out P2, out Pc, S1_right, S2_right);
				ArcType is2Arc = is2GraphObj.SegmentSegmentFillet(L00, L11, pRes.Value, S1_right, S2_right);

				Point3d acP1, acP2, acPc;
				acP1 = is2GraphTranslator.ToAcad_3d(P1);
				acP2 = is2GraphTranslator.ToAcad_3d(P2);
				acPc = is2GraphTranslator.ToAcad_3d(Pc);

				// UBICO PIN AMARILLO EN EL PUNTO #1 
				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 0.6;
				DBPoint Pin1 = new DBPoint(acP1);
				Pin1.ColorIndex = 2;		// AMARILLO
				acBlkTblRec.AppendEntity(Pin1);
				acTrans.AddNewlyCreatedDBObject(Pin1, true);

				// UBICO PIN VERDE EN EL PUNTO #2
				DBPoint Pin2 = new DBPoint(acP2);
				Pin2.ColorIndex = 3;		// VERDE
				acBlkTblRec.AppendEntity(Pin2);
				acTrans.AddNewlyCreatedDBObject(Pin2, true);

				// UBICO PIN ROJO EN EL PUNTO #3
				DBPoint Pin3 = new DBPoint(acPc);
				Pin3.ColorIndex = 1;		// ROJO
				acBlkTblRec.AppendEntity(Pin3);
				acTrans.AddNewlyCreatedDBObject(Pin3, true);

				// Creo el Arco
				Arc A = is2GraphTranslatorEx.ToAcad(is2Arc, Vector3d.ZAxis);
				A.ColorIndex = 255;
				acBlkTblRec.AppendEntity(A);
				acTrans.AddNewlyCreatedDBObject(A, true);

				acTrans.Commit();
			}

			acDoc.SendStringToExecute("zoom e ", true, false, false);
			//acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: Circle_Segment_Fillet -
		[CommandMethod("Circle_Segment_Fillet")]
		public static void Circle_Segment_Fillet()
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

				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione el Circulo");
				var selCircle = UserSelectEntity("CIRCLE", 1, OpenMode.ForRead);
				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione ahora la Recta");
				var seltLine = UserSelectEntity("LINE", 1, OpenMode.ForRead);

				PromptDoubleOptions pRadioOpt = new PromptDoubleOptions("");
				pRadioOpt.Message = "\nDefina el valor del radio para el Fillet: ";
				pRadioOpt.DefaultValue = 10;

				PromptDoubleResult pResRadio = acDoc.Editor.GetDouble(pRadioOpt);

				PromptIntegerOptions optRight = new PromptIntegerOptions("Defina si Right or Left: [1]Right, [0]Left");
				optRight.DefaultValue = 1;
				PromptIntegerResult resRight = acDoc.Editor.GetInteger(optRight);

				PromptIntegerOptions optOut = new PromptIntegerOptions("Defina si Outside or Intern: [1]Outside, [0]Intern");
				optOut.DefaultValue = 1;
				PromptIntegerResult resOut = acDoc.Editor.GetInteger(optOut);

				PromptIntegerOptions optUp = new PromptIntegerOptions("Defina si Up or Down: [1]Up, [0]Down");
				optUp.DefaultValue = 1;
				PromptIntegerResult resUp = acDoc.Editor.GetInteger(optUp);

				bool right = (resRight.Value == 1);
				bool outside = (resOut.Value == 1);
				bool up = (resUp.Value == 1);

				SegmentType S;
				CircleType C;
				PointType P1, P2, Pc;

				C = is2GraphTranslator.Tois2Graph(selCircle[0] as Circle);
				S = is2GraphTranslator.Tois2Graph(seltLine[0] as Line);

				is2GraphObj.CircleSegmentFillet(C, S, pResRadio.Value, out P1, out P2, out Pc, right, outside, up);
				ArcType is2Arc = is2GraphObj.CircleSegmentFillet(C, S, pResRadio.Value, right, outside, up);

				Point3d acP1, acP2, acPc;
				acP1 = is2GraphTranslator.ToAcad_3d(P1);
				acP2 = is2GraphTranslator.ToAcad_3d(P2);
				acPc = is2GraphTranslator.ToAcad_3d(Pc);

				// UBICO PIN AMARILLO EN EL PUNTO #1 EN EL CIRCLE 
				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 0.6;
				DBPoint Pin1 = new DBPoint(acP1);
				Pin1.ColorIndex = 2;		// AMARILLO
				acBlkTblRec.AppendEntity(Pin1);
				acTrans.AddNewlyCreatedDBObject(Pin1, true);

				// UBICO PIN VERDE EN EL PUNTO #2 EN LA LINE
				DBPoint Pin2 = new DBPoint(acP2);
				Pin2.ColorIndex = 3;		// VERDE
				acBlkTblRec.AppendEntity(Pin2);
				acTrans.AddNewlyCreatedDBObject(Pin2, true);

				// UBICO PIN ROJO QUE ES EL CENTER DEL FILLET
				DBPoint Pin3 = new DBPoint(acPc);
				Pin3.ColorIndex = 1;		// ROJO
				acBlkTblRec.AppendEntity(Pin3);
				acTrans.AddNewlyCreatedDBObject(Pin3, true);

				// Creo el Arco
				Arc A = is2GraphTranslatorEx.ToAcad(is2Arc, Vector3d.ZAxis);
				A.ColorIndex = 255;
				acBlkTblRec.AppendEntity(A);
				acTrans.AddNewlyCreatedDBObject(A, true);
				
				acTrans.Commit();
			}

			acDoc.SendStringToExecute("zoom e ", true, false, false);
			//acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: Circle_Circle_Fillet -
		[CommandMethod("Circle_Circle_Fillet")]
		public static void Circle_Circle_Fillet ()
		{
			Document acDoc = Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;

			using (var acTrans = acCurDb.TransactionManager.StartTransaction())
			{
				BlockTable acBlkTbl;
				acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

				BlockTableRecord acBlkTblRec;
				acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

				if (acBlkTblRec == null) return;

				PointType P1, P2, Pc;
				CircleType C00, C11;

				acDoc.Editor.WriteMessage("Seleccione 2 Circle");
				var seltLine = UserSelectEntity("CIRCLE", 2, OpenMode.ForRead);

				PromptDoubleOptions pOptions = new PromptDoubleOptions("");
				pOptions.Message = "\nDefina el valor del radio para el Fillet: ";
				pOptions.DefaultValue = 10;
				var pRes = acDoc.Editor.GetDouble(pOptions);

				var optUp = new PromptIntegerOptions("Defina si Up or Down: [1]Up, [0]Down");
				optUp.DefaultValue = 1;
				PromptIntegerResult resUp = acDoc.Editor.GetInteger(optUp);

				var optOut_C1 = new PromptIntegerOptions("Defina si Outside or Inside al Circle #1: [1]Outside, [0]Inside");
				optOut_C1.DefaultValue = 1;
				PromptIntegerResult resOut_C1 = acDoc.Editor.GetInteger(optOut_C1);

				var optOut_C2 = new PromptIntegerOptions("Defina si Outside or Inside al Circle #2: [1]Outside, [0]Inside");
				optOut_C2.DefaultValue = 1;
				PromptIntegerResult resOut_C2 = acDoc.Editor.GetInteger(optOut_C2);

				bool up = (resUp.Value == 1);
				bool outside_c1 = (resOut_C1.Value == 1);
				bool outside_c2 = (resOut_C2.Value == 1);

				C00 = is2GraphTranslator.Tois2Graph(seltLine[0] as Circle);
				C11 = is2GraphTranslator.Tois2Graph(seltLine[1] as Circle);

				is2GraphObj.CircleCircleFillet(C00, C11, pRes.Value, out P1, out P2, out Pc, up, outside_c1, outside_c2);
				ArcType is2Arc = is2GraphObj.CircleCircleFillet(C00, C11, pRes.Value, up, outside_c1, outside_c2);

				Point3d acP1, acP2, acPc;
				acP1 = is2GraphTranslator.ToAcad_3d(P1);
				acP2 = is2GraphTranslator.ToAcad_3d(P2);
				acPc = is2GraphTranslator.ToAcad_3d(Pc);

				// UBICO PIN AMARILLO EN EL PUNTO #1 
				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 0.6;
				DBPoint Pin1 = new DBPoint(acP1);
				Pin1.ColorIndex = 2;		// AMARILLO
				acBlkTblRec.AppendEntity(Pin1);
				acTrans.AddNewlyCreatedDBObject(Pin1, true);

				// UBICO PIN VERDE EN EL PUNTO #2
				DBPoint Pin2 = new DBPoint(acP2);
				Pin2.ColorIndex = 3;		// VERDE
				acBlkTblRec.AppendEntity(Pin2);
				acTrans.AddNewlyCreatedDBObject(Pin2, true);

				// UBICO PIN ROJO EN EL PUNTO #3
				DBPoint Pin3 = new DBPoint(acPc);
				Pin3.ColorIndex = 1;		// ROJO
				acBlkTblRec.AppendEntity(Pin3);
				acTrans.AddNewlyCreatedDBObject(Pin3, true);

				// Creo el Arco
				Arc A = is2GraphTranslatorEx.ToAcad(is2Arc, Vector3d.ZAxis);
				A.ColorIndex = 255;
				acBlkTblRec.AppendEntity(A);
				acTrans.AddNewlyCreatedDBObject(A, true);

				acTrans.Commit();
			}

			acDoc.SendStringToExecute("zoom e ", true, false, false);
			//acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: Arc_Segment_Fillet -
		[CommandMethod("Arc_Segment_Fillet")]
		public static void Arc_Segment_Fillet ()
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

				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione el Arco");
				var selArc = UserSelectEntity("Arc", 1, OpenMode.ForRead);
				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione ahora la Recta");
				var seltLine = UserSelectEntity("LINE", 1, OpenMode.ForRead);

				PromptDoubleOptions pRadioOpt = new PromptDoubleOptions("");
				pRadioOpt.Message = "\nDefina el valor del radio para el Fillet: ";
				pRadioOpt.DefaultValue = 10;

				PromptDoubleResult pResRadio = acDoc.Editor.GetDouble(pRadioOpt);

				PromptIntegerOptions optRight = new PromptIntegerOptions("Defina si Right or Left: [1]Right, [0]Left");
				optRight.DefaultValue = 1;
				PromptIntegerResult resRight = acDoc.Editor.GetInteger(optRight);

				PromptIntegerOptions optOut = new PromptIntegerOptions("Defina si Outside or Intern: [1]Outside, [0]Intern");
				optOut.DefaultValue = 1;
				PromptIntegerResult resOut = acDoc.Editor.GetInteger(optOut);

				PromptIntegerOptions optUp = new PromptIntegerOptions("Defina si Up or Down: [1]Up, [0]Down");
				optUp.DefaultValue = 1;
				PromptIntegerResult resUp = acDoc.Editor.GetInteger(optUp);

				bool right = (resRight.Value == 1);
				bool outside = (resOut.Value == 1);
				bool up = (resUp.Value == 1);

				SegmentType S;
				ArcType A;
				PointType P1, P2, Pc;

				A = is2GraphTranslator.Tois2Graph(selArc[0] as Arc);
				S = is2GraphTranslator.Tois2Graph(seltLine[0] as Line);

				is2GraphObj.ArcSegmentFillet(A, S, pResRadio.Value, out P1, out P2, out Pc, right, outside, up);
				ArcType is2Arc = is2GraphObj.ArcSegmentFillet(A, S, pResRadio.Value, right, outside, up);

				Point3d acP1, acP2, acPc;
				acP1 = is2GraphTranslator.ToAcad_3d(P1);
				acP2 = is2GraphTranslator.ToAcad_3d(P2);
				acPc = is2GraphTranslator.ToAcad_3d(Pc);

				// UBICO PIN AMARILLO EN EL PUNTO #1 EN EL CIRCLE 
				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 0.6;
				DBPoint Pin1 = new DBPoint(acP1);
				Pin1.ColorIndex = 2;		// AMARILLO
				acBlkTblRec.AppendEntity(Pin1);
				acTrans.AddNewlyCreatedDBObject(Pin1, true);

				// UBICO PIN VERDE EN EL PUNTO #2 EN LA LINE
				DBPoint Pin2 = new DBPoint(acP2);
				Pin2.ColorIndex = 3;		// VERDE
				acBlkTblRec.AppendEntity(Pin2);
				acTrans.AddNewlyCreatedDBObject(Pin2, true);

				// UBICO PIN ROJO QUE ES EL CENTER DEL FILLET
				DBPoint Pin3 = new DBPoint(acPc);
				Pin3.ColorIndex = 1;		// ROJO
				acBlkTblRec.AppendEntity(Pin3);
				acTrans.AddNewlyCreatedDBObject(Pin3, true);

				// Creo el Arco
				/*Arc acA = is2GraphTranslatorEx.ToAcad(is2Arc, Vector3d.ZAxis);
				acBlkTblRec.AppendEntity(acA);
				acTrans.AddNewlyCreatedDBObject(acA, true);*/

				acTrans.Commit();
			}

			acDoc.SendStringToExecute("zoom e ", true, false, false);
			//acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: Arc_Arc_Fillet -
		[CommandMethod("Arc_Arc_Fillet")]
		public static void Arc_Arc_Fillet()
		{
			Document acDoc = Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;

			using (var acTrans = acCurDb.TransactionManager.StartTransaction())
			{
				BlockTable acBlkTbl;
				acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

				BlockTableRecord acBlkTblRec;
				acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

				if (acBlkTblRec == null) return;

				PointType P1, P2, Pc;
				ArcType A00, A11;

				acDoc.Editor.WriteMessage("Seleccione 2 Arcos");
				var selArc = UserSelectEntity("Arc", 2, OpenMode.ForRead);

				PromptDoubleOptions pOptions = new PromptDoubleOptions("");
				pOptions.Message = "\nDefina el valor del radio para el Fillet: ";
				pOptions.DefaultValue = 10;
				var pRes = acDoc.Editor.GetDouble(pOptions);

				var optUp = new PromptIntegerOptions("Defina si Up or Down: [1]Up, [0]Down");
				optUp.DefaultValue = 1;
				PromptIntegerResult resUp = acDoc.Editor.GetInteger(optUp);

				var optOut_C1 = new PromptIntegerOptions("Defina si Outside or Inside al Arc #1: [1]Outside, [0]Inside");
				optOut_C1.DefaultValue = 1;
				PromptIntegerResult resOut_C1 = acDoc.Editor.GetInteger(optOut_C1);

				var optOut_C2 = new PromptIntegerOptions("Defina si Outside or Inside al Arc #2: [1]Outside, [0]Inside");
				optOut_C2.DefaultValue = 1;
				PromptIntegerResult resOut_C2 = acDoc.Editor.GetInteger(optOut_C2);

				bool up = (resUp.Value == 1);
				bool outside_c1 = (resOut_C1.Value == 1);
				bool outside_c2 = (resOut_C2.Value == 1);

				A00 = is2GraphTranslator.Tois2Graph(selArc[0] as Arc);
				A11 = is2GraphTranslator.Tois2Graph(selArc[1] as Arc);

				is2GraphObj.ArcArcFillet(A00, A11, pRes.Value, out P1, out P2, out Pc, up, outside_c1, outside_c2);
				ArcType is2Arc = is2GraphObj.ArcArcFillet(A00, A11, pRes.Value, up, outside_c1, outside_c2);

				Point3d acP1, acP2, acPc;
				acP1 = is2GraphTranslator.ToAcad_3d(P1);
				acP2 = is2GraphTranslator.ToAcad_3d(P2);
				acPc = is2GraphTranslator.ToAcad_3d(Pc);

				// UBICO PIN AMARILLO EN EL PUNTO #1 
				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 0.6;
				DBPoint Pin1 = new DBPoint(acP1);
				Pin1.ColorIndex = 2;		// AMARILLO
				acBlkTblRec.AppendEntity(Pin1);
				acTrans.AddNewlyCreatedDBObject(Pin1, true);

				// UBICO PIN VERDE EN EL PUNTO #2
				DBPoint Pin2 = new DBPoint(acP2);
				Pin2.ColorIndex = 3;		// VERDE
				acBlkTblRec.AppendEntity(Pin2);
				acTrans.AddNewlyCreatedDBObject(Pin2, true);

				// UBICO PIN ROJO EN EL PUNTO #3
				DBPoint Pin3 = new DBPoint(acPc);
				Pin3.ColorIndex = 1;		// ROJO
				acBlkTblRec.AppendEntity(Pin3);
				acTrans.AddNewlyCreatedDBObject(Pin3, true);

				// Creo el Arco
				Arc A = is2GraphTranslatorEx.ToAcad(is2Arc, Vector3d.ZAxis);
				A.ColorIndex = 255;
				acBlkTblRec.AppendEntity(A);
				acTrans.AddNewlyCreatedDBObject(A, true);

				acTrans.Commit();
			}

			acDoc.SendStringToExecute("zoom e ", true, false, false);
			//acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: Line_Angle -
		[CommandMethod("Line_Angle")]
		public static void LineAngle()
		{			
			Document acDoc = Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;

			// Create radius TypedValue array to define the filter criteria
			TypedValue[] acTypValAr = new TypedValue[1];
			acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, "LINE"), 0);

			// Assign the filter criteria to radius SelectionFilter object
			SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);

			// Request for objects to be selected in the drawing area
			PromptSelectionResult acSSPrompt;
			acSSPrompt = acDoc.Editor.GetSelection(acSelFtr);

			//----
			/*PromptSelectionResult pSelecRes;
			PromptSelectionOptions pSelecOpts = new PromptSelectionOptions();

			// Prompt for the start point
			pSelecOpts.SetKeywords("[lolo]/[pepe]", "lolo pepe");
			pSelecRes = acDocEd.GetSelection(pSelecOpts, acSelFtr);
			Point3d ptStart = pSelecRes.Value;*/
			//-----

			PointType is2P1;			
			SegmentType is2S1, is2S2;
			
			is2S1 = new SegmentType();
			is2S2 = new SegmentType();

			// If the prompt status is OK, objects were selected
			if (acSSPrompt.Status == PromptStatus.OK)
			{
				SelectionSet acSSet = acSSPrompt.Value;

				//if (acSSet.Count == 2)
				{
					int i = 0;

					using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
					{
						BlockTable acBlkTbl;
						acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

						BlockTableRecord acBlkTblRec;
						acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

						// Step through the objects in the selection set
						foreach (SelectedObject acSSObj in acSSet)
						{
							// Check to make sure radius valid SelectedObject object was returned
							if (acSSObj != null)
							{
								// Open the selected object for write
								//Entity acEnt = acTrans.GetObject(acSSObj.ObjectId, OpenMode.ForWrite) as Entity;
								Line acEnt = acTrans.GetObject(acSSObj.ObjectId, OpenMode.ForRead) as Line;

								if (acEnt != null)
								{
									// Change the object's color to Green
									//acEnt.ColorIndex = 3;

									switch (i)
									{
										case 0:
											is2S1 = is2GraphTranslator.Tois2Graph(acEnt);
											break;

										case 1:
											is2S2 = is2GraphTranslator.Tois2Graph(acEnt);
											break;
									}
								}
							}

							i++;
						}

						double angleAgudo = is2GraphObj.SegmentSegmentAngle(is2S1, is2S2);
						double angleObtuso = is2GraphObj.SegmentSegmentAngle(is2S1, is2S2, false);
						is2GraphObj.SegmentsApparentIntersect(is2S1, is2S2, out is2P1);

						DBPoint Pin = is2GraphTranslator.ToAcad_DB(is2P1);
						acCurDb.Pdmode = 34;
						acCurDb.Pdsize = 0.6;
						Pin.ColorIndex = 3;
						acBlkTblRec.AppendEntity(Pin);
						acTrans.AddNewlyCreatedDBObject(Pin, true);

						// Save the new object to the database
						acTrans.Commit();

						Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Angulo Agudo: " + Convert.ToInt32(Math.Round(angleAgudo)) + " || Angulo Obtuso: " + Convert.ToInt32(Math.Round(angleObtuso)));
					}
				}
			}
			else
			{				
				Application.ShowAlertDialog("Number of objects selected: 0");
			}			

			//acDoc.SendStringToExecute("zoom e ", true, false, false);
			//acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: Punt_En_Linea -
		[CommandMethod("Punt_En_Linea")]
		public static void Point_In_Line()
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

				PointType is2P;
				LineType is2L;

				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione una linea");
				var entities = UserSelectEntity("LINE", 1, OpenMode.ForRead);
				is2L = is2GraphTranslator.Tois2Graph(entities[0] as Line).ConvertToLine();

				PromptPointResult pPtRes;
				PromptPointOptions pPtOpts = new PromptPointOptions("");

				// Prompt for the start point
				pPtOpts.Message = "\nSeleccion un punto del plano: ";
				pPtRes = acDoc.Editor.GetPoint(pPtOpts);

				// Exit if the user presses ESC or cancels the command
				if (pPtRes.Status == PromptStatus.Cancel) return;

				is2P = is2GraphTranslator.Tois2Graph(pPtRes.Value);
				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 2.9;
				DBPoint Pin1 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P));
				Pin1.ColorIndex = 1; // verde
				acBlkTblRec.AppendEntity(Pin1);
				acTrans.AddNewlyCreatedDBObject(Pin1, true);

				string msg;
				if (is2GraphObj.PointInLine(is2P, is2L))
					msg = "El punto pertenece a la linea";
				else
					msg = "El punto NO PERTENECE a la linea";

				Application.ShowAlertDialog(msg);

				// Save the new object to the database
				acTrans.Commit();
			}
		}
		#endregion

		#region - Command: Punto_En_Segmento -
		[CommandMethod("Punto_En_Segmento")]
		public static void Point_In_Segment()
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

				PointType is2P;
				SegmentType is2S;

				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione una linea");
				var entities = UserSelectEntity("LINE", 1, OpenMode.ForRead);
				is2S = is2GraphTranslator.Tois2Graph(entities[0] as Line);

				PromptPointResult pPtRes;
				PromptPointOptions pPtOpts = new PromptPointOptions("");

				// Prompt for the start point
				pPtOpts.Message = "\nSeleccion un punto del plano: ";
				pPtRes = acDoc.Editor.GetPoint(pPtOpts);
				
				// Exit if the user presses ESC or cancels the command
				if (pPtRes.Status == PromptStatus.Cancel) return;

				is2P = is2GraphTranslator.Tois2Graph(pPtRes.Value);
				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 2.9;
				DBPoint Pin1 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P));
				Pin1.ColorIndex = 1; // verde
				acBlkTblRec.AppendEntity(Pin1);
				acTrans.AddNewlyCreatedDBObject(Pin1, true);

				string msg;
				if (is2GraphObj.PointInSegment(is2P, is2S))
					msg = "El punto pertenece al segmento";
				else
					msg = "El punto NO PERTENECE al segmento";

				Application.ShowAlertDialog(msg);

				// Save the new object to the database
				acTrans.Commit();
			}
		}
		#endregion

		#region - Command: Punto_En_Circulo -
		[CommandMethod("Punt_En_Circulo")]
		public static void Point_In_Circle()
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

				PointType is2P;
				CircleType is2C;

				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione un circle");
				var entities = UserSelectEntity("CIRCLE", 1, OpenMode.ForRead);
				is2C = is2GraphTranslator.Tois2Graph(entities[0] as Circle);

				PromptPointResult pPtRes;
				PromptPointOptions pPtOpts = new PromptPointOptions("");

				// Prompt for the start point
				pPtOpts.Message = "\nSeleccion un punto del plano: ";
				pPtRes = acDoc.Editor.GetPoint(pPtOpts);

				// Exit if the user presses ESC or cancels the command
				if (pPtRes.Status == PromptStatus.Cancel) return;

				is2P = is2GraphTranslator.Tois2Graph(pPtRes.Value);
				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 2.9;
				DBPoint Pin1 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P));
				Pin1.ColorIndex = 1; // verde
				acBlkTblRec.AppendEntity(Pin1);
				acTrans.AddNewlyCreatedDBObject(Pin1, true);

				string msg;
				if (is2GraphObj.PointInCircle(is2P, is2C))
					msg = "El punto pertenece a la circunferencia";
				else
					msg = "El punto NO PERTENECE a la circunferencia";

				Application.ShowAlertDialog(msg);

				// Save the new object to the database
				acTrans.Commit();
			}
		}
		#endregion

		#region - Command: Punto_En_Arco -
		[CommandMethod("Punt_En_Arco")]
		public static void Point_In_Arc()
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

				PointType is2P;
				ArcType is2A;

				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione un arco");
				var entities = UserSelectEntity("ARC", 1, OpenMode.ForRead);
				is2A = is2GraphTranslator.Tois2Graph(entities[0] as Arc);

				PromptPointResult pPtRes;
				PromptPointOptions pPtOpts = new PromptPointOptions("");

				// Prompt for the start point
				pPtOpts.Message = "\nSeleccion un punto del plano: ";
				pPtRes = acDoc.Editor.GetPoint(pPtOpts);

				// Exit if the user presses ESC or cancels the command
				if (pPtRes.Status == PromptStatus.Cancel) return;

				is2P = is2GraphTranslator.Tois2Graph(pPtRes.Value);
				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 2.9;
				DBPoint Pin1 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P));
				Pin1.ColorIndex = 1; // verde
				acBlkTblRec.AppendEntity(Pin1);
				acTrans.AddNewlyCreatedDBObject(Pin1, true);

				string msg;
				if (is2GraphObj.PointInArc(is2P, is2A))
					msg = "El punto pertenece al arco";
				else
					msg = "El punto NO PERTENECE al arco";

				Application.ShowAlertDialog(msg);

				// Save the new object to the database
				acTrans.Commit();
			}
		}
		#endregion

		#region - Command: Point_Line_Distace -
		[CommandMethod("Point_Line_Distace")]
		public static void Point_Line_Distace()
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

				PointType is2P;
				LineType is2L;

				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione una linea");
				var entities = UserSelectEntity("LINE", 1, OpenMode.ForRead);
				is2L = is2GraphTranslator.Tois2Graph(entities[0] as Line).ConvertToLine();

				PromptPointResult pPtRes;
				PromptPointOptions pPtOpts = new PromptPointOptions("");

				// Prompt for the start point
				pPtOpts.Message = "\nSeleccion un punto del plano: ";
				pPtRes = acDoc.Editor.GetPoint(pPtOpts);

				// Exit if the user presses ESC or cancels the command
				if (pPtRes.Status == PromptStatus.Cancel) return;

				is2P = is2GraphTranslator.Tois2Graph(pPtRes.Value);
				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 2.9;
				DBPoint Pin1 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P));
				Pin1.ColorIndex = 1; // verde
				acBlkTblRec.AppendEntity(Pin1);
				acTrans.AddNewlyCreatedDBObject(Pin1, true);

				double dist = is2GraphObj.PointLineDistance(is2P, is2L);
				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Distancia Point-Line: " + dist);

				// Save the new object to the database
				acTrans.Commit();
			}
		}
		#endregion

		#region - Command: Debug_Fillet -
		[CommandMethod("Debug_Fillet_And_ArEx")]
		public static void Debug_Fillet_And_ArEx()
		{
			Document acDoc = Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;

			using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
			{
				BlockTable acBlkTbl;
				acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

				BlockTableRecord acBlkTblRec;
				acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

				Point3d acP1, acP2, acP3;
				PointType P1, P2, P3, P4, P5, P6, Px;
				SegmentType L00, L11;

				L00 = new SegmentType(new PointType(45, 20, 0), new PointType(70, 45, 0));
				L11 = new SegmentType(new PointType(65, -20, 0), new PointType(70, 35, 0));

				//L11 = new SegmentType(new PointType(45, 70, 0), new PointType(45, 20, 0));
				//L00 = new SegmentType(new PointType(45, 20, 0), new PointType(15, 20, 0));
				//Point3d any = new Point3d(41, 23, 0.0);

				const double R1 = 10;
				is2GraphObj.SegmentSegmentFillet(L00, L11, R1, out P1, out P2, out P3);

				//--- Session donde se dibuja ---//
				if (acBlkTblRec == null) return;

				acP1 = is2GraphTranslator.ToAcad_3d(P1);
				acP2 = is2GraphTranslator.ToAcad_3d(P2);
				acP3 = is2GraphTranslator.ToAcad_3d(P3);

				// Variante #1: (Start, Any, End) ---OK---|---OK---
				//Point3d any = new Point3d(56.5663, 26.1523, 0.0);
				Point3d any = new Point3d(56.5663, 36.1523, 0.0);
				//Point3d any = new Point3d(53.6981, 20.4992, 0.0);
				//Point3d any = new Point3d(41.2558, 13.5176, 0.0);
				//Point3d any = new Point3d(45.0522, 13.1313, 0.0);
				//Point3d any = new Point3d(8.4576, -21.4657, 0.0);
				//Point3d any = new Point3d(20.0422, -15.1145, 0.0);
				AcadUtilities.AddPinToModel(is2GraphTranslator.ToAcad_DB(any), 34, 0.6, 6);
				ArcEx Arc = new ArcEx(new StartPointEx(acP1), new AnyPointEx(any), new EndPointEx(acP2), Vector3d.ZAxis);
				//ArcEx Arc = new ArcEx(new StartPointEx(acP1), new AnyPointEx(new Point3d(57.6798, 29.5580, 0.0)), new EndPointEx(acP2), Vector3d.ZAxis);
				//--------------------
				/*P4 = is2GraphTranslator.Tois2Graph(acP1);
				P5 = is2GraphTranslator.Tois2Graph(acP2);
				Px = is2GraphTranslator.Tois2Graph(any);
				//ArcType A = new ArcType(new ArcType.StartPointEx(P4), new ArcType.AnyPointEx(Px), new ArcType.EndPointEx(P5));
				ArcType A = new ArcType(new ArcType.StartPointEx(P5), new ArcType.AnyPointEx(Px), new ArcType.EndPointEx(P4));
				ArcEx Arc = is2GraphTranslatorEx.ToAcad(A, Vector3d.ZAxis);*/
				//--------------------

				// Variante #2: (Start, Center, End) ---OK---|---OK---
				//ArcEx Arc = new ArcEx(new StartPointEx(acP1), new CenterPointEx(acP3), new EndPointEx(acP2), Vector3d.ZAxis, false);
				//--------------------
				/*P4 = is2GraphTranslator.Tois2Graph(acP1);
				P5 = is2GraphTranslator.Tois2Graph(acP2);
				P6 = is2GraphTranslator.Tois2Graph(acP3);
				ArcType A = new ArcType(new ArcType.StartPointEx(P4), new ArcType.CenterPointEx(P6), new ArcType.EndPointEx(P5), false);
				//ArcType A = new ArcType(new ArcType.StartPointEx(P5), new ArcType.CenterPointEx(P6), new ArcType.EndPointEx(P4), false);
				ArcEx Arc = is2GraphTranslator.ToAcad(A, Vector3d.ZAxis);*/
				//--------------------

				// Variante #3: (Start, Center, Angle) ---OK---|---OK---
				//ArcEx Arc = new ArcEx(new StartPointEx(acP1), new CenterPointEx(acP3), new GradeAngleEx(-90), Vector3d.ZAxis, false);
				//--------------------
				/*P4 = is2GraphTranslator.Tois2Graph(acP1);
				P5 = is2GraphTranslator.Tois2Graph(acP2);
				P6 = is2GraphTranslator.Tois2Graph(acP3);
				ArcType A = new ArcType(new ArcType.StartPointEx(P4), new ArcType.CenterPointEx(P6), new ArcType.GradeAngleEx(90), false);
				//ArcType A = new ArcType(new ArcType.StartPointEx(P5), new ArcType.CenterPointEx(P6), new ArcType.GradeAngleEx(90), true);
				ArcEx Arc = is2GraphTranslator.ToAcad(A, Vector3d.ZAxis);*/
				//--------------------

				// Variante #4: (Start, Center, Lenght) ---OK---|---OK---
				//ArcEx Arc = new ArcEx(new StartPointEx(acP1), new CenterPointEx(acP3), new DistanceEx(29.33), Vector3d.ZAxis, false);
				//--------------------
				/*P4 = is2GraphTranslator.Tois2Graph(acP1);
				P5 = is2GraphTranslator.Tois2Graph(acP2);
				P6 = is2GraphTranslator.Tois2Graph(acP3);
				//double dist = 18.8024;
				//double dist = 14.1421;
				double dist = 19.33;
				ArcType A = new ArcType(new ArcType.StartPointEx(P4), new ArcType.CenterPointEx(P6), new ArcType.DistanceEx(dist), false);
				//ArcType A = new ArcType(new ArcType.StartPointEx(P5), new ArcType.CenterPointEx(P6), new ArcType.DistanceEx(dist), !false);
				ArcEx Arc = is2GraphTranslator.ToAcad(A, Vector3d.ZAxis);*/
				//--------------------

				// Variante #5: (Start, End, Angle) ---OK---|---OK---
				//ArcEx Arc = new ArcEx (new StartPointEx(acP1), new EndPointEx(acP2), new GradeAngleEx(90), Vector3d.ZAxis, false);
				//--------------------
				/*P4 = is2GraphTranslator.Tois2Graph(acP1);
				P5 = is2GraphTranslator.Tois2Graph(acP2);
				ArcType A = new ArcType(new ArcType.StartPointEx(P4), new ArcType.EndPointEx(P5), new ArcType.GradeAngleEx(90), !false);
				//ArcType A = new ArcType(new ArcType.StartPointEx(P5), new ArcType.EndPointEx(P4), new ArcType.GradeAngleEx(90), false);
				ArcEx Arc = is2GraphTranslator.ToAcad(A, Vector3d.ZAxis);*/
				//--------------------

				// Variante #6: (Start, Center, ArcDirection)
				//ArcEx Arc = new ArcEx ()

				// Variante #7: (Start, End, ArcRadius) ---OK---|---OK---
				//ArcEx Arc = new ArcEx(new StartPointEx(acP1), new EndPointEx(acP2), new ArcRadius(R1+0), Vector3d.ZAxis, false);
				//--------------------
				/*P4 = is2GraphTranslator.Tois2Graph(acP1);
				P5 = is2GraphTranslator.Tois2Graph(acP2);
				ArcType A = new ArcType(new ArcType.StartPointEx(P4), new ArcType.EndPointEx(P5), new ArcType.ArcRadius(R1 + 0), false);
				//ArcType A = new ArcType(new ArcType.StartPointEx(P5), new ArcType.EndPointEx(P4), new ArcType.ArcRadius(R1 + 0), !false);
				//ArcType A = new ArcType(new ArcType.StartPointEx(P4), new ArcType.EndPointEx(P5), new ArcType.ArcRadius(9.4027), true);
				ArcEx Arc = is2GraphTranslator.ToAcad(A, Vector3d.ZAxis);*/
				//--------------------

				// Variante #8: (Center, Start, End) ---OK---|
				//ArcEx Arc = new ArcEx(new CenterPointEx(acP3), new StartPointEx(acP1), new EndPointEx(acP2), Vector3d.ZAxis, false);
				//--------------------
				/*P4 = is2GraphTranslator.Tois2Graph(acP1);
				P5 = is2GraphTranslator.Tois2Graph(acP2);
				P6 = is2GraphTranslator.Tois2Graph(acP3);
				ArcType A = new ArcType(new ArcType.StartPointEx(P4), new ArcType.CenterPointEx(P6), new ArcType.EndPointEx(P5), false);
				ArcEx Arc = is2GraphTranslator.ToAcad(A, Vector3d.ZAxis);*/
				//--------------------

				// Variante #9: (Center, Start, Angle) ---OK---|
				//ArcEx Arc = new ArcEx(new CenterPointEx(acP3), new StartPointEx(acP1), new GradeAngleEx(150), Vector3d.ZAxis, false);
				//--------------------
				/*P4 = is2GraphTranslator.Tois2Graph(acP1);
				P6 = is2GraphTranslator.Tois2Graph(acP3);
				ArcType A = new ArcType(new ArcType.StartPointEx(P4), new ArcType.CenterPointEx(P6), new ArcType.GradeAngleEx(90), false);
				ArcEx Arc = is2GraphTranslator.ToAcad(A, Vector3d.ZAxis);*/
				//--------------------

				// Variante #10: (Start, Center, Lenght) ---OK---|
				//ArcEx Arc = new ArcEx(new CenterPointEx(acP3), new StartPointEx(acP1), new DistanceEx(18.8054), Vector3d.ZAxis, true);
				/*P4 = is2GraphTranslator.Tois2Graph(acP1);
				P6 = is2GraphTranslator.Tois2Graph(acP3);
				ArcType A = new ArcType(new ArcType.StartPointEx(P4), new ArcType.CenterPointEx(P6), new ArcType.DistanceEx(14.1421), false);
				ArcEx Arc = is2GraphTranslator.ToAcad(A, Vector3d.ZAxis);*/
				//--------------------

				// Variante #11-A: (Continuos with Line, End)
				//ArcEx Arc = new ArcEx ()

				// Variante #11-B: (Continuos with Arc, End)
				//ArcEx Arc = new ArcEx ()

				// Variante #12: (Start, End, Perimeter)
				//ArcEx Arc = new ArcEx ()

				// DIBUJO EL ARCO
				Arc.SetDatabaseDefaults();
				acBlkTblRec.AppendEntity(Arc);
				acTrans.AddNewlyCreatedDBObject(Arc, true);

				// DIBUJO LINEA #1
				Line S1 = is2GraphTranslator.ToAcad(L00);
				S1.SetDatabaseDefaults();
				acBlkTblRec.AppendEntity(S1);
				acTrans.AddNewlyCreatedDBObject(S1, true);

				// DIBUJO LINEA #2
				Line S2 = is2GraphTranslator.ToAcad(L11);
				S2.SetDatabaseDefaults();
				acBlkTblRec.AppendEntity(S2);
				acTrans.AddNewlyCreatedDBObject(S2, true);

				// UBICO PIN AMARILLO EN EL PUNTO #1 
				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 0.6;
				DBPoint Pin1 = new DBPoint(acP1);
				Pin1.ColorIndex = 2;		// AMARILLO
				acBlkTblRec.AppendEntity(Pin1);
				acTrans.AddNewlyCreatedDBObject(Pin1, true);

				// UBICO PIN VERDE EN EL PUNTO #2
				DBPoint Pin2 = new DBPoint(acP2);
				Pin2.ColorIndex = 3;		// VERDE
				acBlkTblRec.AppendEntity(Pin2);
				acTrans.AddNewlyCreatedDBObject(Pin2, true);

				// UBICO PIN ROJO EN EL PUNTO #3
				DBPoint Pin3 = new DBPoint(acP3);
				Pin3.ColorIndex = 1;		// ROJO
				acBlkTblRec.AppendEntity(Pin3);
				acTrans.AddNewlyCreatedDBObject(Pin3, true);

				// isColinearSegment
				/*// DIBUJO LINEA #3
				SegmentType S1 = new SegmentType (new PointType(45, 20, 0), new PointType(70, 45, 0));
				Line L3 = is2GraphTranslator.ToAcad(S1);
				L3.SetDatabaseDefaults();
				acBlkTblRec.AppendEntity(L3);
				acTrans.AddNewlyCreatedDBObject(L3, true);

				// DIBUJO LINEA #4
				SegmentType S2 = new SegmentType(new PointType(90, 45, 0), new PointType(65, 20, 0));
				Line L4 = is2GraphTranslator.ToAcad(S2);
				L4.SetDatabaseDefaults();
				acBlkTblRec.AppendEntity(L4);
				acTrans.AddNewlyCreatedDBObject(L4, true);

				bool A = is2GraphObj.isColinearSegment(S1, S2);*/

				acTrans.Commit();
			}

			acDoc.SendStringToExecute("zoom e ", true, false, false);
			//acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: Debug_CircleLine_Intersect -
		[CommandMethod("Debug_CircleLine_Intersect")]
		public static void Debug_CircleLine_Intersect()
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

				double A, B, C;
				PointType P1, P2, P3, P4, P5, P6, midP;

				P1 = new PointType(-20.0244, 0.0009, 0.0);
				P2 = new PointType(-20.0000, 0.0, 0.0);

				const double radio = 0.0244;

				CircleType C1 = new CircleType(P1, radio);
				CircleType C2 = new CircleType(P2, radio);
				midP = is2GraphObj.MidPointBetweenPoint(P1, P2);

				//double angle = is2GraphObj.PointPointAngle(P1, P2);
				//LineType L = new LineType(midP, angle + 90.0);

				//is2GraphObj.LineCoefficient(L, out A, out B, out C);

				P3 = new PointType(-20.0232, 0.0443, 0.0);
				P4 = new PointType(-19.9865, -0.0131, 0.0);

				double angle = is2GraphObj.PointPointAngle(P3, P4);
				LineType L = new LineType(P3, angle);

				//is2GraphObj.SetPresicion(4);
				int intersect = is2GraphObj.CircleLineIntersect(C1, L, out P5, out P6);

				Circle acC1 = is2GraphTranslator.ToAcad(C1, Vector3d.ZAxis);
				acC1.ColorIndex = 150;
				acBlkTblRec.AppendEntity(acC1);
				acTrans.AddNewlyCreatedDBObject(acC1, true);

				Circle acC2 = is2GraphTranslator.ToAcad(C2, Vector3d.ZAxis);
				acC2.ColorIndex = 150;
				acBlkTblRec.AppendEntity(acC2);
				acTrans.AddNewlyCreatedDBObject(acC2, true);

				DBPoint PinMid = new DBPoint(is2GraphTranslator.ToAcad_3d(midP));
				PinMid.ColorIndex = 1; // ROJO
				acBlkTblRec.AppendEntity(PinMid);
				acTrans.AddNewlyCreatedDBObject(PinMid, true);

				/*Line Secante = new Line(new Point3d(-20.0114, 0.0216, 0.0), new Point3d(-20.0130, -0.0207, 0.0));
				Secante.SetDatabaseDefaults();
				acBlkTblRec.AppendEntity(Secante);
				acTrans.AddNewlyCreatedDBObject(Secante, true);*/

				Line Tangente = new Line(is2GraphTranslator.ToAcad_3d(P3), is2GraphTranslator.ToAcad_3d(P4));
				Tangente.SetDatabaseDefaults();
				acBlkTblRec.AppendEntity(Tangente);
				acTrans.AddNewlyCreatedDBObject(Tangente, true);

				DBPoint PinTan1 = new DBPoint(is2GraphTranslator.ToAcad_3d(P5));
				PinTan1.ColorIndex = 4; // 
				acBlkTblRec.AppendEntity(PinTan1);
				acTrans.AddNewlyCreatedDBObject(PinTan1, true);
				DBPoint PinTan2 = new DBPoint(is2GraphTranslator.ToAcad_3d(P5));
				PinTan2.ColorIndex = 4; // 
				acBlkTblRec.AppendEntity(PinTan2);
				acTrans.AddNewlyCreatedDBObject(PinTan2, true);

				Line ejeCentral = new Line(is2GraphTranslator.ToAcad_3d(P1), is2GraphTranslator.ToAcad_3d(P2));
				ejeCentral.ColorIndex = 11;
				acBlkTblRec.AppendEntity(ejeCentral);
				acTrans.AddNewlyCreatedDBObject(ejeCentral, true);

				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 0.0006;

				if (intersect != 0)
				{
					Line ejeRadial = new Line(is2GraphTranslator.ToAcad_3d(P3), is2GraphTranslator.ToAcad_3d(P4));
					ejeRadial.ColorIndex = 11;
					acBlkTblRec.AppendEntity(ejeRadial);
					acTrans.AddNewlyCreatedDBObject(ejeRadial, true);

					DBPoint Pin1 = new DBPoint(is2GraphTranslator.ToAcad_3d(P3));
					Pin1.ColorIndex = 3; // Verde
					acBlkTblRec.AppendEntity(Pin1);
					acTrans.AddNewlyCreatedDBObject(Pin1, true);

					DBPoint Pin2 = new DBPoint(is2GraphTranslator.ToAcad_3d(P4));
					Pin2.ColorIndex = 3; // Verde
					acBlkTblRec.AppendEntity(Pin2);
					acTrans.AddNewlyCreatedDBObject(Pin2, true);
				}

				acTrans.Commit();
			}

			acDoc.SendStringToExecute("zoom e ", true, false, false);
			acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: Debug_CircleCircle_Intersect -
		[CommandMethod("Debug_CircleCircle_Intersect")]
		public static void Debug_CircleCircle_Intersect()
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

				PointType P1, P2, P3, P4, midP;

				P1 = new PointType(-20.0244, 0.0009, 0.0);
				P2 = new PointType(-20.0000, 0.0, 0.0);

				double dist = 0.0244;

				CircleType C1 = new CircleType(P1, dist);
				CircleType C2 = new CircleType(P2, dist);
				midP = is2GraphObj.MidPointBetweenPoint(P1, P2);

				double angle = is2GraphObj.PointPointAngle(P1, P2);
				LineType L = new LineType(midP, angle + 90.0);

				byte intersect = is2GraphObj.CircleCircleIntersect(C1, C2, out P3, out P4);
				//int intersect = is2GraphObj.LineCircleIntersect(C, L, out P3, out P4);
				//int intersect = is2GraphObj.CircIntercepMarquez(R, C2, out P3, out P4);

				/*CircleType C3 = new CircleType(new PointType(6.0, 6.0), 6.0);
				CircleType C4 = new CircleType(new PointType(8.0, 2.0), 4.0);
				
				int intersect = is2GraphObj.CircleCircleIntersect(C3, C4, out P3, out P4);*/


				Circle acC1 = is2GraphTranslator.ToAcad(C1, Vector3d.ZAxis);
				acC1.ColorIndex = 150;
				acBlkTblRec.AppendEntity(acC1);
				acTrans.AddNewlyCreatedDBObject(acC1, true);

				Circle acC2 = is2GraphTranslator.ToAcad(C2, Vector3d.ZAxis);
				acC2.ColorIndex = 150;
				acBlkTblRec.AppendEntity(acC2);
				acTrans.AddNewlyCreatedDBObject(acC2, true);

				DBPoint PinMid = new DBPoint(is2GraphTranslator.ToAcad_3d(midP));
				PinMid.ColorIndex = 1; // ROJO
				acBlkTblRec.AppendEntity(PinMid);
				acTrans.AddNewlyCreatedDBObject(PinMid, true);

				/*Line Secante = new Line(new Point3d(-20.0114, 0.0216, 0.0), new Point3d(-20.0130, -0.0207, 0.0));
				Secante.SetDatabaseDefaults();
				acBlkTblRec.AppendEntity(Secante);
				acTrans.AddNewlyCreatedDBObject(Secante, true);*/

				Line ejeCentral = new Line(is2GraphTranslator.ToAcad_3d(P1), is2GraphTranslator.ToAcad_3d(P2));
				ejeCentral.ColorIndex = 11;
				acBlkTblRec.AppendEntity(ejeCentral);
				acTrans.AddNewlyCreatedDBObject(ejeCentral, true);

				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 0.0006;

				if (intersect != 0)
				{
					Line ejeRadial = new Line(is2GraphTranslator.ToAcad_3d(P3), is2GraphTranslator.ToAcad_3d(P4));
					ejeRadial.ColorIndex = 11;
					acBlkTblRec.AppendEntity(ejeRadial);
					acTrans.AddNewlyCreatedDBObject(ejeRadial, true);

					DBPoint Pin1 = new DBPoint(is2GraphTranslator.ToAcad_3d(P3));
					Pin1.ColorIndex = 3; // Verde
					acBlkTblRec.AppendEntity(Pin1);
					acTrans.AddNewlyCreatedDBObject(Pin1, true);

					DBPoint Pin2 = new DBPoint(is2GraphTranslator.ToAcad_3d(P4));
					Pin2.ColorIndex = 3; // Verde
					acBlkTblRec.AppendEntity(Pin2);
					acTrans.AddNewlyCreatedDBObject(Pin2, true);
				}

				acTrans.Commit();
			}

			acDoc.SendStringToExecute("zoom e ", true, false, false);
			acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: Debug_Line -
		[CommandMethod("Debug_Line")]
		public static void DebugLine()
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

				double A, B, C;
				LineType L1 = new LineType(new PointType(3.0, -4.0), 21.80);
				is2GraphObj.LineCoefficient(L1, out A, out B, out C);

				//try
				//{
				//Line pLine1 = new Line(new Point3d(0, 0, 0), new Point3d(1, 1, 0));
				//Line pLine2 = new Line(new Point3d(1, 1, 0), new Point3d(4, 1, 0));

				// Join the second line to the first line 
				//pLine1.JoinEntity(pLine2);

				//acBlkTblRec.AppendEntity(pLine1);
				//acTrans.AddNewlyCreatedDBObject(pLine1, true);
				/*}
				catch (Autodesk.AutoCAD.Runtime.Exception e)
				{
					Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("nJoin error: "); 
					Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(e.ErrorStatus.ToString());
				}*/

				acTrans.Commit();
			}
		}
		#endregion

		#region - Command: Debug_Arc -
		[CommandMethod("Debug_Arc_Traslator")]
		public static void DebugArcTraslator()
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

				PointType is2P;
				ArcType is2Arc;

				// Obtengo el arc de la seleccion.
				var entities = UserSelectEntity("ARC", 1, OpenMode.ForRead);

				// Convierto el arco de autoCAD a is2Graph
				is2Arc = is2GraphTranslator.Tois2Graph(entities[0] as Arc);

				Arc Arc = is2GraphTranslatorEx.ToAcad(is2Arc, Vector3d.ZAxis);
				Arc.ColorIndex = 1;
				acBlkTblRec.AppendEntity(Arc);
				acTrans.AddNewlyCreatedDBObject(Arc, true);

				acTrans.Commit();
			}
		}
		#endregion

		#region - Command: Debug_All_Arc_Type -
		[CommandMethod("Debug_All_Arc_Type")]
		public static void Debug_All_Arc_Type()
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

				ArcEx acArc;
				ArcType isArc;
				PointType sP, eP, xP, cP;
				double angle, lenght;

				PromptPointResult pPtRes;
				PromptPointOptions pPtOpts = new PromptPointOptions("");

				//-------------------------------------------------------------------------
				// SOLICITAR DATO 1.
				//-------
				// Prompt for the start point
				pPtOpts.Message = "\nEspecifique el punto de inicio: ";
				pPtRes = acDoc.Editor.GetPoint(pPtOpts);

				// Exit if the user presses ESC or cancels the command
				if (pPtRes.Status == PromptStatus.Cancel) return;
				Point3d start = pPtRes.Value;

				//-------------------------------------------------------------------------
				// SOLICITAR DATO 2.
				//-------
				// Prompt for the end point
				/*pPtOpts.Message = "\nEspecifique un punto cualquiera: ";
				pPtRes = acDoc.Editor.GetPoint(pPtOpts);
				Point3d any = pPtRes.Value;*/

				pPtOpts.Message = "\nEspecifique un punto centro: ";
				pPtRes = acDoc.Editor.GetPoint(pPtOpts);

				// Exit if the user presses ESC or cancels the command
				if (pPtRes.Status == PromptStatus.Cancel) return;
				Point3d center = pPtRes.Value;

				//-------------------------------------------------------------------------
				// SOLICITAR DATO 3.
				//-------
				// Prompt for the direction point
				/*pPtOpts.Message = "\nEspecifique el punto final: ";
				pPtRes = acDoc.Editor.GetPoint(pPtOpts);
				  
				// Exit if the user presses ESC or cancels the command
				if (pPtRes.Status == PromptStatus.Cancel) return; 
				Point3d end = pPtRes.Value;*/

				PromptDoubleOptions optAngle = new PromptDoubleOptions("Especifique el ángulo del arco");
				optAngle.DefaultValue = 30;
				PromptDoubleResult resAngle = acDoc.Editor.GetDouble(optAngle);

				if (resAngle.Status == PromptStatus.Cancel) return;
				angle = resAngle.Value;

				//-------------------------------------------------------------------------
				// SOLICITAR DATO 4.
				//-------
				PromptIntegerOptions optInverse = new PromptIntegerOptions("Defina si Invertir la orientacion del arco o No: [0]No invertir, [1]Invertir");
				optInverse.DefaultValue = 0;
				PromptIntegerResult resInverse = acDoc.Editor.GetInteger(optInverse);

				if (resInverse.Status == PromptStatus.Cancel) return;
				bool inverse = (resInverse.Value == 1);

				//-----------
				// Convierto los puntos seleccionados en Autocad a puntos de is2GraphObj.
				sP = is2GraphTranslator.Tois2Graph(start);
				//eP = is2GraphTranslator.Tois2Graph(end);
				//xP = is2GraphTranslator.Tois2Graph(any);
				cP = is2GraphTranslator.Tois2Graph(center);

				//------------------------------------
				// Variante #1: (Start, Any, End)
				//isArc = new ArcType(new ArcStartPoint(sP), new ArcAnyPoint(xP), new ArcEndPoint(eP));
				
				//------------------------------------
				// Variante #2: (Start, Center, End)
				//isArc = new ArcType(new ArcStartPoint(sP), new ArcCenterPoint(cP), new ArcEndPoint(eP), inverse);

				//-------------------------------------
				// Variante #3: (Start, Center, Angle)
				isArc = new ArcType(new ArcStartPoint(sP), new ArcCenterPoint(cP), new ArcGradeAngle(angle), inverse);

				//-------------------------------------
				// Variante #4: (Start, Center, Length)
				//isArc = new ArcType(new ArcStartPoint(sP), new ArcCenterPoint(cP), new ArcDistance(lenght), inverse);

				//-------------------------------------
				// Variante #5: (Start, End, Angle)
				//isArc = new ArcType(new ArcStartPoint(sP), new ArcEndPoint(cP), new ArcGradeAngle(angle), inverse);

				//-------------------------------------
				// Variante #6: (Start, End, Direction)
				//isArc = new ArcType(new ArcStartPoint(sP), new ArcEndPoint(cP), new ArcVectorDirection(dP), inverse);

				//-------------------------------------
				// Variante #7: (Start, End, Radius)


				//-------------------------------------
				// Variante #8: (Line, End)


				//-------------------------------------
				// Variante #9: (Arc, End)


				//isArc = new ArcType(new ArcStartPoint(sP), new ArcEndPoint(eP), new ArcVectorDirection(dP), inverse);
				acArc = is2GraphTranslatorEx.ToAcad(isArc, Vector3d.ZAxis);
				acArc.ColorIndex = 2;

				acBlkTblRec.AppendEntity(acArc);
				acTrans.AddNewlyCreatedDBObject(acArc, true);

				acTrans.Commit();
			}
		}
		#endregion

		#region - Command: Debug_Arc_Direction -
		[CommandMethod("Debug_Arc_Direction")]
		public static void DebugArcDirection()
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

				PromptPointResult pPtRes;
				PromptPointOptions pPtOpts = new PromptPointOptions("");

				//-------
				// Prompt for the start point
				pPtOpts.Message = "\nEspecifique el punto de inicio del arco: ";
				pPtRes = acDoc.Editor.GetPoint(pPtOpts);
				Point3d start = pPtRes.Value;

				// Exit if the user presses ESC or cancels the command
				if (pPtRes.Status == PromptStatus.Cancel) return;

				//-------
				// Prompt for the end point
				pPtOpts.Message = "\nEspecifique el punto final del arco: ";
				pPtRes = acDoc.Editor.GetPoint(pPtOpts);
				Point3d end = pPtRes.Value;

				// Exit if the user presses ESC or cancels the command
				if (pPtRes.Status == PromptStatus.Cancel) return;

				//-------
				// Prompt for the direction point
				pPtOpts.Message = "\nEspecifique el punto de direccion del arco: ";
				pPtRes = acDoc.Editor.GetPoint(pPtOpts);
				Point3d direction = pPtRes.Value;

				// Exit if the user presses ESC or cancels the command
				if (pPtRes.Status == PromptStatus.Cancel) return;

				PromptIntegerOptions optInverse = new PromptIntegerOptions("Defina si Invertir la orientacion del arco o No: [0]No invertir, [1]Invertir");
				optInverse.DefaultValue = 0;
				PromptIntegerResult resInverse= acDoc.Editor.GetInteger(optInverse);

				bool inverse = (resInverse.Value == 1);

				ArcEx acArc;
				ArcType isArc;
				PointType sP, eP, dP;

				sP = is2GraphTranslator.Tois2Graph(start);
				eP = is2GraphTranslator.Tois2Graph(end);
				dP = is2GraphTranslator.Tois2Graph(direction);

				//arc = new ArcEx(new StartPointEx(start), new EndPointEx(end), new DirectionEx(direction), Vector3d.ZAxis);
				isArc = new ArcType(new ArcStartPoint(sP), new ArcEndPoint(eP), new ArcVectorDirection(dP), inverse);
				acArc = is2GraphTranslatorEx.ToAcad(isArc, Vector3d.ZAxis);
				acArc.ColorIndex = 2;

				acBlkTblRec.AppendEntity(acArc);
				acTrans.AddNewlyCreatedDBObject(acArc, true);

				acTrans.Commit();
			}
		}
		#endregion

		#region - Command: Debug_Arc_Continue_Line -
		[CommandMethod("Debug_Arc_Continue_Line")]
		public static void DebugArcContinueLine()
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

				PromptPointResult pPtRes;
				PromptPointOptions pPtOpts = new PromptPointOptions("");

				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione ahora la Recta");
				var seltLine = UserSelectEntity("LINE", 1, OpenMode.ForRead);

				//-------
				// Prompt for the end point
				pPtOpts.Message = "\nEspecifique el punto final del arco: ";
				pPtRes = acDoc.Editor.GetPoint(pPtOpts);
				Point3d end = pPtRes.Value;

				// Exit if the user presses ESC or cancels the command
				if (pPtRes.Status == PromptStatus.Cancel) return;

				PromptIntegerOptions optInverse = new PromptIntegerOptions("Defina si Invertir la orientacion del arco o No: [0]No invertir, [1]Invertir");
				optInverse.DefaultValue = 0;
				PromptIntegerResult resInverse= acDoc.Editor.GetInteger(optInverse);

				bool inverse = (resInverse.Value == 1);

				ArcEx acArc;
				PointType eP;
				SegmentType is2S;
				ArcType isArc;
				
				is2S = is2GraphTranslator.Tois2Graph(seltLine[0] as Line);
				eP = is2GraphTranslator.Tois2Graph(end);

				//arc = new ArcEx(new StartPointEx(start), new EndPointEx(end), new DirectionEx(direction), Vector3d.ZAxis);
				isArc = new ArcType(is2S, new ArcEndPoint(eP), inverse);
				acArc = is2GraphTranslatorEx.ToAcad(isArc, Vector3d.ZAxis);
				acArc.ColorIndex = 2;

				acBlkTblRec.AppendEntity(acArc);
				acTrans.AddNewlyCreatedDBObject(acArc, true);

				acTrans.Commit();
			}
		}
		#endregion

		#region - Command: Debug_Arc_Continue_Arc -
		[CommandMethod("Debug_Arc_Continue_Arc")]
		public static void DebugArcContinueArc()
		{
			Document acDoc = Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;

			using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
			{
				BlockTable acBlkTbl;
				acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

				BlockTableRecord acBlkTblRec;
				acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

				//if (acBlkTblRec == null) return;

				PromptPointResult pPtRes;
				PromptPointOptions pPtOpts = new PromptPointOptions("");

				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione ahora el Arco");
				var seltArc = UserSelectEntity("ARC", 1, OpenMode.ForRead);

				//-------
				// Prompt for the end point
				pPtOpts.Message = "\nEspecifique el punto final del arco: ";
				pPtRes = acDoc.Editor.GetPoint(pPtOpts);
				Point3d end = pPtRes.Value;

				// Exit if the user presses ESC or cancels the command
				if (pPtRes.Status == PromptStatus.Cancel) return;

				PromptIntegerOptions optInverse = new PromptIntegerOptions("Defina si Invertir la orientacion del arco o No: [0]No invertir, [1]Invertir");
				optInverse.DefaultValue = 0;
				PromptIntegerResult resInverse = acDoc.Editor.GetInteger(optInverse);

				bool inverse = (resInverse.Value == 1);

				ArcEx acArc;
				PointType eP;
				ArcType isArc, is2UserArc;

				is2UserArc = is2GraphTranslator.Tois2Graph(seltArc[0] as Arc);
				eP = is2GraphTranslator.Tois2Graph(end);

				isArc = new ArcType(is2UserArc, new ArcEndPoint(eP), inverse);
				acArc = is2GraphTranslatorEx.ToAcad(isArc, Vector3d.ZAxis);
				acArc.ColorIndex = 2;

				acBlkTblRec.AppendEntity(acArc);
				acTrans.AddNewlyCreatedDBObject(acArc, true);

				acTrans.Commit();
			}
		}
		#endregion

		#region - Command: Debug_Circle -
		[CommandMethod("Debug_Circle")]
		public static void DebugCircle()
		{
			//Circle_From_3P();
			Circle_From_2Tangent_1Radius();
			//Circle_From_3Tangent();
		}
		#endregion

		#region - Command: Debug_Line_Angle -
		[CommandMethod("Debug_Line_Angle")]
		public static void DebugLineAngle()
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

				/*PointType P1 = is2GraphObj.PolarPoint(is2GraphObj.OrigenXYZ, 30, 10);
				SegmentType S1 = new SegmentType(is2GraphObj.OrigenXYZ, P1);

				PointType P2 = is2GraphObj.PolarPoint(is2GraphObj.OrigenXYZ, 65, 10);
				SegmentType S2 = new SegmentType(is2GraphObj.OrigenXYZ, P2);

				PointType P3 = is2GraphObj.PolarPoint(is2GraphObj.OrigenXYZ, 100, 10);
				SegmentType S3 = new SegmentType(is2GraphObj.OrigenXYZ, P3);

				is2GraphObj.SegmentSegmentAngle(S1, S3);

				AcadUtilities.AddEntityToModel(is2GraphTranslator.ToAcad(S1));
				AcadUtilities.AddEntityToModel(is2GraphTranslator.ToAcad(S2));
				AcadUtilities.AddEntityToModel(is2GraphTranslator.ToAcad(S3));*/

				PointType P1, P2, Px;
				Point3d acP0, acP1, acP2, acPx;

				acP0 = new Point3d(79, -4, 0.0);
				acP1 = new Point3d(119, 22, 0.0);
				acP2 = new Point3d(113, -13, 0.0);
				acPx = new Point3d(117, -1, 0.0);
				//acPx = new Point3d(107, -1, 0.0);

				AcadUtilities.AddPinToModel(is2GraphTranslator.ToAcad_DB(acP1), 34, 0.6, 2);
				AcadUtilities.AddPinToModel(is2GraphTranslator.ToAcad_DB(acPx), 34, 0.6, 3);
				AcadUtilities.AddPinToModel(is2GraphTranslator.ToAcad_DB(acP2), 34, 0.6, 4);

				AcadUtilities.AddEntityToModel(new Line(acP1, acP0));
				AcadUtilities.AddEntityToModel(new Line(acP0, acP2));

				P1 = is2GraphTranslator.Tois2Graph(acP1);
				P2 = is2GraphTranslator.Tois2Graph(acP2);
				Px = is2GraphTranslator.Tois2Graph(acPx);
				ArcType A = new ArcType(new ArcStartPoint(P1), new ArcAnyPoint(Px), new ArcEndPoint(P2));
				ArcEx Arc = is2GraphTranslatorEx.ToAcad(A, Vector3d.ZAxis);
				//ArcEx Arc = new ArcEx(new StartPointEx(acP1), new AnyPointEx(acPx), new EndPointEx(acP2), Vector3d.ZAxis);
				acBlkTblRec.AppendEntity(Arc);
				acTrans.AddNewlyCreatedDBObject(Arc, true);

				acTrans.Commit();
			}

			acDoc.SendStringToExecute("zoom e ", true, false, false);
			//acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: Debug_Arc_Line_Intersect -
		[CommandMethod("Debug_Arc_Line_Intersect")]
		public static void DebugArcLineIntersect()
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

				/*PointType Pa1, Pa2, Pc, P1, P2, P3, P4;

				// Dibujo el arco
				Pa1 = new PointType(9.0, 26.0, 0.0);
				Pa2 = new PointType(22.0, 12.0, 0.0);
				Pc = new PointType(22.0, 25.0, 0.0);
				ArcType A = new ArcType(new ArcType.StartPointEx(Pa1), new ArcType.CenterPointEx(Pc), new ArcType.EndPointEx(Pa2), false);
				ArcEx Arc = is2GraphTranslator.ToAcad(A, Vector3d.ZAxis);

				acBlkTblRec.AppendEntity(Arc);
				acTrans.AddNewlyCreatedDBObject(Arc, true);

				// Dibujo la linea.
				P1 = new PointType(7.0, 25.0, 0.0);
				P2 = new PointType(22.0, 9.0, 0.0);
				SegmentType C = new SegmentType(P1, P2);
				Line acL = is2GraphTranslator.ToAcad(C);

				acBlkTblRec.AppendEntity(acL);
				acTrans.AddNewlyCreatedDBObject(acL, true);

				// Calculo interseccion Arc-Line
				LineType isL = new LineType(C.StartPoint, C.Angle);
				int intersec = is2GraphObj.ArcLineIntersect(A, isL, out P3, out P4);
				//if (intersec != 0)
				{
					//if (intersec == 2)
					{
						AcadUtilities.AddPinToModel(is2GraphTranslator.ToAcad_DB(P3), 34, 0.3, 2);
						AcadUtilities.AddPinToModel(is2GraphTranslator.ToAcad_DB(P4), 34, 0.3, 3);
					}
				}
				/*else
				{
					Application.ShowAlertDialog("La linea  el Arco no se intersectan!!!");
				}*/

				LineType L;
				SegmentType S;
				CircleType C;

				PointType is2P1, is2P2;
				var selCircle = UserSelectEntity("CIRCLE", 1, OpenMode.ForRead);
				var seltLine = UserSelectEntity("LINE", 1, OpenMode.ForRead);

				C = is2GraphTranslator.Tois2Graph(selCircle[0] as Circle);
				S = is2GraphTranslator.Tois2Graph(seltLine[0] as Line);
				L = new LineType(S.StartPoint, S.Angle);

				is2GraphObj.CircleLineIntersect(C, L, out is2P1, out is2P2);

				Line acL = new Line(is2GraphTranslator.ToAcad_3d(is2P1), is2GraphTranslator.ToAcad_3d(is2P2));
				acL.SetDatabaseDefaults();
				acL.ColorIndex = 3;
				acBlkTblRec.AppendEntity(acL);
				acTrans.AddNewlyCreatedDBObject(acL, true);

				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 0.9;
				DBPoint Pin1 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P1));
				Pin1.ColorIndex = 1; // verde
				acBlkTblRec.AppendEntity(Pin1);
				acTrans.AddNewlyCreatedDBObject(Pin1, true);

				DBPoint Pin2 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P2));
				Pin2.ColorIndex = 1; // verde
				acBlkTblRec.AppendEntity(Pin2);
				acTrans.AddNewlyCreatedDBObject(Pin2, true);

				acTrans.Commit();
			}

			acDoc.SendStringToExecute("zoom e ", true, false, false);
			//acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: MiArco_Tipo5 -
		[CommandMethod("MiArco_Tipo5")]
		public static void MiArco5()
		{
			// Get the current database and start the Transaction Manager
			Document acDoc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;

			PromptPointResult pPtRes;
			PromptPointOptions pPtOpts = new PromptPointOptions("");

			// Prompt for the start point
			pPtOpts.Message = "\nEntre el punto Inicial del Arco: ";
			pPtRes = acDoc.Editor.GetPoint(pPtOpts);
			Point3d ptStart = pPtRes.Value;

			// Exit if the user presses ESC or cancels the command
			if (pPtRes.Status == PromptStatus.Cancel) return;

			// Prompt for the end point
			pPtOpts.Message = "\nEntre el punto Inicial del Arco: ";
			pPtOpts.UseBasePoint = true;
			pPtOpts.BasePoint = ptStart;
			pPtRes = acDoc.Editor.GetPoint(pPtOpts);
			Point3d ptEnd = pPtRes.Value;

			if (pPtRes.Status == PromptStatus.Cancel) return;

			PromptIntegerResult dRes;
			dRes = acDoc.Editor.GetInteger("\nIntroduzca el angulo: ");
			int Angle = dRes.Value;

			if (pPtRes.Status == PromptStatus.Cancel) return;

			// Start radius transaction
			using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
			{
				BlockTable acBlkTbl;
				BlockTableRecord acBlkTblRec;

				// Open Model space for write
				acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
																		 OpenMode.ForRead) as BlockTable;

				acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
																				OpenMode.ForWrite) as BlockTableRecord;

				ArcEx arc = new ArcEx(new StartPointEx(ptStart), new EndPointEx(ptEnd), new GradeAngleEx(Angle), Vector3d.ZAxis);
				acBlkTblRec.AppendEntity(arc);
				acTrans.AddNewlyCreatedDBObject(arc, true);


				// Zoom to the extents or limits of the drawing
				acDoc.SendStringToExecute("zoom e ", true, false, false);

				// Commit the changes and dispose of the transaction
				acTrans.Commit();
			}
		}
		#endregion

		#region - Command: PruebaArc -
		[CommandMethod("PruebaArc")]
		public static void PruebaArc()
		{
			Document acDoc = Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;

			using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
			{
				BlockTable acBlkTbl;
				acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

				BlockTableRecord acBlkTblRec;
				acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

				//--- Session donde se dibuja ---//
				Point3d P1 = new Point3d(60.0, 25.0, 0.0);
				Point3d P2 = new Point3d(25.0, 50.0, 0.0);
				//is2ArcExtend arc = new is2ArcExtend(P2, P2, is2GraphObj.GradToRad(50.0), Vector3d.ZAxis);
				ArcEx arc = new ArcEx(new StartPointEx(P2), new EndPointEx(P1), new GradeAngleEx(336.0), Vector3d.ZAxis);

				acBlkTblRec.AppendEntity(arc);
				acTrans.AddNewlyCreatedDBObject(arc, true);

				acTrans.Commit();
			}

			acDoc.SendStringToExecute("zoom e ", true, false, false);
			//acDoc.SendStringToExecute("regen ", true, false, false);
		}
		#endregion

		#region - Command: PruebaJoin -
		[CommandMethod("PruebaJoin")]
		public static void PruebaJoin()
		{
			Document acDoc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;

			using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
			{
				BlockTable acBlkTbl;
				acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

				BlockTableRecord acBlkTblRec;
				acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

				//try
				//{
				Line pLine1 = new Line(new Point3d(0, 0, 0), new Point3d(1, 1, 0));
				Line pLine2 = new Line(new Point3d(1, 1, 0), new Point3d(4, 1, 0));

				// Join the second line to the first line 
				pLine1.JoinEntity(pLine2);

				acBlkTblRec.AppendEntity(pLine1);
				acTrans.AddNewlyCreatedDBObject(pLine1, true);
				/*}
				catch (Autodesk.AutoCAD.Runtime.Exception e)
				{
					Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("nJoin error: "); 
					Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(e.ErrorStatus.ToString());
				}*/

				acTrans.Commit();
			}
		}
		#endregion

		#region - Command: TestPolarPoint -
		[CommandMethod("Test_Polar_Point")]
		public static void TestPolarPoint()
		{
			Document acDoc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;

			using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
			{
				BlockTable acBlkTbl;
				acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

				BlockTableRecord acBlkTblRec;
				acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

				//try
				//{
				PointType P_origen = new PointType(0,0,0);
				PointType P = is2GraphObj.PolarPoint(P_origen, 180, 10);

				DBPoint acP1 = is2GraphTranslator.ToAcad_DB(P_origen);
				DBPoint acP2 = is2GraphTranslator.ToAcad_DB(P);

				AcadUtilities.AddPinToModel(acP1, 34, 0.6);
				AcadUtilities.AddPinToModel(acP2, 34, 0.6, 1);

				acDoc.SendStringToExecute("zoom e ", true, false, false);
				//acBlkTblRec.AppendEntity(acP1);
				//acTrans.AddNewlyCreatedDBObject(acP1, true);
				
				acTrans.Commit();
			}
		}
		#endregion

		#region - Command: TestLineTangentToCircle -
		[CommandMethod("Test_Line_Tangent_To_Circle")]
		public static void TestLineTangentToCircle ()
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

				PointType is2P;
				CircleType is2C;
				LineType L1, L2;

				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione un circle");
				var entities = UserSelectEntity("CIRCLE", 1, OpenMode.ForRead);
				is2C = is2GraphTranslator.Tois2Graph(entities[0] as Circle);

				PromptPointResult pPtRes;
				PromptPointOptions pPtOpts = new PromptPointOptions("");

				// Prompt for the start point
				pPtOpts.Message = "\nSeleccion un punto del plano: ";
				pPtRes = acDoc.Editor.GetPoint(pPtOpts);

				// Exit if the user presses ESC or cancels the command
				if (pPtRes.Status == PromptStatus.Cancel) return;

				is2P = is2GraphTranslator.Tois2Graph(pPtRes.Value);
				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 0.3;
				DBPoint Pin1 = new DBPoint(is2GraphTranslator.ToAcad_3d(is2P));
				Pin1.ColorIndex = 1; // rojo
				acBlkTblRec.AppendEntity(Pin1);
				acTrans.AddNewlyCreatedDBObject(Pin1, true);

				is2GraphObj.LineTangentToCircle(is2C, is2P, out L1, out L2);

				string msg;
				if (L1 == null && L2 == null)
				{
					msg = "El punto P es interior a la Circunferencia. No existe ninguna recta que pase por este punto y sea tangente a la circunferencia al mismo tiempo";
					Application.ShowAlertDialog(msg);
				}
				else if (L1 == L2)
				{
					PointType P1, P2;
					int x = is2GraphObj.CircleLineIntersect(is2C, L1, out P1, out P2);

					if (x == 1)
					{
						DBPoint Pin = new DBPoint(is2GraphTranslator.ToAcad_3d(P1));
						Pin.ColorIndex = 4; // cian
						acBlkTblRec.AppendEntity(Pin);
						acTrans.AddNewlyCreatedDBObject(Pin, true);

						msg = "Efectivamente, se obtuvo una linea que es tangente a la circunferencia en el punto de color CIAN";
						Application.ShowAlertDialog(msg);
					}
				}
				else
				{
					PointType aP1, aP2, bP1, bP2;

					int x1 = is2GraphObj.CircleLineIntersect(is2C, L1, out aP1, out aP2);

					Line acL1 = new Line(is2GraphTranslator.ToAcad_3d(is2P), is2GraphTranslator.ToAcad_3d(aP1));
					acL1.ColorIndex = 2; // amarillo
					acBlkTblRec.AppendEntity(acL1);
					acTrans.AddNewlyCreatedDBObject(acL1, true);

					DBPoint Pin2 = new DBPoint(is2GraphTranslator.ToAcad_3d(aP1));
					Pin2.ColorIndex = 4; // cian
					acBlkTblRec.AppendEntity(Pin2);
					acTrans.AddNewlyCreatedDBObject(Pin2, true);

					int x2 = is2GraphObj.CircleLineIntersect(is2C, L2, out bP1, out bP2);

					Line acL2 = new Line(is2GraphTranslator.ToAcad_3d(is2P), is2GraphTranslator.ToAcad_3d(bP1));
					acL2.ColorIndex = 3; // verde
					acBlkTblRec.AppendEntity(acL2);
					acTrans.AddNewlyCreatedDBObject(acL2, true);

					DBPoint Pin3 = new DBPoint(is2GraphTranslator.ToAcad_3d(bP1));
					Pin3.ColorIndex = 4; // cian
					acBlkTblRec.AppendEntity(Pin3);
					acTrans.AddNewlyCreatedDBObject(Pin3, true);
				}

				// Save the new object to the database
				acTrans.Commit();
			}
		}
		#endregion

		#region - Command: TestLineTangentToCircle -
		[CommandMethod("Test_PolylineType")]
		public static void Test_PolylineType()
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

				ArcType a;
				PointType pi, pf, p1, p2;
				PolylineType Pl = new PolylineType();

				pi = new PointType(5, 5);
				pf = new PointType(7, 10);
				p1 = new PointType(15, 6);
				p2 = new PointType(16, 11);
				a = new ArcType(new ArcStartPoint(pi), new ArcEndPoint(pf), new ArcGradeAngle(90));
				//a = new ArcType(new ArcStartPoint(pf), new ArcEndPoint(pi), new ArcGradeAngle(90), !true);
				ArcEx arc1 = new ArcEx(new StartPointEx(is2GraphTranslator.ToAcad_3d(pi)), 
															 new EndPointEx(is2GraphTranslator.ToAcad_3d(pf)), 
															 new GradeAngleEx(90), Vector3d.ZAxis);
				SegmentType acS = new SegmentType(is2GraphObj.OrigenXYZ, pi);

				//Pl.AddVertex(new PointType(2, 2));
				//Pl.AddVertex(a.StartPoint);
				Pl.JoinEntity(acS);
				Pl.JoinEntity(a);
				Pl.AddVertex(p1);
				Pl.JoinEntity(new ArcType(new ArcStartPoint(p1), new ArcEndPoint(p2), new ArcRadius(5)));
				Pl.AddVertex(new PointType(20, 20));
				Pl.AddVertex(new PointType(25, 20));
				Pl.AddVertex(new PointType(25, 0));
				Pl.JoinEntity(new SegmentType(new PointType(25, 0), is2GraphObj.OrigenXYZ));
				//Pl.AddVertex(new PointType(18, 9));
				//Pl.AddVertex(new PointType(25, 25));
				//Pl.AddVertex(pi);
				//Pl.AddVertex(new PointType(20, 20));

				//DBPoint dbP = is2GraphTranslator.ToAcad_DB(a.EndPoint);
				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 0.4;
				DBPoint dbP1 = is2GraphTranslator.ToAcad_DB(a.EndPoint);
				dbP1.ColorIndex = 1; // color rojo
				//acBlkTblRec.AppendEntity(dbP1);
				//acTrans.AddNewlyCreatedDBObject(dbP1, true);

				Arc arc = is2GraphTranslator.ToAcad(a, Vector3d.ZAxis);
				//acBlkTblRec.AppendEntity(arc);
				//acTrans.AddNewlyCreatedDBObject(arc, true);

				/*
				//-----------------------------------------------------------------
				PointType P0,P1, P2, P3, P4, P5, P6, P7, P8;

				P0 = new PointType(0.0, 0.0, 0.0);
				P1 = new PointType(0, 32);
				P2 = new PointType(0, 73);
				P3 = new PointType(5, 73);
				P4 = new PointType(5, 17);
				P5 = new PointType(27, 17);
				P6 = new PointType(27, 10);
				P7 = new PointType(-17, 10);
				P8 = new PointType(-17, 17);

				ArcType arc1 = new ArcType(new ArcStartPoint(P8), new ArcEndPoint(P1), new ArcVectorDirection(P4));
				
				PolylineType Pl = new PolylineType();
				Pl.AddVertex(P1);
				Pl.AddVertex(P2);
				Pl.AddVertex(P3);
				Pl.AddVertex(P4);
				Pl.AddVertex(P5);
				Pl.AddVertex(P6);
				Pl.AddVertex(P7);
				Pl.AddVertex(P8);
				Pl.JoinEntity(arc1);
				//-----------------------------------------------------------------
				*/

				Polyline acPl = is2GraphTranslator.ToAcad(Pl, Vector3d.ZAxis);
				/*Polyline acPl = new Polyline();
				acPl.AddVertexAt(0, is2GraphTranslator.ToAcad_2d(pi), 0, 0, 0);
				acPl.JoinEntity(arc1);
				acPl.ReverseCurve();
				acPl.AddVertexAt(2, is2GraphTranslator.ToAcad_2d(p1), 0, 0, 0);*/
				acBlkTblRec.AppendEntity(acPl);
				acTrans.AddNewlyCreatedDBObject(acPl, true);

				// Save the new object to the database
				acTrans.Commit();
			}

			//acDoc.SendStringToExecute("_zoom e ", true, false, false);
		}
		#endregion

		#region - Utilities Funtion -
		private static List<Entity> UserSelectEntity (string nameEntity, short count, OpenMode mode)
	  {
			Document acDoc = Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;

			// Create radius TypedValue array to define the filter criteria
			TypedValue[] acTypValAr = new TypedValue[1];
			acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, nameEntity), 0);

			// Assign the filter criteria to radius SelectionFilter object
			SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);

			// Request for objects to be selected in the drawing area
			PromptSelectionResult acSSPrompt;
			acSSPrompt = acDoc.Editor.GetSelection(acSelFtr);

			List <Entity> Selection = new List <Entity>();

			// If the prompt status is OK, objects were selected
			if (acSSPrompt.Status == PromptStatus.OK)
			{
				SelectionSet acSSet = acSSPrompt.Value;

				if (acSSet.Count == count)
				{
					using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
					{
						BlockTable acBlkTbl;
						acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, mode) as BlockTable;

						BlockTableRecord acBlkTblRec;
						acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

						if (acBlkTblRec == null) return null;

						// Step through the objects in the selection set
						foreach (SelectedObject acSSObj in acSSet)
						{
							// Check to make sure radius valid SelectedObject object was returned
							if (acSSObj != null)
							{
								// Open the selected object for write
								//Entity acEnt = acTrans.GetObject(acSSObj.ObjectId, OpenMode.ForWrite) as Entity;
								Entity acEnt = acTrans.GetObject(acSSObj.ObjectId, mode) as Entity;

								if (acEnt != null)
								{
									Selection.Add(acEnt);									
								}
							}
						}
					}
				}
			}
			
			return Selection;
	  }

		private static List<Entity> UserSelectEntity (Dictionary <string, int> dictionary)
	  {
			Document acDoc = Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;

			List<Entity> Selection = new List<Entity>();

			foreach (KeyValuePair<string, int> keyvalue in dictionary)
		  {
				// Create radius TypedValue array to define the filter criteria
				TypedValue[] acTypValAr = new TypedValue[1];
				acTypValAr.SetValue(new TypedValue((int)DxfCode.Start, keyvalue.Key), 0);

				// Assign the filter criteria to radius SelectionFilter object
				SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);

				// Request for objects to be selected in the drawing area
				PromptSelectionResult acSSPrompt;
				acSSPrompt = acDoc.Editor.GetSelection(acSelFtr);

				// If the prompt status is OK, objects were selected
			  if (acSSPrompt.Status == PromptStatus.OK)
			  {
				  SelectionSet acSSet = acSSPrompt.Value;

				  if (acSSet.Count == keyvalue.Value)
				  {
					  using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
					  {
						  BlockTable acBlkTbl;
						  acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;

						  BlockTableRecord acBlkTblRec;
						  acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

						  if (acBlkTblRec == null) return null;

						  // Step through the objects in the selection set
						  foreach (SelectedObject acSSObj in acSSet)
						  {
							  // Check to make sure radius valid SelectedObject object was returned
							  if (acSSObj != null)
							  {
								  // Open the selected object for write
								  //Entity acEnt = acTrans.GetObject(acSSObj.ObjectId, OpenMode.ForWrite) as Entity;
								  Entity acEnt = acTrans.GetObject(acSSObj.ObjectId, OpenMode.ForRead) as Entity;

								  if (acEnt != null)
								  {
									  Selection.Add(acEnt);
								  }
							  }
						  }
					  }
				  }
			  }
		  }
			
			return Selection;
		}
		#endregion

		#region - Modificar Cirulo. PRUEBA -
	  [CommandMethod ("Modificar_Circle")]
		public static void Modificar_Circle()
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

			  Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Seleccione el Circulo");
				var selCircle = UserSelectEntity("CIRCLE", 1, OpenMode.ForWrite);

				Circle C = selCircle[0] as Circle;
	
			  //C.ColorIndex = 3;
				C.Erase(true);
				//acBlkTblRec.

				//acBlkTblRec.AppendEntity(A);
				//acTrans.AddNewlyCreatedDBObject(A, true);

				//Application.DocumentManager.MdiActiveDocument.Editor.Regen();

				acTrans.Commit();
			}

			//acDoc.SendStringToExecute("zoom e ", true, false, false);
			//acDoc.SendStringToExecute("regen ", true, false, false);


			/*// Get the current document and database
			Document acDoc = Application.DocumentManager.MdiActiveDocument;
			Database acCurDb = acDoc.Database;

			// Start a transaction
			using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
			{
				// Open the Block table for read
				BlockTable acBlkTbl;
				acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId,
																		 OpenMode.ForRead) as BlockTable;

				// Open the Block table record Model space for write
				BlockTableRecord acBlkTblRec;
				acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace],
																				OpenMode.ForWrite) as BlockTableRecord;

				// Create a lightweight polyline
				Polyline acPoly = new Polyline();
				acPoly.SetDatabaseDefaults();
				acPoly.AddVertexAt(0, new Point2d(2, 4), 0, 0, 0);
				acPoly.AddVertexAt(1, new Point2d(4, 2), 0, 0, 0);
				acPoly.AddVertexAt(2, new Point2d(6, 4), 0, 0, 0);

				// Add the new object to the block table record and the transaction
				acBlkTblRec.AppendEntity(acPoly);
				acTrans.AddNewlyCreatedDBObject(acPoly, true);

				// Update the display and display an alert message
				acDoc.Editor.Regen();
				Application.ShowAlertDialog("Erase the newly added polyline.");

				// Erase the polyline from the drawing
				acPoly.Erase(true);

				// Save the new object to the database
				acTrans.Commit();
			}*/

	  }
		#endregion

		#region - Funciones Privadas - 
		private static void Circle_From_3P()
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

				PromptPointResult pPtRes_1, pPtRes_2, pPtRes_3;
				PromptPointOptions pPtOpts1 = new PromptPointOptions("");
				PromptPointOptions pPtOpts2 = new PromptPointOptions("");
				PromptPointOptions pPtOpts3 = new PromptPointOptions("");

				// Prompt for the start point
				pPtOpts1.Message = "\nSelecciona el 1er punto: ";
				pPtRes_1 = acDoc.Editor.GetPoint(pPtOpts1);

				pPtOpts2.Message = "\nSelecciona el 2do punto: ";
				pPtRes_2 = acDoc.Editor.GetPoint(pPtOpts2);

				pPtOpts1.Message = "\nSelecciona el 3er punto: ";
				pPtRes_3 = acDoc.Editor.GetPoint(pPtOpts3);

				var optType = new PromptIntegerOptions("Defina si es Circunscripto or Inscripto: [1]Circunscripto, [2]Inscripto");
				optType.DefaultValue = 1;
				PromptIntegerResult resType = acDoc.Editor.GetInteger(optType);

				Point3d acP1, acP2, acP3;
				//PointType isP1, isP2, isP3;

				acP1 = pPtRes_1.Value; 
				acP2 = pPtRes_2.Value; 
				acP3 = pPtRes_3.Value;
				//isP1 = is2GraphTranslator.Tois2Graph(acP1);
				//isP2 = is2GraphTranslator.Tois2Graph(acP2);
				//isP3 = is2GraphTranslator.Tois2Graph(acP3);

				// Exit if the user presses ESC or cancels the command
				if (pPtRes_1.Status == PromptStatus.Cancel) return;
				if (pPtRes_2.Status == PromptStatus.Cancel) return;
				if (pPtRes_3.Status == PromptStatus.Cancel) return;

				acCurDb.Pdmode = 34;
				acCurDb.Pdsize = 0.3;

				DBPoint Pin1 = new DBPoint(acP1);
				DBPoint Pin2 = new DBPoint(acP2);
				DBPoint Pin3 = new DBPoint(acP3);

				Pin1.ColorIndex = 2; // amarillo
				Pin2.ColorIndex = 2; // amarillo
				Pin3.ColorIndex = 2; // amarillo

				acBlkTblRec.AppendEntity(Pin1);
				acTrans.AddNewlyCreatedDBObject(Pin1, true);
				acBlkTblRec.AppendEntity(Pin2);
				acTrans.AddNewlyCreatedDBObject(Pin2, true);
				acBlkTblRec.AppendEntity(Pin3);
				acTrans.AddNewlyCreatedDBObject(Pin3, true);

				CircleEx C = new CircleEx(acP1, acP2, acP3, Vector3d.ZAxis, (CircleEx.Type)resType.Value);
				acBlkTblRec.AppendEntity(C);
				acTrans.AddNewlyCreatedDBObject(C, true);

				acTrans.Commit();
			}
	  }

	  private static void Circle_From_2Tangent_1Radius()
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

				acDoc.Editor.WriteMessage("Seleccione 2 Lineas");
				var seltLine = UserSelectEntity("LINE", 2, OpenMode.ForRead);

				PromptDoubleOptions pOptionsRadius = new PromptDoubleOptions("");
				pOptionsRadius.Message = "\nDefina el valor del radio para el Fillet: ";
				pOptionsRadius.DefaultValue = 10;
				var pResRadius = acDoc.Editor.GetDouble(pOptionsRadius);
				
				// Exit if the user presses ESC or cancels the command
				if (pResRadius.Status == PromptStatus.Cancel) return;

				var optRight = new PromptIntegerOptions("Defina si Right or Left al Segmento #1: [1]Right, [0]Left");
				optRight.DefaultValue = 1;
				PromptIntegerResult resRight = acDoc.Editor.GetInteger(optRight);

				// Exit if the user presses ESC or cancels the command
				if (resRight.Status == PromptStatus.Cancel) return;

				var optLeft = new PromptIntegerOptions("Defina si Right or Left al Segmento #2: [1]Right, [0]Left");
				optLeft.DefaultValue = 1;
				PromptIntegerResult resLeft = acDoc.Editor.GetInteger(optLeft);

				// Exit if the user presses ESC or cancels the command
				if (resLeft.Status == PromptStatus.Cancel) return;

				double radius;
				bool S1_right = (resRight.Value == 1);
				bool S2_right = (resLeft.Value == 1);

				SegmentType S1 = is2GraphTranslator.Tois2Graph(seltLine[0] as Line);
				SegmentType S2 = is2GraphTranslator.Tois2Graph(seltLine[1] as Line);
				radius = pResRadius.Value;
				
				//CircleEx C = new CircleEx(acP1, acP2, acP3, Vector3d.ZAxis, (CircleEx.Type)resType.Value);
				CircleType C = new CircleType(S1, S2, radius, S1_right, S2_right);

				Circle acC = is2GraphTranslator.ToAcad(C, Vector3d.ZAxis);
				acBlkTblRec.AppendEntity(acC);
				acTrans.AddNewlyCreatedDBObject(acC, true);

				acTrans.Commit();
			}
	  }

		private static void Circle_From_3Tangent ()
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

				acDoc.Editor.WriteMessage("Seleccione 3 Lineas");
				var seltLine = UserSelectEntity("LINE", 3, OpenMode.ForRead);

				Line L1, L2, L3;

				L1 = seltLine[0] as Line;
				L2 = seltLine[1] as Line;
				L3 = seltLine[2] as Line;

				CircleEx C = new CircleEx(L1, L2, L3, Vector3d.ZAxis);
				acBlkTblRec.AppendEntity(C);
				acTrans.AddNewlyCreatedDBObject(C, true);

				acTrans.Commit();
			}
		}
		#endregion
	}
}
