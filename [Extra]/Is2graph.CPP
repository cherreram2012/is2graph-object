#include "math.h"
#include "is2graph.h"

#ifndef  DEF_FLAG
#define  Top      100
typedef  SCALAR             RealVector [ Top ];
typedef  RealVector         RealMatrix [ 4 ];
typedef  PointType          PointVector [ Top ];
#define  DEF_FLAG
#endif

/* -------------------------------------------------------------- */
PointType P000 = {0.0, 0.0, 0.0};
PointType _Origen; /* = { (SCALAR) 0.0, (SCALAR) 0.0, (SCALAR) 0.0 }; */

/* -------------------------------------------------------------- */

void NewOrigen ( PointType NewOrig ) {

    _Origen.CoordX = NewOrig.CoordX;
                _Origen.CoordY = NewOrig.CoordY;
    _Origen.CoordZ = NewOrig.CoordZ;
 }

/* -------------------------------------------------------------- */

PointType PolarToCarte ( SCALAR Radius, SCALAR  Angle ) {
 PointType p;
 SCALAR Cosine, Sine;
 
         Sine = sin ( Angle * Pi180 );
 Cosine = cos ( Angle * Pi180 );
 
 p.CoordX = ( Radius * Cosine );
 p.CoordY = ( Radius *   Sine );
 p.CoordZ = 0.0;
 
 return ( p );
 
 }

 
/* -------------------------------------------------------------- */

PointType PolarPoint ( PointType LastP, SCALAR Radius, SCALAR  Angle ) {
 PointType p;
 SCALAR Cosine, Sine;
 
   Sine = sin ( Angle * Pi180 );
 Cosine = cos ( Angle * Pi180 );

 p.CoordX =  LastP.CoordX + ( Radius * Cosine);
 p.CoordY =  LastP.CoordY + ( Radius *   Sine);
 p.CoordZ = 0.0;
 
 return ( p );
 
 }
 
 
SCALAR Angle ( PointType p1, PointType p2 ) {

  SCALAR DiffX, DiffY, Angle;
  
  DiffY = p2.CoordY-p1.CoordY;
  DiffX = p2.CoordX-p1.CoordX;

  if ( EqualCero( DiffY ) )
     if ( DiffX > 0.0 || EqualCero( DiffX ) )
        return 0.;
     else 
        return 180.;
   else          
    if ( EqualCero( DiffX ) )
       if ( DiffY > 0.0 )
           return 90.;
       else
         return 270.; 
     else {
     
        Angle = atan ( DiffY/DiffX ) * 180/PI;

        if ( DiffX < 0.0 ) Angle += 180.0;
         
        if ( DiffY < 0.0 && Angle < 0.0 ) Angle += 360.0;
         
        return NormAng (Angle);
      } 
 }
 

/*------------------------------------------Translate Primitiven---------*/

PointType TranslPoint ( PointType  OldP,
                        PointType  Incr ) {

PointType  NewP;

  NewP.CoordX  =  OldP.CoordX + Incr.CoordX + _Origen.CoordX;
  NewP.CoordY  =  OldP.CoordY + Incr.CoordY + _Origen.CoordY;

        NewP.CoordZ  =  OldP.CoordZ;           /* --- Up to now only for 2D --- */
  
  return ( NewP );
}


SegmentType TranslSegment ( SegmentType  OldS,
                              PointType  Incr ) {

  SegmentType  NewS;

  NewS.P1 =  TranslPoint ( OldS.P1, Incr );
  NewS.P2 =  TranslPoint ( OldS.P2, Incr );
  
  return ( NewS );
}


CircleType TranslCircle ( CircleType  OldC, PointType  Incr ) {

  OldC.Pc =  TranslPoint ( OldC.Pc, Incr );
  
  return ( OldC );
}


ArcType TranslArc ( ArcType  OldA, PointType  Incr ) {

  OldA.Pc =  TranslPoint ( OldA.Pc, Incr );
  
  return ( OldA );
}

/* -----------------------------------------Rotation Primitives--------- */

PointType RotaPoint ( PointType  OldP,  SCALAR  Angle ) {

  PointType  NewP;
     SCALAR  Cosine, Sine;

  Cosine = cos ( Angle * Pi180 );
  Sine   = sin ( Angle * Pi180 );
  
  NewP.CoordX = (OldP.CoordX - _Origen.CoordX) * Cosine - 
                (OldP.CoordY - _Origen.CoordY) * Sine + _Origen.CoordX;
  NewP.CoordY = (OldP.CoordX - _Origen.CoordX) * Sine   + 
                (OldP.CoordY - _Origen.CoordY) * Cosine + _Origen.CoordY;
  NewP.CoordZ = OldP.CoordZ;
  
  return ( NewP );
}


SegmentType RotaSegment ( SegmentType  OldS,
                               SCALAR  Angle) {

  SegmentType  NewS;

  NewS.P1 = RotaPoint ( OldS.P1, Angle );
  NewS.P2 = RotaPoint ( OldS.P2, Angle );

  return ( NewS );
 
}


CircleType RotaCircle ( CircleType  OldC, SCALAR  Angle) {

  OldC.Pc = RotaPoint ( OldC.Pc, Angle );

  return ( OldC );
 
}


