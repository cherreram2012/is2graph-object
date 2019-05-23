using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using is2GraphObject;

namespace Translator_is2graph_acad
{
  public class is2GraphTranslator
  {
		//------------------------------------------------------------------------//
	//
	public static Point3d Acad_2dTo3d (Point2d P)
	{
		return new Point3d(P.X, P.Y, 0.0);
	}

	//------------------------------------------------------------------------//
	//
	public static Point2d Acad_3dTo2d (Point3d P)
	{
		return new Point2d(P.X, P.Y);
	}

	//------------------------------------------------------------------------//
	// Convierte la entidad <Point> de is2Graph a su tipo equivalente de ACAD-2d.
	public static Point2d ToAcad_2d (PointType P)
	{
		return new Point2d(P.cX, P.cY);
	}

		//------------------------------------------------------------------------//
		// Convierte la entidad <Point> is2Graph a su tipo equivalente de ACAD-2d 
		// Geometry.
		public static Point2d ToAcad_2dG (PointType P)
		{
			return new Point2d(P.cX, P.cY);
		}

    //------------------------------------------------------------------------//
		// Convierte la entidad <Point> de is2Graph a su tipo equivalente de ACAD-3d.
    public static Point3d ToAcad_3d (PointType P)
    {
      return new Point3d(P.cX, P.cY, P.cZ);
    }

		//------------------------------------------------------------------------//
		// Convierte la entidad <Point> de is2Graph a su tipo equivalente de ACAD-3d
		// Geometry.
		public static Point3d ToAcad_3dG (PointType P)
		{
			return new Point3d(P.cX, P.cY, P.cZ);
		}

		//------------------------------------------------------------------------//
		// Convierte la entidad <Point> de is2Graph a su tipo equivalente de ACAD-3d
		// Database.
	  public static DBPoint ToAcad_DB (PointType P)
	  {
			return new DBPoint(new Point3d(P.cX, P.cY, P.cZ));
	  }

		//------------------------------------------------------------------------//
		// Convierte la entidad <Point2d> de ACAD a su tipo equivalente de ACAD-3d
		// Database.
		public static DBPoint ToAcad_DB (Point2d P)
		{
			return new DBPoint(new Point3d(P.X, P.Y, 0.0));
		}

		//------------------------------------------------------------------------//
		// Convierte la entidad <Point3d> de ACAD a su tipo equivalente de ACAD-3d
		// Database.
		public static DBPoint ToAcad_DB (Point3d P)
		{
			return new DBPoint(new Point3d(P.X, P.Y, P.Z));
		}

	  //------------------------------------------------------------------------//
		// Convierte la entidad <Segment> de is2Graph a su tipo equivalente de ACAD.
		public static Line ToAcad (is2GraphObject.SegmentType S)
		{
			return new Line(ToAcad_3d(S.Pi), ToAcad_3d(S.Pf));
		}

    //------------------------------------------------------------------------//
    // Convierte la entidad <Circle> de is2Graph a su tipo equivalente de ACAD.
    public static Circle ToAcad (CircleType C, Vector3d normal)
    {
      return new Circle(ToAcad_3d(C.Center), normal, C.Radius);
    }

    //------------------------------------------------------------------------//
    // Convierte la entidad
    public static PointType Tois2Graph (Point2d P)
    {
      return new PointType(P.X, P.Y, 0.0);
    }

    //------------------------------------------------------------------------//
    // Convierte la entidad
    public static PointType Tois2Graph (Point3d P)
    {
      return new PointType(P.X, P.Y, P.Z);
    }

    //------------------------------------------------------------------------//
		// Convierte la entidad Circle de ACAD a la entidad homologa de is2Graph.
    public static CircleType Tois2Graph (Circle C)
    {
      return new CircleType(Tois2Graph(C.Center), C.Radius);
    }

		//------------------------------------------------------------------------//
		// Convierte la entidad Arc de ACAD a la entidad homologa de is2Graph.
		public static ArcType Tois2Graph(Arc A)
		{
			ArcType Arc;
			double sAng, eAng;
			PointType Pi, Pf, Pc;
			//is2GraphObj.Quadrant sQ, eQ;

			//sAng = A.StartAngle;
			//eAng = A.EndAngle;

			//sQ = is2GraphObj.QuadrantAngle(is2GraphObj.RadToGrad(sAng));
			//eQ = is2GraphObj.QuadrantAngle(is2GraphObj.RadToGrad(eAng));

			//Pi = Tois2Graph(A.ArcStartPoint);
			//Pf = Tois2Graph(A.ArcEndPoint);
			//Pc = Tois2Graph(A.Center);

			/*if (is2GraphObj.MenorOrEqual(sAng, eAng) || 
				 (is2GraphObj.MayorOrEqual(sAng, eAng) && sQ == is2GraphObj.Quadrant.IV && eQ == is2GraphObj.Quadrant.I))
				Arc = new ArcType(new ArcType.ArcStartPoint(Pi), new ArcType.ArcCenterPoint(Pc), new ArcType.ArcEndPoint(Pf));
			else
				Arc = new ArcType(new ArcType.ArcStartPoint(Pi), new ArcType.ArcCenterPoint(Pc), new ArcType.ArcEndPoint(Pf), true);*/

			sAng = is2GraphObj.RadToGrad(A.StartAngle); 
			eAng = is2GraphObj.RadToGrad(A.EndAngle);

			Pc = Tois2Graph(A.Center);
			Pi = is2GraphObj.PolarPoint(Pc, sAng, A.Radius);
			Pf = is2GraphObj.PolarPoint(Pc, eAng, A.Radius);

			Arc = new ArcType(new ArcStartPoint(Pi), new ArcCenterPoint(Pc), new ArcEndPoint(Pf));

			return Arc;
		}

    //------------------------------------------------------------------------//
    // Convierte la entidad Line de ACAD a la entidad homologa de is2Graph.
    public static is2GraphObject.SegmentType Tois2Graph (Line L)
    {
      PointType Pi, Pf;

      Pi = Tois2Graph(L.StartPoint);
      Pf = Tois2Graph(L.EndPoint);

      return new is2GraphObject.SegmentType(Pi, Pf);
    }

    //------------------------------------------------------------------------//
    // Convierte la entidad Line de ACAD a la entidad homologa de is2Graph.
    public static is2GraphObject.SegmentType Tois2Graph(Line2d L)
    {
      PointType Pi, Pf;

      Pi = Tois2Graph(L.StartPoint);
      Pf = Tois2Graph(L.EndPoint);

      return new is2GraphObject.SegmentType(Pi, Pf);
    }

    //------------------------------------------------------------------------//
    // Convierte la entidad Line de ACAD a la entidad homologa de is2Graph.
    public static is2GraphObject.SegmentType Tois2Graph(Line3d L)
    {
      PointType Pi, Pf;

      Pi = Tois2Graph(L.StartPoint);
      Pf = Tois2Graph(L.EndPoint);

      return new is2GraphObject.SegmentType(Pi, Pf);
    }
  }
}
