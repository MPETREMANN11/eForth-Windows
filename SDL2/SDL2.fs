\ *********************************************************************
\ SDL2 / Simple DirectMedia Player for eForth
\    Filename:      SDL2.fs
\    Date:          19 oct 2024
\    Updated:       19 oct 2024
\    File Version:  1.0
\    Forth:         eFORTH Windows
\    Author:        Marc PETREMANN
\    GNU General Public License
\ *********************************************************************

\ Source: https://wiki.libsdl.org/SDL2/CategoryAPI 

vocabulary SDL2
SDL2 definitions  windows
z" SDL2.dll" dll SDL2
SDL2

4096 constant SDL_MAX_LOG_MESSAGE       \ maximum size of a log message prior to SDL 2.0.24



\ compute arc cosine of x                               @ERROR: dont- work
z" SDL_acos"                1 SDL2 acos ( n -- n ) 

\ returns the SDL_AudioStatus of the audio device opened by SDL_OpenAudio
z" SDL_GetAudioStatus"      0 SDL2 GetAudioStatus ( -- status ) 




\ get the directory where the application was run from
z" SDL_GetBasePath"         0 SDL2 GetBasePath ( -- strz ) 

\ returns the total number of logical CPU cores
z" SDL_GetCPUCount"         0 SDL2 GetCPUCount ( -- n ) 

\ returns the active cursor or NULL if there is no mouse
z" SDL_GetCursor"           0 SDL2 GetCursor ( -- n ) 



\ structure that defines a point - @ERROR: don-t work
\ z" SDL_Point"               0 SDL2 Point ( x y -- ) 

