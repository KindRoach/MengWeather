using MengWeather.Model;
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
    public sealed partial class AddCityPage : Page
    {
        public List<CityInfo> AllCities { get; set; }
        public List<string> SearchSuggestion { get; set; }
        public ObservableCollection<CityInfo> SearchResult { get; set; }
        public MainPage ParentPage { get; set; }
        public AddCityPage()
        {
            this.InitializeComponent();
            SearchResult = new ObservableCollection<Model.CityInfo>();
            SearchSuggestion = new List<string>();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AllCities = await CityManager.GetAllCities();
            autoSuggestBox.Focus(FocusState.Keyboard);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ParentPage = e.Parameter as MainPage;
            if (ParentPage == null)
            {
                throw new Exception("Navigation Parameter is unavailable");
            }
            base.OnNavigatedTo(e);
        }

        private void autoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                SearchSuggestion = AllCities.
                    Where(x => x.City.StartsWith(sender.Text)).
                    Select(x => $"{x.City}({x.Prov})").ToList<string>();   //上海（直辖市）
                autoSuggestBox.ItemsSource = SearchSuggestion;
            }
        }

        private void autoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var resultList = AllCities.Where(x => $"{x.City}({x.Prov})".StartsWith(sender.Text)).ToList<CityInfo>();
            SearchResult.Clear();
            if (resultList.Count == 1)              //回车能够直接添加，而不需要再次点击listView
            {
                var resultCity = resultList[0];
                ParentPage.AddCity(resultCity);
            }
            else
            {
                foreach (var item in resultList)
                {
                    SearchResult.Add(item);
                }
            }
        }

        private void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var resultCity = e.ClickedItem as CityInfo;
            if (resultCity == null)
            {
                throw new Exception("SelextItem is not CityInfo");
            }
            ParentPage.AddCity(resultCity);
        }
    }
}
