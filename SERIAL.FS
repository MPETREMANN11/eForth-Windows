\ *********************************************************************
\ Serial tests
\    Filename:      SERIAL.fs
\    Date:          06 dec 2024
\    Updated:       06 dec 2024
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



\ load windows extensions
needs tools/dumpTool.fs


\ load structures extensions
\ needs extensions/structures-definitions.fs

\ load graphics extensions
\ needs extensions/Kernel32-definitions.fs

\ load graphics extensions
needs extensions/User32-definitions.fs

\ load graphics extensions
needs extensions/Gdi32-definitions.fs



needs serial/main.fs

