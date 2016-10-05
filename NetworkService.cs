using UnityEngine;
using System.Collections;
using System;

public class NetworkService {
    private const string xmlApi = "http://api.openweathermap.org/data/2.5/weather?id=2934246&APPID=d1770ac1358ebd4db17727680e711682&mode=xml";
    private const string jsonApi = "http://api.openweathermap.org/data/2.5/weather?q=tokyo&APPID=d1770ac1358ebd4db17727680e711682&mode=json";

    private bool IsResponseValid(WWW www)
    {
        if(www.error != null)
        {
            Debug.Log("bad connection");
            return false;
        }
        else if (string.IsNullOrEmpty(www.text))
        {
            Debug.Log("bad data");
            return false;
        }
        else
        {
            return true;
        }
    }

    private IEnumerator CallAPI(string url, Action<string> callback)
    {
        WWW www = new WWW(url);
        yield return www;

        if (!IsResponseValid(www))
        {
            yield break;
        }
        callback(www.text);

    }

    public IEnumerator GetWeatherXML(Action<string> callback)
    {
        return CallAPI(xmlApi, callback);

    }

    public IEnumerator GetWeatherJSON(Action<string> callback)
    {
        return CallAPI(jsonApi, callback);

    }
}
