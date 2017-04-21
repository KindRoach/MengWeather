using System;
using System.Collections.Generic;

namespace MengWeather.Model.Weather.Displayed
{
    public class Weather_Displayed
    {
        /// <summary>
        ///     最后更新时间
        /// </summary>
        public string UpdateTime { get; set; }

        /// <summary>
        ///     实时天气
        /// </summary>
        public WeatherUnit Now { get; set; }

        /// <summary>
        ///     8小时级别预报，时间只显示小时
        /// </summary>
        public List<WeatherUnit> HourlyForecates_8 { get; set; }

        /// <summary>
        ///     48小时级别预报，时间显示完整日期
        /// </summary>
        public List<WeatherUnit> HourlyForecates_48 { get; set; }

        /// <summary>
        ///     未来5天预报
        /// </summary>
        public List<WeatherUnit> DailyForecates { get; set; }

        /// <summary>
        ///     当天的生活贴士
        /// </summary>
        public List<Suggestion> Suggestions { get; set; }
    }

    public class WeatherUnit
    {
        /// <summary>
        ///     温度
        /// </summary>
        public string Temperature { get; set; }

        /// <summary>
        ///     天气概况
        /// </summary>
        public string Skycon { get; set; }

        /// <summary>
        ///     空气质量
        /// </summary>
        public string Aqi { get; set; }

        /// <summary>
        ///     相对湿度
        /// </summary>
        public string Humidity { get; set; }

        /// <summary>
        ///     PM2.5
        /// </summary>
        public string Pm25 { get; set; }

        /// <summary>
        ///     降水强度
        /// </summary>
        public string Precipitation { get; set; }

        /// <summary>
        ///     风
        /// </summary>
        public string Wind { get; set; }

        /// <summary>
        ///     时间
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        ///     icon图标路径
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///     完整的日期和时间
        /// </summary>
        public DateTime FullDateTime { get; set; }

        /// <summary>
        ///     日出时间 hh：mm
        /// </summary>
        public string SunRise { get; set; }

        /// <summary>
        ///     日落时间 hh：mm
        /// </summary>
        public string SunSet { get; set; }
    }

    public class Suggestion
    {
        /// <summary>
        ///     贴士名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     概述
        /// </summary>
        public string Brf { get; set; }

        /// <summary>
        ///     详细描述
        /// </summary>
        public string Txt { get; set; }

        /// <summary>
        ///     Icon文件名
        /// </summary>
        public string Icon { get; set; }
    }
}