
\ *********************************************************************
\ Definitions for inference engine
\    Filename:      main.fs
\    Date:          08 oct 2026
\    Updated:       20 oct 2026
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
    ptr field ->factZstring     \ point to fact zstring
forth

\ create a new fact
: FACT: ( n|fl comp: -- <word> | exec: -- n )
    in-facts
    value                   \ create value
    ,                       \ compile pointer to fact zstring
    in-forth
  ;

\ display fact zstring and value in fact
\ example: ' <fact> explain-fact
: explain-fact { xtAddr -- }
    xtAddr >name type
    xtAddr >body ->factZstring @ z>s cr  4 spaces type ."  = "
    xtAddr >body ->value @ { value }
    value 0= if
        ." FALSE"
    then
    value -1 = if
        ." TRUE"
    then
    value 0=  value -1 = or invert if
        value .
    then
    cr cr 
  ;

FRANCAIS ?\ z" LISTE DES FAITS:"
DEUTSCH  ?\ z" LISTE DER FAKTEN:"
ENGLISH  ?\ z" LIST OF FACTS:"
value fact-banner

: .facts-banner ( -- )
    cr 14 bg space
    fact-banner z>s type space
    0 bg cr cr  ;

forth definitions also internals
\ Explore facts vocabulary recursively and display facts 
: (lr-fac) ( thread -- )
    dup nonvoc? if
        dup >link recurse
        explain-fact
    else
        drop
    then ;

: list-facts ( -- )
    .facts-banner
    facts context @ @ (lr-fac) ;
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
    ptr field ->condition       \ Rule condition field
    ptr field ->action          \ field of the rule's action part
    ptr field ->execFlag        \ rule execution flag field, set to TRUE if rule executed
    ptr field ->ruleZstring     \ point to rule zstring
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
        ,           \ store auto-documentation string pointer
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
    facts
    :NONAME
  ;

\ define action for a rule
: :ACTION ( -- )
    facts
    :NONAME
  ;


\ display rule zstring and name in rules
\ example: ' <rule> explain-rule
: explain-rule { xtAddr -- }
    xtAddr >name type
    xtAddr >body ->ruleZstring @ z>s cr  4 spaces type ."  = "
    xtAddr >body ->execFlag @
    0= if
        09 bg ."  KO " 0 bg     \ red background
    else 
        10 bg ."  OK " 0 bg     \ green background
    then
    cr cr
  ;

FRANCAIS ?\ z" LISTE DES REGLES:"
DEUTSCH  ?\ z" REGELVERZEICHNIS:"
ENGLISH  ?\ z" LIST OF RULES:"
value rule-banner

: .rules-banner ( -- )
    cr 11 bg space
    rule-banner z>s type space
    0 bg cr cr  ;

\ List all rules
forth definitions also internals
\ Explore rule vocabulary recursively and display rules 
: (lr-rec) ( thread -- )
    dup nonvoc? if
        dup >link recurse   \ 1. Descend jusqu'au fond
        explain-rule
    else
        drop                \ 3. Fond de chaîne : nettoie et remonte sans rien exécuter
    then ;

: list-rules ( -- )
    .rules-banner
    rules context @ @ (lr-rec) ;
only


\ Resets the execution flag for all rules
forth definitions also internals
: reset-rules ( -- )
    rules context @ @
    begin dup nonvoc?
    while
        FALSE over >body ->execFlag !  \ set ->execFlag à FALSE
        >link
    repeat
    drop
  ;
only

\ Exécute un cycle d'inférence complet (chaînage avant)
\ : run-inference ( -- )
\   reset-rules
\   BEGIN
\     FALSE to MODIFIED_RULE
\
\     \ Parcourt toutes les règles du vocabulaire 'rules'
\     rules context @ @
\     begin dup nonvoc?
\     while
\       dup >body execute        \ Exécute le comportement du DOES> de la règle
\       >link
\     repeat
\     drop
\
\     MODIFIED_RULE              \ Continue tant qu'une règle a modifié l'état
\   0= UNTIL ;


\ ** Inreference general management ***********************************



: star-inference ( -- )
    page select-language
\     run-inference
  ;
