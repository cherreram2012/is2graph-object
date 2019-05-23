using System;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using is2GraphObject;
using Translator_is2graph_acad;
using SegmentType = is2GraphObject.SegmentType;

namespace EntityExtension_acad2011
{
	public class DiameterEx
	{
		public double val { set; get; }

		public DiameterEx (double d)
		{
			val = d;
		}
	}

	public class CircleEx : Circle
	{
		public enum Type { Circunscripta = 1, Inscripta }

		//====================================================================//
		// Variante 2: Crea un Circulo usando un Punto y un valor de Diametro. 
		//====================================================================//
		public CircleEx (Point3d center, DiameterEx d, Vector3d normal)
		{
			//Center
			//ArcRadius
			//Normal
			//Thickness = 0.0;
		}

		//====================================================================//
		// Variante 3: Crea un Circulo usando 2 puntos.
		//====================================================================//
		public CircleEx (Point3d P1, Point3d P2, Vector3d normal)
		{
			PointType p1, p2, center;

			p1 = is2GraphTranslator.Tois2Graph(P1);
			p2 = is2GraphTranslator.Tois2Graph(P2);

			center = is2GraphObj.MidPointBetweenPoint(p1, p2);

			Center = is2GraphTranslator.ToAcad_3d(center);
			Radius = is2GraphObj.PointPointDistance(center, p1);
			Normal = normal;
			Thickness = 0.0;
		}

		//====================================================================//
		// Variante 4: Crea un Circulo usando 3 puntos.
		//====================================================================//
		public CircleEx (Point3d P1, Point3d P2, Point3d P3, Vector3d normal, Type t = Type.Circunscripta)
		{
			double radius;
			PointType p1, p2, p3, center;

			center = new PointType();

			p1 = is2GraphTranslator.Tois2Graph(P1);
			p2 = is2GraphTranslator.Tois2Graph(P2);
			p3 = is2GraphTranslator.Tois2Graph(P3);

			// La circunferencia circunscripta se calcula a partir del circuncentro.
			// Nota: el circuncentro es el punto en el que se intersecan las mediatrices de un triangulo.
			// La mediatriz de un lado de un triángulo es la recta perpendicular a dicho lado trazada por 
			// su punto medio. El circuncentro equidista de los tres vértices del triangulo.
			if (t == Type.Circunscripta)
			{
				LineType L1, L2;
				SegmentType S1, S2;

				S1 = new SegmentType(p1, p2);
				S2 = new SegmentType(p2, p3);

				L1 = is2GraphObj.PerperdicularLineAt(S1.ConvertToLine(), S1.MidPoint);
				L2 = is2GraphObj.PerperdicularLineAt(S2.ConvertToLine(), S2.MidPoint);

				is2GraphObj.LineLineIntercept(L1, L2, out center);

				radius = is2GraphObj.PointPointDistance(center, p1);
			}
			// La circunferencia inscripta se calcula a partir del Incentro.
			// Nota: El Incentro es el punto en el que se intersecan las tres bisectrices de los ángulos 
			// interiores del triángulo, este punto equidista de los tres lados del triangulo, siendo 
			// tangente ademas a ellos.
			else
			{
				double a, b, c;

				// a - Lado opuesto a P1. (P2-P3)
				a = is2GraphObj.PointPointDistance(p2, p3);
				// b - Lado opuesto a P2. (P1-P3)
				b = is2GraphObj.PointPointDistance(p1, p3);
				// c - Lado opuesto a P3. (P1-P2)
				c = is2GraphObj.PointPointDistance(p1, p2);

				center.cX = (a * p1.cX + b * p2.cX + c * p3.cX) / (a + b + c);
				center.cY = (a * p1.cY + b * p2.cY + c * p3.cY) / (a + b + c);
				center.cZ = 0.0;

				radius = is2GraphObj.PointLineDistance(center, new SegmentType(p1, p2).ConvertToLine());
			}

			Center = is2GraphTranslator.ToAcad_3d(center);
			Radius = radius;
			Normal = normal;
			Thickness = 0.0;
		}

		//====================================================================//
		// Variante 5: Crea un Circulo dos rectas tangentes y en valor de radio.
		//====================================================================//
		public CircleEx (Line L1, Line L2, double r, Vector3d normal, bool s1_right = true, bool s2_right = true)
		{


			//Center = is2GraphTranslator.ToAcad_3d(center);
			//RadiusEx = radius;
			Normal = normal;
			Thickness = 0.0;
		}

		//====================================================================//
		// Variante 6: Crea un Circulo 3 rectas tangentes.
		//====================================================================//
		public CircleEx (Line L1, Line L2, Line L3, Vector3d normal, bool right_up = true)
		{
			byte intersec;
			bool x1, x2, x3;
			PointType A, B, C, Ps1, Ps2, P;
			SegmentType S1, S2, S3, Sp1, Sp2, Ss;

			S1 = is2GraphTranslator.Tois2Graph(L1);
			S2 = is2GraphTranslator.Tois2Graph(L2);
			S3 = is2GraphTranslator.Tois2Graph(L3);

			intersec = 0;
			x1 = is2GraphObj.SegmentsApparentIntercept(S1, S2, out A);
			x2 = is2GraphObj.SegmentsApparentIntercept(S2, S3, out B);
			x3 = is2GraphObj.SegmentsApparentIntercept(S3, S1, out C);

			if (x1) intersec++;
			if (x2) intersec++;
			if (x3) intersec++;

			if (intersec < 2 || (is2GraphObj.isEqualPoint(A, B) || is2GraphObj.isEqualPoint(B, C) || is2GraphObj.isEqualPoint(C, A)))
			{
				throw new CircleException("No se puede crear una circunferencia tangente a los 3 segmentos dados.");
			}

			if (!x1)
			{
				Sp1 = S1;
				//Sp2 = S2;
				Ss  = S3;
			}
			else if (!x2)
			{
				Sp1 = S2;
				//Sp2 = S3;
				Ss  = S1;
			}
			else
			{
				Sp1 = S3;
				//Sp2 = S1;
				Ss  = S2;
			}

			if (right_up)
			{
				P = (Sp1.isVertical) ? Sp1.PointMayorY() : Sp1.PointMayorX();
			}
			else
			{
				P = (Sp1.isVertical) ? Sp1.PointMenorY() : Sp1.PointMenorX();
			}


			Utilities_acad2014.AcadUtilities.AddPinToModel(is2GraphTranslator.ToAcad_DB(A), 34, 0.3, 2);
			Utilities_acad2014.AcadUtilities.AddPinToModel(is2GraphTranslator.ToAcad_DB(B), 34, 0.3, 2);
			Utilities_acad2014.AcadUtilities.AddPinToModel(is2GraphTranslator.ToAcad_DB(C), 34, 0.3, 2);

			//Center = is2GraphTranslator.ToAcad_3d(center);
			//RadiusEx = radius;
			Normal = normal;
			Thickness = 0.0;
		}
	}

	//=============================================================================================//
	// ClassName  : ArcException
	//
	// Description: Representa una exception de tipo Arc Error.
	//
	// Revision   : 30-06-2017
	//=============================================================================================//
	public class CircleException : Exception
	{
		public CircleException ()
		{
		}

		public CircleException (string msg)
			: base(msg)
		{
		}
	}
}
