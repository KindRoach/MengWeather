using System;
using System.Collections.ObjectModel;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using MengWeather.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MengWeather
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            InitializeComponent();
            pivot.SelectedIndex = 0;
            ReadSetting();
            WriteUpdateLog();
        }

        public int LastPviotSelectedIndex { get; set; }
        public ObservableCollection<CityInfo> AddedCity { get; set; }
        public CityInfo TileCity { get; set; }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pivotItem = pivot.Items[LastPviotSelectedIndex] as PivotItem;
            if (pivotItem != null)
            {
                var textBlock = pivotItem.Header as TextBlock;
                textBlock.Foreground = new SolidColorBrush(Colors.Gray);
            }
            pivotItem = pivot.SelectedItem as PivotItem;
            if (pivotItem != null)
            {
                var textBlock = pivotItem.Header as TextBlock;
                textBlock.Foreground = new SolidColorBrush(Colors.White);
            }
            LastPviotSelectedIndex = pivot.SelectedIndex;
        }

        private void WriteUpdateLog()
        {
            updateText.Text = updateText.Text + "2017/4/21" + Environment.NewLine;
            updateText.Text = updateText.Text + "1.添加了对WinPhone后退键的支持" + Environment.NewLine;
            updateText.Text = updateText.Text + "2.优化了页面导航逻辑" + Environment.NewLine;
            updateText.Text = updateText.Text + Environment.NewLine;
            updateText.Text = updateText.Text + "2017/3/9" + Environment.NewLine;
            updateText.Text = updateText.Text + "1.UI布局调整，修复了部分页面显示不全、内容重叠的bug" + Environment.NewLine;
            updateText.Text = updateText.Text + Environment.NewLine;
            updateText.Text = updateText.Text + "2017/2/8" + Environment.NewLine;
            updateText.Text = updateText.Text + "1.更换了新的API，现在已经可以提供未来48小时的小时级预报" + Environment.NewLine;
            updateText.Text = updateText.Text + "2.重新设计UI布局，增加了汉堡菜单，避免了原来切换城市与滑动“未来7天”的操作冲突" + Environment.NewLine;
            updateText.Text = updateText.Text + "3.实现了下拉刷新功能" + Environment.NewLine;
            updateText.Text = updateText.Text + "4.现已可以设置磁贴显示地点" + Environment.NewLine;
            updateText.Text = updateText.Text + Environment.NewLine;
            updateText.Text = updateText.Text + "2016/11/6" + Environment.NewLine;
            updateText.Text = updateText.Text + "1.更正了未来七天包括今天的错误" + Environment.NewLine;
            updateText.Text = updateText.Text + "2.尝试修复闪退的bug，如果仍然闪退可卸载并重新安装应用" + Environment.NewLine;
            updateText.Text = updateText.Text + Environment.NewLine;
            updateText.Text = updateText.Text + "2016/10/1" + Environment.NewLine;
            updateText.Text = updateText.Text + "1.添加了小时预报，因为api接口仅提供了温度和降水概率，所以比较简陋_(:з」∠)_" + Environment.NewLine;
            updateText.Text = updateText.Text + "2.修复了跟新日志显示不全的bug" + Environment.NewLine;
            updateText.Text = updateText.Text + Environment.NewLine;
            updateText.Text = updateText.Text + "2016/9/16" + Environment.NewLine;
            updateText.Text = updateText.Text + "1.修复了部分地区因为缺少空气质量数据而导致应用崩溃的bug" + Environment.NewLine;
            updateText.Text = updateText.Text + Environment.NewLine;
            updateText.Text = updateText.Text + "2016/9/16" + Environment.NewLine;
            updateText.Text = updateText.Text + "1.后台优化（我默默改了好多代码，而你萌都看不到 ಥ_ಥ）" + Environment.NewLine;
            updateText.Text = updateText.Text + "2.未来7天预报的气温更改为：最小值~最大值" + Environment.NewLine;
            updateText.Text = updateText.Text + "3.点击生活贴士后，屏幕将自动滚动至低端" + Environment.NewLine;
            updateText.Text = updateText.Text + "4.布局细节的调整" + Environment.NewLine;
            updateText.Text = updateText.Text + Environment.NewLine;
            updateText.Text = updateText.Text + "2016/9/6" + Environment.NewLine;
            updateText.Text = updateText.Text + "1.修复了删除城市时确认对话框出现的乱码" + Environment.NewLine;
            updateText.Text = updateText.Text + "2.更正了进入设置页面后返回按钮的说明文字" + Environment.NewLine;
            updateText.Text = updateText.Text + "3.定位失败的信息可以选择不再提示" + Environment.NewLine;
        }

        private void ReadSetting()
        {
            try
            {
                AddedCity = new ObservableCollection<CityInfo>(SettingManager.GetAddedCity());
                TileCity = SettingManager.GetTileCity();
            }
            catch (Exception)
            {
                AddedCity = new ObservableCollection<CityInfo>();
            }
            finally
            {
                AddedCity.Add(new CityInfo {City = "自动定位"});
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectCity = comboBox.SelectedItem as CityInfo;
            if (selectCity == null)
                throw new Exception("Clicked item is not CityInfo");
            SettingManager.SetTileCity(selectCity);
            tileCityTextBlock.Text = "设置已保存";
            if (selectCity.City == "自动定位")
                tileCityTextBlock.Text += Environment.NewLine + "请确保打开定位功能";
        }
    }
}