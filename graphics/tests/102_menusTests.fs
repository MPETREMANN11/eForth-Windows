\ *********************************************************************
\ menus tests for windows
\    Filename:      102_menusTests.fs
\    Date:          24 nov 2024
\    Updated:       24 nov 2024
\    File Version:  1.0
\    MCU:           eFORTH
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

.( 102_menusTests.fs loaded )

forth definitions
windows also graphics also internals

\ test for uEforth window
0 value hwnd        \ handle for current window
0 value hdc         \ handle for DC

: getHandles  ( -- )
    GetForegroundWindow to hwnd
    hwnd getDC to hdc
  ;


\ xxx @todo à teste rapidement
z" CreatePopupMenu"  0 User32 CreatePopupMenu   ( -- hmenu|0  )




create MENU_MSG
    MSG allot


0 value hmenu

: testMenu  ( -- )
    CreateMenu to hmenu
    hmenu MF_STRING 1 z" vlist"     AppendMenuA drop
    hmenu MF_STRING 2 z" page"      AppendMenuA drop
    hmenu MF_STRING 3 z" hex"       AppendMenuA drop
    hmenu MF_STRING 4 z" decimal"   AppendMenuA drop
    hwnd hmenu SetMenu 0= if
        abort" ERROR: SetMenu"
    then
\     hwnd DrawMenuBar if
\         abort" ERROR: DrawMenuBar"
\     then
  ;

variable wParam
variable lParam

create mymsg 
    MSG allot 

: WndProc { hwnd mymsg wParam lParam }
  ;
 


\ struct MSG
\     ptr field ->hwnd
\     i32 field ->message
\     i16 field ->wParam
\     i32 field ->lParam
\     i32 field ->time
\   POINT field ->pt
\     i32 field ->lPrivate

: MenuProc  ( -- )
    msgbuf ->message ul@ WM_COMMAND = if
        msgbuf ->lParam ul@
        case
            1 of vlist          endof
            2 of page           endof
            3 of hex            endof
            4 of decimal        endof
        endcase
    then
  ;


hwnd mymsg wParam ' WndProc callback DefWindowProcA

: run102  ( -- )
    600 400 window
    getHandles
    testMenu
    begin
        poll
        MenuProc
        event FINISHED = 
    until
  ;


