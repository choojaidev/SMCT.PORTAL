using System;
using System.Collections.Generic;
using System.Globalization;

namespace SMCTWebTemplate.Model
{
    public class ForecastResponse {
        public List<WeatherForecast7DayResponse> WeatherForecast7DayResponse { get; set; }
        public Weather3HourResponse Weather3HourResponse { get; set; }
        public PMResponse PMResponse { get; set; }
    }
    public class WeatherForecast7DayResponse
    {
        public WeatherForecast7Day weatherForecast7Day { get; set; }
        public Province province { get; set; }
    }
    public class Weather3HourResponse
    {
        public weather3Hour weather3Hour { get; set; }
    }
    public class weather3Hour
    {
        public string currentPhenomenonText { get; set; }
        public string currentPhenomenonImage { get; set; }
        public string barometerTemperature { get; set; }
    }
    public class WeatherForecast7Day
    {
        public int id { get; set; }
        public int provinceId { get; set; }
        public int? regionId { get; set; }
        public double maxTemperature { get; set; }
        public double minTemperature { get; set; }
        public int rainArea { get; set; }
        public string windDirectionImagePath { get; set; }
        public int windDirection { get; set; }
        public int windSpeed { get; set; }
        public int weatherType { get; set; }
        public string description { get; set; }
        public string descriptionEN { get; set; }
        public DateTime recordTime { get; set; }
        public string display_recordTime1
        {
            get
            {
                var display = "";
                var day = Convert.ToDateTime(this.recordTime, new CultureInfo("th-TH")).DayOfWeek;
                if (DateTime.Today.DayOfWeek == day)
                {
                    display = "วันนี้";
                }
                else if (DateTime.Today.AddDays(1).DayOfWeek == day)
                {
                    display = "พรุ่งนี้";
                }
                else
                {
                    switch (day.ToString().ToLower())
                    {
                        case "sunday": { display = "อาทิตย์"; break; }
                        case "monday": { display = "จันทร์"; break; }
                        case "tuesday": { display = "อังคาร"; break; }
                        case "wednesday": { display = "พุธ"; break; }
                        case "thursday": { display = "พฤหัส"; break; }
                        case "friday": { display = "ศุกร์"; break; }
                        case "saturday": { display = "เสาร์"; break; }
                    }
                }
                return display;
            }
        }
        public string display_recordTime2
        {
            get
            {
                var display = "";
                var recordDate = Convert.ToDateTime(this.recordTime, new CultureInfo("th-TH"));
                var month = recordDate.Month;

                switch (month)
                {
                    case 1: { display = "ม.ค."; break; }
                    case 2: { display = "ก.พ."; break; }
                    case 3: { display = "มี.ค."; break; }
                    case 4: { display = "เม.ย"; break; }
                    case 5: { display = "พ.ค."; break; }
                    case 6: { display = "มิ.ย."; break; }
                    case 7: { display = "ก.ค."; break; }
                    case 8: { display = "ส.ค"; break; }
                    case 9: { display = "ก.ย."; break; }
                    case 10: { display = "ต.ค"; break; }
                    case 11: { display = "พ.ย."; break; }
                    case 12: { display = "ธ.ค."; break; }
                }
                return recordDate.Day + " " +  display;
            }
        }
        public string name { get; set; }
        public string nameEN { get; set; }
        public string weatherTypeText { get; set; }
        public string rainAreaText { get; set; }
        public string weatherTypeImagePath { get; set; }
        public int waveType { get; set; }
        public string waveTypeText { get; set; }
        public string waveTypeImage { get; set; }
        public int temperatureType { get; set; }
        public string temperatureTypeText { get; set; }
        public string temperatureTypeImage { get; set; }
        public string mainWeatherTypeText { get; set; }
        public DateTime? creationTime { get; set; }
        public string creatorId { get; set; }
        public DateTime? lastModificationTime { get; set; }
        public string lastModifierId { get; set; }
        public bool? isDeleted { get; set; }
        public string deleterId { get; set; }
        public DateTime? deletionTime { get; set; }
    }
    public class Province
    {
        public int id { get; set; }
        public string name { get; set; }
        public string geoCode { get; set; }
        public int region { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public string nameEN { get; set; }
        public DateTime? creationTime { get; set; }
        public string creatorId { get; set; }
        public DateTime? lastModificationTime { get; set; }
        public string lastModifierId { get; set; }
        public bool? isDeleted { get; set; }
        public string deleterId { get; set; }
        public DateTime? deletionTime { get; set; }
    }
}
