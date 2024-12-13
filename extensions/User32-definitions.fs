\ *********************************************************************
\ User32 definitions for windows vocabulary
\    Filename:      User32-definitions.fs
\    Date:          16 nov 2024
\    Updated:       01 dec 2024
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************



only forth 
windows also
windows definitions
structures

\ get handle for current window
z" GetForegroundWindow"  0 User32 GetForegroundWindow   ( -- hwnd )

\ Minimizes (but does not destroy) the specified window
z" CloseWindow"  1 User32 CloseWindow   ( hwnd -- fl )



\ write text in RECT structure
z" DrawTextA"   5 User32 DrawTextA  ( hdc lpchText cchText lprc format -- fl )

\ DrawText format flags
$00000000 constant DT_TOP
$00000000 constant DT_LEFT                  \ Aligns text to the left
$00000001 constant DT_CENTER                \ Centers text horizontally in the rectangle
$00000002 constant DT_RIGHT                 \ Aligns text to the right
$00000004 constant DT_VCENTER               \ Centers text vertically
$00000008 constant DT_BOTTOM                \ Justifies the text to the bottom of the rectangle
$00000010 constant DT_WORDBREAK             \ Breaks words
$00000020 constant DT_SINGLELINE
$00000040 constant DT_EXPANDTABS
$00000080 constant DT_TABSTOP
$00000100 constant DT_NOCLIP
$00000200 constant DT_EXTERNALLEADING
$00000400 constant DT_CALCRECT
$00000800 constant DT_NOPREFIX
$00001000 constant DT_INTERNAL
$00002000 constant DT_EDITCONTROL
$00004000 constant DT_PATH_ELLIPSIS 
$00008000 constant DT_END_ELLIPSIS
$00010000 constant DT_MODIFYSTRING
$00020000 constant DT_RTLREADING
$00040000 constant DT_WORD_ELLIPSIS 
$00080000 constant DT_NOFULLWIDTHCHARBREAK
$00100000 constant DT_HIDEPREFIX
$00200000 constant DT_PREFIXONLY


\           @TODO: à tester rapidement
\ https://learn.microsoft.com/fr-fr/windows/win32/api/winuser/nf-winuser-sendmessagea 
z" SendMessageA"    4 User32 SendMessageA  ( hWnd msg wParam iParam -- LRESULT )




\ sets the coordinates of the specified POINT structure
: SetPoint  ( LPPOINT x y -- fl )
    rot >r
    r@ ->y   L!
    r> ->x   L!
  ;

\ gets the coordinates from the specified POINT structure
: GetPoint  ( LPPOINT -- x y )
    >r
    r@ ->x   SL@
    r> ->y   SL@
  ;


\ sets the coordinates of the specified rectangle
z" SetRect"         5 User32 SetRect    ( LPRECT xLeft yTop xRight yBottom -- fl )

\ get the coordinates of the specified rectangle
: GetRect  ( LPRECT -- left top right bottom )
    >r
    r@ ->left   SL@
    r@ ->top    SL@
    r@ ->right  SL@
    r> ->bottom SL@
  ;

\ move RECT structure
z" OffsetRect"  3 User32 OffsetRect   ( lprc dx dy -- fl )

\ @TODO: à tester rapidement
z" UnionRect"     3 User32 UnionRect    ( lprcDst lprcSrc1 lprcSrc2 -- fl )

\ Create a menu
z" CreateMenu"  0 User32 CreateMenu   ( -- hmenu|0 )

\ Assigns a new menu to the specified window
z" SetMenu"  2 User32 SetMenu   ( hwnd hmenu -- fl )

\ Adds a new item to the end of the specified menu bar, drop-down menu, submenu, or context menu
z" AppendMenuA"  4 User32 AppendMenuA   ( hmenu uFlags uIDNewItem lpNewItem -- fl )

\ **  Menu flags  **
$00000000 constant MF_INSERT
$00000080 constant MF_CHANGE
$00000100 constant MF_APPEND
$00000200 constant MF_DELETE
$00001000 constant MF_REMOVE
$00000080 constant MF_END

$00000000 constant MF_ENABLED
$00000001 constant MF_GRAYED
$00000002 constant MF_DISABLED
$00000000 constant MF_STRING
$00000004 constant MF_BITMAP
$00000000 constant MF_UNCHECKED
$00000008 constant MF_CHECKED
$00000010 constant MF_POPUP
$00000020 constant MF_MENUBARBREAK
$00000040 constant MF_MENUBREAK
$00000000 constant MF_UNHILITE
$00000080 constant MF_HILITE
$00000100 constant MF_OWNERDRAW
$00000200 constant MF_USECHECKBITMAPS
$00000000 constant MF_BYCOMMAND
$00000400 constant MF_BYPOSITION
$00000800 constant MF_SEPARATOR
$00001000 constant MF_DEFAULT
$00002000 constant MF_SYSMENU
$00004000 constant MF_HELP
$00004000 constant MF_RIGHTJUSTIFY
$00008000 constant MF_MOUSESELECT

forth definitions
