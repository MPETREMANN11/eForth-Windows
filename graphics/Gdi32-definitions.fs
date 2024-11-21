\ *********************************************************************
\ Graphics extensions for graphics vocabulary
\    Filename:      Gdi32-definitions.fs
\    Date:          09 mar 2023
\    Updated:       18 nov 2024
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
graphics definitions

\ Creates a logical pen that has the specified style, width, and color
z" CreatePen"       3 Gdi32 CreatePen ( iStyle cWidth color -- hPen )

\ ***  Pen Styles  **********************************
0 constant PS_SOLID
1 constant PS_DASH
2 constant PS_DOT
3 constant PS_DASHDOT
4 constant PS_DASHDOTDOT
5 constant PS_NULL
6 constant PS_INSIDEFRAME


\         7 constant PS_USERSTYLE
\         8 constant PS_ALTERNATE
\ $0000000F constant PS_STYLE_MASK

\ #define PS_ENDCAP_ROUND     0x00000000
\ #define PS_ENDCAP_SQUARE    0x00000100
\ #define PS_ENDCAP_FLAT      0x00000200
\ #define PS_ENDCAP_MASK      0x00000F00
\
\ #define PS_JOIN_ROUND       0x00000000
\ #define PS_JOIN_BEVEL       0x00001000
\ #define PS_JOIN_MITER       0x00002000
\ #define PS_JOIN_MASK        0x0000F000
\
\ #define PS_COSMETIC         0x00000000
\ #define PS_GEOMETRIC        0x00010000
\ #define PS_TYPE_MASK        0x000F0000


\ LineTo draws a line from the current position to,
\ but not including, the specified point.
z" LineTo"      3 Gdi32 LineTo ( hdc x y -- fl )

\ MoveToEx updates the current position
z" MoveToEx"    4 Gdi32 MoveToEx ( hdc x y LPPOINT -- fl )

\  @TODO: à tester rapidement
z" Rectangle"   5 gdi32 Rectangle   ( hdc left top right bottom -- )

\  @TODO: à tester rapidement
z" Ellipse"     5 gdi32 Ellipse     \ hdc left top right bottom

\ The CloseFigure function closes an open figure in a path.    @TODO: à tester rapidement
z" CloseFigure" 1 gdi32 CloseFigure ( hdc --  fl )

\ The GetPixel function retrieves the red, green, blue (RGB) color value
\ of the pixel at the specified coordinates.   @TODO: à tester rapidement
z" GetPixel"    3 gdi32 GetPixel ( hdc x y -- color )

\ selects an object in the specified device context (DC)
z" SelectObject"    2 Gdi32 SelectObject ( hdc h -- fl )

\ The SetPixel function sets the pixel at the specified coordinates
\ to the specified color.    @TODO: à tester rapidement
z" SetPixel"    4 gdi32 SetPixel ( hdc x y colorref -- colorref )


\ write text
z" TextOutA"    5 Gdi32 TextOutA  ( hdc x y lpString c -- fl )



\ Set background color
z" SetBkColor"  2 Gdi32 SetBkColor  ( hdc color -- fl )

\ Set text color
z" SetTextColor" 2 Gdi32 SetTextColor  ( hdc color -- fl )



\ HGDIOBJ GetCurrentObject(
\   [in] HDC  hdc,
\   [in] UINT type
\ );
\ link: https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-getcurrentobject
z" GetCurrentObject" 2 Gdi32 GetCurrentObject


\ example:
\ HFONT font = CreateFont(pixels_height, 0, // size
\                         0, 0, 0, // normal orientation
\                         FW_NORMAL,   // normal weight--e.g., bold would be FW_BOLD
\                         false, false, false, // not italic, underlined or strike out
\                         DEFAULT_CHARSET,
\                         OUT_OUTLINE_PRECIS, // select only outline (not bitmap) fonts
\                         CLIP_DEFAULT_PRECIS,
\                         CLEARTYPE_QUALITY,
\                         VARIABLE_PITCH | FF_SWISS,
\                         "Arial");

\ link: https://learn.microsoft.com/fr-fr/windows/win32/api/wingdi/nf-wingdi-createfonta
z" CreateFontA" 14 Gdi32 CreateFontA



\ ***  Alternate words in graphics voc.  ****************************************


: gdiError ( n -- )
    0= if ." ERROR" then
  ;

\ create LPPOINT
\     POINT allot


\ Example of alternate versions

\ : _moveTo ( x y -- )
\     hdc -rot LPPOINT Gdi.MoveToEx gdiError
\   ;

\ : _lineTo ( x y -- )
\     hdc -rot Gdi.LineTo gdiError
\   ;

\ : _rectangle ( left top right bottom -- )
\     >r >r >r >r hdc r> r> r> r> Gdi.Rectangle gdiError
\   ;

\ : _closeFigure ( -- )
\     hdc Gdi.CloseFigure gdiError
\   ;

\ : _getPixel ( x y -- colorref )
\     hdc -rot Gdi.GetPixel
\   ;

\ : _setPixel ( x y color -- )
\     hdc -rot Gdi.SetPixel
\   ;

only forth definitions




