#pragma once

using namespace System;

namespace is2GraphObject {
	/*public enum Plane
	{
		XY,
		YZ,
		XZ
	};

	public enum CircleCircleRelation
	{
		Equal,
		Concentric,
		Interior,
		Exterior,
		Tangent_In,
		Tangent_Out,
		Secant
	};

	public enum CircleSegmentRelation
	{
		Exterior,
		Tangent,
		Secant,
		SimpleAcross
	};*/

	//=============================================================================================//
	// ClassName  : PointType
	//
	// Description: Representa un tipo Punto.
	//							
	// Revision   : 
	//=============================================================================================//
	/*class PointType
  {
    public:
			double cX, cY, cZ;

		public:
			PointType ();
			//PointType (PointType P);
			PointType (double x, double y);
			PointType (double x, double y, double z);
  };*/

	public struct PointType
	{
		double cX, cY, cZ;

		PointType ();
		PointType (const PointType &P);
		PointType (double x, double y);
		PointType (double x, double y, double z);
	};

	//=============================================================================================//
	// ClassName  : PolarPointType
	//
	// Description: Rereps
	//							
	// Revision   : 
	//=============================================================================================//



	//=============================================================================================//
	// ClassName  : LineType
	//
	// Description: Representa un tipo linea. Esta linea se define por un punto y un angulo, 
	//							dando como resultado una linea que pasa por el punto 'P' y tiene longitud
	//							infinita.
	//							
	// Revision   : 13-08-2017
	//=============================================================================================//



	//=============================================================================================//
	// ClassName  : CircleType
	//
	// Description: Representa un tipo Circunferencia
	//							
	// Revision   : 13-08-2017
	//=============================================================================================//



	//=============================================================================================//
	// ClassName  : SegmentType
	//
	// Description: Representa un tipo Segmento.
	//							
	// Revision   : 13-08-2017
	//=============================================================================================//



	//=============================================================================================//
	// ClassName  : ArcType
	//
	// Description: Representa un tipo Arco que puede ser definido de 7 formas.
	//							[1]: [Start, Any, End]
	//							[2]: [Start, Center, End]
	//							[3]: [Start, Center, Angle]
	//							[4]: [Start, Center, Length]
	//							[5]: [Start, End, Angle]
	//							[6]: [?, ?, ?]
	//							[7]: [Start, Center, Radius]
	//							
	// Revision   : 13-08-2017
	//=============================================================================================//



	//=============================================================================================//
	// ClassName  : PlaneType
	//
	// Description: ?
	//							
	//
	// Revision   : ?
	//=============================================================================================//



	//=============================================================================================//
	// ClassName  : PolylineType
	//
	// Description: ?
	//							
	//
	// Revision   : ?
	//=============================================================================================//



	//=============================================================================================//
	// ClassName  : 
	//
	// Description: 
	//							
	//
	// Revision   : 
	//=============================================================================================//
	/*static class is2GraphObj
  {
		private:
			static double CeroReal;
			static const PointType _Origen_();

		public:
			enum Quadrant { I, II, III, IV };
				  
			/*PointType OrigenXYZ () { 
				PointType O = _Origen_;
				return O; 
			}*/

		/*public:
			void SetPresicion (unsigned short d);
	};

	double is2GraphObj::CeroReal = 1E-6;*/
}
