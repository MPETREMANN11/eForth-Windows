\ *********************************************************************
\ xxx
\    Filename:      http.fs
\    Date:          11 dec 2024
\    Updated:       11 dec 2024
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

windows
z" Wininet.dll" dll Wininet

\ Initializes an application's use of WinINet functions
z" InternetOpenA"       5 Wininet InternetOpenA  ( [5 params] -- hSession )

\ Opens a File Transfer Protocol (FTP) or HTTP session for a given site
z" InternetConnectA"    8 Wininet InternetConnectA  ( [8 params] -- hConnect|0 )

\ Creates an HTTP request handle
z" HttpOpenRequestA"    8 Wininet HttpOpenRequestA  ( [8 params] -- hRequest|0 )

\ Sends the specified request to the HTTP server
z" HttpSendRequestA"    6 Wininet HttpSendRequestA   ( [5 params] -- fl )

\ Reads data from a handle opened by InternetOpenUrl, FtpOpenFile, or HttpOpenRequestA
z" InternetReadFile"    4 Wininet InternetReadFile  ( [4 params] -- fl )

\ Closes a single Internet handle
z" InternetCloseHandle" 1 Wininet InternetCloseHandle  ( hInternet -- fl )

\ exemple ouverture flux Internet

\ int main() {
\     HINTERNET hSession = InternetOpen("My Agent", INTERNET_OPEN_TYPE_DIRECT, NULL, NULL, 0);
\     if (hSession) {
\         HINTERNET hConnect = InternetConnect(hSession, "www.example.com", INTERNET_DEFAULT_HTTP_PORT, NULL, NULL, INTERNET_SERVICE_HTTP, 0, 0);
\         if (hConnect) {
\             HINTERNET hRequest = HttpOpenRequest(hConnect, "GET", "/index.html", NULL, NULL, NULL, INTERNET_FLAG_RELOAD, 0);
\             if (hRequest) {
\                 if (HttpSendRequest(hRequest, NULL, 0, NULL, 0)) {
\                     // Lire la réponse
\                     char buffer[1024];
\                     DWORD dwBytesRead;
\                     while (InternetReadFile(hRequest, buffer, sizeof(buffer), &dwBytesRead) && dwBytesRead > 0) {
\                         // Traiter les données lues
\                     }
\                 }
\                 InternetCloseHandle(hRequest);
\             }
\             InternetCloseHandle(hConnect);
\         }
\         InternetCloseHandle(hSession);
\     }
\     return 0;
\ }








