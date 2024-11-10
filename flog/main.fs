\ *********************************************************************
\ FLOG / ....
\    Filename:      main.fs
\    Date:          10 nov 2024
\    Updated:       10 nov 2024
\    File Version:  1.0
\    Forth:         eFORTH Windows
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


DEFINED? --flog [IF]
    forget --flog
[THEN]
create --flog

\ load FLOG
include flog.fs

\ load FLOG tests
\ include tests/flogTest.fs