ArcType RotaArc ( ArcType  OldA, SCALAR  RotAngle) {
  
  PointType P1, P3;
  ArcType NewA;
  
  P1 = PolarPoint ( OldA.Pc, OldA.Ra, OldA.Ai );
  P3 = PolarPoint ( OldA.Pc, OldA.Ra, OldA.Af );
  
  P1 = RotaPoint ( P1, RotAngle );
  P3 = RotaPoint ( P3, RotAngle );
  
  NewA.Pc = RotaPoint ( OldA.Pc, RotAngle );
  NewA.Ai = Angle ( NewA.Pc, P1 );
  NewA.Af = Angle ( NewA.Pc, P3 );
  NewA.Ra = OldA.Ra;

  return ( NewA );
 
}

/* -----------------------------------------Reflect Primitiven---------- */

static  PointType RfPb1, RfPb2;

void  SetReflectPB ( PointType  P1, PointType  P2 ) {

  RfPb1 = P1;
  RfPb2 = P2;
}


PointType  ReflectPoint ( PointType  P ) {

  PointType P1;
     SCALAR Ang;
   LineType L1, L2;   

  Ang = Angle ( RfPb1, RfPb2 );
  
  L1.P1    = RfPb1;
  L1.Angle = Ang;

  L2.P1    = P;
  L2.Angle = Ang + 90.0;
  
  LineIntercept ( L1, L2, &P1 );
  
  P1.CoordX = P.CoordX + 2 * ( P1.CoordX - P.CoordX );
  P1.CoordY = P.CoordY + 2 * ( P1.CoordY - P.CoordY );
  
  return( P1 );
  
}  


SegmentType  ReflectSegment ( SegmentType S ) {
 
  SegmentType S1;
  
  S1.P1 = ReflectPoint ( S.P1 );
  S1.P2 = ReflectPoint ( S.P2 );
  
  return ( S1 );
 
}


CircleType  ReflectCircle ( CircleType C ) {
  
  C.Pc = ReflectPoint ( C.Pc );
  return ( C );
  
}


ArcType  ReflectArc ( ArcType A ) {

  PointType P1, P3;
  
  P1 = PolarPoint ( A.Pc, A.Ra, A.Ai );
  P3 = PolarPoint ( A.Pc, A.Ra, A.Af );
     
  P1 = ReflectPoint ( P1 );
  P3 = ReflectPoint ( P3 );
  
  A.Pc = ReflectPoint ( A.Pc );
  A.Ai = Angle( A.Pc, P3 );
  A.Af = Angle( A.Pc, P1 );
  
  return A;
  
}

/* -----------------------------------------Scale Primitiven-------- */

static  PointType SclPb;
static  SCALAR    SclRd;

void  SetScalePB ( PointType  P, SCALAR  Escale ) {

  SclPb = P;
  SclRd = Escale;
}


PointType  ScalePoint ( PointType  P ) {
  
  PointType P1;
     SCALAR Ang, Dis;

  Ang = Angle ( SclPb, P );
  
  Dis = DistPointPoint ( SclPb, P );
  
  P1 = PolarPoint ( SclPb, Dis * SclRd, Ang );
  
  return( P1 );
  
}  


SegmentType  ScaleSegment ( SegmentType S ) {
 
  SegmentType S1;
  
  S1.P1 = ScalePoint ( S.P1 );
  S1.P2 = ScalePoint ( S.P2 );
  
  return ( S1 );
 
}


CircleType  ScaleCircle ( CircleType C ) {
  
  C.Pc = ScalePoint ( C.Pc );
  C.Radius *= SclRd;
  
  return ( C );
  
}


ArcType  ScaleArc ( ArcType A ) {

  A.Pc  = ScalePoint ( A.Pc );
  A.Ra *= SclRd;
  
  return ( A );
  
}


/* ----------------Rotar el sistema de Coordenadas------------------------ */

PointType RotaCoordSyst ( PointType  OldP,
                             SCALAR  Angle ) {

PointType  NewP;
   SCALAR  Cosine, Sine;

       Cosine  =  cos ( Angle * Pi180 );
         Sine  =  sin ( Angle * Pi180 );
  NewP.CoordX  =  (OldP.CoordX - _Origen.CoordX) * Cosine +
                  (OldP.CoordY - _Origen.CoordY) * Sine;
  NewP.CoordY  = -(OldP.CoordX - _Origen.CoordX) * Sine   +
                  (OldP.CoordY - _Origen.CoordY) * Cosine ;
  NewP.CoordZ  =  OldP.CoordZ;
  
  return ( NewP );
}

/* ------------------------Mezcla de traslacion y rotacion------- */

PointType TransRotaPoint ( PointType  OldP, 
                           PointType  Incr,
                              SCALAR  Angle ) {

PointType  NewP;
   SCALAR  Cosine, Sine;
   SCALAR  TempX, TempY;
   
       Cosine  =  cos ( Angle * Pi180 );
         Sine  =  sin ( Angle * Pi180 );
         
  TempX = OldP.CoordX + Incr.CoordX + _Origen.CoordX;
  TempY = OldP.CoordY + Incr.CoordY + _Origen.CoordY;
   
  NewP.CoordX  =  TempX * Cosine - TempY * Sine   ;
  NewP.CoordY  =  TempX * Sine   + TempY * Cosine ;
  NewP.CoordZ  =  OldP.CoordZ;
  
  return ( NewP );
}


