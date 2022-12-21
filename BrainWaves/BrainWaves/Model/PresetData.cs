using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainWaves.Model
{
    public class PresetData
    {
        public string PresetName { get; set; }
        public double LeftWave { get; set; }
        public double RightWave { get; set; }
        public double Resonance => Math.Abs(LeftWave - RightWave);
        public string WaveName
        {
            get
            {
                if (Math.Abs(LeftWave - RightWave) >= 42.01) return "Undifined";
                if (Math.Abs(LeftWave - RightWave) >= 38.01) return "Gamma";
                if (Math.Abs(LeftWave - RightWave) >= 12.01) return "Beta";
                if (Math.Abs(LeftWave - RightWave) >= 8.01) return "Alpha";
                if (Math.Abs(LeftWave - RightWave) >= 3.01) return "Theta";
                if (Math.Abs(LeftWave - RightWave) >= 0.51) return "Delta";
                if (Math.Abs(LeftWave - RightWave) >= 00.01) return "Infra-Low";
                else return "Mono";
            }
        }

        public PresetData(string presetName, double leftWave, double rightWave)
        {
            PresetName = presetName;
            LeftWave = leftWave;
            RightWave = rightWave;
        }
    }
}
