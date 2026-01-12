\ tests for BMP files
\    Filename:      tests.fs
\    Date:          09 jan 2026
\    Updated:       10 jan 2026
\    File Version:  1.0
\    Forth:         eFORTH Windows
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

\ 1. read BMP head file
\ s" BMP/blueberry.bmp" read-header


s" BMP/blueberry.bmp" read-file-header
dispHeadDatas

