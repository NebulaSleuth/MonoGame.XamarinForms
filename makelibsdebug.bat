nuget restore MonoGame.Framework\MonoGame.Framework.AndroidForms.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.AndroidForms.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.iOS.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.iOSForms.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.Windows.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.WPFForms.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.WPF.csproj -SolutionDir .
nuget restore MonoGame.Framework\MonoGame.Framework.WindowsUniversal.csproj -SolutionDir .


msbuild MonoGame.Framework\MonoGame.Framework.Android.csproj /p:Configuration=Debug /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.AndroidForms.csproj /p:Configuration=Debug /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.iOS.csproj /p:Configuration=Debug /p:Platform=iPhone /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.iOSForms.csproj /p:Configuration=Debug /p:Platform=iPhone /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.Windows.csproj /p:Configuration=Debug /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.WPFForms.csproj /p:Configuration=Debug /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.WPF.csproj /p:Configuration=Debug /p:Platform=AnyCPU /t:Clean,Rebuild
msbuild MonoGame.Framework\MonoGame.Framework.WindowsUniversal.csproj /p:Configuration=Debug /p:Platform=AnyCPU /t:Clean,Rebuild

IF NOT exist debugoutputlibs mkdir debugoutputlibs
IF NOT exist debugoutputlibs\Android mkdir debugoutputlibs\Android
IF NOT exist debugoutputlibs\AndroidForms mkdir debugoutputlibs\AndroidForms
IF NOT exist debugoutputlibs\iOS mkdir debugoutputlibs\iOS
IF NOT exist debugoutputlibs\iOSForms mkdir debugoutputlibs\iOSForms
IF NOT exist debugoutputlibs\Windows mkdir debugoutputlibs\Windows
IF NOT exist debugoutputlibs\WPFForms mkdir debugoutputlibs\WPFForms
IF NOT exist debugoutputlibs\WPF mkdir debugoutputlibs\WPF
IF NOT exist debugoutputlibs\UWP mkdir debugoutputlibs\UWP

copy MonoGame.Framework\bin\Android\AnyCPU\Debug\MonoGame.Framework.dll debugoutputlibs\Android
copy MonoGame.Framework\bin\AndroidForms\AnyCPU\Debug\MonoGame.Framework.dll debugoutputlibs\AndroidForms
copy MonoGame.Framework\bin\iOS\iPhone\Debug\MonoGame.Framework.dll debugoutputlibs\iOS
copy MonoGame.Framework\bin\iOSForms\iPhone\Debug\MonoGame.Framework.dll debugoutputlibs\iOSForms
copy MonoGame.Framework\bin\Windows\AnyCPU\Debug\MonoGame.Framework.dll debugoutputlibs\Windows
copy MonoGame.Framework\bin\WPFForms\AnyCPU\Debug\MonoGame.Framework.dll debugoutputlibs\WPFForms
copy MonoGame.Framework\bin\WPF\AnyCPU\Debug\MonoGame.Framework.dll debugoutputlibs\WPF
copy MonoGame.Framework\bin\WindowsUniversal\AnyCPU\Debug\MonoGame.Framework.dll debugoutputlibs\UWP

copy MonoGame.Framework\bin\Android\AnyCPU\Debug\MonoGame.Framework.pdb debugoutputlibs\Android
copy MonoGame.Framework\bin\AndroidForms\AnyCPU\Debug\MonoGame.Framework.pdb debugoutputlibs\AndroidForms
copy MonoGame.Framework\bin\iOS\iPhone\Debug\MonoGame.Framework.pdb debugoutputlibs\iOS
copy MonoGame.Framework\bin\iOSForms\iPhone\Debug\MonoGame.Framework.pdb debugoutputlibs\iOSForms
copy MonoGame.Framework\bin\Windows\AnyCPU\Debug\MonoGame.Framework.pdb debugoutputlibs\Windows
copy MonoGame.Framework\bin\WPFForms\AnyCPU\Debug\MonoGame.Framework.pdb debugoutputlibs\WPFForms
copy MonoGame.Framework\bin\WPF\AnyCPU\Debug\MonoGame.Framework.pdb debugoutputlibs\WPF
copy MonoGame.Framework\bin\WindowsUniversal\AnyCPU\Debug\MonoGame.Framework.pdb debugoutputlibs\UWP


