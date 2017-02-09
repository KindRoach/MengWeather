using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls;
using MengWeather.Model;

namespace MengWeather.Model
{
    public static class LocationManager
    {
        public static async Task<Geoposition> GetLocation()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            if (accessStatus == GeolocationAccessStatus.Allowed)
            {
                Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = 1000 };
                Geoposition pos = await geolocator.GetGeopositionAsync();
                return pos;
            }
            else
            {
                throw new Exception("Locating Failed");
            }
        }

        public static async void ShowLocateFailDialog()
        {
            if (!SettingManager.ShouldShowLFD()) return;
            var dialog = new ContentDialog();
            dialog.Title = $"定位失败，无法自动添加所在城市，请确保在打开设备的定位功能，并在设置中允许本应用的访问您的位置。" + '\n' + "您也可以手动添加城市。";
            dialog.PrimaryButtonText = "确认";
            dialog.SecondaryButtonText = "不再提醒";
            ContentDialogResult result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Secondary)
            {
                SettingManager.SetShouldShowLFD(false);
            }
            else
            {
                SettingManager.SetShouldShowLFD(true);
            }
        }
    }
}