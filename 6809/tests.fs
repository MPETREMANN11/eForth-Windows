\ *********************************************************************
\ tets for meta and assembler
\    Filename:      tests.fs
\    Date:          20 oct. 2024
\    Updated:       20 oct. 2024
\    File Version:  1.0
\    MCU:           eFORTH windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


s" assert.fs"       included

only  forth definitions
in-forth

1024 constant target-allocation

\ create Z79FORTH target with target-allocation size
target-allocation target: Z79FORTH

assert( Z79FORTH target-address = )
assert( target-allocation target-size = )
assert( dp-t 0 = )
\ assert( dp-t  )








