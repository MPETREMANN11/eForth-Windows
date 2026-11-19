\ *********************************************************************
\ User32 definitions for windows vocabulary
\    Filename:      User32-definitions.fs
\    Date:          16 nov 2024
\    Updated:       19 nov 2026
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


<EOF>

\ Partie de code à intégrer et à tester:

\ =====================================================================
\ 1. POP-UPS : MessageBox
\ =====================================================================

\ Displays a modal dialog box
z" MessageBoxA" 4 User32 MessageBoxA ( hwnd lpText lpCaption uType -- id )

\ ** MessageBox flags **
$00000000 constant MB_OK
$00000001 constant MB_OKCANCEL
$00000002 constant MB_ABORTRETRYIGNORE
$00000003 constant MB_YESNOCANCEL
$00000004 constant MB_YESNO
$00000005 constant MB_RETRYCANCEL
$00000010 constant MB_ICONERROR
$00000020 constant MB_ICONQUESTION
$00000030 constant MB_ICONWARNING
$00000040 constant MB_ICONINFORMATION

\ ** MessageBox return values **
1 constant IDOK
2 constant IDCANCEL
3 constant IDABORT
4 constant IDRETRY
5 constant IDIGNORE
6 constant IDYES
7 constant IDNO

\ MessageBox without owner window
: MsgBox ( zText zTitle uType -- id )
    >r 0 rot rot r> MessageBoxA
;

\ =====================================================================
\ 2. MENUS CONTEXTUELS (pop-up) et compléments sur les menus
\ =====================================================================

\ Create a drop-down menu, submenu, or context menu
z" CreatePopupMenu" 0 User32 CreatePopupMenu ( -- hmenu|0 )

\ Destroys the specified menu
z" DestroyMenu" 1 User32 DestroyMenu ( hmenu -- fl )

\ Redraws the menu bar of the specified window (needed after SetMenu)
z" DrawMenuBar" 1 User32 DrawMenuBar ( hwnd -- fl )

\ Enables, disables, or grays a menu item
z" EnableMenuItem" 3 User32 EnableMenuItem ( hmenu uIDItem uEnable -- prev )

\ Sets the check-mark state of a menu item
z" CheckMenuItem" 3 User32 CheckMenuItem ( hmenu uIDItem uCheck -- prev )

\ Displays a shortcut menu at the specified location
z" TrackPopupMenu" 7 User32 TrackPopupMenu
        ( hmenu uFlags x y nReserved hwnd prcRect -- id|fl )

\ ** TrackPopupMenu flags **
$00000000 constant TPM_LEFTBUTTON
$00000002 constant TPM_RIGHTBUTTON
$00000000 constant TPM_LEFTALIGN
$00000004 constant TPM_CENTERALIGN
$00000008 constant TPM_RIGHTALIGN
$00000000 constant TPM_TOPALIGN
$00000010 constant TPM_VCENTERALIGN
$00000020 constant TPM_BOTTOMALIGN
$00000040 constant TPM_VERTICAL
$00000080 constant TPM_NONOTIFY
$00000100 constant TPM_RETURNCMD

\ Retrieves the cursor position in screen coordinates
z" GetCursorPos" 1 User32 GetCursorPos ( LPPOINT -- fl )

\ Moves the cursor
z" SetCursorPos" 2 User32 SetCursorPos ( x y -- fl )

\ Display popup menu at x y (screen coordinates), wait for choice.
\ TPM_RETURNCMD: returns the chosen item ID (0 if cancelled)
\ -> no window procedure (callback) needed
: PopupAt ( hmenu x y -- id )
    >r >r
    TPM_RETURNCMD TPM_RIGHTBUTTON or
    r> r>
    0 GetForegroundWindow 0 TrackPopupMenu
;

create ptCursor  8 allot

\ Display popup menu at mouse cursor position
: PopupAtCursor ( hmenu -- id )
    ptCursor GetCursorPos drop
    ptCursor GetPoint
    PopupAt
;

\ =====================================================================
\ 3. FENETRES : gestion de base
\ =====================================================================

