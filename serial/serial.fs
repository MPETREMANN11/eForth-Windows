\ *********************************************************************
\ Serial Port
\    Filename:      serial.fs
\    Date:          06 dec 2024
\    Updated:       07 dec 2024
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************



.( serial.fs loaded)

only also 
windows also structures

$10000000 constant GENERIC_ALL      \ All possible access rights
$20000000 constant GENERIC_EXECUTE  \ Execute access
$40000000 constant GENERIC_WRITE    \ Write access
$80000000 constant GENERIC_READ     \ Read access


\ parameters used by CreateFileA
z" COM5" value CF_lpFileName
GENERIC_READ GENERIC_WRITE or value CF_dwDesiredAccess
0       value CF_dwShareMode
NULL    value CF_lpSecurityAttributes
OPEN_EXISTING value CF_dwCreationDisposition
0       value CF_dwFlagsAndAttributes
NULL    value CF_hTemplateFile

: .error  ( n -- )
    getLastError
    dup ." Error: " . space
    case
        2 of ." port indisponible "    endof
        5 of ." acces refuse "         endof
    endcase
  ;

-1 constant INVALID_HANDLE_VALUE

0 value hSerial

\ create handle for serial port
: create-serial ( -- hSerial )
    CF_lpFileName
    CF_dwDesiredAccess
    CF_dwShareMode
    CF_lpSecurityAttributes
    CF_dwCreationDisposition
    CF_dwFlagsAndAttributes
    CF_hTemplateFile
    CreateFileA 
    dup INVALID_HANDLE_VALUE = if
        .error
    then
  ;


\ Sets the control parameter for a serial communication device
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

create dcbSerialParams
    DCB allot

\ store serial parameters in DCB structure
: get-serial-params ( -- )
    hSerial dcbSerialParams GetCommState 0 = if
        abort" Error: GetCommState"
    then
  ;

\ initialise serial port
: init-serial
    create-serial to hSerial
    get-serial-params
  ;

\ close serial port
: close-serial ( -- )
    hSerial CloseHandle 0 = if
        abort" Error: CloseHandle"
    then
  ;