/* -------------------------------------------------------------------- */
/*                                                                      */
/* Slope betwen two points                                              */
/* -------------------------------------------------------------------- */


SCALAR Slope ( PointType P1, PointType P2 ) {
 
 return ( (SCALAR) ( ( P1.CoordY - P2.CoordY ) / ( P1.CoordX - P2.CoordX ) ) );
}
 


/* -------------------------------------------------------------------- */
/*                                                                      */
/* Distance betwen two points                                           */
/* -------------------------------------------------------------------- */


SCALAR DistPointPoint ( PointType P1, PointType P2 ) {
 
return ((SCALAR) sqrt( ( P1.CoordX - P2.CoordX) * ( P1.CoordX - P2.CoordX ) + 
					   ( P1.CoordY - P2.CoordY) * ( P1.CoordY - P2.CoordY ) +
					   ( P1.CoordZ - P2.CoordZ) * ( P1.CoordZ - P2.CoordZ )
					 ));
}


/* -------------------------------------------------------------------- */
/*                                                                      */
/* Stright line coefficients. Example: Ax + By + C = 0                  */
/* -------------------------------------------------------------------- */
 
void LineCoeff ( LineType L, SCALAR * A, SCALAR * B, SCALAR * C ) {

 if ( EqualCero (L.Angle - 90.0) || EqualCero (L.Angle - 270.0) ) {
     *A = ( SCALAR ) 1.;
     *B = ( SCALAR ) 0.;
     *C = -L.P1.CoordX;
  }
 else 
   if ( EqualCero (L.Angle - 0.0) || EqualCero (L.Angle - 180.0) ) {
      *A = ( SCALAR ) 0.;
      *B = ( SCALAR ) 1.;
      *C = - L.P1.CoordY;
    }
   else  {
     *A = - tan ( L.Angle * Pi180 );
     *B = 1.;
     *C = - L.P1.CoordY + L.P1.CoordX * tan ( L.Angle * Pi180 );
    }  

}

/* -------------------------------------------------------------------- */
/*                                                                      */
/*  Point of interception betwen two lines.                             */
/* -------------------------------------------------------------------- */

int LineIntercept ( LineType L1, LineType L2, PointType *P1 ) {
  PointType p;
     SCALAR A1, B1, C1, A2, B2, C2;
     SCALAR Denominador;
 
  LineCoeff ( L1, &A1, &B1, &C1 );    
  LineCoeff ( L2, &A2, &B2, &C2 );    
 
  Denominador =  ( A1 * B2 ) - ( A2 * B1 );
  
  if ( EqualCero( Denominador ) )
     return( 0 );

  p.CoordX = - ( ( C1 * B2 ) - ( C2 * B1 ) ) / Denominador;
  p.CoordY = - ( ( A1 * C2 ) - ( A2 * C1 ) ) / Denominador;
  p.CoordZ = ( SCALAR ) 0.0;
  
  *P1 = p;
  return ( 1 );
 
}


int RootPQ( SCALAR p, SCALAR q, SCALAR *r1, SCALAR *r2 ) {
  SCALAR Delta = p*p/4 - q;
  
  if ( EqualCero( Delta ) ) {
     *r1 = *r2 = - p/2.;
     return( 1 );
   }
  if ( Delta > 0. ) {
    *r1 = - p/2. - sqrt( Delta );
    *r2 = - p/2. + sqrt( Delta );
    return( 2 );
   }
  return( 0 ); 
}


void CirCoeff( CircleType C, SCALAR *A1, SCALAR *B1, SCALAR *C1 ) {
  
  *A1 = - 2.* C.Pc.CoordX;
  *B1 = - 2.* C.Pc.CoordY;
  *C1 = - C.Radius * C.Radius + C.Pc.CoordX * C.Pc.CoordX +
          C.Pc.CoordY * C.Pc.CoordY;
}


void MakePQx( SCALAR A, SCALAR B, SCALAR C, SCALAR A1, SCALAR B1, SCALAR C1,
              SCALAR *p, SCALAR *q ) {
  *p = ( 2*B*C - A*A1*B + A*A*B1 ) / ( A*A + B*B );
  *q = (   C*C + A*A*C1 - A*A1*C ) / ( A*A + B*B );
}


void MakePQy( SCALAR A, SCALAR B, SCALAR C, SCALAR A1, SCALAR B1, SCALAR C1,
              SCALAR *p, SCALAR *q ) {
  *p = ( 2*A*C - A*B*B1 + B*B*A1 ) / ( A*A + B*B );
  *q = (   C*C + B*B*C1 - B*B1*C ) / ( A*A + B*B );
}

/* -------------------------------------------------------------------- */
/*                                                                      */
/*  Point of interception among a line and circle                      */
/* -------------------------------------------------------------------- */

