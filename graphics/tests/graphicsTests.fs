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


.( graphicsTests.fs loaded )

windows also
graphics



graphics internals

: draw 
    COLOR_BLACK to color
    20 20 moveTo
    50 20 lineTo 
    50 50 lineTo 
    20 50 lineTo 
    20 20 lineTo 
;




: run
    600 400 window
    draw
    GetLastError
    exit
\     begin
\         poll
\         IDLE event = if 
\             draw
\         then
\     event FINISHED = until
    key drop
    bye
;


\ run




