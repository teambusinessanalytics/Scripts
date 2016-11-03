﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using VRTK;

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

    public virtual void ChangedMaterial (bool gazed)
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

    protected virtual void PlayOpenSound()
    {
        gameObject.GetComponent<AudioSource>().clip = OpenSound;
        gameObject.GetComponent<AudioSource>().Play();
        print("play open sound");
    }

    public virtual void HandlePointerInTriggerEvent()
    {

    }
    public virtual void HandlePointerOutTriggerEvent()
    {

    }
    public virtual void ResetAllFlipcharts()
    {

    }

}
