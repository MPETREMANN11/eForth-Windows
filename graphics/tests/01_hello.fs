\ *********************************************************************
\ Hello test
\    Filename:      01_hello.fs
\    Date:          14 nov. 2024
\    Updated:       16 nov. 2024
\    File Version:  1.0
\    Forth:         eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************



.( 01_hello.fs loaded - test MessageBoxA / similar as MessageBox )

\ MessageBoxA info: 
\   https://learn.microsoft.com/fr-fr/windows/win32/api/winuser/nf-winuser-messageboxA


only 
windows also
graphics internals



: lpText r" You must make a choice:
 Yes to continue
 No  to stop" s>z ;
z" make a choice"      constant lpCaption

: MSGbox  ( -- )
    NULL lpText lpCaption MB_YESNO MessageBoxA 
    ?dup if
        cr ." You have pressed: " 
        case
            6 of ." Yes"      endof
            7 of ." No"       endof
        endcase
    then
;




: run
\     600 400 window 1000 ms
    MSGbox
\     GetLastError
\     exit
\     begin
\         poll
\         IDLE event = if 
\             draw
\         then
\     event FINISHED = until
    key drop
\     bye
;





