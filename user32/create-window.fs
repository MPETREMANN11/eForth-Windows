\ *********************************************************************
\ create window for windows
\    Filename:      create-menu.fs
\    Date:          04 dec 2024
\    Updated:       05 dec 2024
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


.( creaate-menu.fs loaded )

\ à éplucher:
\ https://github.com/microsoft/Windows-classic-samples
\ https://www.code4th.com/posts/create-windows-gui-using-forth/

only
also structures
also windows also internals
also graphics internals

z" MyClass"     constant MyClassName
z" Test Window" constant MyWindowName

NULL GetModuleHandleA constant hinst

create myWindClass
    WINDCLASSA allot

0 value myClass

: set-myWindClass ( -- )
    myWindClass WINDCLASSA erase
    WindowProcShim myWindClass ->lpfnWndProc !
    hinst myWindClass ->hInstance !
    MyClassName myWindClass ->lpszClassName !
    NULL IDC_ARROW LoadCursorA myWindClass ->hCursor !
    hinst IDI_MAIN_ICON LoadIconA myWindClass ->hIcon !
    myWindClass RegisterClassA to myclass
  ;

set-myWindClass

: MyWindowProc { hwnd msg w l }
  WM_DESTROY msg = if
    0 PostQuitMessage
    0 exit
  then
  hwnd msg w l DefWindowProcA
;


0 value hwnd

0 value CW_dwExStyle
myclass      value CW_lpClassName
MyWindowName value CW_lpWindowName
WS_OVERLAPPEDWINDOW value CW_dwStyle

create CW_WidthHeight
    RECT allot
    CW_WidthHeight 10 10 640 480 SetRect drop

NULL    value CW_hWndParent
NULL    value CW_hMenu
hinst   value CW_hInstance
' MyWindowProc callback value CW_lpParam    \ NULL value CW_lpParam

: create-window  ( -- hwnd )  \ 12 params
    CW_dwExStyle 
    CW_lpClassName 
    CW_lpWindowName
    CW_dwStyle 
    CW_WidthHeight GetRect
    CW_hWndParent 
    CW_hMenu 
    CW_hInstance 
    CW_lpParam     \ ['] MyWindowProc callback
    CreateWindowExA
  ;

: show-window  ( hwnd -- )
    >r
    r@ SW_SHOWNORMAL ShowWindow drop
    r> SetForegroundWindow drop
  ;

.( use RUN to test window)
: run ( -- )
    create-window to hwnd
    hwnd show-window
    begin
        poll
        IDLE event = if 
             
        then
        event FINISHED = 
    until
  ;


