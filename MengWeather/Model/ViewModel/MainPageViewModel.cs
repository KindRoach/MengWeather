using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MengWeather.Model.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<CityInfo> cityList;
        public ObservableCollection<CityInfo> CityList
        {
            get { return cityList; }
            set
            {
                cityList = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CityList)));
            }
        }


        private CityInfo selectedCity;

        public CityInfo SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCity)));
            }
        }

    }
}
