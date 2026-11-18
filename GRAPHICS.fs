\ *********************************************************************
\ Graphics tests
\    Filename:      GRAPHICS.fs
\    Date:          09 mar 2023
\    Updated:       18 nov 2026
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


\ stop compilation before end of included file
: <EOF> ( -- )
    #tib @ >in ! ;

\ load windows extensions
needs tools/dumpTool.fs


\ load graphics extensions
needs extensions/Gdi32-definitions.fs


\ load windows extensions
needs extensions/User32-definitions.fs


needs graphics/main.fs
