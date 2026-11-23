\ *********************************************************************
\ Winmm definitions for musics vocabulary
\    Filename:      Winmm-definitions.fs
\    Date:          19 nov 2026
\    Updaate:       23 nov 2026
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
windows also
structures also
musics definitions

\ Loads the Windows dynamic-link library Winmm.dll and associates it with the 
\ Forth word Winmm in order to call its multimedia functions.
z" Winmm.dll" dll Winmm


\ errors constants
 0 constant MMSYSERR_NOERROR
 5 constant MMSYSERR_INVALHANDLE
 6 constant MMSYSERR_NODRIVER
 7 constant MMSYSERR_NOMEM
 8 constant MMSYSERR_NOTSUPPORTED

\ erreors codes for midi interface
 2 constant MMSYSERR_BADDEVICEID    \ The specified device identifier is out of range.
 4 constant MMSYSERR_ALLOCATED      \ The specified resource is already allocated.
11 constant MMSYSERR_INVALPARAM     \ The specified pointer or structure is invalid.
68 constant MIDIERR_NODEVICE        \ No MIDI port was found. This error occurs only when the mapper is opened.



\ Interrupts the program in the event of an error
: mm-abort ( fl -- )
    dup MMSYSERR_NOERROR = if  drop exit  then
    case
        MMSYSERR_BADDEVICEID of
            abort" The specified device identifier is out of range"
        endof
        MMSYSERR_ALLOCATED of
            abort" The specified resource is already allocated"
        endof
        MMSYSERR_INVALHANDLE of
            abort" The specified device handle is not valid"
        endof
        MMSYSERR_NODRIVER of
            abort" No device driver is present"
        endof
        MMSYSERR_NOMEM of
            abort" Unable to allocate or lock memory"
        endof
        MMSYSERR_NOTSUPPORTED of
            abort" The definition is not supported"
        endof
        MMSYSERR_INVALPARAM of
            abort" The specified pointer or structure is invalid"
        endof
        MIDIERR_NODEVICE of
            abort" No MIDI port was found"
        endof
    endcase
  ;

\ Interrupts the program if fl is false
: mm-success ( fl -- )
    if  exit  then
    abort" Undefined erreor"
  ;


\ =====================================================================
\  PlaySound : simple playback of .wav files | tests OK
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
: PlayWav ( zFile -- )
    0 SND_FILENAME SND_ASYNC or PlaySoundA mm-success
  ;

\ play a .wav file, wait until the end
: PlayWavSync ( zFile -- )
    0 SND_FILENAME PlaySoundA mm-success
  ;

\ play a .wav file in loop
: PlayWavLoop ( zFile -- )
    0 SND_FILENAME SND_ASYNC or SND_LOOP or PlaySoundA mm-success
  ;

\ stop any sound played by PlaySound
: StopSound ( -- )
    0 0 0 PlaySoundA mm-success
  ;


\ =====================================================================
\  WAVE OUT : volume et echantillons PCM
\ =====================================================================

z" waveOutGetNumDevs"       0 Winmm waveOutGetNumDevs     ( -- n )
z" waveOutSetVolume"        2 Winmm waveOutSetVolume      ( hwo dwVolume -- err )
z" waveOutGetVolume"        2 Winmm waveOutGetVolume      ( hwo lpdwVolume -- err )
z" waveOutOpen"             6 Winmm waveOutOpen
        ( lphwo uDeviceID pwfx dwCallback dwInstance fdwOpen -- err )
z" waveOutClose"            1 Winmm waveOutClose          ( hwo -- err )
z" waveOutReset"            1 Winmm waveOutReset          ( hwo -- err )
z" waveOutPrepareHeader"    3 Winmm waveOutPrepareHeader  ( hwo pwh cbwh -- err )
z" waveOutUnprepareHeader"  3 Winmm waveOutUnprepareHeader ( hwo pwh cbwh -- err )
z" waveOutWrite"            3 Winmm waveOutWrite          ( hwo pwh cbwh -- err )

$FFFFFFFF constant WAVE_MAPPER
1 constant WAVE_FORMAT_PCM

\ set main volume, left and right channels 0..$FFFF
: SetVolume ( left right -- )
    16 lshift or 0 swap waveOutSetVolume mm-abort
  ;

\ waveOutOpen/Write demandent les structures WAVEFORMATEX et WAVEHDR

struct WAVEFORMATEX ( -- n )
    i16 field ->wFormatTag          \ audio format type (e.g. WAVE_FORMAT_PCM, WAVE_FORMAT_IEEE_FLOAT, WAVE_FORMAT_EXTENSIBLE)
    i16 field ->nChannels           \ number of audio channels (1 for mono, 2 for stereo)
    i32 field ->nSamplesPerSec      \ sampling rate in hertz (e.g., 11025, 44100, 48000)
     i8 field ->nAvgBytesPerSec     \ average data rate in bytes per second (nSamplesPerSec * nBlockAlign for PCM)
    i16 field ->nBlockAlign         \ size of a sample block in bytes (nChannels * wBitsPerSample / 8 for PCM)
    i16 field ->wBitsPerSample      \ number of bits per sample, depending on wFormatTag (e.g., 8 or 16 for standard PCM)
    i16 field ->cbSize              \ size in bytes of any additional information appended to the end of the WAVEFORMATEX 
                                    \ structure (0 if no extension is required, as is the case for standard PCM)

struct WAVEHDR ( -- n )  \ @TODO: à vérifier
    i64 field ->lpData              \ pointer to the buffer containing the audio data
    i32 field ->dwBufferLength      \ size of the buffer, in bytes
    i32 field ->dwBytesRecorded     \ number of bytes actually recorded (used in audio capture
    i64 field ->dwUser              \ user-defined data (a pointer or custom value associated with the buffer)
    i32 field ->dwFlags             \ status or control flags (can be combined using |)
    i32 field ->dwLoops             \ number of loop repetitions during audio playback (used in conjunction with loop flags)
    i64 field ->lpNext              \ reserved for system use
    i64 field ->reserved            \ reserved for system use


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


\ handle of opened MIDI device
create hMidi  8 allot

\ open default MIDI device
: MidiOpen ( -- )
    hMidi MIDI_MAPPER 0 0 CALLBACK_NULL midiOutOpen mm-abort
  ;

\ close MIDI device
: MidiClose ( -- )
    hMidi @ midiOutClose mm-abort
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

\ =====================================================================
\  TIMERS multimedia
\ =====================================================================

z" timeGetTime"     0 Winmm timeGetTime     ( -- ms )
z" timeBeginPeriod" 1 Winmm timeBeginPeriod ( uPeriod -- err )
z" timeEndPeriod"   1 Winmm timeEndPeriod   ( uPeriod -- err )


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


forth definitions

<EOF>

MCI : mciSendStringA, mciGetErrorStringA et un mot mci qui envoie une commande texte (open, play, close).

MIDI : midiOutOpen, midiOutShortMsg et leurs voisines, plus MidiOpen, NoteOn, NoteOff, Instrument et MidiClose. 
    Un exemple d’usage est en commentaire dans le fichier.

WaveOut : les imports, y compris waveOutOpen/waveOutWrite, et un mot SetVolume (gauche/droite, de 0 à $FFFF). 
    La lecture d’échantillons PCM reste à faire : elle demande les structures WAVEFORMATEX et WAVEHDR, que je n’ai 
    pas osé définir sans connaître votre syntaxe structures.

Timers : timeGetTime, timeBeginPeriod et timeEndPeriod.

