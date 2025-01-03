\ *********************************************************************
\ 6809 assembler
\    Filename:      assembler.fs
\    Date:          08 oct. 2024
\    Updated:       24 oct. 2024
\    File Version:  1.0
\    MCU:           eFORTH windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

forth definitions

\ definitions for general compatibility
: ?error  ( fl n -- )
    swap if
        throw
    else
        drop
    then
  ;

: within ( test low high -- flag )
   >r over < 0=  swap r> < and
;

: 8bit?  ( n -- fl )
    -$80  $7f  within ;

: 5bit?  ( n -- fl )
    -$10  $0f  within ;

: 2+  ( n -- n+2 )
    1+ 1+ ;

: 2-  ( n -- n-2 )
    1- 1- ;

\ ***  6809 ASSEMBLER main definitions  ****************************************

meta
assembler definitions

\ alias for word ,  in 6809 assembler
: (,)  ( n -- )
    ,
  ;

\ store word as hibyte, lobyte
: w,  ( n -- )
    $100 /mod  swap c, c,
  ;

\ store opcode with prefix (if any) in addr pointed by dp-t minus target-offset
: opcode,
    dup $FF00 and
    if  ,-t  exit  then
    c,-t
  ;

\ $00=immed, $10=direct, $20=indexed, $30=extended
variable mode

\ select immediate mode
: #  ( -- )
    $00 mode !
  ;

\ select direct mode
: <>  ( -- )
    $10 mode !
  ;

\ select indexed mode
: indexreg  ( rval postbyte -- postbyte )
    $20 mode !
    swap  1-  dup 0 3 within 0= 3 ?error    \ must be x,y,u, or s
    $20 * or                                \ put reg # in postbyte
  ;

: xmode ( comp: postbyte -- | exec: rval -- postbyte )
    create
        (,)                                 \ Simple Indexed Modes
    does>
        @ indexreg
  ;

$84 xmode 0,    $86 xmode A,    $85 xmode B,    $8B xmode D,
$80 xmode ,+    $81 xmode ,++   $82 xmode -,    $83 xmode --,

\ ***  6809 assembler: addressing modes  ***************************************

: ,  ( rval n -- n postbyte )
    swap $89 indexreg
  ;

: ,pcr  ( n -- n postbyte )
    $20 mode !
    $8d
  ;

: []
    mode @ $20 =    \ indexed:  postbyte -- postbyte
                    \ extended:        n -- n postbyte
    if
        dup $9d and $80 = 3 ?error  $10 +  \ indexed indirect
    else
        $20 mode !  $9f
    then            \ extended indirect
  ;

: reset  ( -- )
    $30 mode !
  ;

\ ***  register definitions  ***************************************************

$00 constant D      $01 constant X      $02 constant Y      $03 constant U
$04 constant S      $05 constant PC     $08 constant A      $09 constant B
$0A constant CCR    $0B constant DPR

Y constant IP       U constant SP       S constant RP       X constant W

\ ***  6809 assembler: inherent instruction  ***********************************

: inhop
    create
        (,)      \ opcode -- | Inherent Addressing
    does>
        @ opcode, reset \ -- | lay one or two bytes
  ;

$3a inhop abx,      $48 inhop asla,     $58 inhop aslb,     $47 inhop asra,
$57 inhop asrb,     $4f inhop clra,     $5f inhop clrb,     $43 inhop coma,
$53 inhop comb,     $19 inhop daa,      $4a inhop deca,     $5a inhop decb,
$4c inhop inca,     $5c inhop incb,     $48 inhop lsla,     $58 inhop lslb,
$44 inhop lsra,     $54 inhop lsrb,     $3d inhop mul,      $40 inhop nega,
$50 inhop negb,     $12 inhop nop,      $49 inhop rola,     $59 inhop rolb,
$46 inhop rora,     $56 inhop rorb,     $3b inhop rti,      $39 inhop rts,
$1d inhop sex,      $3f inhop swi,    $103f inhop swi2,   $113f inhop swi3,
$13 inhop sync,     $4d inhop tsta,     $5d inhop tstb,


\ ***  6809 assembler: immediate instructions  *********************************

