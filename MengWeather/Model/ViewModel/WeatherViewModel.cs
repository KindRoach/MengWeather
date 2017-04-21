using System.ComponentModel;
using MengWeather.Model.Weather.Displayed;

namespace MengWeather.Model.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private Weather_Displayed weather;

        public Weather_Displayed Weather
        {
            get => weather;
            set
            {
                weather = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Weather)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}