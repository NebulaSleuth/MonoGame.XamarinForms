// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.IO;
using Microsoft.Xna.Framework.Utilities;

namespace Microsoft.Xna.Framework
{
    partial class TitleContainer
    {
        static partial void PlatformInit()
        {
#if WINDOWS || DESKTOPGL
#if DESKTOPGL
            // Check for the package Resources Folder first. This is where the assets
            // will be bundled.
            if (CurrentPlatform.OS == OS.MacOSX)
                Location = Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "..", "Resources");
            if (!Directory.Exists (Location))
#endif
            Location = AppDomain.CurrentDomain.BaseDirectory;
            TempLocation = AppDomain.CurrentDomain.BaseDirectory;
#endif
        }

        private static Stream PlatformOpenStream(string safeName)
        {
            if (safeName.ToLower().Contains(".pak"))
            {
                // Pull from the .zip files
                int idx = safeName.ToLower().IndexOf(".pak") + 4;
                string pakName = Path.Combine(Location, safeName.Substring(0, idx));
                string filename = safeName.Substring(idx);
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
            else
            {
                var absolutePath = Path.Combine(Location, safeName);

                return File.OpenRead(absolutePath);
            }
        }
    }

}

