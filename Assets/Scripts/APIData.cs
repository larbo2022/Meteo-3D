using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Globalization;
using UnityEngine.UI;
using System;
using System.Linq;
public class APIData : MonoBehaviour
{
    //  [SerializeField] WeatherDisplay2 wDisplay;
    [SerializeField] Text outputField;
    float tmp;
    float lat;
    float lon;
    string twn;

    // string _startDate;
    // string _endDate;
    float lati;
    float longi;
    Vector2 longLat;
    NumberFormatInfo nfi = new NumberFormatInfo();
    // Start is called before the first frame update
    /*  void Start()
      {
          // string uri = "https://api.openweathermap.org/data/2.5/weather?appid=34db0613f8131128ffb627ee457cf083&lat=43.6961&lon=7.27178&exclude=daily&units=metric&lang=fr";
          // var uri = $"https://api.openweathermap.org/geo/1.0/reverse?lat={lati.ToString(nfi)}&lon={longi.ToString(nfi)}&limit=5&appid=34db0613f8131128ffb627ee457cf083";
          //  var uri = $"https://api.open-meteo.com/v1/meteofrance?latitude={lati.ToString(nfi)}&longitude={longi.ToString(nfi)}&daily=temperature_2m_max,temperature_2m_min&start_date={_startDate.ToString(nfi)}&end_date={_endDate.ToString(nfi)}&timezone=Europe%2FBerlin";
          var uri = $"https://api.openweathermap.org/data/2.5/forecast?appid=34db0613f8131128ffb627ee457cf083&lat=43.6961&lon=7.27178&exclude=daily&units=metric&lang=fr";
          // A correct website page.
          StartCoroutine(GetRequest(uri));

          // A non-existing page.
           StartCoroutine(GetRequest("https://error.html"));
      }   */
    public void GetSearchCoord(Vector2 longLat)
    {
        float lati = longLat.y;
        float longi = longLat.x;
        Debug.Log("Coordonnées cartésiennes : " + lati + longi);

        //  _startDate = System.DateTime.Now.ToString("yyyy-MM-dd");
        //  _endDate = System.DateTime.Now.AddDays(4).ToString("yyyy-MM-dd");

        //string uri = "http://api.openweathermap.org/geo/1.0/reverse?" + "lat=" + lati.ToString(nfi) + "&lon=" + longi.ToString(nfi) + "&limit=5&appid=34db0613f8131128ffb627ee457cf083";
        // var uri = $"http://api.openweathermap.org/geo/1.0/reverse?lat={lati.ToString(nfi)}&lon={longi.ToString(nfi)}&limit=5&appid=34db0613f8131128ffb627ee457cf083";
        // var uri = $"https://api.open-meteo.com/v1/meteofrance?latitude={lati.ToString(nfi)}&longitude={longi.ToString(nfi)}&daily=temperature_2m_max,temperature_2m_min&start_date={_startDate.ToString(nfi)}&end_date={_endDate.ToString(nfi)}&timezone=Europe%2FBerlin";
        var uri = $"https://api.openweathermap.org/data/2.5/forecast?appid=34db0613f8131128ffb627ee457cf083&lat={lati.ToString(nfi)}&lon={longi.ToString(nfi)}&exclude=daily&units=metric&lang=fr";
        print(uri);

        StartCoroutine(GetRequest(uri));

    }
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    /*Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);*/
                    string json = webRequest.downloadHandler.text;
                    //  WeatherJson weatherJson = JsonConvert.DeserializeObject<WeatherJson>(json);
                    Root root = JsonConvert.DeserializeObject<Root>(json);
                    /* twn = root.name;
                     lat = 43.6961f;
                     lon = 7.27178f;
                     tmp = weatherJson.main.temp;*/

                    List<ListForcast> dailyForcast = root.list.Where(item => item.dt_Date.Hour == 12).ToList();

                    foreach (var itemForecast in dailyForcast)
                    {
                        string textDayMeteo = itemForecast.dt_Date.ToLongDateString();
                        string textTemperature = "Température : " + itemForecast.main.temp.ToString() + "°C";
                        string textDescription = "Temps : " + itemForecast.weather.ElementAt(0).description;
                        outputField.text += textDayMeteo + "\n" + textTemperature + "\n" + textDescription + "\n\n";
                    }
                    break;
            }
        }
    }

    /*  public class WeatherJson
      {
          public string name { get; set; }
          public WeatherMain main { get; set; }

      }

      public class WeatherMain
      {
          public float temp { get; set; }
      }  */

    /*   public void OnClickDisplay()
       {
           wDisplay.GetCoord(lat, lon);
           wDisplay.GetTemp(tmp);
           wDisplay.GetTown(twn);
       }   */

    public class ListForcast
    {
        public DateTime dt_Date;

        private string _dt_txt;
        public string dt_txt
        {
            get { return _dt_txt; }
            set
            {
                _dt_txt = value;
                dt_Date = DateTime.ParseExact(_dt_txt, "yyyy-MM-dd HH:mm:ss", null);
            }
        }

        public int dt
        {
            get;
            set;
        }
        public Main main { get; set; }
        public List<Weather> weather { get; set; }

    }

    public class Main
    {
        public double temp { get; set; }

    }
    public class Root
    {
        public List<ListForcast> list { get; set; }
        //   public City city { get; set; }
    }

    public class Weather
    {
        public string main { get; set; }
        public string description { get; set; }

    }

}
