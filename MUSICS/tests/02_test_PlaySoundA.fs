\ *********************************************************************
\ PlaySoundA test
\    Filename:      02_test_PlaySoundA.fs
\    Date:          19 nov 2026
\    Updated:       20 nov 2026
\    File Version:  1.0
\    Forth:         eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

musics

\ set main volume
$ffff $ffff SetVolume

: play-alias ( zStr -- )
    0                 \ hmod = NULL
    SND_ALIAS SND_ASYNC or
    PlaySoundA mm-success 
  ;

z" SystemAsterisk"  play-alias
z" SystemExclamation" play-alias
z" SystemHand"      play-alias

\ Fichier .wav, lecture asynchrone (rend la main tout de suite)
z" C:\Windows\Media\tada.wav" 0 SND_FILENAME SND_ASYNC or PlaySoundA mm-abort

\ Fichier .wav, lecture asynchrone (rend la main tout de suite)
z" C:\Users\mpmp9\Downloads\cockcrow.wav" 0 SND_FILENAME SND_ASYNC or PlaySoundA 

z" C:\Users\mpmp9\OneDrive\Bureau\eforth\MUSICS\tests\cockcrow.wav" 0 SND_FILENAME SND_ASYNC or PlaySoundA 

cd tests
z" cockcrow.wav" 0 SND_FILENAME SND_ASYNC or PlaySoundA 



\ Même chose avec le mot utilitaire
z" C:\Windows\Media\tada.wav" PlayWav

\ Lecture synchrone : le mot ne rend la main qu'à la fin du son
z" C:\Windows\Media\tada.wav" PlayWavSync

\ Lecture en boucle, puis arrêt après 2 secondes
z" C:\Windows\Media\chimes.wav" PlayWavLoop
2000 ms
StopSound

