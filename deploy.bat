setlocal
set _path=%1

CALL :dequote _path
goto runme

:DeQuote
for /f "delims=" %%A in ('echo %%%1%%') do set %1=%%~A
Goto :eof

:runme
IF NOT exist "%_path%\MonoGame\Debug" mkdir "%_path%\MonoGame\Debug"
IF NOT exist "%_path%\MonoGame\Debug\Andorid" mkdir "%_path%\MonoGame\Release"
IF NOT exist "%_path%\MonoGame\Debug\Android" mkdir outputlibs\Release\Android
IF NOT exist "%_path%\MonoGame\Debug\AndroidForms" mkdir outputlibs\Release\AndroidForms
IF NOT exist "%_path%\MonoGame\Debug\iOS" mkdir outputlibs\Release\iOS
IF NOT exist "%_path%\MonoGame\Debug\iOSForms" mkdir outputlibs\Release\iOSForms
IF NOT exist "%_path%\MonoGame\Debug\Windows" mkdir outputlibs\Release\Windows
IF NOT exist "%_path%\MonoGame\Debug\WPFForms" mkdir outputlibs\Release\WPFForms
IF NOT exist "%_path%\MonoGame\Debug\WPF" mkdir outputlibs\Release\WPF
IF NOT exist "%_path%\MonoGame\Debug\GTK" mkdir outputlibs\Release\GTK
IF NOT exist "%_path%\MonoGame\Debug\Portable" mkdir outputlibs\Release\Portable
IF NOT exist "%_path%\MonoGame\Debug\UWP" mkdir outputlibs\Release\UWP

IF NOT exist "%_path%\MonoGame\Release" mkdir "%_path%\MonoGame\Release"
IF NOT exist "%_path%\MonoGame\Release\Andorid" mkdir "%_path%\MonoGame\Release"
IF NOT exist "%_path%\MonoGame\Release\Android" mkdir outputlibs\Release\Android
IF NOT exist "%_path%\MonoGame\Release\AndroidForms" mkdir outputlibs\Release\AndroidForms
IF NOT exist "%_path%\MonoGame\Release\iOS" mkdir outputlibs\Release\iOS
IF NOT exist "%_path%\MonoGame\Release\iOSForms" mkdir outputlibs\Release\iOSForms
IF NOT exist "%_path%\MonoGame\Release\Windows" mkdir outputlibs\Release\Windows
IF NOT exist "%_path%\MonoGame\Release\WPFForms" mkdir outputlibs\Release\WPFForms
IF NOT exist "%_path%\MonoGame\Release\WPF" mkdir outputlibs\Release\WPF
IF NOT exist "%_path%\MonoGame\Release\GTK" mkdir outputlibs\Release\GTK
IF NOT exist "%_path%\MonoGame\Release\Portable" mkdir outputlibs\Release\Portable
IF NOT exist "%_path%\MonoGame\Release\UWP" mkdir outputlibs\Release\UWP

copy MonoGame.Framework\bin\Android\AnyCPU\Release\MonoGame.Framework.dll "%_path%\MonoGame\Release\Android" /Y
copy MonoGame.Framework\bin\AndroidForms\AnyCPU\Release\MonoGame.Framework.dll "%_path%\MonoGame\Release\AndroidForms" /Y
copy MonoGame.Framework\bin\iOS\iPhone\Release\MonoGame.Framework.dll "%_path%\MonoGame\Release\iOS" /Y
copy MonoGame.Framework\bin\iOSForms\iPhone\Release\MonoGame.Framework.dll "%_path%\MonoGame\Release\iOSForms" /Y
copy MonoGame.Framework\bin\Windows\AnyCPU\Release\MonoGame.Framework.dll "%_path%\MonoGame\Release\Windows" /Y
copy MonoGame.Framework\bin\WPFForms\AnyCPU\Release\MonoGame.Framework.dll "%_path%\MonoGame\Release\WPFForms" /Y
copy MonoGame.Framework\bin\WPF\AnyCPU\Release\MonoGame.Framework.dll "%_path%\MonoGame\Release\WPF" /Y
copy MonoGame.Framework\bin\GTK\AnyCPU\Release\MonoGame.Framework.dll "%_path%\MonoGame\Release\GTK" /Y
copy MonoGame.Framework\bin\WindowsUniversal\AnyCPU\Release\MonoGame.Framework.dll "%_path%\MonoGame\Release\UWP" /Y
copy MonoGame.Framework.Content.Pipeline\bin\Windows\AnyCPU\Release\MonoGame.Framework.Content.Pipeline.dll "%_path%\MonoGame\Release\Windows" /Y