: immop
    create
        (,)     \ opcode -- | Immediate Only (8-bit)
    does>
        mode @ 3 ?error
        @ c,-t c,-t reset
  ;  \ operand --

$3C immop CWAI,     $34 immop $PSHS,    $36 immop PSHU,     $35 immop PULS,
$37 immop PULU,     $1C immop $ANDCC,   $1A immop ORCC,

: rrop
    create          \ opcode -- | Register-Register
        (,)
    does>           \ srcrval dstrval --
        @ c,-t  swap $10 * + c,-t reset
  ;

$1E rrop EXG,    $1F rrop TFR,


\ ***  6809 assembler: +mode  **************************************************

\ modify operand per mode
: +mode  ( operand -- operand )
    mode @ +  dup $0f0 and $50 =
    if
        $0f and                  \ chng 5x to 0x
    then
  ;


\ ***  6809 assembler: pcrel, cofset  ******************************************

\ lay pc relative
: pcrel  ( operand postbyte -- )
    swap  here 2+ -  dup 8bit?      \ try 8 bit relative offset
    if
        swap $0fe and c, c,      \ it fits...lay postbyte,offset
    else
        1- swap c, w,           \ no good...use 16 bit relative
    then ;

\ test for indirect
: notindir?   ( postbyte -- fl )
    $10 and 0=
  ;

\ lay constant offset
: cofset   ( operand postbyte -- )
    over 0=  if
        $0f0 and 4 or c,-t drop     \ no offset
    else
        over 5bit? over notindir? and if
            $60 and swap $1f and or c,-t          \ 5 bit offset
        else
            over 8bit? if
                $0fe and c,-t c,-t    \ 8 bit offset
            else
                c,-t ,-t        \ 16 bit offset
            then
        then
    then
  ;


\ ***  6809 assembler: indexed, immed  *****************************************

\ lay extended indirect
: extind  ( operand postbyte -- )
    c,-t ,-t                    \ lay postbyte and operand
  ;

\ lay indexed poststuff
: indexed   ( operand? postbyte -- )
    dup $8f and                 \ check postbyte for modes w/ operands
    case
        $89 of cofset   endof   \ const.offset
        $8d of pcrel    endof   \ pc relative
        $8f of extind   endof   \ extended indir
	swap c,-t               \ simple modes, postbyte only
    endcase
  ;

: immed  (  operand opcode-pfa -- | lay immediate poststuff )
    2+ @  dup 0= 3 ?error       \ test immedsize
    1- if                       \ lay immed. operand in reqd.size
        ,-t
    else
        c,-t
    then
  ;


\ ***  6809 assembler: general addr instr  *************************************

\ Gen'l Addr
: genop  ( comp: immedsize opcode -- )
    create
        (,) (,)
    does>
        dup @ +mode opcode,             \ [see below] | lay opcode
        mode @
        case 0 of  immed         endof  \ immediate
            10 of  drop c,-t     endof  \ direct
            20 of  drop indexed  endof  \ indexed
            30 of  drop ,-t      endof  \ extended
        endcase
        reset
  ;

\ Indexed Only
: inxop  ( comp: opcode -- )
    create
        (,)
    does>
        mode @ 20 - 3 ?error
        @ opcode, indexed reset
  ;

\ Stack action of general addressing instructions
\ (1) immediate, direct, extended:                 operand --
\ (2) all indexed except (3):                     postbyte --
\ (3) const.offset, PCR, extended indir:  operand postbyte --


\ ***  6809 assembler: general addr instr  *************************************

