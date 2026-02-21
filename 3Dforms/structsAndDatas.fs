\ *********************************************************************
\ Define structures and datas
\    Filename:      structsAndDatas.fs
\    Date:          16 feb 2026
\    Updated:       18 feb 2026
\    File Version:  1.0
\    Forth:         eFORTH Windows
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


DEFINED? L, invert [IF]
\ compile 32 bits value in dictionnary
: L,  ( u -- )
    dup c,
    8 rshift dup c,
    8 rshift dup c,
    8 rshift dup c,
    drop
  ;
[THEN]

DEFINED? W, invert [IF]
\ compile 16 bits value in dictionnary
: W,  ( u -- )
    dup c,
    8 rshift dup c,
    drop
  ;
[THEN]

0 value nbItems

also structures

\ *** Define 3D vertex structures **********************************************

\ compile 3D values x y z in dictionnary
: 3Dpoint, { x y z -- }
    x L,
    y L,
    z L,
    1 +to nbItems
  ;

\ define 3D point structure, fields are 32 bits format
struct 3Dpoint
    i32 field ->x
    i32 field ->y
    i32 field ->z

\ get x y z points from address
: 3Dpoint@  { addr -- x y z }
    addr @field ->x
    addr @field ->y
    addr @field ->z
  ;

: calcPointOffset ( addr n -- addr' )
    3Dpoint * +     \ calc. offset in datas array
  ;

\ get x y z points at address + offset n * 3Dpoint
: get3DPoint ( addr n -- x y z )
    calcPointOffset
    3dpoint@
  ;


\ store 3D points x y z in address
: 3Dpoint!    { x y z addr -- }
    x addr !field ->x
    y addr !field ->y
    z addr !field ->z
  ;

\ set x y z points at address + offset n * 3Dpoint
: set3DPoint ( x y z addr n -- )
    calcPointOffset
    3dpoint!
  ;


\ *** Define 3D edges structures ***********************************************

\ compile 3D edges po pe in dictionnary
: 3Dedge, { po pe -- }
    po W,
    pe W,
    1 +to nbItems
  ;

\ define 3D edge structure, fields are 16 bits format
struct 3Dedge
    u16 field ->po      \ po for Point origin
    u16 field ->pe      \ pe for Point extremity

\ get po pe edges from address
: 3Dedge@  { addr -- po pe }
    addr @field ->po
    addr @field ->pe
  ;

: calcEdgeOffset ( addr n -- addr' )
    3Dedge * +          \ calc. offset in datas array
  ;

\ get po pe points at address + offset n * 3Dedge
: get3Dedge ( addr n -- po pe )
    calcEdgeOffset
    3Dedge@
  ;

\ store 3D edges po pe in address
: 3Dedge!    { po pe addr -- }
    po addr !field ->po
    pe addr !field ->pe
  ;

\ set po pe points at address + offset n * 3Dedge
: set3Dedge ( po pe addr n -- )
    calcEdgeOffset
    3Dedge!
  ;


only FORTH






