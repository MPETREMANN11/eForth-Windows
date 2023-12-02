\ *********************************************************************
\ DUMP tool for ESP32Forth
\    Filename:      dumpTool.txt
\    Date:          12 jan 2022
\    Updated:       02 dec 2023
\    File Version:  1.2
\    MCU:           ESP32-WROOM-32
\    Forth:         ESP32forth all versions 7.0.7.4++
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

\ Modifications:
\  02 dec 2022: replace all decimal values by hex values to prevent comp in other base than decimal

\ locals variables in DUMP:
\ START_ADDR      \ first address to dump
\ END_ADDR        \ latest address to dump
\ 0START_ADDR     \ first address for dump loop
\ LINES           \ number of lines for dump loop
\ myBASE          \ current numeric base
          
internals
: dump ( start len -- )
    cr cr ." --addr---  "
    ." 00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F  ------chars-----"
    2dup + { END_ADDR }             \ store latest address to dump
    swap { START_ADDR }             \ store START address to dump
    START_ADDR $10 / $10 * { 0START_ADDR } \ calc. addr for loop start
    $10 / 1+ { LINES }
    base @ { myBASE }               \ save current base
    hex
    \ outer loop
    LINES 0 do
        0START_ADDR i $10 * +        \ calc start address for current line
        cr <# # # # #  [char] - hold # # # # #> type
        space space     \ and display address
        \ first inner loop, display bytes
        $10 0 do
            \ calculate real address
            0START_ADDR j $10 * i + +
            ca@ <# # # #> type space \ display byte in format: NN
        loop 
        space
        \ second inner loop, display chars
        $10 0 do
            \ calculate real address
            0START_ADDR j $10 * i + +
            \ display char if code in interval 32-127
            ca@     dup $20 < over $7f > or
            if      drop [char] . emit
            else    emit
            then
        loop 
    loop
    myBASE base !               \ restore current base
    cr cr
  ;
forth
