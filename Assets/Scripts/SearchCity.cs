using UnityEngine.Networking;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class SearchCity : MonoBehaviour
{
    [SerializeField] Button Search;
    [SerializeField] TMP_InputField searchCity;
    [SerializeField] APIData apiData;
    [SerializeField] TextMeshProUGUI notCity;
    private void Start()
    {
        Search.onClick.AddListener(GetCity);
    }

    public void ValidEnter(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            GetCity();
        }
    }

    public void GetCity()
    {
        string city = searchCity.text;
        Debug.Log(city);
        /*apiData.CallApiCity(city);*/
        searchCity.text = "";
    }

    public void NotCity(string cityName)
    {
        notCity.gameObject.SetActive(true);
        notCity.text = "La ville " + cityName + " n'est pas référencé";
    }

    public void OKCity()
    {
        notCity.gameObject.SetActive(false);
    }
}
