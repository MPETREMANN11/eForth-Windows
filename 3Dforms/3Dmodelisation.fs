\ *********************************************************************
\ Define 3D modelisation tools
\    Filename:      3Dmodelisation.fs
\    Date:          16 feb 2026
\    Updated:       18 feb 2026
\    File Version:  1.0
\    Forth:         eFORTH Windows
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

\ store numbers of defined items
: setNbItemsInObject ( nbItems addr -- )
    cell -          \ point to nbItems field
    !               \ store nbItems
  ;

\ get numbers of defined items
: getNbItemsInObject ( addr -- nbItems )
    cell -          \ point to nbItems field
    @               \ get nbItems
  ;

\ reserve one cell for nbItems field
: reserveItemField ( -- )
    0 ,             \ reserved for nbItems in 3D object
    0 to nbItems   \ init nbItems
  ;

\ convert initail addr to datas address
: >datasField ( add -- addr' )
    cell +
  ;

\ defining of 3D points collection
: 3Dobject: ( -- <name> | <name> -- addr )
    create  reserveItemField
    does>   >datasField
  ;

\ defining of 3D edges collection
: 3Dedge: ( -- <name> | <name> -- addr )
    create  reserveItemField
    does>   >datasField
  ;