copy MonoGame.Framework\bin\Android\AnyCPU\Release\MonoGame.Framework.pdb "%_path%\MonoGame\Release\Android" /Y
copy MonoGame.Framework\bin\AndroidForms\AnyCPU\Release\MonoGame.Framework.pdb "%_path%\MonoGame\Release\AndroidForms" /Y
copy MonoGame.Framework\bin\iOS\iPhone\Release\MonoGame.Framework.pdb "%_path%\MonoGame\Release\iOS" /Y
copy MonoGame.Framework\bin\iOSForms\iPhone\Release\MonoGame.Framework.pdb "%_path%\MonoGame\Release\iOSForms" /Y
copy MonoGame.Framework\bin\Windows\AnyCPU\Release\MonoGame.Framework.pdb "%_path%\MonoGame\Release\Windows" /Y
copy MonoGame.Framework\bin\WPFForms\AnyCPU\Release\MonoGame.Framework.pdb "%_path%\MonoGame\Release\WPFForms" /Y
copy MonoGame.Framework\bin\WPF\AnyCPU\Release\MonoGame.Framework.pdb "%_path%\MonoGame\Release\WPF" /Y
copy MonoGame.Framework\bin\GTK\AnyCPU\Release\MonoGame.Framework.pdb "%_path%\MonoGame\Release\GTK" /Y
copy MonoGame.Framework\bin\WindowsUniversal\AnyCPU\Release\MonoGame.Framework.pdb "%_path%\MonoGame\Release\UWP" /Y

copy MonoGame.Framework\bin\Android\AnyCPU\Debug\MonoGame.Framework.dll "%_path%\MonoGame\Debug\Android" /Y
copy MonoGame.Framework\bin\AndroidForms\AnyCPU\Debug\MonoGame.Framework.dll "%_path%\MonoGame\Debug\AndroidForms" /Y
copy MonoGame.Framework\bin\iOS\iPhone\Debug\MonoGame.Framework.dll "%_path%\MonoGame\Debug\iOS" /Y
copy MonoGame.Framework\bin\iOSForms\iPhone\Debug\MonoGame.Framework.dll "%_path%\MonoGame\Debug\iOSForms" /Y
copy MonoGame.Framework\bin\Windows\AnyCPU\Debug\MonoGame.Framework.dll "%_path%\MonoGame\Debug\Windows" /Y
copy MonoGame.Framework\bin\WPFForms\AnyCPU\Debug\MonoGame.Framework.dll "%_path%\MonoGame\Debug\WPFForms" /Y
copy MonoGame.Framework\bin\WPF\AnyCPU\Debug\MonoGame.Framework.dll "%_path%\MonoGame\Debug\WPF" /Y
copy MonoGame.Framework\bin\GTK\AnyCPU\Debug\MonoGame.Framework.dll "%_path%\MonoGame\Debug\GTK" /Y
copy MonoGame.Framework\bin\WindowsUniversal\AnyCPU\Debug\MonoGame.Framework.dll "%_path%\MonoGame\Debug\UWP" /Y
copy MonoGame.Framework.Content.Pipeline\bin\Windows\AnyCPU\Debug\MonoGame.Framework.Content.Pipeline.dll "%_path%\MonoGame\Debug\Windows" /Y

copy MonoGame.Framework\bin\Android\AnyCPU\Debug\MonoGame.Framework.pdb "%_path%\MonoGame\Debug\Android" /Y
copy MonoGame.Framework\bin\AndroidForms\AnyCPU\Debug\MonoGame.Framework.pdb "%_path%\MonoGame\Debug\AndroidForms" /Y
copy MonoGame.Framework\bin\iOS\iPhone\Debug\MonoGame.Framework.dll "%_path%\MonoGame\Debug\iOS" /Y
copy MonoGame.Framework\bin\iOSForms\iPhone\Debug\MonoGame.Framework.pdb "%_path%\MonoGame\Debug\iOSForms" /Y
copy MonoGame.Framework\bin\Windows\AnyCPU\Debug\MonoGame.Framework.pdb "%_path%\MonoGame\Debug\Windows" /Y
copy MonoGame.Framework\bin\WPFForms\AnyCPU\Debug\MonoGame.Framework.pdb "%_path%\MonoGame\Debug\WPFForms" /Y
copy MonoGame.Framework\bin\WPF\AnyCPU\Debug\MonoGame.Framework.pdb "%_path%\MonoGame\Debug\WPF" /Y
copy MonoGame.Framework\bin\GTK\AnyCPU\Debug\MonoGame.Framework.pdb "%_path%\MonoGame\Debug\GTK" /Y
copy MonoGame.Framework\bin\WindowsUniversal\AnyCPU\Debug\MonoGame.Framework.pdb "%_path%\MonoGame\Debug\UWP" /Y

