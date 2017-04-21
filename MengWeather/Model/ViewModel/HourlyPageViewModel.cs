using System.Collections.ObjectModel;
using System.ComponentModel;
using MengWeather.Model.Weather.Displayed;

namespace MengWeather.Model.ViewModel
{
    public class HourlyPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<WeatherUnit> hours;

        private WeatherUnit selectedHour;

        public ObservableCollection<WeatherUnit> Hours
        {
            get => hours;
            set
            {
                hours = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Hours)));
            }
        }

        public WeatherUnit SelectedHour
        {
            get => selectedHour;
            set
            {
                selectedHour = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedHour)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}