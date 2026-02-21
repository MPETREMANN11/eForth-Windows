\ *********************************************************************
\ SINUS table and trigs calcs 
\    Filename:      sinTab.fs
\    Date:          16 feb 2026
\    Updated:       16 feb 2026
\    File Version:  1.0
\    Forth:         eFORTH Windows
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


CREATE SINTAB 91 CELLS ALLOT

: init-sintab ( -- )
    91 0 DO
        i s>f PI f* 180e f/ fsin 
        256e f* f>s 
        i CELLS SINTAB + !
    LOOP ;

init-sintab

: get-sin-90 ( angle -- sin_val )
    360 MOD DUP 0 < IF 360 + THEN ( normalize 0-359 )
    CASE
        DUP 91 < OF CELLS SINTAB + @ ENDOF
        DUP 181 < OF 180 SWAP - CELLS SINTAB + @ ENDOF
        DUP 271 < OF 180 - CELLS SINTAB + @ NEGATE ENDOF
        360 SWAP - CELLS SINTAB + @ NEGATE
    ENDCASE ;

: get-cos-90 ( angle -- cos_val )
    90 + get-sin-90 ;



