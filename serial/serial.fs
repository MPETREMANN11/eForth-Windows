\ *********************************************************************
\ Serial Port
\    Filename:      serial.fs
\    Date:          06 dec 2024
\    Updated:       09 dec 2024
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************



.( serial.fs loaded)

only also 
windows also structures

\ Initializes the communication parameters of a specified communication device
\ @TODO à tester
z" SetupComm"  3 Kernel32 SetupComm  ( hFile dwInQueue dwOutQueue -- fl )


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

: .error  ( -- )
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
\ struct DCB \ transfered in Kernel32-definitions.fs

\ DCB structure for COM port
create dcbSerialParams
    DCB allot

\ store serial parameters in DCB structure
: get-serial-params ( hSerial -- )
     dcbSerialParams GetCommState 0 = if
        abort" Error: GetCommState"
    then
  ;

2 constant EVENPARITY
3 constant MARKPARITY
0 constant NOPARITY
1 constant ODDPARITY
3 constant SPACEPARITY

0 constant ONESTOPBIT       \ 1 stop bit
1 constant ONE5STOPBITS     \ 1.5 stop bits
2 constant TWOSTOPBITS      \ 2 stop bits

\ set speedn byte size, parity and stop bit
: set-speed-8N1 ( dcbStruct -- )
    >r
    115200      r@ !field ->BaudRate
    8           r@ !field ->ByteSize
    NOPARITY    r@ !field ->Parity
    ONESTOPBIT  r> !field ->StopBits
  ;

\ set serial port with DCB structure
: set-serial-params ( hSerial -- )
    dcbSerialParams SetCommState 0 = if
        abort" Error: GetCommState"
    then
  ;

\ initialise serial port
: init-serial
    create-serial to hSerial
    hSerial get-serial-params
    dcbSerialParams set-speed-8N1
    hSerial set-serial-params
  ;

\ close serial port
: close-serial ( -- )
    hSerial CloseHandle 0 = if
        abort" Error: CloseHandle"
    then
  ;


