using System;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using is2GraphObject;

namespace Translator_is2graph_acad
{
	/// <summary>
	/// 
	/// </summary>
	public class is2GraphTranslator
	{
		#region - ACAD to ACAD -
		/// <summary>
		/// 
		/// </summary>
		/// <param name="P"></param>
		/// <returns></returns>
		public static Point3d Acad_2dTo3d (Point2d P)
		{
			return new Point3d(P.X, P.Y, 0.0);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="P"></param>
		/// <returns></returns>
		public static Point2d Acad_3dTo2d(Point3d P)
		{
			return new Point2d(P.X, P.Y);
		}

		//------------------------------------------------------------------------//
		// Convierte la entidad <Point2d> de ACAD a su tipo equivalente de ACAD-3d
		// Database.
		/// <summary>
		/// 
		/// </summary>
		/// <param name="P"></param>
		/// <returns></returns>
		public static DBPoint ToAcad_DB (Point2d P)
		{
			return new DBPoint(new Point3d(P.X, P.Y, 0.0));
		}

		//------------------------------------------------------------------------//
		// Convierte la entidad <Point3d> de ACAD a su tipo equivalente de ACAD-3d
		// Database.
		/// <summary>
		/// 
		/// </summary>
		/// <param name="P"></param>
		/// <returns></returns>
		public static DBPoint ToAcad_DB (Point3d P)
		{
			return new DBPoint(new Point3d(P.X, P.Y, P.Z));
		}
		#endregion

		#region - is2GraphObj to ACAD -
		//------------------------------------------------------------------------//
		// Convierte la entidad <Point> de is2Graph a su tipo equivalente de ACAD-3d
		// Database.
		/// <summary>
		/// 
		/// </summary>
		/// <param name="P"></param>
		/// <returns></returns>
		public static DBPoint ToAcad_DB (PointType P)
		{
			return new DBPoint(new Point3d(P.cX, P.cY, P.cZ));
		}

		/// <summary>
		/// Convierte la entidad Point de is2Graph a su tipo equivalente de ACAD-2d.
		/// </summary>
		/// <param name="P"></param>
		/// <returns></returns>
		public static Point2d ToAcad_2d (PointType P)
		{
			return new Point2d(P.cX, P.cY);
		}

		//------------------------------------------------------------------------//
		// Convierte la entidad <Point> is2Graph a su tipo equivalente de ACAD-2d 
		// Geometry.
		/// <summary>
		/// 
		/// </summary>
		/// <param name="P"></param>
		/// <returns></returns>
		public static Point2d ToAcad_2dG (PointType P)
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
		public static Point3d ToAcad_3d (PointType P)
		{
			return new Point3d(P.cX, P.cY, P.cZ);
		}

		//------------------------------------------------------------------------//
		// Convierte la entidad <Point> de is2Graph a su tipo equivalente de ACAD-3d
		// Geometry.
		/// <summary>
		/// 
		/// </summary>
		/// <param name="P"></param>
		/// <returns></returns>
		public static Point3d ToAcad_3dG (PointType P)
		{
			return new Point3d(P.cX, P.cY, P.cZ);
		}

		//------------------------------------------------------------------------//
		// Convierte la entidad <Segment> de is2Graph a su tipo equivalente de ACAD.
		/// <summary>
		/// 
		/// </summary>
		/// <param name="S"></param>
		/// <returns></returns>
		public static Line ToAcad (is2GraphObject.SegmentType S)
		{
			return new Line(ToAcad_3d(S.StartPoint), ToAcad_3d(S.EndPoint));
		}

		/// <summary>
		/// Convierte la entidad "Circle" de is2Graph a su tipo equivalente de ACAD.
		/// </summary>
		/// <param name="C"></param>
		/// <param name="normal"></param>
		/// <returns></returns>
		public static Circle ToAcad (CircleType C, Vector3d normal)
		{
			return new Circle(ToAcad_3d(C.Center), normal, C.Radius);
		}

		/// <summary>
		/// Convierte la entidad "Arc" de is2GraphObj a su equivalente de ACAD.
		/// </summary>
		/// <param name="A"></param>
		/// <param name="normal"></param>
		/// <returns></returns>
		public static Arc ToAcad (ArcType A, Vector3d normal)
		{
			return new Arc(ToAcad_3d(A.Center), normal, A.Radius, A.StartAngle, A.EndAngle);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Pl"></param>
		/// <param name="normal"></param>
		/// <returns></returns>
		public static Polyline ToAcad (PolylineType Pl, Vector3d normal)
		{
			int i, index;
			Polyline acPl;
			PolylineElement elem;

			i = 0;
			index = 0;
			acPl = new Polyline();
			acPl.Closed = false;

			while ((elem = Pl.getByIndex(index++)) != null)
			{
				switch (elem.TypeElement)
				{
					case PolylineElementType.Point:
						acPl.AddVertexAt(i, ToAcad_2d(elem.Point), 0, 0, 0);
						i++;
					break;

					case PolylineElementType.Segment:
						if (i == 0)
						{
							acPl.AddVertexAt(0, ToAcad_2d(elem.Segment.StartPoint), 0, 0, 0);
							acPl.AddVertexAt(1, ToAcad_2d(elem.Segment.EndPoint), 0, 0, 0);
							i = 2;
						}
						else
						{
							acPl.JoinEntity(ToAcad(elem.Segment));
							i++;
						}
					break;

					case PolylineElementType.Arc:
						if (i == 0)
						{
							acPl.AddVertexAt(0, ToAcad_2d(elem.Arc.StartPoint), 0, 0, 0);
							acPl.JoinEntity(ToAcad(elem.Arc, normal));
							acPl.ReverseCurve();
							i = 2;
						}
						else
						{
							acPl.JoinEntity(ToAcad(elem.Arc, normal));
							i++;
						}
					break;
				}
			}

			return acPl;
		}
		#endregion

		#region - ACAD to is2GraphObj -
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

		//------------------------------------------------------------------------//
		// Convierte la entidad Circle de ACAD a la entidad homologa de is2Graph.
		/// <summary>
		/// 
		/// </summary>
		/// <param name="C"></param>
		/// <returns></returns>
		public static CircleType Tois2Graph (Circle C)
		{
			return new CircleType(Tois2Graph(C.Center), C.Radius);
		}

		//------------------------------------------------------------------------//
		// Convierte la entidad Arc de ACAD a la entidad homologa de is2Graph.
		/// <summary>
		/// 
		/// </summary>
		/// <param name="A"></param>
		/// <returns></returns>
		public static ArcType Tois2Graph (Arc A)
		{
			ArcType Arc;
			double sAng, eAng;
			PointType Pi, Pf, Pc;

			sAng = is2GraphObj.RadToGrad(A.StartAngle);
			eAng = is2GraphObj.RadToGrad(A.EndAngle);

			Pc = Tois2Graph(A.Center);
			Pi = is2GraphObj.PolarPoint(Pc, sAng, A.Radius);
			Pf = is2GraphObj.PolarPoint(Pc, eAng, A.Radius);

			return new ArcType(new ArcStartPoint(Pi), new ArcCenterPoint(Pc), new ArcEndPoint(Pf));
		}

		//------------------------------------------------------------------------//
		// Convierte la entidad Line de ACAD a la entidad homologa de is2Graph.
		/// <summary>
		/// 
		/// </summary>
		/// <param name="L"></param>
		/// <returns></returns>
		public static is2GraphObject.SegmentType Tois2Graph (Line L)
		{
			PointType Pi, Pf;

			Pi = Tois2Graph(L.StartPoint);
			Pf = Tois2Graph(L.EndPoint);

			return new is2GraphObject.SegmentType(Pi, Pf);
		}

		//------------------------------------------------------------------------//
		// Convierte la entidad Line de ACAD a la entidad homologa de is2Graph.
		/// <summary>
		/// 
		/// </summary>
		/// <param name="L"></param>
		/// <returns></returns>
		public static is2GraphObject.SegmentType Tois2Graph(Line2d L)
		{
			PointType Pi, Pf;

			Pi = Tois2Graph(L.StartPoint);
			Pf = Tois2Graph(L.EndPoint);

			return new is2GraphObject.SegmentType(Pi, Pf);
		}

		//------------------------------------------------------------------------//
		// Convierte la entidad Line de ACAD a la entidad homologa de is2Graph.
		/// <summary>
		/// 
		/// </summary>
		/// <param name="L"></param>
		/// <returns></returns>
		public static is2GraphObject.SegmentType Tois2Graph(Line3d L)
		{
			PointType Pi, Pf;

			Pi = Tois2Graph(L.StartPoint);
			Pf = Tois2Graph(L.EndPoint);

			return new is2GraphObject.SegmentType(Pi, Pf);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static ElipseType Tois2Graph(Ellipse E)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}