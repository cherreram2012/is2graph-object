using Autodesk.AutoCAD.Geometry;
using EntityExtension_acad2016;
using is2GraphObject;
using Utilities_acad2016;
using Translator_is2graph_acad;

namespace EntityExtension_acad2018
{
	public class is2GraphTranslatorEx
	{
		//------------------------------------------------------------------------//
		// Convierte la entidad <Arc> de is2Graph a su tipo equivalente de ACAD.
		public static ArcEx ToAcad(ArcType Arc, Vector3d normal)
		{
			PointType Pi, Pm, Pf;
			Point3d acPi, acPf, acPm;

			Arc.GetArc3Points(out Pi, out Pm, out Pf); AcadUtilities.AddPinToModel(is2GraphTranslator.ToAcad_DB(Pm), 34, 0.4);
			acPi = is2GraphTranslator.ToAcad_3d(Pi);
			acPm = is2GraphTranslator.ToAcad_3d(Pm);
			acPf = is2GraphTranslator.ToAcad_3d(Pf);

			return new ArcEx(new StartPointEx(acPi), new AnyPointEx(acPm), new EndPointEx(acPf), normal);
		}
	}
}
