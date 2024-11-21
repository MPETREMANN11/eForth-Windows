\ *********************************************************************
\ DialogBox test
\    Filename:      06_DialogBox.fs
\    Date:          19 nov. 2024
\    Updated:       19 nov. 2024
\    File Version:  1.0
\    Forth:         eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************



.( 06_DialogBox.fs loaded - DialogBox test )

\ USER32.DLL contain these functions:
\ CreateDialogIndirectParamA
\ CreateDialogIndirectParamAorW
\ CreateDialogIndirectParamW
\ CreateDialogParamA
\ CreateDialogParamW


windows

\ Récupère un handle dans un contrôle dans la boîte de dialogue spécifiée
z" CreateDialogA"  4 User32 CreateDialogA   ( hInstance lpName hWndParent lpDialogFunc -- )



only 
windows also
graphics internals


0 value HWND

create LISTBOX
    RECT allot

: LISTtest  ( -- )
    LISTBOX 20 24 56 45 SetRect drop
    hdc LISTBOX GetDlgItem ?dup 0= if
        GetLastError .
        exit
    then
  ;



: run06
    400 200 window 100 ms
    $FF $00 $00 RGB to color
    LISTtest
    key drop
  ;





