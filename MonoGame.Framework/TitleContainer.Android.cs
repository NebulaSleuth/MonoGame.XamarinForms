// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using Java.Util.Zip;
using Microsoft.Xna.Framework.Utilities;
using System;
using System.IO;

namespace Microsoft.Xna.Framework
{

    partial class TitleContainer
    {
        static partial void PlatformInit()
        {
            TempLocation = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        }
        private static Stream PlatformOpenStream(string safeName)
        {
            if (safeName.ToLower().Contains(".pak"))
            {
                // Pull from the .zip files
                int idx = safeName.ToLower().IndexOf(".pak") + 4;
                string pakName = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), safeName.Substring(0, idx));
                string resPakName = safeName.Substring(0, idx);
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
                    else
                    {
                        // Check the rsources
                        return FilePacker.GetFileStream(Android.App.Application.Context.Assets.Open(resPakName, Android.Content.Res.Access.Random), filename);
                    }
                }
                catch
                {
                }
                return null;
            }
            if (Path.IsPathRooted(safeName) && File.Exists(safeName))
            {
                return File.OpenRead(safeName);
            }
            return Android.App.Application.Context.Assets.Open(safeName);
        }
    }
}

