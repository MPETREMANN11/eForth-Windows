\ *********************************************************************
\ Kernel extensions 
\    Filename:      Kernel32-definitions.fs
\    Date:          01 dec 2024
\    Updated:       20 nov 2026
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


\ online doc: https://learn.microsoft.com/fr-fr/windows/win32/api/_gdi/

only forth 
windows also
windows definitions
structures

\ ***  directory list and selection  *******************************************

260 constant MAX_PATH
create cwd-buf
     MAX_PATH 1+ allot

\ récupère le chemin courant
z" GetCurrentDirectoryA" 2 Kernel32 GetCurrentDirectoryA ( n buf -- len )

: getcwd ( -- a n )
    MAX_PATH cwd-buf GetCurrentDirectoryA  cwd-buf swap 
  ;

z" SetCurrentDirectoryA" 1 Kernel32 SetCurrentDirectoryA ( z -- fl )

\ display current directory
\ pwd = print working directory
: pwd  ( -- )
    getcwd type cr ;

\ select other directory, use in compilation
: (cd) ( zPath -- )
    SetCurrentDirectoryA drop
  ;

\ select other directory, use in iterpret only
: cd ( -- <path> )
    state @ if  abort" cd : Interpret only!"  then 
    bl parse s>z (cd)
  ; immediate

z" FindFirstFileA" 2 Kernel32 FindFirstFileA ( z *fd -- hFind )
z" FindNextFileA"  2 Kernel32 FindNextFileA  ( hFind *fd -- fl )
z" FindClose"      1 Kernel32 FindClose      ( hFind -- fl )

create _fd
    320 allot       \ structure WIN32_FIND_DATAA
variable hFind

\ Accès aux champs de la structure
: fd-attr  ( -- n )
    _fd UL@ ;                 \ attributs, offset 0
: fd-size  ( -- n )    
    _fd 32 + UL@ ;            \ taille (32 bits de poids faible), offset 32
: fd-name  ( -- a n )  
    _fd 44 + z>s ;            \ nom, offset 44
: fd-dir?  ( -- f )    
    fd-attr $10 and 0<> ;    \ FILE_ATTRIBUTE_DIRECTORY

: .entry ( -- )
    fd-dir? if 
        ." <DIR> " 
    else 
        fd-size u. 
    then
    fd-name type cr
  ;

\ display files in path. path optionnal
\ ls = abbreviated list
: ls ( -- <path> )
    state @ if  abort" ls : Interpret only!"  then 
    bl parse dup 0= if 
        2drop s" *" 
    then
    s>z _fd FindFirstFileA
    dup hFind !  -1 = if 
        ." not found" 
        cr exit 
    then
    begin
        .entry
        hFind @ _fd FindNextFileA 0=
    until
    hFind @ FindClose drop 
  ; immediate



\ ***  General words  **********************************************************







\ Appends one string to another
z" lstrcatA" 2 Kernel32 lstrcatA  ( lpString1 lpString2 -- lpString1|0 )

\ returns the system's time and date
z" GetLocalTime"       1 Kernel32 GetLocalTime ( addr -- )

struct SYSTEMTIME
  i16 field ->wYear
  i16 field ->wMonth
  i16 field ->wDayOfWeek
  i16 field ->wDay
  i16 field ->wHour
  i16 field ->wMinute
  i16 field ->wSecond
  i16 field ->wMilliseconds

\  Windows handles bottom out as void pointers. )
\ : HANDLE    ptr ;
\ : DWORD     i32 ;
\ : WINLONG   i32 ;
\ : UINT      i32 ;
\ : WPARAM    ptr ;
\ : LPARAM    ptr ;
\ : WINBOOL   i32 ;
\ : WINWORD   i16 ;
\ : WORD      i16 ;
\ : BYTE      i8 ;
\ : char      i8 ;


\ ***  SERIAL WORDS  ***********************************************************

struct DCB 
  i32 field ->DCBlength
  i32 field ->BaudRate
  i32 field ->fBinary
  i32 field ->fParity
  i32 field ->fOutxCtsFlow
  i32 field ->fOutxDsrFlow
  i32 field ->fDtrControl
  i32 field ->fDsrSensitivity
  i32 field ->fTXContinueOnXoff
  i32 field ->fOutX
  i32 field ->fInX
  i32 field ->fErrorChar
  i32 field ->fNull
  i32 field ->fRtsControl
  i32 field ->fAbortOnError
  i32 field ->fDummy2
  i16  field ->wReserved
  i16  field ->XonLim
  i16  field ->XoffLim
  i8  field ->ByteSize
  i8  field ->Parity
  i8  field ->StopBits
  i8  field ->XonChar
  i8  field ->XoffChar
  i8  field ->ErrorChar
  i8  field ->EofChar
  i8  field ->EvtChar
  i16  field ->wReserved1

\ Retrieves the current control settings for a specified communications device
z" GetCommState" 2 Kernel32 GetCommState  ( hSerial lpDCB -- fl )

\ configures a communications device according to the specifications in a DCB
z" SetCommState" 2 Kernel32 SetCommState  ( hSerial lpDCB -- fl )

\ Transmits a specified character before any pending data in the output buffer
z" TransmitCommChar" 2 Kernel32 TransmitCommChar  ( hFile cChar -- fl )

\ Initializes the communication parameters of a specified communication device
z" SetupComm"  3 Kernel32 SetupComm  ( hFile dwInQueue dwOutQueue -- fl )

\ structure control for timeouts
struct COMMTIMEOUTS
  i32 field ->ReadIntervalTimeout
  i32 field ->ReadTotalTimeoutMultiplier
  i32 field ->ReadTotalTimeoutConstant
  i32 field ->WriteTotalTimeoutMultiplier
  i32 field ->WriteTotalTimeoutConstant

\ sets the time-out parameters for all read and write operations on a specified communications device
z" SetCommTimeouts" 2 Kernel32 SetCommTimeouts  ( hSerial timeouts -- fl )



only forth definitions




