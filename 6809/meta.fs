\ *********************************************************************
\ 6809 assembler meta definitions
\    Filename:      meta.fs
\    Date:          18 oct. 2024
\    Updated:       23 oct. 2024
\    File Version:  1.0
\    MCU:           eFORTH windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

vocabulary assembler        \ vocabulary ASSEMBLER with assembl. isntruct.
vocabulary target           \ vocabulary TARGET receive headers

vocabulary meta             \ voc. META for meta-assembler/compiler

: in-assembler  ( -- )
    only forth also
    meta also 
    assembler definitions also
  ;

: in-target  ( -- )
    only target definitions \ search order target >> forth
  ;

: in-meta  ( -- )
    only forth also
    meta definitions also   \ search order: meta >> forth
  ;

: in-forth  ( -- )
    only forth also
    forth definitions
    meta also 
    target also
    assembler
  ;


in-meta

0 value target-init?        \ flag at 0 if target is not initialized
0 value target-address      \ initial TARGET address
0 value target-size         \ size allocated for target
0 value target-offset       \ relative 
0 value dp-t                \ Dictionnary Pointer in Target

\ if true, display assembled datas
0 value target-echo

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

\ display byte in hex format
: #HH.  ( c -- )
    base @ >r
    0 <# # # #> type
    r> base !
  ;    

\ store byte in target
: c!-t  ( c taddr -- )
    target-abort            \ abort if target not initialized
    target-address dp-t +  target-offset -  
    target-echo if
        dup #HH.
    then
    c!
  ;

: !-t  ( n taddr -- )        \ stocke un mot 16 bits dans la cible
    target-abort             \ abort if target not initialized
    target-address dp-t +  target-offset -  
    target-echo if
        dup $100 /mod #HH. #HH.
    then
    !
  ;


\ push target dictionnary pointer
: here-t  ( -- taddr )     
    dp-t target-offset +
;

\ allot n bytes in target
: allot-t  ( n -- ) 
    target-abort            \ abort if target not initialized
    +to dp-t
;

\ increment dp-t
: dp-t++  ( -- )
    1 +to dp-t
  ;

\ compile byte in target
: c,-t  ( char -- )          
    target-abort            \ abort if target not initialized
    dp-t target-offset - c!
    dp-t++
;

\ compile two bytes in target
: ,-t  ( n -- )
    $100 /mod  c,-t  c,-t 
;

\ ***  META instructions:  *****************************************************



: label:  ( -- <name> | -- n )
    dp-t
    in-target 
    constant
    in-forth
  ;