int CircleLineIntercept( CircleType Cir, LineType L, PointType *Pto1,
                         PointType *Pto2 ) {
  SCALAR A, B, C, A1, B1, C1, p, q, x1, x2, y1, y2;
  PointType P1 = {0.0, 0.0, 0.0},
	        P2 = {0.0, 0.0, 0.0};
  int f1, f2;
  
  CirCoeff( Cir, &A1, &B1, &C1 );
  LineCoeff( L, &A, &B, &C );
  
  MakePQx( A, B, C, A1, B1, C1, &p, &q );
  f1 = RootPQ( p, q, &y1, &y2 );

  MakePQy( A, B, C, A1, B1, C1, &p, &q );
  f2 = RootPQ( p, q, &x1, &x2 );

  if ( f1 == 2 || f2 == 2 ) {
     if ( EqualCero( x1*A + y1*B + C ) ) {
        P1.CoordX = x1;
        P1.CoordY = y1;
        P2.CoordX = x2;
        P2.CoordY = y2;
       }
      else {    
        P1.CoordX = x1;
        P1.CoordY = y2;
        P2.CoordX = x2;
        P2.CoordY = y1;
       }
     *Pto1 = P1;
     *Pto2 = P2;  
     return( 2 );   
   }
  if ( f1 && f2 ) {
     P1.CoordX = x1;
     P1.CoordY = y1;
     *Pto1 = P1;
     return( 1 );
   }   
  return( 0 );
}

/* -------------------------------------------------------------------- */
/*                                                                      */
/*  Point of interception among a line and arc                         */
/* -------------------------------------------------------------------- */

int ArcLineIntercept( ArcType A, LineType L, PointType *P1, PointType *P2) {
  CircleType C;
  int CantCir, CantArc = 0;
  
  C.Pc = A.Pc;
  C.Radius = A.Ra;
  CantCir = CircleLineIntercept( C, L, P1, P2 );
  if ( CantCir == 2 ) {
     if ( PointinArc( A, *P1) )
        CantArc++;
     else if ( PointinArc( A, *P2) ) {
               *P1 = *P2;
               return 1;
          }  
     if ( PointinArc( A, *P2) )
          CantArc++;
     return CantArc;
  }
  if ( CantCir )
     if ( PointinArc( A, *P1) )
        return 1;
  return 0;
}

/* -------------------------------------------------------------------- */
/*                                                                      */
/*  Point of interception betwen two segment.                           */
/* -------------------------------------------------------------------- */

int SegmentIntercept( SegmentType S1, SegmentType S2, PointType *P ) {
  LineType L1, L2;
  
  L1.P1    = S1.P1;
  L1.Angle = Angle( S1.P1, S1.P2 );
  
  L2.P1    = S2.P1;
  L2.Angle = Angle( S2.P1, S2.P2 );
  
  if ( ! LineIntercept( L1, L2, P ) )
    return( 0 );
    
  if ( PointinSegment( S1, *P ) && PointinSegment( S2, *P ) )
    return( 1 );
  return( 0 );
}

/* -------------------------------------------------------------------- */
/*                                                                      */
/*  Point of interception amoung a segment and circle                   */
/* -------------------------------------------------------------------- */

int CircleSegmentIntercept( CircleType Cir, SegmentType S, PointType *Pto1,
                            PointType *Pto2 ) {
   LineType L;
   int Cant, Cont = 0;
   
   L.P1    = S.P1;
   L.Angle = Angle( S.P1, S.P2 );
   
   Cant = CircleLineIntercept( Cir, L, Pto1, Pto2 );
   
   if ( Cant == 0 )
     return( 0 );
   
   Cont = PointinSegment( S, *Pto1 ) ? 1 : 0 ;
    
   if ( Cant == 1 )
      return( Cont );
    
   Cont += PointinSegment( S, *Pto2 ) ? 1 : 0 ;   
    
   return( Cont );
}

/* -------------------------------------------------------------------- */
/*                                                                      */
/*  Point of interception amoung a segment and arc                      */
/* -------------------------------------------------------------------- */

int ArcSegmentIntercept( ArcType Arc, SegmentType S, PointType *Pto1,
                         PointType *Pto2 ) {
   LineType L;
   int Cant, Cont = 0;
   
   L.P1    = S.P1;
   L.Angle = Angle( S.P1, S.P2 );
   
   Cant = ArcLineIntercept( Arc, L, Pto1, Pto2 );
   
   if ( Cant == 0 )
     return( 0 );
   
   Cont = PointinSegment( S, *Pto1 ) ? 1 : 0 ;
    
   if ( Cant == 1 )
      return( Cont );
    
   Cont += PointinSegment( S, *Pto2 ) ? 1 : 0 ;   
    
   return( Cont );
}
  
  
int EqualCero ( SCALAR x ) {
 
  return fabs(x) < CeroReal ? 1 : 0;

}


int MayorOIgual ( SCALAR x, SCALAR y ) {
 
  return x > y || EqualCero (x - y);

}


int MenorOIgual ( SCALAR x, SCALAR y ) {
 
  return x < y || EqualCero (x - y);

}


int EqualPoint( PointType P1, PointType P2 ) {
 
  return( EqualCero( P1.CoordX - P2.CoordX ) && 
          EqualCero( P1.CoordY - P2.CoordY ) &&
		  EqualCero( P1.CoordZ - P2.CoordZ ));
}

int EqualSegment( SegmentType E1, SegmentType E2 ) {
  int Conv1, Conv2;

  Conv1 = EqualPoint( E1.P1, E2.P1 ) && EqualPoint( E1.P2, E2.P2 );
  Conv2 = EqualPoint( E1.P1, E2.P2 ) && EqualPoint( E1.P2, E2.P1 );

  return( Conv1 || Conv2 );
}

