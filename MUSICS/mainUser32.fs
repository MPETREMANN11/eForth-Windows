\ *********************************************************************
\ User32 extensions main file tests
\    Filename:      mainUser32.fs
\    Date:          27 nov 2024
\    Updated:       27 nov 2024
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


DEFINED? --user32 [if] forget --user32 [then]
create --user32

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

\ load windows extensions
include dumpTool.fs

\ load windows extensions
include User32-definitions.fs

\ load graphics extensions
include Gdi32-definitions.fs

\ load graphics tests
include config.fs


\ 100_hwndHdcTests.fs
\ include tests/100_hwndHdcTests.fs

\ 101_rectanglesTests.fs
include tests/101_rectanglesTests.fs

\ 102_menusTests.fs
\ include tests/102_menusTests.fs


