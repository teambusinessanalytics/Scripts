using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using VRTK;

public class ExhibitorLogic : PickupLogic
{
    public GameObject Exhibitor;
    public GameObject Chart3D;
    public AudioClip SpeechAudio;
    public GameObject BackgroundMusicSourceObject;
    public GameObject ControlButton;
    private AudioSource BackgroundMusicSource;
    private AudioSource SpeechAudioSource;

    private Transform DefaultChartShapeTransform;
    private Transform DefaultChartBackgroundTransform;
    //float originalMusicVolume;

    new void Awake()
    {
        base.Awake();
        OriginalMatrials = Exhibitor.GetComponent<Renderer>().materials;
        BackgroundMusicSource = BackgroundMusicSourceObject.GetComponent<AudioSource>();
        SpeechAudioSource = gameObject.GetComponent<AudioSource>();
        //DefaultChartShapeTransform = Chart3D.transform.GetChild(0); //get the chart shape
        //DefaultChartBackgroundTransform = Chart3D.transform.GetChild(1); // get the chart background
        SpeechAudioSource.clip = SpeechAudio;
    }

    new void Start()
    {
        base.Start();

        SpeechAudioSource.enabled = false;
        print("speechaudiosource: " + SpeechAudioSource.name + ", on: " + SpeechAudioSource.enabled);
        print("backgroundaudiosource: " + BackgroundMusicSource.name + "initiated");
    }
    new void Update()
    {
        //base.Update();
        if (SpeechAudio && SpeechAudioSource.enabled == true)
        {
            //SpeechAudioSource.clip = SpeechAudio;
            if (SpeechAudioSource.isPlaying)
            {
                print(SpeechAudioSource.clip.name + " playing...");
                //print("volume turned down to: " + originalMusicVolume.ToString());
                //GameObject.FindObjectOfType<EventSystem>().enabled = false;
                if (BackgroundMusicSource.volume > 0f) BackgroundMusicSource.volume -= .5f * Time.deltaTime;
            }
            else if (!SpeechAudioSource.isPlaying)
            {
                print(SpeechAudioSource.clip.name + " not playing...");
                //GameObject.FindObjectOfType<EventSystem>().enabled = true;
                if (BackgroundMusicSource.volume < 0.3f)
                {
                    BackgroundMusicSource.volume += .1f * Time.deltaTime;
                }else
                {
                    SpeechAudioSource.enabled = false;
                }
            }
        }
    }

    public override void ChangeMaterial(bool gazed)
    {
        if (gazed)
        {
            if (GazedMaterial) Exhibitor.GetComponent<Renderer>().material = GazedMaterial;
            if (HighLight) HighLight.SetActive(true);
        }
        else
        {
            Exhibitor.GetComponent<Renderer>().materials = OriginalMatrials;
            if (HighLight) HighLight.SetActive(false);
        };
    }

    public void showChart2D()
    {
        if (Chart2D.activeInHierarchy == true)
        {
            Chart2D.SetActive(false);
        }
        else
        {
            Chart2D.SetActive(true);
        }

    }

    public void ToggleChart3D()
    {
        //SpeechAudioSource.enabled = true;
        Debug.Log("toggling chart 3d from: " + gameObject.name + ", speechAudioSource.enabled: " + SpeechAudioSource.enabled);
        if (Chart3D!=null && !Chart3D.activeInHierarchy)
        {

            ResetAllFlipcharts();
            Chart3D.SetActive(true);

            //here should first play the speech using play(), then use PlayOneShot() for playing opening sound
            //the oder here is important because the play() will override the PlayOneShot(), not the opposite

            StartCoroutine(PlayOpenAndSpeech());
            //ControlButton.GetComponent<KnobReactor>().go = Chart3D;

        }
        else if(Chart3D.activeInHierarchy)
        {
            gameObject.GetComponent<AudioSource>().Stop();
            PlayOpenSound();
            Chart3D.SetActive(false);
            //ControlButton.GetComponent<KnobReactor>().go = null;
        }
    }

    public void ShowChart3D(bool show)
    {
        if (show)
        {
            if (Chart3D.activeInHierarchy)
            {
                return;
            }
            ResetAllFlipcharts();
            Chart3D.SetActive(true);
            //here should first play the speech using play(), then use PlayOneShot() for playing opening sound
            //the oder here is important because the play() will override the PlayOneShot(), not the opposite

            StartCoroutine(PlayOpenAndSpeech());

        }
        else
        {
            PlayOpenSound();
            Chart3D.SetActive(false);
            Exhibitor.GetComponent<AudioSource>().Stop();
        }
    }

    public override IEnumerator PlayOpenAndSpeech()
    {
        yield return new WaitForSeconds(PlayOpenSound());
        PlaySpeech();
    }

    public override void PlaySpeech()
    {
        Debug.Log("start to play speech from exhibitor...");
        SpeechAudioSource.enabled = true;
        SpeechAudioSource.clip = SpeechAudio;
        SpeechAudioSource.Play();
        
    }

    //protected override void PlayOpenSound()
    //{
    //    print("Play Open Sound");
    //    Exhibitor.GetComponent<AudioSource>().PlayOneShot(OpenSound);
    //}

    public static void KillAllReportings()
    {
        foreach (var reporting in GameObject.FindGameObjectsWithTag("Reporting"))
        {
            reporting.SetActive(false);
        }
    }

    public override void ResetAllFlipcharts()
    {
        KillAllReportings();
        foreach (var flipchart in GameObject.FindGameObjectsWithTag("Flipchart"))
        {
            var audioSource = flipchart.GetComponent<AudioSource>();
            if (audioSource && audioSource.isPlaying)
            {
                audioSource.enabled = false;
                audioSource.Stop();
            }
        }
    }

    public override void HandlePointerInTriggerEvent()
    {
        base.HandlePointerInTriggerEvent();
        ToggleChart3D();
    }

    public override void HandlePointerOutTriggerEvent()
    {
        ShowChart3D(false);
    }

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);
        ChangeMaterial(true);
    }

    public override void StopUsing(GameObject usingObject)
    {
        base.StopUsing(usingObject);
        ChangeMaterial(false);

    }



}
