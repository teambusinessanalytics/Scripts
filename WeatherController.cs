using UnityEngine;
using System.Collections;


public class WeatherController : MonoBehaviour
{

    public bool render;
    public GameObject dataContainer;

    [SerializeField]
    private Material sky;
    [SerializeField]
    private Light sun;

    private float _fullIntensity;

    void Awake()
    {
        Messenger.AddListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);

    }

    void Destroy()
    {
        Messenger.RemoveListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);

    }

    private void OnWeatherUpdated()
    {
        if (render)
            SetOvercast(Managers.Weather.weather.clouds.all / 100f);

    }

    // Use this for initialization
    void Start()
    {
        if (sun)
        {
            _fullIntensity = sun.intensity;
        }
    }


    private void SetOvercast(float value)
    {
        sky.SetFloat("_Blend", value);
        if (sun)
        {
            sun.intensity = _fullIntensity - (_fullIntensity * value);
        }

        var counter = 0.1f;
        var position = dataContainer.transform.position;

        while (counter < value)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.parent = dataContainer.transform;
            cube.GetComponent<Collider>().enabled = false;
            cube.AddComponent<SendDataTrigger>();
            cube.GetComponent<SendDataTrigger>().identifier = "DataCube";
            //cube.AddComponent<VRTK.VRTK_InteractableObject>();
            //cube.GetComponent<VRTK.VRTK_InteractableObject>().isGrabbable = true;
            //cube.GetComponent<VRTK.VRTK_InteractableObject>().precisionSnap = true;
            //cube.GetComponent<VRTK.VRTK_InteractableObject>().highlightOnTouch = true;
            //cube.GetComponent<VRTK.VRTK_InteractableObject>().touchHighlightColor = Color.green;
            //cube.GetComponent<VRTK.VRTK_InteractableObject>().detachThreshold = 2000;
            cube.transform.localScale = cube.transform.localScale * .5f;
            cube.transform.position = new Vector3(position.x + Random.Range(-1f, 1f) * 5, position.y + Random.Range(-1f, 1f) * 5, position.z + Random.Range(-1f, 1f) * 5);
            counter = counter + 0.01f;
        }

    }
}