z" ShowWindow"          2 User32 ShowWindow          ( hwnd nCmdShow -- fl )
z" UpdateWindow"        1 User32 UpdateWindow        ( hwnd -- fl )
z" DestroyWindow"       1 User32 DestroyWindow       ( hwnd -- fl )
z" EnableWindow"        2 User32 EnableWindow        ( hwnd fl -- fl )
z" IsWindowVisible"     1 User32 IsWindowVisible     ( hwnd -- fl )
z" SetForegroundWindow" 1 User32 SetForegroundWindow ( hwnd -- fl )
z" SetFocus"            1 User32 SetFocus            ( hwnd -- hwnd )
z" GetDesktopWindow"    0 User32 GetDesktopWindow    ( -- hwnd )
z" FindWindowA"         2 User32 FindWindowA         ( lpClass lpTitle -- hwnd )

\ ** ShowWindow commands **
0 constant SW_HIDE
1 constant SW_SHOWNORMAL
2 constant SW_SHOWMINIMIZED
3 constant SW_SHOWMAXIMIZED
5 constant SW_SHOW
6 constant SW_MINIMIZE
9 constant SW_RESTORE

\ Changes position and dimensions of a window
z" MoveWindow" 6 User32 MoveWindow ( hwnd x y w h repaint -- fl )

\ Changes size, position and Z order of a window
z" SetWindowPos" 7 User32 SetWindowPos ( hwnd hAfter x y cx cy uFlags -- fl )
$0001 constant SWP_NOSIZE
$0002 constant SWP_NOMOVE
$0004 constant SWP_NOZORDER
$0040 constant SWP_SHOWWINDOW

\ Retrieves window / client area rectangle (see GetRect)
z" GetWindowRect" 2 User32 GetWindowRect ( hwnd LPRECT -- fl )
z" GetClientRect" 2 User32 GetClientRect ( hwnd LPRECT -- fl )

\ Window text
z" SetWindowTextA"       2 User32 SetWindowTextA       ( hwnd lpString -- fl )
z" GetWindowTextA"       3 User32 GetWindowTextA       ( hwnd lpString nMax -- len )
z" GetWindowTextLengthA" 1 User32 GetWindowTextLengthA ( hwnd -- len )

\ Screen / client coordinates conversion
z" ScreenToClient" 2 User32 ScreenToClient ( hwnd LPPOINT -- fl )
z" ClientToScreen" 2 User32 ClientToScreen ( hwnd LPPOINT -- fl )

\ Posts a message without waiting
z" PostMessageA" 4 User32 PostMessageA ( hwnd msg wParam lParam -- fl )

\ =====================================================================
\ 4. LIEN AVEC GDI32 : contexte de périphérique, rafraîchissement
\ =====================================================================

z" GetDC"          1 User32 GetDC          ( hwnd -- hdc )
z" ReleaseDC"      2 User32 ReleaseDC      ( hwnd hdc -- fl )
z" InvalidateRect" 3 User32 InvalidateRect ( hwnd lprc bErase -- fl )

\ =====================================================================
\ 5. CLAVIER, SOURIS, SYSTEME (par scrutation, sans callback)
\ =====================================================================

z" GetAsyncKeyState" 1 User32 GetAsyncKeyState ( vKey -- state )
z" GetKeyState"      1 User32 GetKeyState      ( vKey -- state )
z" GetSystemMetrics" 1 User32 GetSystemMetrics ( index -- n )
z" MessageBeep"      1 User32 MessageBeep      ( uType -- fl )

$01 constant VK_LBUTTON
$02 constant VK_RBUTTON
$0D constant VK_RETURN
$1B constant VK_ESCAPE
$20 constant VK_SPACE

0 constant SM_CXSCREEN
1 constant SM_CYSCREEN

\ true if key / mouse button currently down
: KeyDown? ( vKey -- fl )
    GetAsyncKeyState $8000 and 0<>
;

\ =====================================================================
\ 6. CONTROLES STANDARDS (BUTTON, EDIT, STATIC, LISTBOX, COMBOBOX)
\ =====================================================================

\ Creates an overlapped, pop-up, or child window
\ @TODO: 12 paramètres, à tester
z" CreateWindowExA" 12 User32 CreateWindowExA
    ( exStyle lpClass lpName style x y w h hParent hMenu hInst lpParam -- hwnd )

