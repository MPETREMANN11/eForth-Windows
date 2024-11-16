\ *********************************************************************
\ Graphics extensions for windows
\    Filename:      graphicsExtensions.fs
\    Date:          09 mar 2023
\    Updated:       10 mar 2023
\    File Version:  1.0
\    MCU:           eFORTH
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


\ online doc: https://learn.microsoft.com/fr-fr/windows/win32/api/_gdi/


\ ***  Graphics primitives in graphics internals voc.  *************************

only forth 
windows also 
graphics internals definitions

\ MoveToEx updates the current position
z" MoveToEx"      4 Gdi32 Gdi.MoveToEx ( hdc x y LPPOINT -- fl )


\ LineTo draws a line from the current position to,
\ but not including, the specified point.
z" LineTo"      3 Gdi32 Gdi.LineTo ( hdc x y -- fl )

\  @TODO: à tester rapidement
z" Rectangle"   5 gdi32 Gdi.Rectangle   ( hdc left top right bottom -- )

\  @TODO: à tester rapidement
z" Ellipse"     5 gdi32 Gdi.Ellipse     \ hdc left top right bottom

\ The CloseFigure function closes an open figure in a path.    @TODO: à tester rapidement
z" CloseFigure" 1 gdi32 Gdi.CloseFigure ( hdc --  fl )

\ The GetPixel function retrieves the red, green, blue (RGB) color value 
\ of the pixel at the specified coordinates.   @TODO: à tester rapidement
z" GetPixel"    3 gdi32 Gdi.GetPixel ( hdc x y -- color )

\ The SetPixel function sets the pixel at the specified coordinates 
\ to the specified color.    @TODO: à tester rapidement
z" SetPixel"    4 gdi32 Gdi.SetPixel ( hdc x y colorref -- colorref )

\ write text   
z" TextOutA"            5 Gdi32 TextOutA


\ ***  Graphics words in graphics voc.  ****************************************

only forth 
windows also 
graphics internals also
graphics definitions



: gdiError ( n -- )
    0= if ." ERROR" then
  ;

create LPPOINT
    POINT allot

: moveTo ( x y -- )
    hdc -rot LPPOINT Gdi.MoveToEx gdiError
  ;

: lineTo ( x y -- )
    hdc -rot Gdi.LineTo gdiError
  ;

: rectangle ( left top right bottom -- )
    >r >r >r >r hdc r> r> r> r> Gdi.Rectangle gdiError
  ;

: closeFigure ( -- )
    hdc Gdi.CloseFigure gdiError
  ;

: getPixel ( x y -- colorref )
    hdc -rot Gdi.GetPixel
  ;

: setPixel ( x y color -- )
    hdc -rot Gdi.SetPixel
  ;

only forth definitions 




