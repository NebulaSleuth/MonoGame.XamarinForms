// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.IO;
#if IOS
using Foundation;
using Microsoft.Xna.Framework.Utilities;
using UIKit;
#elif MONOMAC
using Foundation;
#endif

namespace Microsoft.Xna.Framework
{
    partial class TitleContainer
    {
        static partial void PlatformInit()
        {
            Location = NSBundle.MainBundle.ResourcePath;
            TempLocation = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
#if IOS
            SupportRetina = UIScreen.MainScreen.Scale >= 2.0f;
            RetinaScale = (int)Math.Round(UIScreen.MainScreen.Scale);
#endif
        }

#if IOS
        static internal bool SupportRetina { get; private set; }
        static internal int RetinaScale { get; private set; }
#endif

        private static Stream PlatformOpenStream(string safeName)
        {
#if IOS
            var absolutePath = Path.Combine(Location, safeName);
            if (absolutePath.ToLower().Contains(".pak"))
            {
                // Pull from the .zip files
                int idx = absolutePath.ToLower().IndexOf(".pak") + 4;
                string pakName = Path.Combine(Location, absolutePath.Substring(0, idx));
                string filename = absolutePath.Substring(idx);
                while (filename.StartsWith("/") || filename.StartsWith("\\"))
                {
                    filename = filename.Substring(1);
                }

                try
                {
                    if (File.Exists(pakName))
                    {
                        return FilePacker.GetFileStream(pakName, filename);
                    }
                }
                catch
                {
                }
                return null;
            }
            if (SupportRetina)
            {
                for (var scale = RetinaScale; scale >= 2; scale--)
                {
                    // Insert the @#x immediately prior to the extension. If this file exists
                    // and we are on a Retina device, return this file instead.
                    var absolutePathX = Path.Combine(Path.GetDirectoryName(absolutePath),
                                                      Path.GetFileNameWithoutExtension(absolutePath)
                                                      + "@" + scale + "x" + Path.GetExtension(absolutePath));
                    if (File.Exists(absolutePathX))
                        return File.OpenRead(absolutePathX);
                }
            }
            return File.OpenRead(absolutePath);
#else
            var absolutePath = Path.Combine(Location, safeName);
            return File.OpenRead(absolutePath);
#endif
        }
    }
}

