\ *********************************************************************
\ Drawing shapes tests for windows
\    Filename:      10_shapesTests.fs
\    Date:          20 nov 2024
\    Updated:       20 nov 2024
\    File Version:  1.0
\    MCU:           eFORTH
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


.( 10_shapesTests.fs loaded )

windows also
graphics

\ @TODO à tester
z" SetDCPenColor"   2 Gdi32 SetDCPenColor ( hdc RGBAcolor -- colorref )

z" SetDCBrushColor"   2 Gdi32 SetDCBrushColor ( hdc RGBAcolor -- colorref )

 
\ Draw ellipse
z" Ellipse"     5 gdi32 Ellipse     ( hdc left top right bottom -- fl )

graphics internals

\ pour voc. windows
: RGBA { r g b a -- n } 
    a $1000000 *
    b $10000   *
    g $100     *
    r + + +
  ;

0 value HPEN_RED
0 value HPEN_BLUE

: setPens  ( -- )
    PS_SOLID    1 $FF $00 $00 RGB CreatePen to HPEN_RED
    PS_SOLID    1 $00 $00 $FF RGB CreatePen to HPEN_BLUE
  ;



create RECT01
     10 L,  10 L, 220 L,  80 L,

create RECT02
     25 L,  25 L, 235 L,  95 L,

: drawRects
    setPens
    setBrushes
    hdc RECT01 GetRect Rectangle drop
    hdc HPEN_RED    SelectObject drop
    hdc HBRUSH_BLUE SelectObject drop
    hdc RECT02 GetRect Rectangle drop
  ;

create POYGON01
     30 L, 37 L,
     44 L, 22 L,
     58 L, 41 L,
     43 L, 54 L,
     56 L, 68 L,
     43 L, 79 L,
     31 L, 65 L,
     20 L, 75 L,
      5 L, 58 L,
     19 L, 47 L,
      6 L, 31 L,
     19 L, 22 L,


: drawPolygon
    setPens
    setBrushes
    hdc HPEN_RED    SelectObject drop
    hdc HBRUSH_BLUE SelectObject drop
    hdc POYGON01 12 Polygon drop
  ;


\ Ellipses with transparent background


0 value HBRUSH

0 value HBRUSH_BLUE
0 value HBRUSH_RED

: setBrushes  ( -- )
    $00 $00 $FF RGB CreateSolidBrush to HBRUSH_BLUE
    $FF $00 $00 RGB CreateSolidBrush to HBRUSH_RED
    NULL_BRUSH GetStockObject ?dup if
        to HBRUSH
    else
        GetLastError .
        exit
    then
  ;



: drawEllipse ( -- )
    setPens
    setBrushes
    NULL_BRUSH GetStockObject to HBRUSH
    hdc HBRUSH SelectObject drop
    hdc HPEN_RED    SelectObject drop
    hdc 10 10 400 200 Ellipse drop 
    hdc HPEN_BLUE   SelectObject drop
    hdc 30 30 420 220 Ellipse drop 
  ;

: run10
    600 400 window
    drawEllipse
    begin
        poll
        IDLE event = if 
             
        then
        event FINISHED = 
    until
    exit
  ;


\ run


\ HPEN dashPen =    CreatePen(PS_DASH, 1, RGB(255, 255, 0));
\ HPEN dashDotPen = CreatePen(PS_DASHDOT, 1, RGB(255, 255, 0));
\ 
\     // changing color works with DC_PEN but want something similar for custom pen
\     SelectObject(hdc, GetStockObject(DC_PEN));
\     SetDCPenColor(hdc, RGB(250, 0, 0));
\     MoveToEx(hdc, 100, 100, NULL);
\     LineTo(hdc, 200, 200); 
\ 
\ 
\     // Changing of Pen color doesn't seem to work for the custom pen
\     SelectObject(hdc, dashPen);
\     SetDCPenColor(hdc, COLOR_BLUE);
\     MoveToEx(hdc, 150, 150, NULL);
\     LineTo(hdc, 250, 250); 
\ 
\     SelectObject(hdc, dashDotPen);
\     SetDCPenColor(hdc, COLOR_GRAY);
\     MoveToEx(hdc, 175, 175, NULL);
\     LineTo(hdc, 275, 275);
\ 
\     DeleteObject(dashPen);
\     DeleteObject(dashDotPen);

