using UnityEngine.Networking;
using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using System.Globalization;

public class ApiRequest : MonoBehaviour
{
    [SerializeField] ResearchDisplay rDisplay;
    [SerializeField] WeatherDisplay2 wDisplay;
    float tmp;
    float lat;
    float lon;
    string twn;
    string cntr;
    Vector2 longLat;

    NumberFormatInfo nfi = new NumberFormatInfo();


    private void Awake()
    {

        nfi.NumberDecimalSeparator = ".";
    }




    public void OnDisplay()
    {
        rDisplay.GetCoord(lat, lon);

        rDisplay.GetTown(twn, cntr);
    }
    public void OnClickDisplay()
    {
        wDisplay.GetCoord(lat, lon);
        wDisplay.GetTemp(tmp);
        wDisplay.GetTown(twn);
    }

    
    public void GetSearchCoord(Vector2 longLat)
    {
        float lati = longLat.y;
        float longi = longLat.x;

        //string uri = "http://api.openweathermap.org/geo/1.0/reverse?" + "lat=" + lati.ToString(nfi) + "&lon=" + longi.ToString(nfi) + "&limit=5&appid=34db0613f8131128ffb627ee457cf083";
        var uri = $"http://api.openweathermap.org/geo/1.0/reverse?lat={lati.ToString(nfi)}&lon={longi.ToString(nfi)}&limit=5&appid=34db0613f8131128ffb627ee457cf083";
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
                    Root root = JsonConvert.DeserializeObject<Root>(json);
                    twn = root.name;
                    lat = root.lat;
                    lon = root.lon;
                    cntr = root.country;
                    OnDisplay();
                    break;
            }
        }
    }


    public class Root
    {
        public string name { get; set; }
        public LocalNames local_names { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public string country { get; set; }
    }

    public class LocalNames
    {
        public string fr { get; set; }
    }
}