\ ** Window styles **
$00000000 constant WS_OVERLAPPED
$80000000 constant WS_POPUP
$40000000 constant WS_CHILD
$10000000 constant WS_VISIBLE
$08000000 constant WS_DISABLED
$00800000 constant WS_BORDER
$00C00000 constant WS_CAPTION
$00080000 constant WS_SYSMENU
$00040000 constant WS_THICKFRAME
$00020000 constant WS_MINIMIZEBOX
$00010000 constant WS_MAXIMIZEBOX
$00010000 constant WS_TABSTOP
$00100000 constant WS_HSCROLL
$00200000 constant WS_VSCROLL
$00CF0000 constant WS_OVERLAPPEDWINDOW
$00000200 constant WS_EX_CLIENTEDGE

WS_CHILD WS_VISIBLE or constant WS_CHILDVIS

\ ** BUTTON styles **
0 constant BS_PUSHBUTTON
1 constant BS_DEFPUSHBUTTON
2 constant BS_CHECKBOX
3 constant BS_AUTOCHECKBOX
4 constant BS_RADIOBUTTON
7 constant BS_GROUPBOX
9 constant BS_AUTORADIOBUTTON

\ ** EDIT styles **
$0004 constant ES_MULTILINE
$0080 constant ES_AUTOHSCROLL
$0800 constant ES_READONLY

\ ** COMBOBOX / LISTBOX styles **
1 constant CBS_SIMPLE
2 constant CBS_DROPDOWN
3 constant CBS_DROPDOWNLIST
1 constant LBS_NOTIFY

\ create a standard control, hInstance and lpParam = 0, id = hMenu
\ ex: 0 z" BUTTON" z" OK" WS_CHILDVIS BS_PUSHBUTTON or
\     10 10 80 24 GetForegroundWindow 100 CreateControl
: CreateControl ( ex zClass zText style x y w h hParent id -- hwnd )
    0 0 CreateWindowExA
;

\ ** Messages **
$000C constant WM_SETTEXT
$000D constant WM_GETTEXT
$000E constant WM_GETTEXTLENGTH
$0010 constant WM_CLOSE
$0030 constant WM_SETFONT
$0111 constant WM_COMMAND

$00F0 constant BM_GETCHECK
$00F1 constant BM_SETCHECK

$0143 constant CB_ADDSTRING
$0146 constant CB_GETCOUNT
$0147 constant CB_GETCURSEL
$0148 constant CB_GETLBTEXT
$014B constant CB_RESETCONTENT
$014E constant CB_SETCURSEL
$014F constant CB_SHOWDROPDOWN

$0180 constant LB_ADDSTRING
$0184 constant LB_RESETCONTENT
$0186 constant LB_SETCURSEL
$0188 constant LB_GETCURSEL
$0189 constant LB_GETTEXT
$018B constant LB_GETCOUNT

\ COMBOBOX helpers
: CbAdd    ( hwnd zText -- )   >r CB_ADDSTRING 0 r> SendMessageA drop ;
: CbSel@   ( hwnd -- index )   CB_GETCURSEL 0 0 SendMessageA ;
: CbSel!   ( hwnd index -- )   >r CB_SETCURSEL r> 0 SendMessageA drop ;
: CbClear  ( hwnd -- )         CB_RESETCONTENT 0 0 SendMessageA drop ;

\ LISTBOX helpers
: LbAdd    ( hwnd zText -- )   >r LB_ADDSTRING 0 r> SendMessageA drop ;
: LbSel@   ( hwnd -- index )   LB_GETCURSEL 0 0 SendMessageA ;
: LbSel!   ( hwnd index -- )   >r LB_SETCURSEL r> 0 SendMessageA drop ;
: LbClear  ( hwnd -- )         LB_RESETCONTENT 0 0 SendMessageA drop ;

\ BUTTON (check box / radio) helpers
: Checked? ( hwnd -- fl )      BM_GETCHECK 0 0 SendMessageA 1 = ;
: Check!   ( hwnd fl -- )      >r BM_SETCHECK r> 0 <> 1 and 0 SendMessageA drop ;

forth definitions

