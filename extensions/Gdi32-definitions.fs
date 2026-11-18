\ *********************************************************************
\ Graphics extensions for graphics vocabulary
\    Filename:      Gdi32-definitions.fs
\    Date:          09 mar 2023
\    Updated:       18 nov 2026
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


\ ***  Font weight  ************************************************************

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

\ ***  for SetBkMode  **********************************************************
1 constant TRANSPARENT
2 constant OPAQUE

\ ***   for GetStockObject  ****************************************************
0 constant WHITE_BRUSH    1 constant LTGRAY_BRUSH   2 constant GRAY_BRUSH
3 constant DKGRAY_BRUSH   4 constant BLACK_BRUSH    5 constant NULL_BRUSH
NULL_BRUSH constant HOLLOW_BRUSH
6 constant WHITE_PEN      7 constant BLACK_PEN      8 constant NULL_PEN
10 constant OEM_FIXED_FONT  11 constant ANSI_FIXED_FONT  12 constant ANSI_VAR_FONT
13 constant SYSTEM_FONT   14 constant DEVICE_DEFAULT_FONT
17 constant DEFAULT_GUI_FONT  18 constant DC_BRUSH  19 constant DC_PEN


\ ***  SetTextAlign  ***********************************************************
0 constant TA_LEFT   2 constant TA_RIGHT   6 constant TA_CENTER
0 constant TA_TOP    8 constant TA_BOTTOM  24 constant TA_BASELINE
1 constant TA_UPDATECP

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



\ ***  General definitions ***

\ Releases a GDI object (pen, brush, font, bitmap, etc.). ESSENTIAL: prevents GDI leaks.
z" DeleteObject" 1 Gdi32 DeleteObject ( hObj -- fl )


\ ***  Palettes  ***

\ @TODO: à tester
z" CreatePalette"  1 Gdi32 CreatePalette  ( *LOGPALETTE -- hPal )

\ @TODO: à tester
z" SelectPalette"  3 Gdi32 SelectPalette  ( hdc hPal bForceBkgd -- hOldPal )

\ @TODO: à tester
z" RealizePalette" 1 Gdi32 RealizePalette ( hdc -- n )


\ ***  Brushes  ***

\ creates a logical brush that has the specified solid color.
z" CreateSolidBrush"  1 Gdi32 CreateSolidBrush ( color -- HBRUSH )

\ @TODO: à tester
z" CreateHatchBrush"   2 Gdi32 CreateHatchBrush   ( style color -- hBrush )

\ @TODO: à tester
z" CreatePatternBrush" 1 Gdi32 CreatePatternBrush ( hBitmap -- hBrush )

\ @TODO: à tester
z" CreateBrushIndirect" 1 Gdi32 CreateBrushIndirect ( *LOGBRUSH -- hBrush )

\ @TODO: à tester
z" ExtCreatePen"       5 Gdi32 ExtCreatePen      ( style width *LOGBRUSH cStyle *pStyle -- hPen )

\ @TODO: à tester
z" CreatePenIndirect"  1 Gdi32 CreatePenIndirect ( *LOGPEN -- hPen )



\ ***  Font and text  ***

\ Set text color
z" SetTextColor" 2 Gdi32 SetTextColor  ( hdc color -- fl )

\ write text
z" TextOutA"    5 Gdi32 TextOutA  ( hdc x y lpString c -- fl )

\ @TODO: à tester
z" GetTextColor"        1 Gdi32 GetTextColor        ( hdc -- color )

\ @TODO: à tester
z" GetBkColor"          1 Gdi32 GetBkColor          ( hdc -- color )

\ @TODO: à tester
z" SetTextAlign"        2 Gdi32 SetTextAlign        ( hdc mode -- old )

\ @TODO: à tester
z" GetTextExtentPoint32A" 4 Gdi32 GetTextExtentPoint32A ( hdc lpStr c *SIZE -- fl )

\ @TODO: à tester
z" ExtTextOutA"         8 Gdi32 ExtTextOutA ( hdc x y opts *rect lpStr c *dx -- fl )

\ @TODO: à tester
z" CreateFontIndirectA" 1 Gdi32 CreateFontIndirectA ( *LOGFONT -- hFont )

\ @TODO: à tester
z" GetTextMetricsA"     2 Gdi32 GetTextMetricsA     ( hdc *TEXTMETRIC -- fl )

\ @TODO: à tester
z" TextOutW"            5 Gdi32 TextOutW            ( hdc x y lpWStr c -- fl )







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

\ @TODO: à tester
z" Pie"       9 Gdi32 Pie       ( hdc l t r b xr1 yr1 xr2 yr2 -- fl )

\ @TODO: à tester
z" Chord"     9 Gdi32 Chord     ( hdc l t r b xr1 yr1 xr2 yr2 -- fl )

\ @TODO: à tester
z" ArcTo"     9 Gdi32 ArcTo     ( hdc l t r b xr1 yr1 xr2 yr2 -- fl )

\ @TODO: à tester
z" RoundRect" 7 Gdi32 RoundRect ( hdc l t r b w h -- fl )

