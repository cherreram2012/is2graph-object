using System;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using is2GraphObject;
using Translator_is2graph_acad;
using SegmentType = is2GraphObject.SegmentType;

namespace EntityExtension_acad2011
{
	public class ArcEx : Arc
	{
    private enum ArcDirection { AntiHorario, Horario };

    //====================================================================//
    // Variante 1: Crea un Arco usando 3 puntos [Start, Mid, End]
		//====================================================================//
		public ArcEx (StartPointEx start, AnyPointEx any, EndPointEx end, Vector3d normal)
		{			
			LineType L1, L2;
			SegmentType S1, S2;
			ArcDirection direction;
			PointType pC, P1, P2, Px;
			double hipo, s_Ang, e_Ang;

			// Paso 1: Creo dos mediatrices sobre los segmentos S1 y S2.
			//				 Estas 2 bisectrices se cortan sobre el centro del arco.
			P1 = is2GraphTranslator.Tois2Graph(start.val);
			P2 = is2GraphTranslator.Tois2Graph(end.val);
			Px = is2GraphTranslator.Tois2Graph(any.val);

			S1 = new SegmentType(P1, Px);
			S2 = new SegmentType(Px, P2);

			L1 = is2GraphObj.PerperdicularLineAt(S1.ConvertToLine(), S1.MidPoint);
			L2 = is2GraphObj.PerperdicularLineAt(S2.ConvertToLine(), S2.MidPoint);

			is2GraphObj.LineLineIntercept(L1, L2, out pC);

			// Paso 2: Determinar distancia entre "P1" y "pC", que es el radio del arco.
			hipo = is2GraphObj.PointPointDistance(pC, P1);

			// b) Calculo los valores en radianes de Inicio Y Fin del arco
			s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(pC, P1));
			e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(pC, P2));

			direction = GetArcDirection_3P(start.val, any.val, end.val);
			if (direction == ArcDirection.Horario) is2GraphObj.SwapValue(ref s_Ang, ref e_Ang);

