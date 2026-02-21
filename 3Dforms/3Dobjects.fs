\ *********************************************************************
\ Define 3D objects
\    Filename:      3Dobjects.fs
\    Date:          17 feb 2026
\    Updated:       17 feb 2026
\    File Version:  1.0
\    Forth:         eFORTH Windows
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************




3Dobject: CUBE-POINTS
  -10 -10 -10  3Dpoint, \ 0: arrière-bas-gauche
   10 -10 -10  3Dpoint, \ 1: arrière-bas-droit
   10  10 -10  3Dpoint, \ 2: arrière-haut-droit
  -10  10 -10  3Dpoint, \ 3: arrière-haut-gauche
  -10 -10  10  3Dpoint, \ 4: avant-bas-gauche
   10 -10  10  3Dpoint, \ 5: avant-bas-droit
   10  10  10  3Dpoint, \ 6: avant-haut-droit
  -10  10  10  3Dpoint, \ 7: avant-haut-gauche
nbItems CUBE-POINTS setNbItemsInObject

3Dedge: CUBE-EDGES
 0  1  3Dedge,  1  2  3Dedge,  2  3  3Dedge,  3  0  3Dedge,  \ Face arrière
 4  5  3Dedge,  5  6  3Dedge,  6  7  3Dedge,  7  4  3Dedge,  \ Face avant
 0  4  3Dedge,  1  5  3Dedge,  2  6  3Dedge,  3  7  3Dedge,  \ Liaisons
nbItems CUBE-EDGES setNbItemsInObject
