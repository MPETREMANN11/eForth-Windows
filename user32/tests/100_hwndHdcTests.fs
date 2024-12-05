\ *********************************************************************
\ hwnd & hdc tests for windows
\    Filename:      100_windowsTests.fs
\    Date:          24 nov 2024
\    Updated:       24 nov 2024
\    File Version:  1.0
\    MCU:           eFORTH
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

.( 100_hwndHdcTests.fs loaded )

windows also graphics

\ Destroys the specified window @TODO: semble pas fonctionner
z" DestroyWindow"  1 User32 DestroyWindow   ( hwnd -- fl )



\ test for uEforth window
0 value hwnd        \ handle for current window
0 value hdc         \ handle for DC

: getHandles  ( -- )
    GetForegroundWindow to hwnd
    hwnd getDC to hdc
  ;


\ CreateMenu
z" CreateMenu"  0 User32 CreateMenu   ( -- hmenu )

\ CreatePopupMenu  
z" CreatePopupMenu"  0 User32 CreatePopupMenu   ( -- hmenu )


: run80  ( -- )
    hdc $3F $3F $FF RGB setTextColor drop
  ;


create LPRECT
    RECT allot

: lpchText1 s" This is a first string. This is second string. Here a third string. And finaly a lastest string. " ;
: lpchText2 s" Another string. " ;


: DRAWtext1  ( -- )
    LPRECT 10 10 300 80 SetRect drop
    hdc lpchText1 LPRECT DT_TOP DT_WORDBREAK OR DT_CENTER OR DrawTextA drop
    hdc lpchText2 LPRECT DT_TOP DT_WORDBREAK OR DT_CENTER OR DrawTextA drop
  ;


: STR01 ( -- addr len )
    s" This is my first example.. I try to draw a very long text in this graphic window." ;

: FORMATTING ( -- n )
    DT_TOP          \ draw frop top
    DT_WORDBREAK OR \ break words
    DT_CENTER    OR \ center text
  ;

: DRAWtext2  ( -- )
    LPRECT 280 10 380 120 SetRect drop
    hdc $CF $00 $00 RGB setTextColor drop  \ text in dark RED
    hdc STR01 LPRECT FORMATTING DrawTextA
  ;

0 value HPEN_RED
0 value HPEN_BLUE

: setPens  ( -- )
    PS_SOLID    3 $FF $00 $00 RGB CreatePen to HPEN_RED
    PS_DOT      1 $00 $00 $FF RGB CreatePen to HPEN_BLUE
  ;

: draw 
    setPens
    hdc HPEN_RED SelectObject drop
    hdc 20 20 NULL moveToEx drop
    hdc 90 20 LineTo drop
    hdc 90 50 LineTo drop
    hdc 20 90 LineTo drop
    hdc 20 20 LineTo drop

    hdc HPEN_BLUE SelectObject drop
    hdc 25 25 NULL moveToEx drop
    hdc 95 25 LineTo drop
    hdc 95 55 LineTo drop
    hdc 25 95 LineTo drop
    hdc 25 25 LineTo drop
;

: run100  ( -- )
    getHandles
    draw
  ;


