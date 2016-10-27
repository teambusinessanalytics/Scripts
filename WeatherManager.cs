using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using MiniJSON;

public class WeatherManager : MonoBehaviour, IGameManager{

    public ManagerStatus status { get; private set; }

    private NetworkService _network;
    public float cloudValue { get; private set; }

    public void Startup(NetworkService service)
    {
        Debug.Log("Weather manager starting...");

        _network = service;
        StartCoroutine(_network.GetWeatherJSON(OnJSONDataLoaded));

        status = ManagerStatus.Initializing;
    }

    private void OnJSONDataLoaded(string data)
    {
        Dictionary<string, object> dict;
        dict = Json.Deserialize(data) as Dictionary<string, object>;

        Dictionary<string, object> clouds = (Dictionary<string, object>) dict["clouds"];
        cloudValue = (long)clouds["all"] / 100f;

        Debug.Log("cloud value: " + cloudValue);
        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);

        status = ManagerStatus.Started;
    }

    private void OnXMLDataLoaded(string data)
    {
        Debug.Log(data);

        status = ManagerStatus.Started;
    }

    public void LogWeather(string name)
    {
        StartCoroutine(_network.LogWeather(name, cloudValue, OnLogged));
    }

    private void OnLogged(string response)
    {
        Debug.Log(response);
    }
}
