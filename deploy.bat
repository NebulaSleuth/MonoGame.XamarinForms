IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Debug mkdir G:\Bingonomics\Main\BarXtreme\MonoGame\Debug
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\Andorid mkdir G:\Bingonomics\Main\BarXtreme\MonoGame\Release
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\Android mkdir outputlibs\Release\Android
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\AndroidForms mkdir outputlibs\Release\AndroidForms
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\iOS mkdir outputlibs\Release\iOS
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\iOSForms mkdir outputlibs\Release\iOSForms
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\Windows mkdir outputlibs\Release\Windows
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\WPFForms mkdir outputlibs\Release\WPFForms
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\WPF mkdir outputlibs\Release\WPF
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\GTK mkdir outputlibs\Release\GTK
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\Portable mkdir outputlibs\Release\Portable
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\UWP mkdir outputlibs\Release\UWP

IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Release mkdir G:\Bingonomics\Main\BarXtreme\MonoGame\Release
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Release\Andorid mkdir G:\Bingonomics\Main\BarXtreme\MonoGame\Release
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Release\Android mkdir outputlibs\Release\Android
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Release\AndroidForms mkdir outputlibs\Release\AndroidForms
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Release\iOS mkdir outputlibs\Release\iOS
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Release\iOSForms mkdir outputlibs\Release\iOSForms
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Release\Windows mkdir outputlibs\Release\Windows
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Release\WPFForms mkdir outputlibs\Release\WPFForms
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Release\WPF mkdir outputlibs\Release\WPF
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Release\GTK mkdir outputlibs\Release\GTK
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Release\Portable mkdir outputlibs\Release\Portable
IF NOT exist G:\Bingonomics\Main\BarXtreme\MonoGame\Release\UWP mkdir outputlibs\Release\UWP

copy MonoGame.Framework\bin\Android\AnyCPU\Release\MonoGame.Framework.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Release\Android /Y
copy MonoGame.Framework\bin\AndroidForms\AnyCPU\Release\MonoGame.Framework.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Release\AndroidForms /Y
copy MonoGame.Framework\bin\iOS\iPhone\Release\MonoGame.Framework.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Release\iOS /Y
copy MonoGame.Framework\bin\iOSForms\iPhone\Release\MonoGame.Framework.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Release\iOSForms /Y
copy MonoGame.Framework\bin\Windows\AnyCPU\Release\MonoGame.Framework.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Release\Windows /Y
copy MonoGame.Framework\bin\WPFForms\AnyCPU\Release\MonoGame.Framework.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Release\WPFForms /Y
copy MonoGame.Framework\bin\WPF\AnyCPU\Release\MonoGame.Framework.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Release\WPF /Y
copy MonoGame.Framework\bin\GTK\AnyCPU\Release\MonoGame.Framework.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Release\GTK /Y
copy MonoGame.Framework\bin\WindowsUniversal\AnyCPU\Release\MonoGame.Framework.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Release\UWP /Y
copy MonoGame.Framework.Content.Pipeline\bin\Windows\AnyCPU\Release\MonoGame.Framework.Content.Pipeline.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Release\Windows /Y

copy MonoGame.Framework\bin\Android\AnyCPU\Release\MonoGame.Framework.pdb G:\Bingonomics\Main\BarXtreme\MonoGame\Release\Android /Y
copy MonoGame.Framework\bin\AndroidForms\AnyCPU\Release\MonoGame.Framework.pdb G:\Bingonomics\Main\BarXtreme\MonoGame\Release\AndroidForms /Y
copy MonoGame.Framework\bin\iOS\iPhone\Release\MonoGame.Framework.pdb G:\Bingonomics\Main\BarXtreme\MonoGame\Release\iOS /Y
copy MonoGame.Framework\bin\iOSForms\iPhone\Release\MonoGame.Framework.pdb G:\Bingonomics\Main\BarXtreme\MonoGame\Release\iOSForms /Y
copy MonoGame.Framework\bin\Windows\AnyCPU\Release\MonoGame.Framework.pdb G:\Bingonomics\Main\BarXtreme\MonoGame\Release\Windows /Y
copy MonoGame.Framework\bin\WPFForms\AnyCPU\Release\MonoGame.Framework.pdb G:\Bingonomics\Main\BarXtreme\MonoGame\Release\WPFForms /Y
copy MonoGame.Framework\bin\WPF\AnyCPU\Release\MonoGame.Framework.pdb G:\Bingonomics\Main\BarXtreme\MonoGame\Release\WPF /Y
copy MonoGame.Framework\bin\GTK\AnyCPU\Release\MonoGame.Framework.pdb G:\Bingonomics\Main\BarXtreme\MonoGame\Release\GTK /Y
copy MonoGame.Framework\bin\WindowsUniversal\AnyCPU\Release\MonoGame.Framework.pdb G:\Bingonomics\Main\BarXtreme\MonoGame\Release\UWP /Y

copy MonoGame.Framework\bin\Android\AnyCPU\Debug\MonoGame.Framework.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\Android /Y
copy MonoGame.Framework\bin\AndroidForms\AnyCPU\Debug\MonoGame.Framework.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\AndroidForms /Y
copy MonoGame.Framework\bin\iOS\iPhone\Debug\MonoGame.Framework.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\iOS /Y
copy MonoGame.Framework\bin\iOSForms\iPhone\Debug\MonoGame.Framework.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\iOSForms /Y
copy MonoGame.Framework\bin\Windows\AnyCPU\Debug\MonoGame.Framework.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\Windows /Y
copy MonoGame.Framework\bin\WPFForms\AnyCPU\Debug\MonoGame.Framework.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\WPFForms /Y
copy MonoGame.Framework\bin\WPF\AnyCPU\Debug\MonoGame.Framework.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\WPF /Y
copy MonoGame.Framework\bin\GTK\AnyCPU\Debug\MonoGame.Framework.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\GTK /Y
copy MonoGame.Framework\bin\WindowsUniversal\AnyCPU\Debug\MonoGame.Framework.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\UWP /Y
copy MonoGame.Framework.Content.Pipeline\bin\Windows\AnyCPU\Debug\MonoGame.Framework.Content.Pipeline.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\Windows /Y

copy MonoGame.Framework\bin\Android\AnyCPU\Debug\MonoGame.Framework.pdb G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\Android /Y
copy MonoGame.Framework\bin\AndroidForms\AnyCPU\Debug\MonoGame.Framework.pdb G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\AndroidForms /Y
copy MonoGame.Framework\bin\iOS\iPhone\Debug\MonoGame.Framework.dll G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\iOS /Y
copy MonoGame.Framework\bin\iOSForms\iPhone\Debug\MonoGame.Framework.pdb G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\iOSForms /Y
copy MonoGame.Framework\bin\Windows\AnyCPU\Debug\MonoGame.Framework.pdb G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\Windows /Y
copy MonoGame.Framework\bin\WPFForms\AnyCPU\Debug\MonoGame.Framework.pdb G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\WPFForms /Y
copy MonoGame.Framework\bin\WPF\AnyCPU\Debug\MonoGame.Framework.pdb G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\WPF /Y
copy MonoGame.Framework\bin\GTK\AnyCPU\Debug\MonoGame.Framework.pdb G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\GTK /Y
copy MonoGame.Framework\bin\WindowsUniversal\AnyCPU\Debug\MonoGame.Framework.pdb G:\Bingonomics\Main\BarXtreme\MonoGame\Debug\UWP /Y

