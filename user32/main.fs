\ *********************************************************************
\ User32 extensions main file
\    Filename:      main.fs
\    Date:          27 nov 2024
\    Updated:       02 dec 2024
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


DEFINED? L, invert [IF]
\ compile 32 bits value in dictionnary
: L,  ( u -- )
    dup c,
    8 rshift dup c,
    8 rshift dup c,
    8 rshift dup c,
    drop
  ;
[THEN]


DEFINED? .( invert [IF]
-1 value COMMENT_DISPLAY
: .(   
    [char] ) parse 
    COMMENT_DISPLAY if
        type cr 
    else
        2drop
    then
  ; immediate
[THEN]

\ load graphics tests
include config.fs


\ 100_hwndHdcTests.fs
\ include tests/100_hwndHdcTests.fs

\ 101_rectanglesTests.fs
\ include tests/101_rectanglesTests.fs

\ 102_menusTests.fs
\ include tests/102_menusTests.fs

\ 103_CreateWindowEx.fs
needs create-window.fs
needs tests/103_CreateWindowEx.fs

