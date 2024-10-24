\ *********************************************************************
\ Z79FORTH listing 
\    Filename:      Z79FORTH.fs
\    Date:          18 oct. 2024
\    Updated:       23 oct. 2024
\    File Version:  1.0
\    MCU:           eFORTH windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

\ meta instructions
s" Z79Fconstants.fs" included

in-assembler
-1 to target-echo

\ ***  Z79Forth specifics meta instructions  ***********************************




\ ***  Z79Forth starting  ******************************************************

\ E7E9 11830338         (        forth.asm):01393 (4)     NPOP    cmpu    #NSTBOT
\ E7ED 2406             (        forth.asm):01394 (3)             bhs     @npop1          Anything >= than #NSTBOT indicates underflow
\ E7EF 3710             (        forth.asm):01395 (4+2)           pulu    x
\ E7F1 1037C1           (        forth.asm):01396 (4)             cmpr    0,x             Update CC based on the outcome
\ E7F4 39               (        forth.asm):01397 (4)             rts
\ E7F5 C601             (        forth.asm):01398 (2)     @npop1  ldb     #1              Data stack underflow
\ E7F7 BDE74C           (        forth.asm):01399 (7)             jsr     ERRHDLR         No return
\      E7FA             (        forth.asm):01400         DPOPRA  equ     *
\ E7FA 12               (        forth.asm):01401 (1)             nop

label: NPOP
        NSTBOT # cmpu,
        bhs, IF,
\             x pulu,
        ENDIF,
label: DPOPRA

