\ *********************************************************************
\ Kernel extensions 
\    Filename:      Kernel32-definitions.fs
\    Date:          01 dec 2024
\    Updated:       09 dec 2024
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


\ online doc: https://learn.microsoft.com/fr-fr/windows/win32/api/_gdi/


\ ***  Graphics primitives in graphics internals voc.  *************************

only forth 
windows also
windows definitions
structures

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


only forth definitions




