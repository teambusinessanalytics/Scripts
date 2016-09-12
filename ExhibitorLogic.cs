using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ExhibitorLogic : PickupLogic
{
    public GameObject Exhibitor;
    public GameObject Chart3D;
    public AudioClip SpeechAudio;
    public GameObject BackgroundMusicSourceObject;
    protected AudioSource BackgroundMusicSource;
    protected AudioSource SpeechAudioSource;

    private Transform DefaultChartShapeTransform;
    private Transform DefaultChartBackgroundTransform;
    //float originalMusicVolume;
    void Start()
    {
        OriginalMatrials = Exhibitor.GetComponent<Renderer>().materials;
        BackgroundMusicSource = BackgroundMusicSourceObject.GetComponent<AudioSource>();
        SpeechAudioSource = Exhibitor.GetComponent<AudioSource>();
        DefaultChartShapeTransform = Chart3D.transform.GetChild(0); //get the chart shape
        DefaultChartBackgroundTransform = Chart3D.transform.GetChild(1); // get the chart background


        SpeechAudioSource.clip = SpeechAudio;
        SpeechAudioSource.enabled = false;
        print("speechaudiosource: " + SpeechAudioSource.name);
        print("backgroundaudiosource: " + BackgroundMusicSource.name);
    }
    void Update()
    {
        if (SpeechAudio && SpeechAudioSource.enabled == true)
        {
            //SpeechAudioSource.clip = SpeechAudio;
            if (SpeechAudioSource.isPlaying)
            {
                print(SpeechAudioSource.clip.name + " playing...");
                //print("volume turned down to: " + originalMusicVolume.ToString());
                //GameObject.FindObjectOfType<EventSystem>().enabled = false;
                if (BackgroundMusicSource.volume > .05f) BackgroundMusicSource.volume -= .5f * Time.deltaTime;
            }
            else if (!SpeechAudioSource.isPlaying)
            {
                print(SpeechAudioSource.clip.name + " not playing...");
                //GameObject.FindObjectOfType<EventSystem>().enabled = true;
                if (BackgroundMusicSource.volume < 1f)
                {
                    BackgroundMusicSource.volume += .2f * Time.deltaTime;
                }else
                {
                    SpeechAudioSource.enabled = false;
                }
            }
        }
    }

    public override void ChangedMaterial(bool gazed)
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

    public void toggleChart3D()
    {
        SpeechAudioSource.enabled = true;

        if (!Chart3D.activeInHierarchy)
        {
            //here should first play the speech using play(), then use PlayOneShot() for playing opening sound
            //the oder here is important because the play() will override the PlayOneShot(), not the opposite
            PlaySpeech();
            PlayOpenSound();

            ResetAllFlipcharts();
            Chart3D.SetActive(true);
            //Chart3D.GetComponent<Animator>().Play("show_reporting");
            Chart3D.GetComponent<Transform>().GetChild(0).position = DefaultChartShapeTransform.position;
            Chart3D.GetComponent<Transform>().GetChild(1).position = DefaultChartBackgroundTransform.position;

            
        }
        else if (Chart3D.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            PlayOpenSound();
            Chart3D.SetActive(false);
            Exhibitor.GetComponent<AudioSource>().Stop();
        }
    }

    public void showChart3D(bool show)
    {
        SpeechAudioSource.enabled = true;
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
            PlaySpeech();
            PlayOpenSound();
        }
        else
        {
            PlayOpenSound();
            Chart3D.SetActive(false);
            Exhibitor.GetComponent<AudioSource>().Stop();
        }
    }

    void PlaySpeech()
    {
        Exhibitor.GetComponent<AudioSource>().clip = SpeechAudio;
        Exhibitor.GetComponent<AudioSource>().Play();
    }

    protected override void PlayOpenSound()
    {
        print("Play Open Sound");
        Exhibitor.GetComponent<AudioSource>().PlayOneShot(OpenSound);
    }

    static void KillAllReportings()
    {
        foreach (var reporting in GameObject.FindGameObjectsWithTag("Reporting"))
        {
            reporting.SetActive(false);
        }
    }

    public void ResetAllFlipcharts()
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
}
