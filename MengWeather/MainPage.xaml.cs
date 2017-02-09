using MengWeather.Model;
using MengWeather.Model.ViewModel;
using MengWeather.Model.Weather.Displayed;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MengWeather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel Model { get; set; }
        public HashSet<CityInfo> AddedCity { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            Model = new MainPageViewModel();
            Model.CityList = new ObservableCollection<CityInfo>();
            AddedCity = new HashSet<CityInfo>();
        }

        private async void _rootPage_Loaded(object sender, RoutedEventArgs e)
        {
            progressRing.IsActive = true;
            ReadSetting();
            Geoposition pos = null;
            try
            {
                pos = await LocationManager.GetLocation();
            }
            catch (Exception)
            {
                LocationManager.ShowLocateFailDialog();
            }

            var lat = pos.Coordinate.Point.Position.Latitude;
            var lon = pos.Coordinate.Point.Position.Longitude;
            var locatedCity = await CityManager.GetCity(lon, lat);
            AddCity(locatedCity);
        }

        private void ReadSetting()
        {
            List<CityInfo> settingList = null;
            try
            {
                settingList = SettingManager.GetAddedCity();
            }
            catch (Exception)
            {
                return;
            }
            foreach (var item in settingList)
            {
                AddCity(item);
            }
        }

        private void splitViewButton_Click(object sender, RoutedEventArgs e)
        {
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }



        public async Task ChangeCity(CityInfo newCity)
        {
            TileManager.UpdateTile();
            progressRing.IsActive = true;
            Model.SelectedCity = newCity;
            Weather_Displayed weather = null;
            try
            {
                weather = await WeatherManager.GetWeather(newCity);
            }
            catch (Exception ex)
            {
                WeatherManager.ShowConnectFailDialog(ex);
                return;
            }
            finally
            {
                progressRing.IsActive = false;
            }
            var tuple = new Tuple<Weather_Displayed, MainPage>(weather, this);
            mainPageFrame.Navigate(typeof(WeatherPage), tuple);
        }

        public async void AddCity(CityInfo newCity)
        {
            if (AddedCity.Add(newCity))
            {
                Model.CityList.Add(newCity);
                SettingManager.SetAddedCity(new List<CityInfo>(AddedCity));
            }
            await ChangeCity(newCity);
        }

        private async void cityListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            splitView.IsPaneOpen = false;
            var clickedCity = e.ClickedItem as CityInfo;
            if (clickedCity == null)
            {
                throw new Exception("Clicked item is not CityInfo");
            }
            if (clickedCity.Equals(Model.SelectedCity)) return;
            await ChangeCity(clickedCity);
        }

        private async void buttonListview_ItemClick(object sender, ItemClickEventArgs e)
        {
            splitView.IsPaneOpen = false;
            var clickedItem = e.ClickedItem as StackPanel;
            if (clickedItem == null)
            {
                throw new Exception("Clicked item is not CityInfo");
            }
            if (clickedItem.Name == "AddItem")
            {
                mainPageFrame.Navigate(typeof(AddCityPage), this);
                cityNameTextBlock.Text = "";
            }
            else if (clickedItem.Name == "SettingItem")
            {
                mainPageFrame.Navigate(typeof(SettingPage));
                cityNameTextBlock.Text = "设置";
                goBackButton.Visibility = Visibility.Visible;
                splitViewButton.Visibility = Visibility.Collapsed;
            }
            else if (clickedItem.Name == "DeleteItem")
            {
                if (mainPageFrame.CurrentSourcePageType != typeof(WeatherPage)) return;
                var dialog = new ContentDialog();
                dialog.Title = $"确定从关注列表中删除 {Model.SelectedCity.City}？";
                if (Model.CityList.Count == 0) dialog.Title = "关注列表已为空";
                dialog.PrimaryButtonText = "确认";
                dialog.SecondaryButtonText = "取消";
                ContentDialogResult result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    Model.CityList.Remove(Model.SelectedCity);
                    AddedCity.Remove(Model.SelectedCity);
                    SettingManager.SetAddedCity(Model.CityList.ToList());
                    if (Model.CityList.Count == 0)
                    {
                        mainPageFrame.Navigate(typeof(AddCityPage), this);
                        Model.SelectedCity = new CityInfo() { City = "" };
                    }
                    else
                        await ChangeCity(Model.CityList[0]);
                }
            }
            else // Refresh
            {
                if (mainPageFrame.CurrentSourcePageType != typeof(WeatherPage)) return;
                await ChangeCity(Model.SelectedCity);
            }
        }


        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            mainPageFrame.GoBack();
            cityNameTextBlock.Text = Model.SelectedCity.City;
            goBackButton.Visibility = Visibility.Collapsed;
            splitViewButton.Visibility = Visibility.Visible;
        }

        public void GoHourlyPage(Weather_Displayed weatherDisplayed)
        {
            mainPageFrame.Navigate(typeof(HourlyPage), weatherDisplayed);
            goBackButton.Visibility = Visibility.Visible;
            splitViewButton.Visibility = Visibility.Collapsed;
            cityNameTextBlock.Text += "(未来48小时)";
            splitView.IsPaneOpen = false;
        }

        /// <summary>
        /// 设置状态栏颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                StatusBar statusBar = StatusBar.GetForCurrentView();
                statusBar.ForegroundColor = Colors.White;
                var gridBackground = (sender as Grid).Background;
                statusBar.BackgroundColor = ((SolidColorBrush)gridBackground).Color;
                statusBar.BackgroundOpacity = 1;
            }
        }
    }
}