using UnityEngine;
using System.Collections;

public class PhoneLogic : PickupLogic {
    public AudioClip[] playList;
    public string musicSourceName;
    AudioSource musicSource;
    int counter = 0;

    void Start()
    {
        musicSource= GameObject.Find(musicSourceName).GetComponent<AudioSource>();
        musicSource.loop = false;
        Pickup = gameObject;
        OriginalMatrials = Pickup.GetComponent<Renderer>().materials;
    }
    void Update()
    {
        if (!musicSource.isPlaying && counter>0)
        {
            ChangeMusic();
        }
    }
    public void ChangeMusic()
    {
        musicSource.clip = playList[counter%playList.Length];
        musicSource.Play();
        counter++;
    }
}
