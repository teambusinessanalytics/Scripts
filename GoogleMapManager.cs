using UnityEngine;
using System.Collections.Generic;
using System;


public class GoogleMapManager : MonoBehaviour, IGameManager {

    public bool loadOnStart = true;
    public bool autoLocateCenter = true;
    public GoogleMapLocation centerLocation;
    public int zoom = 13;
    public GoogleMapType mapType;
    public int size = 512;
    //public int width;
    //public int height;
    public bool doubleResolution = false;
    public GoogleMapMarker[] markers;
    public GoogleMapPath[] paths;

    private NetworkService _mapService;

    public ManagerStatus status {get; private set;}
    public Texture2D mapTexure { get; private set; }
    public List<Vector2> markerPositionsPix { get; private set; }

    public void Startup(NetworkService service)
    {
        _mapService = service;
        if (loadOnStart) Refresh();

        status = ManagerStatus.Initializing;
    }
    
    public void Refresh()
    {
        if (autoLocateCenter && (markers.Length == 0 && paths.Length == 0))
        {
            Debug.LogError("Auto Center will only work if paths or markers are used.");
        }
        StartCoroutine(_mapService.RefreshGoogleMap(autoLocateCenter, centerLocation, zoom, mapType, size, doubleResolution, markers, paths, OnMapRefreshed));

        
    }

    private void OnMapRefreshed(Texture2D mapTexure)
    {
        Debug.Log("googlemap refreshed");
        this.mapTexure = mapTexure;
        Debug.Log(mapTexure.ToString());
        PlaceMarkers();
        Messenger.Broadcast(GameEvent.GOOGLEMAP_UPDATED);

        status = ManagerStatus.Started;
    }

    //Calculating Marker Pixel Positions to be accessed by controller
    public void PlaceMarkers()
    {
        var tileSize = doubleResolution ? size : size / 2;
        Debug.Log("marker size: " + markers.Length);
        markerPositionsPix = new List<Vector2>();
        
        foreach (var marker in markers)
        {
            foreach (var location in marker.locations)
            {

                var lon = location.longitude;
                var lat = location.latitude;

                Debug.Log("lon: " + lon + " lat: " + lat);

                //1. Determine where the markers are in pixel coordinates in the static map texture.

                var x = MapUtils.convertLonToX2(lon, zoom, tileSize);
                var y = MapUtils.convertLatToY2(lat, zoom, tileSize);

                Debug.Log("x: " + x + " y: " + y);
                markerPositionsPix.Add(new Vector2(x, y));
                
            }
        }
    }


}
