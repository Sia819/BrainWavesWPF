using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace BrainWaves
{
    internal class PlaySound
    {
        public PlaySound()
        {
            // Set the desired frequency in Hz
            int frequencyLeft = 75;
            int frequencyRight = 73;

            // Set the duration of the sound in milliseconds
            int duration = 100;

            // Set the volume of the sound (range 0-100)
            int volume = 5;

            // Generate the sample data
            byte[] sampleData = GenerateSineWave(frequencyLeft, frequencyRight, duration, volume);

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

            // Play the sound
            player.PlaySync();
        }

        static byte[] GenerateSineWave(int frequencyLeft, int frequencyRight, int duration, int volume)
        {
            // Calculate the number of samples needed
            int sampleCount = (44100 * duration) / 1000;

            // Create a new array to store the samples
            byte[] data = new byte[sampleCount * 4];

            // Set the volume of the sound
            double amplitude = (short.MaxValue * volume) / 100;

            // Set the period of the waves (2 * PI / frequency)
            double periodLeft = (2 * Math.PI) / 44100;
            double periodRight = (2 * Math.PI) / 44100;

            // Generate the samples
            for (int i = 0; i < sampleCount; i++)
            {
                // Calculate the sample values (sine waves)
                double sampleLeft = Math.Sin(frequencyLeft * periodLeft * i);
                double sampleRight = Math.Sin(frequencyRight * periodRight * i);

                // Scale the sample values to the desired amplitude
                short valueLeft = (short)(amplitude * sampleLeft);
                short valueRight = (short)(amplitude * sampleRight);

                // Convert the sample values to bytes and store in the array
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
            ushort formatTag = 1;
            ushort channels = 2;
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