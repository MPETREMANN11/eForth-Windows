\ *********************************************************************
\ Tests for inference engine
\    Filename:      tests.fs
\    Date:          08 oct 2026
\    Updated:       08 oct 2026
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

structures
struct __RULES
    ptr field ->condition   \ Rule condition field
    ptr field ->action      \ field of the rule's action part
    ptr field ->execFlag    \ rule execution flag field, set to TRUE if rule executed
forth

\ execute the action
: exec-action ( addr fl -- )
    if      ->action @ execute
    else    drop
    then
  ;

: RULE:
    create
        swap , ,  0 ,
    does>
        { addr }
        addr ->condition @ execute
        if
            addr ->action @ execute
        then
  ;

:NONAME ." ceci est une condition" -1 ;
:NONAME ." ceci est une action" ;
RULE: test1
test1

