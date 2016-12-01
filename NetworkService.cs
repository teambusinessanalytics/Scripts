using UnityEngine;
using System.Collections;
using System;

public class NetworkService {
    private const string currentWeatherXmlApi = "http://api.openweathermap.org/data/2.5/weather?id=2934246&APPID=d1770ac1358ebd4db17727680e711682&mode=xml";
    private const string currentWeatherJsonApi = "http://api.openweathermap.org/data/2.5/weather?q=duesseldorf&APPID=d1770ac1358ebd4db17727680e711682&mode=json";
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
        return CallAPI(currentWeatherXmlApi, null, callback); //null makes the request a GET

    }

    public IEnumerator GetWeatherJSON(Action<string> callback)
    {
        return CallAPI(currentWeatherJsonApi, null, callback); //null makes the request a GET

    }

    public IEnumerator LogWeather(string name, float cloudValue, Action<string> callback)
    {
        Hashtable args = new Hashtable();
        args.Add("message", name);
        args.Add("cloud_value", cloudValue);
        args.Add("timestamp", DateTime.UtcNow.Ticks);

        return CallAPI(localApi, args, callback);

    }

    public IEnumerator RefreshGoogleMap(bool autoLocateCenter, GoogleMapLocation centerLocation, int zoom, GoogleMapType mapType, int size, bool doubleResolution, GoogleMapMarker[] markers, GoogleMapPath[] paths, Action<Texture2D> callback)
    {
        var url = "http://maps.googleapis.com/maps/api/staticmap";
        var qs = "";
        if (!autoLocateCenter)
        {
            if (centerLocation.address != "")
                qs += "center=" + WWW.UnEscapeURL(centerLocation.address);
            else
            {
                qs += "center=" + WWW.UnEscapeURL(string.Format("{0},{1}", centerLocation.latitude, centerLocation.longitude));
            }

            qs += "&zoom=" + zoom.ToString();
        }
        qs += "&size=" + WWW.UnEscapeURL(string.Format("{0}x{1}", size, size));
        qs += "&scale=" + (doubleResolution ? "2" : "1");
        qs += "&maptype=" + mapType.ToString().ToLower();
        var usingSensor = false;
#if UNITY_IPHONE
		usingSensor = Input.location.isEnabledByUser && Input.location.status == LocationServiceStatus.Running;
#endif
        qs += "&sensor=" + (usingSensor ? "true" : "false");

        foreach (var i in markers)
        {
            qs += "&markers=" + string.Format("size:{0}|color:{1}|label:{2}", i.size.ToString().ToLower(), i.color, i.label);
            foreach (var loc in i.locations)
            {
                if (loc.address != "")
                    qs += "|" + WWW.UnEscapeURL(loc.address);
                else
                    qs += "|" + WWW.UnEscapeURL(string.Format("{0},{1}", loc.latitude, loc.longitude));
            }
        }

        foreach (var i in paths)
        {
            qs += "&path=" + string.Format("weight:{0}|color:{1}", i.weight, i.color);
            if (i.fill) qs += "|fillcolor:" + i.fillColor;
            foreach (var loc in i.locations)
            {
                if (loc.address != "")
                    qs += "|" + WWW.UnEscapeURL(loc.address);
                else
                    qs += "|" + WWW.UnEscapeURL(string.Format("{0},{1}", loc.latitude, loc.longitude));
            }
        }


        var req = new WWW(url + "?" + qs);
        Debug.Log(url + "?" + qs);
        yield return req;

        callback(req.texture);
    }
}
