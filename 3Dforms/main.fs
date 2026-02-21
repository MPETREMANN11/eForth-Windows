\ *********************************************************************
\ FILES 
\    Filename:      main.fs
\    Date:          16 feb 2026
\    Updated:       16 feb 2026
\    File Version:  1.0
\    Forth:         eFORTH Windows
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


DEFINED? --3D [IF]
    forget --3D
[THEN]
create --3D

include dumpTool.fs
include Gdi32-definitions.fs
include structsAndDatas.fs
include 3Dmodelisation.fs
include manipulations.fs
include 3Dobjects.fs
include tests.fs