int EqualArc( ArcType A1, ArcType A2 ) {
  int Cen, Angi, Angf, Rad;
  
  Cen  = EqualPoint( A1.Pc, A2.Pc );
  Angi = EqualCero( A1.Ai - A2.Ai );
  Angf = EqualCero( A1.Af - A2.Af );
  Rad  = EqualCero( A1.Ra - A2.Ra );
  
  return ( Cen && Angi && Angf && Rad );
}

int EqualCircle( CircleType C1, CircleType C2 ) {
  int Cen, Rad;
  
  Cen  = EqualPoint( C1.Pc, C2.Pc );
  Rad  = EqualCero( C1.Radius - C2.Radius );
  
  return( Cen && Rad );
}

int EqualEntity( EntityType E1, EntityType E2 ) {
  int EQ = 0;

  if ( E1.Entity == E2.Entity )
    switch( E1.Entity ) {
      case 'L': EQ = EqualSegment( E1.e.Segment, E2.e.Segment );
                break; 
      case 'A': EQ = EqualArc( E1.e.Arc, E2.e.Arc );
                break;
      case 'C': EQ = EqualCircle( E1.e.Circle, E2.e.Circle );
   }
  return EQ;     
}

SCALAR NormAng( SCALAR Ang ){
 
 if ( Ang < 0. ) 
     while ( Ang < 0. )
      Ang += 360.;
 else
    if (MayorOIgual(Ang, 360.0))
       while (MayorOIgual(Ang, 360.0))
         Ang -= 360.;
 return Ang;
}

int GradPert( ArcType A, SCALAR Grad ) { // Arco Antihorario por defecto
  SCALAR Ai, Af;
   
  Ai = NormAng( A.Ai );
  Af = NormAng( A.Af );
  
  if ( Ai > Af )
       Ai -= 360.;

   return  (MayorOIgual (Grad,         Ai) && MenorOIgual (Grad,         Af)) || 
           (MayorOIgual (Grad - 360.0, Ai) && MenorOIgual (Grad - 360.0, Af));
}

int PointinSegment( SegmentType S, PointType P ) {
  
  if ( EqualPoint (S.P1, P) || EqualPoint (S.P2, P) ||
	   (MayorOIgual(P.CoordX, MinXSegment( S )) && MenorOIgual(P.CoordX, MaxXSegment( S )) &&
        MayorOIgual(P.CoordY, MinYSegment( S )) && MenorOIgual(P.CoordY, MaxYSegment( S ))))
       return 1;
  return 0;     
}

int PointinCircle( CircleType C, PointType P ) {
  SCALAR Dist;
  
  Dist = DistPointPoint( C.Pc, P );
  
  if ( EqualCero( Dist - C.Radius ) )
     return  1;
  
  return 0;
}

int PointinArc( ArcType A, PointType P ) {
  SCALAR Dist, Ang;
  
  Dist = DistPointPoint( A.Pc, P );
  
  if ( ! EqualCero( Dist - A.Ra ) )
    return 0;
    
  Ang = Angle( A.Pc, P );
  
  return GradPert( A, Ang );
}

SCALAR MinXSegment( SegmentType S ) {

  return( min( S.P1.CoordX, S.P2.CoordX ) );
}

SCALAR MaxXSegment( SegmentType S ) {

  return( max( S.P1.CoordX, S.P2.CoordX ) );
}

SCALAR MinYSegment( SegmentType S ) {

  return( min( S.P1.CoordY, S.P2.CoordY ) );
}

SCALAR MaxYSegment( SegmentType S ) {

  return( max( S.P1.CoordY, S.P2.CoordY ) );
}

SCALAR MinXArc( ArcType A ) {
  PointType P1, P2;
  
  if ( GradPert( A, 180. ) )
     return( A.Pc.CoordX - A.Ra );
  P1 = PolarPoint( A.Pc, A.Ra, A.Ai);
  P2 = PolarPoint( A.Pc, A.Ra, A.Af);
  return( min( P1.CoordX, P2.CoordX ) );
}

SCALAR MaxXArc( ArcType A ) {
  PointType P1, P2;
  
  if ( GradPert( A, 0. ) )
     return( A.Pc.CoordX + A.Ra );
  P1 = PolarPoint( A.Pc, A.Ra, A.Ai);
  P2 = PolarPoint( A.Pc, A.Ra, A.Af);
  return( max( P1.CoordX, P2.CoordX ) );
}

SCALAR MinYArc( ArcType A ) {
  PointType P1, P2;
  
  if ( GradPert( A, 270. ) )
     return( A.Pc.CoordY - A.Ra );
  P1 = PolarPoint( A.Pc, A.Ra, A.Ai);
  P2 = PolarPoint( A.Pc, A.Ra, A.Af);
  return( min( P1.CoordY, P2.CoordY ) );
}

SCALAR MaxYArc( ArcType A ) {
  PointType P1, P2;
  
  if ( GradPert( A, 90. ) )
     return( A.Pc.CoordY + A.Ra );
  P1 = PolarPoint( A.Pc, A.Ra, A.Ai);
  P2 = PolarPoint( A.Pc, A.Ra, A.Af);
  return( max( P1.CoordY, P2.CoordY ) );
}

