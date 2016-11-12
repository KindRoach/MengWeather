using MengWeather.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Popups;
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
        public CityInfo City { get; set; }
        public Pivot FatherPivot { get; set; }

        public CityPage(CityInfo newCity)
        {
            this.InitializeComponent();
            City = newCity;
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

            try
            {
                Data.Weather = await WeatherManager.GetWeather(City);
            }
            catch (Exception exp)
            {
                await new MessageDialog("数据错误，请稍后刷新 " + exp.Message).ShowAsync();
                return;
            }

            this.Bindings.Update();
            MyProgressRing.IsActive = false;

            MyScrollViewer.Visibility = Visibility.Visible;

            // Update Tiles
            var tileXml = await GetTileXml();

            XmlNodeList tileTextAttributes = tileXml.GetElementsByTagName("text");
            int i = 0;
            foreach (IXmlNode item in tileTextAttributes)
            {
                if (i % 2 == 0)
                {
                    item.InnerText = Data.Weather.City.City;
                }
                else
                {
                    item.InnerText = Data.Weather.Now.Tmp;
                }
                i++;
            }
            XmlNodeList tileImageAttributes = tileXml.GetElementsByTagName("image");
            foreach (IXmlNode item in tileImageAttributes)
            {
                (item as XmlElement).SetAttribute("src", Data.Weather.Now.Icon);
            }
            var tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }

        public static async Task<XmlDocument> GetTileXml()
        {
            var uri = new Uri("ms-appx:///Assets/TilesTemplate.xml");
            var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            var tileXml = await XmlDocument.LoadFromFileAsync(file);
            return tileXml;
        }

        private async void TipsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var suggestion = e.ClickedItem as MySuggestion;
            TipsTextBlock.Text = suggestion.Brief + '\n' + suggestion.Txt;
            if (suggestion.Name == "防晒指数")
            {
                TipsTextBlock.Text = "紫外线" + TipsTextBlock.Text;
            }
            // 好像scrollView高度的调节有延迟，所以立马下滚没有反应，故延迟100ms
            await Task.Delay(100);
            MyScrollViewer.ChangeView(null, MyScrollViewer.VerticalOffset + 1000, null);
        }

        private void WeatherGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var weather = e.ClickedItem as DailyForecast;
            WeatherInfoStackPanel.Visibility = Visibility.Visible;
            RainTextBlock.Text = weather.Pop;
            WindTextBlock.Text = weather.Wind;
            HumTextBlock.Text = weather.Hum;
            SrTextBlock.Text = weather.Sr;
            SsTextBlock.Text = weather.Ss;
        }

    }

    public class WeatherViewModel : INotifyPropertyChanged
    {
        private MyWeather weather;

        public MyWeather Weather
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