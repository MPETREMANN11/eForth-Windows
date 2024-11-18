\ *********************************************************************
\ User32 definitions for windows vocabulary
\    Filename:      User32-definitions.fs
\    Date:          16 nov 2024
\    Updated:       18 nov 2024
\    File Version:  1.0
\    MCU:           eFORTH
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************



only forth 
windows definitions

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
z" SendMessageA"     4 User32 SendMessageA  ( hWnd msg wParam iParam -- LRESULT )





\ sets the coordinates of the specified rectangle
z" SetRect"     5 User32 SetRect

