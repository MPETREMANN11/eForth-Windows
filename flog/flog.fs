\ *********************************************************************
\ FLOG main words
\    Filename:      flog.fs
\    Date:          10 nov 2024
\    Updated:       10 nov 2024
\    File Version:  1.1
\    Forth:         eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************



vocabulary facts
vocabulary rules

vocabulary flog 
    flog definitions

\ select vocs: facts flog forth, compile in facts vocabulary
: in-facts ( -- )
    forth also flog also 
    facts definitions
  ;

\ select vocs: rules flog forth, compile in rules vocabulary
: in-rules ( -- )
    forth also flog also 
    rules definitions
  ;

: in-flog ( -- )
    only forth flog
  ;

\ create a new fact in facts vocabulary
: FACT:  ( comp: -- <name> )
    in-facts            \ compile fact in facts vocabulary
    >in @ >r
    bl parse find 0=
    if
        r> >in ! 
        -1 value
    else
        r> drop
    then
    in-flog
  ;

: FAIT:                 \ FACT: in french
    FACT:  ;

FAIT: x1
FAIT: x2
FAIT: x1



