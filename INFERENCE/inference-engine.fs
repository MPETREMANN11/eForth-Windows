
\ *********************************************************************
\ Definitions for inference engine
\    Filename:      main.fs
\    Date:          08 oct 2026
\    Updated:       15 oct 2026
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

\ *** Definitions of vocabularies *************************************

vocabulary facts    \ new facts defined in this vocabulary
vocabulary rules    \ new rules defined in this vocabulary

: in-facts ( -- )
    facts definitions
  ;

: in-rules ( -- )
    rules definitions
  ;

: in-forth ( -- )
    only 
    forth definitions
  ;


\ *** facts definitions ***********************************************

structures
struct __FACTS
    ptr field ->value       \ fact value
    ptr field ->zstring     \ point to zstring
forth

\ create a new fact
: FACT: ( n|fl comp: -- <word> | exec: -- n )
    in-facts
    value                   \ create value
    ,                       \ compile pointer to zstring
    in-forth
  ;

\ display zstring and value in fact
\ example: ' <fact> explain-fact
: explain-fact { xtAddr -- }
    xtAddr >name type
    xtAddr >body ->zstring @ z>s cr  4 spaces type ."  = " 
    xtAddr >body ->value @ { value }
    value 0= if
        ." FALSE" exit
    then
    value -1 = if
        ." TRUE" exit
    then
    value .
  ;

forth definitions also internals
: list-facts ( -- )
    facts context @ @
    begin  dup nonvoc? 
    while  dup explain-fact cr cr >link 
    repeat 
    drop
    forth
  ;
only


\ reset fact value to FALSE
: (reset-fact) ( xt -- )
    >body FALSE swap !
  ;

\ reset values of all facts defined in facts vocabulary
forth definitions also internals
: reset-facts ( -- )
    facts context @ @
    begin  dup nonvoc? 
    while  dup (reset-fact) >link 
    repeat 
    drop cr ." All facts set to FALSE"
    forth
  ;
only


\ *** rules definitions ***********************************************

\ global flag. Set to TRUE if a rule is modified
0 value MODIFIED_RULE 

structures
struct __RULES
    ptr field ->condition   \ Rule condition field
    ptr field ->action      \ field of the rule's action part
    ptr field ->execFlag    \ rule execution flag field, set to TRUE if rule executed
forth

\ Executes the condition
: exec-condition ( addr -- fl )
    ->condition @ execute
  ;

\ execute the action
: exec-action ( addr -- )
    ->action @ execute
  ;

\ Mark rule as triggered
\ Indicate that a rule triggered in this cycle
: set-flag { addr -- }
    TRUE addr ->execFlag !
    TRUE to MODIFIED_RULE
  ;

\ create a new rule
: (RULE:) 
    create
        swap , ,    \ store xt condition and xt action
        0 ,         \ store initial flag, value FALSE
    does>  
        { addr }
        \ Execute rule if has not yet been triggered in the cycle
        addr ->execFlag @ 0= if
            addr ->condition @ execute if
                addr ->action @ execute
                addr set-flag
            then
        then
  ;

\ define new rule in rules vocabulary
: RULE: ( -- )
    in-rules
    (RULE:) 
    in-forth 
  ;

\ define condition of a rule
: :CONDITION ( -- fl )
    :NONAME
  ;

\ define action for a rule
: :ACTION ( -- )
    :NONAME
  ;

