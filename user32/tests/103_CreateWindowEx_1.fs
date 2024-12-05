


\ à éplucher:
\ https://github.com/microsoft/Windows-classic-samples


windows also 
graphics internals 

\ UINT GetDlgItemTextA(
\   [in]  HWND  hDlg,
\   [in]  int   nIDDlgItem,
\   [out] LPSTR lpString,
\   [in]  int   cchMax
\ );

\ Destroys the specified window @TODO: semble pas fonctionner
z" GetDlgItemTextA"  4 User32 GetDlgItemTextA   ( hdlg nIDDlgItel lpString cchMax -- n|0 )



100 constant ID_EDIT  \ Identifiant du contrôle d'édition

$00000030 constant MB_ICONEXCLAMATION
$00000030 constant MB_ICONWARNING
$00000040 constant MB_ICONINFORMATION
$00000040 constant MB_ICONASTERISK
$00000020 constant MB_ICONQUESTION
$00000010 constant MB_ICONSTOP
$00000010 constant MB_ICONERROR
$00000010 constant MB_ICONHAND



\ HWND CreateWindowExA(
\   [in]           DWORD     dwExStyle,
\   [in, optional] LPCSTR    lpClassName,
\   [in, optional] LPCSTR    lpWindowName,
\   [in]           DWORD     dwStyle,
\   [in]           int       X,
\   [in]           int       Y,
\   [in]           int       nWidth,
\   [in]           int       nHeight,
\   [in, optional] HWND      hWndParent,
\   [in, optional] HMENU     hMenu,
\   [in, optional] HINSTANCE hInstance,
\   [in, optional] LPVOID    lpParam
\ );

\             HWND hEdit = CreateWindowEx(WS_EX_CLIENTEDGE, "EDIT", "Texte par défaut", 
\                                         WS_CHILD | WS_VISIBLE | ES_AUTOHSCROLL, 
\                                         10, 10, 200, 20, hWnd, NULL, GetModuleHandle(NULL), NULL);

$00000200 constant WS_EX_CLIENTEDGE  \ The window has a border with a sunken edge

0 value hwInput

: show-input-box 
    WS_EX_CLIENTEDGE  \ dwExStyle
    NULL              \ lpClassName - optionnal
    z" Saisissez votre texte ici"  \ lpWindowName
    WS_CHILD WS_VISIBLE or            \ dwStyle
    10 10           \ X Y
    200 20         \ nWidth nHeight
    hwnd            \ hWndParent
    NULL            \ hMenu
    NULL            \ hInstance
    NULL            \ lpParam
\     MB_OK MB_ICONINFORMATION or ID_EDIT CreateWindowExA  \ Crée la boîte de dialogue
    CreateWindowExA to hwInput \ Crée la boîte de dialogue
    hwInput SW_SHOW ShowWindow  ;  \ Affiche la boîte de dialogue

: get-input 
    100 GetDlgItemText  \ Récupère le texte du contrôle d'édition
    s" Vous avez saisi : " type ;  \ Affiche le texte saisi

: run 
    show-input-box 
    get-input ;