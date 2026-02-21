\ *********************************************************************
\ FILES 
\    Filename:      main.fs
\    Date:          07 jan 2026
\    Updated:       12 jan 2026
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
  0 value BITS_PER_PIXEL
  0 value PICTURE_SIZE

create FILE_HEAD
    HEADER_SIZE allot

\ get datas from header and store this in values
: get-header-values ( -- )
    FILE_HEAD >r
    r@ get-file-size      to FILE_SIZE
    r@ get-bmp-offset     to BMP_OFFSET
    r@ get-header-size    to HEADER_SIZE
    r@ get-picture-width  to PICTURE_WIDTH
    r@ get-picture-height to PICTURE_HEIGHT
    r@ get-bits-per-pixel to BITS_PER_PIXEL
    r@ get-picture-size   to PICTURE_SIZE
    r> drop
  ;

: dispHeadDatas  ( -- )
    FILE_HEAD HEADER_SIZE dump
    cr ." FILE_SIZE     : " FILE_SIZE . 
    cr ." BMP_OFFSET    : " BMP_OFFSET . 
    cr ." HEADER_SIZE   : " HEADER_SIZE .
    cr ." PICT._WIDTH   : " PICTURE_WIDTH .
    cr ." PICT._HEIGHT  : " PICTURE_HEIGHT .
    cr ." BITS / PIXEL  : " BITS_PER_PIXEL .
    cr ." PICT._SIZE    : " PICTURE_SIZE .
    cr
  ;


variable fileID     \ stores the file handle

: open-bmp-file ( addr len -- )
    r/o bin open-file throw fileID !        \ open file in read only mode
  ;

: close-bmp-file ( -- )
    fileID @ close-file throw           \ close file
    0 fileID !
  ;

\ point to an array for picture datas storage
0 value BMP_RAW_DATAS
    
\ allot n bytes for BMP datas storage
: bmp-raw-allot
\     BMP_RAW_DATAS throw                 \ stop if BMP array previously alloted
    here to BMP_RAW_DATAS
    PICTURE_SIZE allot
  ;

\ read BMP header
: read-file-header ( -- )
    0 fileID @ reposition-file
    FILE_HEAD HEADER_SIZE fileID @ read-file throw drop
  ;

\ read BMP datas from file
: read-file-bmp-datas ( -- )
    BMP_OFFSET fileID @ reposition-file
    BMP_RAW_DATAS PICTURE_SIZE fileID @ read-file throw drop
  ;

\ load BMP header and BMP datas
: load-bmp ( addr len -- )
    open-bmp-file
    read-file-header
    get-header-values
    bmp-raw-allot
    read-file-bmp-datas
    close-bmp-file
  ;


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

