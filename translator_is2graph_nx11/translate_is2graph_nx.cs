using System;
using NXOpen;
using NXOpen.UF;
using is2GraphObject;

namespace Translator_is2graph_nx
{
  public class is2GraphTranslator
  {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="P"></param>
		/// <returns></returns>
		public static Point3d Nx_2dTo3d (Point2d P)
		{
			return new Point3d(P.X, P.Y, 0.0);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="P"></param>
		/// <returns></returns>
		public static Point2d Nx_3dTo2d (Point3d P)
		{
			return new Point2d(P.X, P.Y);
		}

    //------------------------------------------------------------------------//
    // Convierte la entidad <Point> de is2Graph a su tipo equivalente de NX-2d.
		/// <summary>
		/// 
		/// </summary>
		/// <param name="P"></param>
		/// <returns></returns>
    public static Point2d ToNx_2d (PointType P)
    {
      return new Point2d(P.cX, P.cY);
    }

    //------------------------------------------------------------------------//
		// Convierte la entidad <Point> de is2Graph a su tipo equivalente de ACAD-3d.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="P"></param>
    /// <returns></returns>
		public static Point3d ToNx_3d (PointType P)
    {
      return new Point3d(P.cX, P.cY, P.cZ);
    }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="P"></param>
		/// <returns></returns>
	  public static double[] ToNx (PointType P)
	  {
		  var coord = new double[3];

		  coord[0] = P.cX;
			coord[1] = P.cY;
			coord[2] = P.cZ;

		  return coord;
	  }

	  //------------------------------------------------------------------------//
		// Convierte la entidad <Segment> de is2Graph a su tipo equivalente de ACAD.
		/// <summary>
		/// 
		/// </summary>
		/// <param name="S"></param>
		/// <returns></returns>
		public static UFCurve.Line ToNx (is2GraphObject.SegmentType S)
		{
			UFCurve.Line L = new UFCurve.Line();

			L.start_point = ToNx(S.StartPoint);
			L.end_point = ToNx(S.EndPoint);

			return L;
		}

    //------------------------------------------------------------------------//
    // Convierte la entidad <Circle> de is2Graph a su tipo equivalente de NX.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="C"></param>
    /// <returns></returns>
		public static UFCurve.Arc ToNx (CircleType C)
    {
			UFCurve.Arc nxC = new UFCurve.Arc();

			nxC.start_angle = 0.0;
			nxC.end_angle = 2 * Math.PI;
	    nxC.arc_center = ToNx(C.Center);
			nxC.radius = C.Radius;

	    return nxC;
    }

    //------------------------------------------------------------------------//
    // Convierte la entidad
    /// <summary>
    /// 
    /// </summary>
    /// <param name="P"></param>
    /// <returns></returns>
		public static PointType Tois2Graph (Point2d P)
    {
      return new PointType(P.X, P.Y, 0.0);
    }

    //------------------------------------------------------------------------//
    // Convierte la entidad
    /// <summary>
    /// 
    /// </summary>
    /// <param name="P"></param>
    /// <returns></returns>
		public static PointType Tois2Graph (Point3d P)
    {
      return new PointType(P.X, P.Y, P.Z);
    }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="coord"></param>
		/// <returns></returns>
		public static PointType Tois2Graph (double []coord)
	  {
			return new PointType(coord[0], coord[1], coord[2]);
	  }

	  //------------------------------------------------------------------------//
		// Convierte la entidad Circle de ACAD a la entidad homologa de is2Graph.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="C"></param>
    /// <returns></returns>
		public static CircleType Tois2Graph (UFCurve.Arc C)
    {
      return new CircleType(Tois2Graph(C.arc_center), C.radius);
    }

		//------------------------------------------------------------------------//
		// Convierte la entidad Arc de ACAD a la entidad homologa de is2Graph.
		/*public static ArcType Tois2Graph(Arc A)
		{
			ArcType Arc;
			double sAng, eAng;
			PointType Pi, Pf, Pc;

			sAng = is2GraphObj.RadToGrad(A.StartAngle); 
			eAng = is2GraphObj.RadToGrad(A.EndAngle);

			Pc = Tois2Graph(A.Center);
			Pi = is2GraphObj.PolarPoint(Pc, sAng, A.Radius);
			Pf = is2GraphObj.PolarPoint(Pc, eAng, A.Radius);

			Arc = new ArcType(new ArcType.ArcStartPoint(Pi), new ArcType.ArcCenterPoint(Pc), new ArcType.ArcEndPoint(Pf));

			return Arc;
		}

    //------------------------------------------------------------------------//
    // Convierte la entidad Line de ACAD a la entidad homologa de is2Graph.
    public static is2GraphObject.SegmentType Tois2Graph (Line L)
    {
      PointType Pi, Pf;

      Pi = Tois2Graph(L.ArcStartPoint);
      Pf = Tois2Graph(L.ArcEndPoint);

      return new is2GraphObject.SegmentType(Pi, Pf);
    }

    //------------------------------------------------------------------------//
    // Convierte la entidad Line de ACAD a la entidad homologa de is2Graph.
    public static is2GraphObject.SegmentType Tois2Graph(Line2d L)
    {
      PointType Pi, Pf;

      Pi = Tois2Graph(L.ArcStartPoint);
      Pf = Tois2Graph(L.ArcEndPoint);

      return new is2GraphObject.SegmentType(Pi, Pf);
    }

    //------------------------------------------------------------------------//
    // Convierte la entidad Line de ACAD a la entidad homologa de is2Graph.
    public static is2GraphObject.SegmentType Tois2Graph(Line3d L)
    {
      PointType Pi, Pf;

      Pi = Tois2Graph(L.ArcStartPoint);
      Pf = Tois2Graph(L.ArcEndPoint);

      return new is2GraphObject.SegmentType(Pi, Pf);
    }*/
  }
}
