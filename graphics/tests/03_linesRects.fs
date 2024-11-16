\ *********************************************************************
\ lines, rectangles, etc... test
\    Filename:      03_linesRects.fs
\    Date:          16 nov. 2024
\    Updated:       16 nov. 2024
\    File Version:  1.0
\    Forth:         eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************



.( 3_linesRects.fs loaded - display lines and other graphics )



only 
windows also
graphics internals



: lpString s" This is a test string..."  ;


: TXTout  ( -- )
    hdc 10 10 lpString TextOutA  drop
    $ff0000 to color 
    hdc 10 30 lpString TextOutA  drop
    $000000 to color 
;




: run
    600 400 window 100 ms
    TXTout
    key drop
  ;