1   $89 genop adca,     1   $c9 genop adcb,     1   $8b genop adda,
1   $cb genop addb,     2   $c3 genop addd,     1   $84 genop anda,
1   $c4 genop andb,     1   $85 genop bita,     1   $c5 genop bitb,
0   $48 genop asl,      0   $47 genop asr,      0   $4f genop clr,
1   $81 genop cmpa,     1   $c1 genop cmpb,     2 $1083 genop cmpd,
2 $118c genop cmps,     2 $1183 genop cmpu,     2   $8c genop cmpx,
2 $108c genop cmpy,     1   $88 genop eora,     1   $c8 genop eorb,
0   $43 genop com,      0   $4a genop dec,      0   $4c genop inc,
1   $86 genop lda,      1   $c6 genop ldb,      2   $cc genop ldd,
2 $10ce genop lds,      2   $ce genop ldu,      2   $8e genop ldx,
2 $108e genop ldy,      0   $4e genop jmp,      0   $8d genop jsr,
0   $48 genop lsl,      0   $44 genop lsr,      0   $40 genop neg,
1   $8a genop ora,      1   $ca genop orb,      0   $49 genop rol,
0   $46 genop ror,      1   $82 genop sbca,     1   $c2 genop sbcb,
0   $87 genop sta,      0   $c7 genop stb,      0   $cd genop std,
0 $10cf genop sts,      0   $cf genop stu,      0   $8f genop stx,
0 $108f genop sty,      1   $80 genop suba,     1   $c0 genop subb,
2   $83 genop subd,     0   $4d genop tst,

$32 inxop leas,     $33 inxop leau,
$30 inxop leax,     $31 inxop leay,


\ ***  6809 assembler: branches  ***********************************************

\ conditional branch
: condbr  ( comp: opcode -- | exec: -- )
    create
        (,)
    does>
        @  swap here 2+ -               \ addr --
        dup 8bit?
        if
            swap c,-t c,-t              \ 8 bit
        else
            $10 c,-t  swap c,-t 2-  (,)  \ @TODO: à vérifier
        then
        reset
  ;

\ : UNCBR   <BUILDS (,)       \ short:long -- | Uncondit'l Bran
\    DOES>   @  SWAP HERE 2+ -     \ addr --
\    DUP 8BIT? IF  SWAP >< C, C,        \ 8 bit: use short opcod
\    ELSE  SWAP C, 1- W,  THEN RESET ;  \ 16 bit: use long opcod


\ ***  6809 assembler: branch instructions  ************************************

$24 condbr bcc,     $25 condbr bcs,     $27 condbr beq,     $2c condbr bge,
$2e condbr bgt,     $22 condbr bhi,     $24 condbr bhs,     $2f condbr ble,
$25 condbr blo,     $23 condbr bls,     $2d condbr blt,     $2b condbr bmi,
$26 condbr bne,     $2a condbr bpl,     $21 condbr brn,     $28 condbr bvc,
$29 condbr bvs,   $2016 condbr bra,   $8d17 condbr bsr,


\ ***  6809 assembler: conditions  *********************************************

$24 constant CS     $25 constant CC     $27 constant NE     $2C constant LT
$2E constant LE     $22 constant LS     $24 constant LO     $2F constant GT
$25 constant HS     $23 constant HI     $2D constant GE     $2B constant PL
$26 constant EQ     $2A constant MI     $21 constant ALW    $28 constant VS
$29 constant VC     $20 constant NVR


\ ***  6809 assembler: structured cond'ls  *************************************

\ copile branching and reserve space
: IF,  ( br.opcode -- adr.next.instr 2 )
    c,-t  $00 c,-t
    here-t  2
  ;

\ patch the forward ref.
: ENDIF,  ( adr.instr.after.br 2 -- ) 
   2 ?pairs   
    here over -  dup 8bit? 0= 3 ?error  swap 1- c! 
  ;

\ : ELSE,   \ adr.after.br 2 -- adr.after.this.br 2
\    2 ?PAIRS   NVR C, 0 C, HERE SWAP  2 ENDIF, 2 ;
\ : BEGIN,  \ -- dest.adr 1
\    HERE 1 ;
\ : UNTIL,  \ dest.adr 1 br.opcode --
\    SWAP 1 ?PAIRS   C,  HERE 1+ -  DUP 8BIT? 0= 3 ?ERROR  C, ;
\ : WHILE,  \ dest.adr 1 br.opcod -- adr.after.this 2 dest.adr 1
\    IF, 2SWAP ;
\ : REPEAT, \ adr.after.while 2 dest.adr.of.begin 1 --
\    NVR UNTIL, ENDIF, ;
\ : THEN,  ENDIF, ;
\ : END,  UNTIL, ;



\ : ;C   CURRENT @ CONTEXT !   ?CSP   SMUDGE ;
\ : NEXT,   Y ,++ LDX,  X 0, [] JMP, ;
\ : NEXT    NEXT, ;

forth definitions

