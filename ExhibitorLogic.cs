﻿using UnityEngine;
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
    public Animator anim;

    //float originalMusicVolume;
    void Start()
    {
        OriginalMatrials = Exhibitor.GetComponent<Renderer>().materials;
        BackgroundMusicSource = BackgroundMusicSourceObject.GetComponent<AudioSource>();
        SpeechAudioSource = Exhibitor.GetComponent<AudioSource>();
        anim = Chart3D.GetComponent<Animator>();

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
        Debug.Log("current animation is: " + anim.GetCurrentAnimatorStateInfo(0).IsName("show_reporting"));

        if (!Chart3D.activeInHierarchy)
        {
            ResetAllFlipcharts();
            Chart3D.SetActive(true);
            anim.Play("show_reporting");

            //here should first play the speech using play(), then use PlayOneShot() for playing opening sound
            //the oder here is important because the play() will override the PlayOneShot(), not the opposite
            PlaySpeech();
            PlayOpenSound();
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
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

    void KillAllReportings()
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
