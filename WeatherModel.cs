using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WeatherModel
{
    public Coord coord;
    public Weather[] weather;
    public string _base;
    public Main main;
    public int visibility;
    public Wind wind;
    public Clouds clouds;
    public int dt;
    public Sys sys;
    public int id;
    public string name;
    public int cod;
}

public class Coord
{
    public float lon;
    public float lat;
}

public class Main
{
    public float temp;
    public int pressure;
    public int humidity;
    public float temp_min;
    public float temp_max;
}

public class Wind
{
    public float speed;
    public int deg;
}

public class Clouds
{
    public int all;
}

public class Sys
{
    public int type;
    public int id;
    public float message;
    public string country;
    public int sunrise;
    public int sunset;
}

public class Weather
{
    public int id;
    public string main;
    public string description;
    public string icon;
}
