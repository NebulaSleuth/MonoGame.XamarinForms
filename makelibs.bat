@echo off
IF NOT exist outputlibs mkdir outputlibs
IF NOT exist outputlibs\Release mkdir outputlibs\Release
IF NOT exist outputlibs\Release\Android mkdir outputlibs\Release\Android
IF NOT exist outputlibs\Release\AndroidForms mkdir outputlibs\Release\AndroidForms
IF NOT exist outputlibs\Release\GTK mkdir outputlibs\Release\GTK
IF NOT exist outputlibs\Release\iOS mkdir outputlibs\Release\iOS
IF NOT exist outputlibs\Release\iOSForms mkdir outputlibs\Release\iOSForms
IF NOT exist outputlibs\Release\Portable mkdir outputlibs\Release\Portable
IF NOT exist outputlibs\Release\UWP mkdir outputlibs\Release\UWP
IF NOT exist outputlibs\Release\Windows mkdir outputlibs\Release\Windows
IF NOT exist outputlibs\Release\WindowsGL mkdir outputlibs\Release\WindowsGL
IF NOT exist outputlibs\Release\WPF mkdir outputlibs\Release\WPF
IF NOT exist outputlibs\Release\WPFForms mkdir outputlibs\Release\WPFForms
IF NOT exist outputlibs\Release\WPFFormsCore mkdir outputlibs\Release\WPFFormsCore

rem IF NOT exist outputlibs\ReleaseDeploy mkdir outputlibs\ReleaseDeploy

call clean.bat
nuget restore MonoGame.Framework\MonoGame.Framework.Android.csproj -SolutionDir .
msbuild MonoGame.Framework\MonoGame.Framework.Android.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
if not %errorlevel% == 0 goto error
IF NOT exist outputlibs\Release\Android mkdir outputlibs\Release\Android
rem IF NOT exist outputlibs\ReleaseDeploy\Android mkdir outputlibs\ReleaseDeploy\Android
copy MonoGame.Framework\bin\Android\AnyCPU\Release\MonoGame.Framework.dll outputlibs\Release\Android /Y

call clean.bat
nuget restore MonoGame.Framework\MonoGame.Framework.AndroidForms.csproj -SolutionDir .
msbuild MonoGame.Framework\MonoGame.Framework.AndroidForms.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
if not %errorlevel% == 0 goto error
IF NOT exist outputlibs\Release\AndroidForms mkdir outputlibs\Release\AndroidForms
rem IF NOT exist outputlibs\ReleaseDeploy\AndroidForms mkdir outputlibs\ReleaseDeploy\AndroidForms
copy MonoGame.Framework\bin\AndroidForms\AnyCPU\Release\MonoGame.Framework.dll outputlibs\Release\AndroidForms /Y

call clean.bat
nuget restore MonoGame.Framework\MonoGame.Framework.Windows.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.WindowsGL.csproj -SolutionDir .
nuget restore MonoGame.Framework.Content.Pipeline\MonoGame.Framework.Content.Pipeline.Windows.csproj -SolutionDir .
msbuild MonoGame.Framework\MonoGame.Framework.Windows.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
if not %errorlevel% == 0 goto error
msbuild MonoGame.Framework\MonoGame.Framework.WindowsGL.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
if not %errorlevel% == 0 goto error
msbuild MonoGame.Framework.Content.Pipeline\MonoGame.Framework.Content.Pipeline.Windows.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
if not %errorlevel% == 0 goto error
IF NOT exist outputlibs\Release\Windows mkdir outputlibs\Release\Windows
IF NOT exist outputlibs\Release\WindowsGL mkdir outputlibs\Release\WindowsGL
IF NOT exist outputlibs\Release\Portable mkdir outputlibs\Release\Portable
copy MonoGame.Framework\bin\Windows\AnyCPU\Release\MonoGame.Framework.dll outputlibs\Release\Windows /Y
copy MonoGame.Framework\bin\WindowsGL\AnyCPU\Release\MonoGame.Framework.dll outputlibs\Release\WindowsGL /Y
copy MonoGame.Framework.Content.Pipeline\bin\Windows\AnyCPU\Release\MonoGame.Framework.Content.Pipeline.dll outputlibs\Release\Windows /Y

