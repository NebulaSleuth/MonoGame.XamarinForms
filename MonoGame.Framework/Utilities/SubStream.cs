using System;
using System.IO;

namespace Microsoft.Xna.Framework.Utilities
{
    public class SubStream : Stream
    {
        Stream _host;
        long _length;
        long _start;

        public SubStream(Stream host, long start, long len, long curPos)
        {
            _host = host;
            _length = len;
            _start = start;
            if (host.CanSeek)
                host.Seek(_start, SeekOrigin.Begin);
            else
            {

                long skip = _start - curPos;
                SkipBytes((int)skip);
                _position = 0;
            }
        }

        public override bool CanRead => _host.CanRead;

        public override bool CanSeek => true;

        public override bool CanWrite => _host.CanWrite;

        public override long Length => _length;

        private long _position = 0;
        public override long Position
        {
            get
            {
                if (_host.CanSeek)
                    return _host.Position - _start;
                else
                    return _position;
            }

            set
            {
                if (_host.CanSeek)
                    _host.Position = value + _start;
                else
                    _position = value;
            }
        }

        public override void Flush()
        {
            _host.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            long len = count;
            if (_host.CanSeek)
            {
                if ((Position + len) > _length) len = _length - Position;
                if (len < 1) return 0;
                return _host.Read(buffer, offset, (int)len);
            }

            if (_position + len > Length) len = _length - _position;

            if (len < 1) throw new EndOfStreamException();

            int rlen = _host.Read(buffer, offset, (int)len);
            _position += rlen;
            return rlen;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (_host.CanSeek)
            {
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        return _host.Seek(offset + _start, origin) - _start;
                    case SeekOrigin.Current:
                        return _host.Seek(offset, SeekOrigin.Current) - _start;
                    case SeekOrigin.End:
                        _host.Seek(_start + Length, SeekOrigin.Begin);
                        return _host.Seek(offset, SeekOrigin.Current) - _start;
                }
                return _host.Position - _start;
            }
            else
            {
                // Read forward if we are seeking forward
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        if (offset > _position)
                        {
                            SkipBytes((int)(offset - _position));
                        }
                        if (offset < _position) throw new InvalidOperationException();

                        return offset;
                    case SeekOrigin.Current:
                        if (offset < 0) throw new InvalidOperationException();
                        _position += offset;
                        return _position;
                    case SeekOrigin.End:
                        if (offset > 0) throw new EndOfStreamException();

                        long _npos = Length + offset;
                        if (_npos < _position) throw new InvalidOperationException();
                        if (_npos > _position) SkipBytes((int)(_npos - _position));
                        return _position;
                }
                return _host.Position - _start;

            }
        }

        private void SkipBytes(int cnt)
        {
            byte[] buffer = new byte[10240];
            long skip = cnt;

            long slen = Math.Min(skip, 10240L);
            while (skip > 0)
            {
                var rlen = _host.Read(buffer, 0, (int)slen);
                if (rlen != slen)
                {
                    _position = Length;
                    throw new EndOfStreamException();
                }
                skip -= rlen;
                slen = Math.Min(skip, 10240L);
            }
            _position += cnt;
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _host.Write(buffer, offset, count);
        }
    }
}
