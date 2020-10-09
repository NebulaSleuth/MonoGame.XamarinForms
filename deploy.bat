@echo off

setlocal
set _path=%1

CALL :dequote _path
goto runme

:DeQuote
for /f "delims=" %%A in ('echo %%%1%%') do set %1=%%~A
Goto :eof

:runme
IF NOT exist "%_path%\MonoGame\Release" mkdir "%_path%\MonoGame\Release"
IF NOT exist "%_path%\MonoGame\Release\Android" mkdir "%_path%\MonoGame\Release\Android"
IF NOT exist "%_path%\MonoGame\Release\AndroidForms" mkdir "%_path%\MonoGame\Release\AndroidForms"
IF NOT exist "%_path%\MonoGame\Release\iOS" mkdir "%_path%\MonoGame\Release\iOS"
IF NOT exist "%_path%\MonoGame\Release\iOSForms" mkdir "%_path%\MonoGame\Release\iOSForms"
IF NOT exist "%_path%\MonoGame\Release\Windows" mkdir "%_path%\MonoGame\Release\Windows"
IF NOT exist "%_path%\MonoGame\Release\WindowsGL" mkdir "%_path%\MonoGame\Release\WindowsGL"
IF NOT exist "%_path%\MonoGame\Release\WPF" mkdir "%_path%\MonoGame\Release\WPF"
IF NOT exist "%_path%\MonoGame\Release\WPFForms" mkdir "%_path%\MonoGame\Release\WPFForms"
IF NOT exist "%_path%\MonoGame\Release\WPFFormsCore" mkdir "%_path%\MonoGame\Release\WPFFormsCore"
IF NOT exist "%_path%\MonoGame\Release\GTK" mkdir "%_path%\MonoGame\Release\GTK"
IF NOT exist "%_path%\MonoGame\Release\UWP" mkdir "%_path%\MonoGame\Release\UWP"

copy outputlibs\Release\Android\MonoGame.Framework.dll "%_path%\MonoGame\Release\Android" /Y
copy outputlibs\Release\AndroidForms\MonoGame.Framework.dll "%_path%\MonoGame\Release\AndroidForms" /Y
copy outputlibs\Release\iOS\MonoGame.Framework.dll "%_path%\MonoGame\Release\iOS" /Y
copy outputlibs\Release\iOSForms\MonoGame.Framework.dll "%_path%\MonoGame\Release\iOSForms" /Y
copy outputlibs\Release\Windows\MonoGame.Framework.dll "%_path%\MonoGame\Release\Windows" /Y
copy outputlibs\Release\WindowsGL\MonoGame.Framework.dll "%_path%\MonoGame\Release\WindowsGL" /Y
copy outputlibs\Release\Portable\MonoGame.Framework.dll "%_path%\MonoGame\Release\Portable" /Y
copy outputlibs\Release\WPFForms\MonoGame.Framework.dll "%_path%\MonoGame\Release\WPFForms" /Y
copy outputlibs\Release\WPFFormsCore\MonoGame.Framework.dll "%_path%\MonoGame\Release\WPFFormsCore" /Y
copy outputlibs\Release\WPF\MonoGame.Framework.dll "%_path%\MonoGame\Release\WPF" /Y
copy outputlibs\Release\GTK\MonoGame.Framework.dll "%_path%\MonoGame\Release\GTK" /Y
copy outputlibs\Release\UWP\MonoGame.Framework.dll "%_path%\MonoGame\Release\UWP" /Y
copy outputlibs\Release\Windows\MonoGame.Framework.Content.Pipeline.dll "%_path%\MonoGame\Release\Windows" /Y

