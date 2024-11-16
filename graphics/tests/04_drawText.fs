\ *********************************************************************
\ drawText test
\    Filename:      04_drawText.fs
\    Date:          16 nov. 2024
\    Updated:       16 nov. 2024
\    File Version:  1.0
\    Forth:         eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************



.( 04_drawText.fs loaded - display text in window )



\ MessageBoxA info: 
\   https://learn.microsoft.com/fr-fr/windows/win32/api/winuser/nf-winuser-drawtexta


only 
windows also
graphics internals

\ struct RECT
\   i32 field ->left
\   i32 field ->top
\   i32 field ->right
\   i32 field ->bottom

: RECT! { left top right bottom addr -- }
    left   addr ->left   w!
    top    addr ->top    w!
    right  addr ->right  w!
    bottom addr ->bottom w!
  ;



create LPRECT
    RECT allot

: lpchText z" This is a test string..."  ;


: DRAWtext  ( -- )
    10 10 300 80 LPRECT RECT!
    hdc lpchText -1 LPRECT DT_TOP DrawTextA drop
\     $ff0000 to color 
\     hdc 10 30 lpString TextOutA  drop
\     $000000 to color 
;




: run04
    600 400 window 100 ms
    DRAWtext
    key drop
  ;





