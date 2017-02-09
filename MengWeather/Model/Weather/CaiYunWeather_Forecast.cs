using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MengWeather.Model.Weather.Forecast
{
    public class CaiYunWeather_Forecast
    {
        /// <summary>
        /// 请求状态
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        [JsonProperty("lang")]
        public string Lang { get; set; }
        /// <summary>
        /// 请求结果，包括小时级，分钟级，天级预报
        /// </summary>
        [JsonProperty("result")]
        public Result Result { get; set; }
        /// <summary>
        /// Unix时间戳
        /// </summary>
        [JsonProperty("server_time")]
        public int ServerTime { get; set; }
        /// <summary>
        /// 空气质量
        /// </summary>
        [JsonProperty("api_status")]
        public string ApiStatus { get; set; }
        /// <summary>
        /// 时区偏移秒数，东八区为28800秒，即8小时
        /// </summary>
        [JsonProperty("tzshift")]
        public int Tzshift { get; set; }
        /// <summary>
        /// api版本号
        /// </summary>
        [JsonProperty("api_version")]
        public string ApiVersion { get; set; }
        /// <summary>
        /// 单位，metric表示米制
        /// </summary>
        [JsonProperty("unit")]
        public string Unit { get; set; }
        /// <summary>
        /// 经纬度坐标
        /// </summary>
        [JsonProperty("location")]
        public List<double> Location { get; set; }
    }
    public class Result
    {
        /// <summary>
        /// 小时预报
        /// </summary>
        [JsonProperty("hourly")]
        public Hourly Hourly { get; set; }
        /// <summary>
        /// 分钟预报
        /// </summary>
        [JsonProperty("minutely")]
        public Minutely Minutely { get; set; }
        /// <summary>
        /// 天预报
        /// </summary>
        [JsonProperty("daily")]
        public Daily Daily { get; set; }
        /// <summary>
        /// 未知项
        /// </summary>
        [JsonProperty("primary")]
        public int Primary { get; set; }
    }
    public class Hourly
    {
        /// <summary>
        /// 请求状态
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
        /// <summary>
        /// 文字描述
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// 天气概况
        /// </summary>
        [JsonProperty("skycon")]
        public List<Skycon> Skycon { get; set; }
        /// <summary>
        /// 云量
        /// </summary>
        [JsonProperty("cloudrate")]
        public List<Cloudrate> Cloudrate { get; set; }
        /// <summary>
        /// 空气质量
        /// </summary>
        [JsonProperty("aqi")]
        public List<Aqi> Aqi { get; set; }
        /// <summary>
        /// 相对湿度
        /// </summary>
        [JsonProperty("humidity")]
        public List<Humidity> Humidity { get; set; }
        /// <summary>
        /// PM2.5
        /// </summary>
        [JsonProperty("pm25")]
        public List<Pm25> Pm25 { get; set; }
        /// <summary>
        /// 降雨强度
        /// </summary>
        [JsonProperty("precipitation")]
        public List<Precipitation> Precipitation { get; set; }
        /// <summary>
        /// 风
        /// </summary>
        [JsonProperty("wind")]
        public List<Wind> Wind { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        [JsonProperty("temperature")]
        public List<Temperature> Temperature { get; set; }
    }
    public class Skycon
    {
        /// <summary>
        /// CLEAR_DAY：晴天，CLEAR_NIGHT：晴夜，PARTLY_CLOUDY_DAY：多云，PARTLY_CLOUDY_NIGHT：多云，CLOUDY：阴，RAIN：雨，SNOW：雪，WIND：风，FOG：雾
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("datetime")]
        public string Datetime { get; set; }
    }
    public class Cloudrate
    {
        /// <summary>
        /// 0-10，表示：无云-全天被云遮蔽
        /// </summary>
        [JsonProperty("value")]
        public double Value { get; set; }
        [JsonProperty("datetime")]
        public string Datetime { get; set; }
    }
    public class Aqi
    {
        [JsonProperty("value")]
        public double Value { get; set; }
        [JsonProperty("datetime")]
        public string Datetime { get; set; }
    }
    public class Humidity
    {
        /// <summary>
        /// 0-1，百分比*100
        /// </summary>
        [JsonProperty("value")]
        public double Value { get; set; }
        [JsonProperty("datetime")]
        public string Datetime { get; set; }
    }
    public class Pm25
    {
        [JsonProperty("value")]
        public double Value { get; set; }
        [JsonProperty("datetime")]
        public string Datetime { get; set; }
    }
    public class Precipitation
    {
        /// <summary>
        /// 毫米/小时，*24后，0-10：小雨，10-25：中雨，25-50：大雨，50以上：暴雨
        /// </summary>
        [JsonProperty("value")]
        public double Value { get; set; }
        [JsonProperty("datetime")]
        public string Datetime { get; set; }
    }
    public class Wind
    {
        /// <summary>
        /// 0-360，顺时针对应北-东-南-西
        /// </summary>
        [JsonProperty("direction")]
        public double Direction { get; set; }
        /// <summary>
        /// 千米/小时
        /// </summary>
        [JsonProperty("speed")]
        public double Speed { get; set; }
        [JsonProperty("datetime")]
        public string Datetime { get; set; }
    }
    public class Temperature
    {
        /// <summary>
        /// 摄氏度℃
        /// </summary>
        [JsonProperty("value")]
        public double Value { get; set; }
        [JsonProperty("datetime")]
        public string Datetime { get; set; }
    }
    public class Minutely
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("probability")]
        public List<double> Probability { get; set; }
        [JsonProperty("datasource")]
        public string Datasource { get; set; }
        [JsonProperty("precipitation_2h")]
        public List<double> Precipitation2h { get; set; }
        [JsonProperty("precipitation")]
        public List<double> Precipitation { get; set; }
    }
    public class Daily
    {
        /// <summary>
        /// 请求状态
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
        /// <summary>
        /// 感冒指数
        /// </summary>
        [JsonProperty("coldRisk")]
        public List<ColdRisk> ColdRisk { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        [JsonProperty("temperature")]
        public List<Temperature_Daily> Temperature { get; set; }
        /// <summary>
        /// 天气概况
        /// </summary>
        [JsonProperty("skycon")]
        public List<Skycon_Daily> Skycon { get; set; }
        /// <summary>
        /// 云量
        /// </summary>
        [JsonProperty("cloudrate")]
        public List<Cloudrate_Daily> Cloudrate { get; set; }
        [JsonProperty("aqi")]
        public List<Aqi_Daily> Aqi { get; set; }
        [JsonProperty("humidity")]
        public List<Humidity_Daily> Humidity { get; set; }
        /// <summary>
        /// 日出日落
        /// </summary>
        [JsonProperty("astro")]
        public List<Astro> Astro { get; set; }
        /// <summary>
        /// 紫外线强度
        /// </summary>
        [JsonProperty("ultraviolet")]
        public List<Ultraviolet> Ultraviolet { get; set; }
        [JsonProperty("pm25")]
        public List<Pm25_Daily> Pm25 { get; set; }
        /// <summary>
        /// 穿衣指数
        /// </summary>
        [JsonProperty("dressing")]
        public List<Dressing> Dressing { get; set; }
        /// <summary>
        /// 洗车指数
        /// </summary>
        [JsonProperty("carWashing")]
        public List<CarWashing> CarWashing { get; set; }
        /// <summary>
        /// 降水强度
        /// </summary>
        [JsonProperty("precipitation")]
        public List<Precipitation_Daliy> Precipitation { get; set; }
        /// <summary>
        /// 风
        /// </summary>
        [JsonProperty("wind")]
        public List<Wind_Daily> Wind { get; set; }
    }
    public class ColdRisk
    {
        [JsonProperty("index")]
        public string Index { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty("desc")]
        public string Desc { get; set; }
        [JsonProperty("datetime")]
        public string Datetime { get; set; }
    }
    public class Temperature_Daily
    {
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("max")]
        public double Max { get; set; }
        [JsonProperty("avg")]
        public double Avg { get; set; }
        [JsonProperty("min")]
        public double Min { get; set; }
    }
    public class Skycon_Daily
    {
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
    public class Cloudrate_Daily
    {
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("max")]
        public double Max { get; set; }
        [JsonProperty("avg")]
        public double Avg { get; set; }
        [JsonProperty("min")]
        public double Min { get; set; }
    }
    public class Aqi_Daily
    {
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("max")]
        public double Max { get; set; }
        [JsonProperty("avg")]
        public double Avg { get; set; }
        [JsonProperty("min")]
        public double Min { get; set; }
    }
    public class Humidity_Daily
    {
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("max")]
        public double Max { get; set; }
        [JsonProperty("avg")]
        public double Avg { get; set; }
        [JsonProperty("min")]
        public double Min { get; set; }
    }
    public class Astro
    {
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("sunset")]
        public Sunset Sunset { get; set; }
        [JsonProperty("sunrise")]
        public Sunrise Sunrise { get; set; }
    }
    public class Sunset
    {
        [JsonProperty("time")]
        public string Time { get; set; }
    }
    public class Sunrise
    {
        [JsonProperty("time")]
        public string Time { get; set; }
    }
    public class Ultraviolet
    {
        [JsonProperty("index")]
        public string Index { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty("desc")]
        public string Desc { get; set; }
        [JsonProperty("datetime")]
        public string Datetime { get; set; }
    }
    public class Pm25_Daily
    {
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("max")]
        public double Max { get; set; }
        [JsonProperty("avg")]
        public double Avg { get; set; }
        [JsonProperty("min")]
        public double Min { get; set; }
    }
    public class Dressing
    {
        [JsonProperty("index")]
        public string Index { get; set; }
        [JsonProperty("desc")]
        public string Desc { get; set; }
        [JsonProperty("datetime")]
        public string Datetime { get; set; }
    }
    public class CarWashing
    {
        [JsonProperty("index")]
        public string Index { get; set; }
        [JsonProperty("desc")]
        public string Desc { get; set; }
        [JsonProperty("datetime")]
        public string Datetime { get; set; }
    }
    public class Precipitation_Daliy
    {
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("max")]
        public double Max { get; set; }
        [JsonProperty("avg")]
        public double Avg { get; set; }
        [JsonProperty("min")]
        public double Min { get; set; }
    }
    public class Wind_Daily
    {
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("max")]
        public Wind_WithoutDate Max { get; set; }
        [JsonProperty("avg")]
        public Wind_WithoutDate Avg { get; set; }
        [JsonProperty("min")]
        public Wind_WithoutDate Min { get; set; }
    }
    public class Wind_WithoutDate
    {
        [JsonProperty("direction")]
        public double Direction { get; set; }
        [JsonProperty("speed")]
        public double Speed { get; set; }
    }
}
