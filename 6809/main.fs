\ *********************************************************************
\ 6809 assembler
\    Filename:      main.fs
\    Date:          08 oct. 2024
\    Updated:       08 oct. 2024
\    File Version:  1.0
\    MCU:           eFORTH windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

DEFINED? --6809 [if] forget --6809  [then]
create --6809

\ meta instructions
s" meta.fs" included


\ 6809 assembler
s" assembler.fs" included


\ test meta and assembler
s" tests.fs" included


\ test meta and assembler
s" Z79FORTH.fs" included


