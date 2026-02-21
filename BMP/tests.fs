\ tests for BMP files
\    Filename:      tests.fs
\    Date:          09 jan 2026
\    Updated:       10 jan 2026
\    File Version:  1.0
\    Forth:         eFORTH Windows
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


: fileToOpen ( -- addr len )
    s" BMP/blueberry.bmp"
  ;

fileToOpen load-bmp

dispHeadDatas

\ BMP_RAW_DATAS PICTURE_SIZE dump

0 value BITS_RATIO

: bb ( n -- )
    base @  >r 2 base !
    <# # # # # # # # # #> type space ." c, "
    r> base !
  ;

: bc ( n -- )
    base @  >r hex
    <# # # [char] $ hold #> type 
    ."  c, "
    r> base !

  ;

: .datas ( -- )
    PICTURE_SIZE PICTURE_HEIGHT / to BITS_RATIO
    PICTURE_HEIGHT 0 do
        cr
        BITS_RATIO 0 do
            BMP_RAW_DATAS
            PICTURE_HEIGHT 1- j - BITS_RATIO * + i + 
            c@ bc
        loop
    loop 
    cr
  ;