\ @TODO: à tester
z" Polyline"  3 Gdi32 Polyline  ( hdc *apt cpt -- fl )

\ @TODO: à tester
z" PolylineTo" 3 Gdi32 PolylineTo ( hdc *apt cpt -- fl )

\ @TODO: à tester
z" PolyBezier" 3 Gdi32 PolyBezier ( hdc *apt cpt -- fl )

\ @TODO: à tester
z" PolyBezierTo" 3 Gdi32 PolyBezierTo ( hdc *apt cpt -- fl )

\ @TODO: à tester
z" ExtFloodFill" 5 Gdi32 ExtFloodFill ( hdc x y color type -- fl )

\ @TODO: à tester
z" FloodFill" 4 Gdi32 FloodFill ( hdc x y color -- fl )

\ **  Maping and coordinates  **

\ Specifies which window point maps to the viewport origin (0,0)
z" SetViewportOrgEx"  4 Gdi32 SetViewportOrgEx  ( hdc x y lppt -- fl )





\ The CloseFigure function closes an open figure in a path.    @TODO: à tester rapidement
z" CloseFigure" 1 gdi32 CloseFigure ( hdc --  fl )

\ @TODO: à tester
z" BeginPath"         1 Gdi32 BeginPath         ( hdc -- fl )

\ @TODO: à tester
z" EndPath"           1 Gdi32 EndPath           ( hdc -- fl )

\ @TODO: à tester
z" FillPath"          1 Gdi32 FillPath          ( hdc -- fl )

\ @TODO: à tester
z" StrokePath"        1 Gdi32 StrokePath        ( hdc -- fl )

\ @TODO: à tester
z" StrokeAndFillPath" 1 Gdi32 StrokeAndFillPath ( hdc -- fl )

\ @TODO: à tester
z" AbortPath"         1 Gdi32 AbortPath         ( hdc -- fl )



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

<EOF>

\ @TODO: à tester et à rajouter:


\ Objets prédéfinis (pinceau blanc, NULL_BRUSH, DEFAULT_GUI_FONT...)
z" GetStockObject" 1 Gdi32 GetStockObject ( index -- hObj )

\ Mode de fond du texte (TRANSPARENT / OPAQUE)
z" SetBkMode" 2 Gdi32 SetBkMode ( hdc mode -- oldmode )

\ Version rapide de SetPixel
z" SetPixelV" 4 Gdi32 SetPixelV ( hdc x y color -- fl )


Contexte de dessin (DC) et bitmaps
forth
z" CreateCompatibleDC"     1 Gdi32 CreateCompatibleDC     ( hdc -- hdcMem )
z" CreateCompatibleBitmap" 3 Gdi32 CreateCompatibleBitmap ( hdc w h -- hBitmap )
z" DeleteDC"               1 Gdi32 DeleteDC               ( hdc -- fl )
z" SaveDC"                 1 Gdi32 SaveDC                 ( hdc -- n )
z" RestoreDC"              2 Gdi32 RestoreDC              ( hdc n -- fl )
z" BitBlt"                 9 Gdi32 BitBlt  ( hdcDest x y w h hdcSrc xs ys rop -- fl )
z" StretchBlt"            11 Gdi32 StretchBlt ( hdcD xD yD wD hD hdcS xS yS wS hS rop -- fl )
z" PatBlt"                 6 Gdi32 PatBlt ( hdc x y w h rop -- fl )
z" GetDeviceCaps"          2 Gdi32 GetDeviceCaps          ( hdc index -- n )
z" GdiFlush"               0 Gdi32 GdiFlush               ( -- fl )

Modes de dessin, coordonnées et régions
forth
z" SetROP2"          2 Gdi32 SetROP2          ( hdc mode -- old )
z" SetPolyFillMode"  2 Gdi32 SetPolyFillMode  ( hdc mode -- old )
z" SetArcDirection"  2 Gdi32 SetArcDirection  ( hdc dir -- old )
z" SetMapMode"       2 Gdi32 SetMapMode       ( hdc mode -- old )
z" SetWindowOrgEx"   4 Gdi32 SetWindowOrgEx   ( hdc x y lppt -- fl )
z" SetWindowExtEx"   4 Gdi32 SetWindowExtEx   ( hdc cx cy lpsize -- fl )
z" SetViewportExtEx" 4 Gdi32 SetViewportExtEx ( hdc cx cy lpsize -- fl )
z" GetCurrentPositionEx" 2 Gdi32 GetCurrentPositionEx ( hdc lppt -- fl )
z" CreateRectRgn"    4 Gdi32 CreateRectRgn    ( l t r b -- hRgn )
z" CreateEllipticRgn" 4 Gdi32 CreateEllipticRgn ( l t r b -- hRgn )
z" SelectClipRgn"    2 Gdi32 SelectClipRgn    ( hdc hRgn -- type )
z" FillRgn"          3 Gdi32 FillRgn          ( hdc hRgn hBrush -- fl )
z" FrameRgn"         5 Gdi32 FrameRgn         ( hdc hRgn hBrush w h -- fl )