SCALAR MinXCircle( CircleType C ) {

  return( C.Pc.CoordX - C.Radius );
}

SCALAR MaxXCircle( CircleType C ) {

  return( C.Pc.CoordX + C.Radius );
}

SCALAR MinYCircle( CircleType C ) {

  return( C.Pc.CoordY - C.Radius );
}

SCALAR MaxYCircle( CircleType C ) {

  return( C.Pc.CoordY + C.Radius );
}

SCALAR MinXEntity( EntityType E ) {
  SCALAR Min_X = 1000.0;
  
  switch( E.Entity ) {
    case 'L': Min_X = MinXSegment( E.e.Segment ); 
              break;
    case 'A': Min_X = MinXArc( E.e.Arc );
              break;
    case 'C': Min_X = MinXCircle( E.e.Circle );
   }
  return( Min_X );  
}

SCALAR MinYEntity( EntityType E ) {
  SCALAR Min_Y = 1000.0;
  
  switch( E.Entity ) {
    case 'L': Min_Y = MinYSegment( E.e.Segment ); 
              break;
    case 'A': Min_Y = MinYArc( E.e.Arc );
              break;
    case 'C': Min_Y = MinYCircle( E.e.Circle );
   }
  return( Min_Y );  
}

SCALAR MaxXEntity( EntityType E ) {
  SCALAR Max_X = -1000.0;
  
  switch( E.Entity ) {
    case 'L': Max_X = MaxXSegment( E.e.Segment ); 
              break;
    case 'A': Max_X = MaxXArc( E.e.Arc );
              break;
    case 'C': Max_X = MaxXCircle( E.e.Circle );
   }
  return( Max_X );  
}

SCALAR MaxYEntity( EntityType E ) {
  SCALAR Max_Y = -1000.0;
  
  switch( E.Entity ) {
    case 'L': Max_Y = MaxYSegment( E.e.Segment ); 
              break;
    case 'A': Max_Y = MaxYArc( E.e.Arc );
              break;
    case 'C': Max_Y = MaxYCircle( E.e.Circle );
   }
  return( Max_Y );  
}

/* Utilidades para convertir un arco definido por 3 Puntos en un arco definido por 
 * Centro, Radio, Angulo Inicial y Angulo Final.
 */

// Retorna el Punto Medio de un Segmento
PointType MidPoint ( PointType P1, PointType P2 ) { PointType P;

P.CoordX = P1.CoordX > P2.CoordX ? P2.CoordX + ((P1.CoordX - P2.CoordX) / 2.0 ):
								   P1.CoordX + ((P2.CoordX - P1.CoordX) / 2.0 );
P.CoordY = P1.CoordY > P2.CoordY ? P2.CoordY + ((P1.CoordY - P2.CoordY) / 2.0 ):
								   P1.CoordY + ((P2.CoordY - P1.CoordY) / 2.0 );
P.CoordZ = P1.CoordZ > P2.CoordZ ? P2.CoordZ + ((P1.CoordZ - P2.CoordZ) / 2.0 ):
								   P1.CoordZ + ((P2.CoordZ - P1.CoordZ) / 2.0 );
return P;
}

// Retorna el Punto Centro de un Arco definido por tres Puntos
PointType PcOf3PArc ( Arc3PType A ) {
LineType L1, L2;
SCALAR A1, A2;
PointType Pc;

	 A1 = Angle (A.Pi, A.Pm);        // Calculo el angulo de la primera cuerda
	 A2 = Angle (A.Pm, A.Pf);        // Calculo el angulo de la segunda cuerda
	 L1.Angle = NormAng (A1 + 90.0); // Defino una recta perpendicular a la primera cuerda 
	 L2.Angle = NormAng (A2 + 90.0); // Defino una recta perpendicular a la segunda cuerda 
	 L1.P1 = MidPoint (A.Pi, A.Pm);  // Calculo el punto medio de la primera recta
	 L2.P1 = MidPoint (A.Pm, A.Pf);  // Calculo el punto medio de la segunda recta
	 LineIntercept (L1, L2, &Pc);    // Calculo el punto de intercepcion entre la primera recta y
	                                 // la segunda y lo guardo en Pc (Punto Centro del Arco)
return Pc; // Retorno el Punto Centro del Arco
}

// Retorna el Radio de un Arco definido por 3 Puntos
float RadOf3PArc ( Arc3PType A ) { 
PointType Pc = PcOf3PArc (A); // Calculo el Punto Centro de un Arco definido por tres Puntos
         
return (float) DistPointPoint (A.Pi, Pc); // Retorno la distancia entre Pc y un punto del arco. (Radio)
}

// Retorna si el arco definido por 3 Puntos es Horario o Antihorario
char TypeOfArc (Arc3PType A3P) {
char Type = 'U';
ArcType Arc;
double Am;

  Arc.Pc = PcOf3PArc  (A3P);
  Arc.Ra = RadOf3PArc (A3P);
  Arc.Ai = Angle (Arc.Pc, A3P.Pi); // Grados
  Arc.Af = Angle (Arc.Pc, A3P.Pf); // Grados

  Am = Angle (Arc.Pc, A3P.Pm); // Grados
  
  if (Arc.Ai < Arc.Af)
      if ((Am < Arc.Ai && Am < Arc.Af) || (Am > Arc.Ai && Am > Arc.Af)) //Horario
	      Type = 'H';
      else if (Am < Arc.Af) // Antihorario
	  	       Type = 'A';
	       else;
  else if ((Am < Arc.Ai && Am < Arc.Af) || (Am > Arc.Ai && Am > Arc.Af)) //Antihorario
   	       Type = 'A';
       else if (Am > Arc.Af) //Horario
	            Type = 'H';
  return Type;
}

