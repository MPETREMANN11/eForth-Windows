\ *********************************************************************
\ PlaySoundA test
\    Filename:      02_test_PlaySoundA.fs
\    Date:          19 nov 2026
\    Updated:       19 nov 2026
\    File Version:  1.0
\    Forth:         eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

musics

: play-alias ( zStr -- )
    0                 \ hmod = NULL
    SND_ALIAS SND_ASYNC or
    PlaySoundA drop 
  ;

z" SystemAsterisk"  play-alias
\ z" SystemExclamation" play-alias
\ z" SystemHand"      play-alias
