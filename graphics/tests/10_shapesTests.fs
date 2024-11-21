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
\ z" SetDCPenColor"   2 Gdi32 SetDCPenColor ( hdc color -- prev-color )

z" CreateSolidBrush"  1 Gdi32 CreateSolidBrush ( color -- HBRUSH )



graphics internals



0 value HPEN_RED
0 value HPEN_BLUE

: setPens  ( -- )
    PS_SOLID    3 $FF $00 $00 RGB CreatePen to HPEN_RED
    PS_DOT      1 $00 $00 $FF RGB CreatePen to HPEN_BLUE
  ;

0 value HBRUSH_BLUE

: setBrushes  ( -- )
    $00 $00 $FF RGB CreateSolidBrush to HBRUSH_BLUE
  ;

: draw 
    setPens
    setBrushes
    hdc 10 10 220 80 Rectangle drop
    hdc HPEN_RED    SelectObject drop
    hdc HBRUSH_BLUE SelectObject drop
    hdc 25 25 235 95 Rectangle drop
  ;




: run10
    600 400 window
    draw
    key drop
    bye
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

