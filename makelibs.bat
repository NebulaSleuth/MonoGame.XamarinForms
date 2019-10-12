nuget restore MonoGame.Framework\MonoGame.Framework.Android.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.AndroidForms.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.iOS.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.iOSForms.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.Windows.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.WPFForms.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.WPF.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.WindowsUniversal.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.GTK.csproj -SolutionDir .

nuget restore MonoGame.Framework.Content.Pipeline\MonoGame.Framework.Content.Pipeline.Windows.csproj -SolutionDir .

msbuild MonoGame.Framework\MonoGame.Framework.Android.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.AndroidForms.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.iOS.csproj /p:Configuration=Release /p:Platform=iPhone /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.iOSForms.csproj /p:Configuration=Release /p:Platform=iPhone /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.Windows.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.WPFForms.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.WPF.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.WindowsUniversal.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.GTK.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework.Content.Pipeline\MonoGame.Framework.Content.Pipeline.Windows.csproj /p:Configuration=Release /p:Platform=AnyCPU /t:Clean,Rebuild


msbuild MonoGame.Framework\MonoGame.Framework.Android.csproj /p:Configuration=Debug /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.AndroidForms.csproj /p:Configuration=Debug /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.iOS.csproj /p:Configuration=Debug /p:Platform=iPhone /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.iOSForms.csproj /p:Configuration=Debug /p:Platform=iPhone /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.Windows.csproj /p:Configuration=Debug /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.WPFForms.csproj /p:Configuration=Debug /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.WPF.csproj /p:Configuration=Debug /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.WindowsUniversal.csproj /p:Configuration=Debug /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.GTK.csproj /p:Configuration=Debug /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework.Content.Pipeline\MonoGame.Framework.Content.Pipeline.Windows.csproj /p:Configuration=Debug /p:Platform=AnyCPU /t:Clean,Rebuild


IF NOT exist outputlibs mkdir outputlibs
IF NOT exist outputlibs\Release mkdir outputlibs\Release
IF NOT exist outputlibs\Release\Android mkdir outputlibs\Release\Android
IF NOT exist outputlibs\Release\AndroidForms mkdir outputlibs\Release\AndroidForms
IF NOT exist outputlibs\Release\iOS mkdir outputlibs\Release\iOS
IF NOT exist outputlibs\Release\iOSForms mkdir outputlibs\Release\iOSForms
IF NOT exist outputlibs\Release\Windows mkdir outputlibs\Release\Windows
IF NOT exist outputlibs\Release\WPFForms mkdir outputlibs\Release\WPFForms
IF NOT exist outputlibs\Release\WPF mkdir outputlibs\Release\WPF
IF NOT exist outputlibs\Release\GTK mkdir outputlibs\Release\GTK
IF NOT exist outputlibs\Release\Portable mkdir outputlibs\Release\Portable
IF NOT exist outputlibs\Release\UWP mkdir outputlibs\Release\UWP

IF NOT exist outputlibs\ReleaseDeploy mkdir outputlibs\ReleaseDeploy
IF NOT exist outputlibs\ReleaseDeploy\Android mkdir outputlibs\ReleaseDeploy\Android
IF NOT exist outputlibs\ReleaseDeploy\AndroidForms mkdir outputlibs\ReleaseDeploy\AndroidForms
IF NOT exist outputlibs\ReleaseDeploy\iOS mkdir outputlibs\ReleaseDeploy\iOS
IF NOT exist outputlibs\ReleaseDeploy\iOSForms mkdir outputlibs\ReleaseDeploy\iOSForms
IF NOT exist outputlibs\ReleaseDeploy\Windows mkdir outputlibs\ReleaseDeploy\Windows
IF NOT exist outputlibs\ReleaseDeploy\WPFForms mkdir outputlibs\ReleaseDeploy\WPFForms
IF NOT exist outputlibs\ReleaseDeploy\WPF mkdir outputlibs\ReleaseDeploy\WPF
IF NOT exist outputlibs\ReleaseDeploy\GTK mkdir outputlibs\ReleaseDeploy\GTK
IF NOT exist outputlibs\ReleaseDeploy\Portable mkdir outputlibs\ReleaseDeploy\Portable
IF NOT exist outputlibs\ReleaseDeploy\UWP mkdir outputlibs\ReleaseDeploy\UWP

