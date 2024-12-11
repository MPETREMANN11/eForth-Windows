\ *********************************************************************
\ Find avaialbles serial ports 
\    Filename:      findSerial.fs
\    Date:          07 dec 2024
\    Updated:       08 dec 2024
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


\ Searches a directory for a file or subdirectory with a name that matches a specific name
z" FindFirstFileA"  2 Kernel32 FindFirstFileA  ( lpFileName lpFindFileData -- handle|-1 )

\ https://learn.microsoft.com/fr-fr/windows/win32/api/fileapi/nf-fileapi-findnextfilea
\ Searches a directory for a file or subdirectory with a name that matches a specific name
z" FindNextFileA"  2 Kernel32 FindNextFileA  ( hFindFile lpFindFileData -- 0|infos )

\ Closes a file search handle
z" FindClose"  1 Kernel32 FindClose  ( hFindFile -- hFindFile )


\ typedef struct _WIN32_FIND_DATAA {
\   DWORD    dwFileAttributes;
\   FILETIME ftCreationTime;
\   FILETIME ftLastAccessTime;
\   FILETIME ftLastWriteTime;
\   DWORD    nFileSizeHigh;
\   DWORD    nFileSizeLow;
\   DWORD    dwReserved0;
\   DWORD    dwReserved1;
\   CHAR     cFileName[MAX_PATH];
\   CHAR     cAlternateFileName[14];
\   DWORD    dwFileType; // Obsolete. Do not use.
\   DWORD    dwCreatorType; // Obsolete. Do not use
\   WORD     wFinderFlags; // Obsolete. Do not use
\ } WIN32_FIND_DATAA, *PWIN32_FIND_DATAA, *LPWIN32_FIND_DATAA;

260 constant MAX_PATH

structures 

struct FILETIME 
  i32   field ->dwLowDateTime
  i32   field ->dwHighDateTime

struct WIN32_FIND_DATAA
  i32   field ->dwFileAttributes
  FILETIME field ->ftCreationTime
  FILETIME field ->ftLastAccessTime
  FILETIME field ->ftLastWriteTime
  i32   field ->nFileSizeHigh
  i32   field ->nFileSizeLow
  i32   field ->dwReserved0
  i32   field ->dwReserved1
  MAX_PATH  field ->cFileName
  14    field ->cAlternateFileName
  i32   field ->dwFileType      \ Obsolete. Do not use.
  i32   field ->dwCreatorType   \ Obsolete. Do not use
  i16   field ->wFinderFlags    \ Obsolete. Do not use


create FindFileData
    WIN32_FIND_DATAA allot

: @dwFileAttributes ( addr -- val )
    ->dwFileAttributes UL@
  ;

: @cFileName ( addr -- addr len )
    ->cFileName z>s
  ;

-1 constant INVALID_HANDLE_VALUE
$00000010 constant FILE_ATTRIBUTE_DIRECTORY

0 value hFind

: search-COM ( -- )
    z" COM1" FindFileData FindFirstFileA to hFind
    hFind INVALID_HANDLE_VALUE = if
        ." Error : no COM port found "
    else
        begin
            hFind FindFileData FindNextFileA
        while
\             FindFileData @dwFileAttributes FILE_ATTRIBUTE_DIRECTORY  and if
                ." Port COM found: " FindFileData @cFileName type
\             then
        repeat
    then
\     hFind FindClose drop
  ;


\ code de référence en C
\ 
\ int main() {
\     HANDLE hFind;
\     WIN32_FIND_DATA FindFileData;
\ 
\     // On commence à chercher à partir de COM1
\     hFind = FindFirstFile("COM1", &FindFileData);
\ 
\     if (hFind == INVALID_HANDLE_VALUE) {
\         printf("Aucun port COM trouvé.\n");
\     } else {
\         do {
\             if (!(FindFileData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)) {
\                 printf("Port COM trouvé : %s\n", FindFileData.cFileName);
\             }
\         } while (FindNextFile(hFind, &FindFileData));
\         FindClose(hFind);
\     }
\ 
\     return 0;
\ }


\ include serial.fs


