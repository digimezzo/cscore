﻿using System;

namespace CSCore.Compression.ACM
{
    public class AcmConverter : IDisposable
    {
        AcmBufferConverter _acmBufferConverter;

        public WaveFormat SourceFormat { get; private set; }
        public WaveFormat DestinationFormat { get; private set; }

        public AcmConverter(WaveFormat sourceFormat)
        {
            SourceFormat = sourceFormat;
            DestinationFormat = AcmBufferConverter.SuggestFormat(sourceFormat);
            _acmBufferConverter = new AcmBufferConverter(sourceFormat, DestinationFormat);
        }

        public int Convert(byte[] sourceBuffer, int count, byte[] destinationBuffer, int offset)
        {
            var result = _acmBufferConverter.Convert(sourceBuffer, count);
            if (result.HasError)
            {
                Context.Current.Logger.Error(new Exception("Couldn't convert to whole sourcebuffer"),
                    "AcmFrameConverter.Convert(byte[], int, byte[], int)", true);

                Array.Clear(destinationBuffer, 0, destinationBuffer.Length);
                return -1;
            }
            else
            {
                Array.Copy(result.DestinationBuffer, 0, destinationBuffer, offset, result.DestinationUsed);
                return result.DestinationUsed;
            }
        }

        private bool _disposed;
		public void Dispose()
        {
			if(!_disposed)
			{
				_disposed = true;
				
				Dispose(true);
				GC.SuppressFinalize(this);
			}
        }

        protected virtual void Dispose(bool disposing)
        {
			if(disposing)
			{
				//dispose managed
			}
            if (_acmBufferConverter != null)
            {
                _acmBufferConverter.Dispose();
                _acmBufferConverter = null;
            }
        }

        ~AcmConverter()
        {
            Dispose(false);
        }
    }
}
