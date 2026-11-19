\ *********************************************************************
\ Musics  extensions main file
\    Filename:      main.fs
\    Date:          19 nov 2026
\    Updated:       19 nov 2026
\    File Version:  1.0
\    MCU:           eFORTH
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


DEFINED? --musics [if] forget --musics  [then]
create --musics

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


\ load musics tests
include config.fs

\ load graphics tests
\ include tests/graphicsTests.fs

\ load 01_hello.fs test
include tests/01_hello.fs

\ load 01_hello.fs test
include tests/03_test_SetVolume.fs
