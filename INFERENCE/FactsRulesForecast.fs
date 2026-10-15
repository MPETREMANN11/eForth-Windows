\ *********************************************************************
\ Define facts
\    Filename:      facts.fs
\    Date:          08 oct 2026
\    Updated:       10 oct 2026
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

\ facts defined in facts vocabulary
DEUTSCH  ?\ z" Außentemperatur"
ENGLISH  ?\ z" Outside temperature"
FRANCAIS ?\ z" Température extérieure"
-2 FACT: ext-temp

DEUTSCH  ?\ z" Regnet es?"
ENGLISH  ?\ z" Is it raining?"
FRANCAIS ?\ z" Est-ce qu'il pleut?"
TRUE FACT: is-raining

DEUTSCH  ?\ z" Friert es?"
ENGLISH  ?\ z" Is it freezing?"
FRANCAIS ?\ z" Gèle-t-il?"
FALSE FACT: is-freezing


