using System;
using System.IO;

namespace Steeltoe.Initializr.WebApi.Test.TestUtils
{
    public sealed class TempFile : IDisposable
    {
        private string _path;

        public String Path
        {
            get
            {
                if (_path == null)
                {
                    throw new ObjectDisposedException(GetType().Name);
                }

                return _path;
            }
        }

        public TempFile() : this(System.IO.Path.GetTempFileName())
        {
        }

        public TempFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            _path = path;
        }

        ~TempFile()
        {
            Dispose(false);
        }


        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }

            if (_path != null)
            {
                try
                {
                    File.Delete(_path);
                }
                catch
                {
                    // ignored
                }

                _path = null;
            }
        }
    }
}
