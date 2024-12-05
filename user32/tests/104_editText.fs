\ *********************************************************************
\ create window tests for windows
\    Filename:      103_CreateWindowEx.fs
\    Date:          24 nov 2024
\    Updated:       02 dec 2024
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


.( 103_CreateWindowEx.fs loaded )


\ à éplucher:
\ https://github.com/microsoft/Windows-classic-samples
\ https://www.code4th.com/posts/create-windows-gui-using-forth/

only
also structures
also windows also internals
also graphics internals

\ UINT GetDlgItemTextA(
\   [in]  HWND  hDlg,
\   [in]  int   nIDDlgItem,
\   [out] LPSTR lpString,
\   [in]  int   cchMax
\ );

\ Destroys the specified window @TODO: semble pas fonctionner
z" GetDlgItemTextA"  4 User32 GetDlgItemTextA   ( hdlg nIDDlgItel lpString cchMax -- n|0 )



100 constant ID_EDIT  \ Identifiant du contrôle d'édition

$00000030 constant MB_ICONEXCLAMATION
$00000030 constant MB_ICONWARNING
$00000040 constant MB_ICONINFORMATION
$00000040 constant MB_ICONASTERISK
$00000020 constant MB_ICONQUESTION
$00000010 constant MB_ICONSTOP
$00000010 constant MB_ICONERROR
$00000010 constant MB_ICONHAND

\ create ps PAINTSTRUCT allot

\ 255 192 0 RGB CreateSolidBrush constant orange
\ 0 255 0 RGB CreateSolidBrush constant green

\ create side RECT allot
\ 0 0 200 100 SetRect

\ : MyWindowProc { hwnd msg w l }
\   WM_DESTROY msg = if
\     0 PostQuitMessage
\     0 exit
\   then
\   WM_PAINT msg = if
\     hwnd ps BeginPaint drop
\     ps ->hdc @ ps ->rcPaint orange FillRect drop
\     ps ->hdc @ side green FillRect drop
\     hwnd ps EndPaint drop
\     0 exit
\   then
\   hwnd msg w l DefWindowProcA
\ ;



$00000200 constant WS_EX_CLIENTEDGE


\ https://learn.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles


0 value hwInput

: show-input-box 
    WS_EX_CLIENTEDGE  \ dwExStyle
    NULL              \ lpClassName - optionnal
    z" Saisissez votre texte ici"  \ lpWindowName
    WS_CHILD WS_VISIBLE or            \ dwStyle
    10 10           \ X Y
    200 20         \ nWidth nHeight
    hwnd            \ hWndParent
    NULL            \ hMenu
    NULL            \ hInstance
    NULL            \ lpParam
\     MB_OK MB_ICONINFORMATION or ID_EDIT CreateWindowExA  \ Crée la boîte de dialogue
    CreateWindowExA to hwInput \ Crée la boîte de dialogue
    hwInput SW_SHOW ShowWindow  ;  \ Affiche la boîte de dialogue
  ;

: get-input 
    100 GetDlgItemTextA  \ Récupère le texte du contrôle d'édition
    s" Vous avez saisi : " type \ Affiche le texte saisi
  ;

\ : run 
\     show-input-box 
\     get-input 
\   ;



: run103
    create-window to hwnd
    hwnd show-window
    begin
        poll
        IDLE event = if 
             
        then
        event FINISHED = 
    until
  ;





