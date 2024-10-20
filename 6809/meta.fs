\ *********************************************************************
\ 6809 assembler meta definitions
\    Filename:      meta.fs
\    Date:          18 oct. 2024
\    Updated:       18 oct. 2024
\    File Version:  1.0
\    MCU:           eFORTH windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


vocabulary target           \ vocabulary TARGET receive headers

vocabulary meta
meta definitions

0 value target-init?        \ flag at 0 if target is not initialized
0 value target-address      \ initial TARGET address
0 value target-size         \ size allocated for target
0 value target-offset       \ relative 
0 value dp-t                \ Dictionnary Pointer in Target

\ initialize target
: init-target  ( size taddr -- )
    to target-address          \ set real target address
    dup allot               \ allocate target size
    to target-size          \ set target-size
    -1 to target-init?      \ set target-initi? to true
  ;

\ define a new target
: target:  ( comp: size -- <name> | exec: -- addr )
    create
        here init-target
        target-address target-size erase
  ;

\ set offset in target
: offset!-t  ( n -- )
    to target-offset
  ;

\ abort if target is not initialized
: target-abort  ( -- )
    target-init? invert if
        abort" TARGET not initialized with target:"
    then
  ;

\ store byte in target
: c!-t  ( c taddr -- )
    target-abort            \ abort if target not initialized
    target-address dp-t +  target-offset -  c!
  ;

: !-t  ( n taddr -- )        \ stocke un mot 16 bits dans la cible
    target-abort             \ abort if target not initialized
    target-address dp-t +  target-offset -  !
  ;


\ push target dictionnary pointer
: here-t  ( -- taddr )     
    dp-t target-offset +
;

\ allot n bytes in target
: allot-t  ( n -- ) 
    target-abort            \ abort if target not initialized
    dp-t +!
;

\ compile byte in target
: c,-t  ( char -- )          
    target-abort            \ abort if target not initialized
    here-t c!-t 1 allot-t
;

\ compile 16 bits word in target
: ,-t  ( n -- )
    target-abort            \ abort if target not initialized
    here-t !-t 2 allot-t
;





