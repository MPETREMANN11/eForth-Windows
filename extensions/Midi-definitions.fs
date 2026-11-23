\ *********************************************************************
\ MIDI definitions (complements for Winmm-definitions.fs)
\    Filename:       Midi-definitions.fs
\    Requires: Winmm-definitions.fs (MidiOpen, MidiShort, NoteOn, NoteOff,
\              Instrument, MidiClose) and the word 'ms'
\    Date:          23 nov 2026
\    Updated:       23 nov 2026
\    File Version:  1.0  @TODO: tout est à tester
\    Forth:         eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


only forth
windows also
structures also
musics definitions

\ =====================================================================
\ 1. INSTRUMENTS (General MIDI, numerotation 0..127 dans le message)
\ =====================================================================
\ Le synthe de Windows (Microsoft GS Wavetable Synth) suit General MIDI.
\ Les numeros de la documentation GM vont de 1 a 128 : retirer 1.
\ Canal 10 (index 9) = percussions, chaque note = un instrument de batterie.

  0 constant GM_PIANO
  4 constant GM_EPIANO
 11 constant GM_VIBRAPHONE
 16 constant GM_ORGAN
 19 constant GM_CHURCH_ORGAN
 24 constant GM_GUITAR_NYLON
 25 constant GM_GUITAR_STEEL
 32 constant GM_BASS
 40 constant GM_VIOLIN
 48 constant GM_STRINGS
 56 constant GM_TRUMPET
 65 constant GM_ALTO_SAX
 73 constant GM_FLUTE
 80 constant GM_SQUARE_LEAD
 89 constant GM_WARM_PAD

 9 constant DRUM_CH
35 constant DRUM_BASS
38 constant DRUM_SNARE
42 constant DRUM_HIHAT
49 constant DRUM_CRASH

\ ex: GM_FLUTE 0 Instrument     \ flute sur le canal 0
\     GM_BASS  1 Instrument     \ basse sur le canal 1


\ =====================================================================
\ 2. CONTROL CHANGE : volume, panoramique, banque, silence
\ =====================================================================

\ send a Control Change message
: CC ( cc value ch -- )
    $B0 or rot rot MidiShort
;

: Bank        ( msb ch -- )   0 rot rot CC ;   \ Bank Select MSB
: ChVolume    ( 0..127 ch -- ) 7 rot rot CC ;
: Pan         ( 0..127 ch -- ) 10 rot rot CC ; \ 64 = centre
: Sustain     ( 0|127 ch -- )  64 rot rot CC ; \ pedale de sustain
: AllNotesOff ( ch -- )        123 0 rot CC ;
: AllSoundOff ( ch -- )        120 0 rot CC ;


\ =====================================================================
\ 3. POLYPHONIE : plusieurs notes en meme temps
\ =====================================================================
\ MIDI est polyphonique par nature : il suffit d'envoyer plusieurs
\ NoteOn sans NoteOff entre eux. Chaque canal a son instrument.

variable MidiCh
variable MidiVel

: NoteOn!  ( note -- )   MidiVel @ MidiCh @ NoteOn ;
: NoteOff! ( note -- )   0 MidiCh @ NoteOff ;

\ chord of 3 notes
: Chord3 ( n1 n2 n3 vel ch -- )
    MidiCh ! MidiVel !
    NoteOn! NoteOn! NoteOn!
;

: ChordOff3 ( n1 n2 n3 ch -- )
    MidiCh !
    NoteOff! NoteOff! NoteOff!
;

\ ex: MidiOpen drop
\     GM_PIANO 0 Instrument
\     60 64 67 100 0 Chord3      \ do majeur
\     1500 ms
\     60 64 67 0 ChordOff3
\     MidiClose drop
\
\ ex polyphonie multi-instruments :
\     GM_PIANO 0 Instrument   GM_BASS 1 Instrument
\     60 100 0 NoteOn   36 110 1 NoteOn   \ piano + basse ensemble

\ =====================================================================
\ 4. ENVELOPPE (Attack / Decay / Release)
\ =====================================================================
\ General MIDI ne definit pas de reglage ADSR complet. Deux voies :
\  a) Control Change "Sound Controllers" (GM2) : 72 release, 73 attack,
\     75 decay. Valeur 64 = reglage normal. Selon le synthe, ignore.
\  b) NRPN Roland GS : attack/decay/release par instrument.
\ Pas de "sustain level" : le CC 64 est la pedale, pas le niveau ADSR.
\ @TODO: tester lequel des deux est pris en compte par le synthe.

: Attack  ( 0..127 ch -- )  73 rot rot CC ;
: Decay   ( 0..127 ch -- )  75 rot rot CC ;
: Release ( 0..127 ch -- )  72 rot rot CC ;

\ NRPN : CC99 = msb, CC98 = lsb, CC6 = valeur
: NRPN ( msb lsb val ch -- )
    MidiCh ! >r >r
    99 swap MidiCh @ CC
    r> 98 swap MidiCh @ CC
    r>  6 swap MidiCh @ CC
;

\ GS : NRPN 1,$63 = attack ; 1,$64 = decay ; 1,$66 = release
: GsAttack  ( 0..127 ch -- )  >r 1 $63 rot r> NRPN ;
: GsDecay   ( 0..127 ch -- )  >r 1 $64 rot r> NRPN ;
: GsRelease ( 0..127 ch -- )  >r 1 $66 rot r> NRPN ;

\ ex: GM_STRINGS 0 Instrument
\     120 0 Attack              \ attaque lente (CC)
\     60 100 0 NoteOn  2000 ms  60 0 0 NoteOff
\     120 0 GsAttack            \ meme chose via NRPN GS

forth definitions




