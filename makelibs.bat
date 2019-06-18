nuget restore MonoGame.Framework\MonoGame.Framework.AndroidForms.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.AndroidForms.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.iOS.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.iOSForms.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.Windows.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.WPFForms.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.WPF.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.WindowsUniversal.csproj -SolutionDir .


msbuild MonoGame.Framework\MonoGame.Framework.Android.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.AndroidForms.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.iOS.csproj /p:Configuration=Release /p:Platform=iPhone /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.iOSForms.csproj /p:Configuration=Release /p:Platform=iPhone /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.Windows.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.WPFForms.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.WPF.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.WindowsUniversal.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild

IF NOT exist outputlibs mkdir outputlibs
IF NOT exist outputlibs\Android mkdir outputlibs\Android
IF NOT exist outputlibs\AndroidForms mkdir outputlibs\AndroidForms
IF NOT exist outputlibs\iOS mkdir outputlibs\iOS
IF NOT exist outputlibs\iOSForms mkdir outputlibs\iOSForms
IF NOT exist outputlibs\Windows mkdir outputlibs\Windows
IF NOT exist outputlibs\WPFForms mkdir outputlibs\WPFForms
IF NOT exist outputlibs\WPF mkdir outputlibs\WPF
IF NOT exist outputlibs\Portable mkdir outputlibs\Portable
IF NOT exist outputlibs\UWP mkdir outputlibs\UWP

ThirdParty\Dependencies\Piranha\Piranha.exe make-portable-skeleton -i MonoGame.Framework\bin\Windows\AnyCPU\Release\MonoGame.Framework.dll -o outputlibs\Portable\MonoGame.Framework.dll -p ".NETPortable,Version=v4.0,Profile=Profile328"

copy MonoGame.Framework\bin\Android\AnyCPU\Release\MonoGame.Framework.dll outputlibs\Android
copy MonoGame.Framework\bin\AndroidForms\AnyCPU\Release\MonoGame.Framework.dll outputlibs\AndroidForms
copy MonoGame.Framework\bin\iOS\iPhone\Release\MonoGame.Framework.dll outputlibs\iOS
copy MonoGame.Framework\bin\iOSForms\iPhone\Release\MonoGame.Framework.dll outputlibs\iOSForms
copy MonoGame.Framework\bin\Windows\AnyCPU\Release\MonoGame.Framework.dll outputlibs\Windows
copy MonoGame.Framework\bin\WPFForms\AnyCPU\Release\MonoGame.Framework.dll outputlibs\WPFForms
copy MonoGame.Framework\bin\WPF\AnyCPU\Release\MonoGame.Framework.dll outputlibs\WPF
copy MonoGame.Framework\bin\WindowsUniversal\AnyCPU\Release\MonoGame.Framework.dll outputlibs\UWP


