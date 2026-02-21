\ *********************************************************************
\ random number generaotor
\    Filename:      random.fs
\    Date:          24 mar 2025
\    Updated:       24 mar 2025
\    File Version:  1.0
\    MCU:           eFORTH windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

.( random.fs loaded )

windows
\ returns the system's time and date
z" GetTickCount64"       0 Kernel32 GetTickCount64 ( addr -- )

variable seed

\ initialize seed value
: seed-init ( -- )
    GetTickCount64 seed !
  ;

\ seed-init

\ get a big random value
: random ( -- n ) 
    seed @ 1+
    1680716807 * dup
    seed !
  ;

\ get a random value between 0 and n-1
: rnd  ( n -- n' )
    random swap mod
  ;







