using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Forcast : MonoBehaviour
{
    public TextMeshProUGUI[] forecasts;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ForecastUi(string forecast, int i)
    {
        /*forecastFond.SetActive(true);*/
        forecasts[i].text = forecast;
    }
}
