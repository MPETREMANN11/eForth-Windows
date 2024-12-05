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


\ BOOL RedrawWindow(
\   [in] HWND       hWnd,
\   [in] const RECT *lprcUpdate,
\   [in] HRGN       hrgnUpdate,
\   [in] UINT       flags
\ );

\ @TODO semble pas fonctionner dans fenêtre eForth
z" RedrawWindow"  4 User32 RedrawWindow   ( hwnd lprcUpdate hrgnUpdate flags -- fl )


\ test for uEforth window
\ 0 value hwnd        \ handle for current window
\ 0 value hdc         \ handle for DC

: getHandles  ( -- )
    GetForegroundWindow to hwnd
    hwnd getDC to hdc
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

0 value HBRUSH_BLUE
0 value HBRUSH_RED

: setBrushes  ( -- )
    $00 $00 $FF RGB CreateSolidBrush to HBRUSH_BLUE
    $FF $00 $00 RGB CreateSolidBrush to HBRUSH_RED
  ;


\ z" FillRect"  3 User32 FillRect   ( hdc lprc hbr -- fl )

\ ** RedrawWindow flags

\ $0001 constant RDW_INVALIDATE          
\ $0002 constant RDW_INTERNALPAINT       
\ $0004 constant RDW_ERASE               
\ 
\ $0008 constant RDW_VALIDATE            
\ $0010 constant RDW_NOINTERNALPAINT     
\ $0020 constant RDW_NOERASE             
\ 
\ $0040 constant RDW_NOCHILDREN         
\ $0080 constant RDW_ALLCHILDREN         
\ 
\ $0100 constant RDW_UPDATENOW           
\ $0200 constant RDW_ERASENOW            
\ 
\ $0400 constant RDW_FRAME               
\ $0800 constant RDW_NOFRAME             


\ void EffacerFenetre(HWND hWnd) {
\     RedrawWindow(hWnd, NULL, NULL, RDW_INVALIDATE | RDW_ERASE);
\ }


\ : clearWindow ( -- )
\     hwnd NULL NULL RDW_INVALIDATE RDW_ERASE or RedrawWindow drop
\   ;


: setPens  ( -- )
    PS_SOLID    1 $FF $00 $00 RGB CreatePen to HPEN_RED
    PS_SOLID    1 $00 $00 $FF RGB CreatePen to HPEN_BLUE
  ;



create RECT01
     10 L,  10 L, 220 L,  80 L,

create RECT02
     25 L,  25 L, 235 L,  95 L,

: drawRects
\     setPens
\     setBrushes
    hdc RECT01 GetRect Rectangle drop
    hdc HPEN_RED    SelectObject drop
    hdc HBRUSH_BLUE SelectObject drop
    hdc RECT02 GetRect Rectangle drop
  ;


: testRects ( -- )
\     getHandles
\     setBrushes
\     400 300 window
\     setPens
\     setBrushes
    LPRECT 10 10 300 80 SetRect drop
    hdc LPRECT HBRUSH_BLUE FillRect  drop
    hdc LPRECT HBRUSH_RED  FrameRect drop
  ;

: run101  ( -- )
    400 300 window
    setPens
    setBrushes
    drawRects
    begin
        poll
        event IDLE = if 
            drawRects
        then
        event FINISHED = 
    until
  ;


