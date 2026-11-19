\ *********************************************************************
\ Winmm definitions for musics vocabulary
\    Filename:      Winmm-definitions.fs
\    Date:          19 nov 2026
\    File Version:  0.1
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


\ @TODO: tout le fichier doit être testé et documenté

\ *** CODE QUI FONCTIONNE ******************************************************

vocabulary musics

only forth
also windows 
also structures
also musics definitions

z" Winmm.dll" dll Winmm

\ =====================================================================
\  PlaySound : simple playback of .wav files
\ =====================================================================

\ ** PlaySound flags **
$00000000 constant SND_SYNC
$00000001 constant SND_ASYNC
$00000002 constant SND_NODEFAULT
$00000004 constant SND_MEMORY
$00000008 constant SND_LOOP
$00000010 constant SND_NOSTOP
$00000040 constant SND_PURGE
$00000080 constant SND_APPLICATION
$00002000 constant SND_NOWAIT
$00010000 constant SND_ALIAS
$00020000 constant SND_FILENAME
$00040004 constant SND_RESOURCE

\ Plays a sound specified by the given file name, resource, or alias
z" PlaySoundA" 3 Winmm PlaySoundA ( pszSound hmod fdwSound -- fl )

\ play a .wav file, return immediately
: PlayWav ( zFile -- fl )
    0 SND_FILENAME SND_ASYNC or PlaySoundA
  ;

\ play a .wav file, wait until the end
: PlayWavSync ( zFile -- fl )
    0 SND_FILENAME PlaySoundA
  ;

\ play a .wav file in loop
: PlayWavLoop ( zFile -- fl )
    0 SND_FILENAME SND_ASYNC or SND_LOOP or PlaySoundA
  ;

\ stop any sound played by PlaySound
: StopSound ( -- )
    0 0 0 PlaySoundA drop
  ;


\ =====================================================================
\  WAVE OUT : volume et echantillons PCM
\ =====================================================================

z" waveOutGetNumDevs"     0 Winmm waveOutGetNumDevs     ( -- n )
z" waveOutSetVolume"      2 Winmm waveOutSetVolume      ( hwo dwVolume -- err )
z" waveOutGetVolume"      2 Winmm waveOutGetVolume      ( hwo lpdwVolume -- err )
z" waveOutOpen"           6 Winmm waveOutOpen
        ( lphwo uDeviceID pwfx dwCallback dwInstance fdwOpen -- err )
z" waveOutClose"          1 Winmm waveOutClose          ( hwo -- err )
z" waveOutReset"          1 Winmm waveOutReset          ( hwo -- err )
z" waveOutPrepareHeader"  3 Winmm waveOutPrepareHeader  ( hwo pwh cbwh -- err )
z" waveOutUnprepareHeader" 3 Winmm waveOutUnprepareHeader ( hwo pwh cbwh -- err )
z" waveOutWrite"          3 Winmm waveOutWrite          ( hwo pwh cbwh -- err )

$FFFFFFFF constant WAVE_MAPPER
1 constant WAVE_FORMAT_PCM

\ set main volume, left and right channels 0..$FFFF
: SetVolume ( left right -- )
    16 lshift or 0 swap waveOutSetVolume drop
;

\ @TODO: waveOutOpen/Write demandent les structures WAVEFORMATEX et
\        WAVEHDR, a definir avec le mecanisme 'structures'.


\ =====================================================================
\   MIDI OUT : notes directes sur le synthetiseur de Windows @TODO à tester
\ =====================================================================

z" midiOutGetNumDevs" 0 Winmm midiOutGetNumDevs ( -- n )
z" midiOutOpen"       5 Winmm midiOutOpen
        ( lphmo uDeviceID dwCallback dwInstance fdwOpen -- err )
