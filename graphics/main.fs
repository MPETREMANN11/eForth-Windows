\ *********************************************************************
\ Graphics extensions main file
\    Filename:      main.fs
\    Date:          09 mar 2023
\    Updated:       22 nov 2024
\    File Version:  1.0
\    MCU:           eFORTH
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


DEFINED? --graphics [if] forget --graphics  [then]
create --graphics

DEFINED? L, invert [IF]
\ compile 32 bits value in dictionnary
: L,  ( u -- )
    dup c,
    8 rshift dup c,
    8 rshift dup c,
    8 rshift dup c,
    drop
  ;
[THEN]


DEFINED? .( invert [IF]
-1 value COMMENT_DISPLAY
: .(   
    [char] ) parse 
    COMMENT_DISPLAY if
        type cr 
    else
        2drop
    then
  ; immediate
[THEN]


\ load graphics tests
include config.fs

\ load graphics tests
\ include tests/graphicsTests.fs

\ load 01_hello.fs test
include tests/01_hello.fs

\ load 02_textOut.fs test
\ include tests/02_textOut.fs

\ load 04_drawText.fs test
\ include tests/04_drawText.fs

\ load 05_SetTextColor.fs test
\ include tests/05_SetTextColor.fs

\ load 06_DialogBox.fs test
\ include tests/06_DialogBox.fs

\ load 06_ListBox.fs test
\ include tests/06_ListBox.fs

\ load 07_ListBox.fs test
\ include tests/07_ListBox.fs

\ load 08_CreateWindow.fs windows test
\ include tests/08_CreateWindow.fs

\ load 09_graphicsTests.fs line box, etc... test
\ include tests/09_graphicsTests.fs

\ load 10_shapesTests.fs rectangles, etc...
\ include tests/10_shapesTests.fs

\ load 11_ellipseTests.fs 
\ include tests/11_ellipseTests.fs

\ load 12_arcsTests.fs 
\ include tests/12_arcsTests.fs

\ load 13_mappingTests.fs 
\ include tests/13_mappingTests.fs 

\ load 14_fontTests.fs
\ include tests/14_fontTests.fs

\ load 15_imageTest.fs
\ include tests/15_imageTest.fs

\ load 16_rectangleTest.fs
\ include tests/16_rectangleTest.fs

\ load 17_messageTests.fs
\ include tests/17_messageTests.fs

