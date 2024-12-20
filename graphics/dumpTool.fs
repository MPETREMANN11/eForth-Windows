\ *********************************************************************
\ DUMP tool for eFORTH
\    Filename:      dumpTool.fs
\    Date:          12 jan 2022
\    Updated:       16 nov 2024
\    File Version:  1.1
\    Forth:         eFORTH all versions 7.x++
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

\ locals variables in DUMP:
\ START_ADDR      \ first address to dump
\ END_ADDR        \ latest address to dump
\ 0START_ADDR     \ first address for dump loop
\ LINES           \ number of lines for dump loop
\ myBASE          \ current numeric base
          
: dump ( start len -- )
    cr cr 
    229 bg  ." --addr---  "
    ." 00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F  ------chars-----" 0 bg
    2dup + { END_ADDR }             \ store latest address to dump
    swap { START_ADDR }             \ store START address to dump
    START_ADDR 16 / 16 * { 0START_ADDR } \ calc. addr for loop start
    16 / 1+ { LINES }
    base @ { myBASE }               \ save current base
    hex
    \ outer loop
    LINES 0 do
        0START_ADDR i 16 * +        \ calc start address for current line
        cr <# # # # #  [char] - hold # # # # #> type
        space space     \ and display address
        \ first inner loop, display bytes
        16 0 do
            \ calculate real address
            0START_ADDR j 16 * i + +
            c@ <# # # #> type space \ display byte in format: NN
        loop 
        space
        \ second inner loop, display chars
        16 0 do
            \ calculate real address
            0START_ADDR j 16 * i + +
            \ display char if code in interval 32-127
            c@     dup 32 < over 127 > or
            if      drop [char] . emit
            else    emit
            then
        loop 
    loop
    myBASE base !               \ restore current base
    cr cr
  ;
