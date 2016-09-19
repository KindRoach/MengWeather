using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

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
                throw new Exception("Locating not allowed.");
            }
        }
    }
}