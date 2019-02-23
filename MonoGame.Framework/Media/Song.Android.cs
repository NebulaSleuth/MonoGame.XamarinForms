// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using Android.Content;
using Android.Content.Res;
using Android.Database;
using Android.Provider;
using Java.IO;
using Microsoft.Xna.Framework.Utilities;
using System;
using System.IO;

namespace Microsoft.Xna.Framework.Media
{
    public sealed partial class Song : IEquatable<Song>, IDisposable
    {
        static Android.Media.MediaPlayer _androidPlayer;
        static Song _playingSong;

        private Album album;
        private Artist artist;
        private Genre genre;
        private string name;
        private TimeSpan duration;
        private TimeSpan position;
        private Android.Net.Uri assetUri;

        [CLSCompliant(false)]
        public Android.Net.Uri AssetUri
        {
            get { return this.assetUri; }
        }

        static Song()
        {
            _androidPlayer = new Android.Media.MediaPlayer();
            _androidPlayer.Completion += AndroidPlayer_Completion;
        }

        internal Song(Album album, Artist artist, Genre genre, string name, TimeSpan duration, Android.Net.Uri assetUri)
        {
            this.album = album;
            this.artist = artist;
            this.genre = genre;
            this.name = name;
            this.duration = duration;
            this.assetUri = assetUri;
        }

        private void PlatformInitialize(string fileName)
        {
            // Nothing to do here
            if (_androidPlayer == null)
            {
                _androidPlayer = new Android.Media.MediaPlayer();
                _androidPlayer.Completion += AndroidPlayer_Completion;

            }
        }

        static void AndroidPlayer_Completion(object sender, EventArgs e)
        {
            var playingSong = _playingSong;
            _playingSong = null;

            if (playingSong != null && playingSong.DonePlaying != null)
                playingSong.DonePlaying(sender, e);
        }

        /// <summary>
        /// Set the event handler for "Finished Playing". Done this way to prevent multiple bindings.
        /// </summary>
        internal void SetEventHandler(FinishedPlayingHandler handler)
        {
            if (DonePlaying != null)
                return;
            DonePlaying += handler;
        }

        private void PlatformDispose(bool disposing)
        {
            _androidPlayer.Completion -= AndroidPlayer_Completion;
            _androidPlayer.Stop();
            _androidPlayer = null;
        }


        internal void Play(TimeSpan? startPosition)
        {
            // Prepare the player
            _androidPlayer.Reset();

            if (assetUri != null)
            {
                _androidPlayer.SetDataSource(MediaLibrary.Context, this.assetUri);
            }
            else
            {
                if (_name.ToLower().Contains(".pak"))
                {
                    // Android resources suck and the the MediaPlayer has qeird issues with local files
                    // So map this and assume the resource is available
                    string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), _name);
                    int idx = path.ToLower().IndexOf(".pak") + 4;
                    string pakName = path.Substring(0, idx);

                    string filename = path.Substring(idx);
                    while (filename.StartsWith("/") || filename.StartsWith("\\"))
                    {
                        filename = filename.Substring(1);
                    }

                    var afd = Game.Activity.Assets.OpenFd(filename);
                    if (afd == null)
                        return;

                    _androidPlayer.SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
                }
                else
                {
                    var afd = Game.Activity.Assets.OpenFd(_name);
                    if (afd == null)
                        return;
                    _androidPlayer.SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
                }
            }


            _androidPlayer.Prepare();
            _androidPlayer.Looping = MediaPlayer.IsRepeating;
            _playingSong = this;

            if (startPosition.HasValue)
                Position = startPosition.Value;
            _androidPlayer.Start();
            _playCount++;
        }

        internal void Resume()
        {
            _androidPlayer.Start();
        }

        internal void Pause()
        {
            _androidPlayer.Pause();
        }

        internal void Stop()
        {
            _androidPlayer.Stop();
            _playingSong = null;
            _playCount = 0;
            position = TimeSpan.Zero;
        }

        internal float Volume
        {
            get
            {
                return 0.0f;
            }

            set
            {
                _androidPlayer.SetVolume(value, value);
            }
        }

        public TimeSpan Position
        {
            get
            {
                if (_playingSong == this && _androidPlayer.IsPlaying)
                    position = TimeSpan.FromMilliseconds(_androidPlayer.CurrentPosition);

                return position;
            }
            set
            {
                _androidPlayer.SeekTo((int)value.TotalMilliseconds);   
            }
        }


        private Album PlatformGetAlbum()
        {
            return this.album;
        }

        private Artist PlatformGetArtist()
        {
            return this.artist;
        }

        private Genre PlatformGetGenre()
        {
            return this.genre;
        }

        private TimeSpan PlatformGetDuration()
        {
            return this.assetUri != null ? this.duration : _duration;
        }

        private bool PlatformIsProtected()
        {
            return false;
        }

        private bool PlatformIsRated()
        {
            return false;
        }

        private string PlatformGetName()
        {
            return this.assetUri != null ? this.name : Path.GetFileNameWithoutExtension(_name);
        }

        private int PlatformGetPlayCount()
        {
            return _playCount;
        }

        private int PlatformGetRating()
        {
            return 0;
        }

        private int PlatformGetTrackNumber()
        {
            return 0;
        }
    }
}

