\ *********************************************************************
\ Kernel extensions 
\    Filename:      Kernel32-definitions.fs
\    Date:          01 dec 2024
\    Updated:       01 dec 2024
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


\ online doc: https://learn.microsoft.com/fr-fr/windows/win32/api/_gdi/


\ ***  Graphics primitives in graphics internals voc.  *************************

only forth
windows also structures also
windows definitions

\ returns the system's time and date  @TODO: à tester
z" GetLocalTime"       1 Kernel32 GetLocalTime ( iStyle cWidth color -- hPen )

\ typedef struct _SYSTEMTIME {
\   WORD wYear;
\   WORD wMonth;
\   WORD wDayOfWeek;
\   WORD wDay;
\   WORD wHour;
\   WORD wMinute;
\   WORD wSecond;
\   WORD wMilliseconds;
\ } SYSTEMTIME, *PSYSTEMTIME, *LPSYSTEMTIME;

stuct SYSTEMTIME
  i16 field ->wYear
  i16 field ->wMonth
  i16 field ->wDayOfWeek
  i16 field ->wDay
  i16 field ->wHour
  i16 field ->wMinute
  i16 field ->wSecond
  i16 field ->wMilliseconds



only forth definitions




