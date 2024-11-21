\ *********************************************************************
\ textOut test
\    Filename:      02_textOut.fs
\    Date:          16 nov. 2024
\    Updated:       16 nov. 2024
\    File Version:  1.0
\    Forth:         eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************



.( 02_textOut.fs loaded - display text in window )



\ MessageBoxA info: 
\   https://learn.microsoft.com/fr-fr/windows/win32/api/winuser/nf-winuser-messageboxA


only 
windows also
graphics internals

 
z" ReleaseDC" 2 User32 ReleaseDC

: String01 s" This is a first test string..."  ;
: String02 s" This is a second test string..."  ;


\ : TXTout  ( -- )
\     WIN02 10 30 String02 TextOutA  drop
\ ;

: inWIN01
    500 300 window 100 ms
    hdc 10 10 String01 TextOutA  drop
    hWnd hDc ReleaseDC drop
  ;

: inWIN02
    500 300 window 100 ms
    hdc 10 10 String02 TextOutA  drop
    hwnd hdc ReleaseDC drop
  ;

: run02
    inWIN01
    key drop
    inWIN02
    key drop
  ;





