\ *********************************************************************
\ Drawing ellipses tests for windows
\    Filename:      11_ellipseTests.fs
\    Date:          21 nov 2024
\    Updated:       21 nov 2024
\    File Version:  1.0
\    MCU:           eFORTH
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


.( 11_ellipseTests.fs loaded )

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


\ Ellipses with transparent background

0 value HPEN_RED
0 value HPEN_BLUE

: setPens  ( -- )
    PS_SOLID    1 $FF $00 $00 RGB CreatePen to HPEN_RED
    PS_SOLID    1 $00 $00 $FF RGB CreatePen to HPEN_BLUE
  ;


0 value HBRUSH

0 value HBRUSH_BLUE
0 value HBRUSH_RED

: setBrushes  ( -- )
    $00 $00 $FF RGB CreateSolidBrush to HBRUSH_BLUE
    $FF $00 $00 RGB CreateSolidBrush to HBRUSH_RED
    NULL_BRUSH GetStockObject ?dup if
        to HBRUSH
        ." HBRUSH initialized" 
    else
        ." HBRUSH not initialized" 
\         GetLastError .
        exit
    then
  ;



: drawEllipse ( -- )
\     setPens
    setBrushes
    hdc HBRUSH SelectObject drop
    hdc HPEN_RED    SelectObject drop
    hdc 10 10 400 200 Ellipse drop 
    hdc HPEN_BLUE   SelectObject drop
    hdc 30 30 420 220 Ellipse drop 
  ;

: run11
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

