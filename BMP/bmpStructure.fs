\ *********************************************************************
\ Definition of BMP file structure 
\    Filename:      bmpStructure.fs
\    Date:          09 jan 2026
\    Updated:       11 jan 2026
\    File Version:  1.0
\    Forth:         eFORTH Windows
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

\ documentation online:
\  - https://www.siloged.fr/cours/html/ISN_images/index.html?EtudeduformatBMP.html

\ A BMP file is divided into three areas:
\     the file header (bitmap-file header and bitmap-information header)[2];
\     the color palette;
\     the image data.

\ BMP header file

also structures

struct bmp_file_header             \ file header structure
    2 field ->magic_number
    4 field ->file_size
    2 field ->id_appli01           \ unused
    2 field ->id_appli02           \ unused
    4 field ->offset_content

struct bmp_picture_header          \ picture header structure
    4 field ->header_size
    4 field ->picture_width
    4 field ->picture_height
\         Le nombre de plans (sur 2 octets). Cette valeur vaut toujours 1
\         La profondeur de codage de la couleur (sur 2 octets)
\         La méthode de compression (sur 4 octets). Cette valeur vaut 0 lorsque l'image n'est pas compressée
\         La taille totale de l'image en octets (sur 4 octets).
\         La résolution horizontale (sur 4 octets), c'est-à-dire le nombre de pixels par mètre horizontalement
\         La résolution verticale (sur 4 octets), c'est-à-dire le nombre de pixels par mètre verticalement
\         Le nombre de couleurs de la palette (sur 4 octets)
\         Le nombre de couleurs importantes de la palette (sur 4 octets). Ce champ peut être égal à 0 lorsque chaque couleur a son importance.

struct bmp_file
    bmp_file_header     field ->file_header
    bmp_picture_header  field ->picture_header


\ get file size from BMP header
: get-file-size ( addr -- n )
    ->file_header ->file_size ul@
  ;

\ get offset for BMP content
: get-bmp-offset ( addr -- n )
    ->file_header ->offset_content ul@
  ;

\ get header size in bytes
: get-header-size ( addr -- n )
    ->picture_header ->header_size ul@
  ;

\ get picture-width in pixels
: get-picture-width ( addr -- n )
    ->picture_header ->picture_width ul@
  ;

\ get picture-height in pixels
: get-picture-height ( addr -- n )
    ->picture_header ->picture_height ul@
  ;

only FORTH
