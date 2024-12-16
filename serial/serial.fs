\ *********************************************************************
\ Serial Port
\    Filename:      serial.fs
\    Date:          06 dec 2024
\    Updated:       16 dec 2024
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

\ initialise in and out buffer sizes
128 value dwInQueue
128 value dwOutQueue

\ set in and out buffer sizes of serial port
: setup-comm ( hFile -- fl )
    dwInQueue dwOutQueue SetupComm  0 = if
        abort" Error: SetupComm "
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
        abort" Error: GetCommState "
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
    hSerial setup-comm
  ;

\ close serial port
: close-serial ( -- )
    hSerial CloseHandle 0 = if
        abort" Error: CloseHandle"
    then
  ;

\ get numbers of transmitted bytes
variable BytesWritten 

\ send string to serial port
: to-serial { addr len -- }
    hSerial addr len BytesWritten NULL WriteFile 0= if
        ." Error : WiteFile " .error
    then
  ;

\ send one char to serial port
: char-to-serial  ( char -- )
    hSerial swap TransmitCommChar  0= if
        ." Error : TransmitCommChar " .error
    then
  ;

\ send CR to serial port
: CR-to-serial ( -- )
    $0D char-to-serial
  ;

\ send CRLF to serial port
: CRLF-to-serial ( -- )
    $0D char-to-serial
    $0A char-to-serial
  ;

\ defered word to send Cr or CRLF to serial port
defer serialCR
    ' CR-to-serial is serialCR

\ send string to serial port, terminated by CRLF
: str-to-serial ( addr len -- )
    to-serial
  ;

\ get numbers of received bytes
variable BytesRead

create BUFFER
    dwInQueue allot
    
\ get transmission from serial port
: from-serial ( -- )
    BUFFER dwInQueue erase
    hSerial BUFFER dwInQueue BytesRead NULL ReadFile 0= if
        ." Error : ReadFile " .error
    then
  ;

\ display buffer content
: .buffer ( -- )
    buffer BytesRead @ type
  ;

