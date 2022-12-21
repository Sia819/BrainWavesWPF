using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BrainWaves.Model;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Input;

namespace BrainWaves.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public Uri ShowingPageName { get; set; }
        public RelayCommand<string> NavigateFrame { get; set; }

        public ObservableCollection<PresetData> PresetList { get; set; }

        public MainViewModel()
        {
            ShowingPageName = new Uri("pack://application:,,,/View/Waves.xaml");
            NavigateFrame = new(d => NavigateFrame_Command(d));
            PresetList = new ObservableCollection<PresetData>();

            PresetList.Add(new("Visualization", 101.08, 109.75));
            PresetList.Add(new("Createive", 110.72, 138.85));
            PresetList.Add(new("Focus", 133.00, 162.00));
            PresetList.Add(new("Work", 194.66, 181.33));
            PresetList.Add(new("Concentrate", 158.44, 144.98));
            PresetList.Add(new("Healing", 80.72, 82.22));
            PresetList.Add(new("Sleep", 75.00, 73.00));
            PresetList.Add(new("Deep Sleep", 55.96, 54.33));
            PresetList.Add(new("Perception", 140.00, 100.43));
            PresetList.Add(new("Cognitive tasks", 340.00, 300.00));
            PresetList.Add(new("InfraLow", 89.00, 89.35));
            PresetList.Add(new("Meditation", 85.25, 89.75));
            PresetList.Add(new("Relax", 95.66, 100.22));
        }

        private void NavigateFrame_Command(string? pageName)
        {
            if (pageName is null) return;
            if (Uri.TryCreate(pageName, UriKind.Absolute, out Uri? result))
            {
                ShowingPageName = result;
            }

        }
    }

}

