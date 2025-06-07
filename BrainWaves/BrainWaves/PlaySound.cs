using System;
using System.IO;
using System.Media;
using System.Text;

namespace BrainWaves
{
    internal class PlaySound
    {
        private double _leftFrequency = 75.0;
        private double _rightFrequency = 73.0;
        private double _leftGain = 0.5;
        private double _rightGain = 0.5;
        private int _duration = 5000; // 5 seconds by default

        public void SetFrequencies(double left, double right)
        {
            _leftFrequency = left;
            _rightFrequency = right;
        }

        public void SetGains(double left, double right)
        {
            _leftGain = Math.Max(0, Math.Min(1, left));
            _rightGain = Math.Max(0, Math.Min(1, right));
        }

        public void SetDuration(int milliseconds)
        {
            _duration = milliseconds;
        }

        public SoundPlayer Play()
        {
            // Generate the sample data
            byte[] sampleData = GenerateSineWave(_leftFrequency, _rightFrequency, _duration, _leftGain, _rightGain);

            // Create the wave file header
            byte[] header = CreateWaveFileHeader(sampleData.Length);

            // Combine the header and sample data into a single array
            byte[] data = new byte[header.Length + sampleData.Length];
            Array.Copy(header, 0, data, 0, header.Length);
            Array.Copy(sampleData, 0, data, header.Length, sampleData.Length);

            // Create a new SoundPlayer instance
            SoundPlayer player = new SoundPlayer();

            // Set the data for the SoundPlayer to play
            player.Stream = new MemoryStream(data);

            // Play the sound asynchronously and loop
            player.PlayLooping();

            return player;
        }

        static byte[] GenerateSineWave(double frequencyLeft, double frequencyRight, int duration, double gainLeft, double gainRight)
        {
            const int sampleRate = 44100;
            
            // Calculate the number of samples needed
            int sampleCount = (sampleRate * duration) / 1000;

            // Create a new array to store the samples
            byte[] data = new byte[sampleCount * 4]; // 2 channels * 2 bytes per sample

            // Set the maximum amplitude based on gain
            double amplitudeLeft = short.MaxValue * gainLeft * 0.8; // 0.8 to avoid clipping
            double amplitudeRight = short.MaxValue * gainRight * 0.8;

            // Generate the samples
            for (int i = 0; i < sampleCount; i++)
            {
                // Calculate the sample values (sine waves)
                double timeInSeconds = (double)i / sampleRate;
                double sampleLeft = Math.Sin(2.0 * Math.PI * frequencyLeft * timeInSeconds);
                double sampleRight = Math.Sin(2.0 * Math.PI * frequencyRight * timeInSeconds);

                // Scale the sample values to the desired amplitude
                short valueLeft = (short)(amplitudeLeft * sampleLeft);
                short valueRight = (short)(amplitudeRight * sampleRight);

                // Convert the sample values to bytes and store in the array (little-endian)
                data[i * 4] = (byte)(valueLeft & 0xff);
                data[i * 4 + 1] = (byte)((valueLeft >> 8) & 0xff);
                data[i * 4 + 2] = (byte)(valueRight & 0xff);
                data[i * 4 + 3] = (byte)((valueRight >> 8) & 0xff);
            }

            return data;
        }

        static byte[] CreateWaveFileHeader(int dataSize)
        {
            // Set the wave file format (PCM, 16-bit, stereo)
            ushort formatTag = 1; // PCM
            ushort channels = 2; // Stereo
            uint samplesPerSecond = 44100;
            ushort bitsPerSample = 16;
            ushort blockAlign = (ushort)(channels * (bitsPerSample / 8));
            uint averageBytesPerSecond = samplesPerSecond * blockAlign;

            // Calculate the size of the wave file header
            int headerSize = 44;

            // Create a new array to store the wave file header
            byte[] header = new byte[headerSize];

            // Set the wave file header data
            Array.Copy(Encoding.ASCII.GetBytes("RIFF"), 0, header, 0, 4);
            Array.Copy(BitConverter.GetBytes((uint)(headerSize + dataSize - 8)), 0, header, 4, 4);
            Array.Copy(Encoding.ASCII.GetBytes("WAVE"), 0, header, 8, 4);
            Array.Copy(Encoding.ASCII.GetBytes("fmt "), 0, header, 12, 4);
            Array.Copy(BitConverter.GetBytes((uint)16), 0, header, 16, 4);
            Array.Copy(BitConverter.GetBytes(formatTag), 0, header, 20, 2);
            Array.Copy(BitConverter.GetBytes(channels), 0, header, 22, 2);
            Array.Copy(BitConverter.GetBytes(samplesPerSecond), 0, header, 24, 4);
            Array.Copy(BitConverter.GetBytes(averageBytesPerSecond), 0, header, 28, 4);
            Array.Copy(BitConverter.GetBytes(blockAlign), 0, header, 32, 2);
            Array.Copy(BitConverter.GetBytes(bitsPerSample), 0, header, 34, 2);
            Array.Copy(Encoding.ASCII.GetBytes("data"), 0, header, 36, 4);
            Array.Copy(BitConverter.GetBytes((uint)dataSize), 0, header, 40, 4);

            return header;
        }
    }
}