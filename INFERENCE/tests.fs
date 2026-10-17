\ *********************************************************************
\ Tests for inference engine
\     Filename:     tests.fs
\     Date:         08 oct 2026
\     Updated:      15 oct 2026
\     File Version: 1.0
\     MCU:          eForth Windows
\     Copyright:    Marc PETREMANN
\     Author:       Marc PETREMANN
\     GNU General Public License
\ *********************************************************************


\ : explain-rule { xtAddr -- }
\     xtAddr >name type cr
\   ;
\ 
\ \ List all rules
\ forth definitions also internals
\ : list-rules ( -- )
\     rules context @ @
\     begin dup nonvoc? 
\     while 
\         dup explain-rule
\         >link
\     repeat 
\     drop
\   ;
\ only


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

reset-rules



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
