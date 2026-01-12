\ *********************************************************************
\ FILES 
\    Filename:      main.fs
\    Date:          07 jan 2026
\    Updated:       07 jan 2026
\    File Version:  1.0
\    Forth:         eFORTH Windows
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


DEFINED? --bmp [IF]
    forget --bmp
[THEN]
create --bmp

needs dumpTool.fs
needs bmpStructure.fs
needs bmpDecode.fs
needs tests.fs


