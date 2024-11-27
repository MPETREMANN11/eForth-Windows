\ *********************************************************************
\ image tests for windows
\    Filename:      15_imageTest.fs
\    Date:          23 nov 2024
\    Updated:       23 nov 2024
\    File Version:  1.0
\    MCU:           eFORTH
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

\ lien: https://melem.developpez.com/tutoriels/api-windows/gdi/ 

.( 15_imageTest.fs loaded )

\ define window dimension
600 constant WINDOW_WIDTH
400 constant WINDOW_HEIGHT











\ LRESULT CALLBACK WndProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
\ {
\     static HDC hDC;
\     PAINTSTRUCT ps;
\     
\     static HBITMAP hBitmap;
\     
\     switch(message)
\     {
\         case WM_CREATE:
\         {
\             hDC = GetDC(hwnd);
\             hBitmap = LoadImage(NULL, "c:\\image.bmp", IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE);
\             break;
\         }
\         
\         case WM_PAINT:
\         {
\             BeginPaint(hwnd, &ps);
\             DrawState(hDC, NULL, NULL, (LPARAM)hBitmap, 0, 20, 20, 0, 0, DST_BITMAP);
\             EndPaint(hwnd, &ps);
\             break;
\         }
\         
\         case WM_DESTROY:
\         {
\             ReleaseDC(hwnd, hDC);
\             DeleteObject(hBitmap);
\             PostQuitMessage(0);
\             break;
\         }
\         
\         default:
\         {
\             return DefWindowProc(hwnd, message, wParam, lParam);
\         }
\     }
\     
\     return 0L;
\ }






