using System;
using System.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BrainWaves.ViewModel
{
    public partial class WavesViewModel : ObservableObject
    {
        private SoundPlayer? _soundPlayer;
        private bool _isPlaying;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Resonance))]
        [NotifyPropertyChangedFor(nameof(WaveType))]
        [NotifyPropertyChangedFor(nameof(WaveDescription))]
        private double leftFrequency = 75.0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Resonance))]
        [NotifyPropertyChangedFor(nameof(WaveType))]
        [NotifyPropertyChangedFor(nameof(WaveDescription))]
        private double rightFrequency = 73.0;

        [ObservableProperty]
        private double leftGain = 50.0;

        [ObservableProperty]
        private double rightGain = 50.0;

        [ObservableProperty]
        private string playButtonText = "Play";

        [ObservableProperty]
        private string playButtonIcon = "Play";

        public double Resonance => Math.Abs(LeftFrequency - RightFrequency);

        public string WaveType
        {
            get
            {
                double resonance = Resonance;
                if (resonance >= 42.01) return "Undefined";
                if (resonance >= 38.01) return "Gamma";
                if (resonance >= 12.01) return "Beta";
                if (resonance >= 8.01) return "Alpha";
                if (resonance >= 3.01) return "Theta";
                if (resonance >= 0.51) return "Delta";
                if (resonance >= 0.01) return "Infra-Low";
                return "Mono";
            }
        }

        public string WaveDescription
        {
            get
            {
                return WaveType switch
                {
                    "Gamma" => "High Focus",
                    "Beta" => "Active Thinking",
                    "Alpha" => "Relaxation",
                    "Theta" => "Meditation",
                    "Delta" => "Sleep",
                    "Infra-Low" => "Deep Healing",
                    "Mono" => "No Effect",
                    _ => "Unknown"
                };
            }
        }

        [RelayCommand]
        private void IncreaseLeftFrequency()
        {
            if (LeftFrequency < 1000)
                LeftFrequency = Math.Round(LeftFrequency + 0.01, 2);
        }

        [RelayCommand]
        private void DecreaseLeftFrequency()
        {
            if (LeftFrequency > 0.01)
                LeftFrequency = Math.Round(LeftFrequency - 0.01, 2);
        }

        [RelayCommand]
        private void IncreaseRightFrequency()
        {
            if (RightFrequency < 1000)
                RightFrequency = Math.Round(RightFrequency + 0.01, 2);
        }

        [RelayCommand]
        private void DecreaseRightFrequency()
        {
            if (RightFrequency > 0.01)
                RightFrequency = Math.Round(RightFrequency - 0.01, 2);
        }

        [RelayCommand]
        private void TogglePlay()
        {
            if (_isPlaying)
            {
                StopSound();
            }
            else
            {
                PlaySound();
            }
        }

        private void PlaySound()
        {
            try
            {
                _soundPlayer?.Stop();
                _soundPlayer?.Dispose();

                var playSound = new PlaySound();
                playSound.SetFrequencies(LeftFrequency, RightFrequency);
                playSound.SetGains(LeftGain / 100.0, RightGain / 100.0);
                
                _soundPlayer = playSound.Play();
                _isPlaying = true;
                PlayButtonText = "Stop";
                PlayButtonIcon = "Stop";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error playing sound: {ex.Message}");
            }
        }

        private void StopSound()
        {
            _soundPlayer?.Stop();
            _soundPlayer?.Dispose();
            _soundPlayer = null;
            _isPlaying = false;
            PlayButtonText = "Play";
            PlayButtonIcon = "Play";
        }

        [RelayCommand]
        private void ShowTimer()
        {
            // Timer 기능은 나중에 구현
        }
    }
}