\ *********************************************************************
\ Read Write to LoRa Transmitter
\    Filename:      RWtoLoRa.fs
\    Date:          15 dec 2024
\    Updated:       18 dec 2024
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
    ['] CRLF-to-serial is serialCR
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


\ test AT string to LoRa transmitter
\ AT zCRLF +buff!
\ 
\ init-COM7
\ SEND_BUFFER z>s to-serial 
\ from-serial
\ .buffer

\ *** defining LoRa Help words *****************************

: transmit-receive-display  ( -- )
    zCRLF +buff!
    buffer-to-serial
    200 ms
    from-serial .buffer
  ;

: ATaddress?
    ATaddress +buff" ?"  transmit-receive-display ;

: ATband?
    ATband +buff" ?"  transmit-receive-display ;

: ATcrfop?
    ATcrfop +buff" ?"  transmit-receive-display ;

: ATcpin?
    ATcpin +buff" ?"  transmit-receive-display ;