/* -------------------------------------------------------------------- */
/*  Return an arch defined by three Points in format                    */
/*  Center, Radius, Initial Angle and Final Angle Antihorario           */
/* -------------------------------------------------------------------- */

ArcType Arc3P2Arc (Arc3PType A) { // Return an arch Antihorario
double Ang;
ArcType Arc;

  Arc.Pc = PcOf3PArc  (A);
  Arc.Ra = RadOf3PArc (A);
  Arc.Ai = Angle (Arc.Pc, A.Pi); // initial Degrees
  Arc.Af = Angle (Arc.Pc, A.Pf); // Degrees

 if (TypeOfArc (A) == 'H') {// Si es Horario como tengo que devolver un arco
	      Ang = Arc.Ai;     // Antihorario intercambio los angulos
          Arc.Ai = Arc.Af;
	      Arc.Af = Ang;      
  }
 
  return Arc;
}

// Retorna un arco Arco definido por tres Puntos en formato
// Centro, Radio, Angulo Inicial y Angulo Final Antihorario
ArcType Arc3P2Arc (PointType Pi, PointType Pm, PointType Pf) { // Devuelve un arco Antihorario
Arc3PType A = {{Pi.CoordX, Pi.CoordY, Pi.CoordZ},
               {Pm.CoordX, Pm.CoordY, Pm.CoordZ},
			   {Pf.CoordX, Pf.CoordY, Pf.CoordZ}};

  return Arc3P2Arc(A);
}

PointType PointMajor (PointType P0, PointType P1) { // Se si un punto es mayor que otro
	return MayorOIgual(P1.CoordX, P0.CoordX) && 
		   MayorOIgual(P1.CoordY, P0.CoordY) ? P1 : P0; 
}

PointType PointMinor (PointType P0, PointType P1) { // Se si un punto es menor que otro
	return MenorOIgual(P1.CoordX, P0.CoordX) && 
		   MenorOIgual(P1.CoordY, P0.CoordY) ? P1 : P0; 
}

PointType PointRight (PointType P0, PointType P1) {// Se que punto esta + a la derecha
	return MayorOIgual(P1.CoordX, P0.CoordX) ? P1 : P0; 
}

PointType PointLeft (PointType P0, PointType P1) {// Se que punto esta + a la izquierda
	return MenorOIgual(P1.CoordX, P0.CoordX) ? P1 : P0; 
}

PointType PointHigh (PointType P0, PointType P1) {// Se que punto esta + arriba
	return MayorOIgual(P1.CoordY, P0.CoordY) ? P1 : P0; 
}

PointType PointLow (PointType P0, PointType P1) {// Se que punto esta + abajo
	return MenorOIgual(P1.CoordY, P0.CoordY) ? P1 : P0; 
}

PointType ArcLineIntercept (ArcType A, LineType L, char Pos) {
PointType P = {0.0, 0.0, -1.0};
PointType P1, P2;  
int Cant;
  
  Cant = ArcLineIntercept( A, L, &P1, &P2 );
  
  if (Cant == 1)
	  return P1;

  if (Cant == 2) // Si se interceptó en dos puntos
     switch (Pos) {
     case 'U': // Arriba
			   return PointHigh (P1, P2);  
     case 'D': // Abajo
			   return PointLow (P1, P2);  
     case 'R': // Derecha
			   return PointRight (P1, P2);  
     case 'L': // Izquierda
			   return PointLeft (P1, P2);  
  } 

  return P;
}

// Funcion para hayar el punto tangente a un circulo de una recta que pasa por el punto Po,
// si Pos es I el punto tangente será el que queda a la izquierda del circulo mirando desde
// Po, sino el punto quedará a la derecha.
PointType TanToCircle (PointType Po, CircleType C, char Pos, char &Flag) { // 
PointType Pt = {0.0, 0.0, 0.0};
double Len, Alfa, Ang, Tl;


       Len = DistPointPoint (Po, C.Pc); // Distancia entre Po y el centro del Circulo
	   if (Len < C.Radius) 
		   Flag = 0; // Si Po es interior al Circulo termino
       else { 
		      Alfa = (asin((double)C.Radius/Len) * 180.0) / PI; // Angulo entre la recta tangente y la recta 
			                                      // que pasa por Po y Pc
			  Ang = Angle (Po, C.Pc); // Angulo de la recta que pasa por Po y Pc 
			  Tl = sqrt ((Len)*(Len)-(C.Radius)*(C.Radius)); // Longitud de la recta tangente
			  Pt = PolarPoint (Po, Tl, Ang + (Pos == 'I' ? Alfa : - Alfa)); //  Punto tangente
			  Flag = 1;
	   }    
return Pt;
}

