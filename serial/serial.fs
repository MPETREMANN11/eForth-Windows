\ *********************************************************************
\ Serial Port
\    Filename:      serial.fs
\    Date:          06 dec 2024
\    Updated:       18 dec 2024
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************



.( serial.fs loaded)

\ define FALSE and TRUE C compatible flags
0 constant FALSE
1 constant TRUE

only also 
windows also structures

\ show error
: .error  ( -- )
    getLastError
    dup ." Error: " . space
    case
        2 of ." port indisponible "    endof
        5 of ." acces refuse "         endof
        6 of ." invalid handle "       endof
    endcase
  ;


\ *** Global parameters ********************************************************

\ initialise in and out buffer sizes
256 constant dwInQueue
256 constant dwOutQueue

\ RECeiVe BUFFER used by ReadFile
create RECV_BUFFER
    dwInQueue allot
    
\ SEND BUFFER used by WriteFile
create SEND_BUFFER
    dwOutQueue allot


\ *** SET PARAMS for CreateFileA ***********************************************

\ parameters for CF_dwDesiredAccess
$10000000 constant GENERIC_ALL      \ All possible access rights
$20000000 constant GENERIC_EXECUTE  \ Execute access
$40000000 constant GENERIC_WRITE    \ Write access
$80000000 constant GENERIC_READ     \ Read access

\ parameters for CF_dwShareMode
\ $00000001 constant FILE_SHARE_READ
\ $00000002 constant FILE_SHARE_WRITE


\ parameters used by CreateFileA
z" COM5" value CF_lpFileName
GENERIC_READ GENERIC_WRITE or value CF_dwDesiredAccess
0       value CF_dwShareMode
NULL    value CF_lpSecurityAttributes
OPEN_EXISTING value CF_dwCreationDisposition
0       value CF_dwFlagsAndAttributes
NULL    value CF_hTemplateFile

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

\ set in and out buffer sizes of serial port
: setup-comm ( hFile -- fl )
    dwInQueue dwOutQueue SetupComm  0 = if
        abort" Error: SetupComm " .error
    then
  ;


\ *** CONTROL TIME OUT *********************************************************

create timeouts
    COMMTIMEOUTS allot

\ set read timeouts
: set-timeouts  { readInterv readTotInterv -- }
    timeouts COMMTIMEOUTS erase
    readInterv      timeouts !field ->ReadIntervalTimeout
    readTotInterv   timeouts !field ->ReadTotalTimeoutConstant
    hSerial timeouts SetCommTimeouts  0 = if
        abort" Error: SetCommTimeouts " .error
    then
  ;
    
\ Usage:
\  50 500 set-timeouts


\ *** SET SERIAL PARAMETERS ****************************************************

\ Sets the control parameter for a serial communication device
\ struct DCB \ transfered in Kernel32-definitions.fs

\ DCB structure for COM port
create dcbSerialParams
    DCB allot

\ store serial parameters in DCB structure
: get-serial-params ( hSerial -- )
     dcbSerialParams GetCommState 0 = if
        abort" Error: GetCommState " .error
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
    DCB         r@ !field ->DCBlength
    TRUE        r@ !field ->fBinary
\     TRUE        r@ !field ->fParity 
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


\ *** INIT and CLOSE SERIAL PORT ***********************************************

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


\ *** ADD zSTRING to SEND BUFFER ***********************************************

\ add string to SEND BUFFER
: +buff!  ( zstr -- )
    SEND_BUFFER swap lstrcatA drop
  ;

\ store new string in SEND BUFFER
: buff!  ( zstr -- )
    SEND_BUFFER dwOutQueue erase        \ reset content of SEND BUFFER
    +buff!
  ;

\ compile a zstring 
: +buff"  ( -- <s-string> )
    postpone z"
    postpone +buff!
  ; immediate

\ CRLF in z-string format
create zCRLF
    $0D c,
    $0A c,
    $00 c,

\ CR in z-string format
create zCR
    $0D c,
    $00 c,


\ *** WRITE and READ ***********************************************************

\ get numbers of transmitted bytes
variable BytesWritten 

\ send string to serial port
: to-serial { addr len -- }
    len if  \ test if str is not empty
        hSerial addr len BytesWritten NULL WriteFile 0= if
            ." Error : WiteFile " .error
        then
    else 
        abort" nothing to send in SEND_BUFFER"
    then
  ;

\ send content of SEND_BUFFER to serial port
: buffer-to-serial ( -- )
    SEND_BUFFER z>s to-serial
  ;

\ send one char to serial port
: char-to-serial  { char -- } 
    hSerial char TransmitCommChar  0= if
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
    serialCR
  ;

\ get numbers of received bytes
variable BytesRead

\ get transmission from serial port
: from-serial ( -- )
    RECV_BUFFER dwInQueue erase
    hSerial RECV_BUFFER dwInQueue BytesRead NULL ReadFile 0= if
        ." Error : ReadFile " .error
    then
  ;

\ display buffer content
: .buffer ( -- )
    RECV_BUFFER BytesRead @ type
  ;

