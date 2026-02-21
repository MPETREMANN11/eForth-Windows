\ *********************************************************************
\ Define 3D manipulations
\    Filename:      manipulations.fs
\    Date:          16 feb 2026
\    Updated:       18 feb 2026
\    File Version:  1.0
\    Forth:         eFORTH Windows
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************



VARIABLE FOCAL  100 FOCAL !

: project { x y z -- xp yp }
    \ Sécurité : si Z est trop proche de 0 ou négatif, 
    \ on le fixe à 1 pour éviter le crash ou l'inversion.
    z 1 < IF 1 TO z THEN 
    \ Calcul de xp = (x * FOCAL) / z
    x FOCAL @ * z / 
    \ Calcul de yp = (y * FOCAL) / z
    y FOCAL @ * z /
;





