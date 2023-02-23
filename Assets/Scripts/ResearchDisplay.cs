using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchDisplay : MonoBehaviour
{
    WeatherDisplay2 wDisplay;

    [SerializeField] Text latitude;
    [SerializeField] Text longitude;
    [SerializeField] Text town;
    [SerializeField] Text country;
    //[SerializeField] Text temperature;
    //float temp;
    // float tmp;
    // float latitude;
    // float longitude;
    float lat;
    float lon;
    Vector2 longLat;
    // Start is called before the first frame update
    void Start()
    {

    }

       
    public void GetCoord(float lat, float lon)
    {
        float lati = lat;
        float longi = lon;

        latitude.text = "la latitude :" + lati;
        longitude.text = "la longitude :" + longi;
    }

    public void GetTown(string twn, string cntr)
    {
        string tow = twn;
        string countr = cntr;
        town.text = "la ville :" + tow;
        country.text = "le Pays :" + countr;

    }
}