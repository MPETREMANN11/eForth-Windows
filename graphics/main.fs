\ *********************************************************************
\ Graphics extensions main file
\    Filename:      main.fs
\    Date:          09 mar 2023
\    Updated:       24 may 2023
\    File Version:  1.0
\    MCU:           eFORTH
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


DEFINED? --graphics [if] forget --graphics  [then]
create --graphics

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

\ load graphics extensions
include graphicsExtensions.fs

\ load graphics tests
include config.fs

\ load graphics tests
include tests/graphicsTests.fs

\ load 01_hello.fs test
\ include tests/01_hello.fs
