using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using VRTK;
using System.Reflection;

public class PickupLogic : VRTK_InteractableObject
{
    public Material GazedMaterial;
    protected Material[] OriginalMatrials;
    protected GameObject Pickup;
    public string EvoAnimation;
    public GameObject AppIcon;
    public GameObject Chart2D;
    public AudioClip OpenSound;
    public GameObject HighLight;

    new void Start()
    {
        base.Start();
        Pickup = gameObject;
        OriginalMatrials = Pickup.GetComponent<Renderer>().materials;  
    }


    void Dead()
    {
        Pickup.SetActive(false);
    }

    public virtual void ChangeMaterial (bool gazed)
    {
        if (gazed)
        {
            //List<Material> materials = new List<Material>(Pickup.GetComponent<Renderer>().materials);
            //materials.Add(GazedMaterial);
            //Pickup.GetComponent<Renderer>().materials = materials.ToArray();
            if (GazedMaterial) Pickup.GetComponent<Renderer>().material = GazedMaterial;
            if(HighLight) HighLight.SetActive(true);
        }
        else
        {
            Pickup.GetComponent<Renderer>().materials = OriginalMatrials;
            if (HighLight) HighLight.SetActive(false);
        };
    }

    public void ObjectEvo()
    {
        HighLight.SetActive(false);
        Pickup.GetComponent<Animator>().Play(EvoAnimation);
    }

    public void ActivateAppIcon()
    {
        print( "activate app");
        if (AppIcon.activeSelf==true)
        {
            AppIcon.SetActive(false);
            AppIcon.SetActive(true);
        }else
        {
            AppIcon.SetActive(true);
        }
        Dead();
        
    }
    
    public void ResetScene()
    {
        print("reset scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public virtual IEnumerator PlayOpenAndSpeech()
    {
        yield return new WaitForSeconds(PlayOpenSound());
        PlaySpeech();
    }

    public virtual float PlayOpenSound()
    {
        var source = gameObject.GetComponent<AudioSource>();
        source.enabled = true;
        source.clip = OpenSound;
        source.Play();
        print("play open sound: " + OpenSound.length);
        return OpenSound.length;
    }

    public virtual void PlaySpeech()
    {
        return;
    }

    public virtual void HandlePointerInTriggerEvent()
    {
        PlayOpenSound();
        return;
    }

    public virtual void HandlePointerOutTriggerEvent()
    {
        return;
    }
    public virtual void ResetAllFlipcharts()
    {
        return;
    }

}
