\ *********************************************************************
\ Read Write to LoRa Transmitter
\    Filename:      RWtoLoRa.fs
\    Date:          15 dec 2024
\    Updated:       26 dec 2024
\    File Version:  1.0
\    MCU:           eForth Windows
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


.( RWtoLoRa.fs loaded)

\ LoRa REYAX serial port configuration:
\  Port: depends on setup
\  Baud rate: 115200
\  Data: 8
\  Stop: 1
\  Parity: None
\  Receive Newline: CR+LF
\  Transmit Command Delimiter: CR+LF


: init-COM7  ( -- )
    z" COM7" to CF_lpFileName
    create-serial to hSerial
    hSerial get-serial-params
    dcbSerialParams set-speed-8N1
    hSerial set-serial-params
    hSerial setup-comm
    50 500 set-timeouts
  ;


\ *** defining LoRa AT sequences  **********************************************

: ATseq:
    create ,
    does>
        @ buff!
  ;

z" AT"              ATseq: AT
z" AT+ADDRESS="     ATseq: ATaddress
z" AT+BAND="        ATseq: ATband
z" AT+CRFOP="       ATseq: ATcrfop
z" AT+CPIN="        ATseq: ATcpin
z" AT+FACTORY"      ATseq: ATfactory
z" AT+IPR="         ATseq: ATipr
z" AT+MODE="        ATseq: ATmode
z" AT+NETWORKID="   ATseq: ATnetworkid
z" AT+PARAMETER="   ATseq: ATparameter
z" AT+RESET"        ATseq: ATreset
z" AT+UID?"         ATseq: ATuid
z" AT+VER?"         ATseq: ATver
\ z" +RCV="           ATseq: +rcv


\ *** defining LoRa get paramaters and help ************************************

\ transmit SEND_BUFFER to LoRa and read result to RECV_BUFFER
\ and display result
: transmit-receive-display  ( -- )
    CRLFbuff+!
    buffer-to-serial
    200 ms
    from-serial .buffer
  ;

\ decode error message in RECV_BUFFER
: LoRa-errors ( -- )
    RECV_BUFFER z>s
    2dup s" +ERR=15" startswith? if 
        cr ." Unknow error" 2drop exit                  then
    2dup s" +ERR=13" startswith? if 
        cr ." TX data more than 240bytes" 2drop exit    then
    2dup s" +ERR=12" startswith? if 
        cr ." CRC error" 2drop exit                     then
    2dup s" +ERR=11" startswith? if 
        cr ." RX is over times" 2drop exit              then
    2dup s" +ERR=10" startswith? if 
        cr ." TX is over times" 2drop exit              then
    2dup s" +ERR=4" startswith? if 
        cr ." Unknow command" 2drop exit                then
    2dup s" +ERR=3" startswith? if 
        cr ." There is not = symbol in the AT command" 
        2drop exit                                      then
    2dup s" +ERR=2" startswith? if 
        cr ." The head of AT command is not AT string" 
        2drop exit                                      then
    2dup s" +ERR=1" startswith? if 
        cr ."  Not CR-LF in the end of the AT Command." 
        2drop exit                                      then
  ;

\ get help for selected LoRa commands
: get-LoRa-params  ( -- )
    cr ." Lora params : "
    cr ."  T test transmission"
    cr ."  A address    B band        C crfof"
    cr ."  I ipr        P parameter"
    cr ." press key "  
    key $5F and  \ conver ASCII code to upper case
    case
        [char] T of AT transmit-receive-display exit    endof
        [char] A of ATaddress                           endof
        [char] B of ATband                              endof
        [char] C of ATcrfop                             endof
        [char] I of ATipr                               endof
        [char] P of ATparameter                         endof
    endcase
    cr  [char] ? cbuff+! 
    transmit-receive-display
    LoRa-errors
  ;