\ Hachures (CreateHatchBrush)
0 constant HS_HORIZONTAL  1 constant HS_VERTICAL   2 constant HS_FDIAGONAL
3 constant HS_BDIAGONAL   4 constant HS_CROSS      5 constant HS_DIAGCROSS

\ SetPolyFillMode
1 constant ALTERNATE   2 constant WINDING

\ GetCurrentObject
1 constant OBJ_PEN   2 constant OBJ_BRUSH   5 constant OBJ_PAL
6 constant OBJ_FONT  7 constant OBJ_BITMAP

\ BitBlt
$00CC0020 constant SRCCOPY    $00EE0086 constant SRCPAINT
$008800C6 constant SRCAND     $00660046 constant SRCINVERT
$00000042 constant BLACKNESS  $00FF0062 constant WHITENESS


\ SetMapMode
1 constant MM_TEXT   2 constant MM_LOMETRIC   3 constant MM_HIMETRIC
8 constant MM_ISOTROPIC   9 constant MM_ANISOTROPIC

Petites remarques sur l’existant
SelectObject : le commentaire indique -- fl, mais la fonction renvoie le handle de l’objet précédent, à conserver pour le restaurer. Le commentaire exact serait ( hdc h -- hOld ).
GetCurrentObject et CreateFontA n’ont pas de commentaire de pile. Pour GetCurrentObject, ce serait ( hdc type -- hObj ). Pour CreateFontA, ( h w esc orient weight italic underline strike charset outprec clipprec quality pitchfam lpszFace -- hFont ).
Les constantes PS_USERSTYLE, PS_ENDCAP_* et PS_JOIN_* sont commentées. Elles ne servent qu’avec ExtCreatePen, donc elles deviendraient utiles si vous ajoutez cette fonction.

Hors gdi32.dll
FillRect, FrameRect, InvalidateRect, DrawTextA, GetDC et ReleaseDC se trouvent dans user32.
TransparentBlt, AlphaBlend et GradientFill se trouvent dans msimg32.

Voici un exemple de double buffering. On dessine dans un bitmap en mémoire, puis on le copie d’un coup à l’écran avec BitBlt. Il suppose que vous avez déjà ajouté à Gdi32-definitions.fs les définitions CreateCompatibleDC, CreateCompatibleBitmap, DeleteDC, DeleteObject, BitBlt, PatBlt, ainsi que les constantes SRCCOPY et WHITENESS.

forth
only forth
also windows
also graphics

640 constant BUF-W
480 constant BUF-H

variable hdcMem
variable hBmp
variable hOldBmp
variable hPen
variable hBrush
variable hOldPen
variable hOldBrush

\ Crée le DC mémoire et son bitmap (à appeler une seule fois)
: buf-create ( hdc -- )
  dup CreateCompatibleDC hdcMem !
  BUF-W BUF-H CreateCompatibleBitmap hBmp !
  hdcMem @ hBmp @ SelectObject hOldBmp ! ;

\ Dessine dans le buffer (rien n'apparaît encore à l'écran)
: buf-draw ( -- )
  hdcMem @ 0 0 BUF-W BUF-H WHITENESS PatBlt drop    \ fond blanc
  PS_SOLID 3 $0000FF CreatePen hPen !                \ stylo rouge ($00BBGGRR)
  $00FF00 CreateSolidBrush hBrush !                  \ pinceau vert
  hdcMem @ hPen @   SelectObject hOldPen !
  hdcMem @ hBrush @ SelectObject hOldBrush !
  hdcMem @  50  50 0 MoveToEx drop
  hdcMem @ 300 200 LineTo drop
  hdcMem @ 100 100 250 180 Ellipse drop
  hdcMem @ 320  60 450 160 Rectangle drop
  \ restaurer les anciens objets, puis détruire les nôtres
  hdcMem @ hOldPen @   SelectObject drop
  hdcMem @ hOldBrush @ SelectObject drop
  hPen @   DeleteObject drop
  hBrush @ DeleteObject drop ;

\ Copie le buffer vers la fenêtre
: buf-show ( hdc -- )
  0 0 BUF-W BUF-H hdcMem @ 0 0 SRCCOPY BitBlt drop ;

\ Libère tout (à la fermeture)
: buf-free ( -- )
  hdcMem @ hOldBmp @ SelectObject drop
  hBmp @ DeleteObject drop
  hdcMem @ DeleteDC drop ;

Pour l’utiliser avec le hdc de votre fenêtre :

forth
\ au démarrage :   hdc buf-create
\ à chaque rafraîchissement :   buf-draw   hdc buf-show
\ à la fermeture :   buf-free

Quelques points à retenir :

Les couleurs sont au format $00BBGGRR, donc $0000FF donne du rouge, et non du bleu comme avec le RGB habituel.
Il faut toujours remettre l’ancien objet avec SelectObject avant de faire DeleteObject. Windows refuse de détruire un objet encore sélectionné dans un DC.
Le hdc vient de votre code de fenêtre (GetDC ou BeginPaint, qui sont dans user32). Je ne sais pas comment votre extension l’obtient, donc adaptez cette partie.
Je n’ai pas pu tester ce code sur eForth Windows.












