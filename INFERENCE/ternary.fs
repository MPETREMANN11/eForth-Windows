\ *********************************************************************
\ Ternary logic operators
\    Filename:      ternary.fs
\    Date:          16 nov 2026
\    Updated:       16 nov 2026
\    File Version:  1.0
\    MCU:           eForth Windows
\    Source:        https://rosettacode.org/wiki/Ternary_logic#Forth
\    Author:        unknow
\    GNU General Public License
\ *********************************************************************

 1 constant maybe
-1 constant true

: tnot 
    dup maybe <> if invert then ;
: tand 
    and ;
: tor 
    or ;
: tequiv 
    2dup and rot tnot rot tnot and or ;
: timply ( a b -- a=>b ) 
    swap tnot tor ;
: txor 
    tequiv tnot ;


: t. ( n -- )
    dup true = if 
        drop ." T" exit 
    then
    0= if 
        ." F" else ." ?" 
    then 
  ;

: table2. ( xt -- )
    cr ."     T  F  ?"
    cr ."   ----------"
    2 true DO
        cr I t.  ."  | "
        2 true DO
            dup J I rot execute t. 
            2 spaces
        LOOP
    LOOP DROP
  ;

: table1. ( xt -- )
    2 true DO
        CR I t. ."  | "
        dup I swap execute t.
    LOOP DROP
  ;

: demo
    cr ." [NOT]"   ['] tnot   table1. cr
    cr ." [AND]"   ['] tand   table2. cr
    cr ." [OR]"    ['] tor    table2. cr
    cr ." [XOR]"   ['] txor   table2. cr
    cr ." [IMPLY]" ['] timply table2. cr
    cr ." [EQUIV]" ['] tequiv table2. cr
  ;

page demo

