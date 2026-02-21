\ *********************************************************************
\ 2048 game tests
\    Filename:      tests.fs
\    Date:          10 mar 2025
\    Updated:       24 mar 2025
\    File Version:  1.0
\    MCU:           eFORTH windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


.( tests.fs loaded )


\ test grid display
seed-init
grid-init

\ push 1 or 2 on stack
: new-value ( -- n )
    10 rnd
    7 > if
        2 exit
    then
    1
  ;

: tt 20 0 do new-value . key drop loop ;

