\ *********************************************************************
\ USER32 tests
\    Filename:      USER32.fs
\    Date:          12 nov 2024
\    Updated:       01 dec 2024
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


DEFINED? --user32 [if] forget --user32  [then]
create --user32


\ load windows extensions
needs tools/dumpTool.fs


\ load graphics extensions
needs extensions/Gdi32-definitions.fs


\ load windows extensions
needs extensions/User32-definitions.fs



needs user32/main.fs