ThirdParty\Dependencies\Piranha\Piranha.exe make-portable-skeleton -i MonoGame.Framework\bin\Windows\AnyCPU\Release\MonoGame.Framework.dll -o outputlibs\Release\Portable\MonoGame.Framework.dll -p ".NETPortable,Version=v4.0,Profile=Profile328"
if not %errorlevel% == 0 goto error
ThirdParty\Dependencies\Piranha\Piranha.exe make-portable-skeleton -i MonoGame.Framework.Content.Pipeline\bin\Windows\AnyCPU\Release\MonoGame.Framework.Content.Pipeline.dll -o outputlibs\Release\Portable\MonoGame.Framework.Content.Pipeline.dll -p ".NETPortable,Version=v4.0,Profile=Profile328"
if not %errorlevel% == 0 goto error
copy outputlibs\Release\Portable\MonoGame.Framework.dll outputlibs\Release\Portable\MonoGame.Framework.dll /Y
copy outputlibs\Release\Portable\MonoGame.Framework.Content.Pipeline.dll outputlibs\Release\Portable\MonoGame.Framework.Content.Pipeline.dll /Y

call clean.bat
nuget restore MonoGame.Framework\MonoGame.Framework.iOS.csproj -SolutionDir .
msbuild MonoGame.Framework\MonoGame.Framework.iOS.csproj /p:Configuration=Release /p:Platform=iPhone /t:Clean,Rebuild
if not %errorlevel% == 0 goto error
IF NOT exist outputlibs\Release\iOS mkdir outputlibs\Release\iOS
copy MonoGame.Framework\bin\iOS\iPhone\Release\MonoGame.Framework.dll outputlibs\Release\iOS /Y

call clean.bat
nuget restore MonoGame.Framework\MonoGame.Framework.iOSForms.csproj -SolutionDir .
msbuild MonoGame.Framework\MonoGame.Framework.iOSForms.csproj /p:Configuration=Release /p:Platform=iPhone /t:Clean,Rebuild
if not %errorlevel% == 0 goto error
IF NOT exist outputlibs\Release\iOSForms mkdir outputlibs\Release\iOSForms
copy MonoGame.Framework\bin\iOSForms\iPhone\Release\MonoGame.Framework.dll outputlibs\Release\iOSForms /Y

call clean.bat
nuget restore MonoGame.Framework\MonoGame.Framework.WPF.csproj -SolutionDir .
msbuild MonoGame.Framework\MonoGame.Framework.WPF.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
if not %errorlevel% == 0 goto error
IF NOT exist outputlibs\Release\WPF mkdir outputlibs\Release\WPF
copy MonoGame.Framework\bin\WPF\AnyCPU\Release\MonoGame.Framework.dll outputlibs\Release\WPF /Y

call clean.bat
nuget restore MonoGame.Framework\MonoGame.Framework.WPFForms.csproj -SolutionDir .
msbuild MonoGame.Framework\MonoGame.Framework.WPFForms.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
if not %errorlevel% == 0 goto error
IF NOT exist outputlibs\Release\WPFForms mkdir outputlibs\Release\WPFForms
copy MonoGame.Framework\bin\WPFForms\AnyCPU\Release\MonoGame.Framework.dll outputlibs\Release\WPFForms /Y

call clean.bat
nuget restore MonoGame.Framework\MonoGame.Framework.WPFFormsCore.csproj -SolutionDir .
msbuild MonoGame.Framework\MonoGame.Framework.WPFFormsCore.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
if not %errorlevel% == 0 goto error
IF NOT exist outputlibs\Release\WPFFormsCore mkdir outputlibs\Release\WPF
copy MonoGame.Framework\bin\WPFFormsCore\AnyCPU\Release\MonoGame.Framework.dll outputlibs\Release\WPFFormsCore /Y

call clean.bat
nuget restore MonoGame.Framework\MonoGame.Framework.WindowsUniversal.csproj -SolutionDir .
msbuild MonoGame.Framework\MonoGame.Framework.WindowsUniversal.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
if not %errorlevel% == 0 goto error
IF NOT exist outputlibs\Release\UWP mkdir outputlibs\Release\WindowsUniversal
copy MonoGame.Framework\bin\WindowsUniversal\AnyCPU\Release\MonoGame.Framework.dll outputlibs\Release\UWP /Y

call clean.bat
nuget restore MonoGame.Framework\MonoGame.Framework.GTK.csproj -SolutionDir .
msbuild MonoGame.Framework\MonoGame.Framework.GTK.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
if not %errorlevel% == 0 goto error
IF NOT exist outputlibs\Release\GTK mkdir outputlibs\Release\GTK
copy MonoGame.Framework\bin\GTK\AnyCPU\Release\MonoGame.Framework.dll outputlibs\Release\GTK /Y

goto :EOF

error:
echo *** FAILED TO BUILD ***
