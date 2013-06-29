﻿using System;
using System.Collections.ObjectModel;
using System.IO;

namespace CSCore.Codecs.MP3
{
    public class FrameInfoCollection : Collection<MP3FrameInfo>, IDisposable
    {
        private Mp3Frame frame;
        private int playbackIndex = 0;

        public int TotalSamples { get; private set; }
        public int PlaybackIndex { get { return playbackIndex; } set { playbackIndex = value; } }

        public bool AddFromMP3Stream(Stream stream)
        {
            try
            {
                MP3FrameInfo info = new MP3FrameInfo();

                info.StreamPosition = stream.Position;
                info.SampleIndex = TotalSamples;
                frame = Mp3Frame.FromStream(stream);
                if (frame != null)
                {
                    info.SampleAmount = frame.SampleCount;
                    info.Size = Convert.ToInt32(stream.Position - info.StreamPosition);
                    TotalSamples += frame.SampleCount;

                    Add(info);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception) { return false; }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            for (int i = 0; i < Count; i++)
            {
                this[i] = null;
            }
            frame = null;
        }
    }

    public class MP3FrameInfo
    {
        public long StreamPosition;
        public int SampleIndex;
        public int SampleAmount;
        public int Size;
    }
}
