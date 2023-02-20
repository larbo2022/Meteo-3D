using UnityEngine.Networking;
using System.Collections;
using UnityEngine;
using Newtonsoft.Json;

public class APIData : MonoBehaviour
{
    [SerializeField] WeatherDisplay2 wDisplay;
    float tmp;
    float lat;
    float lon;
    string twn;
    // Start is called before the first frame update
    void Start()
    {
        string uri = "https://api.openweathermap.org/data/2.5/weather?appid=34db0613f8131128ffb627ee457cf083&lat=43.6961&lon=7.27178&units=metric&lang=fr";
        // A correct website page.
        StartCoroutine(GetRequest(uri));

        // A non-existing page.
        /*StartCoroutine(GetRequest("https://error.html"));*/
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
                    WeatherJson weatherJson = JsonConvert.DeserializeObject<WeatherJson>(json);
                    twn = weatherJson.name;
                    lat = 43.6961f;
                    lon = 7.27178f;
                    tmp = weatherJson.main.temp;
                    
                    break;
            }
        }
    }

    public class WeatherJson
    {
        public string name { get; set; }
        public WeatherMain main { get; set; }
        
    }

    public class WeatherMain
    {
        public float temp { get; set; }
    }

    public void OnClickDisplay()
    {
        wDisplay.GetCoord(lat, lon);
        wDisplay.GetTemp(tmp);
        wDisplay.GetTown(twn);
    }
    

}
