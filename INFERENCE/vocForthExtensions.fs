\ *********************************************************************
\ Forth vocabulary extension
\    Filename:      vocForthExtension.fs
\    Date:          15 oct 2026
\    Updated:       20 oct 2026
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


\ *** Tools extending vocabulary **************************************

\ FALSE and TRUE are not defined in eForth Windows
 0 constant FALSE
-1 constant TRUE

\ wait a value and push it on stack
: val-input ( -- n )
    PAD 80 ACCEPT
    PAD SWAP
    EVALUATE
  ;

\ if lowercase char pressed, convert to UPPERcase
: Xkey ( -- KEY )
    key dup [CHAR] Z >
    if   32 -  then
  ;


\ *** poly language extension *****************************************

\ these values must be used in interpertation mode only
 0 value DEUTSCH
 0 value ENGLISH
 0 value FRANCAIS

: reset-languages ( -- )
    0 to DEUTSCH
    0 to ENGLISH
    0 to FRANCAIS
  ;

\ these words must be used in compilation mode only
: [DEUTSCH]
    DEUTSCH ;  immediate

: [ENGLISH]
    ENGLISH ;  immediate

: [FRANCAIS]
    FRANCAIS ;  immediate

\ Example:
\ : my-word ( -- )
\     [FRANCAIS] ?\ ." mon texte en français"
\     [ENGLISH]  ?\ ." my text in english"
\   ;

: in-english ( -- )
    -1 to ENGLISH
  ;

: en-francais ( -- )
    -1 to FRANCAIS
  ;

: auf-deutsch ( -- )
    -1 to DEUTSCH
  ;

\ Conditionnal compilation
: ?\ ( fl -- )
    0= if
        nl parse drop drop
    then
; immediate

\ set language to english, french or german
: select-language ( -- )
    reset-languages
    cr ."     +----------------------------------+"
    cr ."     |          SELECT LANGUAGE         |"
    cr ."     +----------------------------------+"
    cr
    cr ."  D - Deutsch"
    cr ."  E - English"
    cr ."  F - Français"
    cr
    cr ." Press key: " Xkey
    dup emit
    case
        [char] D of  auf-deutsch   endof
        [char] E of  in-english    endof
        [char] F of  en-francais   endof
    endcase
  ;





