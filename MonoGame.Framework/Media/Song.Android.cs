// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Database;
using Android.Media;
using Android.Provider;
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.Extractor;
using Com.Google.Android.Exoplayer2.Source;
using Com.Google.Android.Exoplayer2.Trackselection;
using Com.Google.Android.Exoplayer2.Upstream;
using Com.Google.Android.Exoplayer2.Upstream.Cache;
using Com.Google.Android.Exoplayer2.Util;
using Java.IO;
using Java.Lang;
using Java.Net;
using Microsoft.Xna.Framework.Utilities;
using System;
using System.IO;

namespace Microsoft.Xna.Framework.Media
{
    public sealed partial class Song : Java.Lang.Object, IEquatable<Song>, IDisposable, IPlayerEventListener
    {
        //static Android.Media.MediaPlayer _androidPlayer;
        static SimpleExoPlayer _androidPlayer;
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
            //_androidPlayer = new Android.Media.MediaPlayer();
            //_androidPlayer.Completion += AndroidPlayer_Completion;
        }

        private void prepareExoPlayerFromFile(string path)
        {
            _androidPlayer = ExoPlayerFactory.NewSimpleInstance(MediaLibrary.Context, new DefaultTrackSelector());
            _androidPlayer.AddListener(this);
            if (MediaPlayer.IsRepeating) _androidPlayer.RepeatMode = Player.RepeatModeAll;
            else _androidPlayer.RepeatMode = Player.RepeatModeOff;

            string userAgent = Util.GetUserAgent(MediaLibrary.Context, "Play Audio");

            ExtractorMediaSource audioSource = new ExtractorMediaSource(
                  Android.Net.Uri.FromFile(new Java.IO.File(path)), 
                  new DefaultDataSourceFactory(MediaLibrary.Context, userAgent),
                  new DefaultExtractorsFactory(), null, null);

            _androidPlayer.Volume = _volume;
            _androidPlayer.Prepare(audioSource);
            _androidPlayer.PlayWhenReady = true;
        }

        private void prepareExoPlayerFromUri(Android.Net.Uri uri)
        {
            _androidPlayer = ExoPlayerFactory.NewSimpleInstance(MediaLibrary.Context, new DefaultTrackSelector());
            _androidPlayer.AddListener(this);
            if (MediaPlayer.IsRepeating) _androidPlayer.RepeatMode = Player.RepeatModeAll;
            else _androidPlayer.RepeatMode = Player.RepeatModeOff;

            string userAgent = Util.GetUserAgent(MediaLibrary.Context, "Play Audio");

            ExtractorMediaSource audioSource = new ExtractorMediaSource(
                  uri,
                  new DefaultDataSourceFactory(MediaLibrary.Context, userAgent),
                  new DefaultExtractorsFactory(), null, null);

            _androidPlayer.Volume = _volume;
            _androidPlayer.Prepare(audioSource);
            _androidPlayer.PlayWhenReady = true;
        }
        class BytesFactory : Java.Lang.Object, IDataSourceFactory
        {
            ByteArrayDataSource _source;

            public BytesFactory(ByteArrayDataSource src)
            {
                _source = src;
            }

            public IDataSource CreateDataSource()
            {
                return _source;
            }

            public void Dispose()
            {
            }
        }
        private void prepareExoPlayerFromBytes(byte[] data)
        {
            ByteArrayDataSource byteArrayDataSource = new ByteArrayDataSource(data);

            _androidPlayer = ExoPlayerFactory.NewSimpleInstance(MediaLibrary.Context, new DefaultTrackSelector());
            _androidPlayer.AddListener(this);
            if (MediaPlayer.IsRepeating) _androidPlayer.RepeatMode = Player.RepeatModeAll;
            else _androidPlayer.RepeatMode = Player.RepeatModeOff;

            string userAgent = Util.GetUserAgent(MediaLibrary.Context, "Play Audio");

            var audioByteUri = GetUri(data);
            DataSpec dataSpec = new DataSpec(audioByteUri);
            try
            {
                byteArrayDataSource.Open(dataSpec);
            }
            catch (System.Exception ex)
            {
            }

            ExtractorMediaSource audioSource = new ExtractorMediaSource(
                  audioByteUri,
                  new BytesFactory(byteArrayDataSource),
                  new DefaultExtractorsFactory(), null, null);

            _androidPlayer.Volume = _volume;
            _androidPlayer.Prepare(audioSource);
            _androidPlayer.PlayWhenReady = true;
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
            prepareExoPlayerFromFile(fileName);
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
            //_androidPlayer.Completion -= AndroidPlayer_Completion;
            _androidPlayer.Stop();
            _androidPlayer = null;
        }

        public Android.Net.Uri GetUri(byte[] data)
        {

            try
            {
                URL url = new URL(null, "bytes:///" + "audio", new BytesHandler(data));
                return Android.Net.Uri.Parse(url.ToURI().ToString());
            }
            catch (MalformedURLException e)
            {
                throw new RuntimeException(e);
            }
            catch (URISyntaxException e)
            {
                throw new RuntimeException(e);
            }

        }

