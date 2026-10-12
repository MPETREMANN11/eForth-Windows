\ *********************************************************************
\ Config parameters for inference engine
\    Filename:      config.fs
\    Date:          08 oct 2026
\    Updated:       10 oct 2026
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

 0 value DEUTSCH
 0 value ENGLISH
 0 value FRANCAIS

: reset-languages ( -- )
    0 to DEUTSCH
    0 to ENGLISH
    0 to FRANCAIS
  ;

: in-english ( -- )
    reset-languages
    -1 to ENGLISH
  ;

: en-francais ( -- )
    reset-languages
    -1 to FRANCAIS
  ;

: auf-deutsch ( -- )
    reset-languages
    -1 to DEUTSCH
  ;



