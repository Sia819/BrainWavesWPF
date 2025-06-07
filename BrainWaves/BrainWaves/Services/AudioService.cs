using System;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace BrainWaves.Services
{
    public class AudioService : ObservableObject
    {
        private static AudioService? _instance;
        private SoundPlayer? _soundPlayer;
        private bool _isPlaying;
        private double _currentLeftFrequency;
        private double _currentRightFrequency;
        private double _currentLeftGain;
        private double _currentRightGain;
        private CancellationTokenSource? _playbackCts;
        private readonly object _playbackLock = new object();

        public static AudioService Instance => _instance ??= new AudioService();

        public bool IsPlaying
        {
            get => _isPlaying;
            private set
            {
                if (SetProperty(ref _isPlaying, value))
                {
                    // 재생 상태가 변경되면 메시지 전송
                    WeakReferenceMessenger.Default.Send(new PlaybackStateChangedMessage(value));
                }
            }
        }

        public double CurrentLeftFrequency => _currentLeftFrequency;
        public double CurrentRightFrequency => _currentRightFrequency;
        public double CurrentLeftGain => _currentLeftGain;
        public double CurrentRightGain => _currentRightGain;

        private AudioService() { }

        public async void Play(double leftFrequency, double rightFrequency, double leftGain = 0.5, double rightGain = 0.5)
        {
            // 이전 재생 취소
            _playbackCts?.Cancel();
            _playbackCts = new CancellationTokenSource();
            var cts = _playbackCts;

            // 짧은 지연을 주어 빠른 클릭 시 마지막 클릭만 처리되도록 함
            try
            {
                await Task.Delay(50, cts.Token);
            }
            catch (TaskCanceledException)
            {
                return;
            }

            lock (_playbackLock)
            {
                if (cts.IsCancellationRequested) return;

                try
                {
                    // 기존 사운드 정지
                    _soundPlayer?.Stop();
                    _soundPlayer?.Dispose();
                    _soundPlayer = null;

                    // 새 사운드 생성 및 재생
                    var playSound = new PlaySound();
                    playSound.SetFrequencies(leftFrequency, rightFrequency);
                    playSound.SetGains(leftGain, rightGain);

                    _soundPlayer = playSound.Play();
                    _currentLeftFrequency = leftFrequency;
                    _currentRightFrequency = rightFrequency;
                    _currentLeftGain = leftGain * 100; // 0.0-1.0을 0-100으로 변환
                    _currentRightGain = rightGain * 100;
                    IsPlaying = true;

                    // 주파수 및 게인 변경 메시지 전송
                    WeakReferenceMessenger.Default.Send(new AudioParametersChangedMessage(leftFrequency, rightFrequency, _currentLeftGain, _currentRightGain));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error playing sound: {ex.Message}");
                    IsPlaying = false;
                }
            }
        }

        public void Stop()
        {
            _playbackCts?.Cancel();
            
            lock (_playbackLock)
            {
                _soundPlayer?.Stop();
                _soundPlayer?.Dispose();
                _soundPlayer = null;
                IsPlaying = false;
            }
        }
    }

    // 재생 상태 변경 메시지
    public class PlaybackStateChangedMessage
    {
        public bool IsPlaying { get; }

        public PlaybackStateChangedMessage(bool isPlaying)
        {
            IsPlaying = isPlaying;
        }
    }

    // 오디오 파라미터 변경 메시지
    public class AudioParametersChangedMessage
    {
        public double LeftFrequency { get; }
        public double RightFrequency { get; }
        public double LeftGain { get; }
        public double RightGain { get; }

        public AudioParametersChangedMessage(double leftFrequency, double rightFrequency, double leftGain, double rightGain)
        {
            LeftFrequency = leftFrequency;
            RightFrequency = rightFrequency;
            LeftGain = leftGain;
            RightGain = rightGain;
        }
    }
}