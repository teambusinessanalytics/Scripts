using UnityEngine;
using System.Collections;
using VRTK;

public class PhoneLogic : PickupLogic {
    public AudioClip[] playList;
    public string musicSourceName;
    AudioSource musicSource;
    int counter = 0;

    new void Start()
    {
        base.Start();
        musicSource= GameObject.Find(musicSourceName).GetComponent<AudioSource>();
        musicSource.loop = false;
        var pickup = gameObject.GetComponent<PickupLogic>();
        var originalMatrials = pickup.GetComponent<Renderer>().materials;
    }
    new void Update()
    {
        base.Update();
        Debug.Log("phone playing music from: " + musicSource.name + ": " + musicSource.isPlaying);
        if (!musicSource.isPlaying && counter>=0)
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

    public override void StartUsing(GameObject currentUsingObject)
    {
        ChangeMusic();
    }
}