// Funcion para hayar los puntos tangente a dos circulos, si Pos es I los puntos será los que 
// queda a la izquierda del circulo mirando desde el circulo de menor radio, si Pos es D los
// puntos quedarán a la derecha. Si Pos es S los puntos quedan a la derecha mirando desde el
// punto de intercepcion entre la recta tangente y la recta entre los Pc y si Pos es B los puntos
// quedan a la izquierda mirando desde dicho punto
SegmentType TanTo2Circles (CircleType C1, CircleType C2, char Pos, char &Flag) { 
double Len, SenAlfa, Ang, h, Delta, H2;
CircleType Cmin, Cmax;
SegmentType St;
PointType Po = P000;
char Flg;


       if (C1.Radius > C2.Radius) { // Encuentro el menor de los circulos
		   Cmax.Radius = C1.Radius;
		   Cmin.Radius = C2.Radius;
		   Cmax.Pc = C1.Pc;
		   Cmin.Pc = C2.Pc;
	   } 
	   else {
		   Cmax.Radius = C2.Radius;
		   Cmin.Radius = C1.Radius;
		   Cmax.Pc = C2.Pc;
		   Cmin.Pc = C1.Pc;
	   }

       Len = DistPointPoint (C1.Pc, C2.Pc); // Distancia entre centros
	   Delta = Cmax.Radius - Cmin.Radius; // Diferencia de diámetros

	   if (EqualCero(Delta) && (Pos == 'I' || Pos == 'D')) { // Caso que sean radios iguales
	       St.P2 = PolarPoint (Cmin.Pc, Cmin.Radius, Pos == 'D' ? 0.0 : 180.0);
           St.P1 = PolarPoint (Cmax.Pc, Cmax.Radius, Pos == 'D' ? 0.0 : 180.0);
           Flag = 1;
	   }
	   else if (Len < Delta)
		        Flag = 0; // Si es interior termino
            else { 
		        SenAlfa = Delta/Len; // Seno del ángulo entre la recta tangente y la recta que pasa
			                         // por los Pc
			    Ang = Angle (Cmin.Pc, Cmax.Pc); // Angulo de la recta que pasa por los Pc 
			    h = Cmin.Radius/SenAlfa; // Distancia entre el PcMin y un Po por donde pasaría le recta 
			    if (Pos == 'I' || Pos == 'D')
			        Po = PolarPoint (Cmin.Pc, h, Ang - 180.0); //  Punto Po
			    else { H2 = Len / ((Cmax.Radius/Cmin.Radius) + 1.0);
			           Po = PolarPoint (Cmin.Pc, H2, Ang); //  Punto Po Int
				  	   Pos = Pos == 'S' ? 'D' : 'I'; 
			    }

                St.P1 = TanToCircle (Po, Cmin, Pos, Flg);
                St.P2 = TanToCircle (Po, Cmax, Pos, Flg);
			    Flag = 1;
	        }    
return St;
}

/*
unsigned char
 IntersectLineSeg(SegmentType ls1, SegmentType ls2, PointType *intp) {
// IntersectLineSeg returns the intersection point of two line segments 
// if an intersection is found. 

 SCALAR dl2x;  // Change in X in line segment 2
 SCALAR dl2y;  // Change in Y in line segment 2
 SCALAR dl1x;  // Change in X in line segment 1
 SCALAR dl1y;  // Change in X in line segment 1
 SCALAR dl12x; // Change in X between line segments 1 and 2
 SCALAR dl12y; // Change in Y between line segments 1 and 2
 SCALAR Denom; // Denominator
 SCALAR parm1;
 SCALAR parm2; // Misc


 dl2x = ls2.P2.CoordX - ls2.P1.CoordX;
 dl2y = ls2.P2.CoordY - ls2.P1.CoordY;
 dl1x = ls1.P2.CoordX - ls1.P1.CoordX;
 dl1y = ls1.P2.CoordY - ls1.P1.CoordY;
 dl12x = ls2.P1.CoordX - ls1.P1.CoordX;
 dl12y = ls2.P1.CoordY - ls1.P1.CoordY;
 Denom = dl2x * dl1y - dl2y * dl1x;
 if ( EQFloat(Denom - 0.0) ) return 0; // *intloc = 1;
 else {
         Denom = inverse(Denom);
         parm1 = ((dl2x * dl12y) - (dl2y * dl12x)) * Denom;
         parm2 = ((dl1x * dl12y) - (dl1y * dl12x)) * Denom;
                // if (((((parmltzero(parm1) ||
                //  parmgt(parm1,1.0)) ||
                //  parmltzero(parm2)) ||
                //  parmgt(parm2,1.0))))
         if ( (parm1 < 0.0) || (parm1 > 1.0) || (parm2 < 0.0) || (parm2 > 1.0) )
                 return 0; // (*intloc) = 2;
         else { // Intersection found
                         // (*intloc) = 0;
                         *intp.CoordX = ls1.P1.CoordX + dl1x * parm1;
                         *intp.CoordY = ls1.P1.CoordY + dl1y * parm1;
                         if ( EQFloat( *intp.CoordX - 0.0 ) *intp.CoordX = 0.0;
                         if ( EQFloat( *intp.CoordY - 0.0 ) *intp.CoordY = 0.0;
                 } // else
         } // else
} // intersectls

*/