z" midiOutClose"      1 Winmm midiOutClose      ( hmo -- err )
z" midiOutReset"      1 Winmm midiOutReset      ( hmo -- err )
z" midiOutShortMsg"   2 Winmm midiOutShortMsg   ( hmo dwMsg -- err )
z" midiOutSetVolume"  2 Winmm midiOutSetVolume  ( hmo dwVolume -- err )
z" midiOutGetVolume"  2 Winmm midiOutGetVolume  ( hmo lpdwVolume -- err )

$FFFFFFFF constant MIDI_MAPPER
$00000000 constant CALLBACK_NULL
0 constant MMSYSERR_NOERROR

\ handle of opened MIDI device
create hMidi  8 allot

\ open default MIDI device
: MidiOpen ( -- err )
    hMidi MIDI_MAPPER 0 0 CALLBACK_NULL midiOutOpen
  ;

\ close MIDI device
: MidiClose ( -- err )
    hMidi @ midiOutClose
  ;

\ assemble a short MIDI message
: MidiMsg ( status data1 data2 -- msg )
    16 lshift swap 8 lshift or or
  ;

\ send a short MIDI message
: MidiShort ( status data1 data2 -- )
    MidiMsg hMidi @ swap midiOutShortMsg drop
;

\ ch = canal MIDI 0..15, note = 0..127, vel = 0..127
: NoteOn  ( note vel ch -- )
    $90 or rot rot MidiShort
;

: NoteOff ( note vel ch -- )
    $80 or rot rot MidiShort
;

\ select instrument
: Instrument ( prog ch -- )
    $C0 or swap 0 MidiShort
;

\ ex: MidiOpen drop
\     60 127 0 NoteOn  500 ms  60 0 0 NoteOff
\     MidiClose drop



forth definitions

<EOF>


\ *** CODE A TESTER ************************************************************






\ =====================================================================
\  MCI : commandes multimedia en texte
\ =====================================================================

\ Sends a command string to the Media Control Interface
z" mciSendStringA" 4 Winmm mciSendStringA
        ( lpCommand lpReturn uReturnLen hwndCallback -- err )

\ Retrieves a string that describes the specified MCI error code
z" mciGetErrorStringA" 3 Winmm mciGetErrorStringA ( err lpBuf len -- fl )

\ send a MCI command without return string
\ ex: z" open monson.wav alias snd" mci drop
\     z" play snd" mci drop
\     z" close snd" mci drop
: mci ( zCmd -- err )
    0 0 0 mciSendStringA
;


\ =====================================================================
\  TIMERS multimedia
\ =====================================================================

z" timeGetTime"    0 Winmm timeGetTime    ( -- ms )
z" timeBeginPeriod" 1 Winmm timeBeginPeriod ( uPeriod -- err )
z" timeEndPeriod"  1 Winmm timeEndPeriod  ( uPeriod -- err )

forth definitions

<EOF>


PlaySound : PlaySoundA, les constantes SND_* et des mots simples (PlayWav, PlayWavSync, PlayWavLoop, StopSound).
MCI : mciSendStringA, mciGetErrorStringA et un mot mci qui envoie une commande texte (open, play, close).
MIDI : midiOutOpen, midiOutShortMsg et leurs voisines, plus MidiOpen, NoteOn, NoteOff, Instrument et MidiClose. Un exemple d’usage est en commentaire dans le fichier.
WaveOut : les imports, y compris waveOutOpen/waveOutWrite, et un mot SetVolume (gauche/droite, de 0 à $FFFF). La lecture d’échantillons PCM reste à faire : elle demande les structures WAVEFORMATEX et WAVEHDR, que je n’ai pas osé définir sans connaître votre syntaxe structures.
Timers : timeGetTime, timeBeginPeriod et timeEndPeriod.

Deux points à vérifier :

Déclaration de Winmm : j’ai supposé que cette bibliothèque se déclare comme User32, mais je ne sais pas comment vous le faites. J’ai laissé un @TODO en tête de fichier ; il faudra ajouter la ligne de déclaration, si elle n’existe pas ailleurs.
Mot ms : l’exemple MIDI suppose que votre eForth fournit ms pour attendre. Sinon, il faudra importer Sleep de Kernel32.

