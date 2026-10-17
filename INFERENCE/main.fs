\ *********************************************************************
\ Definitions for inference engine
\    Filename:      main.fs
\    Date:          08 oct 2026
\    Updated:       15 oct 2026
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

DEFINED? --inference [if] forget --inference  [then]
create --inference

include dumpTool.fs
include vocForthExtensions.fs

\ include config.fs               \ load config
include inference-engine.fs     \ load inference engine

\ include facts.fs
include FactsRulesLoan.fs
\ include tests.fs


