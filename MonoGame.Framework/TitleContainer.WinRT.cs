// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using Microsoft.Xna.Framework.Utilities;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;

namespace Microsoft.Xna.Framework
{
    partial class TitleContainer
    {
        static internal ResourceContext ResourceContext;
        static internal ResourceMap FileResourceMap;

        static partial void PlatformInit()
        {
            //Location = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            Location = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            TempLocation = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
             
            ResourceContext = new Windows.ApplicationModel.Resources.Core.ResourceContext();
            FileResourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("Files");
        }

        private static async Task<Stream> OpenStreamAsync(string name)
        {
            NamedResource result;

            if (name.ToLower().Contains(".pak"))
            {
                // Pull from the .zip files
                int idx = name.ToLower().IndexOf(".pak") + 4;
                string zipName = Path.Combine(Location, name.Substring(0, idx));
                string filename = name.Substring(idx);
                while (filename.StartsWith("/") || filename.StartsWith("\\"))
                {
                    filename = filename.Substring(1);
                }

                try
                {
                    if (File.Exists(zipName))
                    {
                        return FilePacker.GetFileStream(zipName, filename);
                    }
                }
                catch
                {
                }
                return null;
            }
            else
            {

                if (FileResourceMap != null && FileResourceMap.TryGetValue(name, out result))
                {
                    var resolved = result.Resolve(ResourceContext);

                    try
                    {
                        var storageFile = await resolved.GetValueAsFileAsync();
                        var randomAccessStream = await storageFile.OpenReadAsync();
                        return randomAccessStream.AsStreamForRead();
                    }
                    catch (IOException)
                    {
                        // The file must not exist... return a null stream.
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        private static Stream PlatformOpenStream(string safeName)
        {
            return Task.Run(() => OpenStreamAsync(safeName).Result).Result;
        }
    }
}

