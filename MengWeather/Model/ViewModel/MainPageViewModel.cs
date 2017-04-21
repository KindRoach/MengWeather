using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MengWeather.Model.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<CityInfo> cityList;

        private CityInfo selectedCity;

        public ObservableCollection<CityInfo> CityList
        {
            get => cityList;
            set
            {
                cityList = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CityList)));
            }
        }

        public CityInfo SelectedCity
        {
            get => selectedCity;
            set
            {
                selectedCity = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCity)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}