IF NOT exist outputlibs\Debug mkdir outputlibs\Debug
IF NOT exist outputlibs\Debug\Android mkdir outputlibs\Debug\Android
IF NOT exist outputlibs\Debug\AndroidForms mkdir outputlibs\Debug\AndroidForms
IF NOT exist outputlibs\Debug\iOS mkdir outputlibs\Debug\iOS
IF NOT exist outputlibs\Debug\iOSForms mkdir outputlibs\Debug\iOSForms
IF NOT exist outputlibs\Debug\Windows mkdir outputlibs\Debug\Windows
IF NOT exist outputlibs\Debug\WPFForms mkdir outputlibs\Debug\WPFForms
IF NOT exist outputlibs\Debug\WPF mkdir outputlibs\Debug\WPF
IF NOT exist outputlibs\Debug\GTK mkdir outputlibs\Debug\GTK
IF NOT exist outputlibs\Debug\UWP mkdir outputlibs\Debug\UWP

ThirdParty\Dependencies\Piranha\Piranha.exe make-portable-skeleton -i MonoGame.Framework\bin\Windows\AnyCPU\Release\MonoGame.Framework.dll -o outputlibs\Release\Portable\MonoGame.Framework.dll -p ".NETPortable,Version=v4.0,Profile=Profile328"
ThirdParty\Dependencies\Piranha\Piranha.exe make-portable-skeleton -i MonoGame.Framework.Content.Pipeline\bin\Windows\AnyCPU\Release\MonoGame.Framework.Content.Pipeline.dll -o outputlibs\Release\Portable\MonoGame.Framework.Content.Pipeline.dll -p ".NETPortable,Version=v4.0,Profile=Profile328"
copy outputlibs\Release\Portable\MonoGame.Framework.dll outputlibs\Debug\Portable\MonoGame.Framework.dll /Y
copy outputlibs\Release\Portable\MonoGame.Framework.Content.Pipeline.dll outputlibs\Debug\Portable\MonoGame.Framework.Content.Pipeline.dll /Y

copy MonoGame.Framework\bin\Android\AnyCPU\Release\MonoGame.Framework.dll outputlibs\Release\Android
copy MonoGame.Framework\bin\AndroidForms\AnyCPU\Release\MonoGame.Framework.dll outputlibs\Release\AndroidForms
copy MonoGame.Framework\bin\iOS\iPhone\Release\MonoGame.Framework.dll outputlibs\Release\iOS
copy MonoGame.Framework\bin\iOSForms\iPhone\Release\MonoGame.Framework.dll outputlibs\Release\iOSForms
copy MonoGame.Framework\bin\Windows\AnyCPU\Release\MonoGame.Framework.dll outputlibs\Release\Windows
copy MonoGame.Framework\bin\WPFForms\AnyCPU\Release\MonoGame.Framework.dll outputlibs\Release\WPFForms
copy MonoGame.Framework\bin\WPF\AnyCPU\Release\MonoGame.Framework.dll outputlibs\Release\WPF
copy MonoGame.Framework\bin\GTK\AnyCPU\Release\MonoGame.Framework.dll outputlibs\Release\GTK
copy MonoGame.Framework\bin\WindowsUniversal\AnyCPU\Release\MonoGame.Framework.dll outputlibs\Release\UWP
copy MonoGame.Framework.Content.Pipeline\bin\Windows\AnyCPU\Release\MonoGame.Framework.Content.Pipeline.dll outputlibs\Release\Windows

