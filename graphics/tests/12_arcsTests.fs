\ *********************************************************************
\ arcs tests for windows
\    Filename:      12_arcsTests.fs
\    Date:          21 nov 2024
\    Updated:       21 nov 2024
\    File Version:  1.0
\    MCU:           eFORTH
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


.( 12_arcsTests.fs loaded )

windows also
graphics

 

graphics internals



\ Ellipses with transparent background

0 value HPEN_RED
0 value HPEN_BLUE

: setPens  ( -- )
    PS_SOLID    5 $FF $00 $00 RGB CreatePen to HPEN_RED
    PS_SOLID    5 $00 $00 $FF RGB CreatePen to HPEN_BLUE
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

: drawArcs ( -- )
    setPens
    hdc HPEN_RED    SelectObject drop
    hdc 10 10 400 200 10 105 400 105 Arc drop 
    hdc HPEN_BLUE   SelectObject drop
    hdc 10 10 400 200 400 105 10 105 Arc drop 
  ;

: run12
    600 400 window
    drawArcs
    begin
        poll
        IDLE event = if 
             
        then
        event FINISHED = 
    until
    exit
  ;


