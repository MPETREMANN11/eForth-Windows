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


\ include FactsRulesLoan.fs

list-facts
list-rules


: my-word ( -- )
    [FRANCAIS] ?\ ." mon texte en français"
    [ENGLISH]  ?\ ." my text in english"
  ;

\ List all rules
forth definitions also internals
: lr ( -- )
    .rules-banner
    rules context @ @
    begin dup nonvoc?
    while
        dup explain-rule
        >link
    repeat
    drop
  ;
only

forth definitions also internals

\ Parcourt la chaîne récursivement jusqu'au bout, puis affiche au dépilage
: (lr-rec) ( threads -- )
    dup nonvoc? if
        dup >link (lr-rec)   \ Empile la suite et descend
    else
        drop                 \ Fin du vocabulaire (fond de pile)
    endif 
    explain-rule         \ Exécute l'affichage en remontant
  ;

: lr ( -- )
    .rules-banner
    rules context @ @
    (lr-rec) ;

only




forth
forget (lr-fac)

\ List all facts
forth definitions also internals
\ Explore facts vocabulary recursively and display facts 
: (lr-fac) ( thread -- )
    dup nonvoc? if
        dup >link recurse   \ 1. Descend jusqu'au fond
        explain-fact
    else
        drop                \ 3. Fond de chaîne : nettoie et remonte sans rien exécuter
    then ;

: list-facts ( -- )
    .facts-banner
    facts context @ @ (lr-fac) ;
only


facts context @ @ (lr-fac)




