\ *********************************************************************
\ Graphics tests
\    Filename:      MUSICS.fs
\    Date:          19 nov 2026
\    Updated:       19 nov 2026
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
\ needs extensions/Gdi32-definitions.fs

\ load windows extensions
\ needs extensions/User32-definitions.fs

\ load Winmm extensions
needs extensions/Winmm-definitions.fs


needs MUSICS/main.fs
