\ *********************************************************************
\ API extensions for windows
\    Filename:      windowsExtensions.fs
\    Date:          16 nov 2024
\    Updated:       16 nov 2023
\    File Version:  1.0
\    MCU:           eFORTH
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


\ online doc: https://learn.microsoft.com/fr-fr/windows/win32/api/_gdi/


only forth 
windows definitions

\ write text   @TODO: à tester rapidement
z" DrawTextA"   5 User32 DrawTextA
\ int DrawTextA(
\   [in]      HDC    hdc,
\   [in, out] LPCSTR lpchText,
\   [in]      int    cchText,
\   [in, out] LPRECT lprc,
\   [in]      UINT   format
\ );

\ DrawText format flags
$00000000 constant DT_TOP
$00000000 constant DT_LEFT
$00000001 constant DT_CENTER
$00000002 constant DT_RIGHT
$00000004 constant DT_VCENTER
$00000008 constant DT_BOTTOM
$00000010 constant DT_WORDBREAK
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