			// Paso 3 y Final: Seteo las propiedades heredadas de la clase padre 'Arc'.
			Center = is2GraphTranslator.ToAcad_3d(pC);
			StartAngle = s_Ang;
			EndAngle = e_Ang;
			Normal = normal;
			Radius = hipo;
			Thickness = 0.0;
    }

		//====================================================================//
		// Variante 2: Crea un Arco usando 3 puntos [Start, Center, End]
		//====================================================================//
		public ArcEx (StartPointEx start, CenterPointEx center, EndPointEx end, Vector3d normal, bool inverseFocus = false)
    {
			PointType P1, P2, pC;
			double hipo, s_Ang, e_Ang;

			P1 = is2GraphTranslator.Tois2Graph(start.val);
			P2 = is2GraphTranslator.Tois2Graph(end.val);
			pC = is2GraphTranslator.Tois2Graph(center.val);

			// Paso 1: Determinar distancia entre "Start" y "Center".
			hipo  = is2GraphObj.PointPointDistance(pC, P1);

			// b) Calculo los valores en radianes de Inicio Y Fin del arco
			//    usando para el calculo el Angulo opuesto al angulo del eje 
			//    radial.
			s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(pC, P1));
			e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(pC, P2));

			if (inverseFocus) is2GraphObj.SwapValue(ref s_Ang, ref e_Ang);

			// Paso 2 y Final: Seteo las propiedades heredadas de la clase padre 'Arc'.
			Center = center.val;
			StartAngle = s_Ang;
			EndAngle = e_Ang;
			Normal = normal;
			Radius = hipo;
			Thickness = 0.0;			
    }

		//====================================================================//
		// Variante 3: Crea un Arco usando 2 puntos y un angulo 
		// [Start, Center, angleAgudo]
		//====================================================================//
		public ArcEx (StartPointEx start, CenterPointEx center, GradeAngleEx angleEx, Vector3d normal, bool inverseFocus = false)
    {
			PointType pC, P1, P2;
			double hipo, s_Ang, e_Ang;

			// Paso 1: Normalizo el valor del angulo y compruebo su signo.
			angleEx.val = is2GraphObj.NormalizeAngle(angleEx.val);

			if (is2GraphObj.isNegative(angleEx.val) && inverseFocus == false)
			{
				inverseFocus = true;
			}
			else if (inverseFocus && is2GraphObj.isPositive(angleEx.val))
			{
				angleEx.val *= -1;
			}
			else
			{
				inverseFocus = false;
				angleEx.val = Math.Abs(angleEx.val);
			}

			// Paso 2: Calculo la posicion del punto "End" a partir de los datos conocidos  
			//				 de los puntos "Center" y "Start",  y el angulo especificado. 
			P1 = is2GraphTranslator.Tois2Graph(start.val);
			pC = is2GraphTranslator.Tois2Graph(center.val);
			P2 = is2GraphObj.RotatePoint(P1, angleEx.val, pC);

			// Paso 3: Se calculo el radio del arco.
			hipo = is2GraphObj.PointPointDistance(pC, P1);

			// Paso 4: Se calculan los valores en radianes de Inicio Y Fin del arco.		
			s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(pC, P1));
			e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(pC, P2));

			if (inverseFocus) is2GraphObj.SwapValue(ref s_Ang, ref e_Ang);

			// Paso 5 y Final: Seteo las propiedades heredadas de la clase padre 'Arc'.
			Center = is2GraphTranslator.ToAcad_3d(pC);
			StartAngle = s_Ang;
			EndAngle = e_Ang;
			Normal = normal;
			Radius = hipo;
			Thickness = 0.0;
    }

    //====================================================================//
		// Variante 4: Crea un Arco usando 2 puntos y una longitud 
		// [Start, Center, Length]
		//====================================================================//
		public ArcEx (StartPointEx start, CenterPointEx center, DistanceEx length, Vector3d normal, bool inverseFocus = false)
    {
			PointType pC, P1, P2;
			double hipo, angle, s_Ang, e_Ang;

			// Paso 1: Se calcula el valor de angulo que se forma para el valor de distancia dado.
			P1 = is2GraphTranslator.Tois2Graph(start.val);
			pC = is2GraphTranslator.Tois2Graph(center.val);

			hipo = is2GraphObj.PointPointDistance(pC, P1);
			angle = is2GraphObj.RadToGrad(Math.Asin((length.val / 2.0) / hipo)) * 2.0;
			if (inverseFocus) angle *= -1;

			// Se lanza una exception si se obtiene un Arco invalido. 
			if (double.IsNaN(angle))
				throw new ArcException("ArcEx Class: No se puede obtener un arco con el valor de longitud dado.");

			// Paso 2: Calculo la posicion del punto "End" a partir de los datos conocidos  
			//				 de los puntos "Center" y "Start",  y el angulo calculado. 
			P2 = is2GraphObj.RotatePoint(P1, angle, pC);

			// Paso 3: Se calculan los valores en radianes de Inicio Y Fin del arco.		
			s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(pC, P1));
			e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(pC, P2));

			if (inverseFocus) is2GraphObj.SwapValue(ref s_Ang, ref e_Ang);

			// Paso 4 y Final: Seteo las propiedades heredadas de la clase padre 'Arc'.
			Center = is2GraphTranslator.ToAcad_3d(pC);
			StartAngle = s_Ang;
			EndAngle = e_Ang;
			Normal = normal;
			Radius = hipo;
			Thickness = 0.0;
    }

		//====================================================================//
		// Variante 5: Crea un Angulo usando [Start, End, angleEx]
		//====================================================================//
		public ArcEx (StartPointEx start, EndPointEx end, GradeAngleEx angleEx, Vector3d normal, bool inverseFocus = false)
		{
      CircleType C1, C2;
      ArcDirection direction;
			PointType pC, P1, P2, midP1_2, P3, P4, pivote = null;
      double dist, hipo, cateL, angEjeRadial, s_Ang, e_Ang;

			// Paso 1: Normalizo el valor del angulo y compruebo su signo.
			angleEx.val = is2GraphObj.NormalizeAngle(angleEx.val);
			if (is2GraphObj.isNegative(angleEx.val))
			{
				angleEx.val *= -1;
				inverseFocus = !inverseFocus;
			}

			direction = GetArcDirection_2P(start.val, end.val);

			// Paso 2: Determinar distancia entre "Start" y "End" y el punto medio entre ambos.
      P1 = is2GraphTranslator.Tois2Graph(start.val);
      P2 = is2GraphTranslator.Tois2Graph(end.val);	
					
			dist = is2GraphObj.PointPointDistance(P1, P2);
      midP1_2 = is2GraphObj.MidPointBetweenPoint(P1, P2);

      // Paso 3: Se determinan 2 puntos (P3 y P4) en el EJE RADIAL, creando dos circunferencias
      //          auxiliares con centro en 'P2' y 'P2', respectivamente y radio 'radio'.      
			C1 = new CircleType(P1, dist);
			C2 = new CircleType(P2, dist);
      is2GraphObj.CircleCircleIntercept(C1, C2, out P3, out P4);
		
			// Paso 4: Una vez obtenido los dos puntos del eje Radial. Se determina 
      //         cual de los dos puntos se usara para traza la linea sobre 
      //         la que se halla el CENTRO del arco. 
			//         Esto se halla usando la siguiente Estrategia:
      if (direction == ArcDirection.AntiHorario)
      {
        // Si los puntos del eje radial (P3, P4) tienen la misma 'X', y de los puntos  
        // de entrada P2 tiene mayor 'X' que P2 ---> entonces tomar el Punto del eje
        // Radial de menor 'Y'.
        if (P3.cX.Equals(P4.cX) && P1.cX > P2.cX) pivote = (P3.cY < P4.cY) ? P3 : P4;
				// Si los puntos del eje radial (P3, P4) tienen la misma 'X', y de los puntos 
				// de entrada P2 tiene menor 'X' que P2 ---> entonces tomar el Punto del eje
				// Radial con mayor 'Y'.
				if (P3.cX.Equals(P4.cX) && P1.cX < P2.cX) pivote = (P3.cY > P4.cY) ? P3 : P4;
				// Si los puntos del eje radial (P3, P4) tienen la misma 'Y', y de los puntos 
        // de entrada P2 tiene menor 'Y' que P2 ---> entonces tomar el Punto del eje
        // Radial con menor 'X'.
        if (P3.cY.Equals(P4.cY) && P1.cY < P2.cY) pivote = (P3.cX < P4.cX) ? P3 : P4;
				// Si los puntos del eje radial (P3, P4) tienen la misma 'Y', y de los puntos 
				// de entrada P2 tiene mayor 'Y' que P2 ---> entonces tomar el Punto del eje
				// Radial con mayor 'X'.
				if (P3.cY.Equals(P4.cY) && P1.cY > P2.cY) pivote = (P3.cX > P4.cX) ? P3 : P4;

        // Si los puntos de Eje Radial (P3 y P4) son distintos tanto en la 'X' 
				// como en la 'Y', se usan entonces para el analisis los puntos del Eje
				// Central. Por lo tanto:
				// Si de los puntos del Eje Central P2 tiene mayor 'X' y menor 'Y' que P2,
				// ---> entonces tomar el Punto del eje Radial con menor 'X'.
        if (P1.cX > P2.cX && P1.cY < P2.cY) pivote = (P3.cX < P4.cX) ? P3 : P4;
				// Si de los puntos del Eje Central P2 tiene mayor 'X' y mayor 'Y' que P2,
				// ---> entonces tomar el Punto del eje Radial con mayor 'X'.
        if (P1.cX > P2.cX && P1.cY > P2.cY) pivote = (P3.cX > P4.cX) ? P3 : P4;
				// Si de los puntos del Eje Central P2 tiene menor 'X' y mayor 'Y' que P2,
				// ---> entonces tomar el Punto del eje Radial con mayor 'X'.
        if (P1.cX < P2.cX && P1.cY > P2.cY) pivote = (P3.cX > P4.cX) ? P3 : P4;
				// Si de los puntos del Eje Central P2 tiene menor 'X' y menor 'Y' que P2,
				// ---> entonces tomar el Punto del eje Radial con menor 'X'.
        if (P1.cX < P2.cX && P1.cY < P2.cY) pivote = (P3.cX < P4.cX) ? P3 : P4;
      }
      else
      {
				// Si los puntos del eje radial (P3, P4) tienen la misma 'X', y de los puntos  
				// de entrada P2 tiene menor 'X' que P2 ---> entonces tomar el Punto del eje
				// Radial de mayor 'Y'.
				if (P3.cX.Equals(P4.cX) && P1.cX < P2.cX) pivote = (P3.cY > P4.cY) ? P3 : P4;
				// Si los puntos del eje radial (P3, P4) tienen la misma 'X', y de los puntos 
				// de entrada P2 tiene mayor 'X' que P2 ---> entonces tomar el Punto del eje
				// Radial con menor 'Y'.
				if (P3.cX.Equals(P4.cX) && P1.cX > P2.cX) pivote = (P3.cY < P4.cY) ? P3 : P4;
				// Si los puntos del eje radial (P3, P4) tienen la misma 'Y', y de los puntos 
				// de entrada P2 tiene mayor 'Y' que P2 ---> entonces tomar el Punto del eje
				// Radial con mayor 'X'.
				if (P3.cY.Equals(P4.cY) && P1.cY > P2.cY) pivote = (P3.cX > P4.cX) ? P3 : P4;
				// Si los puntos del eje radial (P3, P4) tienen la misma 'Y', y de los puntos
				// de entrada P2 tiene menor 'Y' que P2 ---> entonces tomar el Punto del eje
				// Radial con menor 'X'.
				if (P3.cY.Equals(P4.cY) && P1.cY < P2.cY) pivote = (P3.cX < P4.cX) ? P3 : P4;

				// Si los puntos de Eje Radial (P3 y P4) son distintos tanto en la 'X' 
				// como en la 'Y', se usan entonces para el analisis los puntos del Eje
				// Central. Por lo tanto:
				// Si de los puntos del Eje Central P2 tiene menor 'X' y mayor 'Y' que P2,				
				// ---> entonces tomar el Punto del eje Radial con mayor 'X'.
				if (P1.cX < P2.cX && P1.cY > P2.cY) pivote = (P3.cX > P4.cX) ? P3 : P4;
				// Si de los puntos del Eje Central P2 tiene menor 'X' y menor 'Y' que P2,
				// ---> entonces tomar el Punto del eje Radial con menor 'X'.
				if (P1.cX < P2.cX && P1.cY < P2.cY) pivote = (P3.cX < P4.cX) ? P3 : P4;
				// Si de los puntos del Eje Central P2 tiene mayor 'X' y menor 'Y' que P2,
				// ---> entonces tomar el Punto del eje Radial con menor 'X'.
				if (P1.cX > P2.cX && P1.cY < P2.cY) pivote = (P3.cX < P4.cX) ? P3 : P4;
				// Si de los puntos del Eje Central P2 tiene mayor 'X' y mayor 'Y' que P2,
				// ---> entonces tomar el Punto del eje Radial con mayor 'X'.
				if (P1.cX > P2.cX && P1.cY > P2.cY) pivote = (P3.cX > P4.cX) ? P3 : P4;
      }

			if (inverseFocus) pivote = (is2GraphObj.isEqualPoint(pivote, P3)) ? P4 : P3;

			// Paso 5: Se calculan datos dimensionales de las construcciones auxiliares
      // a) Angulo del Eje Radial
			angEjeRadial = is2GraphObj.PointPointAngle(midP1_2, pivote);

      // b) La Hipotenuza y Cateto-Largo del Triangulo rectangulo en 'midP'			
      hipo  = (dist / 2.0) / Math.Sin(angleEx.ToRadian / 2.0);
      cateL = Math.Sqrt(Math.Pow(hipo, 2) - Math.Pow((dist / 2.0), 2));			      

			// Paso 6: Se calcula el Punto Centro del arco a construir.
			pC = is2GraphObj.PolarPoint(midP1_2, angEjeRadial, cateL);

			// a) Calculo los valores en radianes de Inicio Y Fin del arco
			//    usando para el calculo el Angulo opuesto al angulo del eje 
			//    radial.		
			s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(pC, P1));
			e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(pC, P2));

			if (inverseFocus) is2GraphObj.SwapValue(ref s_Ang, ref e_Ang);

      // Paso 7 y Final: Seteo las propiedades heredadas de la clase padre 'Arc'.
			Center     = is2GraphTranslator.ToAcad_3d(pC);
      StartAngle = s_Ang;
      EndAngle   = e_Ang;
      Normal     = normal;
      Radius     = hipo;
			Thickness  = 0.0;
		}

    //====================================================================//
    // Variante 6: Crea un Arco usando 2 puntos y una direccion 
		// [Start, Center, Direccion]
    //====================================================================//
		public ArcEx (StartPointEx start, CenterPointEx center, Vector2d direction, Vector3d normal, bool inverseFocus = false)
    {

    }

    //====================================================================//
    // Variante 7: Crea un Arco usando 2 puntos y radio [Start, Center, ArcRadius]
    //====================================================================//
		public ArcEx (StartPointEx start, EndPointEx end, RadiusEx radiusEx, Vector3d normal, bool inverseFocus = false)
		{
			CircleType C1, C2;
			ArcDirection direction;
			PointType pC, P1, P2, midP1_2, P3, P4, pivote = null;
			double dist, hipo, cateL, angEjeRadial, s_Ang, e_Ang;

			direction = GetArcDirection_2P(start.val, end.val);

			// Paso 1: Determinar distancia entre "Start" y "End" y el punto medio entre ambos.
			P1 = is2GraphTranslator.Tois2Graph(start.val);
			P2 = is2GraphTranslator.Tois2Graph(end.val);

			dist = is2GraphObj.PointPointDistance(P1, P2);
			midP1_2 = is2GraphObj.MidPointBetweenPoint(P1, P2);

			// Se lanza una exception si el arco es invalido.
			if (is2GraphObj.MenorEstricto(radiusEx.val, dist / 2.0))
				throw new ArcException("ArcEx Class: No se puede obtener un arco con el valor de radio dado.");

			// Paso 2: Se determinan 2 puntos (P3 y P4) en el EJE RADIAL, creando dos circunferencias
			//          auxiliares con centro en 'P2' y 'P2', respectivamente y radio 'radio'.  
			C1 = new CircleType(P1, dist);
			C2 = new CircleType(P2, dist);
			is2GraphObj.CircleCircleIntercept(C1, C2, out P3, out P4);

			// Paso 3: Una vez obtenido los dos puntos del eje Radial. Se determina 
			//         cual de los dos puntos se usara para traza la linea sobre 
			//         la que se halla el CENTRO del arco. 
			//         Esto se halla usando la siguiente Estrategia.
			if (direction == ArcDirection.AntiHorario)
			{
				// Si los puntos del eje radial (P3, P4) tienen la misma 'X', y de los puntos  
				// de entrada P2 tiene mayor 'X' que P2 ---> entonces tomar el Punto del eje
				// Radial de menor 'Y'.

				if (is2GraphObj.isEqualValues(P3.cX, P4.cX) && P1.cX > P2.cX) pivote = (P3.cY < P4.cY) ? P3 : P4;
				// Si los puntos del eje radial (P3, P4) tienen la misma 'X', y de los puntos 
				// de entrada P2 tiene menor 'X' que P2 ---> entonces tomar el Punto del eje
				// Radial con mayor 'Y'.
				if (is2GraphObj.isEqualValues(P3.cX, P4.cX) && P1.cX < P2.cX) pivote = (P3.cY > P4.cY) ? P3 : P4;
				// Si los puntos del eje radial (P3, P4) tienen la misma 'Y', y de los puntos 
				// de entrada P2 tiene menor 'Y' que P2 ---> entonces tomar el Punto del eje
				// Radial con menor 'X'.
				if (is2GraphObj.isEqualValues(P3.cY, P4.cY) && P1.cY < P2.cY) pivote = (P3.cX < P4.cX) ? P3 : P4;
				// Si los puntos del eje radial (P3, P4) tienen la misma 'Y', y de los puntos 
				// de entrada P2 tiene mayor 'Y' que P2 ---> entonces tomar el Punto del eje
				// Radial con mayor 'X'.
				if (is2GraphObj.isEqualValues(P3.cY, P4.cY) && P1.cY > P2.cY) pivote = (P3.cX > P4.cX) ? P3 : P4;

				// Si los puntos de Eje Radial (P3 y P4) son distintos tanto en la 'X' 
				// como en la 'Y', se usan entonces para el analisis los puntos del Eje
				// Central. Por lo tanto:
				// Si de los puntos del Eje Central P2 tiene mayor 'X' y menor 'Y' que P2,
				// ---> entonces tomar el Punto del eje Radial con menor 'X'.
				if (P1.cX > P2.cX && P1.cY < P2.cY) pivote = (P3.cX < P4.cX) ? P3 : P4;
				// Si de los puntos del Eje Central P2 tiene mayor 'X' y mayor 'Y' que P2,
				// ---> entonces tomar el Punto del eje Radial con mayor 'X'.
				if (P1.cX > P2.cX && P1.cY > P2.cY) pivote = (P3.cX > P4.cX) ? P3 : P4;
				// Si de los puntos del Eje Central P2 tiene menor 'X' y mayor 'Y' que P2,
				// ---> entonces tomar el Punto del eje Radial con mayor 'X'.
				if (P1.cX < P2.cX && P1.cY > P2.cY) pivote = (P3.cX > P4.cX) ? P3 : P4;
				// Si de los puntos del Eje Central P2 tiene menor 'X' y menor 'Y' que P2,
				// ---> entonces tomar el Punto del eje Radial con menor 'X'.
				if (P1.cX < P2.cX && P1.cY < P2.cY) pivote = (P3.cX < P4.cX) ? P3 : P4;
			}
			else
			{
				// Si los puntos del eje radial (P3, P4) tienen la misma 'X', y de los puntos  
				// de entrada P2 tiene menor 'X' que P2 ---> entonces tomar el Punto del eje
				// Radial de mayor 'Y'.
				if (is2GraphObj.isEqualValues(P3.cX, P4.cX) && P1.cX < P2.cX) pivote = (P3.cY > P4.cY) ? P3 : P4;
				// Si los puntos del eje radial (P3, P4) tienen la misma 'X', y de los puntos 
				// de entrada P2 tiene mayor 'X' que P2 ---> entonces tomar el Punto del eje
				// Radial con menor 'Y'.
				if (is2GraphObj.isEqualValues(P3.cX, P4.cX) && P1.cX > P2.cX) pivote = (P3.cY < P4.cY) ? P3 : P4;
				// Si los puntos del eje radial (P3, P4) tienen la misma 'Y', y de los puntos 
				// de entrada P2 tiene mayor 'Y' que P2 ---> entonces tomar el Punto del eje
				// Radial con mayor 'X'.
				if (is2GraphObj.isEqualValues(P3.cY, P4.cY) && P1.cY > P2.cY) pivote = (P3.cX > P4.cX) ? P3 : P4;
				// Si los puntos del eje radial (P3, P4) tienen la misma 'Y', y de los puntos
				// de entrada P2 tiene menor 'Y' que P2 ---> entonces tomar el Punto del eje
				// Radial con menor 'X'.
				if (is2GraphObj.isEqualValues(P3.cY, P4.cY) && P1.cY < P2.cY) pivote = (P3.cX < P4.cX) ? P3 : P4;

				// Si los puntos de Eje Radial (P3 y P4) son distintos tanto en la 'X' 
				// como en la 'Y', se usan entonces para el analisis los puntos del Eje
				// Central. Por lo tanto:
				// Si de los puntos del Eje Central P2 tiene menor 'X' y mayor 'Y' que P2,				
				// ---> entonces tomar el Punto del eje Radial con mayor 'X'.
				if (P1.cX < P2.cX && P1.cY > P2.cY) pivote = (P3.cX > P4.cX) ? P3 : P4;
				// Si de los puntos del Eje Central P2 tiene menor 'X' y menor 'Y' que P2,
				// ---> entonces tomar el Punto del eje Radial con menor 'X'.
				if (P1.cX < P2.cX && P1.cY < P2.cY) pivote = (P3.cX < P4.cX) ? P3 : P4;
				// Si de los puntos del Eje Central P2 tiene mayor 'X' y menor 'Y' que P2,
				// ---> entonces tomar el Punto del eje Radial con menor 'X'.
				if (P1.cX > P2.cX && P1.cY < P2.cY) pivote = (P3.cX < P4.cX) ? P3 : P4;
				// Si de los puntos del Eje Central P2 tiene mayor 'X' y mayor 'Y' que P2,
				// ---> entonces tomar el Punto del eje Radial con mayor 'X'.
				if (P1.cX > P2.cX && P1.cY > P2.cY) pivote = (P3.cX > P4.cX) ? P3 : P4;
			}

			if (inverseFocus) pivote = (is2GraphObj.isEqualPoint(pivote, P3)) ? P4 : P3;
				
			// Paso 4: Se calculan datos dimensionales de las construcciones auxiliares
			// a) Angulo del Eje Radial.
			angEjeRadial = is2GraphObj.PointPointAngle(midP1_2, pivote);

			// b) La Hipotenuza y Cateto-Largo del Triangulo rectangulo en 'midP'.
			hipo  = radiusEx.val;
			cateL = Math.Sqrt(Math.Pow(hipo, 2) - Math.Pow((dist / 2.0), 2));

			// Paso 5: Se calcula el Punto Centro del arco a construir.
			pC = is2GraphObj.PolarPoint(midP1_2, angEjeRadial, cateL);

			// a) Calculo los valores en radianes de Inicio Y Fin del arco
			//    usando para el calculo el Angulo opuesto al angulo del eje 
			//    radial.		
			s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(pC, P1));
			e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(pC, P2));

			// Paso 6 y Final: Seteo las propiedades heredadas de la clase padre 'Arc'
			Center = is2GraphTranslator.ToAcad_3d(pC);
			StartAngle = s_Ang;
			EndAngle = e_Ang;
			Normal = normal;
			Radius = hipo;
			Thickness = 0.0;
		}

    //====================================================================//
    // Variante 8: Crea un Arco usando 3 puntos [Center, Start, End] 
    //====================================================================//
		public ArcEx (CenterPointEx center, StartPointEx start, EndPointEx end, Vector3d normal, bool inverseFocus = false)
    {
			PointType P1, P2, pC;
			double hipo, s_Ang, e_Ang;
			 
			P1 = is2GraphTranslator.Tois2Graph(start.val);
			P2 = is2GraphTranslator.Tois2Graph(end.val);
			pC = is2GraphTranslator.Tois2Graph(center.val);

			// Paso 1: Determinar distancia entre "Start" y "Center".
			hipo = is2GraphObj.PointPointDistance(pC, P1);

			// b) Se calculan los valores en radianes de Inicio Y Fin del arco.
			s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(pC, P1));
			e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(pC, P2));

			if (inverseFocus) is2GraphObj.SwapValue(ref s_Ang, ref e_Ang);

			// Paso 2 y Final: Seteo las propiedades heredadas de la clase padre 'Arc'.
			Center = center.val;
			StartAngle = s_Ang;
			EndAngle = e_Ang;
			Normal = normal;
			Radius = hipo;
			Thickness = 0.0;
    }

    //====================================================================//
    // Variante 9:  Crea un Arco usando 2 puntos y un angulo 
		// [Center, Start, angleAgudo]
    //====================================================================//
		public ArcEx (CenterPointEx center, StartPointEx start, GradeAngleEx angleEx, Vector3d normal, bool inverseFocus = false)
    {
			PointType pC, P1, P2;
			double hipo, s_Ang, e_Ang;

			// Paso 1: Normalizo el valor del angulo y compruebo su signo.
			angleEx.val = is2GraphObj.NormalizeAngle(angleEx.val);

			if (is2GraphObj.isNegative(angleEx.val) && inverseFocus == false)
			{
				inverseFocus = true;
			}
			else if (inverseFocus && is2GraphObj.isPositive(angleEx.val))
			{
				angleEx.val *= -1;
			}
			else
			{
				inverseFocus = false;
				angleEx.val = Math.Abs(angleEx.val);
			}

			// Paso 2: Calculo la posicion del punto "End" a partir de los datos conocidos  
			//				 de los puntos "Center" y "Start",  y el angulo especificado. 
			P1 = is2GraphTranslator.Tois2Graph(start.val);
			pC = is2GraphTranslator.Tois2Graph(center.val);
			P2 = is2GraphObj.RotatePoint(P1, angleEx.val, pC); 

			// Paso 3: Se calculo el radio del arco.
			hipo = is2GraphObj.PointPointDistance(pC, P1);

			// Paso 4: Se calculan los valores en radianes de Inicio Y Fin del arco.		
			s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(pC, P1));
			e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(pC, P2));

			if (inverseFocus) is2GraphObj.SwapValue(ref s_Ang, ref e_Ang);

			// Paso 5 y Final: Seteo las propiedades heredadas de la clase padre 'Arc'.
			Center = is2GraphTranslator.ToAcad_3d(pC);
			StartAngle = s_Ang;
			EndAngle = e_Ang;
			Normal = normal;
			Radius = hipo;
			Thickness = 0.0;
    }

		//====================================================================//
		// Variante 10: Crea un Arco usando 2 puntos y una longitud 
		// [Center, Start, Length]
		//====================================================================//
		public ArcEx (CenterPointEx center, StartPointEx start, DistanceEx length, Vector3d normal, bool inverseFocus = false)
		{
			PointType pC, P1, P2;
			double hipo, angle, s_Ang, e_Ang;

			// Paso 1: Se calcula el valor de angulo que se forma para el valor de distancia dado.
			P1 = is2GraphTranslator.Tois2Graph(start.val);
			pC = is2GraphTranslator.Tois2Graph(center.val);

			hipo = is2GraphObj.PointPointDistance(pC, P1);
			angle = is2GraphObj.RadToGrad(Math.Asin((length.val / 2.0) / hipo)) * 2.0;
			if (inverseFocus) angle *= -1;

			// Paso 2: Calculo la posicion del punto "End" a partir de los datos conocidos  
			//				 de los puntos "Center" y "Start",  y el angulo calculado. 
			P2 = is2GraphObj.RotatePoint(P1, angle, pC);

			// Paso 3: Se calculan los valores en radianes de Inicio Y Fin del arco.		
			s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(pC, P1));
			e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(pC, P2));

			if (inverseFocus) is2GraphObj.SwapValue(ref s_Ang, ref e_Ang);

			// Paso 4 y Final: Seteo las propiedades heredadas de la clase padre 'Arc'.
			Center = is2GraphTranslator.ToAcad_3d(pC);
			StartAngle = s_Ang;
			EndAngle = e_Ang;
			Normal = normal;
			Radius = hipo;
			Thickness = 0.0;
		}

		//====================================================================//
		// Variante 11-A: Crea un Arco continuo usando el punto final de una 
		// linea como punto de inicio del arco y en punto final cualquiera.
		// [Start_continuos from Line]
		//====================================================================//
		public ArcEx (StartPointEx start, EndPointEx end, Line L, Vector3d normal, bool inverseFocus = false)
    {

    }

		//====================================================================//
		// Variante 11-B: Crea un Arco continuo usando el punto final de un 
		// arco anterior como punto de inicio del nuevo arco y un punto final 
		// cualquiera.
		// [Start_continuos from Arc]
		//====================================================================//
		public ArcEx(StartPointEx start, EndPointEx end, ArcEx Arc, Vector3d normal, bool inverseFocus = false)
		{

		}

		//====================================================================//
		// Duevuelve la orientacion del arco en {HORARIO / ANTIHORARIO} 
		// analizado la disposicion de sus puntos extremos (Start (P1) y End (P2)).
		//====================================================================//
		private static ArcDirection GetArcDirection_2P (Point3d P1, Point3d P2)
		{
			ArcDirection dir;

			if (// Si los puntos P1, P2 tienen la misma 'Y' y P1 tiene mayor 'X' que P2.
					(is2GraphObj.isEqualValues(P1.Y, P2.Y) && P1.X > P2.X) || 
					// Si los puntos P1, P2 tienen la misma 'Y' y P1 tiene menor 'X' que P2.
					(is2GraphObj.isEqualValues(P1.Y, P2.Y) && P1.X < P2.X) ||
					// Si los puntos P1, P2 tienen la misma 'X' y P1 tiene menor 'Y' que P2.
					(is2GraphObj.isEqualValues(P1.X, P2.X) && P1.Y < P2.Y) ||
					// Si los puntos P1, P2 tienen la misma 'X' y P1 tiene mayor 'Y' que P2.
					(is2GraphObj.isEqualValues(P1.X, P2.X) &&  P1.Y > P2.Y) ||
					// Si P1 tiene mayor 'X' y menor 'Y' que P2.
					(P1.X > P2.X && P1.Y < P2.Y) ||
					// Si P1 tiene mayor 'X' y mayor 'Y' que P2.
					(P1.X > P2.X && P1.Y > P2.Y) ||
					// Si P1 tiene menor 'X' y mayor 'Y' que P2.
					(P1.X < P2.X && P1.Y > P2.Y) ||
					// Si P1 tiene menor 'X' y menor 'Y' que P2.
					(P1.X < P2.X && P1.Y < P2.Y))
			{
				dir = ArcDirection.AntiHorario;
			}
			else
			{
				dir = ArcDirection.Horario;
			}

			return dir;
		}

		//====================================================================//
		// Duevuelve la orientacion del arco en {HORARIO / ANTIHORARIO} 
		// analizado la disposicion de 3 de sus puntos extremos (Start (P1), 
		// End (P2) y Any (Px)).
		//====================================================================//
		private static ArcDirection GetArcDirection_3P (Point3d P1, Point3d Px, Point3d P2)
		{
			ArcDirection dir;
			PointType p1, p2, px;
			double ang1, angOp, angX;

			// Calculo el angulo que forman P1-P2 y P1-Px, ademas del angulo opuesto de este ultimo.
			p1 = is2GraphTranslator.Tois2Graph(P1);
			p2 = is2GraphTranslator.Tois2Graph(P2);
			px = is2GraphTranslator.Tois2Graph(Px);

			ang1 = is2GraphObj.PointPointAngle(p1, p2);
			angX = is2GraphObj.PointPointAngle(p1, px);
			angOp = is2GraphObj.OppositeAngle(ang1);		

			if (// Si los puntos P1, P2 tienen la misma 'Y' ademas P1 tiene mayor 'X' que P2 y
					// el punto arbitrario Px tiene mayor-o-igual 'Y' que P1 y P2.
					(is2GraphObj.isEqualValues(P1.Y, P2.Y) && P1.X > P2.X && Px.Y >= P1.Y) ||
					// Si los puntos P1, P2 tienen la misma 'Y' ademas P1 tiene menor 'X' que P2 y
					// el punto arbitrario Px tiene menor-o-igual 'Y' que P1 y P2.
					(is2GraphObj.isEqualValues(P1.Y, P2.Y) && P1.X < P2.X  && Px.Y <= P1.Y) ||
					// Si los puntos P1, P2 tienen la misma 'X' ademas P1 tiene menor 'Y' que P2 y
					// el punto arbitrario Px tiene mayor-o-igual 'X' que P1 y P2.
					(is2GraphObj.isEqualValues(P1.X, P2.X) && P1.Y < P2.Y && Px.X >= P1.X) ||
					// Si los puntos P1, P2 tienen la misma 'X' ademas P1 tiene mayor 'Y' que P2 y
					// el punto arbitrario Px tiene menor-o-igual 'X' que P1 y P2.
					(is2GraphObj.isEqualValues(P1.X, P2.X) && P1.Y > P2.Y && Px.X <= P1.X) ||
					// Si P1 tiene mayor 'X' y menor 'Y' que P2, ademas el angulo que se forma entre
 					// P1-Px es menor-o-igual que el que se forma entre P1-P2 o mayor-o-igual que su
					// opuesto.
					(P1.X > P2.X && P1.Y < P2.Y && (is2GraphObj.MenorOrEqual(angX, ang1) || is2GraphObj.MayorOrEqual(angX, angOp))) ||
					// Si P1 tiene mayor 'X' y mayor 'Y' que P2, ademas el angulo que se forma entre
					// P1-Px es menor-o-igual que el que se forma entre P1-P2 o mayor-o-igual que su
					// opuesto.
					(P1.X > P2.X && P1.Y > P2.Y && (is2GraphObj.MayorOrEqual(angX, angOp) && is2GraphObj.MenorOrEqual(angX, ang1))) ||
					// Si P1 tiene menor 'X' y mayor 'Y' que P2, ademas el angulo que se forma entre
					// P1-Px es menor-o-igual que el que se forma entre P1-P2 o mayor-o-igual que su
					// opuesto.
					(P1.X < P2.X && P1.Y > P2.Y && (is2GraphObj.MayorOrEqual(angX, angOp) && is2GraphObj.MenorOrEqual(angX, ang1))) ||
					// Si P1 tiene menor 'X' y menor 'Y' que P2, ademas el angulo que se forma entre
					// P1-Px es menor-o-igual que el que se forma entre P1-P2 o mayor-o-igual que su
					// opuesto.
					(P1.X < P2.X && P1.Y < P2.Y && (is2GraphObj.MenorOrEqual(angX, ang1) || is2GraphObj.MayorOrEqual(angX, angOp))))
			{
				dir = ArcDirection.AntiHorario;
			}
			else
			{
				dir = ArcDirection.Horario;
			}

			return dir;
		}
	}

	//=============================================================================================//
	// ClassName  : StartPointEx
	//
	// Description: .
	//
	// Revision   : 
	//=============================================================================================//
	public class StartPointEx
	{
		public Point3d val { set; get; }

		public StartPointEx (double x, double y, double z)
		{
			val = new Point3d(x, y, z);
		}

		public StartPointEx (Point2d p)
		{
			val = is2GraphTranslator.Acad_2dTo3d(p);
		}

		public StartPointEx (Point3d p)
		{
			val = p;
		}
	}

	//=============================================================================================//
	// ClassName  : AnyPointEx
	//
	// Description: .
	//
	// Revision   : 
	//=============================================================================================//
	public class AnyPointEx
	{
		public Point3d val { set; get; }

		public AnyPointEx (double x, double y, double z)
		{
			val = new Point3d(x, y, z);
		}

		public AnyPointEx (Point2d p)
		{
			val = is2GraphTranslator.Acad_2dTo3d(p);
		}

		public AnyPointEx (Point3d p)
		{
			val = p;
		}
	}

	//=============================================================================================//
	// ClassName  : EndPointEx
	//
	// Description: .
	//
	// Revision   : 
	//=============================================================================================//
	public class EndPointEx
	{
		public Point3d val { set; get; }

		public EndPointEx (double x, double y, double z)
		{
			val = new Point3d(x, y, z);
		}

		public EndPointEx (Point2d p)
		{
			val = is2GraphTranslator.Acad_2dTo3d(p);
		}

		public EndPointEx (Point3d p)
		{
			val = p;
		}
	}

	//=============================================================================================//
	// ClassName  : CenterPointEx
	//
	// Description: .
	//
	// Revision   : 
	//=============================================================================================//
	public class CenterPointEx
	{
		public Point3d val { set; get; }

		public CenterPointEx (double x, double y, double z)
		{
			val = new Point3d(x, y, z);
		}

		public CenterPointEx (Point2d p)
		{
			val = is2GraphTranslator.Acad_2dTo3d(p);
		}

		public CenterPointEx (Point3d p)
		{
			val = p;
		}
	}

	//=============================================================================================//
	// ClassName  : GradeAngleEx
	//
	// Description: .
	//
	// Revision   : 
	//=============================================================================================//
	public class GradeAngleEx
	{
		public double val { set; get; }

		public GradeAngleEx (double a)
		{
			val = a;
		}

		public double ToRadian
		{
			get
			{
				return is2GraphObj.GradToRad(val);
			}
		}
	}

	//=============================================================================================//
	// ClassName  : DistanceEx
	//
	// Description: .
	//
	// Revision   : 
	//=============================================================================================//
	public class DistanceEx
	{
		public double val { set; get; }

		public DistanceEx (double l)
		{
			val = l;
		}
	}

	//=============================================================================================//
	// ClassName  : LongitudeEx
	//
	// Description: .
	//
	// Revision   : 
	//=============================================================================================//
	public class LongitudeEx
	{
		public double val { set; get; }

		public LongitudeEx (double l)
		{
			val = l;
		}
	}

	//=============================================================================================//
	// ClassName  : RadiusEx
	//
	// Description: .
	//
	// Revision   : 
	//=============================================================================================//
	public class RadiusEx
	{
		public double val { set; get; }

		public RadiusEx (double r)
		{
			val = r;
		}
	}

	//=============================================================================================//
	// ClassName  : ArcException
  //
	// Description: Representa una exception de tipo Arc Error.
  //
  // Revision   : 30-06-2017
  //=============================================================================================//
	public class ArcException : Exception
	{
		public ArcException()		
		{
		}

		public ArcException (string msg): base(msg)
		{
		}
	}
}


