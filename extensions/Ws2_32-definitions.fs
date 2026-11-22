\ *********************************************************************
\ Winsock definitions for eForth Windows
\    Filename:      Ws2_32-definitions.fs
\    Date:          22 nov 2026
\    Updated:       22 nov 2026
\    File Version:  1.0
\    MCU:           eForth Windows
\    Library:       Ws2_32.dll (Windows Sockets 2)
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************


\ ***  @TODO: code à tester  ***************************************************



\ Usage:
\   needs extensions/Ws2_32-definitions.fs
\   winsock
\   ws-init  ...  ws-exit
\
\ Assumptions: 64 bits cells (SOCKET is a 64 bits value, structure offsets
\ are those of Windows x64).
\ ==========================================================================

vocabulary winsock
only forth
also windows
also structures
also winsock definitions

\ Loads the Windows dynamic-link library Ws2_32.dll and associates it with the
\ Forth word Ws2_32 in order to call its socket functions.
z" Ws2_32.dll" dll Ws2_32


\ ==========================================================================
\ ** Constants **
\ ==========================================================================

\ ** WSAStartup version: MAKEWORD(2,2) **
$0202           constant WINSOCK_VERSION

\ ** special values **
-1              constant INVALID_SOCKET
-1              constant SOCKET_ERROR
$ffffffff       constant INADDR_NONE
0               constant INADDR_ANY
$7f000001       constant INADDR_LOOPBACK        \ host byte order
$7fffffff       constant SOMAXCONN

\ ** address families **
0               constant AF_UNSPEC
2               constant AF_INET
23              constant AF_INET6

\ ** socket types **
1               constant SOCK_STREAM
2               constant SOCK_DGRAM
3               constant SOCK_RAW

\ ** protocols **
0               constant IPPROTO_IP
6               constant IPPROTO_TCP
17              constant IPPROTO_UDP

\ ** setsockopt / getsockopt levels and options **
$ffff           constant SOL_SOCKET
1               constant TCP_NODELAY            \ level IPPROTO_TCP
$0004           constant SO_REUSEADDR
$0008           constant SO_KEEPALIVE
$0020           constant SO_BROADCAST
$0080           constant SO_LINGER
$1001           constant SO_SNDBUF
$1002           constant SO_RCVBUF
$1005           constant SO_SNDTIMEO            \ value in milliseconds
$1006           constant SO_RCVTIMEO            \ value in milliseconds
$1007           constant SO_ERROR
$1008           constant SO_TYPE

\ ** shutdown **
0               constant SD_RECEIVE
1               constant SD_SEND
2               constant SD_BOTH

\ ** send / recv flags **
1               constant MSG_OOB
2               constant MSG_PEEK
8               constant MSG_WAITALL

\ ** ioctlsocket commands **
$8004667e       constant FIONBIO                \ non blocking mode
$4004667f       constant FIONREAD               \ bytes available to read

\ ** WSAPoll events **
$0001           constant POLLERR
$0002           constant POLLHUP
$0004           constant POLLNVAL
$0010           constant POLLOUT                \ POLLWRNORM
$0100           constant POLLRDNORM
$0300           constant POLLIN                 \ POLLRDNORM | POLLRDBAND

\ ** errors constants **
10004           constant WSAEINTR
10009           constant WSAEBADF
10013           constant WSAEACCES
10014           constant WSAEFAULT
10022           constant WSAEINVAL
10024           constant WSAEMFILE
10035           constant WSAEWOULDBLOCK
10036           constant WSAEINPROGRESS
10038           constant WSAENOTSOCK
10039           constant WSAEDESTADDRREQ
10040           constant WSAEMSGSIZE
10043           constant WSAEPROTONOSUPPORT
10045           constant WSAEOPNOTSUPP
10047           constant WSAEAFNOSUPPORT
10048           constant WSAEADDRINUSE
10049           constant WSAEADDRNOTAVAIL
10050           constant WSAENETDOWN
10051           constant WSAENETUNREACH
10053           constant WSAECONNABORTED
10054           constant WSAECONNRESET
10055           constant WSAENOBUFS
10056           constant WSAEISCONN
10057           constant WSAENOTCONN
10060           constant WSAETIMEDOUT
10061           constant WSAECONNREFUSED
10065           constant WSAEHOSTUNREACH
10067           constant WSAEPROCLIM
10091           constant WSASYSNOTREADY
10092           constant WSAVERNOTSUPPORTED
10093           constant WSANOTINITIALISED
11001           constant WSAHOST_NOT_FOUND


