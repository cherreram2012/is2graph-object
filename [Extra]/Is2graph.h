#ifndef ENTITY_FLAG

#define  PI       3.14159265358979323846
#define  Pi180    1.74532925199433E-02
#define  MaxReal  99999.999
#define  CeroReal 1E-6

/* ----------------------------------------------------------------------- */
/* For 3D equivalence we need cylindrical or espherical coordenates system */
/* in the case we use polar point coordenates                              */
/* ----------------------------------------------------------------------- */

typedef  double SCALAR;

typedef struct {                     /* Polar point definition */
		 SCALAR Radius;
		 SCALAR Angle;
} PolarPointType;


typedef struct {                     /* Point definition */
		 SCALAR CoordX;
		 SCALAR CoordY;
		 SCALAR CoordZ;
} PointType;


typedef struct {                       /* Line definition */
		 PointType P1;
			SCALAR Angle;
} LineType;


typedef struct {                       /* Plane definition */
		 PointType OnePoint, DirVector;
	        SCALAR Angle;
} PlaneType;


typedef struct {                        /* Segment definition */
		PointType P1;
		PointType P2;
} SegmentType;


typedef struct {                       /* Circle definition */
	 PointType Pc;
		SCALAR Radius;
}CircleType;


typedef struct A3P {   //* ---------------- Arc definition ----------------- 
   PointType Pi, Pm, Pf;
}Arc3PType;


typedef struct SA { /* Arc definition */
	PointType Pc;
	   SCALAR Ra;
	   SCALAR Ai;
	   SCALAR Af;
} ArcType;


typedef struct {                /* Boundary definition */
	        int Number;
		   char Name[5];
		 SCALAR CoordX;
		 SCALAR CoordY;
}CotaNameType;


typedef union {
	 PointType Point;
   SegmentType Segment;
      LineType Line;
     PlaneType Plane;
	CircleType Circle;
	   ArcType Arc;
	 Arc3PType Arc3P;
  CotaNameType Cota;
} EType;


typedef struct {                  /* All entities definition */
   char Entity;
	int Color;
  EType e;
} EntityType;


extern PointType P000;           // Punto 0,0,0

#define ENTITY_FLAG 1
#endif

#define       max( a, b )     (((a) > (b)) ? (a) : (b))
#define       min( a, b )     (((a) < (b)) ? (a) : (b))

SCALAR        AdjustX         ( void );
SCALAR        AdjustY         ( void );
SCALAR        GetMaxX         ( void );
SCALAR        GetMaxY         ( void );
void          ViewPort        ( int, int, int, int );                /* OK */
void          Window          ( SCALAR, SCALAR, SCALAR, SCALAR );    /* OK */
void          Cartesian       ( void );         			 /* OK */

void          Point           ( SCALAR, SCALAR );			  /* OK */
void          Line            ( SCALAR, SCALAR, SCALAR, SCALAR );     /* OK */
void          LineS           ( PointType, PointType );               /* OK */
void          Circle          ( SCALAR, SCALAR, SCALAR );             /* OK */
void          ArcCRAiAf       ( PointType, SCALAR, SCALAR, SCALAR );  /* OK */
void          ArcPiPfC        ( SCALAR, SCALAR, SCALAR, SCALAR, SCALAR, SCALAR ); /* OK */
void          Rectangle       ( SCALAR, SCALAR, SCALAR, SCALAR );

int           LogicaFisicaX   ( SCALAR );  /* - World System Coord. to Screen Coord.- */
int           LogicaFisicaY   ( SCALAR );  /* -- WSC to SC ---  */

SCALAR        FisicaLogicaX   ( int );     /* -- SC to WSC  ---  */
SCALAR        FisicaLogicaY   ( int );     /* -- SC to WSC  ---  */

void          SetColor        ( int );               /* OK */
int           GetColor        ( void ); 		/* OK */

void          initvideo       ( void );		/* OK */
void          closevideo      ( void );		/* OK */

/* ------------ Geometric functions ---------- */

void          NewOrigen       ( PointType );                       /* OK */
PointType     PolarToCarte    ( SCALAR,       SCALAR );            /* OK */
PointType     PolarPoint      ( PointType,    SCALAR,   SCALAR );  /* OK */
SCALAR        Angle           ( PointType,    PointType );

/*------------------ Prototypes of Translation primitives functions  ------*/

PointType     TranslPoint     ( PointType,    PointType );
SegmentType   TranslSegment   ( SegmentType,  PointType );
CircleType    TranslCircle    ( CircleType,   PointType );
ArcType       TranslArc       ( ArcType,      PointType );

