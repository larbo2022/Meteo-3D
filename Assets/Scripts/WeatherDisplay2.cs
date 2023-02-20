using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherDisplay2 : MonoBehaviour
{
    
    [SerializeField] Text temperature;
    [SerializeField] Text latitude;
    [SerializeField] Text longitude;
    [SerializeField] Text town;
    float temp;
    float tmp;
    // float latitude;
    // float longitude;
    float lat;
    float lon;
    // Start is called before the first frame update
    void Start()
    {

    }

    
    
    public void GetTemp(float tmp)
    {
        temp = tmp;
        temperature.text = "Température :" + temp;
    }
    public void GetCoord(float lat, float lon)
    {
        float lati = lat;
        float longi = lon;
       
        latitude.text = "la latitude :" + lati;
        longitude.text = "la longitude :" + longi;
    }

    public void GetTown(string twn)
    {
        string tow = twn;
        
        town.text = "la ville :" + tow;
       
    }
}