\ ==========================================================================
\ ** Ws2_32.dll functions **
\ ==========================================================================

\ ** library startup / cleanup **
\ Initiates use of the Winsock DLL
z" WSAStartup"      2 Ws2_32 WSAStartup     ( wVersionRequested lpWSAData -- err )
\ Terminates use of the Winsock DLL
z" WSACleanup"      0 Ws2_32 WSACleanup     ( -- err )
\ Returns the error status for the last Winsock operation
z" WSAGetLastError" 0 Ws2_32 WSAGetLastError ( -- err )
z" WSASetLastError" 1 Ws2_32 WSASetLastError ( err -- )

\ ** socket creation / destruction **
z" socket"          3 Ws2_32 socket         ( af type protocol -- s )
z" closesocket"     1 Ws2_32 closesocket    ( s -- err )
z" shutdown"        2 Ws2_32 shutdown       ( s how -- err )

\ ** connection management **
z" bind"            3 Ws2_32 bind           ( s addr namelen -- err )
z" listen"          2 Ws2_32 listen         ( s backlog -- err )
z" accept"          3 Ws2_32 accept         ( s addr addrlen -- s' )
z" connect"         3 Ws2_32 connect        ( s addr namelen -- err )

\ ** data transfer **
z" send"            4 Ws2_32 send           ( s buf len flags -- n )
z" recv"            4 Ws2_32 recv           ( s buf len flags -- n )
z" sendto"          6 Ws2_32 sendto         ( s buf len flags to tolen -- n )
z" recvfrom"        6 Ws2_32 recvfrom       ( s buf len flags from fromlen -- n )

\ ** options, modes and polling **
z" setsockopt"      5 Ws2_32 setsockopt     ( s level optname optval optlen -- err )
z" getsockopt"      5 Ws2_32 getsockopt     ( s level optname optval optlen -- err )
z" ioctlsocket"     3 Ws2_32 ioctlsocket    ( s cmd argp -- err )
z" select"          5 Ws2_32 select         ( nfds readfds writefds exceptfds timeout -- n )
z" WSAPoll"         3 Ws2_32 WSAPoll        ( fdarray fds timeout -- n )

\ ** names and addresses **
z" getsockname"     3 Ws2_32 getsockname    ( s name namelen -- err )
z" getpeername"     3 Ws2_32 getpeername    ( s name namelen -- err )
z" gethostname"     2 Ws2_32 gethostname    ( name namelen -- err )
z" gethostbyname"   1 Ws2_32 gethostbyname  ( zName -- hostent )
z" inet_addr"       1 Ws2_32 inet_addr      ( zIp -- ip )
z" inet_ntoa"       1 Ws2_32 inet_ntoa      ( ip -- zIp )
z" getaddrinfo"     4 Ws2_32 getaddrinfo    ( zNode zService hints ppResult -- err )
z" freeaddrinfo"    1 Ws2_32 freeaddrinfo   ( addrinfo -- )


\ ==========================================================================
\ ** Work areas **
\ ==========================================================================

create wsaData  512 allot       \ WSADATA (408 bytes under x64)
create sa        16 allot       \ sockaddr_in work area
create pollfd    16 allot       \ WSAPOLLFD work area
create hostBuf  256 allot       \ local host name
create crlf      13 c, 10 c,
variable optval                 \ option value / ioctl argument
variable fromLen                \ recvfrom address length
variable sendSock               \ used by sock-send-all


\ ==========================================================================
\ ** Utilities: byte order and memory access **
\ ==========================================================================

\ store 16 bits value, little endian
: u16! ( u addr -- )
    over $ff and over c!
    swap 8 rshift $ff and swap 1+ c!
  ;

\ store 32 bits value, little endian
: u32! ( u addr -- )
    2dup u16!
    swap 16 rshift swap 2 + u16!
  ;

\ fetch 32 bits value, little endian
: u32@ ( addr -- u )
    dup c@ over 1+ c@ 8 lshift or
    over 2 + c@ 16 lshift or
    swap 3 + c@ 24 lshift or
  ;

\ sign extend a 32 bits integer returned by an API function
\ (an int result of -1 comes back as $ffffffff in a 64 bits cell)
: >int32 ( n -- n' )
    $ffffffff and
    dup $80000000 and if  $100000000 -  then
  ;

\ host to network byte order conversions
: htons ( u -- u' )
    $ffff and dup 8 rshift swap $ff and 8 lshift or
  ;
: ntohs ( u -- u' )   htons ;

: htonl ( u -- u' )
    $ffffffff and >r
    r@ 24 rshift $ff and
    r@ 8 rshift $ff00 and or
    r@ $ff00 and 8 lshift or
    r> $ff and 24 lshift or
  ;
: ntohl ( u -- u' )   htonl ;


\ ==========================================================================
\ ** Error management **
\ ==========================================================================

\ Interrupts the program in the event of a Winsock error
: ws-abort ( err -- )
    dup 0= if  drop exit  then
    case
        WSAEINTR            of true abort" Interrupted function call"           endof
        WSAEBADF            of true abort" Bad file handle"                     endof
        WSAEACCES           of true abort" Permission denied"                   endof
        WSAEFAULT           of true abort" Bad address"                         endof
        WSAEINVAL           of true abort" Invalid argument"                    endof
        WSAEMFILE           of true abort" Too many open sockets"               endof
        WSAEWOULDBLOCK      of true abort" Operation would block"               endof
        WSAEINPROGRESS      of true abort" Blocking operation in progress"      endof
        WSAENOTSOCK         of true abort" Operation on something that is not a socket" endof
        WSAEDESTADDRREQ     of true abort" Destination address required"        endof
        WSAEMSGSIZE         of true abort" Message too long"                    endof
        WSAEPROTONOSUPPORT  of true abort" Protocol not supported"              endof
        WSAEOPNOTSUPP       of true abort" Operation not supported"             endof
        WSAEAFNOSUPPORT     of true abort" Address family not supported"        endof
        WSAEADDRINUSE       of true abort" Address already in use"              endof
        WSAEADDRNOTAVAIL    of true abort" Cannot assign requested address"     endof
        WSAENETDOWN         of true abort" Network is down"                     endof
        WSAENETUNREACH      of true abort" Network is unreachable"              endof
        WSAECONNABORTED     of true abort" Software caused connection abort"    endof
        WSAECONNRESET       of true abort" Connection reset by peer"            endof
        WSAENOBUFS          of true abort" No buffer space available"           endof
        WSAEISCONN          of true abort" Socket is already connected"         endof
        WSAENOTCONN         of true abort" Socket is not connected"             endof
        WSAETIMEDOUT        of true abort" Connection timed out"                endof
        WSAECONNREFUSED     of true abort" Connection refused"                  endof
        WSAEHOSTUNREACH     of true abort" No route to host"                    endof
        WSAEPROCLIM         of true abort" Too many Winsock tasks"              endof
        WSASYSNOTREADY      of true abort" Network subsystem is unavailable"    endof
        WSAVERNOTSUPPORTED  of true abort" Winsock version not supported"       endof
        WSANOTINITIALISED   of true abort" WSAStartup not yet performed (ws-init)" endof
        WSAHOST_NOT_FOUND   of true abort" Host not found"                      endof
        ." Winsock error code " dup . cr
        true abort" Unknown Winsock error"
    endcase
  ;

\ Checks the int result of a function: aborts with the last Winsock error
\ if the result is SOCKET_ERROR, otherwise returns the sign extended result
: ws-check ( n -- n' )
    >int32 dup SOCKET_ERROR = if
        drop WSAGetLastError >int32 ws-abort
        true abort" Winsock call failed"
    then
  ;

\ Same check for a function returning a SOCKET
: sock-check ( s -- s )
    dup INVALID_SOCKET = if
        drop WSAGetLastError >int32 ws-abort
        true abort" Invalid socket"
    then
  ;


\ ==========================================================================
\ ** Library initialisation **
\ ==========================================================================

\ Starts Winsock 2.2. Must be called before any other socket word
: ws-init ( -- )
    WINSOCK_VERSION wsaData WSAStartup >int32 ws-abort
  ;

\ Releases Winsock
: ws-exit ( -- )
    WSACleanup ws-check drop
  ;


\ ==========================================================================
\ ** Addresses **
\ ==========================================================================

\ Converts a dotted string "127.0.0.1" into an IPv4 address (network order)
: z>ip ( zStr -- ip )
    inet_addr $ffffffff and
  ;

\ Converts an IPv4 address (network order) into a dotted zero terminated string
: ip>z ( ip -- zStr )
    inet_ntoa
  ;

\ Builds a sockaddr_in structure in the work area sa
\ ip is in network order (see z>ip), port in host order
: >sockaddr ( ip port -- addr )
    sa 16 0 fill
    AF_INET sa u16!
    dup 8 rshift $ff and sa 2 + c!
    $ff and sa 3 + c!
    sa 4 + u32!
    sa
  ;

\ Resolves a host name: returns first IPv4 address (network order) or 0
\ (uses hostent layout of Windows x64: h_addr_list at offset 24)
: host>ip ( zName -- ip|0 )
    gethostbyname dup 0= if exit then
    24 + @ dup 0= if exit then
    @ dup 0= if exit then
    u32@
  ;

\ Prints a zero terminated string
: ws-ztype ( zStr -- )
    begin  dup c@ ?dup while  emit 1+  repeat
    drop
  ;

\ Returns the local host name as a zero terminated string
: local-hostname ( -- zStr )
    hostBuf 255 gethostname ws-check drop
    hostBuf
  ;


\ ==========================================================================
\ ** TCP **
\ ==========================================================================

: tcp-socket ( -- sock )
    AF_INET SOCK_STREAM IPPROTO_TCP socket sock-check
  ;

\ connect sock to zIp:port, zIp is a dotted string
: sock-connect ( sock zIp port -- )
    >r z>ip r> >sockaddr 16 connect ws-check drop
  ;

\ bind sock on all local interfaces, port in host order
: sock-bind ( sock port -- )
    >r INADDR_ANY r> >sockaddr 16 bind ws-check drop
  ;

: sock-listen ( sock -- )
    SOMAXCONN listen ws-check drop
  ;

\ wait for an incoming connection, returns the new socket
: sock-accept ( sock -- newSock )
    0 0 accept sock-check
  ;

\ send at most len bytes, returns the number of bytes really sent
: sock-send ( sock addr len -- n )
    0 send ws-check
  ;

\ receive at most len bytes, returns 0 when the peer closed the connection
: sock-recv ( sock addr len -- n )
    0 recv ws-check
  ;

\ send the whole buffer
: sock-send-all ( sock addr len -- )
    rot sendSock !
    begin
        dup 0>
    while
        2dup sendSock @ -rot sock-send      \ addr len n
        tuck - >r + r>
    repeat
    2drop
  ;

\ send CR LF
: sock-crlf ( sock -- )
    crlf 2 sock-send-all
  ;

: sock-shutdown ( sock how -- )
    shutdown ws-check drop
  ;

: sock-close ( sock -- )
    closesocket ws-check drop
  ;


\ ==========================================================================
\ ** Socket options and modes **
\ ==========================================================================

\ set an integer socket option
: sock-opt! ( sock level optname value -- )
    optval !  optval 4 setsockopt ws-check drop
  ;

: sock-reuse ( sock -- )
    SOL_SOCKET SO_REUSEADDR 1 sock-opt!
  ;

: sock-nodelay ( sock -- )
    IPPROTO_TCP TCP_NODELAY 1 sock-opt!
  ;

\ receive and send time-out in milliseconds
: sock-timeout ( sock ms -- )
    >r dup SOL_SOCKET SO_RCVTIMEO r@ sock-opt!
    SOL_SOCKET SO_SNDTIMEO r> sock-opt!
  ;

\ flag = true: non blocking mode, flag = false: blocking mode
: sock-nonblock ( sock flag -- )
    optval !  FIONBIO optval ioctlsocket ws-check drop
  ;

\ number of bytes that can be read without blocking
: sock-avail ( sock -- n )
    0 optval !  FIONREAD optval ioctlsocket ws-check drop
    optval @ $ffffffff and
  ;

\ waits until one of the events happens or ms elapsed: true if socket is ready
: sock-poll ( sock events ms -- flag )
    >r  pollfd 16 0 fill
    pollfd 8 + u16!
    pollfd !
    pollfd 1 r> WSAPoll ws-check 0>
  ;

: sock-readable? ( sock ms -- flag )   POLLIN swap sock-poll ;
: sock-writable? ( sock ms -- flag )   POLLOUT swap sock-poll ;


\ ==========================================================================
\ ** UDP **
\ ==========================================================================

: udp-socket ( -- sock )
    AF_INET SOCK_DGRAM IPPROTO_UDP socket sock-check
  ;

\ send a datagram to zIp:port
: udp-send ( sock addr len zIp port -- n )
    >r z>ip r> >sockaddr 0 swap 16 sendto ws-check
  ;

\ receive a datagram, the sender address is kept in the work area sa
: udp-recv ( sock addr len -- n )
    0 sa 16 fromLen !  fromLen recvfrom ws-check
  ;

\ sender of the last datagram received by udp-recv
: udp-from ( -- ip port )
    sa 4 + u32@
    sa 2 + c@ 8 lshift sa 3 + c@ or
  ;


\ ==========================================================================
\ ** Example (copy and paste after loading the file) **
\ ==========================================================================
\ winsock
\ ws-init
\ tcp-socket constant s
\ s z" 127.0.0.1" 80 sock-connect
\ s s" GET / HTTP/1.0" sock-send-all  s sock-crlf  s sock-crlf
\ create buf 1024 allot
\ s buf 1024 sock-recv  buf swap type
\ s sock-close
\ ws-exit


<EOF>

Le fichier contient :

Constantes : familles d'adresses, types de sockets, options, WSAPoll et codes d'erreur WSAE*.
Imports de la DLL : WSAStartup, socket, bind, listen, accept, connect, send, recv, sendto, recvfrom, setsockopt, ioctlsocket, WSAPoll, select, gethostbyname, getaddrinfo, etc.
Gestion d'erreurs : ws-abort, ws-check et sock-check.
Mots de haut niveau :
démarrage et arrêt : ws-init, ws-exit ;
TCP : tcp-socket, sock-connect, sock-bind, sock-listen, sock-accept, sock-send-all, sock-recv, sock-close ;
UDP : udp-socket, udp-send, udp-recv ;
options et attente : sock-nonblock, sock-timeout, sock-readable? ;
adresses : host>ip, htons.
Un exemple commenté en fin de fichier.

Quelques hypothèses à vérifier chez vous :

Cellules 64 bits : j'ai supposé des cellules 64 bits, donc SOCKET sur 64 bits et des offsets de structures x64 (par exemple pour hostent dans host>ip).
Résultats int : un -1 revient sous la forme $ffffffff dans une cellule 64 bits. >int32 rétablit le signe avant de comparer à SOCKET_ERROR.
Mots supposés présents : case, abort", fill, rshift, lshift, tuck et 2dup. J'ai écrit true abort" …" dans les branches de ws-abort, pour ne pas dépendre du contenu de la pile.
Pas de l!/w! : les écritures d'entiers passent par c!, dans u16! et u32!.
getaddrinfo : il est importé, mais la structure ADDRINFO n'est pas encapsulée.

Un test simple pour commencer : winsock ws-init local-hostname ws-ztype cr ws-exit.
