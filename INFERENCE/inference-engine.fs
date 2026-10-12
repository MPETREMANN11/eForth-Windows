
\ *********************************************************************
\ Definitions for inference engine
\    Filename:      main.fs
\    Date:          08 oct 2026
\    Updated:       11 oct 2026
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
    only forth
  ;


\ *** facts definitions ***********************************************

\ create a new fact
: FACT: ( n|fl comp: -- <word> | exec: -- n )
    in-facts
    value
    in-forth
  ;


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
: exec-condition [ addr -- ]
    addr ->condition @ execute
  ;

\ execute the action
: exec-action [ fl addr -- ]
    fl if
        addr ->action @ execute
    then
  ;

\ Mark rule as triggered
\ Indicate that a rule triggered in this cycle
: set-flag [ fl addr -- ]
    TRUE addr ->execFlag !
    TRUE to MODIFIED_RULE
  ;

\ create a new rule
: RULE: 
    in-rules
    create
        swap , ,    ( store xt condition and xt action )
        0 ,         ( store initial flag, value FALSE )
        in-forth
    does>
        \ Execute rule if has not yet been triggered in the cycle
        dup ->execFlag @ 0= if
            dup dup
            exec-condition
            exec-action
            set-flag
        else
            drop
        then
  ;

\ RULES example

:NONAME ." ceci est une condition" TRUE ;
:NONAME ." ceci est une action" ;
RULE: test1