/*------------------ Prototypes of Rotation primitives functions  ------*/

PointType     RotaPoint       ( PointType,    SCALAR    );         /* OK */
PointType     RotaCoordSyst   ( PointType,    SCALAR    );
PointType     TransRotaPoint  ( PointType,    PointType, SCALAR );
SegmentType   RotaSegment     ( SegmentType,  SCALAR );
CircleType    RotaCircle      ( CircleType,   SCALAR );
ArcType       RotaArc         ( ArcType,      SCALAR );

/* ----------------- Prototypes of Reflection primitives functions  ------*/

void          SetReflectPB    ( PointType, PointType );
PointType     ReflectPoint    ( PointType );
SegmentType   ReflectSegment  ( SegmentType );
CircleType    ReflectCircle   ( CircleType );
ArcType       ReflectArc      ( ArcType );

/*------------------ Prototypes of Scale primitives functions  ------*/

void          SetScalePB      ( PointType, SCALAR );
PointType     ScalePoint      ( PointType );
SegmentType   ScaleSegment    ( SegmentType );
CircleType    ScaleCircle     ( CircleType );
ArcType       ScaleArc        ( ArcType );

/*------------------ Prototypes of utility general functions  ------*/

SCALAR        NormAng         ( SCALAR );
int           GradPert        ( ArcType,      SCALAR );
SCALAR        Slope           ( PointType,    PointType );
SCALAR        MinXSegment     ( SegmentType );
SCALAR        MaxXSegment     ( SegmentType );
SCALAR        MinYSegment     ( SegmentType );
SCALAR        MaxYSegment     ( SegmentType );
SCALAR        MinXArc         ( ArcType );
SCALAR        MaxXArc         ( ArcType );
SCALAR        MinYArc         ( ArcType );
SCALAR        MaxYArc         ( ArcType );
SCALAR        MinXCircle      ( CircleType );
SCALAR        MaxXCircle      ( CircleType );
SCALAR        MinYCircle      ( CircleType );
SCALAR        MaxYCircle      ( CircleType );
SCALAR        MinXEntity      ( EntityType );
SCALAR        MinYEntity      ( EntityType );
SCALAR        MaxXEntity      ( EntityType );
SCALAR        MaxYEntity      ( EntityType );
int           RootPQ          ( SCALAR,       SCALAR,    SCALAR *,  SCALAR * );
void          LineCoeff       ( LineType,     SCALAR *,  SCALAR *,  SCALAR * );
void          CirCoeff        ( CircleType,   SCALAR *,  SCALAR *,  SCALAR * );
int           LineIntercept   ( LineType,     LineType,  PointType * );
int           CircleLineIntercept
							  ( CircleType, LineType, PointType *,PointType *);
int           ArcLineIntercept( ArcType, LineType, PointType *, PointType * );
int           SegmentIntercept( SegmentType,  SegmentType,  PointType * );
int           CircleSegmentIntercept
							  ( CircleType, SegmentType, PointType *, PointType *);
int           ArcSegmentIntercept
							  ( ArcType, SegmentType, PointType *, PointType * );
SCALAR        DistPointPoint  ( PointType,    PointType );
int           PointinSegment  ( SegmentType,  PointType );
int           PointinCircle   ( CircleType,   PointType );
int           PointinArc      ( ArcType,      PointType );
int           EqualCero       ( SCALAR );
int           EqualPoint      ( PointType,    PointType );
int           EqualSegment    ( SegmentType,  SegmentType );
int           EqualArc        ( ArcType,      ArcType);
int           EqualCircle     ( CircleType,   CircleType);
int           EqualEntity     ( EntityType,   EntityType );
int           MayorOIgual     ( SCALAR, SCALAR );
int           MenorOIgual     ( SCALAR, SCALAR );
PointType     MidPoint        ( PointType, PointType);
PointType     PcOf3PArc       ( Arc3PType );
float         RadOf3PArc      ( Arc3PType );
ArcType       Arc3P2Arc       ( Arc3PType );
char          TypeOfArc       ( Arc3PType );
ArcType       Arc3P2Arc       (PointType, PointType, PointType);
PointType     ArcLineIntercept( ArcType, LineType, char);
PointType     TanToCircle     (PointType Po, CircleType C, char Pos, char &Flag); 
SegmentType   TanTo2Circles   (CircleType C1, CircleType C2, char Pos, char &Flag);
