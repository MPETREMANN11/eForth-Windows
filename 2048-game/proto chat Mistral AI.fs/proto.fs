
\ Définir les constantes pour la taille de la grille
4 constant GRID_WIDTH
GRID_WIDTH GRID_WIDTH * constant GRID_SIZE

\ Créer une grille vide
create 2048grid GRID_SIZE cells allot

\ Initialiser la grille avec des zéros
: grid-init ( -- )
  2048grid GRID_SIZE cells bounds
  do
    i 0!
  loop ;

\ Afficher la grille
: grid-display ( -- )
  2048grid GRID_SIZE cells bounds
  do
    i @ .
    i GRID_WIDTH mod 0= if cr then
  loop ;

\ Ajouter un 2 ou un 4 aléatoirement dans une case vide
: add-random ( -- )
  \ Générer une position aléatoire
  GRID_SIZE srand nip
  \ Vérifier si la case est vide
  dup 2048grid + @ 0= if
    \ Ajouter 2 ou 4 (90% de chance pour 2, 10% pour 4)
    10 rand 1 = if 4 else 2 then swap 2048grid + !
  else
    drop
  then ;

\ Déplacer toutes les tuiles vers la gauche
: move-left ( -- )
  \ Implémentation simplifiée : déplacer toutes les tuiles vers la gauche sans fusion
  0
  GRID_WIDTH 0 do
    GRID_WIDTH 0 do
      dup i GRID_WIDTH * + j + 2048grid + @
      dup 0> if
        \ Trouver la première case vide ou égale à gauche
        i 1- 0 max 0 do
          dup k GRID_WIDTH * + j + 2048grid + @
          0= if
            \ Déplacer la tuile
            dup i GRID_WIDTH * + j + 2048grid + @
            over k GRID_WIDTH * + j + 2048grid + !
            0 over i GRID_WIDTH * + j + 2048grid + !
            leave
          then
        loop
      then
      drop
    loop
  loop
  drop ;

\ Initialiser et jouer
: 2048 ( -- )
  init-grid
  add-random
  grid-display
  \ Pour l'instant, on ne fait qu'un seul mouvement à gauche
  move-left
  grid-display ;

\ Exécuter le jeu
2048