copy MonoGame.Framework\bin\Android\AnyCPU\Debug\MonoGame.Framework.dll outputlibs\Debug\Android
copy MonoGame.Framework\bin\AndroidForms\AnyCPU\Debug\MonoGame.Framework.dll outputlibs\Debug\AndroidForms
copy MonoGame.Framework\bin\iOS\iPhone\Debug\MonoGame.Framework.dll outputlibs\Debug\iOS
copy MonoGame.Framework\bin\iOSForms\iPhone\Debug\MonoGame.Framework.dll outputlibs\Debug\iOSForms
copy MonoGame.Framework\bin\Windows\AnyCPU\Debug\MonoGame.Framework.dll outputlibs\Debug\Windows
copy MonoGame.Framework\bin\WPFForms\AnyCPU\Debug\MonoGame.Framework.dll outputlibs\Debug\WPFForms
copy MonoGame.Framework\bin\WPF\AnyCPU\Debug\MonoGame.Framework.dll outputlibs\Debug\WPF
copy MonoGame.Framework\bin\GTK\AnyCPU\Debug\MonoGame.Framework.dll outputlibs\Debug\GTK
copy MonoGame.Framework\bin\WindowsUniversal\AnyCPU\Debug\MonoGame.Framework.dll outputlibs\Debug\UWP
copy MonoGame.Framework.Content.Pipeline\bin\Windows\AnyCPU\Debug\MonoGame.Framework.Content.Pipeline.dll outputlibs\Debug\Windows


copy MonoGame.Framework\bin\Android\AnyCPU\Debug\MonoGame.Framework.dll.mdb outputlibs\Debug\Android
copy MonoGame.Framework\bin\AndroidForms\AnyCPU\Debug\MonoGame.Framework.dll.mdb outputlibs\Debug\AndroidForms
copy MonoGame.Framework\bin\iOS\iPhone\Debug\MonoGame.Framework.dll.mdb outputlibs\Debug\iOS
copy MonoGame.Framework\bin\iOSForms\iPhone\Debug\MonoGame.Framework.dll.mdb outputlibs\Debug\iOSForms
copy MonoGame.Framework\bin\Windows\AnyCPU\Debug\MonoGame.Framework.dll.mdb outputlibs\Debug\Windows
copy MonoGame.Framework\bin\WPFForms\AnyCPU\Debug\MonoGame.Framework.dll.mdb outputlibs\Debug\WPFForms
copy MonoGame.Framework\bin\WPF\AnyCPU\Debug\MonoGame.Framework.dll.mdb outputlibs\Debug\WPF
copy MonoGame.Framework\bin\GTK\AnyCPU\Debug\MonoGame.Framework.dll.mdb outputlibs\Debug\GTK
copy MonoGame.Framework\bin\WindowsUniversal\AnyCPU\Debug\MonoGame.Framework.dll.mdb outputlibs\Debug\UWP

copy MonoGame.Framework\bin\Android\AnyCPU\Debug\MonoGame.Framework.pdb outputlibs\Debug\Android
copy MonoGame.Framework\bin\AndroidForms\AnyCPU\Debug\MonoGame.Framework.pdb outputlibs\Debug\AndroidForms
copy MonoGame.Framework\bin\iOS\iPhone\Debug\MonoGame.Framework.pdb outputlibs\Debug\iOS
copy MonoGame.Framework\bin\iOSForms\iPhone\Debug\MonoGame.Framework.pdb outputlibs\Debug\iOSForms
copy MonoGame.Framework\bin\Windows\AnyCPU\Debug\MonoGame.Framework.pdb outputlibs\Debug\Windows
copy MonoGame.Framework\bin\WPFForms\AnyCPU\Debug\MonoGame.Framework.pdb outputlibs\Debug\WPFForms
copy MonoGame.Framework\bin\WPF\AnyCPU\Debug\MonoGame.Framework.pdb outputlibs\Debug\WPF
copy MonoGame.Framework\bin\GTK\AnyCPU\Debug\MonoGame.Framework.pdb outputlibs\Debug\GTK
copy MonoGame.Framework\bin\WindowsUniversal\AnyCPU\Debug\MonoGame.Framework.pdb outputlibs\Debug\UWP
