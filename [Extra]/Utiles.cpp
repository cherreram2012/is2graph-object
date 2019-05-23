
#include <stdlib.h>
#include <math.h>
#include <iostream.h>
#include <conio.h>
#include <stdio.h>
#include <string.h>

#include "rxdefs.h"
#include "adslib.h"
#include "adesk.h"
#include "Bombas.h"
#include "is2graph.h"

#define M_PI 3.1415926535897932
#define DELTA 1.0    //Angulo de Incremento para el Trazado de la Voluta
#define INICIOA 65.0   //Angulo Inicial para el Trazado de la Voluta ANTI-HORARIA


ads_point pCentro;
/*
extern double ancho_canal, s_discos, axis, s_pared, dist_ep, angle_ep;

extern double Holgura_axial(void);
extern double Rho(double angle);
extern double r3(void);

extern int tornillo;
*/
extern Centrifugas *BC;  // P U N T E R O

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
double Grad2Rad ( double AngGrad ) {   // Grados a Radianes
    return (AngGrad * M_PI / 180.0);
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
double Rad2Grad ( double AngRad ) {    // Radianes a Grados
    return (AngRad * 180.0 / M_PI);
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
int CircIntercep ( CircleType C1, CircleType C2, PointType *p1, PointType *p2 ) {
    
	SCALAR term1, term2;
	SCALAR a, b, c, d, s, Beta, Cita;

            a = C1.Radius;
			b = C2.Radius;
	    term1 = (C2.Pc.CoordX - C1.Pc.CoordX) * (C2.Pc.CoordX - C1.Pc.CoordX);
        term2 = (C2.Pc.CoordY - C1.Pc.CoordY) * (C2.Pc.CoordY - C1.Pc.CoordY);
            c = sqrt( term1 + term2 );
			s = ( a + b + c ) / 2.0;
			d = sqrt( (s - a) * (s - b) * (s - c) / s );
		 Beta = 2.0 * atan( d / (s- b) );
   
   Cita = atan ( (C2.Pc.CoordY - C1.Pc.CoordY) / (C2.Pc.CoordX - C1.Pc.CoordX) );

   p1->CoordX = C1.Pc.CoordX + C1.Radius * cos ( (Cita + Beta) );
   p1->CoordY = C1.Pc.CoordY + C1.Radius * sin ( (Cita + Beta) );
   p2->CoordX = C2.Pc.CoordX + C2.Radius * cos ( (Cita - Beta) );
   p2->CoordY = C2.Pc.CoordY + C2.Radius * sin ( (Cita - Beta) );

   if ( p1 && p2) return 2;  //Circunsferencias que se cortan en 2 Puntos
   if ( p1 || p2) return 1;  //Circunsferencias tangentes en un Punto

   return 0;                 //Circunsferencias que no se cortan

}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//Función para validar si los círculos se cortan y devolver el punto de intercepción cercano al centro
//int PuntoValido ( int k, TipoPunto p1, TipoPunto p2, ads_point pcv ) {
int PuntoValidoCirculo ( int k, PointType pt1, PointType pt2, ads_point pCentro, ads_point pcv ) {
	
  double d1, d2;

  ads_point pd1, pd2;
	
  pd1[X] = pt1.CoordX;
  pd1[Y] = pt1.CoordY;
  pd1[Z] = pt1.CoordZ;

  pd2[X] = pt2.CoordX;
  pd2[Y] = pt2.CoordY;
  pd2[Z] = pt2.CoordZ;

  if ( k == 2) {
     d1 = ads_distance(pd1, pCentro);
     d2 = ads_distance(pd2, pCentro);

     if ( d1 > d2 ) {
	 // if ( fabs(p1.CoordY) >  fabs(p2.CoordY)) {
	    pcv[X] = pt2.CoordX;
		pcv[Y] = pt2.CoordY;
		pcv[Z] = pt2.CoordZ;
	 }
	 else {
	    pcv[X] = pt1.CoordX;
		pcv[Y] = pt1.CoordY;
		pcv[Z] = pt1.CoordZ;
	 }
	 return 1;
  }
  return 0;

}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
int PuntoValidoCirculoLinea ( int k, PointType Pto1, PointType Pto2, ads_point pcv ) {

  if ( k == 2) {
	 if ( fabs(Pto1.CoordX) > fabs(Pto2.CoordX) ) {
	    pcv[X] = Pto2.CoordX;
		pcv[Y] = Pto2.CoordY;
		pcv[Z] = Pto2.CoordZ;
	 }
	 else {
	    pcv[X] = Pto1.CoordX;
		pcv[Y] = Pto1.CoordY;
		pcv[Z] = Pto1.CoordZ;
	 }
	 return 1;
  }
  return 0;

}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
int PuntoValidoArcoLinea ( int k, PointType Pto1, PointType Pto2, ads_point pcv ) {

  if ( k == 2) {
	 if ( fabs(Pto1.CoordX) > fabs(Pto2.CoordX) ) {
	    pcv[X] = Pto2.CoordX;
		pcv[Y] = Pto2.CoordY;
		pcv[Z] = Pto2.CoordZ;
	 }
	 else {
	    pcv[X] = Pto1.CoordX;
		pcv[Y] = Pto1.CoordY;
		pcv[Z] = Pto1.CoordZ;
	 }
	 return 1;
  }
  return 0;

}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
void InterceptArcLinePatas ( ads_point sp,  ads_point ep, ads_point mp,
					         SCALAR VectorX,
					         PointType &PInt0, PointType &PInt1 ) {
    Arc3PType Arc;
	ArcType A;
	LineType L;

   if ( (sp[X] > VectorX) && (ep[X] < VectorX) ) {
//   if ( (sp[X] < VectorX) && (ep[X] > VectorX) ) {
	     Arc.Pi.CoordX = sp[X];
	     Arc.Pi.CoordY = sp[Y];
	     Arc.Pi.CoordZ = 0.0;
	     Arc.Pm.CoordX = mp[X];
	     Arc.Pm.CoordY = mp[Y];
	     Arc.Pm.CoordZ = 0.0;
	     Arc.Pf.CoordX = ep[X];
	     Arc.Pf.CoordY = ep[Y];
	     Arc.Pf.CoordZ = 0.0;

	     A = Arc3P2Arc (Arc);
	     L.P1.CoordX = VectorX;
	     L.P1.CoordY = ep[Y]; // Cualquier Y; 
         L.P1.CoordZ = 0.0;
	     L.Angle = 270.0;

	     ArcLineIntercept( A, L, &PInt0, &PInt1 );
   }
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
void DibujaPatas (ads_point p5, ads_point p6, ads_point p7, ads_point p10, ads_point p11,
				  ads_point p12, ads_point p13, ads_point p14, ads_point p15, ads_point p16,
				  ads_point p17, ads_point p18, ads_point p19, ads_point p20, ads_point p21,
				  ads_point p22, ads_point p23, ads_point p24, ads_point p25, ads_point p26,
				  ads_point p27, ads_point p28, ads_point p29, ads_point p30, ads_point p31,
				  ads_point p32, ads_point p33, ads_point p34, ads_point p35, ads_point p36,
				  ads_point p37, ads_point p38, ads_point p39,
				  int h1, int n1, int n2, int s1, int b,
				  double s_pared, double PataX1, double PataX2, double PataX3,
				  PointType PInt0, PointType PInt1) {

   ads_polar(pCentro, Grad2Rad(270.0), h1, p10);
   //Pata Izquierda
   ads_polar(p10, M_PI, n1 / 2.0, p11);
   ads_polar(p11, 0.0, b, p13);
   ads_polar(p11, Grad2Rad(90.0), s_pared, p15);
   ads_polar(p13, Grad2Rad(90.0), s_pared, p17);
   ads_polar(p10, M_PI, n2 / 2.0, p19);

   //Pata Izquierda
   ads_polar(p19, M_PI, s1 / 2.0, p21);
   ads_polar(p21, Grad2Rad(90.0), s_pared, p22);
   ads_polar(p19, 0.0, s1 / 2.0, p23);
   ads_polar(p23, Grad2Rad(90.0), s_pared, p24);
   ads_polar(p19, Grad2Rad(270.0), s_pared / 2.0, p25);
   ads_polar(p19, Grad2Rad(90.0), (s_pared / 2.0) + s_pared, p26);

   //Pata Izquierda
   ads_command (RTSTR, "PLINE", RTPOINT, p17, RTPOINT, p13, RTPOINT, p11, RTPOINT, p15, RTPOINT, p17, RTSTR, "", 0 );
   ads_command (RTSTR, "PLINE", RTPOINT, p21, RTPOINT, p22, RTSTR, "", 0 );
   ads_command (RTSTR, "PLINE", RTPOINT, p23, RTPOINT, p24, RTSTR, "", 0 );

   //Pata Derecha
   ads_polar(p10, 0.0, n1 / 2.0, p12);
   ads_polar(p12, M_PI, b, p14);
   ads_polar(p12, Grad2Rad(90.0), s_pared, p16);
   ads_polar(p14, Grad2Rad(90.0), s_pared, p18);
   ads_polar(p10, 0.0, n2 / 2.0, p20);

   //Pata Derecha
   ads_polar(p20, 0.0, s1 / 2.0, p27);
   ads_polar(p27, Grad2Rad(90.0), s_pared, p28);
   ads_polar(p20, M_PI, s1 / 2.0, p29);
   ads_polar(p29, Grad2Rad(90.0), s_pared, p30);
   ads_polar(p20, Grad2Rad(270.0), s_pared / 2.0, p31);
   ads_polar(p20, Grad2Rad(90.0), (s_pared / 2.0) + s_pared, p32);

   //Pata Derecha
   ads_command (RTSTR, "PLINE", RTPOINT, p18, RTPOINT, p14, RTPOINT, p12, RTPOINT, p16, RTPOINT, p18, RTSTR, "", 0 );
   ads_command (RTSTR, "PLINE", RTPOINT, p27, RTPOINT, p28, RTSTR, "", 0 );
   ads_command (RTSTR, "PLINE", RTPOINT, p29, RTPOINT, p30, RTSTR, "", 0 );

   //Líneas de Centro de Agujeros
   ads_command (RTSTR, "LAYER", RTSTR, "S", RTSTR, "CENTROS", RTSTR, "", 0 );
   ads_command (RTSTR, "PLINE", RTPOINT, p25, RTPOINT, p26, RTSTR, "", 0 );
   ads_command (RTSTR, "PLINE", RTPOINT, p31, RTPOINT, p32, RTSTR, "", 0 );
   ads_command (RTSTR, "LAYER", RTSTR, "S", RTSTR, "0", RTSTR, "", 0 );

   //PATA Derecha
   //Intercepción Vertical línea interior
   p33[X] = PInt1.CoordX;
   p33[Y] = PInt1.CoordY;

   ads_polar(p10, 0.0, PataX1, p32);
//   ads_command (RTSTR, "PLINE", RTPOINT, p32, RTPOINT, p33, RTSTR, "", 0 );

   //Intercepción Vertical línea exterior
   p33[X] = PInt0.CoordX;
   p33[Y] = PInt0.CoordY;
   ads_polar(p10, 0.0, PataX2, p38);
//   ads_command (RTSTR, "PLINE", RTPOINT, pc38, RTPOINT, pc33, RTSTR, "", 0 );

   //Intercepción Vertical línea interior más espesor
   p33[X] = PInt0.CoordX;
   p33[Y] = PInt0.CoordY;
   ads_polar(p10, 0.0, PataX3, p39);
//   ads_command (RTSTR, "PLINE", RTPOINT, pc39, RTPOINT, pc33, RTSTR, "", 0 );

   //PATA Izquierda
   //Intercepción Vertical línea interior
   p33[X] = PInt0.CoordX;
   p33[Y] = PInt0.CoordY;
   ads_polar(p10, M_PI, PataX1, p5);
//   ads_command (RTSTR, "PLINE", RTPOINT, pc5, RTPOINT, pc33, RTSTR, "", 0 );

   //Intercepción Vertical línea exterior
   p33[X] = PInt0.CoordX;
   p33[Y] = PInt0.CoordY;
   ads_polar(p10, M_PI, PataX2, p6);
//   ads_command (RTSTR, "PLINE", RTPOINT, pc6, RTPOINT, pc33, RTSTR, "", 0 );

   //Intercepción Vertical línea interior más espesor
   p33[X] = PInt0.CoordX;
   p33[Y] = PInt0.CoordY;
   ads_polar(p10, M_PI, PataX3, p7);
//   ads_command (RTSTR, "PLINE", RTPOINT, pc7, RTPOINT, pc33, RTSTR, "", 0 );

}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//Intercepción Arco Línea Tetón Vertical Anti-horario
void InterceptArcLineTetonVerAnti ( ads_point sp,  ads_point ep, ads_point mp,
					                SCALAR VectorX,
					                PointType &PInt0, PointType &PInt1 ) {
    Arc3PType Arc;
	ArcType A;
	LineType L;

   if ( (sp[X] > VectorX) && (ep[X] < VectorX) ) {
	     Arc.Pi.CoordX = sp[X];
	     Arc.Pi.CoordY = sp[Y];
	     Arc.Pi.CoordZ = 0.0;
	     Arc.Pm.CoordX = mp[X];
	     Arc.Pm.CoordY = mp[Y];
	     Arc.Pm.CoordZ = 0.0;
	     Arc.Pf.CoordX = ep[X];
	     Arc.Pf.CoordY = ep[Y];
	     Arc.Pf.CoordZ = 0.0;

	     A = Arc3P2Arc (Arc);
	     L.P1.CoordX = VectorX;
	     L.P1.CoordY = ep[Y]; // Cualquier Y; 
         L.P1.CoordZ = 0.0;
	     L.Angle = 270.0;

	     ArcLineIntercept( A, L, &PInt0, &PInt1 );
   }
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//Intercepción Arco Línea Tetón Vertical Horario
void InterceptArcLineTetonVerHor ( ads_point sp,  ads_point ep, ads_point mp,
					               SCALAR VectorX,
					               PointType &PInt0, PointType &PInt1 ) {
    Arc3PType Arc;
	ArcType A;
	LineType L;

   if ( (sp[X] < VectorX) && (ep[X] > VectorX) ) {
	     Arc.Pi.CoordX = sp[X];
	     Arc.Pi.CoordY = sp[Y];
	     Arc.Pi.CoordZ = 0.0;
	     Arc.Pm.CoordX = mp[X];
	     Arc.Pm.CoordY = mp[Y];
	     Arc.Pm.CoordZ = 0.0;
	     Arc.Pf.CoordX = ep[X];
	     Arc.Pf.CoordY = ep[Y];
	     Arc.Pf.CoordZ = 0.0;

	     A = Arc3P2Arc (Arc);
	     L.P1.CoordX = VectorX;
	     L.P1.CoordY = sp[Y]; // Cualquier Y; 
         L.P1.CoordZ = 0.0;
	     L.Angle = 270.0;

	     ArcLineIntercept( A, L, &PInt0, &PInt1 );
   }
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//Dibuja Tetón Vertical Anti-Horario
void DibujaTetonVerAnti ( ads_point pc1, ads_point pc2, ads_point pc3, ads_point pc4,
					      ads_point pc5, ads_point pc6, ads_point pc7,
				          PointType PInt0, PointType PInt1, PointType PInt2,
				          double s_pared ) {

     pc1[X] = PInt0.CoordX;
     pc1[Y] = PInt0.CoordY;
     pc2[X] = PInt1.CoordX;
     pc2[Y] = PInt0.CoordY;
     pc3[X] = PInt1.CoordX;
     pc3[Y] = PInt1.CoordY;

//	 ads_command (RTSTR, "PLINE", RTPOINT, pCentro, RTPOINT, pc1, RTSTR, "", 0 );
//	 ads_command (RTSTR, "PLINE", RTPOINT, pCentro, RTPOINT, pc3, RTSTR, "", 0 );

	 //Radios de acuerdo del Tetón
     ads_polar(pc2, M_PI, 3.0, pc7);
     ads_polar(pc2, Grad2Rad(270.0), 3.0, pc6);

	 // Contorno del Tetón
     ads_command (RTSTR, "PLINE", RTPOINT, pc1, RTPOINT, pc7, RTSTR, "A", RTPOINT, pc6, RTSTR, "L",
	              RTPOINT, pc3, RTSTR, "", 0 );

  	 //Punto superior línea de centro del Tetón
     pc5[X] = PInt2.CoordX;
     pc5[Y] = PInt0.CoordY + 5.0;

	 //Punto inferior línea de centro del Tetón
     pc4[X] = PInt2.CoordX;
     pc4[Y] = PInt2.CoordY - (s_pared * 1.5);

	 //Línea de Centro del Tetón (Roja)
     ads_command (RTSTR, "LAYER", RTSTR, "S", RTSTR, "CENTROS", RTSTR, "", 0 );
     ads_command (RTSTR, "LINE", RTPOINT, pc4, RTPOINT, pc5, RTSTR, "", 0);
     ads_command (RTSTR, "LAYER", RTSTR, "S", RTSTR, "0", RTSTR, "", 0 );

}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//Dibuja Tetón Vertical Horario
void DibujaTetonVerHor ( ads_point pc1, ads_point pc2, ads_point pc3, ads_point pc4,
					     ads_point pc5, ads_point pc6, ads_point pc7,
				         PointType PInt0, PointType PInt1, PointType PInt2,
				         double s_pared ) {

     pc1[X] = PInt0.CoordX;
     pc1[Y] = PInt0.CoordY;
     pc2[X] = PInt1.CoordX;
     pc2[Y] = PInt0.CoordY;
     pc3[X] = PInt1.CoordX;
     pc3[Y] = PInt1.CoordY;

//	 ads_command (RTSTR, "PLINE", RTPOINT, pCentro, RTPOINT, pc1, RTSTR, "", 0 );
//	 ads_command (RTSTR, "PLINE", RTPOINT, pCentro, RTPOINT, pc3, RTSTR, "", 0 );

	 //Radios de acuerdo del Tetón
     ads_polar(pc2, 0.0, 3.0, pc7);
     ads_polar(pc2, Grad2Rad(270.0), 3.0, pc6);

	 // Contorno del Tetón
     ads_command (RTSTR, "PLINE", RTPOINT, pc1, RTPOINT, pc7, RTSTR, "A", RTPOINT, pc6, RTSTR, "L",
	              RTPOINT, pc3, RTSTR, "", 0 );

  	 //Punto superior línea de centro del Tetón
     pc5[X] = PInt2.CoordX;
     pc5[Y] = PInt0.CoordY + 5.0;

	 //Punto inferior línea de centro del Tetón
     pc4[X] = PInt2.CoordX;
     pc4[Y] = PInt2.CoordY - (s_pared * 1.5);

	 //Línea de Centro del Tetón (Roja)
     ads_command (RTSTR, "LAYER", RTSTR, "S", RTSTR, "CENTROS", RTSTR, "", 0 );
     ads_command (RTSTR, "LINE", RTPOINT, pc4, RTPOINT, pc5, RTSTR, "", 0);
     ads_command (RTSTR, "LAYER", RTSTR, "S", RTSTR, "0", RTSTR, "", 0 );

}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//Intercepción Arco Línea Tetón Horizontal Anti-Horario
void InterceptArcLineTetonHorAnti ( ads_point sp,  ads_point ep, ads_point mp,
					                SCALAR VectorY,
					                PointType &PInt0, PointType &PInt1 ) {
    Arc3PType Arc;
	ArcType A;
	LineType L;

     if (sp[Y] <= VectorY && ep[Y] >= VectorY) {
	     Arc.Pi.CoordX = sp[X];
	     Arc.Pi.CoordY = sp[Y];
	     Arc.Pi.CoordZ = 0.0;
	     Arc.Pm.CoordX = mp[X];
	     Arc.Pm.CoordY = mp[Y];
	     Arc.Pm.CoordZ = 0.0;
	     Arc.Pf.CoordX = ep[X];
	     Arc.Pf.CoordY = ep[Y];
	     Arc.Pf.CoordZ = 0.0;

	     A = Arc3P2Arc (Arc);
	     L.P1.CoordX = pCentro[X];
	     L.P1.CoordY = VectorY;
         L.P1.CoordZ = 0.0;
	     L.Angle = 0.0;

	     ArcLineIntercept( A, L, &PInt0, &PInt1 );

   }
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//Intercepción Arco Línea Tetón Horizontal Horario
void InterceptArcLineTetonHorHor ( ads_point sp,  ads_point ep, ads_point mp,
					               SCALAR VectorY,
					               PointType &PInt0, PointType &PInt1 ) {
    Arc3PType Arc;
	ArcType A;
	LineType L;

     if (sp[Y] <= VectorY && ep[Y] >= VectorY) {
	     Arc.Pi.CoordX = sp[X];
	     Arc.Pi.CoordY = sp[Y];
	     Arc.Pi.CoordZ = 0.0;
	     Arc.Pm.CoordX = mp[X];
	     Arc.Pm.CoordY = mp[Y];
	     Arc.Pm.CoordZ = 0.0;
	     Arc.Pf.CoordX = ep[X];
	     Arc.Pf.CoordY = ep[Y];
	     Arc.Pf.CoordZ = 0.0;

	     A = Arc3P2Arc (Arc);
	     L.P1.CoordX = pCentro[X];
	     L.P1.CoordY = VectorY;
         L.P1.CoordZ = 0.0;
	     L.Angle = 0.0;

	     ArcLineIntercept( A, L, &PInt0, &PInt1 );

   }
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
void DibujaTetonHorDer ( ads_point pc1, ads_point pc2, ads_point pc3, ads_point pc4, ads_point pc5,
                         ads_point pc6, ads_point pc7,  ads_point pc8, ads_point p34,
				         PointType PInt0, PointType PInt1,
				         double TetonY4 ) {

     pc1[X] = PInt0.CoordX;    //Intercepción abajo delante
     pc1[Y] = PInt0.CoordY;

     pc2[X] = PInt0.CoordX + 12.0; //Intercepción abajo detrás
     pc2[Y] = PInt0.CoordY;

     pc3[X] = PInt1.CoordX;    //Intercepción arriba delante
     pc3[Y] = PInt1.CoordY;

     ads_polar(pc2, Grad2Rad(90.0), TetonY4 * 2.0, pc4); //arriba detrás (así porque sino queda inclinado el Tetón)

     //Radios de acuerdo del Tetón
     ads_polar(pc2, M_PI, 3.0, pc5);
     ads_polar(pc2, Grad2Rad(90.0), 3.0, pc6);
     ads_polar(pc4, Grad2Rad(270.0), 3.0, pc7);
     ads_polar(pc4, M_PI, 3.0, pc8);

  	 p34[X] = pc7[X];
	 p34[Y] = pc7[Y];
	 p34[Z] = pc7[Z];

   	 //Contorno completo del Tetón Horizontal
     ads_command (RTSTR, "PLINE", RTPOINT, pc1, RTPOINT, pc5, RTSTR, "A", RTPOINT, pc6,  RTSTR, "L",
	              RTPOINT, pc7,  RTSTR, "A", RTPOINT, pc8,  RTSTR, "L",RTPOINT, pc3, RTSTR, "", 0 );
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
void DibujaTetonHorIzq ( ads_point pc1, ads_point pc2, ads_point pc3, ads_point pc4, ads_point pc5,
                         ads_point pc6, ads_point pc7,  ads_point pc8, ads_point p34,
				         PointType PInt0, PointType PInt1,
				         double TetonY4 ) {

     pc1[X] = PInt0.CoordX;    //Intercepción abajo delante
     pc1[Y] = PInt0.CoordY;
//        ads_command (RTSTR, "LINE", RTPOINT, pCentro, RTPOINT, pc1, RTSTR, "", 0);

     pc2[X] = PInt0.CoordX - 12.0; //Intercepción abajo detrás
     pc2[Y] = PInt0.CoordY;

     pc3[X] = PInt1.CoordX;    //Intercepción arriba delante
     pc3[Y] = PInt1.CoordY;

     ads_polar(pc2, Grad2Rad(90.0), TetonY4 * 2.0, pc4); //arriba detrás (así porque sino queda inclinado el Tetón)

     //Radios de acuerdo del Tetón
     ads_polar(pc2, 0.0, 3.0, pc5);
     ads_polar(pc2, Grad2Rad(90.0), 3.0, pc6);
     ads_polar(pc4, Grad2Rad(270.0), 3.0, pc7);
     ads_polar(pc4, 0.0, 3.0, pc8);

  	 p34[X] = pc7[X];
	 p34[Y] = pc7[Y];
	 p34[Z] = pc7[Z];

   	 //Contorno completo del Tetón Horizontal
     ads_command (RTSTR, "PLINE", RTPOINT, pc1, RTPOINT, pc5, RTSTR, "A", RTPOINT, pc6,  RTSTR, "L",
	              RTPOINT, pc7,  RTSTR, "A", RTPOINT, pc8,  RTSTR, "L",RTPOINT, pc3, RTSTR, "", 0 );
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
double DistAgujeros ( int n ) {
	if (n % 3)
     	return ( 360.0 / n ) / 2.0;  //para n = 4 y 8
	    return ( 360.0 / n );        //para n = 6 y 12
}

void KillCero (char * Str) {
     char * Ptr = Str;
     register unsigned Len;
	 
    if (strchr (Str, '.')) {
	    Len = strlen (Str);
	    while (--Len)  
              if (*(Ptr + Len) == '0')
		      	  *(Ptr + Len) = '\0';
			  else if (*(Ptr + Len) == '.') {
				       *(Ptr + Len) = '\0';
				       break;
		           }
		           else break;
    }
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
   //B R I D A   D E   D E S C A R G A
void BridaDescarga ( ads_point pc7, ads_point pc8, ads_point pc9, ads_point p10, ads_point p11, ads_point p12,
					 ads_point p13, ads_point p14, ads_point p15, ads_point p16, ads_point p17, ads_point p18,
					 ads_point p19, ads_point p20, ads_point p21, ads_point p22, ads_point p23, ads_point p24,
					 ads_point p25, ads_point p26, ads_point p27, ads_point p28, ads_point p29, ads_point p30,
					 ads_point p31, ads_point p32, ads_point p33, ads_point p34, ads_point p35, ads_point p36,
					 ads_point p37, ads_point p38, ads_point p39, ads_point p40, ads_point p41, ads_point p42,
 					 ads_point p43, ads_point p44, ads_point p45, ads_point p46, ads_point p47,
			         int EspesorBrida, int h2, int DNr, int D, int dc, int da,
					 double s_pared, double bisel ) {

   ads_polar(pCentro, Grad2Rad (90.0), h2, p31);
   ads_polar(p31, Grad2Rad (270.0), EspesorBrida, p30);
   ads_polar(p31, 0.0, (DNr / 2.0) + s_pared, pc7);
   ads_polar(p31, M_PI, (DNr / 2.0) + s_pared, pc8);
   ads_polar(pc7, Grad2Rad(270.0), EspesorBrida, pc9);
   ads_polar(pc8, Grad2Rad(270.0), EspesorBrida, p10);
   ads_polar(p31, 0.0, D / 2.0, p11);
   ads_polar(p31, M_PI, D / 2.0, p12);
   ads_polar(p11, Grad2Rad(270.0), EspesorBrida, p13);
   ads_polar(p12, Grad2Rad(270.0), EspesorBrida, p14);

   //Extremo de la DERECHA
   ads_polar(p31, 0.0, dc / 2.0, p15);
   ads_polar(p15, Grad2Rad(90.0), bisel, p16);
   ads_polar(p15, Grad2Rad(270.0), EspesorBrida + bisel, p17);
   ads_polar(p13, M_PI, 4.0, p21);            //Radio de Acuerdo derecha
   ads_polar(p13, Grad2Rad(90.0), 4.0, p22);  //Radio de Acuerdo derecha

   //Extremo de la IZQUIERDA
   ads_polar(p31, M_PI, dc / 2.0, p23);
   ads_polar(p23, Grad2Rad(90.0), bisel, p24);
   ads_polar(p23, Grad2Rad(270.0), EspesorBrida + bisel, p28);
   ads_polar(p14, Grad2Rad(90.0), 4.0, p25);  //Radio de Acuerdo izquierda
   ads_polar(p14, 0.0, 4.0, p26);             //Radio de Acuerdo izquierda

   //Perfil de la Brida
   ads_polar(p11, Grad2Rad(270.0), bisel, p32); //esquina derecha
   ads_polar(p12, Grad2Rad(270.0), bisel, p27); //esquina izquierda
   ads_polar(p31, Grad2Rad(270.0), bisel, p18);
   ads_polar(p18, 0.0, (dc / 2.0) - (da / 2.0), p19);
   ads_polar(p31, 0.0, (dc / 2.0) - (da / 2.0) - bisel, p20);
   ads_polar(p18, M_PI, (dc / 2.0) - (da / 2.0), p29);
   ads_polar(p31, M_PI, (dc / 2.0) - (da / 2.0) - bisel, p33);

   //////////////////////////////////////////////////////////////////////////
   //BRIDA CORTADA
   ads_polar(p31, 0.0, DNr / 2.0, p34);                 //1-18
   ads_polar(p31, M_PI, DNr / 2.0, p35);                //2-19
   ads_polar(p34, Grad2Rad(270.0), EspesorBrida, p36);  //3-5
   ads_polar(p35, Grad2Rad(270.0), EspesorBrida, p37);  //4-6
   ads_polar(p15, 0.0, da / 2.0, p38);                  //5-17
   ads_polar(p23, M_PI, da / 2.0, p39);                 //6-26
   ads_polar(p38, Grad2Rad(270.0), bisel, p40);         //7-34
   ads_polar(p39, Grad2Rad(270.0), bisel, p41);         //8-38
   ads_polar(p38, Grad2Rad(270.0), EspesorBrida, p42);  //9-21
   ads_polar(p39, Grad2Rad(270.0), EspesorBrida, p43);  //10-32
   ads_polar(p19, Grad2Rad(270.0), EspesorBrida - bisel, p44);
   ads_polar(p29, Grad2Rad(270.0), EspesorBrida - bisel, p45);
   //Arreglar estos puntos
   ads_polar(p36, 0.0, s_pared, p46);                    //11-11
   ads_polar(p37, M_PI, s_pared, p47);                   //12-13

}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//B R I D A   D E   S U C C I O N
void BridaSuccion ( ads_point pc6, ads_point pc7, ads_point pc8, ads_point pc9, ads_point p10, ads_point p11,
					ads_point p12, ads_point p13, ads_point p14, ads_point p15, ads_point p16, ads_point p17,
					ads_point p18, ads_point p19, ads_point p20, ads_point p21, ads_point p22, ads_point p23,
					ads_point p24, ads_point p25, ads_point p26, ads_point p27, ads_point p28, ads_point p29,
					ads_point p30, ads_point p31, ads_point p32, ads_point p33, ads_point p34, ads_point p35,
					ads_point p36, ads_point p37, ads_point p38, ads_point p39, ads_point p40, ads_point p41,
 					ads_point p42, ads_point p43, ads_point p44, ads_point p45, ads_point p46,
			        int EspesorBrida, int a, int DNa, int DS, int dcs, int das,
					double s_pared, double bisel ) {

   //Punto del centro de la brida de Succión (a la izquierda de pCentro)
   ads_polar(pCentro, M_PI, a, pc6);
   //Puntos de la brida de Succión (a la izquierda de pCentro)
   ads_polar(pc6, Grad2Rad (90.0), DNa / 2.0, pc7);
   ads_polar(pc6, Grad2Rad(270.0), DNa / 2.0, pc8);
   ads_polar(pc6, Grad2Rad (90.0), dcs / 2.0, pc9);
   ads_polar(pc6, Grad2Rad(270.0), dcs / 2.0, p10);
   ads_polar(pc6, Grad2Rad (90.0), DS / 2.0, p11);
   ads_polar(pc6, Grad2Rad(270.0), DS / 2.0, p12);
   ads_polar(pc6, Grad2Rad (90.0), (dcs / 2.0) - (das / 2.0), p13);
   ads_polar(pc6, Grad2Rad(270.0), (dcs / 2.0) - (das / 2.0), p14);
   ads_polar(p13, Grad2Rad(270.0), bisel, p15);
   ads_polar(p14, Grad2Rad (90.0), bisel, p16);
   ads_polar(p13, 0.0, bisel, p17);
   ads_polar(p14, 0.0, bisel, p18);
   ads_polar(p11, 0.0, bisel, p19);
   ads_polar(p12, 0.0, bisel, p20);
   ads_polar(p11, 0.0, EspesorBrida, p21);
   ads_polar(p12, 0.0, EspesorBrida, p22);

   ads_polar(p21, Grad2Rad(270.0), 4.0, p23);   //Radio de Acuerdo arriba
   ads_polar(p21, M_PI, 4.0, p24);              //Radio de Acuerdo arriba
   
   ads_polar(p22, M_PI, 4.0, p25);              //Radio de Acuerdo abajo
   ads_polar(p22, Grad2Rad(90.0), 4.0, p26);    //Radio de Acuerdo abajo

//   ads_polar(pc7, 0.0, EspesorBrida, p27);
//   ads_polar(pc8, 0.0, EspesorBrida, p28);

   ads_polar(p21, Grad2Rad(270.0), (DS / 2.0) - (DNa / 2.0) - s_pared, p27);
   ads_polar(p22, Grad2Rad (90.0), (DS / 2.0) - (DNa / 2.0) - s_pared, p28);

   //Agujero de ARRIBA
   ads_polar(pc6, Grad2Rad(90.0), dcs / 2.0, p29);        //Ver Norma
   ads_polar(p29, M_PI, EspesorBrida / 4.0, p30);
   ads_polar(p29, 0.0, bisel, p31);    //Ver Norma
   ads_polar(p31, Grad2Rad(90.0), das / 2.0, p32);  //Ver Norma
   ads_polar(p31, Grad2Rad(270.0), das / 2.0, p33);
   ads_polar(p29, 0.0, EspesorBrida, p34);
   ads_polar(p34, Grad2Rad(90.0), das / 2.0, p35);    //Ver Norma
   ads_polar(p34, Grad2Rad(270.0), das / 2.0, p36);  //Ver Norma
   ads_polar(p29, 0.0, EspesorBrida + (EspesorBrida / 4.0), p37);  //Ver Norma

   //Agujero de ABAJO
   ads_polar(pc6, Grad2Rad(270.0), dcs / 2.0, p38);        //Ver Norma
   ads_polar(p38, M_PI, EspesorBrida / 4.0, p39);
   ads_polar(p38, 0.0, bisel, p40);    //Ver Norma
   ads_polar(p40, Grad2Rad(90.0), das / 2.0, p41);  //Ver Norma
   ads_polar(p40, Grad2Rad(270.0), das / 2.0, p42);
   ads_polar(p38, 0.0, EspesorBrida, p43);
   ads_polar(p43, Grad2Rad(90.0), das / 2.0, p44);    //Ver Norma
   ads_polar(p43, Grad2Rad(270.0), das / 2.0, p45);  //Ver Norma
   ads_polar(p38, 0.0, EspesorBrida + (EspesorBrida / 4.0), p46);  //Ver Norma

}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
void DistribucionAgujerosBS (ads_point p5, ads_point p7, ads_point p8, ads_point p10, ads_point p11, ads_point p12,
							 ads_point p13, ads_point p14, ads_point p15, ads_point p16, ads_point p6, ads_point p19,
							 ads_point p9,
							 int ns, int DNa, int DS, int dcs, int das, int da, double bisel) {

   //Círculos concéntricos mirando la Brida de frente
   ads_command (RTSTR, "CIRCLE", RTPOINT, pCentro, RTREAL, DS / 2.0, 0 );   //Diámetro Exterior
   ads_command (RTSTR, "CIRCLE", RTPOINT, pCentro, RTREAL, DNa / 2.0, 0 );  //Diámetro Interior (Succión)
   ads_command (RTSTR, "CIRCLE", RTPOINT, pCentro, RTREAL, ((dcs / 2.0) - (das / 2.0)), 0 );      //Diámetro comienzo del bisel
   ads_command (RTSTR, "CIRCLE", RTPOINT, pCentro, RTREAL, ((dcs / 2.0) - (das / 2.0) - bisel), 0 ); //Diámetro final del bisel

   //Círculo de Agujeros
   ads_command (RTSTR, "LAYER", RTSTR, "S", RTSTR, "CENTROS", RTSTR, "", 0 );
   ads_command (RTSTR, "CIRCLE", RTPOINT, pCentro, RTREAL, dcs / 2.0, 0 );
   ads_command (RTSTR, "LAYER", RTSTR, "S", RTSTR, "0", RTSTR, "", 0 );

   char str2[10], str3[10];

   if ((ns % 3) || (ns == 6)) {//Distribución de 4, 8, 16 ó 6 Agujeros
   	  //Posición del primer agujero (a la derecha)
      ads_polar(pCentro, Grad2Rad( DistAgujeros ( ns ) ), dcs / 2.0, p5);
      //Eje de Centro de Agujeros de fijación
      ads_polar(p5, Grad2Rad( DistAgujeros ( ns ) ), das / 1.5, p7);
      ads_polar(pCentro, Grad2Rad( DistAgujeros ( ns ) ), (dcs / 2.0) - (das / 1.5), p8);
      //Para Acotar los angulos de distribución de Agujeros en la Brida de Succión
      ads_polar(pCentro, Grad2Rad( DistAgujeros ( ns ) ), dcs / 3.0, p10);
      ads_polar(pCentro, Grad2Rad( DistAgujeros ( ns ) / 2.0 ), ((dcs / 2.0) - (das / 2.0)) - (DNa / 3.0), p11);
	  ads_polar(pCentro, Grad2Rad( DistAgujeros ( ns ) * 3.0 ) , (dcs / 2.0) - (das / 1.5), p13);
	  ads_polar(pCentro, Grad2Rad(DistAgujeros ( ns ) * 2.0) , ((dcs / 2.0) - (das / 2.0)) - (DNa / 3.0), p14);
      //Acotación del valor de los ángulos sin ceros en las décimas
      sprintf (str2, "%.3lf", DistAgujeros ( ns ));
      //Eliminación de los ceros y concatenación de la cadena que acota el valor de los ángulos
      KillCero (str2);
      strcat  (str2, "%%d");
   }
   else { //Distribución de 12 Agujeros
      //Posición del primer agujero (a la derecha)
      ads_polar(pCentro, Grad2Rad( DistAgujeros ( ns ) / 2.0 ), dcs / 2.0, p5);
	  //Eje de Centro de Agujeros de fijación
      ads_polar(p5, Grad2Rad( DistAgujeros ( ns ) / 2.0 ), das / 1.5, p7);
      ads_polar(pCentro, Grad2Rad( DistAgujeros ( ns ) / 2.0 ), (dcs / 2.0) - (das / 1.5), p8);
      //Para Acotar los angulos de distribución de Agujeros en la Brida de Succión
      ads_polar(pCentro, Grad2Rad( DistAgujeros ( ns )  / 2.0), dcs / 3.0, p10);
      ads_polar(pCentro, Grad2Rad( DistAgujeros ( ns ) / 4.0 ), ((dcs / 2.0) - (das / 2.0)) - (DNa / 3.0), p11);
	  ads_polar(pCentro, Grad2Rad( DistAgujeros ( ns ) * 1.5 ) , (dcs / 2.0) - (das / 1.5), p13);
	  ads_polar(pCentro, Grad2Rad(DistAgujeros ( ns ) ) , ((dcs / 2.0) - (das / 2.0)) - (DNa / 3.0), p14);
      //Acotación del valor de los ángulos sin ceros en las décimas
      sprintf (str2, "%.3lf", DistAgujeros ( ns ) / 2.0);
      //Eliminación de los ceros y concatenación de la cadena que acota el valor de los ángulos
      KillCero (str2);
      strcat  (str2, "%%d");
      sprintf (str3, "%.3lf", DistAgujeros ( ns ));
      KillCero (str3);
      strcat  (str3, "%%d");
   }

   if (ns % 3) {//Distribución de 4, 8, 16 Agujeros
   	  ads_polar(pCentro, Grad2Rad( DistAgujeros ( ns ) * 3.0), dcs / 3.0, p12);
      sprintf (str3, "%.3lf", DistAgujeros ( ns ) * 2.0);
      KillCero (str3);
      strcat  (str3, "%%d");
   }
   else if (ns == 12)
   	  ads_polar(pCentro, Grad2Rad( DistAgujeros ( ns ) * 1.5), dcs / 3.0, p12);

   //Para seleccionar el Agujero
   ads_polar(p5, 0.0, das / 2.0, p6);
   ads_polar(p6, 0.0, das / 2.0, p19);
   //Para Acotar los angulos de distribución de Agujeros en la Brida de Succión
   ads_polar(pCentro, 0.0, dcs / 3.0, p9);

   //Primer Agujero (a la derecha)
   ads_command (RTSTR, "CIRCLE", RTPOINT, p5, RTREAL, da / 2.0, 0 );

   //Línea de Centro de los Agujeros y para el acotado de ángulos de Distribución de Agujeros
   ads_command (RTSTR, "LAYER", RTSTR, "S", RTSTR, "CENTROS", RTSTR, "", 0 );
   ads_command (RTSTR, "LINE", RTPOINT, p8, RTPOINT, p7, RTSTR, "", 0 );
   ads_command (RTSTR, "LINE", RTPOINT, pCentro, RTPOINT, p8, RTSTR, "", 0 );

   if ((ns % 3) || (ns == 12)) //Distribución de 4, 8, 16 ó 12 Agujeros
      ads_command (RTSTR, "LINE", RTPOINT, pCentro, RTPOINT, p13, RTSTR, "", 0 );

   ads_command (RTSTR, "LAYER", RTSTR, "S", RTSTR, "0", RTSTR, "", 0 );

   //Arreglo de Agujeros
   ads_command (RTSTR, "ARRAY", RTPOINT, p6, RTPOINT, p7, RTSTR, "", RTSTR, "P",
	            RTPOINT, pCentro, RTSHORT, ns, RTREAL, 360.0, RTSTR, "Y", 0 );

   if (ns == 12)
      ads_polar(pCentro, Grad2Rad(180.0 + DistAgujeros ( ns )), DNa / 1.5, p16);
   else
      ads_polar(pCentro, Grad2Rad(180.0 + DistAgujeros ( ns ) / 2.0), DNa / 1.5, p16);

   //Acotación del Arco del círculo de agujeros de la Brida de Succión
   ads_polar(pCentro, Grad2Rad(10.0), dcs / 2.0, p15);

   //ACOTACION Brida de SUCCION
   ads_command (RTSTR, "LAYER", RTSTR, "S", RTSTR, "COTAS", RTSTR, "", 0 );
   ads_command (RTSTR, "DIM",
                //Angulos de la Distribución de Agujeros en la Brida de Succión
				RTSTR, "ANG", RTPOINT, p9, RTPOINT, p10, RTPOINT, p11, RTSTR, str2, RTSTR, "",
	            //Círculo de Agujeros de la Brida de Succión
				RTSTR, "DIAM", RTPOINT, p15, RTSTR, "", RTPOINT, p16, RTSTR, "",
				//Diametro de los Agujeros
				RTSTR, "DIAM", RTPOINT, p6, RTSTR, "", RTPOINT, p19, RTSTR, "",
 				RTSTR, "EXIT", 0 );
   
   if ((ns % 3) || (ns == 12)) {//Distribución de 4, 8, 16 ó 12 Agujeros
	   ads_command (RTSTR, "DIM",
	                RTSTR, "ANG", RTPOINT, p10, RTPOINT, p12, RTPOINT, p14, RTSTR, str3, RTSTR, "",
	 				RTSTR, "EXIT", 0 );
   }

}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
int multiplo5 (double n) {
int m;

	m =	fabs(n);

	while (m % 5)
	  ++m;

	return m;
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
int ArcLineFillet( Arc3PType Arc, LineType L, SCALAR r_fillet,
				   PointType *Pto1, PointType *Pto2, PointType *Pto3 ) {
//CircleType C;
//ArcType A;
PointType Pc;
SCALAR angle, Radius, dist, ACosine, Sine, Cosine;
	   PointType P1, P2, P3;


    Pc = PcOf3PArc ( Arc );
	Radius = RadOf3PArc ( Arc );
//    C.Radius = A.Ra = RadOf3PArc ( Arc );

    dist    = Pc.CoordY - (L.P1.CoordY + r_fillet);
    angle   = dist / (Radius + r_fillet);
	ACosine = acos(angle);
	Sine    = sin(ACosine);

	P1.CoordX = Pc.CoordX + Sine * (Radius + r_fillet);
    P1.CoordY = Pc.CoordY - (dist + r_fillet);
    P1.CoordZ = ( SCALAR ) 0.0;

	Cosine = cos(ACosine);

	P2.CoordX = Pc.CoordX + Sine * Radius;
    P2.CoordY = Pc.CoordY - Cosine * Radius;
    P2.CoordZ = ( SCALAR ) 0.0;

	P3.CoordX = Pc.CoordX + Sine * (Radius + r_fillet);
    P3.CoordY = Pc.CoordY - dist;
    P3.CoordZ = ( SCALAR ) 0.0;

    *Pto1 = P1;
    *Pto2 = P2;
    *Pto3 = P3;


  return 1;

}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
int CircleLineFillet( CircleType Cir, PointType Pto1, PointType Pto2, SCALAR r_fillet,
				      PointType *PInt1, PointType *PInt2, PointType *PInt3 ) {
  PointType Pc;
  LineType L1;
  SCALAR angle, Radius, distance, dist1, dist2, dist3, ACosine, Sine, Cosine, Rad, ang1,
         side1, side2, a1;
	     PointType P1, P2, P3;

   	ang1 = Angle ( Pto1, Pto2 );

	if ( ang1 == 270.0 )
		ang1 = 90.0;

/*
    L1.P1.CoordX = Pto1.CoordX;
    L1.P1.CoordY = Pto1.CoordY;
    L1.P1.CoordZ = 0.0;
    L1.Angle = ang1;

    L1.P1.CoordX = Pto2.CoordX;
    L1.P1.CoordY = Pto2.CoordY;
    L1.P1.CoordZ = 0.0;
    L1.Angle = ang1;
*/

    // Angulo complementario con el eje
	if ( ang1 <= 90.0 )
		a1 = 90.0 - ang1;
	else if (( ang1 > 90.0 ) && ( ang1 <= 180.0 ))
		a1 = ang1 - 90.0;
	else if (( ang1 > 180.0 ) && ( ang1 <= 270.0 ))
		a1 = 270.0 - ang1;
	else
		a1 = ang1 - 270.0;

	   angle = Angle ( Cir.Pc, Pto1 ) - 10.0;
	   distance = DistPointPoint ( Cir.Pc, Pto1 );
	distance = cos ( Grad2Rad (angle) ) * DistPointPoint ( Cir.Pc, Pto1 );
	     Rad = (distance - Cir.Radius) / 2.0;
       dist3 = (Cir.Radius + Rad) - (r_fillet - Rad);
	  Cosine = dist3 / (Cir.Radius + r_fillet);
	 ACosine = acos (Cosine);
//	 ang1 = Rad2Grad (ACosine);

	   side1 = sin (ACosine) * Cir.Radius;
       side2 = sin (ACosine) * (Cir.Radius + r_fillet);
	   dist1 = cos (ACosine) * Cir.Radius;
	   dist2 = Cir.Radius + (Rad * 2.0);

	if (r_fillet < Rad)
		return 0; // False (NO es posible)

	P1.CoordX = Pc.CoordX + dist1;
    P1.CoordY = Pc.CoordY + side1;
    P1.CoordZ = ( SCALAR ) 0.0;

	P2.CoordX = Pc.CoordX + dist2;
    P2.CoordY = Pc.CoordY + side2;
    P2.CoordZ = ( SCALAR ) 0.0;

	P3.CoordX = Pc.CoordX + dist3;
    P3.CoordY = Pc.CoordY + side2;
    P3.CoordZ = ( SCALAR ) 0.0;

    *PInt1 = P1;
    *PInt2 = P2;
    *PInt3 = P3;


  return 1; // True (es posible)

}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//Fillet betwen two lines
int LineLineFillet ( PointType Pto1, PointType Pto2, PointType Pto3, PointType Pto4, SCALAR r_fillet,
				     PointType *PInt1, PointType *PInt2, PointType *PInt3 ) {

     SCALAR ang1, ang2, ang3, a1, a2, a3, a_fillet, dist, Sine1, Cosine1, Sine2, Cosine2, Sine3, Cosine3, Tangent, Hipo, temp;
  PointType Pv, P1, P2, P3;
   LineType L1, L2;

	ang1 = Angle ( Pto1, Pto2 );
	ang2 = Angle ( Pto3, Pto4 );

    L1.P1.CoordX = Pto1.CoordX;
    L1.P1.CoordY = Pto1.CoordY;
    L1.P1.CoordZ = 0.0;
    L1.Angle = ang1;

    L1.P1.CoordX = Pto2.CoordX;
    L1.P1.CoordY = Pto2.CoordY;
    L1.P1.CoordZ = 0.0;
    L1.Angle = ang1;

    L2.P1.CoordX = Pto3.CoordX;
    L2.P1.CoordY = Pto3.CoordY;
    L2.P1.CoordZ = 0.0;
    L2.Angle = ang2;

    L2.P1.CoordX = Pto4.CoordX;
    L2.P1.CoordY = Pto4.CoordY;
    L2.P1.CoordZ = 0.0;
    L2.Angle = ang2;

    LineIntercept ( L1, L2, &Pv ); // Intercepción de las dos líneas para encontrar el vértice

	if ( EqualCero ( fabs( ang1 - ang2 )) == 0 ) {
		temp = fabs ( ang2 - ang1 );
		if ( EqualCero ( fabs (temp - 180.0 )) == 0 ) {
			ang1 = EqualPoint ( Pv, Pto1 ) ? Angle ( Pv, Pto2 ) : Angle ( Pv, Pto1 );
			ang2 = EqualPoint ( Pv, Pto3 ) ? Angle ( Pv, Pto4 ) : Angle ( Pv, Pto3 );
		}
		else
			return 0; // Líneas colineales
	}
	else
	    return 0; // Líneas colineales

	// Intercambio para que ang2 siempre sea mayor que ang1
    if ( ang1 > ang2 ) {
		temp = ang1;
	    ang1 = ang2;
		ang2 = temp;
	}

	// Verificar si el ángulo que forman las líneas es agudo u obtuso y la posición del punto centro
	ang3 = ( ang2 - ang1 ) < 180.0 ? ang1 + (( ang2 - ang1 ) / 2.0 ) : 180.0 + ang1 + (( ang2 - ang1 ) / 2.0 );

    // Angulo complementario con el eje
	if ( ang1 <= 90.0 )
		a1 = 90.0 - ang1;
	else if (( ang1 > 90.0 ) && ( ang1 <= 180.0 ))
		a1 = ang1 - 90.0;
	else if (( ang1 > 180.0 ) && ( ang1 <= 270.0 ))
		a1 = 270.0 - ang1;
	else
		a1 = ang1 - 270.0;

    // Angulo complementario con el eje
	if ( ang2 <= 90.0 )
		a2 = 90.0 - ang2;
	else if (( ang2 > 90.0 ) && ( ang2 <= 180.0 ))
		a2 = ang2 - 90.0;
	else if (( ang2 > 180.0 ) && ( ang2 <= 270.0 ))
		a2 = 270.0 - ang2;
	else
		a2 = ang2 - 270.0;

    // Angulo complementario con el eje
	if ( ang3 <= 90.0 )
		a3 = 90.0 - ang3;
	else if (( ang3 > 90.0 ) && ( ang3 <= 180.0 ))
		a3 = ang3 - 90.0;
	else if (( ang3 > 180.0 ) && ( ang3 <= 270.0 ))
		a3 = 270.0 - ang3;
	else
		a3 = ang3 - 270.0;

	// Angulo para el cálculo del empalme
	a_fillet = ( ang2 - ang1 ) > 180.0 ? ( ang2 - ang1 ) - 180.0 : 180.0 - ( ang2 - ang1 );

	Tangent = tan ( Grad2Rad ( a_fillet ) / 2.0 );
       dist = r_fillet * Tangent;
	  Sine1 = sin ( Grad2Rad ( a1 ));
	Cosine1 = cos ( Grad2Rad ( a1 ));
	  Sine2 = sin ( Grad2Rad ( a2 ));
	Cosine2 = cos ( Grad2Rad ( a2 ));
	  Sine3 = sin ( Grad2Rad ( a3 ));
	Cosine3 = cos ( Grad2Rad ( a3 ));
	   Hipo = r_fillet / cos ( Grad2Rad ( a_fillet ) / 2.0 );

	// Posición del punto P1 que intercepta con respecto al punto del vértice Pv
	P1.CoordX = ( ang1 <= 90.0 ) || ( ang1 >= 270.0 ) ? Pv.CoordX + Sine1 * dist : Pv.CoordX - Sine1 * dist;
    P1.CoordY = ( ang1 <= 180.0 ) ? Pv.CoordY + Cosine1 * dist : Pv.CoordY - Cosine1 * dist;
    P1.CoordZ = ( SCALAR ) 0.0;

	// Posición del punto P2 que intercepta con respecto al punto del vértice Pv
	P2.CoordX = ( ang2 <= 90.0 ) || ( ang2 >= 270.0 ) ? Pv.CoordX + Sine2 * dist : Pv.CoordX - Sine2 * dist;
    P2.CoordY = ( ang2 <= 180.0 ) ? Pv.CoordY + Cosine2 * dist : Pv.CoordY - Cosine2 * dist;
    P2.CoordZ = ( SCALAR ) 0.0;

	// Posición del punto P3 que intercepta con respecto al punto del vértice Pv
	P3.CoordX = ( ang3 <= 90.0 ) || ( ang3 >= 270.0 ) ? Pv.CoordX + Sine3 * Hipo : Pv.CoordX - Sine3 * Hipo;
    P3.CoordY = ( ang3 <= 180.0 ) ? Pv.CoordY + Cosine3 * Hipo : Pv.CoordY - Cosine3 * Hipo;
    P3.CoordZ = ( SCALAR ) 0.0;

    *PInt1 = P1; // Punto de intercepción en una línea
    *PInt2 = P2; // Punto de intercepción en una línea
    *PInt3 = P3; // Punto del centro entre las dos líneas ( puede ser utilizado para acotar el radio del arco )

    return 1; // True
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//Chamfer betwen two lines
int LineLineChamfer ( PointType Pto1, PointType Pto2, PointType Pto3, PointType Pto4, SCALAR dist1, SCALAR dist2,
				      PointType *PInt1, PointType *PInt2 ) {

     SCALAR ang1, ang2, a1, a2, a_fillet, dist, Sine1, Cosine1, Sine2, Cosine2, temp;
  PointType Pv, P1, P2;
   LineType L1, L2;

	ang1 = Angle ( Pto1, Pto2 );
	ang2 = Angle ( Pto3, Pto4 );

    L1.P1.CoordX = Pto1.CoordX;
    L1.P1.CoordY = Pto1.CoordY;
    L1.P1.CoordZ = 0.0;
    L1.Angle = ang1;

    L1.P1.CoordX = Pto2.CoordX;
    L1.P1.CoordY = Pto2.CoordY;
    L1.P1.CoordZ = 0.0;
    L1.Angle = ang1;

    L2.P1.CoordX = Pto3.CoordX;
    L2.P1.CoordY = Pto3.CoordY;
    L2.P1.CoordZ = 0.0;
    L2.Angle = ang2;

    L2.P1.CoordX = Pto4.CoordX;
    L2.P1.CoordY = Pto4.CoordY;
    L2.P1.CoordZ = 0.0;
    L2.Angle = ang2;

    LineIntercept ( L1, L2, &Pv ); // Intercepción de las dos líneas para encontrar el vértice

	if ( EqualCero ( fabs( ang1 - ang2 )) == 0 ) {
		temp = fabs ( ang2 - ang1 );
		if ( EqualCero ( fabs (temp - 180.0 )) == 0 ) {
			ang1 = EqualPoint ( Pv, Pto1 ) ? Angle ( Pv, Pto2 ) : Angle ( Pv, Pto1 );
			ang2 = EqualPoint ( Pv, Pto3 ) ? Angle ( Pv, Pto4 ) : Angle ( Pv, Pto3 );
		}
		else
			return 0; // Líneas colineales
	}
	else
	    return 0; // Líneas colineales

	// Intercambio para que ang2 siempre sea mayor que ang1
    if ( ang1 > ang2 ) {
		temp = ang1;
	    ang1 = ang2;
		ang2 = temp;
	}

    // Angulo complementario con el eje
	if ( ang1 <= 90.0 )
		a1 = 90.0 - ang1;
	else if (( ang1 > 90.0 ) && ( ang1 <= 180.0 ))
		a1 = ang1 - 90.0;
	else if (( ang1 > 180.0 ) && ( ang1 <= 270.0 ))
		a1 = 270.0 - ang1;
	else
		a1 = ang1 - 270.0;

    // Angulo complementario con el eje
	if ( ang2 <= 90.0 )
		a2 = 90.0 - ang2;
	else if (( ang2 > 90.0 ) && ( ang2 <= 180.0 ))
		a2 = ang2 - 90.0;
	else if (( ang2 > 180.0 ) && ( ang2 <= 270.0 ))
		a2 = 270.0 - ang2;
	else
		a2 = ang2 - 270.0;

	// Angulo para el cálculo del chamfer
	a_fillet = ( ang2 - ang1 ) > 180.0 ? ( ang2 - ang1 ) - 180.0 : 180.0 - ( ang2 - ang1 );

	  Sine1 = sin ( Grad2Rad ( a1 ));
	Cosine1 = cos ( Grad2Rad ( a1 ));
	  Sine2 = sin ( Grad2Rad ( a2 ));
	Cosine2 = cos ( Grad2Rad ( a2 ));

	// Posición del punto P1 que intercepta con respecto al punto del vértice Pv
	P1.CoordX = ( ang1 <= 90.0 ) || ( ang1 >= 270.0 ) ? Pv.CoordX + Sine1 * dist1 : Pv.CoordX - Sine1 * dist1;
    P1.CoordY = ( ang1 <= 180.0 ) ? Pv.CoordY + Cosine1 * dist1 : Pv.CoordY - Cosine1 * dist1;
    P1.CoordZ = ( SCALAR ) 0.0;

	// Posición del punto P2 que intercepta con respecto al punto del vértice Pv
	P2.CoordX = ( ang2 <= 90.0 ) || ( ang2 >= 270.0 ) ? Pv.CoordX + Sine2 * dist2 : Pv.CoordX - Sine2 * dist2;
    P2.CoordY = ( ang2 <= 180.0 ) ? Pv.CoordY + Cosine2 * dist2 : Pv.CoordY - Cosine2 * dist2;
    P2.CoordZ = ( SCALAR ) 0.0;

    *PInt1 = P1; // Punto de intercepción en una línea
    *PInt2 = P2; // Punto de intercepción en una línea

    return 1; // True
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
void Trimteton(double ang1) {
	double t1;
	ads_point p31, ep;

   if ((ancho_canal + (s_discos * 2.0) + (Holgura_axial() * 2.0)) > (Rho( 92.0 ) * 2.0)) {
       axis = ((Rho(92.0)) * (Rho( 92.0))) / ((ancho_canal + (s_discos * 2.0) + (Holgura_axial() * 2.0)) / 2.0);
	     t1 = Rho( 92.0 ) + axis;
   }
   else
	   t1 = Rho( 92.0 ) * 2.0;

   if ((r3() + s_pared + (tornillo * 3.0)) > (r3() + s_pared + t1)) { // Si la voluta en ese punto es mayor que lo tetones

	   //Para cortar el tramo de Voluta dentro del Teton
	   ads_polar(pCentro, Grad2Rad (90.0), r3() + s_pared + (tornillo * 1.5), p31);

       if ((ancho_canal + (s_discos * 2.0) + (Holgura_axial() * 2.0)) > (Rho( 92.0 ) * 2.0))
	       dist_ep = r3() + s_pared + (Rho( 92.0 ) + axis);
       else
	       dist_ep = r3() + s_pared + (Rho( 92.0 ) * 2.0);

       angle_ep = Grad2Rad(INICIOA) + Grad2Rad(92.0);

       ads_polar(pCentro, angle_ep, dist_ep, ep);
   }
}
*/