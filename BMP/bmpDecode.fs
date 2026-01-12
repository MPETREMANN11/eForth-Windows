\ *********************************************************************
\ FILES 
\    Filename:      main.fs
\    Date:          07 jan 2026
\    Updated:       07 jan 2026
\    File Version:  1.0
\    Forth:         eFORTH Windows
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

\ Definition a list of values
  0 value FILE_SIZE
  0 value BMP_OFFSET
128 value HEADER_SIZE
  0 value PICTURE_WIDTH
  0 value PICTURE_HEIGHT

create fileHead
    HEADER_SIZE allot

\ get datas from header and store this in values
: get-header-values ( -- )
    fileHead >r
    r@ get-file-size      to FILE_SIZE
    r@ get-bmp-offset     to BMP_OFFSET
    r@ get-header-size    to HEADER_SIZE
    r@ get-picture-width  to PICTURE_WIDTH
    r@ get-picture-height to PICTURE_HEIGHT
    r> drop
  ;

: dispHeadDatas  ( -- )
    fileHead HEADER_SIZE dump
    cr ." FILE_SIZE     : " FILE_SIZE . 
    cr ." BMP_OFFSET    : " BMP_OFFSET . 
    cr ." HEADER_SIZE   : " HEADER_SIZE .
    cr ." PICT._WIDTH   : " PICTURE_WIDTH .
    cr ." PICT._HEIGHT  : " PICTURE_HEIGHT .
    cr
  ;


variable fileID     \ stores the file handle

: read-file-header ( addr len -- )
    r/o open-file throw fileID !        \ open file in read only mode
    \ Read HEADER_SIZE bytes and store its in fileHead
    fileHead HEADER_SIZE fileID @ read-file throw
    drop \ drop real bytes reads
    fileID @ close-file throw           \ close file
    get-header-values
  ;




\ decimal
\ 
\ create bmp-raw 
\     DATAS_TO_READ allot \ Stockage temporaire du BMP (16 octets * 32 lignes)
\ create oled-data 
\     DATAS_TO_READ allot \ Stockage final (128 colonnes * 3 pages)
\ 
\ 0 variable fd   \ store file descriptor

\ : load-bmp ( s-addr len -- )
\     r/o open-file throw fd !
\     62 fd @ reposition-file \ Sauter l'entête
\     \ Lire les 23 lignes (BMP est Bottom-Up, on stocke tel quel d'abord)
\     bmp-raw DATAS_TO_READ fd @ read-file throw drop
\     fd @ close-file throw 
\   ;

\ : get-pixel ( x y -- bit )
\     \ BMP est Bottom-Up : y=0 est en bas. 
\     \ Pour le SSD1306, on veut y=0 en haut.
\     22 swap - ( x y_inv )
\     16 * over 8 / + bmp-raw + c@ ( x bit-block )
\     swap 7 and 7 swap - rshift 1 and ;

\ : get-pixel { x y -- bit }
\     \ 1. Inversion de l'axe Y (BMP est Bottom-Up)
\     \ On part du principe que l'image fait 23 pixels de haut (0 à 22)
\     22 y - { inv-y }
\     
\     \ 2. Calcul de l'adresse de l'octet dans bmp-raw
\     \ Chaque ligne fait 16 octets (128 pixels / 8)
\     inv-y 16 * x 8 / + { offset }
\     
\     \ 3. Extraction de l'octet
\     bmp-raw offset + c@ { octet }
\     
\     \ 4. Extraction du bit spécifique dans l'octet
\     \ En BMP, le bit de poids fort (MSB) est le pixel le plus à gauche
\     7 x 7 and - { bit-pos }
\     
\     octet bit-pos rshift 1 and
\   ;

\ : convert-to-oled ( -- )
\     oled-data 384 0 fill
\     3 0 do i ( page )
\         128 0 do j ( col )
\             0 ( octet-en-construction )
\             8 0 do ( bit )
\                 over i 8 * j + ( x y )
\                 dup 23 < if get-pixel else 0 then \ Padding noir si y > 22
\                 i lshift or
\             loop
\             \ Stocker l'octet dans oled-data
\             k 128 * j + oled-data + c!
\         loop
\     loop ;

\ : dump-forth-code ( -- )
\     cr ." create oled-img "
\     384 0 do
\         i 16 mod 0= if cr then
\         oled-data i + c@ . ." c, "
\     loop cr ." \ Fin de l'image" cr ;

\ : process ( s-addr len -- )
\     load-bmp convert-to-oled dump-forth-code ;

\ Exemple : s" image.bmp" process

