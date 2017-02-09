using MengWeather.Model.Weather.Displayed;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MengWeather.Model.ViewModel
{
    public class HourlyPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<WeatherUnit> hours;
        public ObservableCollection<WeatherUnit> Hours
        {
            get { return hours; }
            set
            {
                hours = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Hours)));
            }
        }

        private WeatherUnit selectedHour;

        public WeatherUnit SelectedHour
        {
            get { return selectedHour; }
            set
            {
                selectedHour = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedHour)));
            }
        }


    }
}
