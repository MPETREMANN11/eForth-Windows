\ *********************************************************************
\ ListBox test
\    Filename:      06_ListBox.fs
\    Date:          19 nov. 2024
\    Updated:       19 nov. 2024
\    File Version:  1.0
\    Forth:         eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************



.( 06_ListBox.fs loaded - ListBox test )

\ HWND hwndList = GetDlgItem(hDlg, ID_LB1);
\ 
\ SendMessage(hwndList, LB_ADDSTRING, 0, (LONG)"Carré");
\ SendMessage(hwndList, LB_ADDSTRING, 0, (LONG)"Cercle");
\ SendMessage(hwndList, LB_ADDSTRING, 0, (LONG)"Triangle");
\ SendMessage(hwndList, LB_SETCURSEL, Forme, 0);


windows

\ Récupère un handle dans un contrôle dans la boîte de dialogue spécifiée
z" GetDlgItem"  2 User32 GetDlgItem  ( hDlg nIDDlgItem -- 0|HWND )

\ Récupère un handle dans un contrôle dans la boîte de dialogue spécifiée
z" SendMessageA" 4 User32 SendMessageA  ( hWnd Msg wParam lParam -- LRESULT )

$0180 constant LB_ADDSTRING
\ #define LB_INSERTSTRING        0x0181
\ #define LB_DELETESTRING        0x0182
\ #define LB_SELITEMRANGEEX      0x0183
\ #define LB_RESETCONTENT        0x0184
\ #define LB_SETSEL              0x0185
$0186 constant LB_SETCURSEL
\ #define LB_GETSEL              0x0187
\ #define LB_GETCURSEL           0x0188
\ #define LB_GETTEXT             0x0189
\ #define LB_GETTEXTLEN          0x018a
\ #define LB_GETCOUNT            0x018b
\ #define LB_SELECTSTRING        0x018c
\ #define LB_DIR                 0x018d
\ #define LB_GETTOPINDEX         0x018e
\ #define LB_FINDSTRING          0x018f
\ #define LB_GETSELCOUNT         0x0190
\ #define LB_GETSELITEMS         0x0191
\ #define LB_SETTABSTOPS         0x0192
\ #define LB_GETHORIZONTALEXTENT 0x0193
\ #define LB_SETHORIZONTALEXTENT 0x0194
\ #define LB_SETCOLUMNWIDTH      0x0195
\ #define LB_ADDFILE             0x0196
\ #define LB_SETTOPINDEX         0x0197
\ #define LB_GETITEMRECT         0x0198
\ #define LB_GETITEMDATA         0x0199
\ #define LB_SETITEMDATA         0x019a
\ #define LB_SELITEMRANGE        0x019b
\ #define LB_SETANCHORINDEX      0x019c
\ #define LB_GETANCHORINDEX      0x019d
\ #define LB_SETCARETINDEX       0x019e
\ #define LB_GETCARETINDEX       0x019f
\ #define LB_SETITEMHEIGHT       0x01a0
\ #define LB_GETITEMHEIGHT       0x01a1
\ #define LB_FINDSTRINGEXACT     0x01a2
\ #define LB_CARETON             0x01a3
\ #define LB_CARETOFF            0x01a4
\ #define LB_SETLOCALE           0x01a5
\ #define LB_GETLOCALE           0x01a6
\ #define LB_SETCOUNT            0x01a7
\ #define LB_INITSTORAGE         0x01a8
\ #define LB_ITEMFROMPOINT       0x01a9
\ #define LB_GETLISTBOXINFO      0x01b2
\ #define LB_MSGMAX              0x01b3

only 
windows also
graphics internals


0 value hwndList

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





