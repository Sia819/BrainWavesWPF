using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace BrainWaves
{
    internal class PlaySound : IDisposable
    {
        private double _leftFrequency = 75.0;
        private double _rightFrequency = 73.0;
        private double _leftGain = 0.5;
        private double _rightGain = 0.5;
        private IWavePlayer? _waveOut;
        private BinauralBeatProvider? _binauralBeatProvider;

        public void SetFrequencies(double left, double right)
        {
            _leftFrequency = left;
            _rightFrequency = right;
            
            if (_binauralBeatProvider != null)
            {
                _binauralBeatProvider.SetFrequencies(left, right);
            }
        }

        public void SetGains(double left, double right)
        {
            _leftGain = Math.Max(0, Math.Min(1, left));
            _rightGain = Math.Max(0, Math.Min(1, right));
            
            if (_binauralBeatProvider != null)
            {
                _binauralBeatProvider.SetGains(_leftGain, _rightGain);
            }
        }

        public void Play()
        {
            Stop();

            _binauralBeatProvider = new BinauralBeatProvider(_leftFrequency, _rightFrequency, _leftGain, _rightGain);
            _waveOut = new WaveOutEvent
            {
                DesiredLatency = 100 // 100ms latency for smooth playback
            };
            
            _waveOut.Init(_binauralBeatProvider);
            _waveOut.Play();
        }

        public void Stop()
        {
            _waveOut?.Stop();
            _waveOut?.Dispose();
            _waveOut = null;
            _binauralBeatProvider = null;
        }

        public void Dispose()
        {
            Stop();
        }
    }

    internal class BinauralBeatProvider : ISampleProvider
    {
        private readonly object _lockObject = new object();
        private double _leftFrequency;
        private double _rightFrequency;
        private double _leftGain;
        private double _rightGain;
        private double _leftPhase;
        private double _rightPhase;

        public WaveFormat WaveFormat { get; } = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);

        public BinauralBeatProvider(double leftFrequency, double rightFrequency, double leftGain, double rightGain)
        {
            _leftFrequency = leftFrequency;
            _rightFrequency = rightFrequency;
            _leftGain = leftGain;
            _rightGain = rightGain;
        }

        public void SetFrequencies(double left, double right)
        {
            lock (_lockObject)
            {
                _leftFrequency = left;
                _rightFrequency = right;
            }
        }

        public void SetGains(double left, double right)
        {
            lock (_lockObject)
            {
                _leftGain = left;
                _rightGain = right;
            }
        }

        public int Read(float[] buffer, int offset, int count)
        {
            lock (_lockObject)
            {
                for (int i = 0; i < count; i += 2)
                {
                    // Generate left channel sample
                    buffer[offset + i] = (float)(_leftGain * 0.8 * Math.Sin(_leftPhase));
                    _leftPhase += 2 * Math.PI * _leftFrequency / WaveFormat.SampleRate;
                    if (_leftPhase > 2 * Math.PI)
                        _leftPhase -= 2 * Math.PI;

                    // Generate right channel sample
                    buffer[offset + i + 1] = (float)(_rightGain * 0.8 * Math.Sin(_rightPhase));
                    _rightPhase += 2 * Math.PI * _rightFrequency / WaveFormat.SampleRate;
                    if (_rightPhase > 2 * Math.PI)
                        _rightPhase -= 2 * Math.PI;
                }
            }

            return count;
        }
    }
}