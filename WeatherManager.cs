using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using MiniJSON;
using Newtonsoft.Json;

public class WeatherManager : MonoBehaviour, IGameManager{

    public ManagerStatus status { get; private set; }

    private NetworkService _weatherService;
    public WeatherModel weather;

    public void Startup(NetworkService service)
    {
        Debug.Log("Weather manager starting...");

        _weatherService = service;
        StartCoroutine(_weatherService.GetWeatherJSON(OnJSONDataLoaded));

        status = ManagerStatus.Initializing;
    }

    private void OnJSONDataLoaded(string data)
    {
        weather = new WeatherModel();
        weather = JsonConvert.DeserializeObject<WeatherModel>(data);

        Debug.Log("cloud value: " + weather.clouds.all);
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
        StartCoroutine(_weatherService.LogWeather(name, weather.clouds.all, OnLogged));
    }

    private void OnLogged(string response)
    {
        Debug.Log(response);
    }
}
