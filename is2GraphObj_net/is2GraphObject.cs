using System;
using System.Collections.Generic;

namespace is2GraphObject
{
	#region - Enumerations -
	/// <summary>
	///  Define los 3 planos que se forman por la intersección de los ejes de coordendas X-Y, Y-Z y X-Z. 
	/// </summary>
	public enum Plane
	{
		/// <summary>Representa el plano XY</summary>
		XY,
		/// <summary>Representa el plano YZ</summary>
		YZ,
		/// <summary>Representa el plano XZ</summary>
		XZ
	}

	/// <summary>
	/// Describe los tipos de relaciones relativas que ocurren entre dos circunferencias. 
	/// </summary>
	public enum CircleCircleRelation
	{
		/// <summary>Indica que dos circunferencias son iguales.</summary>
		Equal,
		/// <summary>Indica que dos circunferencias son concéntricas.</summary>
		Concentric,
		/// <summary>Indica que una de las circunferencias en interior a la otra.</summary>
		Interior,
		/// <summary>Indica que dos circunferencias son exteriores.</summary>
		Exterior,
		/// <summary>Indica que una circunferencia es tangente interior a otra circunferencia.</summary>
		Tangent_In,
		/// <summary>Indica que una circunferencia es tangente exterior a otra circunferencia.</summary>
		Tangent_Out,
		/// <summary>Indica que dos circunferencias son secantes.</summary>
		Secant
	}

	/// <summary>
	/// Describe los tipos de relaciones que ocurren entre una circunferencia y en segmento de recta. 
	/// </summary>
	public enum CircleSegmentRelation
	{
		/// <summary>Indica que el segmento es exterior a la circunferencia.</summary>
		Exterior,
		/// <summary>Indica que el segmento es tangente a la circunferencia.</summary>
		Tangent,
		/// <summary>Indica que el segmento es Secante a la circunferencia.</summary>
		Secant,
		/// <summary>Indica que el segmento intercepta a la circunferencia en un solo punto pero sin ser tangente.</summary>
		SimpleAcross
	}

	/// <summary>
	/// 
	/// </summary>
	public enum PointLinePosition
	{
		/// <summary> Indica que el punto pertenece a la línea </summary>
		Member = 0,
		/// <summary> Indica que el punto está sobre la línea. </summary>
		Up = 1,
		/// <summary> Indica que el punto está a la izquirda de la línea. </summary>
		Left = 2,
		/// <summary> Indica que el punto está debajo de la línea. </summary>
		Bottom = 4,
		/// <summary> Indica que el punto está a la derecha de la línea. </summary>
		Right = 8
	}

	/// <summary>
	/// Define cada una de las cuatro porciones en la que descompone un sistema de 2 planos interceptados
	/// en el espacio (Frontal, Horizontal).
	/// </summary>
	public enum Cuadrante
	{
		/// <summary>Representa el 1er cuadrante [+X +Y +Z] para un sistema de 2 planos.</summary>
		I = 1,
		/// <summary>Representa el 2do cuadrante [+X -Y +Z] para un sistema de 2 planos.</summary>
		II,
		/// <summary>Representa el 3er cuadrante [+X -Y -Z] para un sistema de 2 planos.</summary>
		III,
		/// <summary>Representa el 4to cuadrante [+X +Y -Z] para un sistema de 2 planos.</summary>
		IV
	}

	/// <summary>
	/// Define cada una de las ochos porciones en la que descompone un sistema de 3 planos interceptados
	/// en el espacio (Frontal, Horizontal, Lateral).
	/// </summary>
	public enum Octante
	{
		/// <summary>Representa el 1er octante [+X +Y +Z] para un sistema de 3 planos.</summary>
		I = 1,
		/// <summary>Representa el 2do octante [+X -Y +Z] para un sistema de 3 planos.</summary>
		II,
		/// <summary>Representa el 3ro octante [+X -Y -Z] para un sistema de 3 planos.</summary>
		III,
		/// <summary>Representa el 4to octante [+X +Y -Z] para un sistema de 3 planos.</summary>
		IV,
		/// <summary>Representa el 5to octante [-X +Y +Z] para un sistema de 3 planos.</summary>
		V,
		/// <summary>Representa el 6to octante [-X -Y +Z] para un sistema de 3 planos.</summary>
		VI,
		/// <summary>Representa el 7mo octante [-X -Y -Z] para un sistema de 3 planos.</summary>
		VII,
		/// <summary>Representa el 8vo octante [-X +Y -Z] para un sistema de 3 planos.</summary>
		VIII
	}

	/// <summary>
	/// 
	/// </summary>
	public enum PolylineElementType
	{
		/// <summary>
		/// 
		/// </summary>
		Point,
		/// <summary>
		/// 
		/// </summary>
		Segment,
		/// <summary>
		/// 
		/// </summary>
		Arc
	}
	#endregion

	#region - 2D Entities -
		#region - PointType -
		/// <summary>
		///  Representa un tipo Punto que puede ser definido tanto en el plano como el espacio. 
		/// </summary>
		public class PointType
		{
			/// <summary>
			/// Representa la coordenada X del punto. 
			/// </summary>
			public double cX;

			/// <summary>
			/// Representa la coordenada Y del punto. 
			/// </summary>
			public double cY;

			/// <summary>
			/// Representa la coordenada Z del punto. 
			/// </summary>
			public double cZ;

			/// <summary>
			/// Constructor por defecto. 
			/// </summary>
			public PointType()
			{
				cX = 0.0;
				cY = 0.0;
				cZ = 0.0;
			}

			/// <summary>
			/// Contructor que toma 2 parámetros, [x, y]. 
			/// </summary>
			/// <param name="x">
			/// Coordenada X del punto.
			/// </param>
			/// <param name="y">
			/// Coordenada Y del punto.
			/// </param>
			public PointType (double x, double y)
			{
				cX = x;
				cY = y;
				cZ = 0.0;
			}

			/// <summary>
			/// Contructor que toma 3 parámetros, [x, y, z]. 
			/// </summary>
			/// <param name="x">
			/// Coordenada X del punto.
			/// </param>
			/// <param name="y">
			/// Coordenada Y del punto.
			/// </param>
			/// <param name="z">
			/// Coordenada Z del punto.
			/// </param>
			public PointType (double x, double y, double z)
			{
				cX = x;
				cY = y;
				cZ = z;
			}
		}
		#endregion

		#region - PolarPointType - 
		/// <summary>
		/// Representa un tipo Punto Polar el cual es definido mediante coordenadas polares.
		/// </summary>
		public class PolarPointType
		{
			private PointType P, pRef;

			/// <summary>
			/// Representa la componente angular del punto polar.
			/// </summary>
			public double Angle;

			/// <summary>
			/// Representa la componente de longitud del punto polar.
			/// </summary>
			public double Radius;
		
			/// <summary>
			/// Constructor por defecto. 
			/// </summary>
			public PolarPointType()
			{
				Angle = 0.0;
				Radius = 0.0;
			}

			/// <summary>
			/// Constructor que toma 3 parámetro, [punto base, ángulo y radio].  
			/// </summary>
			/// <param name="pbase">
			/// Punto base a partir del cuál se calcula el punto polar.
			/// </param>
			/// <param name="angle">
			/// Componente angular del punto polar.
			/// </param>
			/// <param name="radius">
			/// Componente de longitud del punto polar.
			/// </param>
			/// <remarks>
			/// El valor del radio se considera siempre positivo, por lo que establecer un valor de radio negativo
			/// no tiene ninguna influencia en el punto polar calculado.
			/// </remarks>
			public PolarPointType (PointType pbase, double angle, double radius)
			{
				Angle  = angle;
				Radius = Math.Abs(radius);
				pRef  = pbase;
				P = is2GraphObj.PolarPoint(pRef, angle, radius);
			}

			/// <summary>
			/// Propiedad de solo lectura. Devuelve la coordenada X del punto polar. 
			/// </summary>
			public double cX
			{
				get { return P.cX; }
			}

			/// <summary>
			/// Propiedad de solo lectura. Devuelve la coordenada Y del punto polar. 
			/// </summary>
			public double cY
			{
				get { return P.cY; }
			}

			/// <summary>
			/// Propiedad de solo lectura. Devuelve la coordenada Z del punto. 
			/// </summary>
			public double cZ
			{
				get { return P.cZ; }
			}

			/// <summary>
			/// Propiedad de solo lectura. Devuelve el punto de referencia que se uso como Centro 
			/// u Origen para calcular el punto polar.  
			/// </summary>>
			public PointType ReferencePoint
			{
				get { return pRef; }
			}
		}
			#endregion

		#region - CilindricalPointType -
		/// <summary>
		/// 
		/// </summary>
		public class CilindricalPointType
		{
		}
			#endregion

		#region - SphericalPointType - 
		/// <summary>
		/// 
		/// </summary>
		public class SphericalPointType
		{
		}
		#endregion

		#region - LineType -
		/// <summary>
		/// Representa un tipo Línea. Esta línea se define por un punto y un ángulo, 
		///	dando como resultado una línea que pasa por el punto 'P', forma un angulo
		/// con el eje de las abcisas determinado por 'Angle' y tiene longitud infinita.
		/// </summary>
		public class LineType
		{
			private double angle;

			/// <summary>
			/// Representa el punto por donde pasa la línea.
			/// </summary>
			public PointType P;

			/// <summary>
			/// Representa el ángulo de inclinación de la línea con respecto al eje de las abcisas (Eje X).
			/// </summary>
			public double Angle
			{
				get { return angle; }
				set { angle = Math.Abs(value); }
			}

			/// <summary>
			/// Constructor por defecto. 
			/// </summary>
			public LineType()
			{
				P = new PointType();
				angle = 0;
			}

			/// <summary>
			/// Contructor que toma por parámetro un punto y un ángulo. 
			/// </summary>
			/// <param name="p">
			/// Punto por donde pasa la línea.
			/// </param>
			/// <param name="angle">
			/// Ángulo de inclinación de la línea con respecto al eje de las abcisas (Eje X).
			/// </param>
			public LineType (PointType p, double angle)
			{
				P = p;
				Angle = is2GraphObj.NormalizeAngle(angle);
			}
		}
		#endregion

		#region - CircleType -
		/// <summary>
		/// Representa un tipo Circunferencia.
		/// </summary>
		public class CircleType
		{
			/// <summary>
			/// Define la posicion relativa que ocupa la circunferencia con respecto aun polígono.
			/// </summary>
			public enum Type
			{
				/// <summary>Indica que la circunferencia es circunscripta a un polígono.</summary>
				Circunscripta = 1,
				/// <summary>Indica que la circunferencia es inscripta a un polígono.</summary>
				Inscripta
			}

			private double radius;

			/// <summary>
			/// Represente el punto centro de la circunferencia.
			/// </summary>
			public PointType Center;

			/// <summary>
			/// Representa el radio de la circunferencia.<br/>
			/// <b>Nota:</b> El valor del radio se considera siempre positivo, por lo que establecer 
			/// un valor de radio negativo no tiene ninguna influencia.
			/// </summary>
			public double Radius
			{
				get { return radius; }
				set { radius = Math.Abs(value); }
			}

			/// <summary>
			/// Constructor por defecto.
			/// </summary>
			public CircleType()
			{
				Center = new PointType();
				Radius = 0;
			}

			/// <summary>
			/// Constructor que toma como parámetros el punto centro y el radio de la circunferencia.
			/// </summary>
			/// <param name="P">
			/// Punto centro de la circunferencia.
			/// </param>
			/// <param name="radius">
			/// Radio de la circuferencia.
			/// </param>
			public CircleType (PointType P, double radius)
			{
				if (is2GraphObj.isEqualCero(radius)) throw new CircleException("No se puede obtener un circle con radio cero.");

				Center = P;
				this.radius = Math.Abs(radius);
			}

			/// <summary>
			/// Constructor que toma como parámetros 2 puntos que pertenecen a la mayor cuerda (diámetro) de una circunferecia.
			/// </summary>
			/// <param name="P1">
			/// Primer punto.
			/// </param>
			/// <param name="P2">
			/// Segundo punto.
			/// </param>
			public CircleType (PointType P1, PointType P2)
			{
				Center = is2GraphObj.MidPointBetweenPoint(P1, P2);
				Radius = is2GraphObj.PointPointDistance(Center, P1);
			}

			/// <summary>
			/// Constructor que toma como parámetros 3 puntos, y crea una circunferencia que pasa por estos 
			/// 3 puntos si el valor del parámetro "t" es -Circunscripta-. Por el contrario el valor de "t"
			/// es -Inscripta- los puntos dados sirven como vértices para el cálculo del incentro del triángulo 
			/// imaginario que estos forman, con lo que se obtiene una circunferencia inscripta.
			/// </summary>
			/// <param name="P1">
			/// Primer punto.
			/// </param>
			/// <param name="P2">
			/// Segundo punto.
			/// </param>
			/// <param name="P3">
			/// Tercer punto.
			/// </param>
			/// <param name="t">
			/// Indica de que forma respecto a P1, P2, P3 se crea la circunferencia. La cuál puede ser
			/// "inscripta" al triangulo imaginario que forman estos 3 puntos o "circunscripta" a este.  
			/// </param>
			public CircleType (PointType P1, PointType P2, PointType P3, Type t = Type.Circunscripta)
			{
				double r;
				PointType center;

				center = new PointType();

				// La circunferencia circunscripta se calcula a partir del <Circuncentro>.
				// Nota: el circuncentro es el punto en el que se intersecan las mediatrices de un triangulo.
				// La mediatriz de un lado de un triángulo es la recta perpendicular a dicho lado trazada por 
				// su punto medio. El circuncentro equidista de los tres vértices del triangulo.
				if (t == Type.Circunscripta)
				{
					LineType L1, L2;
					SegmentType S1, S2;

					S1 = new SegmentType(P1, P2);
					S2 = new SegmentType(P2, P3);

					L1 = is2GraphObj.PerperdicularLineAt(S1.ConvertToLine(), S1.MidPoint);
					L2 = is2GraphObj.PerperdicularLineAt(S2.ConvertToLine(), S2.MidPoint);

					is2GraphObj.LineLineIntersect(L1, L2, out center);

					r = is2GraphObj.PointPointDistance(center, P1);
				}
				// La circunferencia inscripta se calcula a partir del <Incentro>.
				// Nota: El Incentro es el punto en el que se intersecan las tres bisectrices de los ángulos 
				// interiores del triángulo, este punto equidista de los tres lados del triangulo, siendo 
				// tangente ademas a ellos.
				else
				{
					double a, b, c;

					// (a) - Lado opuesto a P1. (P2-P3)
					a = is2GraphObj.PointPointDistance(P2, P3);
					// (b) - Lado opuesto a P2. (P1-P3)
					b = is2GraphObj.PointPointDistance(P1, P3);
					// (c) - Lado opuesto a P3. (P1-P2)
					c = is2GraphObj.PointPointDistance(P1, P2);

					center.cX = (a * P1.cX + b * P2.cX + c * P3.cX) / (a + b + c);
					center.cY = (a * P1.cY + b * P2.cY + c * P3.cY) / (a + b + c);
					center.cZ = 0.0;

					r = is2GraphObj.PointLineDistance(center, new SegmentType(P1, P2).ConvertToLine());
				}

				Center = center;
				Radius = r;
			}

			/// <summary>
			/// Constructor que toma como parámetros 2 segmentos y un valor de radio, y XXX
			/// </summary>
			/// <param name="S1">
			/// Primer segmento.
			/// </param>
			/// <param name="S2">
			/// Segundo segmento.
			/// </param>
			/// <param name="radius">
			/// Equidistancia a ambos segmentos.<br/>
			/// <b>Nota:</b> El valor del radio se considera siempre positivo, por lo que establecer 
			/// un valor de radio negativo no tiene ninguna influencia.
			/// </param>
			/// <param name="s1_right_up">
			/// Determina hacia que lado del segmento "S1" se calcula el circle.<br/><br/>
			/// Nota: Si el valor es <b>true</b>, el circle se calcula hacia la derecha-o-arriba del segmento.
			/// Por el contrario si el valor es <b>false</b>, el circle se calcula hacia la izquierda-o-abajo del
			/// segmento. En ambos casos su aplicación es sobre el segmento "S1".
			/// </param>
			/// <param name="s2_right_up">
			/// Determina hacia que lado del segmento "S2" se calcula el circle.<br/><br/>
			/// Nota: Si el valor es <b>true</b>, el circle se calcula hacia la derecha-o-arriba del segmento.
			/// Por el contrario si el valor es <b>false</b>, el circle se calcula hacia la izquierda-o-abajo del 
			/// segmento. En ambos casos su aplicación es sobre el segmento "S2".
			/// </param>	
			public CircleType (SegmentType S1, SegmentType S2, double radius, bool s1_right_up = true, bool s2_right_up = true)
			{
				ArcType arc;

				if (is2GraphObj.isEqualCero(radius)) throw new CircleException("No se puede obtener un circle con radio cero.");

				arc = is2GraphObj.SegmentSegmentFillet(S1, S2, radius, s1_right_up, s2_right_up);

				if (arc == null) throw new CircleException("CircleType Exception: no se puede obtener un Circle entre dos rectas paralelas " +
				                                           "o colineales");

				Center = arc.Center;
				Radius = arc.Radius;
			}

			/// <summary>
			/// Constructor que toma como parámetros 3 segmentos y crea una circunferencia que es tangente
			/// al mismo tiempo a los 3 segmentos.
			/// </summary>
			/// <param name="S1">
			/// Primer segmento.
			/// </param>
			/// <param name="S2">
			/// Segundo segmento.
			/// </param>
			/// <param name="S3">
			/// Tercer segmento.
			/// </param>
			public CircleType (SegmentType S1, SegmentType S2, SegmentType S3)
			{

			}

			/// <summary>
			/// Propiedad de solo lectura. Devuelve el diámetro de la circunferencia.
			/// </summary>
			public double Diameter
			{
				get { return Radius * 2.0; }
			}

			/// <summary>
			/// Propiedad de solo lectura. Devuelve el perímetro de la circunferencia.
			/// </summary>
			public double Perimeter
			{
				get { return 2.0 * Math.PI * Radius; }
			}

			/// <summary>
			/// Propiedad de solo lectura. Devuelve el área de la círculo que define la circunferencia. 
			/// </summary>
			public double Area
			{
				get { return Math.PI * Math.Pow(Radius, 2.0); }
			}
		}
		#endregion

		#region - SegmentType -
		/// <summary>
		/// Representa un tipo Segmento. Un segmento se define por dos puntos.
		/// </summary>
		public class SegmentType
		{
			/// <summary>
			/// Representa el punto de inicio del segmento.
			/// </summary>
			public PointType StartPoint;

			/// <summary>
			/// Representa el punto de final del segmento.
			/// </summary>
			public PointType EndPoint;

			/// <summary>
			/// Constructor por defecto. 
			/// </summary>
			public SegmentType()
			{
				StartPoint = new PointType();
				EndPoint = new PointType();
			}

			/// <summary>
			/// Constructor que toma 2 parámetros: dos puntos. Representan los puntos inicio
			/// y final del segmento respectivamente.  
			/// </summary>
			/// <param name="startPoint">
			/// Punto de inicio del segmento.
			/// </param>
			/// <param name="endPoint">
			/// Punto final del segmento.
			/// </param>
			public SegmentType (PointType startPoint, PointType endPoint)
			{
				StartPoint = startPoint;
				EndPoint = endPoint;
			}

			/// <summary>
			/// Propiedad de solo lectura. Devuelve la longitud del segmento. 
			/// </summary>
			public double Longitude
			{
				get { return is2GraphObj.PointPointDistance(StartPoint, EndPoint); }
			}

			/// <summary>
			/// Propiedad de solo lectura. Devuelve el punto medio del segmento. 
			/// </summary>
			public PointType MidPoint
			{
				get { return is2GraphObj.MidPointBetweenPoint(StartPoint, EndPoint); }
			}

			/// <summary>
			/// Propiedad de solo lectura. Devuelve el ángulo que forma el segmento 
			/// con respeto al eje de las Abcisas (Eje X). 
			/// </summary>
			public double Angle
			{
				get { return is2GraphObj.PointPointAngle(StartPoint, EndPoint); }
			}

			/// <summary>
			/// Propiedad de solo lectura. Devuelve el valor de la pendiente del segmento.<br/>
			/// <b>Nota:</b> La propiedad devuelve NaN si el segmento es vertical, o sea, si es
			/// paralelo al eje de laSs Ordenadas (Eje Y).
			/// </summary>
			public double Slope
			{
				get { return is2GraphObj.PointPointSlope(StartPoint, EndPoint); }
			}

			/// <summary>
			/// Propiedad de solo lectura. Devuelve <b>true</b> si el segmento es Horizontal. 
			/// La caracteristica verticalidad se da si el segmento es paralelo al eje
			/// de las Abcisas (Eje X).
			/// </summary>
			public bool isHorizontal
			{
				get { return is2GraphObj.isEqualValues(StartPoint.cY, EndPoint.cY); }
			}

			/// <summary>
			/// Propiedad de solo lectura. Devuelve <b>true</b> si el segmento es Vertical.
			/// La caracteristica verticalidad se da si el segmento es paralelo al eje 
			/// de las Ordenadas (Eje Y).
			/// </summary>
			public bool isVertical
			{
				get { return is2GraphObj.isEqualValues(StartPoint.cX, EndPoint.cX); }
			}

			/// <summary>
			/// Propiedad de solo lectura. Devuelve <b>true</b> si el segmento es Obliquo a los ejes de coodenadas.
			/// Se define el segmento como obliquo si no es paralelo a ninguno de los ejes de coordenadas. 
			/// </summary>
			public bool isObliquo
			{
				get { return !isHorizontal && !isVertical; }
			}

			/// <summary>
			/// Determina si el segmento es paralelo a otro segmento 'S' dado.
			/// </summary>
			/// <param name="S">
			/// Segmento contra el que se comprueba la propiedad de paralelismo.
			/// </param>
			/// <returns>
			/// Devuelve <b>true</b> si el segmento que invoca al método es paralelo al segmento "S" dado,
			/// en caso contrario devuelve <b>false</b>.
			/// </returns>
			public bool isParallelTo (SegmentType S)
			{
				return !isSecanteTo(S);
			}

			/// <summary>
			/// Determina si el segmento es perpendicular a otro segmento 'S' dado.
			/// </summary>
			/// <param name="S">
			/// Segmento contra el que se comprueba la propiedad de perpendicularidad.
			/// </param>
			/// <returns>
			/// Devuelve <b>true</b> si el segmento que invoca al método es perpendicular al segmento "S" dado,
			/// en caso contrario devuelve <b>false</b>.
			/// </returns>
			public bool isPerpendicularTo (SegmentType S)
			{
				double angle;

				angle = is2GraphObj.SegmentSegmentAngle(this, S);

				return is2GraphObj.isEqualValues(angle, 90.0);
			}

			/// <summary>
			/// Determina si el segmento es secante a otro segmento 'S' dado.
			/// </summary>
			/// <param name="S">
			/// Segmento contra el que se comprueba la propiedad de intersección.
			/// </param>
			/// <returns>
			/// Devuelve <b>true</b> si el segmento que invoca al método es secante al segmento "S" dado,
			///  en caso contrario devuelve <b>false</b>. 
			/// </returns>
			public bool isSecanteTo (SegmentType S)
			{
				PointType p;

				return is2GraphObj.SegmentsApparentIntersect(this, S, out p);
			}

			/// <summary>
			/// Determina si el punto 'P' pasado por parámetro pertenece a Segmento. 
			/// </summary>
			/// <param name="P">
			/// Punto para el que se quiere comprobar la pertenencia al segmento.
			/// </param>
			/// <returns>
			/// Devuelve <b>true</b> si el punto pertenece al segmento en caso contrario devuelve <b>false</b>. 
			/// </returns>
			/// <seealso cref="PointType"/>
			public bool PointInSegment (PointType P)
			{
				return is2GraphObj.PointInSegment(P, this);
			}

			/// <summary>
			/// Comprueba si un punto "P" dado, cumple con la condición de posición relativa al segmento establecida 
			/// por "condition".
			/// </summary>
			/// <param name="P"></param>
			/// <param name="condition"></param>
			/// <returns>
			/// Devuelve <b>true</b> si se cumple 
			/// </returns>
			public bool CheckRelativePosition (PointType P, PointLinePosition condition)
			{
				return is2GraphObj.CheckPointLineRelativePosition(P, ConvertToLine(), condition);
			}

			/// <summary>
			/// Convierte el segmento en un LineType. 
			/// </summary>
			/// <returns>
			/// Devuelve el tipo linea que pasa por los dos puntos del segmento.
			/// </returns>
			public LineType ConvertToLine()
			{
				return new LineType(StartPoint, Angle);
			}

			/// <summary>
			/// Determina cuál es el punto del segmento que tiene mayor X. 
			/// </summary>
			/// <returns>
			/// Devuelve el punto de mayor X en el segmento.
			/// </returns>
			public PointType PointMayorX()
			{
				return (is2GraphObj.MayorOrEqual(StartPoint.cX, EndPoint.cX)) ? StartPoint : EndPoint;
			}

			/// <summary>
			/// Determina cuál es el punto del segmento que tiene mayor Y. 
			/// </summary>
			/// <returns>
			/// Devuelve el punto de mayor Y en el segmento.
			/// </returns>
			public PointType PointMayorY()
			{
				return (is2GraphObj.MayorOrEqual(StartPoint.cY, EndPoint.cY)) ? StartPoint : EndPoint;
			}

			/// <summary>
			/// Determina cuál es el punto del segmento que tiene menor X. 
			/// </summary>
			/// <returns>
			/// Devuelve el punto de menor X en el segmento.
			/// </returns>
			public PointType PointMenorX()
			{
				return (is2GraphObj.MenorOrEqual(StartPoint.cX, EndPoint.cX)) ? StartPoint : EndPoint;
			}

			/// <summary>
			/// Determina cuál es el punto del segmento que tiene menor Y. 
			/// </summary>
			/// <returns>
			/// Devuelve el punto de menor Y en el segmento.
			/// </returns>
			public PointType PointMenorY()
			{
				return (is2GraphObj.MenorOrEqual(StartPoint.cY, EndPoint.cY)) ? StartPoint : EndPoint;
			}
		}
		#endregion

		#region - ArcType -
		/// <summary>
		/// Representa un tipo Arco que puede ser definido de 7 formas.<br/>
		///		[1]: [Start, Any, End]<br/>
		///		[2]: [Start, Center, End]<br/>
		///		[3]: [Start, Center, Angle]<br/>
		///		[4]: [Start, Center, Length]<br/>
		///		[5]: [Start, End, Angle]<br/>
		///		[6]: [Start, End, Direction]<br/>
		///		[7]: [Start, End, Radius]<br/><br/>
		/// 
		/// Nota: Por convención el arco siempre queda definido en sentido antihorario.
		/// </summary>
		public class ArcType
		{
			/// <summary>
			/// Define los tipos de Orientación que puede describir un arco.
			/// </summary>
			public enum ArcDirection
			{
				/// <summary>Representa el sentido Anti-horario.</summary>
				AntiHorario,
				/// <summary>Representa el sentido Horario.</summary>
				Horario
			};

			/// <summary>
			/// Representa el radio del arco.
			/// </summary>
			public double Radius { get; private set; }

			/// <summary>
			/// Representa el angulo del arco.
			/// </summary>
			public double Angle { get; private set; }

			/// <summary>
			/// Propiedad de solo lectura. Devuelve la longitud del arco.
			/// </summary>
			public double Longitude { get; private set; }

			/// <summary>
			/// Representa el ángulo de inicio del arco.
			/// </summary>
			public double StartAngle { get; private set; }

			/// <summary>
			/// Representa el ángulo final del arco.
			/// </summary>
			public double EndAngle { get; private set; }

			/// <summary>
			/// Representa el punto centro del arco.
			/// </summary>
			public PointType Center { get; set; }

			/// <summary>
			/// Representa el punto de inicio del arco.
			/// </summary>
			public PointType StartPoint
			{
				get { return S_Point; }
			}

			/// <summary>
			/// Representa el punto medio del arco.
			/// </summary>
			public PointType MidPoint
			{
				get { return M_Point; }
			}

			/// <summary>
			/// Representa el punto final del arco.
			/// </summary>
			public PointType EndPoint
			{
				get { return E_Point; }
			}

			private PointType S_Point;
			private PointType M_Point;
			private PointType E_Point;

			/// <summary>
			/// Constructor por defecto. 
			/// </summary>
			public ArcType()
			{
				Radius = 0.0;
				StartAngle = 0.0;
				EndAngle = 0.0;
				Center = new PointType();
				S_Point = new PointType();
				M_Point = new PointType();
				E_Point = new PointType();
			}

			/// <summary>
			/// [1]: Crea un Arco usando 3 puntos [Start, Any, End].
			/// </summary>
			/// <param name="start">
			/// Punto de inicio de arco (Startpoint).
			/// </param>
			/// <param name="any">
			/// Un punto cualquiera del arco (Anypoint).
			/// </param>
			/// <param name="end">
			/// Punto final del arco (Endpoint).
			/// </param>
			public ArcType (ArcStartPoint start, ArcAnyPoint any, ArcEndPoint end)
			{
				PointType Pc;
				LineType L1, L2;
				SegmentType S1, S2;
				ArcDirection direction;
				double hipo, s_Ang, e_Ang;

				if (is2GraphObj.isEqualPoint(start.val, any.val) || is2GraphObj.isEqualPoint(any.val, end.val) ||
						is2GraphObj.isEqualPoint(end.val, start.val))
					throw new ArcException("ArcType Class: No se puede obtener un arco con los puntos dados. Los puntos start, any " +
					                       "y end no pueden ser iguales entre si.");

				// Paso 1: Creo dos mediatrices sobre los segmentos S1 y S2.
				//				 Estas 2 bisectrices se cortan sobre el centro del arco.
				S1 = new SegmentType(start.val, any.val);
				S2 = new SegmentType(any.val, end.val);

				L1 = is2GraphObj.PerperdicularLineAt(S1.ConvertToLine(), S1.MidPoint);
				L2 = is2GraphObj.PerperdicularLineAt(S2.ConvertToLine(), S2.MidPoint);

				is2GraphObj.LineLineIntersect(L1, L2, out Pc);

				// Paso 2: Determinar distancia entre "P1" y "pC", que es el radio del arco.
				hipo = is2GraphObj.PointPointDistance(Pc, start.val);

				// b) Calculo los valores en radianes de Inicio Y Fin del arco
				//    usando para el calculo el ángulo opuesto al ángulo del eje 
				//    radial.
				s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(Pc, start.val));
				e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(Pc, end.val));

				direction = GetArcDirection_3P(start.val, any.val, end.val);
				if (direction == ArcDirection.Horario) is2GraphObj.SwapValue(ref s_Ang, ref e_Ang);

				// Paso 3: Seteo las propiedades que definen al arco.
				Center = Pc;
				StartAngle = s_Ang;
				EndAngle = e_Ang;
				Radius = hipo;
				Angle = is2GraphObj.RadToGrad((is2GraphObj.isNegative(e_Ang - s_Ang)) ? (2 * Math.PI) - (s_Ang - e_Ang) : e_Ang - s_Ang);
				Longitude = (2.0 * Math.PI * Radius * Angle) / 360.0;

