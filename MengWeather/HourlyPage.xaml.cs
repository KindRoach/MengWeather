using MengWeather.Model;
using MengWeather.Model.ViewModel;
using MengWeather.Model.Weather.Displayed;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MengWeather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HourlyPage : Page
    {
        public HourlyPageViewModel Model { get; set; }
        public HourlyPage()
        {
            this.InitializeComponent();
            Model = new HourlyPageViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var weatherDisplayed = e.Parameter as Weather_Displayed;
            if (weatherDisplayed == null)
            {
                throw new Exception("Navigation Parameter is unavailable");
            }
            Model.Hours = new ObservableCollection<WeatherUnit>(weatherDisplayed.HourlyForecates_48);
            Model.SelectedHour = Model.Hours[0];
            base.OnNavigatedTo(e);
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Model.SelectedHour = e.ClickedItem as WeatherUnit;
            this.Bindings.Update();
        }
    }
}
