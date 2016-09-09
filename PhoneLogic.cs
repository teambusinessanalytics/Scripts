using UnityEngine;
using System.Collections;
using VRTK;

public class PhoneLogic : VRTK_InteractableObject {
    public AudioClip[] playList;
    public string musicSourceName;
    AudioSource musicSource;
    int counter = 0;

    void Start()
    {
        musicSource= GameObject.Find(musicSourceName).GetComponent<AudioSource>();
        musicSource.loop = false;
        var pickup = gameObject.GetComponent<PickupLogic>();
        var originalMatrials = pickup.GetComponent<Renderer>().materials;
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