        class BytesHandler : URLStreamHandler
        {
            byte[] mData;
            public BytesHandler(byte[] data)
            {
                mData = data;
            }

            protected override URLConnection OpenConnection(URL u, Proxy p)
            {
                return new ByteUrlConnection(u, mData);
                //return base.OpenConnection(u, p);
            }

            protected override URLConnection OpenConnection(URL u)
            {
                return new ByteUrlConnection(u, mData);
            }
        }

        class ByteUrlConnection : URLConnection
        {
            byte[]   mData;
            public ByteUrlConnection(URL url, byte[] data) : base (url)
            {
                mData = data;
            }

            public override void Connect()
            {
            }

            public override System.IO.Stream InputStream
            {
                get
                {
                    return new MemoryStream(mData);
                }
            }

        }

        internal void Play(TimeSpan? startPosition)
        {
            // Prepare the player
            _androidPlayer?.Stop();

            if (assetUri != null)
            {
                prepareExoPlayerFromUri(assetUri);
                //_androidPlayer.SetDataSource(MediaLibrary.Context, this.assetUri);
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

                    string resPakName = path.Substring(0, idx);

                    string filename = path.Substring(idx);
                    while (filename.StartsWith("/") || filename.StartsWith("\\"))
                    {
                        filename = filename.Substring(1);
                    }

                    byte[] data = null;
                    try
                    {
                        long len = FilePacker.GetAssetLength(pakName, filename);
                        if (len > 0)
                        {
                            data = new byte[len];
                            if (System.IO.File.Exists(pakName))
                            {
                                using (System.IO.Stream s = FilePacker.GetFileStream(pakName, filename))
                                {
                                    s.Read(data, 0, (int)len);
                                    s.Close();
                                }
                            }
                            else
                            {
                                // Check the rsources
                                using (System.IO.Stream s = FilePacker.GetFileStream(Android.App.Application.Context.Assets.Open(resPakName, Android.Content.Res.Access.Random), filename))
                                {
                                    s.Read(data, 0, (int)len);
                                    s.Close();
                                }
                            }
                        }
                    }
                    catch
                    {
                        data = null;
                    }

                    /*
                                        var afd = Game.Activity.Assets.OpenFd(filename);
                                        if (afd == null)
                                            return;

                                        byte[] data = new byte[afd.Length];
                    */
                    //MemoryStream ms = new MemoryStream(data);
                    //using (var stream = afd.CreateInputStream())
                    //{
                    //    stream.CopyTo(ms);
                    //}
                    if (data != null)
                        prepareExoPlayerFromBytes(data);
                }
                else
                {
                    var afd = Game.Activity.Assets.OpenFd(_name);
                    if (afd == null)
                        return;
                    byte[] data = new byte[afd.Length];
                    MemoryStream ms = new MemoryStream(data);
                    using (var stream = afd.CreateInputStream())
                    {
                        stream.CopyTo(ms);
                    }
                    prepareExoPlayerFromBytes(data);

                }
            }


            //_androidPlayer.Prepare();
            //_androidPlayer.Looping = MediaPlayer.IsRepeating;
            _playingSong = this;

            if (startPosition.HasValue)
                Position = startPosition.Value;
            //_androidPlayer.Start();
            _playCount++;
        }

        internal void Resume()
        {
            if (_androidPlayer == null) return;
            _androidPlayer.PlayWhenReady = true;
        }

        internal void Pause()
        {
            if (_androidPlayer == null) return;
            _androidPlayer.PlayWhenReady = false;
        }

        internal void Stop()
        {
            _androidPlayer?.Stop();
            _playingSong = null;
            _playCount = 0;
            position = TimeSpan.Zero;
        }

        float _volume = 0.5f;
        internal float Volume
        {
            get
            {
                return _volume;
            }

            set
            {
                try
                {
                    _volume = value;
                    if (_androidPlayer != null) _androidPlayer.Volume = value;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
        }

        public TimeSpan Position
        {
            get
            {
                if (_playingSong == this)
                    position = TimeSpan.FromMilliseconds(_androidPlayer.CurrentPosition);

                return position;
            }
            set
            {
                _androidPlayer?.SeekTo((int)value.TotalMilliseconds);   
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

        public void OnLoadingChanged(bool p0)
        {
        }

        public void OnPlaybackParametersChanged(PlaybackParameters p0)
        {
        }

        public void OnPlayerError(ExoPlaybackException p0)
        {
        }

        public void OnPlayerStateChanged(bool p0, int p1)
        {
            switch (p1)
            {
                case Player.StateEnded:
                    AndroidPlayer_Completion(this, EventArgs.Empty);
                    break;
            }
        }

        public void OnPositionDiscontinuity(int p0)
        {
        }

        public void OnRepeatModeChanged(int p0)
        {
        }

        public void OnSeekProcessed()
        {
        }

        public void OnShuffleModeEnabledChanged(bool p0)
        {
        }

        public void OnTimelineChanged(Timeline p0, Java.Lang.Object p1, int p2)
        {
        }

        public void OnTracksChanged(TrackGroupArray p0, TrackSelectionArray p1)
        {
        }
    }
}

