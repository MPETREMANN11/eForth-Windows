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
z" SystemExclamation" play-alias
z" SystemHand"      play-alias

\ Fichier .wav, lecture asynchrone (rend la main tout de suite)
z" C:\Windows\Media\tada.wav" 0 SND_FILENAME SND_ASYNC or PlaySoundA drop

\ Même chose avec le mot utilitaire
z" C:\Windows\Media\tada.wav" PlayWav drop

\ Lecture synchrone : le mot ne rend la main qu'à la fin du son
z" C:\Windows\Media\tada.wav" PlayWavSync drop

\ Lecture en boucle, puis arrêt après 2 secondes
z" C:\Windows\Media\chimes.wav" PlayWavLoop drop
2000 ms
StopSound

