\ *********************************************************************
\ rectangles tests for windows
\    Filename:      101_rectanglesTests.fs
\    Date:          24 nov 2024
\    Updated:       26 nov 2024
\    File Version:  1.0
\    MCU:           eFORTH
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

.( 101_rectanglesTests.fs loaded )

windows also graphics also internals

\ Destroys the specified window @TODO: semble pas fonctionner
z" DestroyWindow"  1 User32 DestroyWindow   ( hwnd -- fl )

\ int FillRect(
\   [in] HDC        hDC,
\   [in] const RECT *lprc,
\   [in] HBRUSH     hbr
\ );

\ fill a rectangle
z" FillRect"  3 User32 FillRect   ( hdc lprc hbr -- fl )


\ int FrameRect(
\   [in] HDC        hDC,
\   [in] const RECT *lprc,
\   [in] HBRUSH     hbr
\ );
z" FrameRect"  3 User32 FrameRect   ( hdc lprc hbr -- fl )

\ BOOL MoveWindow(
\   [in] HWND hWnd,
\   [in] int  X,
\   [in] int  Y,
\   [in] int  nWidth,
\   [in] int  nHeight,
\   [in] BOOL bRepaint
\ );

\ Modifie la position et les dimensions de la fenêtre spécifiée.
z" MoveWindow"  6 User32 MoveWindow   ( hwnd x y nWidth nHeight bRepaint -- fl )



create LPRECT
    RECT allot

0 value HBRUSH_BLUE
0 value HBRUSH_RED

: setBrushes  ( -- )
    $00 $00 $FF RGB CreateSolidBrush to HBRUSH_BLUE
    $FF $00 $00 RGB CreateSolidBrush to HBRUSH_RED
  ;

0 value HPEN_RED
0 value HPEN_BLUE

: setPens  ( -- )
    PS_SOLID    1 $FF $00 $00 RGB CreatePen to HPEN_RED
    PS_SOLID    1 $00 $00 $FF RGB CreatePen to HPEN_BLUE
  ;

create RECT01
     10 L,  10 L, 220 L,  80 L,

create RECT02
     25 L,  25 L, 235 L,  95 L,

: drawRects
    hdc RECT01 GetRect Rectangle drop
    hdc HPEN_RED    SelectObject drop
    hdc HBRUSH_BLUE SelectObject drop
    hdc RECT02 GetRect Rectangle drop
  ;

: run101  ( -- )
    400 300 window
    setPens
    setBrushes
    drawRects
    begin
        poll
        event IDLE = if 
\             drawRects
        then
        event FINISHED = 
    until
  ;


