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
0 value HBRUSH_LIGHTGREY


$df $df $df RGB constant COLOR_LIGHT_GREY

: setBrushes  ( -- )
    $00 $00 $FF RGB CreateSolidBrush to HBRUSH_BLUE
    $FF $00 $00 RGB CreateSolidBrush to HBRUSH_RED
    COLOR_LIGHT_GREY CreateSolidBrush to HBRUSH_LIGHTGREY
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

\ : drawRects
\     hdc RECT01 GetRect Rectangle drop
\     hdc HPEN_RED    SelectObject drop
\     hdc HBRUSH_BLUE SelectObject drop
\     hdc RECT02 GetRect Rectangle drop
\   ;

\ BOOL OffsetRect(
\   [in, out] LPRECT lprc,
\   [in]      int    dx,
\   [in]      int    dy
\ );

\ move RECT structure
\ z" OffsetRect"  3 User32 OffsetRect   ( lprc dx dy -- fl )

: STR01 ( -- addr len )
    s" In this example, I draw a very long text in this graphic window." ;

: FORMATTING ( -- n )
    DT_TOP          \ draw frop top
    DT_WORDBREAK OR \ break words
    DT_CENTER    OR \ center text
  ;

\ set background color
\ z" SetBkColor"  2 User32 SetBkColor   ( hdc color -- fl )

\ Test if a POINT is in a RECTangle
z" PtInRect"  2 User32 PtInRect   ( lprc  pt -- fl )

 

: DRAWtext  ( -- )
    RECT01 10 10 OffsetRect drop
    hdc $CF $00 $00 RGB setTextColor drop   \ text in dark RED
    hdc COLOR_LIGHT_GREY SetBkColor drop    \ text in dark RED
    hdc RECT01 HBRUSH_LIGHTGREY FillRect drop
    hdc STR01 RECT01 FORMATTING DrawTextA drop
  ;


: run101  ( -- )
    400 300 window
\     setPens
    setBrushes
    DRAWtext
    begin
        poll
        event IDLE = if 

        then
        event FINISHED = 
    until
  ;

\ ==============================================================================

\ Test if a POINT is in a RECTangle
\ z" PtInRect"  2 User32 PtInRect   ( lprc pt -- fl )
\ 
\ create RECT01
\      10 L,  10 L, 220 L,  80 L,
\ 
\ create POINT01
\      25 L,  25 L,
\ 
\ RECT01 POINT01 PtInRect .
\ 
\ RECT01 2 3 OffsetRect 






