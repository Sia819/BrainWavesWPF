using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BrainWaves.Model
{
    public partial class PresetData : ObservableObject
    {
        [ObservableProperty]
        private string presetName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Resonance))]
        [NotifyPropertyChangedFor(nameof(WaveName))]
        private double leftWave;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Resonance))]
        [NotifyPropertyChangedFor(nameof(WaveName))]
        private double rightWave;

        public double Resonance => Math.Abs(LeftWave - RightWave);
        
        public string WaveName
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
                else return "Mono";
            }
        }

        public PresetData(string presetName, double leftWave, double rightWave)
        {
            this.presetName = presetName;
            this.leftWave = leftWave;
            this.rightWave = rightWave;
        }
    }
}