using MengWeather.Model.Weather.Displayed;
using System.ComponentModel;

namespace MengWeather.Model.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Weather_Displayed weather;

        public Weather_Displayed Weather
        {
            get { return weather; }
            set
            {
                weather = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Weather)));
            }
        }
    }
}