				// Paso 4 y Final: Calculo los puntos Start, Mid y End.	
				double angS, angM;
				angS = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(Center, start.val));
				angM = (is2GraphObj.isEqualValues(angS, StartAngle)) ? Angle / 2 : Angle / 2 * -1;

				S_Point = start.val;
				M_Point = is2GraphObj.RotatePoint(S_Point, angM, Center);
				E_Point = end.val;
			}

			/// <summary>
			/// [2]: Crea un Arco usando 3 puntos [Start, Center, End]. 
			/// </summary>
			/// <param name="start">
			/// Punto de inicio del arco (Startpoint).
			/// </param>
			/// <param name="center">
			/// Punto centro del arco (Centerpoint).
			/// </param>
			/// <param name="end">
			/// Punto final del arco (Endpoint).
			/// </param>
			/// <param name="inverse">
			/// Indica si se invierte el sentido de barrido por defecto del arco.
			/// </param>
			public ArcType (ArcStartPoint start, ArcCenterPoint center, ArcEndPoint end, bool inverse = false)
			{
				double hipo, s_Ang, e_Ang;

				// Paso 1: Determinar distancia entre "Start" y "Center".		
				hipo = is2GraphObj.PointPointDistance(center.val, start.val);

				// b) Calculo los valores en radianes de Inicio Y Fin del arco
				//    usando para el calculo el ángulo opuesto al ángulo del eje 
				//    radial.
				s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center.val, start.val));
				e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center.val, end.val));

				if (inverse) is2GraphObj.SwapValue(ref s_Ang, ref e_Ang);

				// Paso 2: Seteo las propiedades que definen al arco.
				Center = center.val;
				StartAngle = s_Ang;
				EndAngle = e_Ang;
				Radius = hipo;
				Angle = is2GraphObj.RadToGrad((is2GraphObj.isNegative(e_Ang - s_Ang)) ? (2 * Math.PI) - (s_Ang - e_Ang) : e_Ang - s_Ang);
				Longitude = (2.0 * Math.PI * Radius * Angle) / 360.0;

				// Paso 3 y Final: Calculo los puntos Start, Mid y End.	
				double angS, angM;
				angS = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(Center, start.val));
				angM = (is2GraphObj.isEqualValues(angS, StartAngle)) ? Angle / 2 : Angle / 2 * -1;

				S_Point = start.val;
				M_Point = is2GraphObj.RotatePoint(S_Point, angM, Center);
				E_Point = end.val;
			}

			/// <summary>
			/// [3]: Crea un Arco usando 2 puntos y un ángulo [Start, Center, Angle]. 
			/// </summary>
			/// <param name="start">
			/// Punto de inicio del arco (Startpoint).
			/// </param>
			/// <param name="center">
			/// Punto de centro del arco (Centerpoint).
			/// </param>
			/// <param name="angle">
			/// Ángulo del arco (Angle). 
			/// </param>
			/// <param name="inverse">
			/// Indica si se invierte el sentido de barrido por defecto del arco.
			/// </param>
			public ArcType (ArcStartPoint start, ArcCenterPoint center, ArcGradeAngle angle, bool inverse = false)
			{
				double sA, eA;
				PointType sP, eP, end;
				double hipo, s_Ang, e_Ang;

				// Paso 1: Normalizo el valor del ángulo y compruebo su signo.
				angle.val = is2GraphObj.NormalizeAngle(angle.val);

				if (is2GraphObj.isNegative(angle.val) && inverse == false)
				{
					inverse = true;
				}
				else if (is2GraphObj.isNegative(angle.val) && inverse)
				{
					inverse = false;
					angle.val = Math.Abs(angle.val);
				}
				else if (is2GraphObj.isPositive(angle.val) && inverse)
				{
					angle.val *= -1;
				}

				// Paso 2: Calculo la posicion del punto "End" a partir de los datos conocidos  
				//				 de los puntos "Center" y "Start",  y el ángulo especificado. 
				end = is2GraphObj.RotatePoint(start.val, angle.val, center.val);

				// Paso 3: Se calculo el radio del arco.
				hipo = is2GraphObj.PointPointDistance(center.val, start.val);

				// Paso 4: Se calculan los valores en radianes de Inicio Y Fin del arco.		
				s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center.val, start.val));
				e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center.val, end));

				if (inverse) is2GraphObj.SwapValue(ref s_Ang, ref e_Ang);

				// Paso 4: Seteo las propiedades que definen al arco.
				Center = center.val;
				StartAngle = s_Ang;
				EndAngle = e_Ang;
				Radius = hipo;
				Angle = Math.Abs(angle.val);
				Longitude = (2.0 * Math.PI * Radius * Angle) / 360.0;

				// Paso 5 y Final: Calculo los puntos Start, Mid y End.
				double angS, angM;
				angS = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(Center, start.val));
				angM = (is2GraphObj.isEqualValues(angS, StartAngle)) ? Angle / 2 : Angle / 2 * -1;

				S_Point = start.val;
				M_Point = is2GraphObj.RotatePoint(S_Point, angM, Center);
				E_Point = end;

				/*sP = start.val;
				eP = end;
				sA = is2GraphObj.RadToGrad(StartAngle);
				eA = is2GraphObj.RadToGrad(EndAngle);

				if (inverse || GetArcDirectionByAngle(sA, eA, Angle) == ArcDirection.Horario)
				{
					is2GraphObj.SwapPoint(ref sP, ref eP);
				}

				S_Point = sP;
				M_Point = is2GraphObj.RotatePoint(S_Point, Angle / 2.0, Center);
				E_Point = eP;*/
			}

			/// <summary>
			/// [4]: Crea un Arco usando 2 puntos y una longitud [Start, Center, Length].
			/// </summary>
			/// <param name="start">
			/// Punto de inicio del arco (Startpoint).
			/// </param>
			/// <param name="center">
			/// Punto centro del arco (Centerpoint).
			/// </param>
			/// <param name="length">
			/// Distancia de la cuerda (Lenght). 
			/// </param>
			/// <param name="inverse">
			/// Indica si se invierte el sentido de barrido por defecto del arco.
			/// </param>
			/// <exception cref="ArcException">
			/// Se lanza cuando se intenta crear un arco cuyo valor de longitud de la cuerda es incorrecto.
			/// </exception>
			public ArcType (ArcStartPoint start, ArcCenterPoint center, ArcDistance length, bool inverse = false)
			{
				PointType end, sP, eP;
				double hipo, angle, s_Ang, e_Ang, sA, eA;

				// Paso 1: Se calcula el valor de ángulo que se forma para el valor de distancia dado.
				hipo = is2GraphObj.PointPointDistance(center.val, start.val);
				angle = is2GraphObj.RadToGrad(Math.Asin((length.val / 2.0) / hipo)) * 2.0;
				if (inverse) angle *= -1;

				// Se lanza una exception si se obtiene un Arco invalido. 
				if (double.IsNaN(angle))
					throw new ArcException("ArcType Class: No se puede obtener un arco con el valor de longitud dado.");

				// Paso 2: Calculo la posicion del punto "End" a partir de los datos conocidos  
				//				 de los puntos "Center" y "Start",  y el ángulo calculado. 
				end = is2GraphObj.RotatePoint(start.val, angle, center.val);

				// Paso 3: Se calculan los valores en radianes de Inicio Y Fin del arco.		
				s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center.val, start.val));
				e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center.val, end));

				if (inverse) is2GraphObj.SwapValue(ref s_Ang, ref e_Ang);

				// Paso 4: Seteo las propiedades que definen al arco.
				Center = center.val;
				StartAngle = s_Ang;
				EndAngle = e_Ang;
				Radius = hipo;
				Angle = Math.Abs(angle);
				Longitude = (2.0 * Math.PI * Radius * Angle) / 360.0;

				// Paso 5 y Final: Calculo los puntos Start, Mid y End.
				double angS, angM;
				angS = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(Center, start.val));
				angM = (is2GraphObj.isEqualValues(angS, StartAngle)) ? Angle / 2 : Angle / 2 * -1;

				S_Point = start.val;
				M_Point = is2GraphObj.RotatePoint(S_Point, angM, Center);
				E_Point = end;

				/*sP = start.val;
				eP = end;
				sA = is2GraphObj.RadToGrad(StartAngle);
				eA = is2GraphObj.RadToGrad(EndAngle);

				if (inverse || GetArcDirectionByAngle(sA, eA, Angle) == ArcDirection.Horario)
				{
					is2GraphObj.SwapPoint(ref sP, ref eP);
				}

				S_Point = sP;
				M_Point = is2GraphObj.RotatePoint(S_Point, Angle / 2.0, Center);
				E_Point = eP;*/
			}

			/// <summary>
			/// Variante 5: Crea un Arco usando 2 puntos y un ángulo [Start, End, angle]. 
			/// </summary>
			/// <param name="start">
			/// Punto de inicio del arco (Startpoint).
			/// </param>
			/// <param name="end">
			/// Punto final del arco (Endpoint).
			/// </param>
			/// <param name="angle">
			/// Ángulo del arco (Angle).
			/// </param>
			/// <param name="inverse">
			/// Indica si se invierte el sentido de barrido por defecto del arco.
			/// </param>
			public ArcType (ArcStartPoint start, ArcEndPoint end, ArcGradeAngle angle, bool inverse = false)
			{
				CircleType C1, C2;
				ArcDirection direction;
				PointType P1, P2, center, midP1_2, P3, P4, pivote = null;
				double dist, hipo, cateL, angEjeRadial, s_Ang, e_Ang;

				// Paso 1: Normalizo el valor del ángulo y compruebo su signo.
				angle.val = is2GraphObj.NormalizeAngle(angle.val);
				if (is2GraphObj.isNegative(angle.val))
				{
					angle.val *= -1;
					inverse = !inverse;
				}

				direction = GetArcDirection_2P(start.val, end.val);

				// Paso 2: Determinar distancia entre "Start" y "End" y el punto medio entre ambos.
				P1 = start.val;
				P2 = end.val;
				dist = is2GraphObj.PointPointDistance(P1, P2);
				midP1_2 = is2GraphObj.MidPointBetweenPoint(P1, P2);

				// Paso 3: Se determinan 2 puntos (P3 y P4) en el EJE RADIAL, creando dos circunferencias
				//          auxiliares con centro en 'P2' y 'P2', respectivamente y radio 'radio'.      
				C1 = new CircleType(P1, dist);
				C2 = new CircleType(P2, dist);
				is2GraphObj.CircleCircleIntersect(C1, C2, out P3, out P4);

				// Paso 4: Una vez obtenido los dos puntos del eje Radial. Se determina 
				//         cuál de los dos puntos se usara para traza la línea sobre 
				//         la que se halla el CENTRO del arco. 
				//         Esto se halla usando la siguiente Estrategia:
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

				if (inverse) pivote = (is2GraphObj.isEqualPoint(pivote, P3)) ? P4 : P3;

				// Paso 5: Se calculan datos dimensionales de las construcciones auxiliares
				// a) ángulo del Eje Radial
				angEjeRadial = is2GraphObj.PointPointAngle(midP1_2, pivote);

				// b) La Hipotenuza y Cateto-Largo del Triangulo rectangulo en 'midP'			
				hipo = (dist / 2.0) / Math.Sin(angle.ToRadian / 2.0);
				cateL = Math.Sqrt(Math.Pow(hipo, 2) - Math.Pow((dist / 2.0), 2));

				// Paso 6: Se calcula el Punto Centro del arco a construir.
				center = is2GraphObj.PolarPoint(midP1_2, angEjeRadial, cateL);

				// a) Calculo los valores en radianes de Inicio Y Fin del arco
				//    usando para el calculo el ángulo opuesto al ángulo del eje 
				//    radial.		
				s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P1));
				e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P2));

				if (inverse) is2GraphObj.SwapValue(ref s_Ang, ref e_Ang);

				// Paso 7: Seteo las propiedades que definen al arco.
				Center = center;
				StartAngle = s_Ang;
				EndAngle = e_Ang;
				Radius = hipo;
				Angle = angle.val;
				Longitude = (2.0 * Math.PI * Radius * Angle) / 360.0;

				// Paso 8 y Final: Calculo los puntos Start, Mid y End.
				double angM = (inverse) ? Angle * -1 : Angle;
				S_Point = start.val;
				M_Point = is2GraphObj.RotatePoint(S_Point, angM / 2.0, Center);
				E_Point = end.val;
			}

			/// <summary>
			/// [6]: Crea un Arco usando 2 puntos y una dirección [Start, End, Direction].
			/// </summary>
			/// <param name="start">
			/// Punto de inicio del arco (Startpoint).
			/// </param>
			/// <param name="end">
			/// Punto final del arco (Endpoint).
			/// </param> 
			/// <param name="direction">
			/// Punto que define un vector dirección que es tangente al arco en el punto de inicio (ArcDirection). 
			/// </param>
			/// <param name="inverse">
			/// Indica si se invierte el sentido de barrido por defecto del arco.
			/// </param>
			public ArcType (ArcStartPoint start, ArcEndPoint end, ArcVectorDirection direction, bool inverse = false)
			{
				bool intercept;
				SegmentType cord1_2;
				PointType P1, P2, Pdir, center;
				LineType tangente, normal_dir, normal_dist;
				double hipo, angPP, angPPop, angPdir, s_Ang, e_Ang;

				// Paso 1: Creo las construcciones auxiliares para el calculo del centro del arco.
				P1 = start.val;
				P2 = end.val;
				Pdir = direction.val;

				cord1_2 = new SegmentType(P1, P2);
				tangente = new SegmentType(P1, Pdir).ConvertToLine();
				normal_dir = is2GraphObj.PerperdicularLineAt(tangente, P1);
				normal_dist = is2GraphObj.PerperdicularLineAt(cord1_2.ConvertToLine(), cord1_2.MidPoint);

				// Paso 2: Determino el centro del arco dadas las coonstrucciones auxiliares del paso 1.
				intercept = is2GraphObj.LineLineIntersect(normal_dir, normal_dist, out center);

				if (!intercept) throw new ArcException("ArcType Class: No se puede obtener un arco con el vector dirección dado. El vector" +
																							 "dirección no puede ser colineal a la recta que pasa por los puntos start y end.");

				// Paso 3: Calculo el radio del arco.
				hipo = is2GraphObj.PointPointDistance(center, P1);

				// Paso 4: Calculo los angulos de interes presentes en el arco para poder determinar su orientacion.
				angPP = is2GraphObj.PointPointAngle(P1, P2);
				angPPop = is2GraphObj.OppositeAngle(angPP);
				angPdir = is2GraphObj.PointPointAngle(P1, Pdir);

				if (angPP < angPPop)
				{
					if (angPdir > angPP && angPdir < angPPop)
					{
						s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P2));
						e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P1));
					}
					else
					{
						s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P1));
						e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P2));
					}
				}
				else
				{
					if (angPdir < angPP && angPdir > angPPop)
					{
						s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P1));
						e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P2));
					}
					else
					{
						s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P2));
						e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P1));
					}
				} 

				if (inverse) is2GraphObj.SwapValue(ref s_Ang, ref e_Ang);

				// Paso 5: Seteo las propiedades que definen al arco.
				Center = center;
				StartAngle = s_Ang;
				EndAngle = e_Ang;
				Radius = hipo;
				Angle = is2GraphObj.RadToGrad((is2GraphObj.isNegative(e_Ang - s_Ang)) ? (2 * Math.PI) - (s_Ang - e_Ang) : e_Ang - s_Ang);
				Longitude = (2.0 * Math.PI * Radius * Angle) / 360.0;

				// Paso 6 y Final: Calculo los puntos Start, Mid y End.
				double angS, angM;
				angS = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(Center, start.val));
				angM = (is2GraphObj.isEqualValues(angS, StartAngle)) ? Angle / 2 : Angle / 2 * -1;

				S_Point = start.val;
				M_Point = is2GraphObj.RotatePoint(S_Point, angM, Center);
				E_Point = end.val;
			}

			/// <summary>
			/// [7]: Crea un Arco usando 2 puntos y radio [Start, End, Radius] 
			/// </summary>
			/// <param name="start">
			/// Punto de inicio del arco (Startpoint).
			/// </param>
			/// <param name="end">
			/// Punto final del arco (Endpoint).
			/// </param>
			/// <param name="radius">
			/// Radio del arco (Radius).
			/// </param>
			/// <param name="inverse">
			/// Indica si se invierte el sentido de barrido por defecto del arco.
			/// </param>
			/// <exception cref="ArcException">
			/// Se lanza cuando se intenta crear un arco con un valor de radio incorrecto.
			/// </exception>
			public ArcType (ArcStartPoint start, ArcEndPoint end, ArcRadius radius, bool inverse = false)
			{
				CircleType C1, C2;
				ArcDirection direction;
				PointType P1, P2, center, midP1_2, P3, P4, pivote = null;
				double dist, hipo, cateL, angEjeRadial, s_Ang, e_Ang;

				direction = GetArcDirection_2P(start.val, end.val);

				// Paso 1: Determinar distancia entre "Start" y "End" y el punto medio entre ambos.
				P1 = start.val;
				P2 = end.val;

				dist = is2GraphObj.PointPointDistance(P1, P2);
				midP1_2 = is2GraphObj.MidPointBetweenPoint(P1, P2);

				//
				if (is2GraphObj.MenorEstricto(radius.val, dist / 2.0))
					throw new ArcException("ArcType Class: No se puede obtener un arco con el valor de radio dado.");

				// Paso 2: Se determinan 2 puntos (P3 y P4) en el EJE RADIAL, creando dos circunferencias
				//          auxiliares con centro en 'P2' y 'P2', respectivamente y radio 'radio'.  
				C1 = new CircleType(P1, dist);
				C2 = new CircleType(P2, dist);
				is2GraphObj.CircleCircleIntersect(C1, C2, out P3, out P4);

				// Paso 3: Una vez obtenido los dos puntos del eje Radial. Se determina 
				//         cuál de los dos puntos se usara para traza la línea sobre 
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

				if (inverse) pivote = (is2GraphObj.isEqualPoint(pivote, P3)) ? P4 : P3;

				// Paso 4: Se calculan datos dimensionales de las construcciones auxiliares
				// a) ángulo del Eje Radial.
				angEjeRadial = is2GraphObj.PointPointAngle(midP1_2, pivote);

				// b) La Hipotenuza y Cateto-Largo del Triangulo rectangulo en 'midP'.
				hipo = radius.val;
				cateL = Math.Sqrt(Math.Pow(hipo, 2) - Math.Pow((dist / 2.0), 2));

				// Paso 5: Se calcula el Punto Centro del arco a construir.
				center = is2GraphObj.PolarPoint(midP1_2, angEjeRadial, cateL);

				// a) Calculo los valores en radianes de Inicio Y Fin del arco
				//    usando para el calculo el ángulo opuesto al ángulo del eje 
				//    radial.		
				s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P1));
				e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P2));

				// Paso 6: Seteo las propiedades que definen al arco.
				Center = center;
				StartAngle = s_Ang;
				EndAngle = e_Ang;
				Radius = hipo;
				Angle = is2GraphObj.RadToGrad(Math.Acos(cateL / hipo)) * 2.0;
				Longitude = (2.0 * Math.PI * Radius * Angle) / 360.0;

				// Paso 7 y Final: Calculo los puntos Start, Mid y End.
				double angM = (inverse) ? Angle * -1 : Angle;
				S_Point = start.val;
				M_Point = is2GraphObj.RotatePoint(S_Point, angM / 2.0, Center);
				E_Point = end.val;
			}

			/// <summary>
			/// [8]: Crea un Arco continuo dado una recta y un punto [Line_continue, End].
			/// </summary>
			/// <param name="S">
			/// Segmento de referencia el cual es tangente al arco.
			/// </param>
			/// <param name="end">
			/// Punto final del arco (Endpoint).
			/// </param>
			/// <param name="inverse"></param>
			public ArcType (SegmentType S, ArcEndPoint end, bool inverse = false)
			{
				bool intercept;
				SegmentType cord1_2;
				PointType P1, P2, center;
				LineType normal_S, normal_Cord;
				double hipo, angSeg, angSegOp, angPP, s_Ang, e_Ang;

				// Paso 1: Creo las construcciones auxiliares para el calculo del centro del arco.
				P1 = S.EndPoint;
				P2 = end.val;

				cord1_2 = new SegmentType(P1, P2);
				normal_S = is2GraphObj.PerperdicularLineAt(S.ConvertToLine(), S.EndPoint);
				normal_Cord = is2GraphObj.PerperdicularLineAt(cord1_2.ConvertToLine(), cord1_2.MidPoint);

				// Paso 2: Determino el centro del arco dadas las coonstrucciones auxiliares del paso 1.
				intercept = is2GraphObj.LineLineIntersect(normal_S, normal_Cord, out center);

				if (!intercept) throw new ArcException("ArcType Class: No se puede obtener un arco con el Endpoint dado. El Endpoint del arco no puede " +
				                                       "pertenecer a la recta a la que pertenece el segmento S dado.");
				
				// Paso 3: Calculo el radio del arco.
				hipo = is2GraphObj.PointPointDistance(center, P1);

				// Paso 4: Calculo los angulos de interes presentes en el arco para poder determinar su orientación.
				angPP = is2GraphObj.PointPointAngle(P1, P2);
				angSeg = S.Angle;
				angSegOp = is2GraphObj.OppositeAngle(angSeg);

				if (angSeg < angSegOp)
				{
					if (angPP < angSegOp && angPP > angSeg)
					{
						s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P1));
						e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P2));
					}
					else
					{
						s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P2));
						e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P1));
					}
				}
				else
				{
					if (angPP > angSegOp && angPP < angSeg)
					{
						s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P2));
						e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P1));
					}
					else
					{
						s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P1));
						e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P2));
					}
				}

				if (inverse) is2GraphObj.SwapValue(ref s_Ang, ref e_Ang);

				// Paso 5: Seteo las propiedades que definen al arco.
				Center = center;
				StartAngle = s_Ang;
				EndAngle = e_Ang;
				Radius = hipo;
				Angle = is2GraphObj.RadToGrad((is2GraphObj.isNegative(e_Ang - s_Ang)) ? (2 * Math.PI) - (s_Ang - e_Ang) : e_Ang - s_Ang);
				Longitude = (2.0 * Math.PI * Radius * Angle) / 360.0;

				// Paso 6 y Final: Calculo los puntos Start, Mid y End.
				double angS, angM;
				angS = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(Center, P1));
				angM = (is2GraphObj.isEqualValues(angS, StartAngle)) ? Angle / 2 : Angle / 2 * -1;

				S_Point = P1;
				M_Point = is2GraphObj.RotatePoint(S_Point, angM, Center);
				E_Point = P2;
			}

			/// <summary>
			/// [9]: Crea un Arco continuo dado un arco y un punto [Arc_continue, End]. 
			/// </summary>
			/// <param name="A">
			/// Arco de referencia el cual es tangente al arco a crear.
			/// </param>
			/// <param name="end">
			/// Punto final del arco a crear (Endpoint).
			/// </param>
			/// <param name="inverse">
			/// Indica si se invierte el sentido de barrido por defecto del arco.
			/// </param>
			public ArcType (ArcType A, ArcEndPoint end, bool inverse = false)
			{
				bool intercept;
				SegmentType cord1_2;
				PointType P1, P2, center;
				LineType lineA, normal_Cord, normal_lineA;
				double hipo, angL, angLOp, angPP, s_Ang, e_Ang;

				// Paso 1: Creo las construcciones auxiliares para el calculo del centro del arco.
				P1 = A.EndPoint;
				P2 = end.val; 

				cord1_2 = new SegmentType(P1, P2);
				lineA = new SegmentType(A.Center, A.EndPoint).ConvertToLine();
				normal_lineA = is2GraphObj.PerperdicularLineAt(lineA, P1);
				normal_Cord = is2GraphObj.PerperdicularLineAt(cord1_2.ConvertToLine(), cord1_2.MidPoint);

				// Paso 2: Determino el centro del arco dadas las coonstrucciones auxiliares del paso 1.
				intercept = is2GraphObj.LineLineIntersect(lineA, normal_Cord, out center);

				if (!intercept) throw new ArcException("ArcType Class: No se puede obtener un arco con el Endpoint dado. ");

				// Paso 3: Calculo el radio del arco.
				hipo = is2GraphObj.PointPointDistance(center, P1);

				// Paso 4: Calculo los angulos de interes presentes en el arco para poder determinar su orientación.
				angPP = is2GraphObj.PointPointAngle(P1, P2);
				angL = normal_lineA.Angle;
				angLOp = is2GraphObj.OppositeAngle(angL);

				if (angL < angLOp)
				{
					if (angPP < angLOp && angPP > angL)
					{
						s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P1));
						e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P2));
					}
					else
					{
						s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P2));
						e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P1));
					}
				}
				else
				{
					if (angPP > angLOp && angPP < angL)
					{
						s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P2));
						e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P1));
					}
					else
					{
						s_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P1));
						e_Ang = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(center, P2));
					}
				}

				if (inverse) is2GraphObj.SwapValue(ref s_Ang, ref e_Ang);

				// Paso 5: Seteo las propiedades que definen al arco.
				Center = center;
				StartAngle = s_Ang;
				EndAngle = e_Ang;
				Radius = hipo;
				Angle = is2GraphObj.RadToGrad((is2GraphObj.isNegative(e_Ang - s_Ang)) ? (2 * Math.PI) - (s_Ang - e_Ang) : e_Ang - s_Ang);
				Longitude = (2.0 * Math.PI * Radius * Angle) / 360.0;

				// Paso 6 y Final: Calculo los puntos Start, Mid y End.
				double angS, angM;
				angS = is2GraphObj.GradToRad(is2GraphObj.PointPointAngle(Center, P1));
				angM = (is2GraphObj.isEqualValues(angS, StartAngle)) ? Angle / 2 : Angle / 2 * -1;

				S_Point = P1;
				M_Point = is2GraphObj.RotatePoint(S_Point, angM, Center);
				E_Point = P2;
			}

			/// <summary>
			/// Devuelve por referencia los puntos [Start], [Mid] y [End] del Arco. 
			/// </summary>
			/// <param name="Pi">
			/// Parámetro de salida (out). Retorna por referencia el punto de inicio del arco.
			/// </param>
			/// <param name="Pm">
			/// Parámetro de salida (out). Retorna por referencia punto medio del arco.
			/// </param>
			/// <param name="Pf">
			/// Parámetro de salida (out). Retorna por referencia el punto final del arco.
			/// </param>
			public void GetArc3Points (out PointType Pi, out PointType Pm, out PointType Pf)
			{
				Pi = S_Point;
				Pm = M_Point;
				Pf = E_Point;
			}

			/// <summary>
			/// Duevuelve la orientacion del arco en {HORARIO / ANTIHORARIO} según el ángulo 
			/// de inicio, ángulo final y ángulo de barribo.  
			/// </summary>
			/// <param name="startAngle">
			/// Ángulo de inicio en grados.
			/// </param>
			/// <param name="endAngle">
			/// Ángulo de final en grados.
			/// </param>
			/// <param name="sweepAngle">
			/// Ángulo de barrido en grados.
			/// </param>
			public static ArcDirection GetArcDirectionByAngle (double startAngle, double endAngle, double sweepAngle)
			{
				double ang;
				ArcDirection dir;

				ang = startAngle + sweepAngle;
				endAngle = (is2GraphObj.isEqualCero(endAngle)) ? 360.0 : endAngle;
				dir = (is2GraphObj.isEqualValues(is2GraphObj.NormalizeAngle(ang), endAngle)) ? ArcDirection.AntiHorario : ArcDirection.Horario;

				return dir;
			}

			/// <summary>
			/// Duevuelve la orientacion del arco en {HORARIO / ANTIHORARIO} según sus puntos Start y End. 
			/// </summary>
			/// <param name="start">
			/// Punto de inicio del arco.
			/// </param>
			/// <param name="end">
			/// Punto final del arco.
			/// </param>
			public static ArcDirection GetArcDirection_2P (PointType start, PointType end)
			{
				ArcDirection dir;

				if (// Si los puntos P1, P2 tienen la misma 'Y' y P1 tiene mayor 'X' que P2
						(is2GraphObj.isEqualValues(start.cY, end.cY) && start.cX > end.cX) ||
						// Si los puntos P1, P2 tienen la misma 'Y' y P1 tiene menor 'X' que P2
						(is2GraphObj.isEqualValues(start.cY, end.cY) && start.cX < end.cX) ||
						// Si los puntos P1, P2 tienen la misma 'X' y P1 tiene menor 'Y' que P2
						(is2GraphObj.isEqualValues(start.cX, end.cX) && start.cY < end.cY) ||
						// Si los puntos P1, P2 tienen la misma 'X' y P1 tiene mayor 'Y' que P2
						(is2GraphObj.isEqualValues(start.cX, end.cX) && start.cY > end.cY) ||
						// Si P1 tiene mayor 'X' y menor 'Y' que P2
						(start.cX > end.cX && start.cY < end.cY) ||
						// Si P1 tiene mayor 'X' y mayor 'Y' que P2
						(start.cX > end.cX && start.cY > end.cY) ||
						// Si P1 tiene menor 'X' y mayor 'Y' que P2
						(start.cX < end.cX && start.cY > end.cY) ||
						// Si P1 tiene menor 'X' y menor 'Y' que P2
						(start.cX < end.cX && start.cY < end.cY))
				{
					dir = ArcDirection.AntiHorario;
				}
				else
				{
					dir = ArcDirection.Horario;
				}

				return dir;
			}

			/// <summary>
			/// Duevuelve la orientacion del arco en {HORARIO / ANTIHORARIO} analizado la 
			/// disposicion de 3 de sus puntos (start, any y end).
			/// </summary>
			/// <param name="start">
			/// Punto de inicio del arco.
			/// </param>
			/// <param name="any">
			/// Un punto cualquiera del arco.
			/// </param>
			/// <param name="end">
			/// Punto final del arco.
			/// </param>
			public static ArcDirection GetArcDirection_3P (PointType start, PointType any, PointType end)
			{
				ArcDirection dir;
				double ang1, angOp, angX;

				// Calculo el ángulo que forman start-End y start-Px, ademas del ángulo opuesto de este ultimo.
				ang1 = is2GraphObj.PointPointAngle(start, end);
				angX = is2GraphObj.PointPointAngle(start, any);
				angOp = is2GraphObj.OppositeAngle(ang1);

				if (// Si los puntos Start, End tienen la misma 'Y' ademas Start tiene mayor 'X' que End y
						// el punto arbitrario Px tiene mayor-o-igual 'Y' que Start y End.
						(is2GraphObj.isEqualValues(start.cY, end.cY) && start.cX > end.cX && any.cY >= start.cY) ||
						// Si los puntos Start, End tienen la misma 'Y' ademas Start tiene menor 'X' que End y
						// el punto arbitrario Px tiene menor-o-igual 'Y' que Start y End.
						(is2GraphObj.isEqualValues(start.cY, end.cY) && start.cX < end.cX && any.cY <= start.cY) ||
						// Si los puntos Start, End tienen la misma 'X' ademas Start tiene menor 'Y' que End y
						// el punto arbitrario Px tiene mayor-o-igual 'X' que Start y End.
						(is2GraphObj.isEqualValues(start.cX, end.cX) && start.cY < end.cY && any.cX >= start.cX) ||
						// Si los puntos Start, End tienen la misma 'X' ademas Start tiene mayor 'Y' que End y
						// el punto arbitrario Px tiene menor-o-igual 'X' que Start y End.
						(is2GraphObj.isEqualValues(start.cX, end.cX) && start.cY > end.cY && any.cX <= start.cX) ||
						// Si Start tiene mayor 'X' y menor 'Y' que End, ademas el ángulo que se forma entre
						// Start-Px es menor-o-igual que el que se forma entre Start-End o mayor-o-igual que su
						// opuesto.
						(start.cX > end.cX && start.cY < end.cY && (is2GraphObj.MenorOrEqual(angX, ang1) || is2GraphObj.MayorOrEqual(angX, angOp))) ||
						// Si Start tiene mayor 'X' y mayor 'Y' que End, ademas el ángulo que se forma entre
						// Start-Px es menor-o-igual que el que se forma entre Start-End o mayor-o-igual que su
						// opuesto.
						(start.cX > end.cX && start.cY > end.cY && (is2GraphObj.MayorOrEqual(angX, angOp) && is2GraphObj.MenorOrEqual(angX, ang1))) ||
						// Si Start tiene menor 'X' y mayor 'Y' que End, ademas el ángulo que se forma entre
						// Start-Px es menor-o-igual que el que se forma entre Start-End o mayor-o-igual que su
						// opuesto.
						(start.cX < end.cX && start.cY > end.cY && (is2GraphObj.MayorOrEqual(angX, angOp) && is2GraphObj.MenorOrEqual(angX, ang1))) ||
						// Si Start tiene menor 'X' y menor 'Y' que End, ademas el ángulo que se forma entre
						// Start-Px es menor-o-igual que el que se forma entre Start-End o mayor-o-igual que su
						// opuesto.
						(start.cX < end.cX && start.cY < end.cY && (is2GraphObj.MenorOrEqual(angX, ang1) || is2GraphObj.MayorOrEqual(angX, angOp))))
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
		#endregion

		#region - PolylineType -
		/// <summary>
		/// Representa un tipo Polilínea.
		/// </summary>
		public class PolylineType
		{
			private int nItems;
			private int nVertexs;
			private bool isClosed;
			private PointType startP, lastP;
			private List <PolylineElement> Elements;

			/// <summary>
			/// Constructor por defecto.
			/// </summary>
			public PolylineType()
			{
				nItems = 0;
				nVertexs = 0;
				isClosed = false;
				lastP = null;
				startP = null; 
				Elements = new List<PolylineElement>();
			}

			/// <summary>
			/// Destructor de la clase. 
			/// </summary>
			~PolylineType()
			{
				Clear();
			}

			/// <summary>
			/// Agrega un vertice a la Polyline. 
			/// </summary>
			/// <param name="vertex">
			/// Vértice que se agregara a la polilínea.
			/// </param>
			public void AddVertex(PointType vertex)
			{
				if (isClosed) return;

				if (lastP != null && is2GraphObj.isEqualPoint(vertex, lastP) ||
						lastP == null && is2GraphObj.isEqualPoint(vertex, startP)) 
					return;

				Elements.Add(new PolylineElement(vertex));

				if (startP == null)
					startP = vertex;
				else
					lastP = vertex;

				//if (is2GraphObj.isEqualPoint(startP, lastP) && nVertexs > 2) isClosed = true;
				if (is2GraphObj.isEqualPoint(startP, lastP)) isClosed = true;

				nItems++;
				nVertexs++;
			}

			/// <summary>
			/// Agrega un segmento de recta a la polilínea.
			/// </summary>
			/// <param name="S"></param>
			/// <returns></returns>
			public void JoinEntity (SegmentType S)
			{
				bool left, right;
				PointType opposite;

				if (isClosed) return;

				//--------
				// Paso 1: Compruebo si la polilinea esta vacia. De estarlo, la accion de juntar el segmento
				// pasado por parametro es igual simplemente a agregar el segmento.
				if (nVertexs == 0)
				{
					Elements.Add(new PolylineElement(S));

					nItems = 1;
					nVertexs = 2;
					startP = S.StartPoint;
					lastP = S.EndPoint;

					return;
				}

				//--------
				// Paso 2: Compruebo si es válido realizar el Join.
				opposite = null;
				left = right = false;
				if (is2GraphObj.isEqualPoint(startP, S.EndPoint) && is2GraphObj.isEqualPoint(lastP, S.StartPoint))
				{
					right = true;
					opposite = S.EndPoint;
				}
				else if (is2GraphObj.isEqualPoint(startP, S.StartPoint) && is2GraphObj.isEqualPoint(lastP, S.EndPoint))
				{
					right = true;
					opposite = S.StartPoint;
				}
				else if (is2GraphObj.isEqualPoint(startP, S.StartPoint) && nVertexs == 1)
				{
					right = true;
					opposite = S.EndPoint;
				}
				else if (is2GraphObj.isEqualPoint(startP, S.EndPoint)&& nVertexs == 1)
				{
					right = true;
					opposite = S.StartPoint;
				}
				else if (is2GraphObj.isEqualPoint(startP, S.StartPoint))
				{
					left = true;
					opposite = S.EndPoint;
				}
				else if (is2GraphObj.isEqualPoint(startP, S.EndPoint))
				{
					left = true;
					opposite = S.StartPoint;
				}
				else if (is2GraphObj.isEqualPoint(lastP, S.StartPoint))
				{
					right = true;
					opposite = S.EndPoint;
				}
				else if (is2GraphObj.isEqualPoint(lastP, S.EndPoint))
				{
					right = true;
					opposite = S.StartPoint;
				}

				if (!left && !right)
					throw new PolylineException("PolylineType Class: No se pudo completar el join. No hay puntos extremos coincidentes" +
					                            "entre la polilínea y el segmento.");

				//--------
				// Paso 3: Determino por cual de los dos extremos relativos de la polilinea (izquierzo (start) y derecho (end))
				// se realizará el Join.
				if (left)
				{
					startP = opposite;
					Elements.Insert(0, new PolylineElement(S));
				}
				else
				{
					lastP = opposite;
					Elements.Add(new PolylineElement(S));
				}

				if (is2GraphObj.isEqualPoint(startP, lastP)) isClosed = true;

				nItems++;
				nVertexs++;
			}

			/// <summary>
			/// Agrega un arco de circunferencia a la polilínea.
			/// </summary>
			/// <param name="A">
			/// Arco de circunferencia a juntar.
			/// </param>
			/// <returns>
			/// </returns>
			public void JoinEntity (ArcType A)
			{
				bool left, right;
				PointType opposite;

				if (isClosed) return;

				//--------
				// Paso 1: Compruebo si la polilinea esta vacia. De estarlo, la accion de juntar el arco
				// pasado por parametro es igual simplemente a agregar el arco.
				if (nVertexs == 0)
				{
					Elements.Add(new PolylineElement(A));

					nItems = 1;
					nVertexs = 2;
					startP = A.StartPoint;
					lastP = A.EndPoint;

					return;
				}

				//--------
				// Paso 2: Compruebo si es válido realizar el Join.
				opposite = null;
				left = right = false;
				if (is2GraphObj.isEqualPoint(startP, A.EndPoint) && is2GraphObj.isEqualPoint(lastP, A.StartPoint))
				{
					right = true;
					opposite = A.EndPoint;
				}
				else if (is2GraphObj.isEqualPoint(startP, A.StartPoint) && is2GraphObj.isEqualPoint(lastP, A.EndPoint))
				{
					right = true;
					opposite = A.StartPoint;
				}
				else if (is2GraphObj.isEqualPoint(startP, A.StartPoint) && nVertexs == 1)
				{
					right = true;
					opposite = A.EndPoint;
				}
				else if (is2GraphObj.isEqualPoint(startP, A.EndPoint) && nVertexs == 1)
				{
					right = true;
					opposite = A.StartPoint;
				}
				else if (is2GraphObj.isEqualPoint(startP, A.StartPoint))
				{
					left = true;
					opposite = A.EndPoint;
				}
				else if (is2GraphObj.isEqualPoint(startP, A.EndPoint))
				{
					left = true;
					opposite = A.StartPoint;
				}
				else if (is2GraphObj.isEqualPoint(lastP, A.StartPoint))
				{
					right = true;
					opposite = A.EndPoint;
				}
				else if (is2GraphObj.isEqualPoint(lastP, A.EndPoint))
				{
					right = true;
					opposite = A.StartPoint;
				}

				if (!left && !right)
					throw new PolylineException("PolylineType Class: No se pudo completar el join. No hay puntos extremos coincidentes" +
																			"entre la polilínea y el arco.");

				//--------
				// Paso 3: Determino por cual de los dos extremos de la polilinea (izquierzo y derecho)
				//				 se realizará el Join.
				if (left)
				{
					startP = opposite;
					Elements.Insert(0, new PolylineElement(A));
				}
				else
				{
					lastP = opposite;
					Elements.Add(new PolylineElement(A));
				}

				if (is2GraphObj.isEqualPoint(startP, lastP)) isClosed = true;

				nItems++;
				nVertexs++;
			}

			/// <summary>
			/// Junta la polilínea "pl" dada a la polilínea actual.
			/// </summary>
			/// <param name="pl">
			/// Polilínea a juntar.
			/// </param>
			public void JoinEntity (PolylineType pl)
			{
				bool left, right;
				PointType opposite, sP, eP;

				if (isClosed) return;

				//--------
				// Paso 1: Compruebo si la polilinea esta vacia. De estarlo, la accion de juntar el arco
				// pasado por parametro es igual simplemente a agregar el arco.
				if (nVertexs == 0)
				{
					Elements.AddRange(pl.Explode());

					nItems = pl.getNoItems();
					nVertexs = pl.getNoVertex();
					startP = pl.getFirstVertex();
					lastP = pl.getLastVertex();

					return;
				}

				//--------
				// Paso 2: Compruebo si es válido realizar el Join.
				opposite = null;
				left = right = false;
				sP = pl.getFirstVertex();
				eP = pl.getLastVertex();

				if (is2GraphObj.isEqualPoint(startP, sP))
				{
					left = true;
					opposite = eP;
				}
				if (is2GraphObj.isEqualPoint(startP, eP))
				{
					left = true;
					opposite = sP;
				}
				if (is2GraphObj.isEqualPoint(lastP, sP))
				{
					right = true;
					opposite = eP;
				}
				if (is2GraphObj.isEqualPoint(lastP, eP))
				{
					right = true;
					opposite = sP;
				}

				if (!left && !right)
					throw new PolylineException("PolylineType Class: No se pudo completar el join. No hay puntos extremos coincidentes" +
																			"entre las dos polilíneas.");

				//--------
				// Paso 3: Determino por cual de los dos extremos de la polilinea (izquierzo y derecho)
				//				 se realizará el Join.
				if (left)
				{
					startP = opposite;
					Elements.InsertRange(0, pl.Explode());
				}
				else
				{
					lastP = opposite;
					Elements.AddRange(pl.Explode());
				}

				if (is2GraphObj.isEqualPoint(startP, lastP)) isClosed = true;

				nItems += pl.getNoItems();
				nVertexs += getNoVertex() - 1;
			}

			/// <summary>
			/// Genera una explosión de los elementos que conforman la polilinea.
			/// </summary>
			/// <returns>
			/// Retorna un array de PolylineElement con los elementos de la polilínea. Si la polilinea esta vacia retorna null.
			/// </returns>
			public PolylineElement[] Explode()
			{
				return (Elements.Count > 0) ? Elements.ToArray() : null;
			}

			/// <summary>
			/// Determina el número de vértices que contiene la polilínea.
			/// </summary>
			/// <returns>
			/// Devuelve el número de vértices de la polilínea.
			/// </returns>
			public int getNoVertex()
			{
				return nVertexs;
			}

			/// <summary>
			/// Determina el número de items que contiene la polilínea. 
			/// </summary>
			/// <returns>
			/// Devuelve el número de items de la polilínea.
			/// </returns>
			public int getNoItems()
			{
				return nItems;	
			}

			/// <summary>
			/// Determina cuál es el primer punto de la polilínea.
			/// </summary>
			/// <returns>
			/// Devuelve el primer punto de la polilínea. Si la polilínea esta vacía devuelve null.
			/// </returns>
			public PointType getFirstVertex()
			{
				return (nVertexs != 0) ? startP : null;
			}

			/// <summary>
			/// Determina cuál es el último punto de la polilínea.
			/// </summary>
			/// <returns>
			/// Devuelve el último punto de la polilínea. Si la polilínea esta vacía devuelve null.
			/// </returns>
			public PointType getLastVertex()
			{
				return (nVertexs != 0) ? lastP : null;
			}

			/// <summary>
			/// Determina cuál es el primer elemento de la polilínea.
			/// </summary>
			/// <returns>
			/// Devuelve el primer elemento de la polilínea. Si la polilínea esta vacía devuelve null.
			/// </returns>
			/// <remarks>
			/// Un elemento de la polilínea puede ser: un punto, un segmento, un arco.
			/// </remarks>
			public PolylineElement getFirstElement()
			{
				return (nItems != 0) ? Elements[0] : null;
			}

			/// <summary>
			/// Determina cuál es el último elemento de la polilínea.
			/// </summary>
			/// <returns>
			/// Devuelve el último elemento de la polilínea. Si la polilínea esta vacía devuelve null.
			/// </returns>
			/// <remarks>
			/// Un elemento de la polilínea puede ser: un punto, un segmento, un arco.
			/// </remarks>
			public PolylineElement getLastElement()
			{
				return (nItems != 0) ? Elements[nItems - 1] : null;
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="index">
			/// Indice del vertice a devolver.<br/>
			/// <b>Nota:</b> El primer vértice de la polilínea tiene indice 0.
			/// </param>
			/// <returns>
			/// Devuelve un punto de la polilínea segun su índice. Si la polilínea esta vacía devuelve null.
			/// </returns>
			public PolylineElement getByIndex (int index)
			{
				return (index >= 0 && index <= nItems - 1) ? Elements[index] : null;
			}

			/// <summary>
			/// Borra todos los vértices que contiene la polilinea.
			/// </summary>
			public void Clear()
			{
				nItems = 0;
				nVertexs = 0;
				isClosed = false;
				Elements.Clear();
			}
		}
		#endregion

		#region - ElipseType -
		/// <summary>
		/// Representa un tipo Elipse.
		/// </summary>
		public class ElipseType
		{
			/// <summary>
			/// Representa el centro de la elipse.
			/// </summary>
			public PointType Center;
			/// <summary>
			/// Representa el valor del semi-eje horizontal.
			/// </summary>
			public double SemiEjeA;
			/// <summary>
			/// Representa el valor del semi-eje vertical.
			/// </summary>
			public double SemiEjeB;

			/// <summary>
			/// 
			/// </summary>
			public ElipseType()
			{
				SemiEjeA = 0;
				SemiEjeB = 0;
				Center = new PointType();
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="P"></param>
			/// <param name="semiX"></param>
			/// <param name="semiY"></param>
			public ElipseType (PointType P, double semiX, double semiY)
			{
				Center = P;
				SemiEjeA = semiX;
				SemiEjeB = semiY;
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="P"></param>
			/// <param name="semiX"></param>
			/// <param name="semiY"></param>
			/// <param name="angle"></param>
			public ElipseType(PointType P, double semiX, double semiY, double angle)
			{
				Center = P;
				SemiEjeA = semiX;
				SemiEjeB = semiY;
			}

			public double Area()
			{
				throw new NotImplementedException();
			}

			public double Perimeter()
			{
				throw new NotImplementedException();
			}

			public bool isPointInside (PointType P)
			{
				throw new NotImplementedException();
			}
		}
		#endregion

		#region - PolygonType -
		/// <summary>
		/// Representa un tipo Polígono regular convexo.
		/// </summary>
		public class PolygonType
		{
			private double radius;
			private int Nlados;
			private CircleType.Type TypeOfCircle;

			/// <summary>
			/// 
			/// </summary>
			public PointType Center;

			/// <summary>
			/// 
			/// </summary>
			public double Radius
			{
				set { radius = value; }
				get { return radius; }
			}

			PolygonType (PointType center, double radius, int nlados, CircleType.Type type)
			{
				if (nlados < 3)
					throw new PolygonException("El menor número de lados que puede tener el tipo polígono es 3.");

				Center = center;
				this.radius = radius;
			}

			public double Area ()
			{
				throw new NotImplementedException();
			}

			public double Perimeter ()
			{
				throw new NotImplementedException();
			}

			public bool isPointInside (PointType P)
			{
				throw new NotImplementedException();
			}
		}
		#endregion

		#region - RectangleType -
		/// <summary>
		/// Representa un tipo Rectángulo.
		/// </summary>
		public class RectangleType
		{
			public PointType Lcorner;
			public PointType Rcorner;

			public RectangleType (PointType pL, PointType pR)
			{
			}

			public RectangleType (PointType pL, double Xoffset, double Yoffset)
			{
			}

			public double Area()
			{
				throw new NotImplementedException();
			}

			public double Perimeter()
			{
				throw new NotImplementedException();
			}

			public void GetVertex (out PointType v1, out PointType v2, out PointType v3, out PointType v4)
			{
				throw new NotImplementedException();
			}

			public void GetSides (out SegmentType s1, out SegmentType s2, out SegmentType s3, out SegmentType s4)
			{
				throw new NotImplementedException();
			}

			public bool isPointInside (PointType P)
			{
				throw new NotImplementedException();
			}
		}
		#endregion

		#region - TriangleType -
		/// <summary>
		/// Representa un tipo Triángulo.
		/// </summary>
		public class TriangleType
		{
			/// <summary>
			/// 
			/// </summary>
			public enum TriangleTypeBySide
			{
			}

			/// <summary>
			/// 
			/// </summary>
			public enum TriangleTypeByAngle
			{
			}

			private PointType _Vertex1_;
			private PointType _Vertex2_;
			private PointType _Vertex3_;

			/// <summary>
			/// 
			/// </summary>
			/// <param name="v1"></param>
			/// <param name="v2"></param>
			/// <param name="v3"></param>
			public TriangleType (PointType v1, PointType v2, PointType v3)
			{
				_Vertex1_ = v1;
				_Vertex2_ = v2;
				_Vertex3_ = v3;
			}

			public double Area ()
			{
				throw new NotImplementedException();
			}

			public double Perimeter ()
			{
				throw new NotImplementedException();
			}

			public void GetVertex (out PointType v1, out PointType v2, out PointType v3)
			{
				throw new NotImplementedException();
			}

			public void GetSides (out SegmentType s1, out SegmentType s2, out SegmentType s3)
			{
				throw new NotImplementedException();
			}

			public bool isPointInside (PointType P)
			{
				throw new NotImplementedException();
			}

			public TriangleTypeBySide TypeBySide()
			{
				throw new NotImplementedException();
			}

			public TriangleTypeByAngle TypeByAngle ()
			{
				throw new NotImplementedException();
			}

			public CircleType CircunscriptCicle()
			{
				throw new NotImplementedException();
			}

			public CircleType InscriptCicle()
			{
				throw new NotImplementedException();
			}

			public void GetAlturas (out SegmentType s1, out SegmentType s2, out SegmentType s3)
			{
				throw new NotImplementedException();
			}

			public void GetMediatrices (out SegmentType s1, out SegmentType s2, out SegmentType s3)
			{
				throw new NotImplementedException();
			}

			public void GetBisectrices (out SegmentType s1, out SegmentType s2, out SegmentType s3)
			{
				throw new NotImplementedException();
			}
		}
		#endregion

		#region - PlaneType -
		/// <summary>
		/// Representa un tipo Plano.
		/// </summary>
		public class PlaneType
		{
			/// <summary>
			/// Define un punto en el plano.
			/// </summary>
			public PointType OnePoint;

			/// <summary>
			/// Define un vector dirección en el plano.
			/// </summary>
			public PointType DirVector;

			/// <summary>
			/// Representa el angulo que forma ??.
			/// </summary>
			public double Angle;
		}
		#endregion
	#endregion

	#region - Funciones Estáticas de is2GraphObj -
	/// <summary>
	/// Biblioteca gráfica de entidades 2D.
	/// </summary>
	/// <remarks>
	/// is2GraphObj es la versión Orientada a Objetos de su antecesora is2Graph,  
	/// re-escrita completamente para la tecnologia .Net de Microsoft. 
	/// Representa la evolución de la libreria gráfica escrita por DrC. Ricardo
	/// Ávila Rondón llamada is2Graph (implementada inicialmente en c++ la cuál no
	/// tenia soporte para la orientacion a objetos).
	/// 
	/// Is2GraphObj corrige un grupo de bugs que existían en su antecesora, asi mismo
	/// agrega nuevas entidades geometricas, muchas más características y más funcionalidades.
	/// </remarks>
	public class is2GraphObj
	{
		private static double CeroReal = 1E-6;
		private static readonly PointType _Origen_ = new PointType(0.0, 0.0, 0.0);

		/// <summary>
		/// Propiedad de solo lectura. Devuelve el Origen de Sistema de Coordenadas.
		/// </summary>
		public static PointType OrigenXYZ
		{
			get { return _Origen_; }
		}

		/// <summary>
		/// Establece la precision (posiciones decimales) que tiene en cuenta is2Graph
		/// para comprobar el valor del Cero Real.
		/// </summary>
		/// <param name="d">
		/// Es un número entero entre 1 - 10 que establece la precisión sobre valores decimales
		/// con la que trabajara Is2Graph.<br/> 
		/// </param> 
		public static void SetPresicion (uint d)
		{
			switch (d)
			{
				case 0: case 1:
					CeroReal = 1E-1;
					break;

				case 2:
					CeroReal = 1E-2;
					break;

				case 3:
					CeroReal = 1E-3;
					break;

				case 4:
					CeroReal = 1E-4;
					break;

				case 5:
					CeroReal = 1E-5;
					break;

				case 6:
					CeroReal = 1E-6;
					break;

				case 7:
					CeroReal = 1E-7;
					break;

				case 8:
					CeroReal = 1E-8;
					break;

				case 9:
					CeroReal = 1E-9;
					break;

				case 10:
					CeroReal = 1E-10;
					break;

				default:
					CeroReal = 1E-6;
					break;
			}
		}

		#region - Comparations and Convertion Functions -
		/// <summary>
		/// Convierte de Grados a Radianes.
		/// </summary>
		/// <param name="angle">
		/// Valor de ángulo en grados.
		/// </param>
		/// <returns>
		/// Devuelve un valor de ángulo en radianes.
		/// </returns>
		public static double GradToRad (double angle)
		{
			return angle * Math.PI / 180.0;
		}

		/// <summary>
		/// Convierte de Radianes a Grados.
		/// </summary>
		/// <param name="radian">
		/// Valor de ángulo en radianes.
		/// </param>
		/// <returns>
		/// Devuelve un valor de ángulo en grados.
		/// </returns>
		public static double RadToGrad (double radian)
		{
			return radian * 180.0 / Math.PI;
		}

		/// <summary>
		/// Determina el cuadrante en el que se ubica el valor de ángulo dado.
		/// Nota: el valor de ángulo esta expresado en grados.
		/// </summary>
		/// <param name="angle">
		/// Valor de ángulo en grados.
		/// </param>
		/// <returns>
		/// Devuelve un tipo enum que representa el cuadrante en que se ubica el ángulo dado.
		/// </returns>
		public static Cuadrante AngleInQuadrant (double angle)
		{
			Cuadrante Q;

			angle = NormalizeAngle(Math.Abs(angle));

			if (MayorOrEqual(angle, 0.0) && MenorOrEqual(angle, 90.0))
				Q = Cuadrante.I;
			else if (MayorEstricto(angle, 90.0) && MenorOrEqual(angle, 180.0))
				Q = Cuadrante.II;
			else if (MayorEstricto(angle, 180.0) && MenorOrEqual(angle, 270.0))
				Q = Cuadrante.III;
			else
				Q = Cuadrante.IV;

			return Q;
		}

		/// <summary>
		/// Determina el cuadrante en el que se ubica el punto dado.
		/// </summary>
		/// <param name="P">
		/// Representa el punto a comprobar.
		/// </param>
		/// <returns>
		/// Devuelve un tipo enum que representa el cuadrante en que se ubica el punto dado.
		/// </returns>
		/// <seealso cref="PointType"/>
		public static Cuadrante PointInQuadrant (PointType P)
		{
			Cuadrante Q;

			// 1er cuadrante [+X +Y +Z]
			if (isPositive(P.cX) && isPositive(P.cY) && isPositive(P.cZ))
				Q = Cuadrante.I;
			// 2do cuadrante [+X -Y +Z]
			else if (isPositive(P.cX) && isNegative(P.cY) && isPositive(P.cZ))
				Q = Cuadrante.II;
			// 3er cuadrante [+X -Y -Z]
			else if (isPositive(P.cX) && isNegative(P.cY) && isNegative(P.cZ))
				Q = Cuadrante.III;
			// 4to cuadrante [+X +Y -Z]
			else
			 Q = Cuadrante.IV;

			return Q;
		}

		/// <summary>
		/// Determina sobre que octante se ubica el punto "P" dado.
		/// </summary>
		/// <param name="P">
		/// Punto a comprobar.
		/// </param>
		/// <returns>
		/// Devuelve el octante en que se ubica el punto dado.
		/// </returns>
		/// <seealso cref="PointType"/>
		public static Octante PointInOctant (PointType P)
		{
			Octante Q;

			// 1er octante [+X +Y +Z]
			if (isPositive(P.cX) && isPositive(P.cY) && isPositive(P.cZ))
				Q = Octante.I;
			// 2do octante [+X -Y +Z]
			else if (isPositive(P.cX) && isNegative(P.cY) && isPositive(P.cZ))
				Q = Octante.II;
			// 3er octante [+X -Y -Z]
			else if (isPositive(P.cX) && isNegative(P.cY) && isNegative(P.cZ))
				Q = Octante.III;
			// 4to octante [+X +Y -Z]
			else if (isPositive(P.cX) && isPositive(P.cY) && isNegative(P.cZ))
				Q = Octante.IV;
			// 5to octante [-X +Y +Z]
			else if (isNegative(P.cX) && isPositive(P.cY) && isPositive(P.cZ))
				Q = Octante.V;
			// 6to octante [-X -Y +Z]
			else if (isNegative(P.cX) && isNegative(P.cY) && isPositive(P.cZ))
				Q = Octante.VI;
			// 7mo octante [-X -Y -Z]
			else if (isNegative(P.cX) && isNegative(P.cY) && isNegative(P.cZ))
				Q = Octante.VII;
			// 8vo octante [-X +Y -Z]
			else
				Q = Octante.VIII;

			return Q;
		}

		/// <summary>
		/// Determina dado un ángulo, el valor del ángulo opuesto en el sistema de ejes de coordenadas.
		/// </summary>
		/// <param name="angle">
		/// Valor de ángulo en grados.
		/// </param>
		/// <returns>
		/// Devuelve el valor de ángulo opuesto al ángulo dado.
		/// </returns>
		public static double OppositeAngle (double angle)
		{
			double opositeAng;

			angle = NormalizeAngle(angle);

			if (angle >= 0.0 && angle <= 90.0)
				opositeAng = 180.0 + angle;
			else if (angle > 90.0 && angle <= 180.0)
				opositeAng = 270.0 + (angle - 90.0);
			else if (angle > 180.0 && angle <= 270.0)
				opositeAng = angle - 180.0;
			else
				opositeAng = 90.0 + (angle - 270.0);

			return opositeAng;
		}

		/// <summary>
		/// Determina dado un angulo, el valor del angulo complementario
		/// </summary>
		/// <param name="angle">
		/// Valor de ángulo en grados.
		/// </param>
		/// <remarks>
		/// Se define como ángulo complementario de un ??
		/// </remarks>
		/// <returns>
		/// Devuelve el valor de ángulo complementrio al ángulo dado. 
		/// </returns>
		public static double ComplementaryAngle (double angle)
		{
			double ang;

			if (angle <= 90.0)
				ang = 90.0 - angle;
			else if ((angle > 90.0) && (angle <= 180.0))
				ang = angle - 90.0;
			else if ((angle > 180.0) && (angle <= 270.0))
				ang = 270.0 - angle;
			else
				ang = angle - 270.0;

			return ang;
		}

		/// <summary>
		/// Normaliza un valor de ángulo. 
		/// </summary>
		/// <param name="angle">
		/// Valor de ángulo en grados.
		/// </param>
		/// <returns>
		/// Devuelve ángulo en grados normalizado.
		/// </returns>
		/// <remarks>
		/// El proceso de Normalización garantiza que un valor de angulo no exceda los 360 grados,
		/// para lo cuál se calcula ??
		/// </remarks>
		public static double NormalizeAngle (double angle)
		{
			if (angle < 0.0)
			{
				if (MayorOrEqual(angle, -360.0)) return angle;

				return angle % (-360);
			}

			if (MenorOrEqual(angle, 360.0)) return angle;

			return angle % 360;
		}

		/// <summary>
		/// Determina si el valor pasado por parámetro es positivo.
		/// </summary>
		/// <param name="value">
		/// Valor decimal a comprobar.
		/// </param>
		/// <returns>
		/// Develve <b>true</b> si el valor pasado por parámetro es positivo, en caso contrario devuelve <b>false</b>. 
		/// </returns>
		public static bool isPositive (double value)
		{
			return (value >= 0.0);
		}

		/// <summary>
		/// Determina si el valor pasado por parámetro es negativo.
		/// </summary>
		/// <param name="value">
		/// Valor decimal a comprobar.
		/// </param>
		/// <returns>
		/// Develve <b>true</b> si el valor pasado por parámetro es negativo, en caso contrario devuelve <b>false</b>. 
		/// </returns>
		public static bool isNegative (double value)
		{
			return (value < 0.0);
		}

		/// <summary>
		/// Determina si el parámetro 'value' se considera un cero real.
		/// </summary>
		/// <param name="value">
		/// Valor decimal a comprobar.
		/// </param>
		/// <remarks>
		/// Se considera que un valor es cero real si este valor es menor que la constante definida CERO_REAL. 
		/// Por defecto se considera la precision de la constante CERO_REAL igual a: (+-)1E-6 => 0.000001.
		/// </remarks>
		/// <returns>
		/// Devuelve <b>true</b> si el valor pasado por parámetro presenta un cero real, en caso contrario devuelve <b>false</b>.
		/// </returns>
		public static bool isEqualCero (double value)
		{
			return (Math.Abs(value) < CeroReal);
		}

		/// <summary>
		/// Determina si los dos valor pasados por parámetro son iguales. 
		/// </summary>
		/// <param name="a">
		/// Primer valor decimal.
		/// </param>
		/// <param name="b">
		/// Segundo valor decimal.
		/// </param>
		/// <returns>
		/// Devuelve <b>true</b> si ambas valores son iguales, en caso contrario devuelve <b>false</b>.
		/// </returns>
		public static bool isEqualValues (double a, double b)
		{
			return (isEqualCero(a - b));
		}

		/// <summary>
		/// Determina si el primer parámetro es estrictamente mayor que el segundo. 
		/// </summary>
		/// <param name="a">
		/// Primer valor decimal.
		/// </param>
		/// <param name="b">
		/// Segundo valor decimal.
		/// </param>
		/// <returns>
		/// Devuelve <b>true</b> si se cumple que "a" es mayor estricto que "b", en caso contrario devuevle <b>false</b>.
		/// </returns>
		public static bool MayorEstricto (double a, double b)
		{
			return (a > b);
		}

		/// <summary>
		/// Determina si el primer parámetro es estrictamente menor que el segundo.
		/// </summary>
		/// <param name="a">
		/// Primer valor decimal.
		/// </param>
		/// <param name="b">
		/// Segundo valor decimal.
		/// </param>
		/// <returns>
		/// Devuelve <b>true</b> si se cumple que "a" es menor estricto que "b", en caso contrario devuevle <b>false</b>.
		/// </returns> 
		public static bool MenorEstricto (double a, double b)
		{
			return (a < b);
		}

		/// <summary>
		/// Comprueba si el primer parámetro es mayor o igual que el segundo. 
		/// </summary>
		/// <param name="a">
		/// Primer valor decimal.
		/// </param>
		/// <param name="b">
		/// Segundo valor decimal.
		/// </param>
		/// <returns>
		/// Devuelve <b>true</b> si "a" es mayor o igual que "b", en caso contrario devuelve <b>false</b>. 
		/// </returns>
		public static bool MayorOrEqual (double a, double b)
		{
			return (a > b || isEqualCero(a - b));
		}

		/// <summary>
		/// Comprueba si el parámetro 'a' es menor o igual que 'b'. 
		/// </summary>
		/// <param name="a">
		/// Primer valor decimal.
		/// </param>
		/// <param name="b">
		/// Segundo valor decimal.
		/// </param>
		/// <returns>
		/// Devuelve <b>true</b> si "a" es menor o igual que "b", en caso contrario devuelve <b>false</b>.
		/// </returns>
		public static bool MenorOrEqual (double a, double b)
		{
			return (a < b || isEqualCero(a - b));
		}

		/// <summary>
		/// Comprueba si dos puntos dados "P1" y "P2" son iguales. 
		/// </summary>
		/// <param name="P1">
		/// Primer punto.
		/// </param>
		/// <param name="P2">
		/// Segungo punto.
		/// </param>
		/// <returns>
		/// Devuelve <b>true</b> si ambas puntos son iguales, en caso contrario devuelve <b>false</b>.
		/// </returns>
		/// <seealso cref="PointType"/>
		public static bool isEqualPoint (PointType P1, PointType P2)
		{
			if (P1 == null || P2 == null) return false;

			return (isEqualValues(P1.cX, P2.cX) &&
							isEqualValues(P1.cY, P2.cY) &&
							isEqualValues(P1.cZ, P2.cZ));
		}

		/// <summary>
		/// Comprueba si dos Segmentos dado "S1" y "S2" son iguales. 
		/// </summary>
		/// <param name="S1">
		/// Primer segmento.
		/// </param>
		/// <param name="S2">
		/// Segundo segmento.
		/// </param>
		/// <returns>
		/// Devuelve <b>true</b> si ambos segmentos son iguales, en caso contrario devuelve <b>false</b>.
		/// </returns>
		/// <seealso cref="SegmentType"/>
		public static bool isEqualSegment (SegmentType S1, SegmentType S2)
		{
			if (S1 == null || S2 == null) return false;

			return (isEqualPoint(S1.StartPoint, S2.StartPoint) && isEqualPoint(S1.EndPoint, S2.EndPoint));
		}

		/// <summary>
		/// Comprueba si dos circunferencias dadas "C1" y "C2" son iguales. 
		/// </summary>
		/// <param name="C1">
		/// Primera circunferencia.
		/// </param>
		/// <param name="C2">
		/// Segunda circunferencia.
		/// </param>
		/// <returns>
		/// Devuelve <b>true</b> si ambos circulos son iguales, en caso contrario devuelve <b>false</b>.
		/// </returns>
		/// <seealso cref="CircleType"/>
		public static bool isEqualCircle (CircleType C1, CircleType C2)
		{
			if (C1 == null || C2 == null) return false;

			return (isEqualPoint(C1.Center, C2.Center) && isEqualCero(C1.Radius - C2.Radius));
		}

		/// <summary>
		/// Comprueba si dos arcos dados "A1" y "A2" son iguales. 
		/// </summary>
		/// <param name="A1">
		/// Primer arcos.
		/// </param>
		/// <param name="A2">
		/// Segundo arco.
		/// </param>
		/// <returns>
		/// Devuelve <b>true</b> si ambos arcos son iguales. En caso contrario devuelve <b>false</b>.
		/// </returns>
		/// <seealso cref="ArcType"/>
		public static bool isEqualArc (ArcType A1, ArcType A2)
		{
			if (A1 == null || A2 == null) return false;

			return isEqualPoint(A1.Center, A2.Center) &&
			       isEqualValues(A1.Radius, A2.Radius) &&
			       isEqualValues(A1.StartAngle, A2.StartAngle) &&
			       isEqualValues(A1.EndAngle, A2.EndAngle);
		}

		/// <summary>
		/// Intercambia los dos valores pasados por parámetro. 
		/// </summary>
		/// <param name="a">
		/// Parámetro de salida (ref). Primer valor decimal.
		/// </param>
		/// <param name="b">
		/// Parámetro de salida (ref). Segundo valor decimal.
		/// </param>
		/// <returns>
		/// La función retorna un tipo void. Por referencia quedan intercambiados ambos valores.
		/// </returns>
		public static void SwapValue (ref double a, ref double b)
		{
			double k;

			k = a;
			a = b;
			b = k;
		}

		/// <summary>
		/// Intercambia los dos puntos pasados por parámetro. 
		/// </summary>
		/// <param name="P1">
		/// Parámetro de salida (ref). Primer punto.
		/// </param>
		/// <param name="P2">
		/// Parámetro de salida (ref). Segundo punto.
		/// </param>
		/// <returns>
		/// La función retorna un tipo void. Por referencia quedan intercambiados ambos puntos.
		/// </returns>
		/// <seealso cref="PointType"/>
		public static void SwapPoint (ref PointType P1, ref PointType P2)
		{
			PointType aux;

			aux = P1;
			P1 = P2;
			P2 = aux;
		}

		/// <summary>
		/// Intercambia los dos segmentos pasados por parámetro. 
		/// </summary>
		/// <param name="S1">
		/// Parámetro de salida (ref). Primer segmento.
		/// </param>
		/// <param name="S2">
		/// Parámetro de salida (ref). Segundo segmento.
		/// </param>
		/// <returns>
		/// La función retorna un tipo void. Por referencia quedan intercambiados ambos segmentos. 
		/// </returns>
		/// <seealso cref="SegmentType"/>
		public static void SwapSegment (ref SegmentType S1, ref SegmentType S2)
		{
			SegmentType aux;

			aux = S1;
			S1 = S2;
			S2 = aux;
		}

		/// <summary>
		/// Intercambia las dos líneas pasados por parámetro. 
		/// </summary>
		/// <param name="L1">
		/// Parámetro de salida (ref). Primera línea.
		/// </param>
		/// <param name="L2">
		/// Parámetro de salida (ref). Segunda línea.
		/// </param>
		/// <returns>
		/// La función retorna un tipo void. Por referencia quedan intercambiados ambas lineas.
		/// </returns>
		/// <seealso cref="LineType"/>
		public static void SwapLine (ref LineType L1, ref LineType L2)
		{
			LineType aux;

			aux = L1;
			L1 = L2;
			L2 = aux;
		}

		/// <summary>
		/// Intercambia las dos circunferencias pasados por parámetro. 
		/// </summary>
		/// <param name="C1">
		/// Parámetro de salida (ref). Primera circunferencia.
		/// </param>
		/// <param name="C2">
		/// Parámetro de salida (ref). Segunda circunferencia.
		/// </param>
		/// <returns>
		/// La función retorna un tipo void. Por referencia quedan intercambiados ambos circulos.
		/// </returns>
		/// <seealso cref="CircleType"/>
		public static void SwapCircle (ref CircleType C1, ref CircleType C2)
		{
			CircleType aux;

			aux = C1;
			C1 = C2;
			C2 = aux;
		}

		/// <summary>
		/// Intercambia los dos arcos pasados por parámetro.
		/// </summary>
		/// <param name="A1">
		/// Parámetro de salida (ref). Primer arco.
		/// </param>
		/// <param name="A2">
		/// Parámetro de salida (ref). Segundo arco.
		/// </param>
		/// <returns>
		/// La función retorna un tipo void. Por referencia quedan intercambiados ambos arcos.
		/// </returns>
		/// <seealso cref="ArcType"/>
		public static void SwapArc (ref ArcType A1, ref ArcType A2)
		{
			ArcType aux;

			aux = A1;
			A1 = A2;
			A2 = aux;
		}
		#endregion

		#region - Coordinates System Functions -
		/// <summary>
		/// Convierte coordenadas Polares a coordenadas Cartesianas.
		/// </summary>
		/// <param name="angle">
		/// Representa el valor del ángulo dado en grados en el sistema de coordenadas polares.
		/// </param>
		/// <param name="dist">
		/// Representa el valor de distancia (radio) en el sistema de coordenadas polares.
		/// </param>
		/// <param name="dX">
		/// Parámetro de salida (out). Retorna por referencia la componente dX del punto.
		/// </param>
		/// <param name="dY">
		/// Parámetro de salida (out). Retorna por referencia la componente dY del punto.
		/// </param>
		/// <param name="pbase">
		/// Define el punto base que se usa como origen para calcular las componentes dX y dY. Si "pbase" es igual a null
		/// se asume como punto base el Origen del Sistema de Coordenadas (0,0,0).
		/// </param>
		/// <returns>
		/// Devuelve un tipo void. Por referencia se devuelve las componentes dX y dY.
		/// </returns>
		/// <seealso cref="PointType"/> 
		public static void PolarToCartesian (double angle, double dist, out double dX, out double dY, PointType pbase = null)
		{
			double cos, sin;

			dist = Math.Abs(dist);

			cos = Math.Cos(GradToRad(angle));
			sin = Math.Sin(GradToRad(angle));
			cos = (isEqualCero(cos)) ? 0.0 : cos;
			sin = (isEqualCero(sin)) ? 0.0 : sin;

			if (pbase == null)
			{
				dX = dist * cos;
				dY = dist * sin;
			}
			else
			{
				dX = pbase.cX + dist * cos;
				dY = pbase.cY + dist * sin;
			}
		}

		/// <summary>
		/// Convierte coordenadas Cartesianas a coordenadas Polares. 
		/// </summary>
		/// <param name="dX">
		/// Coordena X en el sistema cartesiano. Tambien se interpreta como desplazamiento a partir del punto "pbase" sobre
		/// el eje de las abcisas (Eje X).
		/// </param>
		/// <param name="dY">
		/// Coordena Y en el sistema cartesiano. Tambien se interpreta como desplazamiento a partir del punto "pbase" sobre
		/// el eje de las ordenadas (Eje Y).
		/// </param>
		/// <param name="angle">
		/// Parámetro de salida (out). Retorna por referencia la componente angular del sistema de coordenadas polares.
		/// </param>
		/// <param name="dist">
		/// Parámetro de salida (out). Retorna por referencia la componente distacia del sistema de coordenadas polares.
		/// </param>
		/// <param name="pbase">
		/// Define el punto base que se usa como origen para calcular las componentes angular y distancia. Si "pbase" es igual a null
		/// se asume como punto base el Origen del Sistema de Coordenadas (0,0,0). 
		/// </param>
		/// <returns>
		/// Devuelve un tipo void. Por referencia se devuelve las componentes angular y distancia del Sistema de Coordenadas Polares.
		/// </returns>
		/// <seealso cref="PointType"/>
		public static void CartesianToPolar (double dX, double dY, out double angle, out double dist, PointType pbase = null)
		{
			if (pbase == null)
			{
				dist = Math.Sqrt(dX * dX + dY * dY);
				angle = (!isEqualCero(dist)) ? RadToGrad(Math.Asin(dY / dist)) : 0;
			}
			else
			{
				angle = 0;
				dist = 0;
			}
		}

		/// <summary>
		/// Ubica un punto 2D mediante coordenadas polares partir de un punto base, un ángulo y una distancia.  
		/// </summary>
		/// <param name="pbase">
		/// Punto de referencia a partir del cuál se calcula el nuevo punto mediante el sistema de coordenadas polares.
		/// </param>
		/// <param name="angle">
		/// Componente angular para el sistema de coordenadas polares.
		/// </param>
		/// <param name="distance">
		/// Componente distancia para el sistema de coordenadas polares.
		/// </param>
		/// <returns>
		/// Devuelve el punto ubicado por coordenadas polares.
		/// </returns>
		/// <seealso cref="PointType"/>
		public static PointType PolarPoint (PointType pbase, double angle, double distance)
		{
			double cos, sin;

			angle = NormalizeAngle(angle);
			distance = Math.Abs(distance);

			cos = Math.Cos(GradToRad(angle));
			sin = Math.Sin(GradToRad(angle));
			cos = (isEqualCero(cos)) ? 0.0 : cos;
			sin = (isEqualCero(sin)) ? 0.0 : sin;

			return new PointType(pbase.cX + distance * cos, pbase.cY + distance * sin, pbase.cZ);
		}

		/// <summary>
		/// Ubica un punto mediante coordenadas cilíndricas. 
		/// </summary>
		/// <param name="pbase">
		/// 
		/// </param>
		/// <returns>
		/// 
		/// </returns>
		/// <seealso cref="PointType"/>
		public static PointType CilindricalPoint (PointType pbase)
		{
			throw new NotImplementedException();			
		}

		/// <summary>
		/// Ubica un punto 3D mediante coordenadas esféricas. 
		/// </summary>
		/// <param name="pbase">
		/// 
		/// </param>
		/// <returns>
		/// 
		/// </returns>
		/// <seealso cref="PointType"/>
		public static PointType SphericalPoint (PointType pbase)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region - 2D Primitives Operations -
		/// <summary>
		/// Traslada el punto 'P' las distancias definas por las componentes dx, dy, dz. 
		/// </summary>
		/// <param name="P">
		/// Representa el punto a trasladar.
		/// </param>
		/// <param name="dx">
		/// Representa la componente de la translacion sobre el eje X.
		/// </param>
		/// <param name="dy">
		/// Representa la componente de la translacion sobre el eje Y.
		/// </param>
		/// <param name="dz">
		/// Representa la componente de la translacion sobre el eje Z.
		/// </param>
		/// <returns>
		/// Devuelve el punto resultante de la translacion sobre las componentes dx, dy, dz. 
		/// </returns>
		/// <seealso cref="PointType"/>
		public static PointType TranslatePoint (PointType P, double dx, double dy = 0.0, double dz = 0.0)
		{
			PointType newP = new PointType();

			if (P == null) throw new NullReferenceException();

			newP.cX = P.cX + dx;
			newP.cY = P.cY + dy;
			newP.cZ = P.cZ + dz;

			return newP;
		}

		/// <summary>
		/// Traslada el segmento 'S' las distancias definas por las componentes dx, dy, dz.
		/// </summary>
		/// <param name="S">
		/// Representa el segmento a trasladar.
		/// </param>
		/// <param name="dx">
		/// Representa la componente de la translacion sobre el eje X.
		/// </param>
		/// <param name="dy">
		/// Representa la componente de la translacion sobre el eje Y.
		/// </param>
		/// <param name="dz">
		/// Representa la componente de la translacion sobre el eje Z.
		/// </param>
		/// <returns>
		/// Devuelve el segmento resultante de la translacion sobre las componentes dx, dy, dz. 
		/// </returns>
		/// <seealso cref="SegmentType"/>
		public static SegmentType TranslateSegment (SegmentType S, double dx, double dy = 0.0, double dz = 0.0)
		{
			SegmentType newS = new SegmentType();

			newS.StartPoint = TranslatePoint(S.StartPoint, dx, dy, dz);
			newS.EndPoint = TranslatePoint(S.EndPoint, dx, dy, dz);

			return newS;
		}

		/// <summary>
		/// Traslada el circulo 'C' las distancias definas por las componentes dx, dy, dz.
		/// </summary>
		/// <param name="C">
		/// Representa el circulo a trasladar.
		/// </param>
		/// <param name="dx">
		/// Representa la componente de la translacion sobre el eje X.
		/// </param>
		/// <param name="dy">
		/// Representa la componente de la translacion sobre el eje Y.
		/// </param>
		/// <param name="dz">
		/// Representa la componente de la translacion sobre el eje Z.
		/// </param>
		/// <returns>
		/// Devuelve el circulo resultante de la translacion sobre las componentes dx, dy, dz. 
		/// </returns>
		/// <seealso cref="CircleType"/>
		public static CircleType TranslateCircle (CircleType C, double dx, double dy = 0.0, double dz = 0.0)
		{
			C.Center = TranslatePoint(C.Center, dx, dy, dz);

			return C;
		}

		/// <summary>
		/// Traslada el arco 'A' las distancias definas por las componentes dx, dy, dz. 
		/// </summary>
		/// <param name="A">
		/// Representa el arco a trasladar. 
		/// </param>
		/// <param name="dx">
		/// Representa la componente de la translacion sobre el eje X. 
		/// </param>
		/// <param name="dy">
		/// Representa la componente de la translacion sobre el eje Y. 
		/// </param>
		/// <param name="dz">
		///  Representa la componente de la translacion sobre el eje Z.
		/// </param>
		/// <returns>
		/// Devuelve el arco resultante de la translacion sobre las componentes dx, dy, dz. 
		/// </returns>
		/// <seealso cref="ArcType"/>
		public static ArcType TranslateArc (ArcType A, double dx, double dy = 0.0, double dz = 0.0)
		{
			A.Center = TranslatePoint(A.Center, dx, dy, dz);

			return A;
		}

		/// <summary>
		/// Rota en el plano XY el punto 'P' sobre un punto 'pbase' dado según un ángulo especificado. 
		/// </summary>
		/// <param name="P">
		/// Representa el punto a rotar.
		/// </param>
		/// <param name="angle">
		/// Representa el ángulo de rotacion.
		/// </param>
		/// <param name="pbase">
		/// Representa el punto sobre el que se hará la rotacion.
		/// </param>
		/// <remarks>
		/// Nota: Si se omite parámetro 'pbase' se asume que la rotacion se hace sobre el Origen de Coordenadas [0; 0]
		/// </remarks>
		/// <returns>
		/// Devuelve el punto resultante de la rotación.
		/// </returns>
		/// <seealso cref="PointType"/>
		public static PointType RotatePoint (PointType P, double angle, PointType pbase = null)
		{
			double dist, angA, angB;
			PointType newP = new PointType();

			angle = NormalizeAngle(angle);
			pbase = pbase ?? _Origen_;

			//--------------
			// La Rotacion se realiza sobre el Origen de Coordenadas (0.0, 0.0, 0.0).
			//--------------
			if (isEqualPoint(pbase, _Origen_))
			{
				double cos, sin;

				double angPOrigen = PointPointAngle(_Origen_, P);
				dist = PointPointDistance(_Origen_, P);

				if (MenorEstricto(angle, 0.0)) /* Rotacion en sentido HORARIO:  (angulo NEGATIVO) */
				{
					angA = angPOrigen - Math.Abs(angle);

					if (MayorOrEqual(angA, 0.0))
					{
						cos = Math.Cos(GradToRad(angA));
						sin = Math.Sin(GradToRad(angA));
					}
					else
					{
						angB = 360.0 - Math.Abs(angA);
						cos = Math.Cos(GradToRad(angB));
						sin = Math.Sin(GradToRad(angB));
					}

					newP.cX = dist * cos;
					newP.cY = dist * sin;
					newP.cZ = P.cZ;
				}
				else /* Rotacion en sentido ANTI-HORARIO: (angulo POSITIVO) */
				{
					angA = angPOrigen + Math.Abs(angle);

					if (MenorEstricto(angA, 360.0))
					{
						cos = Math.Cos(GradToRad(angle));
						sin = Math.Sin(GradToRad(angle));

						newP.cX = P.cX * cos - P.cY * sin;
						newP.cY = P.cX * sin + P.cY * cos;
						newP.cZ = P.cZ;
					}
					else
					{
						angB = Math.Abs(angA) - 360.0;
						cos = Math.Cos(GradToRad(angB));
						sin = Math.Sin(GradToRad(angB));

						newP.cX = dist * cos;
						newP.cY = dist * sin;
						newP.cZ = P.cZ;
					}
				}
			}
			//--------------
			// la Rotacion se realiza sobre un punto arbitrario P.
			//--------------
			else
			{
				dist = PointPointDistance(pbase, P);

				if (MenorEstricto(angle, 0.0)) /* Rotacion en sentido HORARIO: (angulo NEGATIVO) */
				{
					angA = PointPointAngle(pbase, P) - Math.Abs(angle);

					if (MayorOrEqual(angA, 0.0))
					{
						newP = PolarPoint(pbase, angA, dist);
					}
					else
					{
						angB = 360.0 - Math.Abs(angA);
						newP = PolarPoint(pbase, angB, dist);
					}
				}
				else /* Rotacion en sentido ANTI-HORARIO (angulo POSITIVO) */
				{
					angA = PointPointAngle(pbase, P) + Math.Abs(angle);

					if (MenorEstricto(angA, 360.0))
					{
						newP = PolarPoint(pbase, angA, dist);
					}
					else
					{
						angB = Math.Abs(angA) - 360.0;
						newP = PolarPoint(pbase, angB, dist);
					}
				}
			}

			return newP;
		}

		/// <summary>
		/// Rota en el plano XY el segmento 'S' sobre un punto 'pbase' dado según un ángulo especificado. 
		/// </summary>
		/// <param name="S">
		/// Representa el segmento a rotar.
		/// </param>
		/// <param name="angle">
		/// Representa el ángulo de rotacion.
		/// </param>
		/// <param name="pbase">
		/// Representa el punto sobre el que se hará la rotacion.
		/// </param>
		/// <remarks>
		/// Nota: Si se omite parámetro 'pbase' se asume que la rotacion se hace sobre el Origen de Coordenadas [0; 0]
		/// </remarks>
		/// <returns>
		/// Devuelve el segmento resultante de la rotación. 
		/// </returns>
		/// <seealso cref="PointType"/>
		/// <seealso cref="SegmentType"/>
		public static SegmentType RotateSegment (SegmentType S, double angle, PointType pbase = null)
		{
			PointType pi, pf;

			pbase = pbase ?? _Origen_;

			//-----------------------------
			// Se rota el Segmento por el punto StartPoint.
			if (isEqualPoint(pbase, S.StartPoint))
			{
				pi = S.StartPoint;
				pf = RotatePoint(S.EndPoint, angle, S.StartPoint);
			}
				//-----------------------------
				// Se rota el Segmento por el punto EndPoint.
			else if (isEqualPoint(pbase, S.EndPoint))
			{
				pf = S.EndPoint;
				pi = RotatePoint(S.StartPoint, angle, S.EndPoint);
			}
				//-----------------------------
				// Se rota el Segmento por el punto MidPoint.
			else if (isEqualPoint(pbase, S.MidPoint))
			{
				pi = RotatePoint(S.StartPoint, angle, S.MidPoint);
				pf = RotatePoint(S.EndPoint, angle, S.MidPoint);
			}
				//-----------------------------
				// Se rota el Segmento por un punto Arbitrario P.
			else
			{
				pi = RotatePoint(S.StartPoint, angle, pbase);
				pf = RotatePoint(S.EndPoint, angle, pbase);
			}

			return new SegmentType(pi, pf);
		}

		/// <summary>
		/// Rota en el plano XY el circulo 'C' sobre un punto 'pbase' dado según un ángulo especificado. 
		/// </summary>
		/// <param name="C">
		/// Representa el circulo a rotar.
		/// </param>
		/// <param name="angle">
		/// Representa el ángulo de rotacion.
		/// </param>
		/// <param name="pbase">
		/// Representa el punto sobre el que se hará la rotacion.
		/// </param>
		/// <remarks>
		/// Nota: Si se omite parámetro 'pbase' se asume que la rotacion se hace sobre el Origen de Coordenadas [0; 0]
		/// </remarks>
		/// <returns>
		/// Devuelve el circulo resultante de la rotación.  
		/// </returns>
		/// <seealso cref="CircleType"/>
		/// <seealso cref="PointType"/>
		public static CircleType RotateCircle (CircleType C, double angle, PointType pbase = null)
		{
			pbase = pbase ?? _Origen_;

			return new CircleType(RotatePoint(C.Center, angle, pbase), C.Radius);
		}

		/// <summary>
		/// Rota en el plano XY el arco 'A' sobre un punto 'pbase' dado según un ángulo especificado.
		/// </summary>
		/// <param name="C">
		/// Representa el arco a rotar.
		/// </param>
		/// <param name="angle">
		/// Representa el ángulo de rotacion.
		/// </param>
		/// <param name="pbase">
		/// Representa el punto sobre el que se hará la rotacion.
		/// </param>
		/// <remarks>
		/// Nota: Si se omite parámetro 'pbase' se asume que la rotacion se hace sobre el Origen de Coordenadas [0; 0]
		/// </remarks>
		/// <returns>
		/// Devuelve el arco resultante de la rotación. 
		/// </returns>
		/// <seealso cref="CircleType"/>
		/// <seealso cref="PointType"/>
		public static ArcType RotateArc (CircleType C, double angle, PointType pbase = null)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region - Utility General Functions -
		/// <summary>
		/// Determina el menor valor de la coordenada X que contiene el segmento 'S' dado.
		/// </summary>
		/// <param name="S">
		/// Representa el segmento al que se le determinará su menor X.
		/// </param>
		/// <returns>
		/// Devuelve un double que representa el menor valor de la coordenada X del segmento.
		/// </returns>
		/// <seealso cref="SegmentType"/>
		public static double MinXSegment (SegmentType S)
		{
			return (Math.Min(S.StartPoint.cX, S.EndPoint.cX));
		}

		/// <summary>
		/// Determina el mayor valor de la coordenada X que contiene el segmento 'S' dado.
		/// </summary>
		/// <param name="S">
		/// Representa el segmento al que se le determinará su mayor X.
		/// </param>
		/// <returns>
		/// Devuelve un double que representa el mayor valor de la coordenada X del segmento.
		/// </returns>
		/// <seealso cref="SegmentType"/>
		public static double MaxXSegment (SegmentType S)
		{
			return (Math.Max(S.StartPoint.cX, S.EndPoint.cX));
		}

		/// <summary>
		/// Determina el menor valor de la coordenada Y que contiene el segmento 'S' dado.
		/// </summary>
		/// <param name="S">
		/// Representa el segmento al que se le determinará su menor Y.
		/// </param>
		/// <returns>
		/// Devuelve un double que representa el menor valor de la coordenada Y del segmento.
		/// </returns>
		/// <seealso cref="SegmentType"/>
		public static double MinYSegment (SegmentType S)
		{
			return (Math.Min(S.StartPoint.cY, S.EndPoint.cY));
		}

		/// <summary>
		/// Determina el mayor valor de la coordenada Y que contiene el segmento 'S' dado.
		/// </summary>
		/// <param name="S">
		/// Representa el segmento al que se le determinará su mayor Y.
		/// </param>
		/// <returns>
		/// Devuelve un double que representa el mayor valor de la coordenada Y del segmento.
		/// </returns>
		/// <seealso cref="SegmentType"/>
		public static double MaxYSegment (SegmentType S)
		{
			return (Math.Max(S.StartPoint.cY, S.EndPoint.cY));
		}

		/// <summary>
		/// Determina cuál de los puntos "P1" y "P2" dados esta más cercano a "pbase" sobre el eje 
		/// de las abcisas (Eje X).
		/// </summary>
		/// <param name="pbase">
		/// Punto base o de referencia respecto al cuál se calculará la cercanía sobre el eje X.
		/// </param>
		/// <param name="P1">
		/// Primer punto.
		/// </param>
		/// <param name="P2">
		/// Segundo punto.
		/// </param>
		/// <returns>
		/// Devuelve el punto más cercado sobre el eje de las X al punto base dado.
		/// </returns>
		/// <seealso cref="PointType"/>
		public static PointType NearInXAxis (PointType pbase, PointType P1, PointType P2)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Determina cuál de los puntos "P1" y "P2" dados esta más cercano a "pbase" sobre el eje 
		/// de las ordenadas (Eje Y). 
		/// </summary>
		/// <param name="pbase">
		/// Punto base o de referencia respecto al cuál se calculará la cercanía sobre el eje Y.
		/// </param>
		/// <param name="P1">
		/// Primer punto.
		/// </param>
		/// <param name="P2">
		/// Segundo punto.
		/// </param>
		/// <returns>
		/// Devuelve el punto más cercado sobre el eje de las Y al punto base dado.
		/// </returns>
		/// <seealso cref="PointType"/>
		public static PointType NearInYAxis(PointType pbase, PointType P1, PointType P2)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Determina cuál de los puntos "P1" y "P2" dados esta más alejado de "pbase" sobre el eje 
		/// de las abcisas (Eje X). 
		/// </summary>
		/// <param name="pbase">
		/// Punto base o de referencia respecto al cuál se calculará la lejanía sobre el eje X.
		/// </param>
		/// <param name="P1">
		/// Primer punto.
		/// </param>
		/// <param name="P2">
		/// Segundo punto.
		/// </param>
		/// <returns>
		/// Devuelve el punto más alejado sobre el eje de las X al punto base dado.
		/// </returns>
		/// <seealso cref="PointType"/>
		public static PointType FarInXAxis(PointType pbase, PointType P1, PointType P2)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Determina cuál de los puntos "P1" y "P2" dados esta más alejado de "pbase" sobre el eje 
		/// de las ordenadas (Eje Y). 
		/// </summary>
		/// <param name="pbase">
		/// Punto base o de referencia respecto al cuál se calculará la lejanía sobre el eje Y.
		/// </param>
		/// <param name="P1">
		/// Primer punto.
		/// </param>
		/// <param name="P2">
		/// Segundo punto.
		/// </param>
		/// <returns>
		/// Devuelve el punto más alejado sobre el eje de las Y al punto base dado.
		/// </returns>
		/// <seealso cref="PointType"/>
		public static PointType FarInYAxis (PointType pbase, PointType P1, PointType P2)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Determina cuál de los puntos "P1" y "P2" dados está más cercano al origen
		/// de coordenadas [0; 0].
		/// </summary>
		/// <param name="P1">
		/// Primer punto.
		/// </param>
		/// <param name="P2">
		/// Segundo punto.
		/// </param>
		/// <returns>
		/// Devuelve el punto más cercano al origen de coordenadas.
		/// </returns>
		/// <seealso cref="PointType"/>
		public static PointType NearToOrigen (PointType P1, PointType P2)
		{
			double d1, d2;

			d1 = PointPointDistance(_Origen_, P1);
			d2 = PointPointDistance(_Origen_, P2);

			return (MenorOrEqual(d1, d2)) ? P1 : P2;
		}

		/// <summary>
		/// Determina cuál de los puntos "P1" y "P2" dados está más lejano del origen
		/// de coordenadas [0; 0]. 
		/// </summary>
		/// <param name="P1">
		/// Primer punto.
		/// </param>
		/// <param name="P2">
		/// Segundo punto.
		/// </param>
		/// <returns>
		/// Devuelve el punto más alejado del origen de coordenadas.
		/// </returns>
		/// <seealso cref="PointType"/>
		public static PointType FarToOrigen (PointType P1, PointType P2)
		{
			double d1, d2;

			d1 = PointPointDistance(_Origen_, P1);
			d2 = PointPointDistance(_Origen_, P2);

			return (MayorOrEqual(d1, d2)) ? P1 : P2;
		}

		/// <summary>
		/// Determina cuál de los puntos "P1" y "P2" dados está más cercano al punto "pbase".
		/// </summary>
		/// <param name="pbase">
		/// Punto base o de referencia respecto al cuál se calculará la cercanía.
		/// </param>
		/// <param name="P1">
		/// Primer punto.
		/// </param>
		/// <param name="P2">
		/// Segundo punto.
		/// </param>
		/// <returns>
		/// Devuelve el punto más cercano al punto base dado.
		/// </returns>
		/// <seealso cref="PointType"/>
		public static PointType NearToPoint (PointType pbase, PointType P1, PointType P2)
		{
			double d1, d2;

			d1 = PointPointDistance(pbase, P1);
			d2 = PointPointDistance(pbase, P2);

			return (MenorOrEqual(d1, d2)) ? P1 : P2;
		}

		/// <summary>
		/// Determina cuál de los dos puntos dados "P1" y "P2" está más alejado al punto "pbase". 
		/// </summary>
		/// <param name="pbase">
		/// Punto base o de referencia respecto al cuál se calculará la lejanía.
		/// </param>
		/// <param name="P1">
		/// Primer punto.
		/// </param>
		/// <param name="P2">
		/// Segundo punto.
		/// </param>
		/// <returns>
		/// Devuelve el punto más alejado del punto base dado.
		/// </returns>
		/// <seealso cref="PointType"/>
		public static PointType FarToPoint (PointType pbase, PointType P1, PointType P2)
		{
			double d1, d2;

			d1 = PointPointDistance(pbase, P1);
			d2 = PointPointDistance(pbase, P2);

			return (MayorOrEqual(d1, d2)) ? P1 : P2;
		}

		/// <summary>
		/// Determina que punto de una lista de puntos dada esta más cercano al punto "pbase".
		/// </summary>
		/// <param name="pbase">
		/// Punto base o de referencia respecto al cuál se calculará la carcanía.
		/// </param>
		/// <param name="list">
		/// Lista de puntos a comprobar.
		/// </param>
		/// <returns>
		/// Devuelve el punto más cercano al punto "pbase".
		/// </returns>
		/// <seealso cref="PointType"/>
		public static PointType NearToPoint (PointType pbase, List <PointType> list)
		{
			int i;
			double dist;
			PointType near;

			i = 0;
			near = list[0];
			dist = PointPointDistance(pbase, near);

			foreach (var p in list)
			{
				if (i == 0)
				{
					i++;
					continue;
				}

				double d = PointPointDistance(pbase, p);

				if (MenorEstricto(d, dist))
				{
					near = p;
					dist = d;
				}

				i++;
			}

			return near;
		}

		/// <summary>
		/// Determina de una lista de puntos definidas por "list" el punto más alejado del
		/// punto base "pbase".
		/// </summary>
		/// <param name="pbase">
		/// Punto base o de referencia respecto al cuál se calculará la lejanía.
		/// </param>
		/// <param name="list">
		/// Lista de puntos a comprobar.
		/// </param>
		/// <returns>
		/// Devuelve el punto más lejano al punto "pbase".
		/// </returns>
		/// <seealso cref="PointType"/>
		public static PointType FarToPoint (PointType pbase, List <PointType> list)
		{
			int i;
			double dist;
			PointType far;

			i = 0;
			far = list[0];
			dist = PointPointDistance(pbase, far);

			foreach (var p in list)
			{
				if (i == 0)
				{
					i++;
					continue;
				}

				double d = PointPointDistance(pbase, p);

				if (MayorEstricto(d, dist))
				{
					far = p;
					dist = d;
				}

				i++;
			}

			return far;
		}
		#endregion

		#region - Functions of Analytical's Calculation  -
		/// <summary>
		/// Calcula la pendiente de la linea que pasa por los puntos "P1" y "P2" dados.
		/// </summary>
		/// <param name="P1">
		/// Representa uno de los puntos por donde pasa la línea a la que se le quiere determinar su
		/// de pendiente.
		/// </param>
		/// <param name="P2">
		/// Representa el otro punto por donde pasa la línea.
		/// </param>
		/// <returns>
		/// Devuelve un double que expresa el valor de la pendiente de la recta. Devuelve un valor NaN (Not a Number) 
		/// si ambos puntos tienen iguales la coordenada X, lo que significa que la recta es vertical (es decir, es 
		/// paralela al eje de las ordenadas). 
		/// </returns>
		/// <seealso cref="PointType"/>
		public static double PointPointSlope (PointType P1, PointType P2)
		{
			return (!isEqualValues(P1.cX, P2.cX)) ? (P2.cY - P1.cY) / (P2.cX - P1.cX) : double.NaN;
		}

		/// <summary>
		/// Calcula la distancia entre los puntos "P1" y "P2" dados.
		/// </summary>
		/// <param name="P1">
		/// Primer punto.
		/// </param>
		/// <param name="P2">
		/// Segundo punto.
		/// </param>
		/// <returns>
		/// Devuelve un double que expresa el valor de la distancia entre dos puntos.
		/// </returns>
		/// <seealso cref="PointType"/>
		public static double PointPointDistance (PointType P1, PointType P2)
		{
			return (Math.Sqrt((P1.cX - P2.cX) * (P1.cX - P2.cX) +
				                (P1.cY - P2.cY) * (P1.cY - P2.cY) +
				                (P1.cZ - P2.cZ) * (P1.cZ - P2.cZ)));
		}

		/// <summary>
		/// Determina la distancia un punto "P" a una línea "L" dados.
		/// </summary>
		/// <param name="P">
		/// Representa el punto.
		/// </param>
		/// <param name="L">
		/// Representa la línea.
		/// </param>
		/// <returns>
		/// Devuelve la distancia de un punto a una línea.
		/// </returns>
		/// <seealso cref="PointType"/>
		public static double PointLineDistance (PointType P, LineType L)
		{
			double A, B, C;

			LineCoefficient(L, out A, out B, out C);

			return Math.Abs(A * P.cX + B * P.cY + C) / Math.Sqrt(A * A + B * B);
		}

		/// <summary>
		/// Determina el punto medio o punto equidistante entre 2 puntos "P1" y "P2" dados.
		/// </summary>
		/// <param name="P1">
		/// Primer punto.
		/// </param>
		/// <param name="P2">
		/// Segundo punto.
		/// </param>
		/// <returns>
		/// Devuelve un punto que es equidistante a los puntos "P1" y "P2" dados. 
		/// </returns>
		/// <seealso cref="PointType"/>
		public static PointType MidPointBetweenPoint (PointType P1, PointType P2)
		{
			double cX, cY, cZ;

			cX = (P1.cX > P2.cX) ? P2.cX + ((P1.cX - P2.cX) / 2.0) : P1.cX + ((P2.cX - P1.cX) / 2.0);
			cY = (P1.cY > P2.cY) ? P2.cY + ((P1.cY - P2.cY) / 2.0) : P1.cY + ((P2.cY - P1.cY) / 2.0);
			cZ = (P1.cZ > P2.cZ) ? P2.cZ + ((P1.cZ - P2.cZ) / 2.0) : P1.cZ + ((P2.cZ - P1.cZ) / 2.0);

			return new PointType(cX, cY, cZ);
		}

		/// <summary>
		/// Determina el valor del ángulo que se forma entre segmento que describen los puntos "P1" y "P2" 
		/// dados y el eje de las abcisas (Eje X).
		/// </summary>
		/// <param name="P1">
		/// Primer punto.
		/// </param>
		/// <param name="P2">
		/// Segundo punto.
		/// </param>
		/// <returns>
		/// Devuelve el ángulo en grados del segmento que se forma entre los punto "P1" y "P2". 
		/// </returns>
		/// <seealso cref="PointType"/>
		public static double PointPointAngle (PointType P1, PointType P2)
		{
			double difX, difY, angle;

			difY = P2.cY - P1.cY;
			difX = P2.cX - P1.cX;

			if (isEqualCero(difY))
			{
				if (difX > 0.0 || isEqualCero(difX))
					angle = 0.0;
				else
					angle = 180.0;
			}
			else
			{
				if (isEqualCero(difX))
				{
					angle = (difY > 0.0) ? 90.0 : 270.0;
				}
				else
				{
					double ang = Math.Atan(difY / difX) * 180.0 / Math.PI;

					if (difX < 0.0) ang += 180.0;

					if (difY < 0.0 && ang < 0.0) ang += 360.0;

					angle = NormalizeAngle(ang);
				}
			}

			return angle;
		}

		/// <summary>
		/// Determina el ángulo según su tipo (agudo u obtuso) que se forma entre dos líneas "L1" y "L2" dadas.
		/// </summary>
		/// <param name="L1">
		/// Primera línea.
		/// </param>
		/// <param name="L2">
		/// Segunda línea.
		/// </param>
		/// <param name="agude">
		/// Indica el tipo de ángulo que se quiere calcular. Si es <b>true</b> se calcula el valor
		/// para el ángulo agudo, por el contrario si es <b>false</b> se calcula el valor del angulo obtuso.
		/// </param>
		/// <returns>
		/// Devuelve el ángulo en grados (agudo u obtuso) que se forma entre las líneas dadas.
		/// </returns>
		/// <seealso cref="LineType"/>
		public static double LineLineAngle (LineType L1, LineType L2, bool agude = true)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Determina el ángulo según su tipo (agudo u obtuso) que se forma entre dos segmentos "S1" y "S2" dadas.
		/// </summary>
		/// <param name="S1">
		/// Primer segmento.
		/// </param>
		/// <param name="S2">
		/// Segundo segmento.
		/// </param>
		/// <param name="agude">
		/// Indica el tipo de ángulo que se quiere calcular. Si es <b>true</b> se calcula el valor
		/// para el ángulo agudo, por el contrario si es <b>false</b> se calcula el valor del angulo obtuso. 
		/// </param>
		/// <returns>
		/// Devuelve el ángulo en grados (agudo u obtuso) que se forma entre los segmentos dados.
		/// </returns>
		/// <seealso cref="SegmentType"/>
		public static double SegmentSegmentAngle (SegmentType S1, SegmentType S2, bool agude = true)
		{
			bool intersect;
			PointType P0, P1, P2;
			double angAgudo, angObtuso, ang1, ang2;

			intersect = SegmentsApparentIntersect(S1, S2, out P0);

			if (!intersect) return 0.0;

			P1 = (isEqualPoint(S1.StartPoint, P0)) ? S1.EndPoint : S1.StartPoint;
			P2 = (isEqualPoint(S2.StartPoint, P0)) ? S2.EndPoint : S2.StartPoint;

			// Nota: se hace la suposicion de que el Origen del UCS esta sobre P0 y que ang1 < ang2;
			ang1 = PointPointAngle(P0, P1);
			ang2 = PointPointAngle(P0, P2);

			if (ang1 > ang2) SwapValue(ref ang1, ref ang2);

			// Si el punto de interseccion no pertenece a ninguno de los segmentos
			// o pertenece al mismo tiempo a un punto extremo de las dos lineas.
			if ((!isEqualPoint(P0, S1.StartPoint) && !isEqualPoint(P0, S1.EndPoint) && !isEqualPoint(P0, S2.StartPoint) && !isEqualPoint(P0, S2.EndPoint) &&
						!PointInSegment(P0, S1) && !PointInSegment(P0, S2)) ||
				  ((isEqualPoint(P0, S1.StartPoint) || isEqualPoint(P0, S1.EndPoint)) && (isEqualPoint(P0, S2.StartPoint) || isEqualPoint(P0, S2.StartPoint))))
			{
				angAgudo = ang2 - ang1;
				angObtuso = 360 - angAgudo;
			}
			else
			{
				angAgudo = ang2 - ang1;

				if (angAgudo > 180.0) angAgudo -= 180.0;

				angObtuso = 180 - angAgudo;
			}

			if (angAgudo > angObtuso) SwapValue(ref angAgudo, ref angObtuso);

			return Math.Abs((agude) ? angAgudo : angObtuso);
		}

		/// <summary>
		/// Determina una línea que es perpendicular a la línea "L" dada y que pasa por el punto "P". 
		/// </summary>
		/// <param name="L">
		/// Línea a partir de la cual se determina la otra línea que es perpendicular a esta.
		/// </param>
		/// <param name="P">
		/// Punto por el que pasa la línea perpendicar a calcular.
		/// </param>
		/// <returns>
		/// Devuelve un linea que es perperdicular a la linea "L" dada y que pasa por el punto "P".
		/// </returns>
		/// <seealso cref="LineType"/>
		/// <seealso cref="PointType"/>
		public static LineType PerperdicularLineAt (LineType L, PointType P)
		{
			return new LineType(P, L.Angle + 90.0);
		}

		/// <summary>
		/// Determina una línea que es paralela a la línea "L" dada y que pasa por el punto "P".
		/// </summary>
		/// <param name="L">
		/// Línea a partir de la cual se determina la otra línea que es paralela a esta.
		/// </param>
		/// <param name="P">
		/// Punto por el que pasa la línea paralela a calcular.
		/// </param>
		/// <returns>
		/// Devuelve un línea que es paralela a la linea "L" dada y que pasa por el punto "P".
		/// </returns>
		/// <seealso cref="LineType"/>
		/// <seealso cref="PointType"/>
		public static LineType ParallelLineAt (LineType L, PointType P)
		{
			return new LineType(P, L.Angle);
		}

		/// <summary>
		/// Determmina una línea que pasa por el punto "P" y que forma con la línea "L" dada, un
		/// ángulo determinado por el parámetro "angle".
		/// </summary>
		/// <param name="L">
		/// Línea a partir de la cual se determina la otra línea que forma con esta un angulo determinado.
		/// </param>
		/// <param name="P">
		/// Punto por el que pasa la línea oblicua a calcular.
		/// </param>
		/// <param name="angle">
		/// Valor de ángulo en grados.
		/// </param>
		/// <returns>
		/// Devuelve un línea que pasa por el punto "P" y que es oblicua a la linea "L" formando un ángulo determinado.
		/// </returns>
		/// <seealso cref="LineType"/>
		/// <seealso cref="PointType"/>
		public static LineType LineAccordingLineAt (LineType L, PointType P, double angle)
		{
			return new LineType(P, L.Angle + NormalizeAngle(angle));
		}

		/// <summary>
		/// Determina las rectas tangentes a la circunferecia "C" dada y que pasan por el punto "P".
		/// </summary>
		/// <param name="C">
		/// Circunferencia para que le se quieren calcular las rectas tangentes y que pasan por el punto dado.
		/// </param>
		/// <param name="P">
		/// Punto por el que pasan las rectas tangentes a la circunferencia.
		/// </param>
		/// <param name="L1">
		/// Parámetro de salida (out). Retorna por referencia la primera de las líneas tangentes a la circunfenrencia.
		/// </param>
		/// <param name="L2">
		/// Parámetro de salida (out). Retorna por referencia la segunda de las líneas tangentes a la circunfenrencia.
		/// </param>
		/// <returns>
		/// Devuelve un tipo void. Por referencia devuelve las dos lineas que cumplen 
		/// con la propiedad de pasar por el punto dado y ser tangente a la circunferecia.
		/// </returns>
		/// <seealso cref="CircleType"/>
		/// <seealso cref="LineType"/>
		/// <seealso cref="PointType"/>
		public static void LineTangentToCircle (CircleType C, PointType P, out LineType L1, out LineType L2)
		{
			double dist;

			//-------
			// Paso 1: Verifico que el punto no sea interior a la circunferencia, condicion para 
			//				 la cuál no se puede encontrar una recta que sea tangente a la circunferencia
			//				 y que pase por el punto P.
			dist = PointPointDistance(C.Center, P);
			if (MenorEstricto(dist, C.Radius) && !PointInCircle(P, C))
			{
				L1 = L2 = null;	
			}
			//-------
			// Paso 2: Obtengo la línea que es tangente a la circunferencia y que pasa sobre un punto 
			//				 que pertenece a esta ultima.
			else if (PointInCircle(P, C))
			{
				LineType L;
				SegmentType S;

				S = new SegmentType(C.Center, P);
				L = PerperdicularLineAt(S.ConvertToLine(), P);
				L1 = L2 = L;
			}
			//-------
			// Paso 3: Si el punto es exterior a la circunferencia obtengo las dos rectas que pasan
			//				 por el punto P y son tangentes a la circunferencia.	
			else
			{
				double alfa, angle;

				dist = PointPointDistance(P, C.Center);
				angle = PointPointAngle(P, C.Center);
				alfa = RadToGrad(Math.Asin(C.Radius / dist));

				L1 = new LineType(P, angle - alfa);
				L2 = new LineType(P, angle + alfa);			
			}
		}

		/// <summary>
		/// Determina la circunferencia que tiene su centro en "P" y para la cual la línea "L" dada es tangente.
		/// </summary>
		/// <param name="L">
		/// Línea que es tangente a la circunferencia calculada.
		/// </param>
		/// <param name="P">
		/// Punto centro de la circunferencia.
		/// </param>
		/// <returns>
		/// Devuelve una circunferencia que tiene su centro en el punto "P" y que es tangente a la línea dada.
		/// </returns> 
		/// <seealso cref="LineType"/>
		/// <seealso cref="PointType"/>
		public static CircleType CircleTangentToLine (LineType L, PointType P)
		{
			return new CircleType(P, PointLineDistance(P, L));
		}

		/// <summary>
		/// Determina si el punto "P" dado pertenece la línea "L".
		/// </summary>
		/// <param name="P">
		/// Punto que se quiere comprobar si pertenece a la línea dada.
		/// </param>
		/// <param name="L">
		/// Línea a la que se quiere comprobar la pertenencia de un punto.
		/// </param>
		/// <returns>
		/// Devuelve <b>true</b> si el punto dado pertenece a la línea. En caso contrario devuelve <b>false</b>.
		/// </returns>
		/// <seealso cref="LineType"/>
		/// <seealso cref="PointType"/>
		public static bool PointInLine (PointType P, LineType L)
		{
			double A, B, C;

			LineCoefficient(L, out A, out B, out C);

			// La pertenencia de un punto a una línea se determina comprobando la 
			// siguiente congruencia. (Ecuacion General de la recta).
			// Ax + By + C  =  0			
			return (isEqualCero(A * P.cX + B * P.cY + C));
		}

		/// <summary>
		/// Determina si el punto "P" dado pertenece al segmento "S".
		/// </summary>
		/// <param name="P">
		/// Punto que se quiere comprobar si pertenece al segmento dada.
		/// </param>
		/// <param name="S">
		/// Segmento al cuál se le quiere comprobar la pertenencia de un punto. 
		/// </param>
		/// <returns>
		/// Devuelve <b>true</b> si el punto dado pertenece al segmento. En caso contrario devuelve <b>false</b>. 
		/// </returns>
		/// <seealso cref="SegmentType"/>
		/// <seealso cref="PointType"/>
		public static bool PointInSegment (PointType P, SegmentType S)
		{
			double f1, f2, slope;

			// La pertenencia de un punto a un segmento se determina comprobando la 
			// siguiente congruencia. (Ecuacion no parametrica de la recta).
			// (Y-Y1) / (Y2-Y1)  =  (X-X1) / (X2-X1)	
			slope = S.Slope;

			// Resuelve el caso de lineas VERTICALES u HORIZONTALES.
			if (isEqualCero(slope) || double.IsNaN(slope))
				return (MayorOrEqual(P.cX, MinXSegment(S)) && MenorOrEqual(P.cX, MaxXSegment(S)) &&
					      MayorOrEqual(P.cY, MinYSegment(S)) && MenorOrEqual(P.cY, MaxYSegment(S)));

			// Resuelve el caso de lineas que OBLICUAS a ambos ejes.
			f1 = (P.cY - S.StartPoint.cY) / (S.EndPoint.cY - S.StartPoint.cY);
			f2 = (P.cX - S.StartPoint.cX) / (S.EndPoint.cX - S.StartPoint.cX);

			if (!isEqualValues(f1, f2)) return false;

			return (MayorOrEqual(P.cX, MinXSegment(S)) && MenorOrEqual(P.cX, MaxXSegment(S)) &&
				      MayorOrEqual(P.cY, MinYSegment(S)) && MenorOrEqual(P.cY, MaxYSegment(S)));
		}

		/// <summary>
		/// Determina si el punto "P" dado pertenece al circunferencia "C".
		/// </summary>
		/// <param name="P">
		/// Punto que se quiere comprobar si pertenece a la circunferencia dada.
		/// </param>
		/// <param name="C">
		/// Circunferencia para la cuál se quiere comprobar la pertenencia de un punto.  
		/// </param>
		/// <returns>
		/// Devuelve <b>true</b> si el punto dado pertenece a la circunferencia. En caso contrario devuelve <b>false</b>. 
		/// </returns>
		/// <seealso cref="CircleType"/>
		/// <seealso cref="PointType"/>
		public static bool PointInCircle (PointType P, CircleType C)
		{
			double AA, BB, CC;

			CircleCoefficient(C, out AA, out BB, out CC);

			// La pertenencia de un punto a una circunferencia se determina comprobando la 
			// siguiente congruencia. (Ecuacion General de la circunferencia).
			// X^2 + Y^2 + AX + BY + C = 0
			return (isEqualCero(P.cX * P.cX + P.cY * P.cY + AA * P.cX + BB * P.cY + CC));
		}

		/// <summary>
		/// Determina si el punto "P" dado pertenece al arco "A".
		/// </summary>
		/// <param name="P">
		/// Punto que se quiere comprobar si pertenece al arco dado.
		/// </param>
		/// <param name="A">
		/// Arco para el cuál se quiere comprobar la pertenencia de un punto. 
		/// </param>
		/// <returns>
		/// Devuelve <b>true</b> si el punto dado pertenece al arco. En caso contrario devuelve <b>false</b>.
		/// </returns>
		/// <seealso cref="ArcType"/>
		/// <seealso cref="PointType"/>
		public static bool PointInArc (PointType P, ArcType A)
		{
			bool flagA;
			double ang;
			CircleType C;

			C = new CircleType(A.Center, A.Radius);
			ang = GradToRad(PointPointAngle(A.Center, P));
			
			if (A.StartAngle < A.EndAngle)
				flagA = (MayorOrEqual(ang, A.StartAngle) && MenorOrEqual(ang, A.EndAngle));
			else
				flagA = (MayorOrEqual(ang, A.StartAngle) || MenorOrEqual(ang, A.EndAngle));

			return (PointInCircle(P, C) && flagA);
		}

		/// <summary>
		/// Determina si dos segmetos "S1" y "S2" dados son colineales.
		/// </summary>
		/// <param name="S1">
		/// Primer segmento.
		/// </param>
		/// <param name="S2">
		/// Segundo segmento.
		/// </param>
		/// <returns>
		/// Devuelve <b>true</b> si ambos segmentos son colineales. En caso contrario devuelve <b>false</b>.
		/// </returns>
		/// <remarks>
		/// Se define la condición de colinealidad de dos segmentos si estos pertenecen a una misma recta.
		/// </remarks>
		/// <seealso cref="SegmentType"/>
		public static bool isColinearSegment (SegmentType S1, SegmentType S2)
		{
			double ang1, ang2;

			ang1 = S1.Angle;
			ang2 = S2.Angle;

			// Condicion de colinealidad simple (ángulos iguales)
			if (isEqualCero(Math.Abs(ang1 - ang2))) return true; // Líneas colineales

			// Condicion de colinealidad compleja (ángulos opuestos)
			double temp = Math.Abs(ang2 - ang1);
			return isEqualCero(Math.Abs(temp - 180.0));
		}
		#endregion

		#region - Functions of Intersection's Calculation -
		/// <summary>
		/// Calcula el punto de intercepción entre dos líneas.
		/// </summary>
		/// <param name="L1">
		/// Primera línea.
		/// </param>
		/// <param name="L2">
		/// Segunda línea.
		/// </param>
		/// <param name="P">
		/// Parámetro de salida (out). Retorna por referencia el punto de intercepción entre las dos líneas, si existe.<br/>
		/// Nota: Si las líneas no se intersectan el parámetro "P" retorna NULL.
		/// </param>
		/// <returns>
		/// Devuelve <b>true</b> si ambas lineas se intercertan en el plano. En caso contrario devuelve <b>false</b>.
		/// </returns>
		/// <seealso cref="LineType"/>
		/// <seealso cref="PointType"/>
		public static bool LineLineIntersect (LineType L1, LineType L2, out PointType P)
		{
			PointType p = new PointType();
			double A1, B1, C1, A2, B2, C2, Denominador;

			LineCoefficient(L1, out A1, out B1, out C1);
			LineCoefficient(L2, out A2, out B2, out C2);

			Denominador = (A1 * B2) - (A2 * B1);

			if (isEqualCero(Denominador))
			{
				P = null;
				return false;
			}

			p.cX = -((C1 * B2) - (C2 * B1)) / Denominador;
			p.cY = -((A1 * C2) - (A2 * C1)) / Denominador;
			p.cZ = 0.0;

			P = p;
			return true;
		}

		/// <summary>
		/// Calcula el o los puntos de intercepción entre una línea y un circunferencia.
		/// </summary>
		/// <param name="C">
		/// Representa la circunferencia.
		/// </param>
		/// <param name="L">
		/// Representa la línea.
		/// </param>
		/// <param name="P1">
		/// Parámetro de salida (out). Retorna por referencia uno de los puntos de intersección, si existe.
		/// </param>
		/// <param name="P2">
		/// Parámetro de salida (out). Retorna por referencia el otro punto de intersección, si existe.
		/// </param>
		/// <remarks>
		/// - Si la línea es exterior a la circunferencia "P1" y "P2" retornarán NULL.<br/>
		/// - Si la línea es tangente a la circunferencia "P1" y "P2" serán iguales, pues solo existe un punto de intersección.<br/>
		/// - Si la línea es secante a la circunferencia "P1" y "P2" serán distintos pues la línea intersecta por dos puntos a la 
		/// circunferencia.
		/// </remarks>
		/// <returns>
		/// Devuelve un valor entero corto que indica lo siguiente:<br/>
		/// 0 - La línea es exterior a la circunferencia. No existen puntos de intersección.<br/>
		/// 1 - La línea es tangente a la circunferencia. Existe solo un punto de intersección. <br/>
		/// 2 - La línea es secante a la circunferencia. Existen dos puntos de intersección.
		/// </returns>
		/// <seealso cref="CircleType"/>
		/// <seealso cref="LineType"/>
		/// <seealso cref="PointType"/>
		public static byte CircleLineIntersect (CircleType C, LineType L, out PointType P1, out PointType P2)
		{
			byte f1, f2;
			double a, b, c, a1, b1, c1, m, p, q, x1, x2, y1, y2;
			PointType p1, p2;

			p1 = new PointType();
			p2 = new PointType();

			LineCoefficient(L, out a, out b, out c);
			CircleCoefficient(C, out a1, out b1, out c1);

			MakeMPQy(a, b, c, a1, b1, c1, out m, out p, out q);
			f1 = RootMPQ(m, p, q, out x1, out x2);

			MakeMPQx(a, b, c, a1, b1, c1, out m, out p, out q);
			f2 = RootMPQ(m, p, q, out y1, out y2);

			//---------------------
			//	No se encontraron raices por lo que la recta es Exterior a la Circunferencia.
			//---------------------
			if (f1 == 0 && f2 == 0)
			{
				P1 = P2 = null;
				return 0;
			}

			//---------------------
			//	Se encontraron 2 raices por lo que la recta es Secante.
			//---------------------
			if (f1 == 2 || f2 == 2)
			{
				// El eje radial no es paralelo a ninguno de ejes o esta sobre uno de ellos.
				if (isEqualCero(x1 * a + y1 * b + c))
				{
					p1.cX = x1;
					p1.cY = y1;
					p2.cX = x2;
					p2.cY = y2;
				}
				else
				{
					p1.cX = x1;
					p1.cY = y2;
					p2.cX = x2;
					p2.cY = y1;
				}

				// El eje radial es paralelo al eje de las Abcisas (By + C = 0).
				if (isEqualCero(a) && f1 != 0)
				{
					p1.cX = x1;
					p1.cY = -c / b;
					p2.cX = x2;
					p2.cY = -c / b;
				}

				// El eje radial es paralelo al eje de las Ordenadas (Ax + C = 0)
				if (isEqualCero(b) && f2 != 0)
				{
					p1.cX = -c / a;
					p1.cY = y2;
					p2.cX = -c / a;
					p2.cY = y1;
				}

				P1 = p1;
				P2 = p2;
				return 2;
			}

			//---------------------
			//	Se encontró 1 raiz por lo que la recta es Tangente a la Circunferencia.
			//---------------------
			if (f1 == 1 && f2 == 1)
			{
				p1.cX = x1;
				p1.cY = y1;
			}
			else if (f1 == 1 && f2 == 0)
			{
				p1.cX = x1;
				// Si la 2da raiz da 0, entonces se calcula la "Y" de la forma: Y = (-A/B)x + -C/B
				p1.cY = (-a / b) * x1 + -c / b;
			}
			else
			{
				p1.cY = y1;
				// Si la 1ra raiz da 0, entonces se calcula la "X" de la forma: X = (-B/A)y + -C/A
				p1.cX = (-b / a) * y1 + -c / a;
			}

			P1 = P2 = p1;
			return 1;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="E"></param>
		/// <param name="L"></param>
		/// <param name="P1"></param>
		/// <param name="P2"></param>
		/// <returns></returns>
		public static byte ElipseLineIntersect (ElipseType E, LineType L, out PointType P1, out PointType P2)
		{
			throw new NotImplementedException(); 
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="E"></param>
		/// <param name="L"></param>
		/// <param name="P1"></param>
		/// <param name="P2"></param>
		/// <returns></returns>
		public static byte PolygonLineIntersect (PolygonType E, LineType L, out PointType P1, out PointType P2)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="E"></param>
		/// <param name="L"></param>
		/// <param name="P1"></param>
		/// <param name="P2"></param>
		/// <returns></returns>
		public static byte RectangleLineIntersect (RectangleType E, LineType L, out PointType P1, out PointType P2)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Calcula el o los puntos de intercepción entre dos circunferencias.
		/// </summary>
		/// <param name="C1">
		/// Primera circunferencia. 
		/// </param>
		/// <param name="C2">
		/// Segunda circunferencia. 
		/// </param>
		/// <param name="P1">
		/// Parámetro de salida (out). Retorna por referencia uno de los puntos de intersección, si existe.
		/// </param>
		/// <param name="P2">
		/// Parámetro de salida (out). Retorna por referencia el otro punto de intersección, si existe.
		/// </param>
		/// <remarks>
		/// - Si las circunferencias son exteriores, entonces "P1" y "P2" retornarán NULL.<br/>
		/// - Si las circunferencias son tangente, entonces solo existe un punto de intersección por lo que "P1" y "P2" serán iguales.<br/>
		/// - Si las circunferecias son secantes, entonces "P1" y "P2" serán distintos.
		/// </remarks>
		/// <returns>
		/// Devuelve un valor entero corto que indica lo siguiente:<br/>
		/// 0 - Las circunferencias son exteriores. No existen puntos de intersección.<br/>
		/// 1 - Las circunferencias son tangentes. Existe solo un punto de intersección. <br/>
		/// 2 - Las circunferencia son secantes. Existen dos puntos de intersección. 
		/// </returns>
		/// <seealso cref="CircleType"/>
		/// <seealso cref="PointType"/>
		public static byte CircleCircleIntersect (CircleType C1, CircleType C2, out PointType P1, out PointType P2)
		{
			byte f1, f2;
			PointType p1, p2;
			double a, b, c, a1, b1, c1, a2, b2, c2;
			double m, p, q, x1, x2, y1, y2;

			//---------
			// Primero, compruebo que las 2 circunferencias C1 y C2 se cortan realmente. 
			// Para eso se comprueba la desigual TRIANGULAR de un triangulo imaginario
			// que se forma con las longitudes de Dist (distancia entre centro), R1, y R2.
			// Esta propiedad establece que para que dos circunfenrencias sean secantes 
			// o tangentes se debe cumplir que: Dist <= R1 + R2 y Dist >= R1 - R2.
			//---------
			double r1, r2, dist;
			r1 = C1.Radius;
			r2 = C2.Radius;
			dist = PointPointDistance(C1.Center, C2.Center);

			if (!MenorOrEqual(dist, r1 + r2) || !MayorOrEqual(dist, Math.Abs(r1 - r2)))
			{
				P1 = P2 = null;
				return 0;
			}

			p1 = new PointType();
			p2 = new PointType();

			CircleCoefficient(C1, out a1, out b1, out c1);
			CircleCoefficient(C2, out a2, out b2, out c2);

			a = (a1 - a2);
			b = (b1 - b2);
			c = (c1 - c2);

			MakeMPQy(a, b, c, a1, b1, c1, out m, out p, out q);
			f1 = RootMPQ(m, p, q, out x1, out x2);

			MakeMPQx(a, b, c, a1, b1, c1, out m, out p, out q);
			f2 = RootMPQ(m, p, q, out y1, out y2);

			//---------
			// Solucion analitica para circulos secantes.
			//---------
			if (f1 == 2 || f2 == 2)
			{
				// El eje radial no es paralelo a ninguno de ejes o esta sobre uno de ellos.
				if (isEqualCero(x1 * a + y1 * b + c))
				{
					p1.cX = x1;
					p1.cY = y1;
					p2.cX = x2;
					p2.cY = y2;
				}
				else
				{
					p1.cX = x1;
					p1.cY = y2;
					p2.cX = x2;
					p2.cY = y1;
				}

				// El eje radial es paralelo al eje de las Abcisas (By + C = 0).
				if (isEqualCero(a) && f1 != 0)
				{
					p1.cX = x1;
					p1.cY = -c / b;
					p2.cX = x2;
					p2.cY = -c / b;
				}

				// El eje radial es paralelo al eje de las Ordenadas (Ax + C = 0)
				if (isEqualCero(b) && f2 != 0)
				{
					p1.cX = -c / a;
					p1.cY = y2;
					p2.cX = -c / a;
					p2.cY = y1;
				}

				P1 = p1;
				P2 = p2;
				return 2;
			}

			//---------
			// Solucion geometrica para circulos tangentes.
			//---------
			if (f1 == 1 || f2 == 1)
			{
				if (r1 < r2) SwapCircle(ref C1, ref C2);

				double ang = PointPointAngle(C1.Center, C2.Center);
				P1 = P2 = PolarPoint(C1.Center, ang, C1.Radius);

				return 1;
			}

			P1 = P2 = null;
			return 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="C1"></param>
		/// <param name="E2"></param>
		/// <param name="P1"></param>
		/// <param name="P2"></param>
		/// <param name="P3"></param>
		/// <param name="P4"></param>
		/// <returns></returns>
		public static byte CircleElipseIntersect (CircleType C1, ElipseType E2, out PointType P1, out PointType P2, out PointType P3, out PointType P4)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Calcula el o los puntos de intercepcion entre una línea y un arco de circunferencia.
		/// </summary>
		/// <param name="A">
		/// Representa el arco de circunferencia.
		/// </param>
		/// <param name="L">
		/// Representa la línea.
		/// </param>
		/// <param name="P1">
		/// Parámetro de salida (out). Retorna por referencia uno de los puntos de intersección, si existe.
		/// </param>
		/// <param name="P2">
		/// Parámetro de salida (out). Retorna por referencia el otro punto de intersección, si existe.
		/// </param>
		/// <remarks>
		/// - Si la línea no intersecta al arco, entonces "P1" y "P2" retornarán NULL.<br/>
		/// - Si la línea intersecta al arco en un solo punto, entonces "P1" y "P2" son iguales.<br/>
		/// - Si la línea intersecta al arco en dos putos, entonces "P1" y "P2" son distintos.
		/// </remarks>
		/// <returns>
		/// Devuelve un valor entero corto que indica lo siguiente:<br/>
		/// 0 - La línea no intersecta al arco. <br/>
		/// 1 - La línea intersecta al arco en un único punto.<br/>
		/// 2 - La línea intersecta al arco en dos puntos.  
		/// </returns>
		/// <seealso cref="ArcType"/>
		/// <seealso cref="LineType"/>
		/// <seealso cref="PointType"/>
		public static byte ArcLineIntersect (ArcType A, LineType L, out PointType P1, out PointType P2)
		{
			CircleType C;
			PointType p1, p2;
			byte intersec, n;

			P1 = P2 = null;

			C = new CircleType(A.Center, A.Radius);
			intersec = CircleLineIntersect(C, L, out p1, out p2);

			if (intersec == 0) return 0;

			n = 0;
			if (intersec == 2)
			{
				if (PointInArc(p1, A))
				{
					P1 = p1;
					n = 1;
				}

				if (PointInArc(p2, A) && n == 1)
				{
					P2 = p2;
					n = 2;
				}
				else if (PointInArc(p2, A))
				{
					P1 = P2 = p2;
					n = 1;
				}
			}
			else
			{
				if (!PointInArc(p1, A)) return 0;

				P1 = P2 = p1;
				n = 1;
			}

			return n;
		}

		/// <summary>
		/// Calcula el o los puntos de intercepcion entre dos arcos de circunferencias.
		/// </summary>
		/// <param name="A1">
		/// Primer arco.
		/// </param>
		/// <param name="A2">
		/// Segundo arco.
		/// </param>
		/// <param name="P1">
		/// Parámetro de salida (out). Retorna por referencia uno de los puntos de intersección, si existe.
		/// </param>
		/// <param name="P2">
		/// Parámetro de salida (out). Retorna por referencia el otro punto de intersección, si existe.
		/// </param>
		/// <remarks>
		/// - Si los arcos de circunferencia no se interceptan, entonces "P1" y "P2" retornarán NULL.<br/>
		/// - Si los arcos de circunferencia se interceptan en un solo punto, entonces "P1" y "P2" son iguales.<br/>
		/// - Si los arcos de circunferencia se interceptan en dos puntos, entonces "P1" y "P2" son distintos.
		/// </remarks>
		/// <returns>
		/// Devuelve un valor entero corto que indica lo siguiente:<br/>
		/// 0 - Los arcos no intersectan entre si. <br/>
		/// 1 - Los arcos se intersectan en un único punto.<br/>
		/// 2 - Los arcos se intersectan en dos puntos. 
		/// </returns>
		/// <seealso cref="ArcType"/>
		/// <seealso cref="PointType"/>
		public static byte ArcArcIntersect (ArcType A1, ArcType A2, out PointType P1, out PointType P2)
		{
			byte intersec;
			PointType p1, p2;
			CircleType C1, C2;

			P1 = P2 = null;

			C1 = new CircleType(A1.Center, A1.Radius);
			C2 = new CircleType(A2.Center, A2.Radius);

			intersec = CircleCircleIntersect(C1, C2, out p1, out p2);

			if (intersec == 0) return 0;

			byte n = 0;
			if (intersec == 2)
			{
				// Compruebo si el P1 pertenece a ambos Arcos.
				if (PointInArc(p1, A1) && PointInArc(p1, A2))
				{
					P1 = p1;
					P2 = p1;
					n = 1;
				}

				// Compruebo si el P2 pertenece a ambos Arcos.
				if (PointInArc(p2, A1) && PointInArc(p2, A2) && n == 1)
				{
					P2 = p2;
					n = 2;
				}
				else if (PointInArc(p2, A1) && PointInArc(p2, A2))
				{
					P1 = P2 = p2;
					n = 1;
				}
			}
			else
			{
				if (!PointInArc(p1, A1) && !PointInArc(p1, A2)) return 0;

				P1 = P2 = p1;
				n = 1;
			}

			return n;
		}

		/// <summary>
		/// Calcula el o los puntos de intercepcion entre una circunferencia y un arco de circunferencia.
		/// </summary>
		/// <param name="A">
		/// Representa el arco de circunferencia.
		/// </param>
		/// <param name="C">
		/// Representa la circunferencia.
		/// </param>
		/// <param name="P1">
		/// Parámetro de salida (out). Retorna por referencia uno de los puntos de intersección, si existe.
		/// </param>
		/// <param name="P2">
		/// Parámetro de salida (out). Retorna por referencia el otro punto de intersección, si existe.
		/// </param>
		/// <remarks>
		/// - Si el arco y la circunferencia no se interceptan, entonces "P1" y "P2" retornarán NULL.<br/>
		/// - Si el arco y la circunferencia se interceptan en un solo punto, entonces "P1" y "P2" son iguales.<br/>
		/// - Si el arco y la circunferencia se interceptan en dos puntos, entonces "P1" y "P2" son distintos. 
		/// </remarks>
		/// <returns>
		/// Devuelve un valor entero corto que indica lo siguiente:<br/>
		/// 0 - El arco y la circuferecia no se intersectan en ningun punto. <br/>
		/// 1 - El arco y la circuferecia se intersectan en un único punto.<br/>
		/// 2 - El arco y la circuferecia se intersectan en dos puntos.
		/// </returns>
		/// <seealso cref="ArcType"/>
		/// <seealso cref="CircleType"/>
		/// <seealso cref="PointType"/>
		public static byte ArcCircleIntersect (ArcType A, CircleType C, out PointType P1, out PointType P2)
		{
			byte intersec;
			CircleType C1;
			PointType p1, p2;

			P1 = P2 = null;

			C1 = new CircleType(A.Center, A.Radius);
			intersec = CircleCircleIntersect(C1, C, out p1, out p2);

			if (intersec == 0) return 0;

			byte n = 0;
			if (intersec == 2)
			{
				// Compruebo si el P1 pertenece al Arco.
				if (PointInArc(p1, A))
				{
					P1 = p1;
					P2 = p1;
					n = 1;
				}

				// Compruebo si el P2 pertenece al Arco.
				if (PointInArc(p2, A) && n == 1)
				{
					P2 = p2;
					n = 2;
				}
				else if (PointInArc(p2, A))
				{
					P1 = P2 = p2;
					n = 1;
				}
			}
			else
			{
				if (!PointInArc(p1, A)) return 0;

				P1 = P2 = p1;
				n = 1;
			}

			return n;
		}

		/// <summary>
		/// Calcula el o los puntos de intercepcion entre una circunferencia y un segmento de recta.
		/// </summary>
		/// <param name="C">
		/// Representa la circunferencia.
		/// </param>
		/// <param name="S">
		/// Representa el segmento de recta.
		/// </param>
		/// <param name="P1">
		/// Parámetro de salida (out). Retorna por referencia uno de los puntos de intersección, si existe.
		/// </param>
		/// <param name="P2">
		/// Parámetro de salida (out). Retorna por referencia el otro punto de intersección, si existe.
		/// </param>
		/// <remarks>
		/// - Si el segmento no intercepta a la circunferencia, entonces "P1" y "P2" retornarán NULL.<br/>
		/// - Si el segmento intercepta a la circunferencia en un punto, entonces "P1" y "P2" son iguales.<br/>
		/// - Si el segmento intercepta a la circunferencia en dos puntos, entonces "P1" y "P2" son distintos.
		/// </remarks>
		/// <returns>
		/// Devuelve un valor entero corto que indica lo siguiente:<br/>
		/// 0 - El segmento no intersecta a la circunferencia. <br/>
		/// 1 - El segmento intersecta a la circunferencia en un único punto.<br/>
		/// 2 - El segmento intersecta a la circunferencia en dos puntos.
		/// </returns>
		/// <seealso cref="CircleType"/>
		/// <seealso cref="SegmentType"/>
		/// <seealso cref="PointType"/>
		public static byte CircleSegmentIntersect (CircleType C, SegmentType S, out PointType P1, out PointType P2)
		{
			LineType L;
			byte intersec;
			PointType p1, p2;

			L = S.ConvertToLine();
			intersec = CircleLineIntersect(C, L, out p1, out p2);

			//-----
			// El segmento es EXTERIOR a la circunferencia.
			if (intersec == 0)
			{
				P1 = P2 = null;
				return 0;
			}

			//-----
			// El segmento es TANGENTE a la circunferencia.
			if (intersec == 1)
			{
				P1 = P2 = p1;
				return 1;
			}

			//-----
			// El segmento es SECANTE a la circunferencia.
			if (PointInSegment(p1, S) && PointInSegment(p2, S))
			{
				P1 = p1;
				P2 = p2;
				return 2;
			}

			//-----
			// El segmento solo intercepta a la circunferencia en un punto
			// aunque no es tangente a ella.
			if (PointInSegment(p1, S))
				P1 = P2 = p1;
			else
				P1 = P2 = p2;

			return 1;
		}

		/// <summary>
		/// Calcula el o los puntos de intercepcion entre un arco de circunferencia y un segmento de recta.
		/// </summary>
		/// <param name="A">
		/// Representa el arco de circunferencia.
		/// </param>
		/// <param name="S">
		/// Representa el segmento del recta.
		/// </param>
		/// <param name="P1">
		/// Parámetro de salida (out). Retorna por referencia uno de los puntos de intersección, si existe.
		/// </param>
		/// <param name="P2">
		/// Parámetro de salida (out). Retorna por referencia el otro punto de intersección, si existe.
		/// </param>
		/// <remarks>
		/// - Si el segmento no intercepta al arco de circunferencia, entonces "P1" y "P2" retornarán NULL.<br/>
		/// - Si el segmento intercepta al arco de circunferencia en un punto, entonces "P1" y "P2" son iguales.<br/>
		/// - Si el segmento intercepta al arco de circunferencia en dos puntos, entonces "P1" y "P2" son distintos. 
		/// </remarks>
		/// <returns>
		/// Devuelve un valor entero corto que indica lo siguiente:<br/>
		/// 0 - El segmento no intersecta al arco. <br/>
		/// 1 - El segmento intersecta al arco en un único punto.<br/>
		/// 2 - El segmento intersecta al arco en dos puntos. 
		/// </returns>
		/// <seealso cref="ArcType"/>
		/// <seealso cref="SegmentType"/>
		/// <seealso cref="PointType"/>
		public static int ArcSegmentIntersect (ArcType A, SegmentType S, out PointType P1, out PointType P2)
		{
			byte intersec;
			CircleType C;
			PointType p1, p2;
			
			C = new CircleType(A.Center, A.Radius);
			intersec = CircleSegmentIntersect(C, S, out p1, out p2);

			//-----
			// El segmento es EXTERIOR al Arco.
			if (intersec == 0)
			{
				P1 = P2 = null;
				return 0;
			}

			//-----
			// El segmento es SECANTE al Arco.
			if (PointInSegment(p1, S) && PointInSegment(p2, S))
			{
				P1 = p1;
				P2 = p2;
				return 2;
			}

			//-----
			// El segmento solo intercepta al Arco en un punto
			// aunque no es tangente a el.
			if (PointInSegment(p1, S))
				P1 = P2 = p1;
			else
				P1 = P2 = p2;

			return 1;
		}

		/// <summary>
		/// Determina la intercepción real en el plano entre dos segmentos.<br/> 
		/// <b>Nota:</b> La intersección real indica que ambos segmentos tienen realmente un punto en común.
		/// </summary>
		/// <param name="S1">
		/// Primer segmento.
		/// </param>
		/// <param name="S2">
		/// Segundo segmento.
		/// </param>
		/// <param name="P">
		/// Parámetro de salida (out). Retorna por referencia el punto de intercepción entre los dos segmentos, si existe.<br/>
		/// <b>Nota:</b> Si los segmentos no se intersectan por ser colineales o paralels entre si, el parámetro "P" retorna NULL.
		/// </param>
		/// <returns>
		/// Devuelve <b>true</b> si ambos segmentos se intercertan realmente en el plano. En caso contrario devuelve <b>false</b>.
		/// </returns>
		/// <seealso cref="SegmentType"/>
		/// <seealso cref="PointType"/>
		public static bool SegmentsRealIntersect (SegmentType S1, SegmentType S2, out PointType P)
		{
			PointType p;
			bool intersec;

			intersec = SegmentsApparentIntersect(S1, S2, out p);

			if (!intersec)
			{
				P = null;
				return false;
			}

			if (!PointInSegment(p, S1) && !PointInSegment(p, S2))
			{
				P = null;
				return false;
			}

			P = p;
			return true;
		}

		/// <summary>
		/// Determina la intercepción aparente en el plano entre dos segmentos.<br/> 
		/// <b>Nota:</b> La intersección aparente indica 
		/// </summary>
		/// <param name="S1">
		/// Primer segmento.
		/// </param>
		/// <param name="S2">
		/// Segundo segmento.
		/// </param>
		/// <param name="P">
		/// Parámetro de salida (out). Retorna por referencia el punto de intercepción entre los dos segmentos, si existe.<br/>
		/// Nota: Si los segmentos no se intersectan por ser colineales o paralelos el parámetro "P" retorna NULL.
		/// </param>
		/// <returns>
		/// Devuelve <b>true</b> si ambos segmentos se intercertan en el plano. En caso contrario devuelve <b>false</b>.
		/// </returns>
		/// <seealso cref="SegmentType"/>
		/// <seealso cref="PointType"/>
		public static bool SegmentsApparentIntersect (SegmentType S1, SegmentType S2, out PointType P)
		{
			bool intersec;
			PointType p;
			LineType L1, L2;

			L1 = S1.ConvertToLine();
			L2 = S2.ConvertToLine();

			intersec = LineLineIntersect(L1, L2, out p);
			P = (intersec) ? p : null;

			return intersec;
		}

		/// <summary>
		/// Determina si la posición relativa que tiene un punto "P" respecto a recta "L" cumple con la condición
		/// indicada por "condition". 
		/// </summary>
		/// <param name="L">
		/// Representa la linea.
		/// </param>
		/// <param name="P">
		/// Representa el punto.
		/// </param>
		/// <param name="condition">
		/// 
		/// </param>
		/// <returns>
		/// Devuelve <b>true</b> si se cumple la condición dada, en caso contrario devuelve <b>false</b>
		/// </returns>
		//public static PoinLinePosition PointLineRelativePosition (LineType L, PointType P)
		public static bool CheckPointLineRelativePosition (PointType P, LineType L, PointLinePosition condition)
		{
			bool flag;

			// Como estrategia asumo que inicialmente la condicion no se cumple, por lo que la comprobacion va
			// encaminada a determinar si se cumple.
			flag = false;

			if (condition == PointLinePosition.Member)
			{
				flag = PointInLine(P, L);
			}
			else if (isEqualValues(L.Angle, 0) || isEqualValues(L.Angle, 180))
			{
				if (condition == PointLinePosition.Up && MayorEstricto(P.cY, L.P.cY)) flag = true;
				if (condition == PointLinePosition.Bottom && MenorEstricto(P.cY, L.P.cY)) flag = true;
			}
			else if (isEqualValues(L.Angle, 90) || isEqualValues(L.Angle, 270))
			{
				if (condition == PointLinePosition.Left && MenorEstricto(P.cX, L.P.cX)) flag = true;
				if (condition == PointLinePosition.Right && MayorEstricto(P.cX, L.P.cX)) flag = true;
			}
			else
			{
				/*PointType PP;
				LineType normal;
				double

				normal = PerperdicularLineAt(L, P);
				LineLineIntersect(L, normal, out PP);*/


			}

			return flag;
		}

		/// <summary>
		/// Determina la relación que hay entre una circunfencias y un segmento.
		/// </summary>
		/// <param name="C">
		/// Representa la circunferencia.
		/// </param>
		/// <param name="S">
		/// Representa el segmento.
		/// </param>
		/// <returns>
		/// Devuelve un tipo enum que indica la relacion entre la circunferencia y el segmento.
		/// </returns>
		/// <seealso cref="CircleType"/>
		/// <seealso cref="SegmentType"/> 
		/// <seealso cref="CircleSegmentRelation"/>
		public static CircleSegmentRelation CircleSegmentRelationShip (CircleType C, SegmentType S)
		{
			LineType L;
			byte intersec;
			PointType p1, p2;

			L = S.ConvertToLine();
			intersec = CircleLineIntersect(C, L, out p1, out p2);

			//-----
			// El segmento es EXTERIOR a la circunferencia.
			if (intersec == 0)
				return CircleSegmentRelation.Exterior;

			//-----
			// El segmento es SECANTE a la circunferencia.
			if (intersec == 2 && PointInSegment(p1, S) && PointInSegment(p2, S))
				return CircleSegmentRelation.Secant;

			//-----
			// El segmento solo intercepta a la circunferencia en un punto
			// aunque no es tangente a ella.
			if ((PointInSegment(p1, S) && !PointInSegment(p2, S)) || (!PointInSegment(p1, S) && PointInSegment(p2, S)))
				return CircleSegmentRelation.SimpleAcross;

			//-----
			// El segmento es TANGENTE a la circunferencia.			
			return CircleSegmentRelation.Tangent;
		}

		/// <summary>
		/// Determina la relacion que hay entre 2 circunfencias. 
		/// </summary>
		/// <param name="C1">
		/// Representa la 1ra circunferencia.
		/// </param>
		/// <param name="C2">
		/// Representa la 2da circunferencia.
		/// </param>
		/// <returns>
		/// Devuelve un tipo enum que indica la relación entre dos circunferencias.
		/// </returns>	
		/// <seealso cref="CircleType"/>
		/// <seealso cref="CircleCircleRelation"/>
		public static CircleCircleRelation CircleCircleRelationShip (CircleType C1, CircleType C2)
		{
			byte f1, f2;
			double m, p, q, x1, x2, y1, y2;
			double a, b, c, a1, b1, c1, a2, b2, c2;

			//---------
			// Primero, compruebo que las 2 circunferencias C1 y C2 se cortan realmente. 
			// Para eso se comprueba la desigual TRIANGULAR de un triangulo imaginario
			// que se forma con las longitudes de Dist (distancia entre centro), R1, y R2.
			// Esta propiedad establece que para que dos circunfenrencias sean secantes
			// se debe cumplir que: Dist <= R1 + R2 y Dist >= R1 - R2.
			//---------
			double r1, r2, dist;
			r1 = C1.Radius;
			r2 = C2.Radius;
			dist = PointPointDistance(C1.Center, C2.Center);

			if (isEqualCircle(C1, C2)) return CircleCircleRelation.Equal;

			if (!MenorOrEqual(dist, r1 + r2) || !MayorOrEqual(dist, Math.Abs(r1 - r2)))
			{
				CircleCircleRelation rel;

				if (isEqualPoint(C1.Center, C2.Center))
					rel = CircleCircleRelation.Concentric;
				else if (dist > r1 + r2)
					rel = CircleCircleRelation.Exterior;
				else
					rel = CircleCircleRelation.Interior;

				return rel;
			}

			CircleCoefficient(C1, out a1, out b1, out c1);
			CircleCoefficient(C2, out a2, out b2, out c2);

			a = (a1 - a2);
			b = (b1 - b2);
			c = (c1 - c2);

			MakeMPQy(a, b, c, a1, b1, c1, out m, out p, out q);
			f1 = RootMPQ(m, p, q, out x1, out x2);

			MakeMPQx(a, b, c, a1, b1, c1, out m, out p, out q);
			f2 = RootMPQ(m, p, q, out y1, out y2);

			//---------
			// Circunferencias secantes.
			if (f1 == 2 || f2 == 2)
				return CircleCircleRelation.Secant;

			//---------
			// Circunferencias tangentes.
			return isEqualCero(dist - (r1 + r2)) ? CircleCircleRelation.Tangent_Out : CircleCircleRelation.Tangent_In;
		}
		#endregion

		#region - Functions of Fillet's Calculation -
		/// <summary>
		/// Calcula el fillet (EMPALME) entre dos segmentos de rectas "S1" y "S2" dados, con radio determinado por 
		/// "r_fillet". 
		/// </summary>
		/// <param name="S1">
		/// Representa el 1er segmento.
		/// </param>
		/// <param name="S2">
		/// Representa el 2do segmento.
		/// </param>
		/// <param name="r_fillet">
		/// Representa el radio del fillet.
		/// </param>
		/// <param name="P1">
		/// Parámetro de salida (out). Retorna por referencia el punto del arc-fillet que pertenece al segmento "S1".
		/// </param>
		/// <param name="P2">
		/// Parámetro de salida (out). Retorna por referencia el punto del arc-fillet que pertenece al segmento "S2".
		/// </param>
		/// <param name="Pc">
		/// Parámetro de salida (out). Retorna por referencia el punto centro del arc-fillet.
		/// </param>
		/// <param name="s1_right_up">
		/// Determina hacia que lado del segmento "S1" se calcula el fillet.<br/><br/>
		/// Si el valor es <b>true</b>, el fillet se calcula hacia la derecha-o-arriba del segmento.
		/// Por el contrario si el valor es <b>false</b>, el fillet se calcula hacia la izquierda-o-abajo del
		/// segmento. En ambos casos su aplicación es sobre el segmento "S1".
		/// </param>
		/// <param name="s2_right_up">
		/// Determina hacia que lado del segmento "S2" se calcula el fillet.<br/><br/>
		/// Nota:Si el valor es <b>true</b>, el fillet se calcula hacia la derecha-o-arriba del segmento.
		/// Por el contrario si el valor es <b>false</b>, el fillet se calcula hacia la izquierda-o-abajo del 
		/// segmento. En ambos casos su aplicación es sobre el segmento "S2".
		/// </param>
		/// <returns>
		/// Nota:Devuelve un tipo void. Por referencia mediante los parámetros P1, P2 y Pc se obtienen los 3 
		/// puntos del arc-fillet.
		/// </returns>
		/// <remarks>
		/// Nota: "P1" pertenece al segmento "S1", "P2" pertenece al segmento "S2" y "Pc" es el centro del arco.
		/// </remarks>		 	
		/// <seealso cref="SegmentType"/>
		/// <seealso cref="PointType"/>
		public static bool SegmentSegmentFillet (SegmentType S1, SegmentType S2, double r_fillet, out PointType P1, out PointType P2, out PointType Pc,
			bool s1_right_up = true, bool s2_right_up = true)
		{
			byte typeV;
			PointType Pv;
			
			r_fillet = Math.Abs(r_fillet);

			//--------
			// Paso 1: Determino el punto de interseccion entre ambos segmentos y si este punto no existe salgo de la funcion.
			if (!SegmentsApparentIntersect(S1, S2, out Pv))
			{
				P1 = null;
				P2 = null;
				Pc = null;
				return false;
			}

			//--------
			// Paso 2: Determino el tipo de punto de interseccion calculado.
			// TIPO VI: Pv pertenece al mismo tiempo a los dos segmentos pero no es ninguno
			//				  de los puntos extremos de estos segmentos.
			if (S1.PointInSegment(Pv) && S2.PointInSegment(Pv) && !isEqualPoint(S1.StartPoint, Pv) &&
					!isEqualPoint(S1.EndPoint, Pv) && !isEqualPoint(S2.StartPoint, Pv) && !isEqualPoint(S2.EndPoint, Pv))
			{
				typeV = 6;
			}
			// TIPO V: Pv es un punto extremo de uno de los segmentos y pertenece al mismo tiempo
			//				al otro segmento pero sin ser ninguno de los puntos extremos de este utltimo.
			else if (((isEqualPoint(S1.StartPoint, Pv) || isEqualPoint(S1.EndPoint, Pv)) && S2.PointInSegment(Pv) && !isEqualPoint(S2.StartPoint, Pv) && !isEqualPoint(S2.EndPoint, Pv)) ||
							 ((isEqualPoint(S2.StartPoint, Pv) || isEqualPoint(S2.EndPoint, Pv)) && S1.PointInSegment(Pv) && !isEqualPoint(S1.StartPoint, Pv) && !isEqualPoint(S1.EndPoint, Pv)))
			{
				typeV = 5;
			}			
			// TIPO IV: Pv pertenece a unos de los segmentos sin ser los puntos extremos de este,
			//					y ademas no pertenece al otro segmento.	
			else if ((S1.PointInSegment(Pv) && !isEqualPoint(S1.StartPoint, Pv) && !isEqualPoint(S1.EndPoint, Pv) && !S2.PointInSegment(Pv)) || 
							 (S2.PointInSegment(Pv) && !isEqualPoint(S2.StartPoint, Pv) && !isEqualPoint(S2.EndPoint, Pv) && !S1.PointInSegment(Pv)))
			{
				typeV = 4;
			}
			// TIPO III: Pv pertenece al mismo tiempo a uno de los extremos de ambos segmentos
			else if ((isEqualPoint(S1.StartPoint, Pv) || isEqualPoint(S1.EndPoint, Pv)) && (isEqualPoint(S2.StartPoint, Pv) || isEqualPoint(S2.EndPoint, Pv)))
			{
				typeV = 3;
			}
			// TIPO II: Pv es uno de los puntos extremos de uno de los segmento y no pertenece al otro segmento.
			else if (((isEqualPoint(S1.StartPoint, Pv) || isEqualPoint(S1.EndPoint, Pv)) && !S2.PointInSegment(Pv)) ||
							 ((isEqualPoint(S2.StartPoint, Pv) || isEqualPoint(S2.EndPoint, Pv)) && !S1.PointInSegment(Pv)))
			{
				typeV = 2;
			}
			// TIPO I: Pv no pertenece a ninguno de los 2 segmentos. Lo que significa que los 
			//				 segmentos no tienen una intersección real, sino aparente en algun punto del plano.
			else
			{
				typeV = 1;
			}

			//--------
			// Paso 3: Determino hacia que lado se calcula el fillet.
			if (typeV == 4 || typeV == 5 || typeV == 6)
			{
				SegmentType ss1, ss2;

				if (typeV == 6)
				{
					PointType pp1, pp2;

					if (s1_right_up)
						pp1 = (S1.isVertical) ? S1.PointMayorY() : S1.PointMayorX();
					else
						pp1 = (S1.isVertical) ? S1.PointMenorY() : S1.PointMenorX();

					if (s2_right_up)
						pp2 = (S2.isVertical) ? S2.PointMayorY() : S2.PointMayorX();
					else
						pp2 = (S2.isVertical) ? S2.PointMenorY() : S2.PointMenorX();

					ss1 = new SegmentType(pp1, Pv);
					ss2 = new SegmentType(pp2, Pv);
					SS_Fillet_Type_XX(ss1, ss2, r_fillet, out P1, out P2, out Pc);
				}
				else
				{
					PointType ppx;

					// Compruebo si el punto de interseccion (Pv) esta sobre el segmento #1.
					if (S1.PointInSegment(Pv) && !isEqualPoint(S1.StartPoint, Pv) && !isEqualPoint(S1.EndPoint, Pv))
					{
						if (s1_right_up)
							ppx = (S1.isVertical) ? S1.PointMayorY() : S1.PointMayorX();
						else
							ppx = (S1.isVertical) ? S1.PointMenorY() : S1.PointMenorX();

						ss1 = new SegmentType(Pv, ppx);
						SS_Fillet_Type_XX(ss1, S2, r_fillet, out P1, out P2, out Pc);
					}
					// Sino, esta sobre el segmento #2.
					else
					{
						if (s2_right_up)
							ppx = (S2.isVertical) ? S2.PointMayorY() : S2.PointMayorX();
						else
							ppx = (S2.isVertical) ? S2.PointMenorY() : S2.PointMenorX();

						ss2 = new SegmentType(Pv, ppx);
						SS_Fillet_Type_XX(S1, ss2, r_fillet, out P1, out P2, out Pc);
					}
				}
			}
			else
			{
				SS_Fillet_Type_XX(S1, S2, r_fillet, out P1, out P2, out Pc);
			}

			return true;
		}

		/// <summary>
		/// Calcula el fillet (EMPALME) entre dos segmentos de rectas "S1" y "S2" dados, con radio determinado por 
		/// "r_fillet".  
		/// </summary>
		/// <param name="S1">
		/// Representa el 1er segmento.
		/// </param>
		/// <param name="S2">
		/// Representa el 2do segmento.
		/// </param>
		/// <param name="r_fillet">
		/// Representa el radio del fillet.
		/// </param>
		/// <param name="s1_right_up">
		/// Determina hacia que lado del segmento "S1" se calcula el fillet.<br/><br/>
		/// Nota: Si el valor es <b>true</b>, el fillet se calcula hacia la derecha-o-arriba del segmento.
		/// Por el contrario si el valor es <b>false</b>, el fillet se calcula hacia la izquierda-o-abajo del
		/// segmento. En ambos casos su aplicación es sobre el segmento "S1".
		/// </param>
		/// <param name="s2_right_up">
		/// Determina hacia que lado del segmento "S2" se calcula el fillet.<br/><br/>
		/// Nota: Si el valor es <b>true</b>, el fillet se calcula hacia la derecha-o-arriba del segmento.
		/// Por el contrario si el valor es <b>false</b>, el fillet se calcula hacia la izquierda-o-abajo del 
		/// segmento. En ambos casos su aplicación es sobre el segmento "S2".
		/// </param>		
		/// <returns>
		/// Devuelve un tipo Arco que representa el fillet entre los dos segmentos dados.
		/// </returns>
		/// <seealso cref="SegmentType"/>
		public static ArcType SegmentSegmentFillet (SegmentType S1, SegmentType S2, double r_fillet, bool s1_right_up = true, bool s2_right_up = true)
		{
			int sense;
			double Angle, sA, eA;
			PointType p1, p2, pc, px;

			//--------
			// Calculo los puntos del fillet y compruebo si este calculo es válido.
			if (!SegmentSegmentFillet(S1, S2, r_fillet, out p1, out p2, out pc, s1_right_up, s2_right_up)) return null;

			//--------
			// Con los datos calculados creo el arco.
			sA = PointPointAngle(pc, p1);
			eA = PointPointAngle(pc, p2);
			Angle = SegmentSegmentAngle(new SegmentType(pc, p1), new SegmentType(pc, p2));

			if (ArcType.GetArcDirectionByAngle(sA, eA, Angle) == ArcType.ArcDirection.Horario)
				sense = -1;
			else
				sense = 1;

			px = RotatePoint(p1, Angle / 2 * sense, pc);

			return new ArcType(new ArcStartPoint(p1), new ArcAnyPoint(px), new ArcEndPoint(p2));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="S1"></param>
		/// <param name="S2"></param>
		/// <param name="r_fillet"></param>
		/// <param name="s1_right_up"></param>
		/// <param name="s2_right_up"></param>
		/// <returns></returns>
		public static ArcType SegmentSegmentFilletSave (ref SegmentType S1, ref SegmentType S2, double r_fillet, bool s1_right_up = true, bool s2_right_up = true)
		{
			throw new NotImplementedException();

			int sense;
			LineType s1, s2;
			double Angle, sA, eA;
			PointType p1, p2, pc, px;

			//if (!SegmentsApparentIntersect(S1, S2, out px)) throw 

			//--------
			// Calculo los puntos del fillet.
			SegmentSegmentFillet(S1, S2, r_fillet, out p1, out p2, out pc, s1_right_up, s2_right_up);

			//--------
			// Si los puntos del fillet calculados no están estrictamente incluidos en los dos segmentos dados
			// significa que el radio dado es o muy grande o muy pequeño.
			//if (!S1.PointInSegment(p1) || !S2.PointInSegment(p2))

			//--------
			// Con los datos calculados creo el arco.
			sA = PointPointAngle(pc, p1);
			eA = PointPointAngle(pc, p2);
			Angle = SegmentSegmentAngle(new SegmentType(pc, p1), new SegmentType(pc, p2));

			if (ArcType.GetArcDirectionByAngle(sA, eA, Angle) == ArcType.ArcDirection.Horario)
				sense = -1;
			else
				sense = 1;

			px = RotatePoint(p1, Angle / 2 * sense, pc);



			return new ArcType(new ArcStartPoint(p1), new ArcAnyPoint(px), new ArcEndPoint(p2));
		}

		// Función Privada. Calcula el Fillet <SEGMENT-SEGMENT> para el Caso: I
		// Nota: función desarrollada por Marquez en 1999.
		private static void SS_Fillet_Type_XX (SegmentType S1, SegmentType S2, double r_fillet, out PointType P1, out PointType P2, out PointType Pc)
		{
			bool isAngSwap;
			PointType Pv, p1, p2, pc;
			double ang1, ang2, ang3, a1, a2, a3;
			double a_fillet, Tangent, dist, Sine1, Cosine1, Sine2, Cosine2, Sine3, Cosine3, Hipo;

			isAngSwap = false;
			p1 = new PointType();
			p2 = new PointType();
			pc = new PointType();

			//--------
			// Paso 1: Compruebo si el valor de radio valido.
			r_fillet = Math.Abs(r_fillet);
			if (isEqualCero(r_fillet))
				throw new FilletException("No se puede obtener un fillet con un radio de valor 0.");

			//--------
			// Paso 2: Determino el punto de interseccion de los segmentos y compruebo si las lineas son colineales o paralelas.
			if (!SegmentsApparentIntersect(S1, S2, out Pv))
			{
				P1 = P2 = Pc = null;
				return;
			}

			ang1 = isEqualPoint(Pv, S1.StartPoint) ? PointPointAngle(Pv, S1.EndPoint) : PointPointAngle(Pv, S1.StartPoint);
			ang2 = isEqualPoint(Pv, S2.StartPoint) ? PointPointAngle(Pv, S2.EndPoint) : PointPointAngle(Pv, S2.StartPoint);

			//--------
			// Paso 3: Si ang1 es mayor que ang2 los intercambio para que 
			//				 ang2 siempre sea mayor que ang1.
			if (ang1 > ang2)
			{
				isAngSwap = true;
				SwapValue(ref ang1, ref ang2);
			}

			//--------
			// Paso 4: Verifico si el ángulo que forman las líneas es agudo u obtuso.
			ang3 = (ang2 - ang1) < 180.0 ? ang1 + ((ang2 - ang1) / 2.0) : 180.0 + ang1 + ((ang2 - ang1) / 2.0);

			//--------
			// Paso 5: Calculo los ángulos complementario para ang1, ang2, ang3
			a1 = ComplementaryAngle(ang1);
			a2 = ComplementaryAngle(ang2);
			a3 = ComplementaryAngle(ang3);

			//--------
			// Paso 6: Calculo el ángulo para el cálculo del empalme (fillet)
			a_fillet = (ang2 - ang1) > 180.0 ? (ang2 - ang1) - 180.0 : 180.0 - (ang2 - ang1);

			//--------
			// Paso 7: Calculo los valores de las funciones trigonometricas y los 
			//				 valores de longitud necesarias.
			Tangent = Math.Tan(GradToRad(a_fillet) / 2.0);
			Sine1 = Math.Sin(GradToRad(a1));
			Cosine1 = Math.Cos(GradToRad(a1));
			Sine2 = Math.Sin(GradToRad(a2));
			Cosine2 = Math.Cos(GradToRad(a2));
			Sine3 = Math.Sin(GradToRad(a3));
			Cosine3 = Math.Cos(GradToRad(a3));
			dist = r_fillet * Tangent;
			Hipo = r_fillet / Math.Cos(GradToRad(a_fillet) / 2.0);

			//--------
			// Paso 8: Establesco las coordenadas de los puntos P1, P2, y Pc 
			p1.cX = (ang1 <= 90.0) || (ang1 >= 270.0) ? Pv.cX + Sine1 * dist : Pv.cX - Sine1 * dist;
			p1.cY = (ang1 <= 180.0) ? Pv.cY + Cosine1 * dist : Pv.cY - Cosine1 * dist;
			p1.cZ = 0.0;

			p2.cX = (ang2 <= 90.0) || (ang2 >= 270.0) ? Pv.cX + Sine2 * dist : Pv.cX - Sine2 * dist;
			p2.cY = (ang2 <= 180.0) ? Pv.cY + Cosine2 * dist : Pv.cY - Cosine2 * dist;
			p2.cZ = 0.0;

			pc.cX = (ang3 <= 90.0) || (ang3 >= 270.0) ? Pv.cX + Sine3 * Hipo : Pv.cX - Sine3 * Hipo;
			pc.cY = (ang3 <= 180.0) ? Pv.cY + Cosine3 * Hipo : Pv.cY - Cosine3 * Hipo;
			pc.cZ = 0.0;

			if (isAngSwap) SwapPoint(ref p1, ref p2);

			P1 = p1;
			P2 = p2;
			Pc = pc;
		}

		/// <summary>
		/// Calcula el fillet (EMPALME) entre una circunferencia "C" y un segmento de recta "S" dados, con
		/// radio determinado por "r_fillet". 
		/// </summary>
		/// <param name="C">
		/// Representa la circunferecia.
		/// </param>
		/// <param name="S">
		/// Representa el segmento de recta.
		/// </param>
		/// <param name="r_fillet">
		/// Representa el radio del fillet.
		/// </param>
		/// <param name="P1">
		/// Parámetro de salida (out). Retorna por referencia el punto del arc-fillet que pertenece a la circunferenca.
		/// </param>
		/// <param name="P2">
		/// Parámetro de salida (out). Retorna por referencia el punto del arc-fillet que pertenece al segmento de recta.
		/// </param>
		/// <param name="Pc">
		/// Parámetro de salida (out). Retorna por referencia el punto centro del arc-fillet.
		/// </param>
		/// <param name="right">
		/// Indica hacia que lado del segmento "S" dado se calcula el fillet (empalme). Los lados quedan determinados por 
		/// la recta que parte del centro de la circunferencia "C" y es perpendicular al segmento "S". Si el valor es <b>true</b>
		/// el fillet se calcula hacia la derecha del segmento "S", por el contrario si el valor es <b>false</b> el fillet se
		/// calcula hacia la izquierda de dico segmento.
		/// </param>
		/// <param name="outside">
		/// Indica si el fillet (empalme) se calcula exterior o interior a la circunferencia "C" dada. Si el valor es <b>true</b>
		/// el fillet se calcula exterior a la circunferencia, por el contrario si el valor es <b>false</b> el fillet se calcula
		/// hacia el interior de la circunferencia.
		/// </param>
		/// <param name="up">
		/// Indica si el fillet se calcula sobre el segmento "S" dado o por debajo de él. Si el valor es <b>true</b> el fillet
		/// se calcula sobre el segmento "S" dado, por el contrario si el valor es <b>false</b> el fillet se calcula por debajo 
		/// de dicho segmento.
		/// </param>
		/// <returns>
		/// Devuelve un tipo void. Por referencia mediante los parámetros P1, P2 y Pc se obtienen los 3 
		/// puntos del arc-fillet. 
		/// </returns>
		/// <remarks>
		/// Nota: "P1" pertenece a la circunferecia, "P2" pertenece al segmento y "Pc" es el centro del arco.
		/// </remarks>
		/// <exception cref="FilletException">
		/// Se lanza cuando se intenta crear un fillet cuyo valor de radio no lo permite por ser o muy pequeño o muy grande.
		/// </exception>
		/// <seealso cref="CircleType"/>
		/// <seealso cref="SegmentType"/>
		/// <seealso cref="PointType"/> 
		public static void CircleSegmentFillet (CircleType C, SegmentType S, double r_fillet, out PointType P1, out PointType P2, out PointType Pc,
			bool right = true, bool outside = true, bool up = true)
		{		
			r_fillet = Math.Abs(r_fillet);

			//--------
			// Se tratan las relaciones entre el Segmento y la circunferencia: SECANTE y SIMPLE-CROSS.
			if (CircleSegmentRelationShip(C, S) == CircleSegmentRelation.Secant)
			{
				byte b2, b1, b0;

				b2 = (byte) (right ? 1 : 0);
				b1 = (byte) (outside ? 1 : 0);
				b0 = (byte) (up ? 1 : 0);

				//----- SIMBOLOGIA BINARIA UTILIZADA. -----//
				// 8: [1, 1, 1] - Right, Outside, Up
				// 7: [1, 1, 0] - Right, Outside, Down
				// 6: [1, 0, 1] - Right,  Inside, Up
				// 5: [1, 0, 0] - Right,  Inside, Down
				// 4: [0, 1, 1] -  Left, Outside, Up 
				// 3: [0, 1, 0] -  Left, Outside, Down
				// 2: [0, 0, 1] -  Left,  Inside, Up
				// 1: [0, 0, 0] -  Left,  Inside, Down
				//-----------------------------------------//
				switch ((4 * b2) + (2 * b1) + (1 * b0) + 1)
				{
					case 1: case 2: case 5: case 6:	// Right-Left, Inside, Up-Down
						CS_Fillet_Type_X0X(C, S, r_fillet, out P1, out P2, out Pc, right, up);
						break;

					case 3: case 4: case 7: case 8: // Right-Left, Outside, Up-Down 
						CS_Fillet_Type_X1X(C, S, r_fillet, out P1, out P2, out Pc, right, up);
						break;

					default:
						P1 = null;
						P2 = null;
						Pc = null;
						break;
				}
			}
			//--------
			// Se tratan las otras dos relaciones: EXTERIOR y TANGENTE.
			else
			{
				CS_Fillet_Type_X11(C, S, r_fillet, out P1, out P2, out Pc, right);
			}
		}

		/// <summary>
		/// Calcula el fillet (EMPALME) entre una circunferencia "C" y un segmento de recta "S" dados, con
		/// radio determinado por "r_fillet".  
		/// </summary>
		/// <param name="C">
		/// Representa la circunferecia.
		/// </param>
		/// <param name="S">
		/// Representa el segmento de recta.
		/// </param>
		/// <param name="r_fillet">
		/// Representa el radio del fillet.
		/// </param>
		/// <param name="right">
		/// Indica hacia que lado del segmento "S" dado se calcula el fillet (empalme). Los lados quedan determinados por 
		/// la recta que parte del centro de la circunferencia "C" y es perpendicular al segmento "S". Si el valor es <b>true</b>
		/// el fillet se calcula hacia la derecha del segmento "S", por el contrario si el valor es <b>false</b> el fillet se
		/// calcula hacia la izquierda de dico segmento.
		/// </param>
		/// <param name="outside">
		/// Indica si el fillet (empalme) se calcula exterior o interior a la circunferencia "C" dada. Si el valor es <b>true</b>
		/// el fillet se calcula exterior a la circunferencia, por el contrario si el valor es <b>false</b> el fillet se calcula
		/// hacia el interior de la circunferencia.
		/// </param>
		/// <param name="up">
		/// Indica si el fillet se calcula sobre el segmento "S" dado o por debajo de él. Si el valor es <b>true</b> el fillet
		/// se calcula sobre el segmento "S" dado, por el contrario si el valor es <b>false</b> el fillet se calcula por debajo 
		/// de dicho segmento.
		/// </param>
		/// <returns>
		/// Devuelve un tipo Arco que representa el fillet calculado entre la circunferencia y el segmento de recta dados.
		/// </returns>
		/// <exception cref="FilletException">
		/// Se lanza cuando se intenta crear un fillet cuyo valor de radio no lo permite por ser o muy pequeño o muy grande.
		/// </exception>
		public static ArcType CircleSegmentFillet (CircleType C, SegmentType S, double r_fillet, bool right = true, bool outside = true, bool up = true)
		{
			int sense;
			double Angle, sA, eA;
			PointType p1, p2, pc, px;

			//--------
			// Calculo los puntos del fillet.
			CircleSegmentFillet(C, S, r_fillet, out p1, out p2, out pc, right, outside, up);

			//--------
			// Con los datos calculos creo el arco.
			sA = PointPointAngle(pc, p1);
			eA = PointPointAngle(pc, p2);
			Angle = SegmentSegmentAngle(new SegmentType(pc, p1), new SegmentType(pc, p2));

			if (ArcType.GetArcDirectionByAngle(sA, eA, Angle) == ArcType.ArcDirection.Horario)
				sense = -1;
			else
				sense = 1;

			px = RotatePoint(p1, Angle / 2 * sense, pc);

			return new ArcType(new ArcStartPoint(p1), new ArcAnyPoint(px), new ArcEndPoint(p2));
		}

		// Función Privada. Calcula el Fillet <CIRCLE-SEGMENT> para el Caso: Right-Left, Outside, (Up-Down)_absoluto.
		private static void CS_Fillet_Type_X1X(CircleType C, SegmentType S, double r_fillet, out PointType P1, out PointType P2, out PointType Pc, bool right, bool up)
		{
			double angA, angB;
			LineType L, aLN, bLN;
			PointType aP1, aP2, aPc, bP1, bP2, bPc, upP1, upP2, upPc, doP1, doP2, doPc, aPP, bPP;

			//--------
			// Paso 1: Calculo los dos posibles fillet (por debajo y por encima del segmento S).
			CS_Fillet_Type_X11(C, S, r_fillet, out aP1, out aP2, out aPc, right);
			CS_Fillet_Type_X10(C, S, r_fillet, out bP1, out bP2, out bPc, right);

			//--------
			// Paso 2: Determino los valores angulares que se usaran como referencia para determinar que se 
			//				 considera arriba (up) y que se considera abajo (down).
			L = S.ConvertToLine();
			aLN = PerperdicularLineAt(L, aPc);
			bLN = PerperdicularLineAt(L, bPc);

			LineLineIntersect(L, aLN, out aPP);
			LineLineIntersect(L, bLN, out bPP);

			angA = PointPointAngle(aPP, aPc);
			angB = PointPointAngle(bPP, bPc);

			//--------
			// Paso 3: Determino comprobando valores de ángulo cuál de los fillets calculados
			//				 se considera por arriba (up) y cuál por debajo (down) del segmento S.
			if ((MayorOrEqual(angA, 0.0) && MenorEstricto(angA, 180.0)) && (MayorOrEqual(angB, 180.0) && MenorEstricto(angB, 360.0)))
			{
				// En el juego de datos 'A' tengo los puntos de fillet que esta por encima (up) del segmento.
				upP1 = aP1;
				upP2 = aP2;
				upPc = aPc;
				// En el juego de datos 'B' tengo los puntos de fillet que esta por debajo (down) del segmento.
				doP1 = bP1;
				doP2 = bP2;
				doPc = bPc;
			}
			else
			{
				// En el juego de datos 'B' tengo los puntos de fillet que esta por encima (up) del segmento.
				upP1 = bP1;
				upP2 = bP2;
				upPc = bPc;
				// En el juego de datos 'A' tengo los puntos de fillet que esta por debajo (down) del segmento.
				doP1 = aP1;
				doP2 = aP2;
				doPc = aPc;
			}

			//--------
			// Paso 4: Obtengo el juego de puntos del fillet según el valor de "UP".
			if (up)
			{
				P1 = upP1;
				P2 = upP2;
				Pc = upPc;
			}
			else
			{
				P1 = doP1;
				P2 = doP2;
				Pc = doPc;
			}
		}

		// Función Privada. Calcula el Fillet <CIRCLE-SEGMENT> para el Caso: Right-Left, Outside, (Up-Down)_absoluto.
		private static void CS_Fillet_Type_X0X (CircleType C, SegmentType S, double r_fillet, out PointType P1, out PointType P2, out PointType Pc, bool right, bool up)
		{
			PointType PP;
			LineType L, normal;
			double angNormal;

			L = S.ConvertToLine();
			normal = PerperdicularLineAt(L, C.Center);
			LineLineIntersect(L, normal, out PP);
			angNormal = PointPointAngle(C.Center, PP);

			if (up)
			{
				if (MayorOrEqual(angNormal, 0.0) && MenorEstricto(angNormal, 180.0))
					CS_Fillet_Type_X00(C, S, r_fillet, out P1, out P2, out Pc, right);
				else
					CS_Fillet_Type_X01(C, S, r_fillet, out P1, out P2, out Pc, right);
			}
			else
			{
				if (MayorOrEqual(angNormal, 0.0) && MenorEstricto(angNormal, 180.0))
					CS_Fillet_Type_X01(C, S, r_fillet, out P1, out P2, out Pc, right);
				else
					CS_Fillet_Type_X00(C, S, r_fillet, out P1, out P2, out Pc, right);
			}
		}

		// Función Privada. Calcula el Fillet <CIRCLE-SEGMENT> para el Caso: Right-Left, Outside, (Up)_relativo para el caso segmento secante.
		private static void CS_Fillet_Type_X11 (CircleType C, SegmentType S, double r_fillet, out PointType P1, out PointType P2, out PointType Pc, bool right)
		{
			PointType PP, p;
			LineType L, normal;
			double dist, r_min, cateto, side2, angle, angNormal, angLine, ang, angR, angL;

			//--------
			// Paso 1: Trazo las construcciones auxiliares.	  
			L = S.ConvertToLine();
			normal = PerperdicularLineAt(L, C.Center);
			LineLineIntersect(L, normal, out PP);

			angNormal = PointPointAngle(C.Center, PP);
			if (right)
			{
				p = (S.isVertical) ? S.PointMayorY() : S.PointMayorX();
				angLine = PointPointAngle(PP, p);

				angR = angLine;
				angL = OppositeAngle(angLine);
			}
			else
			{
				p = (S.isVertical) ? S.PointMenorY() : S.PointMenorX();
				angLine = PointPointAngle(PP, p);

				angL = angLine;
				angR = OppositeAngle(angLine);
			}

			//--------
			// Paso 2: Determino dimensiones de control.
			dist = PointPointDistance(C.Center, PP);
			r_min = (dist - C.Radius) / 2.0;
			cateto = dist - r_fillet;

			//--------
			// Paso 3: Verifico y valido el r_fillet.
			if (isEqualCero(r_fillet))
				throw new FilletException("No se puede obtener un fillet con un radio de valor 0.");

			if (MenorEstricto(r_fillet, r_min))
				throw new FilletException("Radio muy pequeño. No se puede obtener un fillet con el valor de radio dado.");

			//--------
			// Paso 4: Compruebo el valor del cateto y determino puntos del fillet.
			if (cateto > 0)
			{
				ang = RadToGrad(Math.Acos(cateto / (C.Radius + r_fillet)));
				side2 = Math.Sqrt(Math.Pow(C.Radius + r_fillet, 2) - Math.Pow(cateto, 2));

				// Calculo el valor del ángulo que se usa en la ubicacion del los puntos P1, Pc y P2.
				//--- Relacion: 2 veces Mayor ---> [angL < angNormal > angR].
				if (angL < angNormal && angNormal > angR)
				{
					angle = (right) ? angNormal + ang : angNormal - ang;
				}
				//--- Relacion: 2 veces Menor ---> [angL > angNormal < angR].
				else if (angL > angNormal && angNormal < angR)
				{
					if ((new SegmentType(PP, p)).isVertical)
						angle = (right) ? angNormal + ang : angNormal - ang;
					else
						angle = (right) ? angNormal - ang : angNormal + ang;
				}
				//--- Relacion: 1 vez Mayor, 1 vez Menor ---> [angL < angNormal < angR  -AND-  angL > angNormal > angR].
				else
				{
					angle = (angLine > angNormal) ? angNormal + ang : angNormal - ang;
				}

				P1 = PolarPoint(C.Center, angle, C.Radius);
				P2 = PolarPoint(PP, angLine, side2);
				Pc = PolarPoint(C.Center, angle, C.Radius + r_fillet);				
			}
			else
			{
				ang = RadToGrad(Math.Asin(Math.Abs(cateto) / (C.Radius + r_fillet)));
				side2 = Math.Sqrt(Math.Pow(C.Radius + r_fillet, 2) - Math.Pow(cateto, 2));

				//--------
				// Calculo el valor del ángulo que se usa en la ubicacion del los puntos P1, P2 y Pc.
				//--- Relacion: 2 veces Mayor ---> [angL < angNormal > angR].
				if (angL < angNormal && angNormal > angR)
				{
					angle = (right) ? angLine + ang : angLine - ang;
				}
				//--- Relacion: 2 veces Menor ---> [angL > angNormal < angR].
				else if (angL > angNormal && angNormal < angR)
				{ 
					if ((new SegmentType(PP, p)).isVertical)
						angle = (right) ? angLine + ang : angLine - ang;
					else
						angle = (right) ? angLine - ang : angLine + ang;
				}
				//--- Relacion: 1 vez Mayor, 1 vez Menor ---> [angL < angNormal < angR  -AND-  angL > angNormal > angR].
				else
				{
					angle = (angLine > angNormal) ? angLine + ang : angLine - ang;
				}

				P1 = PolarPoint(C.Center, angle, C.Radius);
				P2 = PolarPoint(PP, angLine, side2);
				Pc = PolarPoint(C.Center, angle, C.Radius + r_fillet);				
			}
		}

		// Función Privada. Calcula el Fillet <CIRCLE-SEGMENT> para el Caso: Right-Left, Outside, (Down)_relativo para el caso segmento secante.
		private static void CS_Fillet_Type_X10 (CircleType C, SegmentType S, double r_fillet, out PointType P1, out PointType P2, out PointType Pc, bool right)
		{
			PointType PP, p;
			LineType L, normal;
			double dist, cateto, side2, angle, angNormal, angLine, ang, angR, angL;

			//--------
			// Paso 1: Trazo las construcciones auxiliares.
			L = S.ConvertToLine();
			normal = PerperdicularLineAt(L, C.Center);
			LineLineIntersect(L, normal, out PP);

			angNormal = PointPointAngle(C.Center, PP);
			if (right)
			{
				p = (S.isVertical) ? S.PointMayorY() : S.PointMayorX();
				angLine = PointPointAngle(PP, p);

				angR = angLine;
				angL = OppositeAngle(angLine);
			}
			else
			{
				p = (S.isVertical) ? S.PointMenorY() : S.PointMenorX();
				angLine = PointPointAngle(PP, p);

				angL = angLine;
				angR = OppositeAngle(angLine);
			}

			//--------
			// Paso 2: Determino dimensiones de control.
			dist = PointPointDistance(C.Center, PP);
			cateto = dist + r_fillet;

			//--------
			// Paso 3: Verifico y valido el r_fillet.
			if (isEqualCero(r_fillet))
				throw new FilletException("No se puede obtener un fillet con un radio de valor 0.");

			//--------
			// Paso 4: Compruebo el valor del cateto y determino puntos del fillet.
			ang = RadToGrad(Math.Acos(cateto / (C.Radius + r_fillet)));
			side2 = Math.Sqrt(Math.Pow(C.Radius + r_fillet, 2) - Math.Pow(cateto, 2));

			//--------
			// Calculo el valor del ángulo que se usa en la ubicacion del los puntos P1, Pc y P2.
			//--- Relacion: 2 veces Mayor ---> [angL < angNormal > angR].
			if (angL < angNormal && angNormal > angR)
			{
				angle = (right) ? angNormal + ang : angNormal - ang;
			}
			//--- Relacion: 2 veces Menor ---> [angL > angNormal < angR].
			else if (angL > angNormal && angNormal < angR)
			{
				if ((new SegmentType(PP, p)).isVertical)
					angle = (right) ? angNormal + ang : angNormal - ang;
				else
					angle = (right) ? angNormal - ang : angNormal + ang;
			}
			//--- Relacion: 1 vez Mayor, 1 vez Menor ---> [angL < angNormal < angR  -AND-  angL > angNormal > angR].
			else
			{
				angle = (angLine > angNormal) ? angNormal + ang : angNormal - ang;
			}

			P1 = PolarPoint(C.Center, angle, C.Radius);
			P2 = PolarPoint(PP, angLine, side2);
			Pc = PolarPoint(C.Center, angle, C.Radius + r_fillet);			
		}

		// Función Privada. Calcula el Fillet <CIRCLE-SEGMENT> para el Caso: Right-Left, Inside, (Up)_relativo para el caso segmento secante.
		private static void CS_Fillet_Type_X01 (CircleType C, SegmentType S, double r_fillet, out PointType P1, out PointType P2, out PointType Pc, bool right)
		{
			PointType PP, p;
			LineType L, normal;
			double dist, r_max, angle, angNormal, angLine, angR, angL;

			//--------
			// Paso 1: Trazo las construcciones auxiliares.
			L = S.ConvertToLine();
			normal = PerperdicularLineAt(L, C.Center);
			LineLineIntersect(L, normal, out PP);

			angNormal = PointPointAngle(C.Center, PP);
			if (right)
			{
				p = (S.isVertical) ? S.PointMayorY() : S.PointMayorX();
				angLine = PointPointAngle(PP, p);

				angR = angLine;
				angL = OppositeAngle(angLine);
			}
			else
			{
				p = (S.isVertical) ? S.PointMenorY() : S.PointMenorX();
				angLine = PointPointAngle(PP, p);

				angL = angLine;
				angR = OppositeAngle(angLine);
			}

			//--------
			// Paso 2: Determino dimensiones de control.
			dist = PointPointDistance(C.Center, PP);
			r_max = (C.Radius + dist) / 2.0;

			//--------
			// Paso 3: Verifico y valido el r_fillet.
			if (isEqualCero(r_fillet))
				throw new FilletException("No se puede obtener un fillet con un radio de valor 0.");

			if (MayorEstricto(r_fillet, r_max))
				throw new FilletException("Radio muy grande. No se puede obtener un fillet con el valor de radio dado.");

			//--------
			// Paso 4: Compruebo el valor del cateto y determino puntos del fillet.
			if (isEqualValues(r_fillet, r_max))
			{
				angle = OppositeAngle(PointPointAngle(C.Center, PP));

				P1 = PolarPoint(PP, angle, r_fillet * 2);
				P2 = PP;
				Pc = PolarPoint(PP, angle, r_fillet);				
			}
			else
			{
				double cateto, side2, ang;

				cateto = dist - r_fillet;

				if (cateto > 0)
				{
					ang = RadToGrad(Math.Acos(cateto / (C.Radius - r_fillet)));
					side2 = Math.Sqrt(Math.Pow(C.Radius - r_fillet, 2) - Math.Pow(cateto, 2));

					//--------
					// Calculo el valor del ángulo que se usa en la ubicacion del los puntos P1, Pc y P2.
					//--- Relacion: 2 veces Mayor ---> [angL < angNormal > angR].
					if (angL < angNormal && angNormal > angR)
					{
						angle = (right) ? angNormal + ang : angNormal - ang;
					}
					//--- Relacion: 2 veces Menor ---> [angL > angNormal < angR].
					else if (angL > angNormal && angNormal < angR)
					{
						if ((new SegmentType(PP, p)).isVertical)
							angle = (right) ? angNormal + ang : angNormal - ang;
						else
							angle = (right) ? angNormal - ang : angNormal + ang;
					}
					//--- Relacion: 1 vez Mayor, 1 vez Menor ---> [angL < angNormal < angR  -AND-  angL > angNormal > angR].
					else
					{
						angle = (angLine > angNormal) ? angNormal + ang : angNormal - ang;
					}

					P1 = PolarPoint(C.Center, angle, C.Radius);
					P2 = PolarPoint(PP, angLine, side2);
					Pc = PolarPoint(C.Center, angle, C.Radius - r_fillet);					
				}
				else
				{
					ang = RadToGrad(Math.Asin(Math.Abs(cateto) / (C.Radius - r_fillet)));
					side2 = Math.Sqrt(Math.Pow(C.Radius - r_fillet, 2) - Math.Pow(cateto, 2));

					//--------
					// Calculo el valor del ángulo que se usa en la ubicacion del los puntos P1, Pc y P2.
					//--- Relacion: 2 veces Mayor ---> [angL < angNormal > angR].
					if (angL < angNormal && angNormal > angR)
					{
						angle = (right) ? angLine + ang : angLine - ang;
					}
					//--- Relacion: 2 veces Menor ---> [angL > angNormal < angR].
					else if (angL > angNormal && angNormal < angR)
					{
						if ((new SegmentType(PP, p)).isVertical)
							angle = (right) ? angLine + ang : angLine - ang;
						else
							angle = (right) ? angLine - ang : angLine + ang;
					}
					//--- Relacion: 1 vez Mayor, 1 vez Menor ---> [angL < angNormal < angR  -AND-  angL > angNormal > angR].
					else
					{
						angle = (angLine > angNormal) ? angLine + ang : angLine - ang;
					}

					P1 = PolarPoint(C.Center, angle, C.Radius);
					P2 = PolarPoint(PP, angLine, side2);
					Pc = PolarPoint(C.Center, angle, C.Radius - r_fillet);					
				}
			}
		}

		// Función Privada. Calcula el Fillet <CIRCLE-SEGMENT> para el Caso: Right-Left, Inside, (Down)_relativo para el caso segmento secante.
		private static void CS_Fillet_Type_X00 (CircleType C, SegmentType S, double r_fillet, out PointType P1, out PointType P2, out PointType Pc, bool right)
		{
			PointType PP, p;
			LineType L, normal;
			double dist, r_max, cateto, angle, angNormal, angLine, angR, angL;

			//--------
			// Paso 1: Trazo las construcciones auxiliares.
			L = S.ConvertToLine();
			normal = PerperdicularLineAt(L, C.Center);
			LineLineIntersect(L, normal, out PP);

			angNormal = PointPointAngle(C.Center, PP);
			if (right)
			{
				p = (S.isVertical) ? S.PointMayorY() : S.PointMayorX();
				angLine = PointPointAngle(PP, p);

				angR = angLine;
				angL = OppositeAngle(angLine);
			}
			else
			{
				p = (S.isVertical) ? S.PointMenorY() : S.PointMenorX();
				angLine = PointPointAngle(PP, p);

				angL = angLine;
				angR = OppositeAngle(angLine);
			}

			//--------
			// Paso 2: Determino dimensiones de control.
			dist = PointPointDistance(C.Center, PP);
			r_max = (C.Radius - dist) / 2.0;
			cateto = dist + r_fillet;

			//--------
			// Paso 3: Verifico y valido el r_fillet
			if (isEqualCero(r_fillet))
				throw new FilletException("No se puede obtener un fillet con un radio de valor 0.");

			if (MayorEstricto(r_fillet, r_max))
				throw new FilletException("Radio muy grande. No se puede obtener un fillet con el valor de radio dado.");

			//--------
			// Paso 4: Compruebo el valor del cateto
			if (isEqualValues(cateto, C.Radius - r_fillet))
			{
				angle = PointPointAngle(C.Center, PP);

				P1 = PolarPoint(C.Center, angle, C.Radius);
				P2 = PP;
				Pc = PolarPoint(C.Center, angle, C.Radius - r_fillet);				
			}
			else
			{
				double side2, ang;

				ang = RadToGrad(Math.Acos(cateto / (C.Radius - r_fillet)));
				side2 = Math.Sqrt(Math.Pow(C.Radius - r_fillet, 2) - Math.Pow(cateto, 2));

				//--------
				// Calculo el valor del ángulo que se usa en la ubicacion del los puntos P1, Pc y P2.
				//--- Relacion: 2 veces Mayor ---> [angL < angNormal > angR].
				if (angL < angNormal && angNormal > angR)
				{
					angle = (right) ? angNormal + ang : angNormal - ang;
				}
				//--- Relacion: 2 veces Menor ---> [angL > angNormal < angR].
				else if (angL > angNormal && angNormal < angR)
				{
					if ((new SegmentType(PP, p)).isVertical)
						angle = (right) ? angNormal + ang : angNormal - ang;
					else
						angle = (right) ? angNormal - ang : angNormal + ang;
				}
				//--- Relacion: 1 vez Mayor, 1 vez Menor ---> [angL < angNormal < angR  -AND-  angL > angNormal > angR].
				else
				{
					angle = (angLine > angNormal) ? angNormal + ang : angNormal - ang;
				}

				P1 = PolarPoint(C.Center, angle, C.Radius);
				P2 = PolarPoint(PP, angLine, side2);
				Pc = PolarPoint(C.Center, angle, C.Radius - r_fillet);				
			}
		}

		/// <summary>
		/// Calcula el fillet (EMPALME) entre dos circunferencias "C1" y "C2" dados, con radio determinado por 
		/// "r_fillet". 
		/// </summary>
		/// <param name="C1">
		/// Representa la 1ra circunferecia.
		/// </param>
		/// <param name="C2">
		/// Representa la 2da circunferencia.
		/// </param>
		/// <param name="r_fillet">
		/// Representa el radio del fillet.
		/// </param>
		/// <param name="P1">
		/// Parámetro de salida (out). Retorna por referencia el punto del arc-fillet que pertenece a la circunferenca.
		/// </param>
		/// <param name="P2">
		/// Parámetro de salida (out). Retorna por referencia el punto del arc-fillet que pertenece al segmento de recta.
		/// </param>
		/// <param name="Pc">
		/// Parámetro de salida (out). Retorna por referencia el punto centro del arc-fillet.
		/// </param>
		/// <param name="right_up">
		/// Indica hacia que lado se calcula el fillet, tomando como referencia el segmento imaginario "Sx" que se forma  
		/// entre los centros de ambas circunferencias.<br/>
		/// Nota: Si el valor es <b>true</b>, el fillet se calcula hacia la 
		/// derecha-o-arriba del segmento "Sx". Por el contrario si el valor es <b>false</b>, el fillet se calcula hacia 
		/// la izquierda-o-abajo del segmento.
		/// </param>
		/// <param name="c1_outside">
		/// Indica si el fillet se calcula hacia adentro o hacia afuera de la circunferencia "C1".<br/>
		/// Nota: Si el valor es <b>true</b>, el fillet se calcula hacia el exterior de la circunferencia. Por el 
		/// contrario si el valor es <b>false</b>, el fillet se calcula hacia el interior de la circunferencia. En ambos 
		/// casos su aplicación es sobre la circunferencia "C1".
		/// </param>
		/// <param name="c2_outside">
		/// Indica si el fillet se calcula hacia adentro o hacia afuera de la circunferencia "C2".<br/>
		/// Nota: Si el valor es <b>true</b>, el fillet se calcula hacia el exterior de la circunferencia. Por el 
		/// contrario si el valor es <b>false</b>, el fillet se calcula hacia el interior de la circunferencia. En ambos 
		/// casos su aplicación es sobre la circunferencia "C2".
		/// </param>
		/// <returns>
		/// Devuelve un tipo void. Por referencia mediante los parámetros P1, P2 y Pc se obtienen los 3 
		/// puntos del arc-fillet. 
		/// </returns>
		/// <remarks>
		/// Nota: "P1" pertenece a la circunferecia "C1", "P2" pertenece a la circunferencia "C2" y "Pc" es el centro del arco.
		/// </remarks>
		public static void CircleCircleFillet (CircleType C1, CircleType C2, double r_fillet, out PointType P1, out PointType P2, out PointType Pc,
			bool right_up = true, bool c1_outside = true, bool c2_outside = true)
		{
			//--------
			// Se trata la relacion SECANTES.
			if (CircleCircleRelationShip(C1, C2) == CircleCircleRelation.Secant)
			{
				byte b2, b1, b0;

				b2 = (byte)(right_up ? 1 : 0);
				b1 = (byte)(c1_outside ? 1 : 0);
				b0 = (byte)(c2_outside ? 1 : 0);

				//------- SIMBOLOGIA BINARIA UTILIZADA. -------//
				// 8: [1, 1, 1] -   Up, Outside_c1, Outside_c2
				// 7: [1, 1, 0] -   Up, Outside_c1,  Inside_c2
				// 6: [1, 0, 1] -   Up,  Inside_c1, Outside_c2
				// 5: [1, 0, 0] -   Up,  Inside_c1,  Inside_c2
				// 4: [0, 1, 1] - Down, Outside_c1, Outside_c2
				// 3: [0, 1, 0] - Down, Outside_c1,  Inside_c2
				// 2: [0, 0, 1] - Down,  Inside_c1, Outside_c2
				// 1: [0, 0, 0] - Down,  Inside_c1,  Inside_c2
				//---------------------------------------------//
				switch ((4 * b2) + (2 * b1) + (1 * b0) + 1)
				{
					case 4: case 8: // Up-Down, Outside_c1, Outside_c2
						CC_Fillet_Type_X11(C1, C2, r_fillet, out P1, out P2, out Pc, right_up);
						break;

					case 3: case 7: // Up-Down, Outside_c1, Inside_c2
						CC_Fillet_Type_X10(C1, C2, r_fillet, out P1, out P2, out Pc, right_up);
						break;

					case 2: case 6: // Up-Down, Inside_c1, Outside_c2
						CC_Fillet_Type_X01(C1, C2, r_fillet, out P1, out P2, out Pc, right_up);
						break;

					case 1: case 5:	// Up-Down, Inside_c1, Inside_c2
					 	CC_Fillet_Type_X00(C1, C2, r_fillet, out P1, out P2, out Pc, right_up);
						break;

					default:
						P1 = null;
						P2 = null;
						Pc = null;
						break;
				}
			}
			//--------
			// Se tratan las otras dos relaciones: EXTERIORES y TANGENTE.
			else
			{
				CC_Fillet_Type_X11(C1, C2, r_fillet, out P1, out P2, out Pc, right_up);				
			}
		}

		/// <summary>
		/// Calcula el fillet (EMPALME) entre dos circunferencias "C1" y "C2" dados, con radio determinado por 
		/// "r_fillet".
		/// </summary>
		/// <param name="C1">
		/// Representa la 1ra circunferecia.
		/// </param>
		/// <param name="C2">
		/// Representa la 2da circunferencia.
		/// </param>
		/// <param name="r_fillet">
		/// Representa el radio del fillet.
		/// </param>
		/// <param name="right_up">
		/// Indica hacia que lado se calcula el fillet, tomando como referencia el segmento imaginario "Sx" que se forma  
		/// entre los centros de ambas circunferencias.<br/>
		/// Nota: Si el valor es <b>true</b>, el fillet se calcula hacia la derecha-o-arriba del segmento "Sx".
		/// Por el contrario si el valor es <b>false</b>, el fillet se calcula hacia la izquierda-o-abajo del segmento.
		/// </param>
		/// <param name="c1_outside">
		/// Indica si el fillet se calcula hacia adentro o hacia afuera de la circunferencia "C1".<br/>
		/// Nota: Si el valor es <b>true</b>, el fillet se calcula hacia el exterior de la circunferencia. Por el 
		/// contrario si el valor es <b>false</b>, el fillet se calcula hacia el interior de la circunferencia. En ambos 
		/// casos su aplicación es sobre la circunferencia "C1".
		/// </param>
		/// <param name="c2_outside">
		/// Indica si el fillet se calcula hacia adentro o hacia afuera de la circunferencia "C2".<br/>
		/// Nota: Si el valor es <b>true</b>, el fillet se calcula hacia el exterior de la circunferencia. Por el 
		/// contrario si el valor es <b>false</b>, el fillet se calcula hacia el interior de la circunferencia. En ambos 
		/// casos su aplicación es sobre la circunferencia "C2".
		/// </param>
		/// <returns>
		/// Devuelve un tipo Arco que representa el fillet calculado las dos circunferencias dadas.
		/// </returns>
		/// <seealso cref="ArcType"/>
		/// <seealso cref="CircleType"/>
		public static ArcType CircleCircleFillet (CircleType C1, CircleType C2, double r_fillet, bool right_up = true, bool c1_outside = true, bool c2_outside = true)
		{
			int sense;
			double Angle, sA, eA;
			PointType p1, p2, pc, px;

			//--------
			// Calculo los puntos del fillet.
			CircleCircleFillet(C1, C2, r_fillet, out p1, out p2, out pc, right_up, c1_outside, c2_outside);

			//--------
			// Con los datos calculos creo el arco.
			sA = PointPointAngle(pc, p1);
			eA = PointPointAngle(pc, p2);
			Angle = SegmentSegmentAngle(new SegmentType(pc, p1), new SegmentType(pc, p2));

			if (ArcType.GetArcDirectionByAngle(sA, eA, Angle) == ArcType.ArcDirection.Horario)
				sense = -1;
			else
				sense = 1;

			px = RotatePoint(p1, Angle / 2 * sense, pc);

			return new ArcType(new ArcStartPoint(p1), new ArcAnyPoint(px), new ArcEndPoint(p2));
		}

		// Función Privada. Calcula el Fillet <CIRCLE-CIRCLE> para el Caso: (Up-Down)_absoluto, C1_outside, C2_outside.
		private static void CC_Fillet_Type_X11 (CircleType C1, CircleType C2, double r_fillet, out PointType P1, out PointType P2, out PointType Pc, bool up)
		{			
			CircleType c1, c2;
			LineType L, aLN, bLN;
			double dist, r_min, aAng, bAng;
			PointType upP1, upP2, upPc, doP1, doP2, aP1, aP2, aPc, bP1, bP2, bPc, doPc, aPP, bPP;

			//--------
			// Paso 1: Calculo distancia entre centros y radio minimo posible. 
			dist = PointPointDistance(C1.Center, C2.Center);
			r_min = (dist - (C1.Radius + C2.Radius)) / 2.0;

			//--------
			// Paso 2: Verifico y valido el r_fillet.
			if (isEqualCero(r_fillet))
				throw new FilletException("No se puede obtener un fillet con un radio de valor 0.");

			if (MenorEstricto(r_fillet, r_min))
				throw new FilletException("Radio muy pequeño. No se puede obtener un fillet con el valor de radio dado.");

			//--------
			// Paso 3: Calculo las dos variantes de fillet que se dan para el caso [X, 1, 1].
			c1 = new CircleType(C1.Center, C1.Radius + r_fillet);
			c2 = new CircleType(C2.Center, C2.Radius + r_fillet);
			CircleCircleIntersect(c1, c2, out aPc, out bPc);

			// a) Variante del fillet posiblemente arriba.
			aP1 = PolarPoint(C1.Center, PointPointAngle(C1.Center, aPc), C1.Radius);
			aP2 = PolarPoint(C2.Center, PointPointAngle(C2.Center, aPc), C2.Radius);

			// b) Variante del fillet posiblemente abajo.
			bP1 = PolarPoint(C1.Center, PointPointAngle(C1.Center, bPc), C1.Radius);
			bP2 = PolarPoint(C2.Center, PointPointAngle(C2.Center, bPc), C2.Radius);

			//--------
			// Paso 4: Determino cuál de las 2 variantes de fillet -A- o -B- corresponde al 
			//				 parámetro "UP".
			L = (new SegmentType(C1.Center, C2.Center)).ConvertToLine();
			aLN = PerperdicularLineAt(L, aPc);
			bLN = PerperdicularLineAt(L, bPc);

			LineLineIntersect(L, aLN, out aPP);
			LineLineIntersect(L, bLN, out bPP);

			aAng = PointPointAngle(aPP, aPc);
			bAng = PointPointAngle(bPP, bPc);

			if ((MayorOrEqual(aAng, 0.0) && MenorEstricto(aAng, 180.0)) && (MayorOrEqual(bAng, 180.0) && MenorEstricto(bAng, 360.0)))
			{
				// En el juego de datos 'A' tengo los puntos de fillet que esta por encima (up) del segmento -dist-.
				upP1 = aP1;
				upP2 = aP2;
				upPc = aPc;
				// En el juego de datos 'B' tengo los puntos de fillet que esta por debajo (down) del segmento -dist-.
				doP1 = bP1;
				doP2 = bP2;
				doPc = bPc;
			}
			else
			{
				// En el juego de datos 'B' tengo los puntos de fillet que esta por encima (up) del segmento -dist-.
				upP1 = bP1;
				upP2 = bP2;
				upPc = bPc;
				// En el juego de datos 'A' tengo los puntos de fillet que esta por debajo (down) del segmento -dist-.
				doP1 = aP1;
				doP2 = aP2;
				doPc = aPc;
			}

			//--------
			// Paso 5: Obtengo el juego de puntos del fillet según el valor de "UP".
			if (up)
			{
				P1 = upP1;
				P2 = upP2;
				Pc = upPc;
			}
			else
			{
				P1 = doP1;
				P2 = doP2;
				Pc = doPc;
			}
		}

		// Función Privada. Calcula el Fillet <CIRCLE-CIRCLE> para el Caso: (Up-Down)_absoluto, C1_outside, C2_outside.
		// Nota: Esta función fue la primero que diseñe al intentar resolver el CIRCLE-CIRCLE-FILLET. La solucion se basa
		//			 en calcular el area de un triangulo rectangulo que se forma entre los centros de ambos circulos y el centro
		//			 del arco, esta area se calcula por la formula de Heron. Habiendo calculado esta area se puede obtener la altura
		//			 de este triangulo y con ella el fillet entre las 2 circunferencias.
		private static void CC_Fillet_Type_X11_v2 (CircleType C1, CircleType C2, double r_fillet, out PointType P1, out PointType P2, out PointType Pc, bool up)
		{
			bool swap;
			double dist, r_min, sp, area, altura, a1, a2, beta1, beta2;

			//--------
			// Paso 1: Calculo distancia entre centros y radio minimo posible. 
			dist = PointPointDistance(C1.Center, C2.Center);
			r_min = (dist - (C1.Radius + C2.Radius)) / 2.0;

			//--------
			// Paso 2: Verifico y valido el r_fillet.
			if (isEqualCero(r_fillet))
				throw new FilletException("No se puede obtener un fillet con un radio de valor 0.");

			if (MenorEstricto(r_fillet, r_min))
				throw new FilletException("Radio muy pequeño. No se puede obtener un fillet con el valor de radio dado.");

			//--------
			// Paso 3: Calculo la altura del triangulo que se forma entre los centros de ambas circunferencias
			//				 y el centro del arco del fillet. Tambien calculo los angulos betas, que son los ángulos 
			//				 que se oponen al segmento H (altura).
			// Nota: los centros de ambos circulos junto al centro del arco del fillet constituyen los vértices 
			//			 de un triangulo cuya altura parte del centro del arco y cae sobre el segmento |C1.Pc-C2.Pc|.
			//			 De este triangulo se conocen sus 3 lados, por lo que el calculo de su altura se realiza
			//			 mediante la forma de area (A = 1/2*b*H), pudiendo calcular el area mediante la formula
			//			 de Eron para el area del triangulo conocidos sus 3 lados y su perimetro.
			sp = (dist + (C1.Radius + r_fillet) + (C2.Radius + r_fillet)) / 2.0;
			area = Math.Sqrt(sp * (sp - dist) * (sp - (C1.Radius + r_fillet)) * (sp - (C2.Radius + r_fillet)));
			altura = 2.0 * area / dist;

			beta1 = RadToGrad(Math.Asin(altura / (C1.Radius + r_fillet)));
			beta2 = RadToGrad(Math.Asin(altura / (C2.Radius + r_fillet)));

			swap = false;
			a1 = PointPointAngle(C1.Center, C2.Center);
			a2 = PointPointAngle(C2.Center, C1.Center);

			// Compruebo si 'a1' es mayor que 'a2'. Si esto ocurre realizo un intercambio provisional
			// con el objetivo de garantizar que siempre 'a1' sea menor que 'a2' pues el resto de los
			// pasos estan basado en esta caracterista.
			if (a1 > a2)
			{
				swap = true;

				SwapValue(ref a1, ref a2);
				SwapValue(ref beta1, ref beta2);
				SwapCircle(ref C1, ref C2);
			}

			//--------
			// Paso 4: Determino los puntos del fillet.	Calculo dos variantes para luego seleccion
			//				 una dependiendo de la posicion que tienen respecto al segmento -dist-.
			if (isEqualValues(r_fillet, r_min))
			{
				P1 = PolarPoint(C1.Center, a1, C1.Radius);
				P2 = PolarPoint(C2.Center, a2, C2.Radius);
				Pc = MidPointBetweenPoint(P1, P2);
			}
			else
			{
				LineType aL1, aL2, bL1, bL2, L, aLN, bLN;
				PointType aP1, aP2, aPc, bP1, bP2, bPc, aPP, bPP, upP1, upP2, upPc, doP1, doP2, doPc;
				double aAngle1, aAngle2, bAngle1, bAngle2, aAng, bAng;

				// Fillet variante -A-
				aAngle1 = a1 + beta1;
				aAngle2 = a2 - beta2;
				aL1 = new LineType(C1.Center, aAngle1);
				aL2 = new LineType(C2.Center, aAngle2);				

				aP1 = PolarPoint(C1.Center, aAngle1, C1.Radius);
				aP2 = PolarPoint(C2.Center, aAngle2, C2.Radius);
				LineLineIntersect(aL1, aL2, out aPc);

				// Fillet variante -B-
				bAngle1 = (isNegative(a1 - beta1)) ? 360.0 + a1 - beta1 : a1 - beta1;
				bAngle2 = a2 + beta2;
				bL1 = new LineType(C1.Center, bAngle1);
				bL2 = new LineType(C2.Center, bAngle2);

				bP1 = PolarPoint(C1.Center, bAngle1, C1.Radius);
				bP2 = PolarPoint(C2.Center, bAngle2, C2.Radius);
				LineLineIntersect(bL1, bL2, out bPc);

				//--------
				// Paso 5: Determino cuál de las 2 variantes de fillet -A- o -B- corresponde al 
				//				 parámetro "UP".
				L = (new SegmentType(C1.Center, C2.Center)).ConvertToLine();
				aLN = PerperdicularLineAt(L, aPc);
				bLN = PerperdicularLineAt(L, bPc);

				LineLineIntersect(L, aLN, out aPP);
				LineLineIntersect(L, bLN, out bPP);

				aAng = PointPointAngle(aPP, aPc);
				bAng = PointPointAngle(bPP, bPc);

				if ((MayorOrEqual(aAng, 0.0) && MenorEstricto(aAng, 180.0)) && (MayorOrEqual(bAng, 180.0) && MenorEstricto(bAng, 360.0)))
				{
					// En el juego de datos 'A' tengo los puntos de fillet que esta por encima (up) del segmento -dist-.
					upP1 = aP1;
					upP2 = aP2;
					upPc = aPc;
					// En el juego de datos 'B' tengo los puntos de fillet que esta por debajo (down) del segmento -dist-.
					doP1 = bP1;
					doP2 = bP2;
					doPc = bPc;
				}
				else
				{
					// En el juego de datos 'B' tengo los puntos de fillet que esta por encima (up) del segmento -dist-.
					upP1 = bP1;
					upP2 = bP2;
					upPc = bPc;
					// En el juego de datos 'A' tengo los puntos de fillet que esta por debajo (down) del segmento -dist-.
					doP1 = aP1;
					doP2 = aP2;
					doPc = aPc;
				}

				//--------
				// Paso 5.1: Obtengo el juego de puntos del fillet según el valor de "UP".
				if (up)
				{
					P1 = upP1;
					P2 = upP2;
					Pc = upPc;
				}
				else
				{
					P1 = doP1;
					P2 = doP2;
					Pc = doPc;
				}

				// Compruebo antes de salir si realice algun intercambio. De haberlo hecho, restituyo 
				// el orden de los puntos, asi garantizo que P1 pertenece al C1 y P2 pertenece a C2.
				if (swap) SwapPoint(ref P1, ref P2);				
			}
		}

		// Función Privada. Calcula el Fillet <CIRCLE-CIRCLE> para el Caso: (Up-Down)_absoluto, C1_outside, C2_inside.
		private static void CC_Fillet_Type_X10 (CircleType C1, CircleType C2, double r_fillet, out PointType P1, out PointType P2, out PointType Pc, bool up = true)
		{
			CircleType c1, c2;
			LineType L, aLN, bLN;
			double dist, r_max, aAng, bAng;
			PointType upP1, upP2, upPc, doP1, doP2, aP1, aP2, aPc, bP1, bP2, bPc, doPc, aPP, bPP, p1, p2;

			//--------
			// Paso 1: Calculo distancia entre centros y radio maximo posible. 
			dist = PointPointDistance(C1.Center, C2.Center);
			r_max = Math.Abs(C2.Radius - ((C1.Radius + C2.Radius) - dist)) / 2.0;

			//--------
			// Paso 2: Verifico y valido el r_fillet.
			if (isEqualCero(r_fillet))
				throw new FilletException("No se puede obtener un fillet con un radio de valor 0.");

			if (MayorEstricto(r_fillet, r_max))
				throw new FilletException("Radio muy grande. No se puede obtener un fillet con el valor de radio dado.");

			//--------
			// Paso 3: Calculo las dos variantes de fillet que se dan para el caso [X, 1, 1].
			c1 = new CircleType(C1.Center, C1.Radius + r_fillet);
			c2 = new CircleType(C2.Center, C2.Radius - r_fillet);
			CircleCircleIntersect(c1, c2, out aPc, out bPc);

			// a) Variante del fillet posiblemente arriba.
			aP1 = PolarPoint(C1.Center, PointPointAngle(C1.Center, aPc), C1.Radius);
			aP2 = PolarPoint(C2.Center, PointPointAngle(C2.Center, aPc), C2.Radius);

			// b) Variante del fillet posiblemente abajo.
			bP1 = PolarPoint(C1.Center, PointPointAngle(C1.Center, bPc), C1.Radius);
			bP2 = PolarPoint(C2.Center, PointPointAngle(C2.Center, bPc), C2.Radius);

			//--------
			// Paso 4: Determino cuál de las 2 variantes de fillet -A- o -B- corresponde al 
			//				 parámetro "UP".
			L = (new SegmentType(C1.Center, C2.Center)).ConvertToLine();
			aLN = PerperdicularLineAt(L, aPc);
			bLN = PerperdicularLineAt(L, bPc);

			LineLineIntersect(L, aLN, out aPP);
			LineLineIntersect(L, bLN, out bPP);

			aAng = PointPointAngle(aPP, aPc);
			bAng = PointPointAngle(bPP, bPc);

			if ((MayorOrEqual(aAng, 0.0) && MenorEstricto(aAng, 180.0)) && (MayorOrEqual(bAng, 180.0) && MenorEstricto(bAng, 360.0)))
			{
				// En el juego de datos 'A' tengo los puntos de fillet que esta por encima (up) del segmento -dist-.
				upP1 = aP1;
				upP2 = aP2;
				upPc = aPc;
				// En el juego de datos 'B' tengo los puntos de fillet que esta por debajo (down) del segmento -dist-.
				doP1 = bP1;
				doP2 = bP2;
				doPc = bPc;
			}
			else
			{
				// En el juego de datos 'B' tengo los puntos de fillet que esta por encima (up) del segmento -dist-.
				upP1 = bP1;
				upP2 = bP2;
				upPc = bPc;
				// En el juego de datos 'A' tengo los puntos de fillet que esta por debajo (down) del segmento -dist-.
				doP1 = aP1;
				doP2 = aP2;
				doPc = aPc;
			}

			//--------
			// Paso 5: Obtengo el juego de puntos del fillet según el valor de "UP".
			if (up)
			{
				P1 = upP1;
				P2 = upP2;
				Pc = upPc;
			}
			else
			{
				P1 = doP1;
				P2 = doP2;
				Pc = doPc;
			}
		}

		// Función Privada. Calcula el Fillet <CIRCLE-CIRCLE> para el Caso: (Up-Down)_absoluto, C1_inside, C2_outside.
		private static void CC_Fillet_Type_X01 (CircleType C1, CircleType C2, double r_fillet, out PointType P1, out PointType P2, out PointType Pc, bool up = true)
		{
			CircleType c1, c2;
			LineType L, aLN, bLN;
			double dist, r_max, aAng, bAng;
			PointType upP1, upP2, upPc, doP1, doP2, aP1, aP2, aPc, bP1, bP2, bPc, doPc, aPP, bPP, p1, p2;

			//--------
			// Paso 1: Calculo distancia entre centros y radio maximo posible. 
			dist = PointPointDistance(C1.Center, C2.Center);
			r_max = Math.Abs(C2.Radius - ((C1.Radius + C2.Radius) - dist)) / 2.0;

			//--------
			// Paso 2: Verifico y valido el r_fillet.
			if (isEqualCero(r_fillet))
				throw new FilletException("No se puede obtener un fillet con un radio de valor 0.");

			if (MayorEstricto(r_fillet, r_max))
				throw new FilletException("Radio muy grande. No se puede obtener un fillet con el valor de radio dado.");

			//--------
			// Paso 3: Calculo las dos variantes de fillet que se dan para el caso [X, 1, 1].
			c1 = new CircleType(C1.Center, C1.Radius - r_fillet);
			c2 = new CircleType(C2.Center, C2.Radius + r_fillet);
			CircleCircleIntersect(c1, c2, out aPc, out bPc);

			// a) Variante del fillet posiblemente arriba.
			aP1 = PolarPoint(C1.Center, PointPointAngle(C1.Center, aPc), C1.Radius);
			aP2 = PolarPoint(C2.Center, PointPointAngle(C2.Center, aPc), C2.Radius);

			// b) Variante del fillet posiblemente abajo.
			bP1 = PolarPoint(C1.Center, PointPointAngle(C1.Center, bPc), C1.Radius);
			bP2 = PolarPoint(C2.Center, PointPointAngle(C2.Center, bPc), C2.Radius);

			//--------
			// Paso 4: Determino cuál de las 2 variantes de fillet -A- o -B- corresponde al 
			//				 parámetro "UP".
			L = (new SegmentType(C1.Center, C2.Center)).ConvertToLine();
			aLN = PerperdicularLineAt(L, aPc);
			bLN = PerperdicularLineAt(L, bPc);

			LineLineIntersect(L, aLN, out aPP);
			LineLineIntersect(L, bLN, out bPP);

			aAng = PointPointAngle(aPP, aPc);
			bAng = PointPointAngle(bPP, bPc);

			if ((MayorOrEqual(aAng, 0.0) && MenorEstricto(aAng, 180.0)) && (MayorOrEqual(bAng, 180.0) && MenorEstricto(bAng, 360.0)))
			{
				// En el juego de datos 'A' tengo los puntos de fillet que esta por encima (up) del segmento -dist-.
				upP1 = aP1;
				upP2 = aP2;
				upPc = aPc;
				// En el juego de datos 'B' tengo los puntos de fillet que esta por debajo (down) del segmento -dist-.
				doP1 = bP1;
				doP2 = bP2;
				doPc = bPc;
			}
			else
			{
				// En el juego de datos 'B' tengo los puntos de fillet que esta por encima (up) del segmento -dist-.
				upP1 = bP1;
				upP2 = bP2;
				upPc = bPc;
				// En el juego de datos 'A' tengo los puntos de fillet que esta por debajo (down) del segmento -dist-.
				doP1 = aP1;
				doP2 = aP2;
				doPc = aPc;
			}

			//--------
			// Paso 5: Obtengo el juego de puntos del fillet según el valor de "UP".
			if (up)
			{
				P1 = upP1;
				P2 = upP2;
				Pc = upPc;
			}
			else
			{
				P1 = doP1;
				P2 = doP2;
				Pc = doPc;
			}
		}

		// Función Privada. Calcula el Fillet <CIRCLE-CIRCLE> para el Caso: (Up-Down)_absoluto, C1_inside, C2_inside.
		private static void CC_Fillet_Type_X00 (CircleType C1, CircleType C2, double r_fillet, out PointType P1, out PointType P2, out PointType Pc, bool up = true)
		{
			CircleType c1, c2;
			LineType L, aLN, bLN;
			double dist, r_max, aAng, bAng;
			PointType upP1, upP2, upPc, doP1, doP2, aP1, aP2, aPc, bP1, bP2, bPc, doPc, aPP, bPP, p1, p2;

			//--------
			// Paso 1: Calculo distancia entre centros y radio maximo posible. 
			dist = PointPointDistance(C1.Center, C2.Center);
			r_max = ((C1.Radius + C2.Radius) - dist) / 2.0;

			//--------
			// Paso 2: Verifico y valido el r_fillet.
			if (isEqualCero(r_fillet))
				throw new FilletException("No se puede obtener un fillet con un radio de valor 0.");

			if (MayorEstricto(r_fillet, r_max))
				throw new FilletException("Radio muy grande. No se puede obtener un fillet con el valor de radio dado.");

			//--------
			// Paso 3: Calculo las dos variantes de fillet que se dan para el caso [X, 0, 0].
			c1 = new CircleType(C1.Center, C1.Radius - r_fillet);
			c2 = new CircleType(C2.Center, C2.Radius - r_fillet);
			CircleCircleIntersect(c1, c2, out aPc, out bPc);

			// a) Variante del fillet posiblemente arriba.
			aP1 = PolarPoint(C1.Center, PointPointAngle(C1.Center, aPc), C1.Radius);
			aP2 = PolarPoint(C2.Center, PointPointAngle(C2.Center, aPc), C2.Radius);

			// b) Variante del fillet posiblemente abajo.
			bP1 = PolarPoint(C1.Center, PointPointAngle(C1.Center, bPc), C1.Radius);
			bP2 = PolarPoint(C2.Center, PointPointAngle(C2.Center, bPc), C2.Radius);

			//--------
			// Paso 4: Determino cuál de las 2 variantes de fillet -A- o -B- corresponde al 
			//				 parámetro "UP".
			L = (new SegmentType(C1.Center, C2.Center)).ConvertToLine();
			aLN = PerperdicularLineAt(L, aPc);
			bLN = PerperdicularLineAt(L, bPc);

			LineLineIntersect(L, aLN, out aPP);
			LineLineIntersect(L, bLN, out bPP);

			aAng = PointPointAngle(aPP, aPc);
			bAng = PointPointAngle(bPP, bPc);

			if ((MayorOrEqual(aAng, 0.0) && MenorEstricto(aAng, 180.0)) && (MayorOrEqual(bAng, 180.0) && MenorEstricto(bAng, 360.0)))
			{
				// En el juego de datos 'A' tengo los puntos de fillet que esta por encima (up) del segmento -dist-.
				upP1 = aP1;
				upP2 = aP2;
				upPc = aPc;
				// En el juego de datos 'B' tengo los puntos de fillet que esta por debajo (down) del segmento -dist-.
				doP1 = bP1;
				doP2 = bP2;
				doPc = bPc;
			}
			else
			{
				// En el juego de datos 'B' tengo los puntos de fillet que esta por encima (up) del segmento -dist-.
				upP1 = bP1;
				upP2 = bP2;
				upPc = bPc;
				// En el juego de datos 'A' tengo los puntos de fillet que esta por debajo (down) del segmento -dist-.
				doP1 = aP1;
				doP2 = aP2;
				doPc = aPc;
			}

			//--------
			// Paso 5: Obtengo el juego de puntos del fillet según el valor de "UP".
			if (up)
			{
				P1 = upP1;
				P2 = upP2;
				Pc = upPc;
			}
			else
			{
				P1 = doP1;
				P2 = doP2;
				Pc = doPc;
			}
		}

		/// <summary>
		/// Calcula el fillet (EMPALME) entre un arco de circunferencia "A" y un segmento de recta "S" dados, con
		/// radio determinado por "r_fillet". 
		/// </summary>
		/// <param name="A">
		/// Representa el arco de circunferecia.
		/// </param>
		/// <param name="S">
		/// Representa el segmento de recta.
		/// </param>
		/// <param name="r_fillet">
		/// Representa el radio del fillet.
		/// </param>
		/// <param name="P1">
		/// Parámetro de salida (out). Retorna por referencia el punto del arc-fillet que pertenece a la circunferenca.
		/// </param>
		/// <param name="P2">
		/// Parámetro de salida (out). Retorna por referencia el punto del arc-fillet que pertenece al segmento de recta.
		/// </param>
		/// <param name="Pc">
		/// Parámetro de salida (out). Retorna por referencia el punto centro del arc-fillet.
		/// </param>
		/// <param name="right">
		/// Indica hacia que lado del segmento "S" dado se calcula el fillet (empalme). Los lados quedan determinados por 
		/// la recta que parte del centro del arco "A" y es perpendicular al segmento "S". Si el valor es <b>true</b>
		/// el fillet se calcula hacia la derecha del segmento "S", por el contrario si el valor es <b>false</b> el fillet se
		/// calcula hacia la izquierda de dico segmento.
		/// </param>
		/// <param name="outside">
		/// Indica si el fillet (empalme) se calcula exterior o interior a la circunferencia imaginaria a la pertenece el arco
		/// "A" dado. Si el valor es <b>true</b> el fillet se calcula exterior a la circunferencia, por el contrario si el
		/// valor es <b>false</b> el fillet se calcula hacia el interior de la circunferencia imaginaria.
		/// </param>
		/// <param name="up">
		/// Indica si el fillet se calcula sobre el segmento "S" dado o por debajo de él. Si el valor es <b>true</b> el fillet
		/// se calcula sobre el segmento "S" dado, por el contrario si el valor es <b>false</b> el fillet se calcula por debajo 
		/// de dicho segmento.
		/// </param>
		/// <returns>
		/// Devuelve un tipo void. Por referencia mediante los parámetros P1, P2 y Pc se obtienen los 3 
		/// puntos del arc-fillet.
		/// </returns>
		/// <remarks>
		/// Nota: "P1" pertenece al arco, "P2" pertenece al segmento de recta y "Pc" es el centro del arco.
		/// </remarks> 
		/// <seealso cref="ArcType"/>
		/// <seealso cref="SegmentType"/>
		/// <seealso cref="PointType"/>
		public static void ArcSegmentFillet (ArcType A, SegmentType S, double r_fillet, out PointType P1, out PointType P2, out PointType Pc, 
			bool right = true, bool outside = true, bool up = true)
		{
			var C = new CircleType(A.Center, A.Radius);

			CircleSegmentFillet(C, S, r_fillet, out P1, out P2, out Pc, right, outside, up);
		}

		/// <summary>
		/// Calcula el fillet (EMPALME) entre un arco de circunferencia "A" y un segmento de recta "S" dados, con
		/// radio determinado por "r_fillet". 
		/// </summary>
		/// <param name="A">
		/// Representa el arco de circunferecia.
		/// </param>
		/// <param name="S">
		/// Representa el segmento de recta.
		/// </param>
		/// <param name="r_fillet">
		/// Representa el radio del fillet.
		/// </param>
		/// <param name="right">
		/// Indica hacia que lado del segmento "S" dado se calcula el fillet (empalme). Los lados quedan determinados por 
		/// la recta que parte del centro del arco "A" y es perpendicular al segmento "S". Si el valor es <b>true</b>
		/// el fillet se calcula hacia la derecha del segmento "S", por el contrario si el valor es <b>false</b> el fillet se
		/// calcula hacia la izquierda de dico segmento.
		/// </param>
		/// <param name="outside">
		/// Indica si el fillet (empalme) se calcula exterior o interior a la circunferencia imaginaria a la pertenece el arco
		/// "A" dado. Si el valor es <b>true</b> el fillet se calcula exterior a la circunferencia, por el contrario si el
		/// valor es <b>false</b> el fillet se calcula hacia el interior de la circunferencia imaginaria.
		/// </param>
		/// <param name="up">
		/// Indica si el fillet se calcula sobre el segmento "S" dado o por debajo de él. Si el valor es <b>true</b> el fillet
		/// se calcula sobre el segmento "S" dado, por el contrario si el valor es <b>false</b> el fillet se calcula por debajo 
		/// de dicho segmento.
		/// </param>
		/// <returns>
		/// Devuelve un tipo Arco que representa el fillet calculado para el arco de circunferencia y el segmento de recta dados.
		/// </returns>
		public static ArcType ArcSegmentFillet (ArcType A, SegmentType S, double r_fillet, bool right = true, bool outside = true, bool up = true)
		{
			var C = new CircleType(A.Center, A.Radius);

			return CircleSegmentFillet(C, S, r_fillet, right, outside, up);
		}

		/// <summary>
		/// Calcula el fillet (EMPALME) entre dos arcos de circunferencias "A1" y "A2" dados, con radio determinado por 
		/// "r_fillet". 
		/// </summary>
		/// <param name="A1">
		/// Representa el 1er arco de circunferecia.
		/// </param>
		/// <param name="A2">
		/// Representa el 2do arco de circunferencia.
		/// </param>
		/// <param name="r_fillet">
		/// Representa el radio del fillet.
		/// </param>
		/// <param name="P1">
		/// Parámetro de salida (out). Retorna por referencia el punto del arc-fillet que pertenece a la circunferenca.
		/// </param>
		/// <param name="P2">
		/// Parámetro de salida (out). Retorna por referencia el punto del arc-fillet que pertenece al segmento de recta.
		/// </param>
		/// <param name="Pc">
		/// Parámetro de salida (out). Retorna por referencia el punto centro del arc-fillet.
		/// </param>
		/// <param name="right_up">
		/// Indica hacia que lado se calcula el fillet, tomando como referencia el segmento imaginario "Sx" que se forma  
		/// entre los centros de ambos arcos.<br/>
		/// Nota: Si el valor es <b>true</b>, el fillet se calcula hacia la 
		/// derecha-o-arriba del segmento "Sx". Por el contrario si el valor es <b>false</b>, el fillet se calcula hacia 
		/// la izquierda-o-abajo del segmento.
		/// </param>
		/// <param name="a1_outside">
		/// Indica si el fillet se calcula hacia adentro o hacia afuera de la circunferencia imaginaria que forma el arco "A1".<br/>
		/// Nota: Si el valor es <b>true</b>, el fillet se calcula hacia el exterior de la circunferencia. Por el 
		/// contrario si el valor es <b>false</b>, el fillet se calcula hacia el interior de la circunferencia. En ambos 
		/// casos su aplicación es sobre la circunferencia "C1".
		/// </param>
		/// <param name="a2_outside">
		/// Indica si el fillet se calcula hacia adentro o hacia afuera de la circunferencia imaginaria que forma el arco "A2".<br/>
		/// Nota: Si el valor es <b>true</b>, el fillet se calcula hacia el exterior de la circunferencia. Por el 
		/// contrario si el valor es <b>false</b>, el fillet se calcula hacia el interior de la circunferencia. En ambos 
		/// casos su aplicación es sobre la circunferencia "C2".
		/// </param>
		/// <returns>
		/// Devuelve un tipo void. Por referencia mediante los parámetros P1, P2 y Pc se obtienen los 3 
		/// puntos del arc-fillet. 
		/// </returns>
		/// <remarks>
		/// Nota: "P1" pertenece al arco "A1", "P2" pertenece al arco "A2" y "Pc" es el centro del arco.
		/// </remarks>
		/// <seealso cref="ArcType"/>
		/// <seealso cref="PointType"/>
		public static void ArcArcFillet (ArcType A1, ArcType A2, double r_fillet, out PointType P1, out PointType P2, out PointType Pc,
			bool right_up = true, bool a1_outside = true, bool a2_outside = true)
		{
			CircleType C1, C2;

			C1 = new CircleType(A1.Center, A1.Radius);
			C2 = new CircleType(A2.Center, A2.Radius);

			CircleCircleFillet(C1, C2, r_fillet, out P1, out P2, out Pc, right_up, a1_outside, a2_outside);
		}

		/// <summary>
		/// Calcula el fillet (EMPALME) entre dos arcos de circunferencias "A1" y "A2" dados, con radio determinado por 
		/// "r_fillet". 
		/// </summary>
		/// <param name="A1">
		/// Representa el 1er arco de circunferecia.
		/// </param>
		/// <param name="A2">
		/// Representa el 2do arco de circunferencia.
		/// </param>
		/// <param name="r_fillet">
		/// Representa el radio del fillet.
		/// </param>
		/// <param name="right_up">
		/// Indica hacia que lado se calcula el fillet, tomando como referencia el segmento imaginario "Sx" que se forma  
		/// entre los centros de ambos arcos.<br/>
		/// Nota: Si el valor es <b>true</b>, el fillet se calcula hacia la 
		/// derecha-o-arriba del segmento "Sx". Por el contrario si el valor es <b>false</b>, el fillet se calcula hacia 
		/// la izquierda-o-abajo del segmento.
		/// </param>
		/// <param name="a1_outside">
		/// Indica si el fillet se calcula hacia adentro o hacia afuera de la circunferencia imaginaria que forma el arco "A1".<br/>
		/// Nota: Si el valor es <b>true</b>, el fillet se calcula hacia el exterior de la circunferencia. Por el 
		/// contrario si el valor es <b>false</b>, el fillet se calcula hacia el interior de la circunferencia. En ambos 
		/// casos su aplicación es sobre la circunferencia "C1".
		/// </param>
		/// <param name="a2_outside">
		/// Indica si el fillet se calcula hacia adentro o hacia afuera de la circunferencia imaginaria que forma el arco "A2".<br/>
		/// Nota: Si el valor es <b>true</b>, el fillet se calcula hacia el exterior de la circunferencia. Por el 
		/// contrario si el valor es <b>false</b>, el fillet se calcula hacia el interior de la circunferencia. En ambos 
		/// casos su aplicación es sobre la circunferencia "C2".
		/// </param>
		/// <returns>
		/// Devuelve un tipo Arco que representa el fillet calculado los dos arcos de circunferencias dados.
		/// </returns>
		public static ArcType ArcArcFillet(ArcType A1, ArcType A2, double r_fillet, bool right_up = true, bool a1_outside = true, bool a2_outside = true)
		{
			CircleType C1, C2;

			C1 = new CircleType(A1.Center, A1.Radius);
			C2 = new CircleType(A2.Center, A2.Radius);

			return CircleCircleFillet(C1, C2, r_fillet, right_up, a1_outside, a2_outside);
		}
		#endregion

		#region - Functions of Chamfer's Calculation -
		/// <summary>
		/// 
		/// </summary>
		/// <param name="S1">
		/// 
		/// </param>
		/// <param name="S2">
		/// 
		/// </param>
		/// <param name="dist1">
		/// 
		/// </param>
		/// <param name="dist2">
		/// 
		/// </param>
		/// <param name="P1">
		/// 
		/// </param>
		/// <param name="P2">
		/// 
		/// </param>
		/// <returns>
		/// 
		/// </returns>
		public static int SegmentSegmentChamfer(SegmentType S1, SegmentType S2, ChamferDistante dist1, ChamferDistante dist2, out PointType P1, out PointType P2)
		{
			throw new NotImplementedException("El método X no esta implementado aun!!!");

			/*bool isAngSwap;
			PointType Pv, p1, p2;
			double ang1, ang2, a1, a2, dist, Sine1, Cosine1, Sine2, Cosine2, a_fillet;

			isAngSwap = false;

			p1 = new PointType();
			p2 = new PointType();

			//--------
			// Paso 2: Determino el punto de interseccion de los segmentos.
			bool intersect = SegmentsApparentIntersect(S1, S2, out Pv);

			ang1 = isEqualPoint(Pv, S1.StartPoint) ? PointPointAngle(Pv, S1.EndPoint) : PointPointAngle(Pv, S1.StartPoint);
			ang2 = isEqualPoint(Pv, S2.StartPoint) ? PointPointAngle(Pv, S2.EndPoint) : PointPointAngle(Pv, S2.StartPoint);

			//--------
			// Paso 3: Compruebo si las lineas son colineales o paralelas.
			if (!intersect)
			{
				P1 = P2 = null;
				//angFillet = 0;

				return 0;
			}

			//--------
			// Paso 4: Si ang1 es mayor que ang2 los intercambio para que 
			//				 ang2 siempre sea mayor que ang1.
			if (ang1 > ang2)
			{
				isAngSwap = true;
				SwapValue(ref ang1, ref ang2);
			}

			//--------
			// Paso 5: Calculo el ángulo complementario con "ang1"
			a1 = ComplementaryAngle(ang1);
			/*if (ang1 <= 90.0)
				a1 = 90.0 - ang1;
			else if ((ang1 > 90.0) && (ang1 <= 180.0))
				a1 = ang1 - 90.0;
			else if ((ang1 > 180.0) && (ang1 <= 270.0))
				a1 = 270.0 - ang1;
			else
				a1 = ang1 - 270.0;*/

			//--------
			// Paso 6: Calculo el ángulo complementario con "ang2"
			/*if (ang2 <= 90.0)
				a2 = 90.0 - ang2;
			else if ((ang2 > 90.0) && (ang2 <= 180.0))
				a2 = ang2 - 90.0;
			else if ((ang2 > 180.0) && (ang2 <= 270.0))
				a2 = 270.0 - ang2;
			else
				a2 = ang2 - 270.0;

			// ???
			// ángulo para el cálculo del chamfer
			a_fillet = (ang2 - ang1) > 180.0 ? (ang2 - ang1) - 180.0 : 180.0 - (ang2 - ang1);
			//angFillet = a_fillet;

			//--------
			// Paso 8: Calculo los valores de las funciones trigonometricas y los 
			//					valores de longitud necesarias.
			Sine1 = Math.Sin(GradToRad(a1));
			Cosine1 = Math.Cos(GradToRad(a1));
			Sine2 = Math.Sin(GradToRad(a2));
			Cosine2 = Math.Cos(GradToRad(a2));

			//--------
			// Paso 9: Establesco las coordenadas de los puntos P2, P2
			p1.cX = (ang1 <= 90.0) || (ang1 >= 270.0) ? Pv.cX + Sine1 * dist1 : Pv.cX - Sine1 * dist1;
			p1.cY = (ang1 <= 180.0) ? Pv.cY + Cosine1 * dist1 : Pv.cY - Cosine1 * dist1;
			p1.cZ = 0.0;

			p2.cX = (ang2 <= 90.0) || (ang2 >= 270.0) ? Pv.cX + Sine2 * dist2 : Pv.cX - Sine2 * dist2;
			p2.cY = (ang2 <= 180.0) ? Pv.cY + Cosine2 * dist2 : Pv.cY - Cosine2 * dist2;
			p2.cZ = 0.0;

			if (isAngSwap) SwapPoint(ref p1, ref p2);

			P1 = p1;
			P2 = p2;

			return 1;*/
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="S1"></param>
		/// <param name="S2"></param>
		/// <param name="dist"></param>
		/// <param name="angle"></param>
		/// <param name="P1"></param>
		/// <param name="P2"></param>
		/// <returns></returns>
		public static int SegmentSegmentChamfer (SegmentType S1, SegmentType S2, ChamferDistante dist, ChamferAngle angle, out PointType P1, out PointType P2)
		{
			throw new NotImplementedException("El método X no esta implementado aun!!!");
		}
		#endregion

		#region - Functions of Algebraical's Calculation -
		/// <summary>
		/// Determina los coeficientes de la ecuacion general de la recta (Ax + By + C = 0) para el segmento 'S' dado.
		/// </summary>
		/// <param name="S">
		/// Representa el segmento.
		/// </param>
		/// <param name="A">
		/// Parámetro de salida (out). Retorna por referencia el valor del coeficiente A.
		/// </param>
		/// <param name="B">
		/// Parámetro de salida (out). Retorna por referencia el valor del coeficiente B.
		/// </param>
		/// <param name="C">
		/// Parámetro de salida (out). Retorna por referencia el valor del coeficiente C.
		/// </param>
		/// <returns>
		/// Devuelve un tipo void. Por referencia mediante los parámetros A, B, C se obtienen los coeficientes 
		/// de la ecuación.
		/// </returns>.	
		public static void LineCoefficient (SegmentType S, out double A, out double B, out double C)
		{
			LineCoefficient(S.ConvertToLine(), out A, out B, out C); 
		}

		/// <summary>
		/// Determina los coeficientes de la ecuacion general de la recta (Ax + By + C = 0) para la recta 'L' dada.
		/// </summary>
		/// <param name="L">
		/// Representa el segmento.
		/// </param>
		/// <param name="A">
		/// Parámetro de salida (out). Retorna por referencia el valor del coeficiente A.
		/// </param>
		/// <param name="B">
		/// Parámetro de salida (out). Retorna por referencia el valor del coeficiente B.
		/// </param>
		/// <param name="C">
		/// Parámetro de salida (out). Retorna por referencia el valor del coeficiente C.
		/// </param>
		/// <returns>
		/// Devuelve un tipo void. Por referencia mediante los parámetros A, B, C se obtienen los coeficientes 
		/// de la ecuación.
		/// </returns>.
		public static void LineCoefficient (LineType L, out double A, out double B, out double C)
		{
			// La línea es paralela al eje de las Ordenadas (Eje Y).
			if (isEqualCero(L.Angle - 90.0) || isEqualCero(L.Angle - 270.0))
			{
				A = 1.0;
				B = 0.0;
				C = -(L.P.cX);
			}
			// La línea es paralela al eje de las Abcisas (Eje X).
			else if (isEqualCero(L.Angle) || isEqualCero(L.Angle - 180.0) || isEqualCero(L.Angle - 360.0))
			{
				A = 0.0;
				B = 1.0;
				C = -(L.P.cY);
			}
			// La línea no es paralela a ningunos de los ejes de coordenas.
			else
			{
				A = -(Math.Tan(GradToRad(L.Angle)));
				B = 1.0;
				C = -(L.P.cY) + L.P.cX * Math.Tan(GradToRad(L.Angle));
			}
		}

		/// <summary>
		/// Determina los coeficientes de la ecuación general de la circunferencia (x^2 + y^2 + Ax + By + C = 0)
		/// para la circunferencia 'Cir' dada.
		/// </summary>
		/// <param name="Cir">
		/// Representa la circunferencia.
		/// </param>
		/// <param name="A">
		/// Parámetro de salida (out). Retorna por referencia el valor del coeficiente A.
		/// </param>
		/// <param name="B">
		/// Parámetro de salida (out). Retorna por referencia el valor del coeficiente B.
		/// </param>
		/// <param name="C">
		/// Parámetro de salida (out). Retorna por referencia el valor del coeficiente C.
		/// </param>
		/// <returns>
		/// Devuelve un tipo void. Por referencia mediante los parámetros A, B, C se obtienen los coeficientes 
		/// de la ecuación.
		/// </returns>
		public static void CircleCoefficient (CircleType Cir, out double A, out double B, out double C)
		{
			A = - 2.0 * Cir.Center.cX;
			B = - 2.0 * Cir.Center.cY;
			C = - Cir.Radius * Cir.Radius + Cir.Center.cX * Cir.Center.cX + Cir.Center.cY * Cir.Center.cY;
		}

		/// <summary>
		/// Determina los coeficientes de la ecuación general de la elipse (Ax^2 + By^2 + Cx + Dy + E = 0)
		/// para la elipse 'Elip' dada. 
		/// </summary>
		/// <param name="Et"></param>
		/// <param name="A"></param>
		/// <param name="B"></param>
		/// <param name="C"></param>
		/// <param name="D"></param>
		/// <param name="E"></param>
		/// <returns>
		/// Devuelve un tipo void. Por referencia mediante los parámetros A, B, C, D, E se obtienen los coeficientes 
		/// de la ecuación.
		/// </returns>
		public static void ElipseCoefficient (ElipseType Elip, out double A, out double B, out double C, out double D, out double E)
		{
			double h, k, a, b;

			h = Elip.Center.cX;
			k = Elip.Center.cY;
			a = Elip.SemiEjeA;
			b = Elip.SemiEjeB;

			A = b * b;
			B = a * a;
			C = -2.0 * h * b * b;
			D = -2.0 * k * a * a;
			E = (h * h * b * b) + (k * k * a * a) - (a * a * b * b);
		}

		/// <summary>
		/// Determina los coeficientes de la ecuación canónica de la elipse (Ax^2 + By^2 + E = 0)
		/// para la elipse 'Elip' dada. 
		/// </summary>
		/// <param name="Elip"></param>
		/// <param name="A"></param>
		/// <param name="B"></param>
		/// <param name="C"></param>
		/// <returns>
		/// Devuelve un tipo void. Por referencia mediante los parámetros A, B, E se obtienen los coeficientes 
		/// de la ecuación.
		/// </returns>
		public static void CanonicElipseCoefficient(ElipseType Elip, out double A, out double B, out double C)
		{
			double a, b;

			a = Elip.SemiEjeA;
			b = Elip.SemiEjeB;

			A = b * b;
			B = a * a;
			C = -a * a * b * b;
		}

		/// <summary>
		/// Resuelve un sistema de ecuaciones de 2 con 2 por el método de sustitución, 
		/// despejando 'X' en la ecuación lineal para luego sustituirla en la ecuación
		/// cuadrática. De lo que se obtiene un polinomio de la forma: mx^2 + px + q.<br/><br/>
		/// <b>Nota:</b> Los parámetros A, B, C, A1, B1, C1 se interpretan de la siguiente forma:<br/>
		/// - A, B, C: coeficiente de una ecuación lineal de la forma Ax + By + C = 0
		/// - A1, B1, C1: coeficientes de una ecuación cuadrática X^2 + y^2 + A1x + B1y + C1 = 0
		/// </summary>
		/// <param name="A">
		/// Coeficiente de la abcisa (X) de la ecuación lineal.
		/// </param>
		/// <param name="B">
		/// Coeficiente de la ordenada (Y) de la ecuación lineal.
		/// </param>
		/// <param name="C">
		/// Coeficiente independiente de la ecuación lineal.
		/// </param>
		/// <param name="A1">
		/// Coeficiente de la abcisa (X) de la ecuación cuadrática.
		/// </param>
		/// <param name="B1">
		/// Coeficiente de la ordenada (Y) de la ecuación cuadrática.
		/// </param>
		/// <param name="C1">
		/// Coeficiente independiente de la ecuación cuadrática.
		/// </param>
		/// <param name="m">
		/// Parámetro de salida (out). Retorna por referencia el valor del coeficiente cuadrático 
		/// </param>
		/// <param name="p">
		/// Parámetro de salida (out). Retorna por referencia el valor
		/// </param>
		/// <param name="q">
		/// Parámetro de salida (out). Retorna por referencia el valor
		/// </param>
		/// <returns>
		/// Devuelve un tipo void. Por referencia mediante los parámetros m, p, q se obtienen los coeficientes 
		/// para un polinomio de la forma mx^2 + px + q.
		/// </returns>
		public static void MakeMPQx (double A, double B, double C, double A1, double B1, double C1, out double m, out double p, out double q)
		{
			m = (B * B) / (A * A) + 1;
			p = (2 * -B * -C) / (A * A) + A1 * -B / A + B1;
			q = (C * C) / (A * A) + A1 * -C / A + C1;
		}

		/// <summary>
		/// Resuelve un sistema de ecuaciones de 2 con 2 por el método de sustitución, 
		/// despejando 'Y' en la ecuación lineal para luego sustituirla en la ecuación
		/// cuadrática. De lo que se obtiene un polinomio de la forma: mx^2 + px + q.
		/// <b>Nota:</b> Los parámetros A, B, C, A1, B1, C1 se interpretan de la siguiente forma:<br/>
		/// - A, B, C: coeficiente de una ecuación lineal de la forma Ax + By + C = 0
		/// - A1, B1, C1: coeficientes de una ecuación cuadrática X^2 + y^2 + A1x + B1y + C1 = 0
		/// </summary>
		/// <param name="A">
		/// 
		/// </param>
		/// <param name="B">
		/// 
		/// </param>
		/// <param name="C">
		/// 
		/// </param>
		/// <param name="A1">
		/// 
		/// </param>
		/// <param name="B1">
		/// 
		/// </param>
		/// <param name="C1">
		/// 
		/// </param>
		/// <param name="m">
		/// Parámetro de salida (out). Retorna por referencia el valor
		/// </param>
		/// <param name="p">
		/// Parámetro de salida (out). Retorna por referencia el valor
		/// </param>
		/// <param name="q">
		/// Parámetro de salida (out). Retorna por referencia el valor
		/// </param>
		/// <returns>
		/// Devuelve un tipo void. Por referencia mediante los parámetros m, p, q se obtienen los coeficientes 
		/// para un polinomio de la forma mx^2 + px + q.
		/// </returns>
		public static void MakeMPQy (double A, double B, double C, double A1, double B1, double C1, out double m, out double p, out double q)
		{
			m = (A * A) / (B * B) + 1;
			p = (2 * -A * -C) / (B * B) + B1 * -A / B + A1;
			q = (C * C) / (B * B) + B1 * -C / B + C1;
		}

		/// <summary>
		/// Calcula las raíces de polinomios de 2do orden que tienen la forma: mx^2 + px + q por el método del Descriminante.
		/// </summary>
		/// <param name="m">
		/// 
		/// </param>
		/// <param name="p">
		/// 
		/// </param>
		/// <param name="q">
		/// 
		/// </param>
		/// <param name="r1">
		/// Parámetro de salida (out). Retorna por referencia el valor
		/// </param>
		/// <param name="r2">
		/// Parámetro de salida (out). Retorna por referencia el valor
		/// </param>
		/// <returns>
		/// Devuelve un tipo void. Por referencia mediante los parámetros "r1" y "r2" se obtienen las raices
		/// del polinomio calculadas.
		/// </returns> 
		public static byte RootMPQ (double m, double p, double q, out double r1, out double r2)
		{
			double Delta = p * p - 4 * m * q;

			if ((Delta < 0.0 || double.IsNaN(Delta)) && !isEqualCero(Delta))
			{
				r1 = r2 = 0.0;

				return 0;
			}

			if (isEqualCero(Delta))
			{
				r1 = r2 = -p / (2.0 * m);

				return 1;
			}

			r1 = (-p - Math.Sqrt(Delta)) / (2.0 * m);
			r2 = (-p + Math.Sqrt(Delta)) / (2.0 * m);

			return 2;
		}
		#endregion
	}
	#endregion

	#region - Pseudo Facade Class -
		#region - Facade class for Polyline Entity -
			/// <summary>
			/// Clase fachada que representa el elemento de una polilínea.
			/// </summary>
			public class PolylineElement
			{
				private PointType P;
				private SegmentType S;
				private ArcType A;
				private PolylineElementType Type;

				/// <summary>
				/// Constuctor que define el elemento como un Tipo Punto.
				/// </summary>
				/// <param name="p">
				/// Representa el pun
				/// </param>
				public PolylineElement (PointType p)
				{
					P = p;
					S = null;
					A = null;
					Type = PolylineElementType.Point;
				}

				/// <summary>
				/// Constuctor que define el elemento como un Tipo Segmento.
				/// </summary>
				/// <param name="s">
				/// </param>
				public PolylineElement (SegmentType s)
				{
					S = s;
					P = null;
					A = null;
					Type = PolylineElementType.Segment;
				}

				/// <summary>
				/// Constuctor que define el elemento como un Tipo Arco.
				/// </summary>
				/// <param name="a"></param>
				public PolylineElement (ArcType a)
				{
					A = a;
					P = null;
					S = null;
					Type = PolylineElementType.Arc;
				}

				/// <summary>
				/// Propiedad de solo lectura. Devuelve un Tipo Punto.
				/// </summary>
				public PointType Point
				{
					get { return P; }
				}

				/// <summary>
				/// Propiedad de solo lectura. Devuelve un Tipo Segmento. 
				/// </summary>
				public SegmentType Segment
				{
					get { return S; }
				}

				/// <summary>
				/// Propiedad de solo lectura. Devuelve un Tipo Arco. 
				/// </summary>
				public ArcType Arc
				{
					get { return A; }
				}

				/// <summary>
				/// Devuelve un enum que indica el tipo de elemento que contiene, el cual puede ser: un punto, un segmento o un arco.
				/// </summary>
				public PolylineElementType TypeElement 
				{
					get { return Type; }
				}
			}
			#endregion

		#region - Facade class for ArcType Entity -
		/// <summary>
		/// Representa el punto de inicio (START_POINT) de un Arco. 
		/// </summary>
		public struct ArcStartPoint
		{
			/// <summary>
			/// 
			/// </summary>
			public PointType val { set; get; }

			/// <summary>
			///  
			/// </summary>
			/// <param name="x">
			/// 
			/// </param>
			/// <param name="y">
			/// 
			/// </param>
			public ArcStartPoint (double x, double y) 
				: this()
			{
				val = new PointType(x, y);
			}

			/// <summary>
			///  
			/// </summary>
			/// <param name="x">
			/// 
			/// </param>
			/// <param name="y">
			/// 
			/// </param>
			/// <param name="z">
			/// 
			/// </param>
			public ArcStartPoint(double x, double y, double z)
				: this()
			{
				val = new PointType(x, y, z);
			}

			/// <summary>
			///  
			/// </summary>
			/// <param name="p">
			/// 
			/// </param>
			public ArcStartPoint (PointType p)
				: this()
			{
				val = p;
			}
		}
	
		/// <summary>
		/// Representa el punto final (END_POINT) de un Arco. 
		/// </summary>
		public struct ArcEndPoint
		{
			/// <summary>
			///  
			/// </summary>
			public PointType val { set; get; }

			/// <summary>
			///  
			/// </summary>
			/// <param name="x">
			/// 
			/// </param>
			/// <param name="y">
			/// 
			/// </param>
			public ArcEndPoint (double x, double y)
				: this()
			{
				val = new PointType(x, y);
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="x">
			/// 
			/// </param>
			/// <param name="y">
			/// 
			/// </param>
			/// <param name="z">
			/// 
			/// </param>
			public ArcEndPoint (double x, double y, double z)
				: this()
			{
				val = new PointType(x, y, z);
			}

			/// <summary>
			///  
			/// </summary>
			/// <param name="p">
			/// 
			/// </param>
			public ArcEndPoint (PointType p)
				: this()
			{
				val = p;
			}
		}

		/// <summary>
		/// Representa el punto del centro (CENTER_POINT) de un Arco. 
		/// </summary>
		public struct ArcCenterPoint
		{
			/// <summary>
			/// 
			/// </summary>
			public PointType val { set; get; }

			/// <summary>
			/// 
			/// </summary>
			/// <param name="x">
			/// 
			/// </param>
			/// <param name="y">
			/// 
			/// </param>
			public ArcCenterPoint (double x, double y)
				: this()
			{
				val = new PointType(x, y);
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="x">
			/// 
			/// </param>
			/// <param name="y">
			/// 
			/// </param>
			/// <param name="z">
			/// 
			/// </param>
			public ArcCenterPoint (double x, double y, double z)
				: this()
			{
				val = new PointType(x, y, z);
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="p">
			/// 
			/// </param>
			public ArcCenterPoint (PointType p)
				: this()
			{
				val = p;
			}
		}

		/// <summary>
		/// Representa un punto cualquiera (ANY_POINT) de un Arco. 
		/// </summary>
		public struct ArcAnyPoint
		{
			/// <summary>
			/// 
			/// </summary>
			public PointType val { set; get; }

			/// <summary>
			/// 
			/// </summary>
			/// <param name="x">
			/// 
			/// </param>
			/// <param name="y">
			/// 
			/// </param>
			public ArcAnyPoint (double x, double y)
				: this()
			{
				val = new PointType(x, y);
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="x">
			/// 
			/// </param>
			/// <param name="y">
			/// 
			/// </param>
			/// <param name="z">
			/// 
			/// </param>
			public ArcAnyPoint (double x, double y, double z)
				: this()
			{
				val = new PointType(x, y, z);
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="p">
			/// 
			/// </param>
			public ArcAnyPoint (PointType p)
				: this()
			{
				val = p;
			}
		}

		/// <summary>
		/// Representa el ángulo en grados de un Arco. 
		/// </summary>
		public struct ArcGradeAngle
		{
			/// <summary>
			/// 
			/// </summary>
			public double val { set; get; }

			/// <summary>
			/// 
			/// </summary>
			/// <param name="a">
			/// 
			/// </param>
			public ArcGradeAngle (double a)
				: this()
			{
				val = a;
			}

			/// <summary>
			/// 
			/// </summary>
			/// <returns>
			/// 
			/// </returns>
			public double ToRadian
			{
				get { return is2GraphObj.GradToRad(val); }
			}
		}

		/// <summary>
		/// Representa la distancia o longitud de la cuerda de un Arco. 
		/// </summary>
		public struct ArcDistance
		{
			/// <summary>
			///  
			/// </summary>
			public double val { set; get; }

			/// <summary>
			///  
			/// </summary>
			/// <param name="d">
			/// 
			/// </param>
			public ArcDistance (double d)
				: this()
			{
				val = d;
			}
		}

		/// <summary>
		/// Representa el radio de un Arco. 
		/// </summary>
		public struct ArcRadius
		{
			/// <summary>
			/// 
			/// </summary>
			public double val { set; get; }

			/// <summary>
			/// 
			/// </summary>
			/// <param name="r">
			/// 
			/// </param>
			public ArcRadius (double r)
				: this()
			{
				val = r;
			}
		}

		/// <summary>
		/// Representa la dirección de un arco. Esta dirección esta dada por un punto 
		/// en el plano.
		/// </summary>
		public struct ArcVectorDirection
		{
			/// <summary>
			/// 
			/// </summary>
			public PointType val { set; get; }

			/// <summary>
			/// 
			/// </summary>
			/// <param name="x">
			/// 
			/// </param>
			/// <param name="y">
			/// 
			/// </param>
			public ArcVectorDirection (double x, double y)
				: this()
			{
				val = new PointType(x, y);
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="x">
			/// 
			/// </param>
			/// <param name="y">
			/// 
			/// </param>
			/// <param name="z">
			/// 
			/// </param>
			public ArcVectorDirection (double x, double y, double z)
				: this()
			{
				val = new PointType(x, y, z);
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="p">
			/// 
			/// </param>
			public ArcVectorDirection(PointType p)
				: this()
			{
				val = p;
			}
		}
		#endregion

		#region - Facade class for Chamfer Operations -
		/// <summary>
		///  
		/// </summary>
		public struct ChamferAngle
		{
			/// <summary>
			/// 
			/// </summary>
			public double val { set; get; }

			/// <summary>
			/// 
			/// </summary>
			/// <param name="angle">
			/// 
			/// </param>
			public ChamferAngle (double angle)
				: this()
			{
				val = angle;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public struct ChamferDistante
		{
			/// <summary>
			/// 
			/// </summary>
			public double val { set; get; }

			/// <summary>
			/// 
			/// </summary>
			/// <param name="dist">
			/// 
			/// </param>
			public ChamferDistante(double dist)
				: this()
			{
				val = dist;
			}
		}
		#endregion
	#endregion

	#region - Exceptions Class -
	/// <summary>
	/// Representa una exception para el tipo Circle Error.
	/// </summary>
	public class CircleException : Exception
	{
		/// <summary>
		/// Constructor por defecto.
		/// </summary>
		public CircleException ()
		{
		}

		/// <summary>
		/// Constructor que toma como parámetro un tipo String.
		/// </summary>
		/// <param name="msg">
		/// Representa una cadena de caracteres que indica la naturaleza de la exception.
		/// </param>
		/// <seealso cref="System.String"/>
		public CircleException (string msg)
			: base(msg)
		{
		}
	}

	/// <summary>
	/// Representa una exception para el tipo Polyline Error.
	/// </summary>
	public class PolylineException : Exception
	{
		/// <summary>
		/// Constructor por defecto.
		/// </summary>
		public PolylineException()
		{
		}

		/// <summary>
		/// Constructor que toma como parámetro un tipo String.
		/// </summary>
		/// <param name="msg">
		/// Representa una cadena de caracteres que indica la naturaleza de la exception.
		/// </param>
		/// <seealso cref="System.String"/>
		public PolylineException(string msg)
			: base(msg)
		{
		}
	}

	/// <summary>
	/// Representa una exception de tipo Arc Error. 
	/// </summary>
	public class ArcException : Exception
	{
		/// <summary>
		/// Constructor por defecto. 
		/// </summary>
		public ArcException ()
		{
		}

		/// <summary>
		/// Representa una exception de tipo Arc Error. 
		/// </summary>
		/// <param name="msg">
		/// 
		/// </param>
		/// <seealso cref="System.String"/>
		public ArcException (string msg)
			: base(msg)
		{
		}
	}

	/// <summary>
	/// Representa una exception de tipo Polygon Error. 
	/// </summary>
	public class PolygonException : Exception
	{
		/// <summary>
		/// Constructor por defecto. 
		/// </summary>
		public PolygonException ()
		{
		}

		/// <summary>
		/// Representa una exception de tipo Arc Error. 
		/// </summary>
		/// <param name="msg">
		/// 
		/// </param>
		/// <seealso cref="System.String"/>
		public PolygonException (string msg)
			: base(msg)
		{
		}
	}

	/// <summary>
	/// Representa una exception para el tipo Fillet Error.
	/// </summary>
	public class FilletException : Exception
	{
		/// <summary>
		/// Constructor por defecto.
		/// </summary>
		public FilletException()
		{
		}

		/// <summary>
		/// Constructor que toma como parámetro un tipo String.
		/// </summary>
		/// <param name="msg">
		/// Representa una cadena de caracteres que indica la naturaleza de la exception.
		/// </param>
		/// <seealso cref="System.String"/>
		public FilletException(string msg)
			: base(msg)
		{
		}
	}
	#endregion
}