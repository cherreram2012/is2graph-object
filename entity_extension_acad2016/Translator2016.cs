using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using is2GraphObject;
using Utilities_acad2016;
using Translator_is2graph_acad;

namespace EntityExtension_acad2016
{
	public class is2GraphTranslatorEx
	{
		/// <summary>
		/// Convierte la entidad ArcType de is2Graph a su tipo equivalente de ACAD. 
		/// </summary>
		/// <param name="Arc"></param>
		/// <param name="normal"></param>
		/// <returns></returns>
		public static ArcEx ToAcad (ArcType Arc, Vector3d normal)
		{
			PointType Pi, Pm, Pf;
			Point3d acPi, acPf, acPm;

			Arc.GetArc3Points(out Pi, out Pm, out Pf);
			acPi = is2GraphTranslator.ToAcad_3d(Pi); AcadUtilities.AddPinToModel(is2GraphTranslator.ToAcad_DB(Pi), 34, 0.4);
			acPm = is2GraphTranslator.ToAcad_3d(Pm); AcadUtilities.AddPinToModel(is2GraphTranslator.ToAcad_DB(Pm), 34, 0.4);
			acPf = is2GraphTranslator.ToAcad_3d(Pf);

			return new ArcEx(new StartPointEx(acPi), new AnyPointEx(acPm), new EndPointEx(acPf), normal);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="A"></param>
		/// <returns></returns>
		public static ArcType Tois2Graph(Arc A)
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
