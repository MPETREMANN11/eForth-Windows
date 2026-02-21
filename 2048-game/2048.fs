\ *********************************************************************
\ 2048 game
\    Filename:      2048.fs
\    Date:          09 mar 2025
\    Updated:       23 mar 2025
\    File Version:  1.0
\    MCU:           eFORTH windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


\ ***  G R I D    D E S C R I P T I O N  ***************************************

\ Define constants for grid size
4 constant GRID_WIDTH
GRID_WIDTH dup * constant GRID_SIZE

\ Create an empty grid
create 2048grid 
    GRID_SIZE allot

\ Initialize the grid with zeros
: grid-init ( -- )
    2048grid GRID_SIZE erase
  ;


\ ***  G R I D    N U M B E R S    M A N I P U L A T I O N  ********************

\ calculate addr in 2048 grid
: getXYaddr  { xpos ypos -- addr }
    ypos GRID_WIDTH * 
    xpos +
    2048grid +
  ;

\ get value n from x y position in 2048 grid
: xyVal@  ( x y -- n )
    getXYaddr c@
  ;

\ store value n to x y position in 2048 grid
: xyVal!  ( n x y -- )
    getXYaddr c!
  ;


\ ***  G R I D    D I S P L A Y  ***********************************************

0 value X_POSITION
0 value Y_POSITION

\ set grid display position
: set-display-position ( x y -- )
    to Y_POSITION
    to X_POSITION
  ;


\ insert digit or _ in string
: #_  ( n -- n' )
    dup 0= if
        [char] _ hold
    else
        #
    then
  ;

\ display 2expN in field ____
: .exp2N  ( n -- )
    ?dup 0= if
        ." ____" 
        exit
    then
    1 swap lshift
    <# #_ #_ #_ #_ #> type
  ;




\ display 2048 grid
: grid-display ( -- )
    GRID_WIDTH 0 do
        cr \ i . space
        GRID_WIDTH 0 do 
            i j xyVal@ .exp2N space
        loop
    loop
  ;







\ Init and play
: 2048-play ( -- )
    grid-init
\   add-random
\   grid-display
\   \ Pour l'instant, on ne fait qu'un seul mouvement à gauche
\   move-left
\   .grid 
  ;

\ Play game
\ 2048-play

