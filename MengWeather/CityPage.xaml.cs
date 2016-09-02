using MengWeather.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class CityPage : Page
    {
        public WeatherViewModel Data { get; set; }
        public string CityID { get; set; }

        public CityPage(string newCityID)
        {
            this.InitializeComponent();
            CityID = newCityID;
            Data = new WeatherViewModel();
            WeatherInfoStackPanel.Visibility = Visibility.Collapsed;
        }

        private async void weatherPage_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshWeather();
        }

        public async Task RefreshWeather()
        {
            MyScrollViewer.Visibility = Visibility.Collapsed;
            MyProgressRing.IsActive = true;
            Data.Weather = await WeatherManager.GetWeather(CityID);
            this.Bindings.Update();
            MyProgressRing.IsActive = false;
            MyScrollViewer.Visibility = Visibility.Visible;
        }

        private void TipsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var suggestion = e.ClickedItem as OneSuggestion;
            TipsTextBlock.Text = suggestion.Brief + '\n' + suggestion.Txt;
            if (suggestion.Name == "防晒指数")
            {
                TipsTextBlock.Text = "紫外线" + TipsTextBlock.Text;
            }
        }

        private void WeatherGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var weather = e.ClickedItem as Daily_forecast;
            WeatherInfoStackPanel.Visibility = Visibility.Visible;
            RainTextBlock.Text = "降雨概率：" + weather.Pop + "%";
            WindTextBlock.Text = "风向：" + weather.Wind.Dir;
            HumTextBlock.Text = "湿度：" + weather.Hum + "%";
            SrTextBlock.Text = weather.Astro.Sr;
            SsTextBlock.Text = weather.Astro.Ss;

        }
    }

    public class WeatherViewModel : INotifyPropertyChanged
    {
        private HeWeather weather;

        public HeWeather Weather
        {
            get { return weather; }
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
