\ *********************************************************************
\ Graphics tests for windows
\    Filename:      graphicsTests.fs
\    Date:          09 mar 2023
\    Updated:       24 may 2023
\    File Version:  1.0
\    MCU:           eFORTH
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

graphics
\ WINDOW_WIDTH WINDOW_HEIGHT window


graphics internals

: drawLines 
\     20 20 moveTo
    50 20 lineTo 
    50 50 lineTo 
    20 50 lineTo 
    20 20 lineTo 
;



: runx
    WINDOW_WIDTH WINDOW_HEIGHT window
\     begin
\        wait
        COLOR_RED to color 
\         0 0 WINDOW_WIDTH WINDOW_HEIGHT box
        drawLines
\         FINISHED event = 
\     until
;

\ runx




