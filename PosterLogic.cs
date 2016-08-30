using UnityEngine;
using System.Collections;

public class PosterLogic : ExhibitorLogic {

    void Start()
    {
        BackgroundMusicSource = BackgroundMusicSourceObject.GetComponent<AudioSource>();
        SpeechAudioSource = Exhibitor.GetComponent<AudioSource>();
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
                if (BackgroundMusicSource.volume > .00f) BackgroundMusicSource.volume -= .5f * Time.deltaTime;
            }
            else if (!SpeechAudioSource.isPlaying)
            {
                print(SpeechAudioSource.clip.name + " not playing...");
                if (BackgroundMusicSource.volume < 1f)
                {
                    BackgroundMusicSource.volume += .2f * Time.deltaTime;
                }
                else
                {
                    SpeechAudioSource.enabled = false;
                }
            }
        }
    }
	public void PlayMusic()
    {
        SpeechAudioSource.enabled = true;
        SpeechAudioSource.GetComponent<AudioSource>().clip = SpeechAudio;
        if (!SpeechAudioSource.isPlaying)
        {
            SpeechAudioSource.GetComponent<AudioSource>().Play();
        }else
        {
            SpeechAudioSource.GetComponent<AudioSource>().Stop();
        }
        
    }
}
