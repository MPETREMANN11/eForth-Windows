\ *********************************************************************
\ mapping tests for windows
\    Filename:      13_mappingTests.fs
\    Date:          22 nov 2024
\    Updated:       22 nov 2024
\    File Version:  1.0
\    MCU:           eFORTH
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


.( 13_mappingTests loaded )

windows also
graphics


\ * Mapping Modes
1 constant MM_TEXT             
2 constant MM_LOMETRIC         
3 constant MM_HIMETRIC         
4 constant MM_LOENGLISH        
5 constant MM_HIENGLISH        
6 constant MM_TWIPS            
7 constant MM_ISOTROPIC        
8 constant MM_ANISOTROPIC      

\ * Min and Max Mapping Mode values 
MM_TEXT         constant MM_MIN       
MM_ANISOTROPIC  constant MM_MAX             
MM_TWIPS        constant MM_MAX_FIXEDSCALE  

\ * Coordinate Modes
1 constant ABSOLUTE           
2 constant RELATIVE        


\ @TODO à tester rapidement
z" SaveDC"      1 Gdi32 SaveDC  ( hdc -- fl )

\ @TODO à tester rapidement
z" RestoreDC"   2 Gdi32 RestoreDC  ( hdc -- nSavedDC )

\ @TODO à tester rapidement
z" SetMapMode"  2 Gdi32 SetMapMode  ( hdc iMode -- precMapMode|0 )

\ ClientToScreen and ScreenToClient


\ @TODO à tester rapidement
z" SetWindowOrgEx"  4 Gdi32 SetWindowOrgEx  ( hdc x y lppt -- fl )





graphics internals


\ define window dimension
600 constant WINDOW_WIDTH
400 constant WINDOW_HEIGHT

: lpString s" This is a test string..."  ;

: centerMap  ( -- )
    hdc WINDOW_WIDTH 2/  WINDOW_HEIGHT 2/ NULL SetViewportOrgEx 0=
    if
        abort" ERROR: SetViewportOrgEx"
    then
  ;

: mathematicMap ( -- )
    hdc 0  WINDOW_HEIGHT NULL SetViewportOrgEx 0=
    if
        abort" ERROR: SetViewportOrgEx"
    then
  ;


: TXTout  ( -- )
    hdc $3F $3F $FF RGB setTextColor drop  \ text in BLUE
    hdc 10 10 lpString TextOutA  drop
\     SaveDC
    centerMap
    hdc $CF $00 $00 RGB setTextColor drop  \ text in dark RED
    hdc 0 0 lpString TextOutA  drop
\     RestoreDC
    hdc $000000 setTextColor drop  \ text in black
    hdc 10 30 lpString TextOutA  drop
;

: LINESout ( -- )
    hdc 20 20 NULL moveToEx drop
    hdc 90 20 LineTo drop
    hdc 90 50 LineTo drop
    hdc 20 90 LineTo drop
    hdc 20 20 LineTo drop
  ;



: run13
   WINDOW_WIDTH WINDOW_HEIGHT window
    TXTout
    LINESout
    begin
        poll
        IDLE event = if 
             
        then
        event FINISHED = 
    until
  ;




