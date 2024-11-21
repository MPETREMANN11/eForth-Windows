\ *********************************************************************
\ drawText test
\    Filename:      04_drawText.fs
\    Date:          16 nov. 2024
\    Updated:       17 nov. 2024
\    File Version:  1.0
\    Forth:         eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************



.( 04_drawText.fs loaded - display text in window )



\ DrawtextA info: 
\   https://learn.microsoft.com/fr-fr/windows/win32/api/winuser/nf-winuser-drawtexta
\   https://cpp.hotexamples.com/examples/-/-/DrawTextA/cpp-drawtexta-function-examples.html <-- à épulcher absolument !!


only 
windows also
graphics internals

\ struct RECT
\   i32 field ->left
\   i32 field ->top
\   i32 field ->right
\   i32 field ->bottom


create LPRECT
    RECT allot

: lpchText1 s" This is a first string. This is second string. Here a third string. And finaly a lastest string. " ;
: lpchText2 s" Another string. " ;


: DRAWtext  ( -- )
    10 10 300 80 LPRECT RECT!
    hdc lpchText1 LPRECT DT_TOP DT_WORDBREAK OR DT_CENTER OR DrawTextA drop
    hdc lpchText2 LPRECT DT_TOP DT_WORDBREAK OR DT_CENTER OR DrawTextA drop
  ;


: STR01 ( -- addr len )
    s" This is my first example.. I try to draw a very long text in this graphic window." ;

: FORMATTING ( -- n )
    DT_TOP          \ draw frop top
    DT_WORDBREAK OR \ break words
    DT_CENTER    OR \ center text
  ;

: DRAWtext  ( -- )
    10 10 200 120 LPRECT RECT!
    $ff0000 to color
    hdc STR01 LPRECT FORMATTING DrawTextA
  ;



: run04
    400 200 window 100 ms
    DRAWtext
    key drop
  ;





