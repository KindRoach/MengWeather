using System;
using System.Collections.Generic;
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
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();
            updateText.Text = updateText.Text + "2017/1/27" + '\n';
            updateText.Text = updateText.Text + "原来的API接口挂掉了，续命中……" + '\n';
            updateText.Text = updateText.Text + '\n';
            updateText.Text = updateText.Text + "2016/11/6" + '\n';
            updateText.Text = updateText.Text + "1.更正了未来七天包括今天的错误" + '\n';
            updateText.Text = updateText.Text + "2.尝试修复闪退的bug，如果仍然闪退可卸载并重新安装应用" + '\n';
            updateText.Text = updateText.Text + '\n';
            updateText.Text = updateText.Text + "2016/10/1" + '\n';
            updateText.Text = updateText.Text + "1.添加了小时预报，因为api接口仅提供了温度和降水概率，所以比较简陋_(:з」∠)_" + '\n';
            updateText.Text = updateText.Text + "2.修复了跟新日志显示不全的bug" + '\n';
            updateText.Text = updateText.Text + '\n';
            updateText.Text = updateText.Text + "2016/9/16" + '\n';
            updateText.Text = updateText.Text + "1.修复了部分地区因为缺少空气质量数据而导致应用崩溃的bug" + '\n';
            updateText.Text = updateText.Text + '\n';
            updateText.Text = updateText.Text + "2016/9/16" + '\n';
            updateText.Text = updateText.Text + "1.后台优化（我默默改了好多代码，而你萌都看不到 ಥ_ಥ）" + '\n';
            updateText.Text = updateText.Text + "2.未来7天预报的气温更改为：最小值~最大值" + '\n';
            updateText.Text = updateText.Text + "3.点击生活贴士后，屏幕将自动滚动至低端" + '\n';
            updateText.Text = updateText.Text + "4.布局细节的调整" + '\n';
            updateText.Text = updateText.Text + '\n';
            updateText.Text = updateText.Text + "2016/9/6" + '\n';
            updateText.Text = updateText.Text + "1.修复了删除城市时确认对话框出现的乱码" + '\n';
            updateText.Text = updateText.Text + "2.更正了进入设置页面后返回按钮的说明文字" + '\n';
            updateText.Text = updateText.Text + "3.定位失败的信息可以选择不再提示" + '\n';
        }
    }
}