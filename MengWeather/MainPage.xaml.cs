using MengWeather.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MengWeather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public List<string> Suggestions { get; set; }
        public ObservableCollection<CityInfo> SearchResult { get; set; }
        public HashSet<string> AddedCity { get; set; }
        public ApplicationDataContainer LocalSetting { get; set; }
        public int LastPviotSelectedIndex { get; set; }
        public string ShowLocateDialogAnyMore { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            Suggestions = new List<string>();
            SearchResult = new ObservableCollection<CityInfo>();
            AddedCity = new HashSet<string>();
            LocalSetting = ApplicationData.Current.LocalSettings;
        }

        private async void _rootPage_Loaded(object sender, RoutedEventArgs e)
        {
            await CityManager.ReadData();
            CityInfo locatedCity = new Model.CityInfo() { ID = "null" };
            try
            {
                var pos = await LocationManager.GetLocation();
                var lat = pos.Coordinate.Point.Position.Latitude;
                var lon = pos.Coordinate.Point.Position.Longitude;
                locatedCity = CityManager.GetCity(lon, lat);
            }
            catch (Exception)
            {
                if (!LocalSetting.Values.ContainsKey(nameof(ShowLocateDialogAnyMore)))
                {
                    ShowLocateFailDialog();
                }
                else if (LocalSetting.Values[nameof(ShowLocateDialogAnyMore)].ToString() == "Yes")
                {
                    ShowLocateFailDialog();
                }
            }

            AddCityOnPivotWithWriteSetting(locatedCity);
            if (LocalSetting.Values.ContainsKey(nameof(AddedCity)))
            {
                List<CityInfo> cities = ReadSetting();
                foreach (var item in cities)
                {
                    if (item.ID != locatedCity.ID)
                    {
                        AddCityOnPivot(item);
                    }
                }
            }

            SettingFrame.Navigate(typeof(SettingPage));
        }

        private async void ShowLocateFailDialog()
        {
            var dialog = new ContentDialog();
            dialog.Title = $"定位失败，无法自动所在城市，请确保在打开设备的定位功能，并在设置中允许本应用的访问您的位置。" + '\n' + "您也可以手动添加城市。";
            dialog.PrimaryButtonText = "确认";
            dialog.SecondaryButtonText = "不再提醒";
            ContentDialogResult result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                ShowLocateDialogAnyMore = "Yes";
                LocalSetting.Values[nameof(ShowLocateDialogAnyMore)] = ShowLocateDialogAnyMore;
            }
            else
            {
                ShowLocateDialogAnyMore = "NoMore";
                LocalSetting.Values[nameof(ShowLocateDialogAnyMore)] = ShowLocateDialogAnyMore;
            }
        }

        private void AddCityOnPivot(CityInfo newCity)
        {
            var pivotItem = new PivotItem();
            var textBlock = new TextBlock();
            textBlock.Text = newCity.City;
            // 设置Header的字体颜色
            textBlock.Foreground = new SolidColorBrush(Colors.Gray);
            pivotItem.Header = textBlock;
            var cityPage = new CityPage(newCity);
            pivotItem.Content = cityPage;
            myPivot.Items.Add(pivotItem);
            AddedCity.Add(newCity.ID);
        }

        private async void AddCityOnPivotWithWriteSetting(CityInfo newCity)
        {
            if (newCity.ID == "null")
            {
                return;
            }
            if (AddedCity.Contains(newCity.ID))
            {
                await new MessageDialog("该城市已添加！").ShowAsync();
            }
            else
            {
                MyFlyout.Hide();
                AddCityOnPivot(newCity);
                myPivot.SelectedIndex = myPivot.Items.Count - 1;
                if (LocalSetting.Values.ContainsKey(nameof(AddedCity)))
                {
                    List<CityInfo> cities = ReadSetting();
                    cities.Add(newCity);
                    WriteSetting(cities);
                }
                else
                {
                    List<CityInfo> cities = new List<Model.CityInfo>();
                    cities.Add(newCity);
                    WriteSetting(cities);
                }
            }
        }

        public List<CityInfo> ReadSetting()
        {
            if (LocalSetting.Values.ContainsKey(nameof(AddedCity)))
            {
                string json = LocalSetting.Values[nameof(AddedCity)].ToString();
                var cities = JsonConvert.DeserializeObject<List<CityInfo>>(json);
                return cities;
            }
            else
            {
                throw new Exception("没有找到设置项");
            }
        }

        public void WriteSetting(List<CityInfo> cities)
        {
            string json = JsonConvert.SerializeObject(cities);
            LocalSetting.Values[nameof(AddedCity)] = json;
        }

        private void AppBar_Opening(object sender, object e)
        {
            RefreshButton.IsCompact = false;
            AddButton.IsCompact = false;
            DeleteButton.IsCompact = false;
            SettingButton.IsCompact = false;
        }

        private void AppBar_Closing(object sender, object e)
        {
            RefreshButton.IsCompact = true;
            AddButton.IsCompact = true;
            DeleteButton.IsCompact = true;
            SettingButton.IsCompact = true;
        }

        private async void Refresh(object sender, RoutedEventArgs e)
        {
            var item = myPivot.SelectedItem as PivotItem;
            var page = item.Content as CityPage;
            await page.RefreshWeather();
        }

        // 添加城市
        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                Suggestions = CityManager.Cities.
                    Where(x => x.City.StartsWith(sender.Text)).
                    Select(x => $"{x.City}({x.Prov})").ToList<string>();   //上海（直辖市）

                MyAutoSuggestBox.ItemsSource = Suggestions;
            }
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var resultList = CityManager.Cities.Where(x => $"{x.City}({x.Prov})".StartsWith(sender.Text)).ToList<CityInfo>();
            SearchResult.Clear();
            if (resultList.Count == 1)              //回车能够直接添加，而不需要再次点击listView
            {
                var city = resultList[0];
                AddCityOnPivotWithWriteSetting(resultList[0]);
            }
            else
            {
                foreach (var item in resultList)
                {
                    SearchResult.Add(item);
                }
            }
        }

        private void Flyout_Opened(object sender, object e)
        {
            MyAutoSuggestBox.Focus(FocusState.Keyboard);
            MyAutoSuggestBox.Text = "";
            SearchResult.Clear();
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var city = e.ClickedItem as CityInfo;
            AddCityOnPivotWithWriteSetting(city);
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (myPivot.SelectedIndex == 0)
            {
                var dialog = new ContentDialog();
                dialog.Title = $"不能删除第一个城市!";
                dialog.PrimaryButtonText = "确认";
                ContentDialogResult result = await dialog.ShowAsync();
            }
            else
            {
                var dialog = new ContentDialog();
                dialog.Title = $"确定从关注列表中删除{((myPivot.SelectedItem as PivotItem).Header as TextBlock).Text}？";
                dialog.PrimaryButtonText = "确认";
                dialog.SecondaryButtonText = "取消";
                ContentDialogResult result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    var selectedCity = ((myPivot.SelectedItem as PivotItem).Content as CityPage).City;
                    AddedCity.Remove(selectedCity.ID);
                    myPivot.Items.Remove(myPivot.SelectedItem);
                    if (LocalSetting.Values.ContainsKey(nameof(AddedCity)))
                    {
                        List<CityInfo> cities = ReadSetting();
                        for (int i = 0; i < cities.Count; i++)
                        {
                            if (cities[i].ID == selectedCity.ID)
                            {
                                cities.RemoveAt(i);
                            }
                        }
                        WriteSetting(cities);
                    }
                }
            }
        }

        // 切换城市后Header颜色分别作出明暗调整
        private void myPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pivotItem = myPivot.Items[LastPviotSelectedIndex] as PivotItem;
            if (pivotItem != null)
            {
                var textBlock = pivotItem.Header as TextBlock;
                textBlock.Foreground = new SolidColorBrush(Colors.Gray);
            }
            pivotItem = myPivot.SelectedItem as PivotItem;
            if (pivotItem != null)
            {
                var textBlock = pivotItem.Header as TextBlock;
                textBlock.Foreground = new SolidColorBrush(Colors.White);
            }

            LastPviotSelectedIndex = myPivot.SelectedIndex;
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            if (SettingFrame.Visibility == Visibility.Collapsed)
            {
                SettingFrame.Visibility = Visibility.Visible;
                (sender as AppBarButton).Icon = new SymbolIcon(Symbol.Back);
                SettingButton.Label = "返回";
            }
            else
            {
                SettingFrame.Visibility = Visibility.Collapsed;
                (sender as AppBarButton).Icon = new SymbolIcon(Symbol.Setting);
                SettingButton.Label = "设置";
            }
        }

        private void BackgroundGrid_Loaded(object sender, RoutedEventArgs e)
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