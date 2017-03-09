using MengWeather.Model.ViewModel;
using MengWeather.Model.Weather.Displayed;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MengWeather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WeatherPage : Page
    {
        public WeatherViewModel Model { get; set; }
        public MainPage ParentPage { get; set; }

        public WeatherPage()
        {
            this.InitializeComponent();
            Model = new WeatherViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var tuple = e.Parameter as Tuple<Weather_Displayed, MainPage>;
            if (tuple == null)
            {
                throw new Exception("Navigation Parameter is unavailable");
            }
            Model.Weather = tuple.Item1;
            ParentPage = tuple.Item2;
            this.Bindings.Update();
            base.OnNavigatedTo(e);
        }

        private void dailyGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var weatherUnit = e.ClickedItem as WeatherUnit;
            if (weatherUnit == null)
            {
                dailyInfoStackPanel.Visibility = Visibility.Collapsed;
                return;
            }
            rainTextBlock.Text = weatherUnit.Precipitation;
            windTextBlock.Text = weatherUnit.Wind;
            humTextBlock.Text = weatherUnit.Humidity;
            AqiTextBlock.Text = weatherUnit.Aqi;
            ssTextBlock.Text = weatherUnit.SunSet;
            srTextBlock.Text = weatherUnit.SunRise;
            dailyInfoStackPanel.Visibility = Visibility.Visible;
        }

        private async void tipsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var suggestion = e.ClickedItem as Suggestion;
            if (suggestion == null)
            {
                tipsTextBlock.Visibility = Visibility.Collapsed;
                return;
            }
            tipsTextBlock.Text = suggestion.Txt;
            tipsTextBlock.Visibility = Visibility.Visible;
            await Task.Delay(200);
            scrollViewer.ChangeView(null, scrollViewer.VerticalOffset + 1000, null);
        }

        private void hourlyButton_Click(object sender, RoutedEventArgs e)
        {
            ParentPage.GoHourlyPage(Model.Weather);
        }

        private async void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            await ParentPage.ChangeCity(ParentPage.Model.SelectedCity);
        }
    }
}