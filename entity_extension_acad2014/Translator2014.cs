using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using is2GraphObject;
using Utilities_acad2014;
using Translator_is2graph_acad;

namespace EntityExtension_acad2014
{
	public class is2GraphTranslatorEx
	{
		/// <summary>
		/// Convierte la entidad "Arc" de is2Graph a su tipo equivalente de ACAD.
		/// </summary>
		/// <param name="A"></param>
		/// <param name="normal"></param>
		/// <returns></returns>
		public static ArcEx ToAcad (ArcType A, Vector3d normal)
		{
			PointType Pi, Pm, Pf;
			Point3d acPi, acPf, acPm;

			A.GetArc3Points(out Pi, out Pm, out Pf); AcadUtilities.AddPinToModel(is2GraphTranslator.ToAcad_DB(Pm), 34, 0.4);
			acPi = is2GraphTranslator.ToAcad_3d(Pi);
			acPm = is2GraphTranslator.ToAcad_3d(Pm);
			acPf = is2GraphTranslator.ToAcad_3d(Pf);

			return new ArcEx(new StartPointEx(acPi), new AnyPointEx(acPm), new EndPointEx(acPf), normal);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="A"></param>
		/// <returns></returns>
		public static ArcType Tois2Graph (Arc A)
		{
			double sAng, eAng;
			PointType Pi, Pf, Pc;

			sAng = is2GraphObj.RadToGrad(A.StartAngle);
			eAng = is2GraphObj.RadToGrad(A.EndAngle);

			Pc = is2GraphTranslator.Tois2Graph(A.Center);
			Pi = is2GraphObj.PolarPoint(Pc, sAng, A.Radius);
			Pf = is2GraphObj.PolarPoint(Pc, eAng, A.Radius);

			return new ArcType(new ArcStartPoint(Pi), new ArcCenterPoint(Pc), new ArcEndPoint(Pf));
		}
	}
}
