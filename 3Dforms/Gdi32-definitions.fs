\ *********************************************************************
\ Graphics extensions for graphics vocabulary
\    Filename:      Gdi32-definitions.fs
\    Date:          09 mar 2023
\    Updated:       01 dec 2024
\    File Version:  1.0
\    MCU:           eFORTH
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


\ online doc: https://learn.microsoft.com/fr-fr/windows/win32/api/_gdi/


\ ***  Graphics primitives in graphics internals voc.  *************************

only forth
also windows 
also structures
also graphics definitions

\ Creates a logical pen that has the specified style, width, and color
z" CreatePen"       3 Gdi32 CreatePen ( iStyle cWidth color -- hPen )


\ ***  Font wieght  ************************************************************

  0 constant FW_DONTCARE
100 constant FW_THIN
200 constant FW_EXTRALIGHT
300 constant FW_LIGHT
400 constant FW_NORMAL
500 constant FW_MEDIUM
600 constant FW_SEMIBOLD
700 constant FW_BOLD
800 constant FW_EXTRABOLD
800 constant FW_HEAVY
FW_EXTRALIGHT constant FW_ULTRALIGHT
FW_NORMAL     constant FW_REGULAR
FW_SEMIBOLD   constant FW_DEMIBOLD
FW_EXTRABOLD  constant FW_ULTRABOLD
FW_HEAVY      constant FW_BLACK


\ ***  Palette structures and constants  ***************************************

struct PALETTEENTRY
   i8 field ->peRed
   i8 field ->peGreen
   i8 field ->peBlue
   i8 field ->peFlags

struct LOGPALETTE
  i32 field ->palVersion
  i32 field ->palNumEntries
  PALETTEENTRY field ->palPalEntry


\ ***  Pen Styles constants  ***************************************************
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

\ $00000000 constant PS_ENDCAP_ROUND
\ $00000100 constant PS_ENDCAP_SQUARE
\ $00000200 constant PS_ENDCAP_FLAT
\ $00000F00 constant PS_ENDCAP_MASK
\
\ $00000000 constant PS_JOIN_ROUND
\ $00001000 constant PS_JOIN_BEVEL
\ $00002000 constant PS_JOIN_MITER
\ $0000F000 constant PS_JOIN_MASK
\
\ $00000000 constant PS_COSMETIC
\ $00010000 constant PS_GEOMETRIC
\ $000F0000 constant PS_TYPE_MASK


\ ***  Brushes  ***

\ creates a logical brush that has the specified solid color.
z" CreateSolidBrush"  1 Gdi32 CreateSolidBrush ( color -- HBRUSH )


\ ***  Font and text  ***

\ Set text color
z" SetTextColor" 2 Gdi32 SetTextColor  ( hdc color -- fl )

\ write text
z" TextOutA"    5 Gdi32 TextOutA  ( hdc x y lpString c -- fl )


\ ***  Lines and Curves  ***

\ Draw arc
z" Arc"     9 gdi32 Arc     ( hdc x1 y1 x2 y2 x3 y3 x4 y4 -- fl )

\ LineTo draws a line from the current position to,
\ but not including, the specified point.
z" LineTo"      3 Gdi32 LineTo ( hdc x y -- fl )

\ MoveToEx updates the current position
z" MoveToEx"    4 Gdi32 MoveToEx ( hdc x y LPPOINT -- fl )


\ **  Filled Shapes  **

\ Draw ellipse
z" Ellipse"     5 gdi32 Ellipse     ( hdc left top right bottom -- fl )

\ draw a polygone
z" Polygon"   3 Gdi32 Polygon ( hdc *apt cpt -- fl )

\ draw a rectangle
z" Rectangle"   5 gdi32 Rectangle   ( hdc left top right bottom -- )


\ **  Maping and coordinates  **

\ Specifies which window point maps to the viewport origin (0,0)
z" SetViewportOrgEx"  4 Gdi32 SetViewportOrgEx  ( hdc x y lppt -- fl )





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


\ Set background color
z" SetBkColor"  2 Gdi32 SetBkColor  ( hdc color -- fl )






\ HGDIOBJ GetCurrentObject(
\   [in] HDC  hdc,
\   [in] UINT type
\ );
\ link: https://learn.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-getcurrentobject
z" GetCurrentObject" 2 Gdi32 GetCurrentObject



\ Creates a logical font with the specified characteristics
z" CreateFontA" 14 Gdi32 CreateFontA





only forth definitions

