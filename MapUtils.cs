using UnityEngine;
using System.Collections;
using System;

public class MapUtils
{

    static float GOOGLEOFFSET = 268435456f;
    static float GOOGLEOFFSET_RADIUS = 85445659.44705395f;//GOOGLEOFFSET / Mathf.PI;
    static float MATHPI_180 = Mathf.PI / 180f;

    static int x_pos = 54;
    static int y_pos = 19;
    static float longitude_shift = 55;

    static private float preLonToX1 = GOOGLEOFFSET_RADIUS * (Mathf.PI / 180f);

    public static int LonToX(float lon)
    {
        return ((int)Mathf.Round(GOOGLEOFFSET + preLonToX1 * lon));
    }

    public static int LatToY(float lat)
    {
        return (int)Mathf.Round(GOOGLEOFFSET - GOOGLEOFFSET_RADIUS * Mathf.Log((1f + Mathf.Sin(lat * MATHPI_180)) / (1f - Mathf.Sin(lat * MATHPI_180))) / 2f);
    }

    public static float XToLon(float x)
    {
        return ((Mathf.Round(x) - GOOGLEOFFSET) / GOOGLEOFFSET_RADIUS) * 180f / Mathf.PI;
    }

    public static float YToLat(float y)
    {
        return (Mathf.PI / 2f - 2f * Mathf.Atan(Mathf.Exp((Mathf.Round(y) - GOOGLEOFFSET) / GOOGLEOFFSET_RADIUS))) * 180f / Mathf.PI;
    }

    public static float adjustLonByPixels(float lon, int delta, int zoom)
    {
        return XToLon(LonToX(lon) + (delta << (21 - zoom)));
    }

    public static float adjustLatByPixels(float lat, int delta, int zoom)
    {
        return YToLat(LatToY(lat) + (delta << (21 - zoom)));
    }

    public static float convertLatToY(float lat, int map_height, int map_width)
    {
        lat = lat * MATHPI_180;  // convert from degrees to radians
        float y = (float)Math.Log(Math.Tan((lat / 2) + (Math.PI / 4)));  // do the Mercator projection (w/ equator of 2pi units)
        y = (float)((map_height / 2) - (map_width * y / (2 * Math.PI)))+ y_pos;
        y -= y_pos;
        
        return y;
    }

    public static float convertLonToX(float lon, int map_width)
    {
        var x = (map_width * (180 + lon) / 360) % map_width + longitude_shift;
        x -= x_pos;
        
        return x;
    }

    public static float convertLatToY2(float lat, int zoom, int tileSize)
    {
        var scale = 1 << zoom;
        var siny = Mathf.Sin(lat * Mathf.PI / 180);
        siny = Mathf.Min(Mathf.Max(siny, -0.9999f), 0.9999f);

        return Mathf.Floor(tileSize * (.5f - Mathf.Log((1 + siny) / (1 - siny)) / (4 * Mathf.PI)) * scale);
    }

    public static float convertLonToX2(float lon, int zoom, int tileSize)
    {
        var scale = 1 << zoom;
        var siny = Mathf.Sin(lon * Mathf.PI / 180);
        siny = Mathf.Min(Mathf.Max(siny, -0.9999f), 0.9999f);

        return Mathf.Floor(tileSize * (.5f + lon / 360) * scale);
    }

}