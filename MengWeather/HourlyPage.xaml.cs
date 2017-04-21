using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MengWeather.Model.ViewModel;
using MengWeather.Model.Weather.Displayed;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MengWeather
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HourlyPage : Page
    {
        public HourlyPage()
        {
            InitializeComponent();
            Model = new HourlyPageViewModel();
        }

        public HourlyPageViewModel Model { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var weatherDisplayed = e.Parameter as Weather_Displayed;
            if (weatherDisplayed == null)
                throw new Exception("Navigation Parameter is unavailable");
            Model.Hours = new ObservableCollection<WeatherUnit>(weatherDisplayed.HourlyForecates_48);
            Model.SelectedHour = Model.Hours[0];
            base.OnNavigatedTo(e);
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Model.SelectedHour = e.ClickedItem as WeatherUnit;
            Bindings.Update();
        }
    }
}