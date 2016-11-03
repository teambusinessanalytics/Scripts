using UnityEngine;
using System.Collections;
using System;

public class NetworkService {
    private const string xmlApi = "http://api.openweathermap.org/data/2.5/weather?id=2934246&APPID=d1770ac1358ebd4db17727680e711682&mode=xml";
    private const string jsonApi = "http://api.openweathermap.org/data/2.5/weather?q=tokyo&APPID=d1770ac1358ebd4db17727680e711682&mode=json";
    private const string localApi = "http://192.168.0.104/vr2go/api.php";

    private bool IsResponseValid(WWW www)
    {
        if(www.error != null)
        {
            Debug.Log("bad connection: " + www.error);
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

    private IEnumerator CallAPI(string url, Hashtable args, Action<string> callback)
    {
        WWW www;
        
        if (args == null)
        {
            www = new WWW(url); //GET request
        }
        else
        {
            WWWForm form = new WWWForm();
            foreach(DictionaryEntry arg in args)
            {
                form.AddField(arg.Key.ToString(), arg.Value.ToString());
            }
            www = new WWW(url, form); //POST request

        }
        yield return www;

        if (!IsResponseValid(www))
        {
            yield break;
        }
        callback(www.text);

    }

    public IEnumerator GetWeatherXML(Action<string> callback)
    {
        return CallAPI(xmlApi, null, callback); //null makes the request a GET

    }

    public IEnumerator GetWeatherJSON(Action<string> callback)
    {
        return CallAPI(jsonApi, null, callback); //null makes the request a GET

    }

    public IEnumerator LogWeather(string name, float cloudValue, Action<string> callback)
    {
        Hashtable args = new Hashtable();
        args.Add("message", name);
        args.Add("cloud_value", cloudValue);
        args.Add("timestamp", DateTime.UtcNow.Ticks);

        return CallAPI(localApi, args, callback);

    }
}
