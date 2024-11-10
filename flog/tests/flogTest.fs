\ *********************************************************************
\ tests for FLOG
\    Filename:      flogTest.fs
\    Date:          10 nov 2024
\    Updated:       10 nov 2024
\    File Version:  1.1
\    Forth:         eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


: tt  ( comp: -- <name> )
    >in @ >r
    bl parse find 0=
    if
        r> >in ! 
        create
        exit
    then
    r> drop
  ;





