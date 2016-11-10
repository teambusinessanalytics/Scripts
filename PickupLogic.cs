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

    protected Animator animator;
    protected Transform originalTransform;

    new void Start()
    {
        base.Start();
        Pickup = gameObject;
        OriginalMatrials = Pickup.GetComponent<Renderer>().materials;
        originalTransform = gameObject.transform;
        animator = gameObject.GetComponent<Animator>();
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
            //if (GazedMaterial) Pickup.GetComponent<Renderer>().material = GazedMaterial;
            if(HighLight) HighLight.SetActive(true);
        }
        else
        {
            //Pickup.GetComponent<Renderer>().materials = OriginalMatrials;
            if (HighLight) HighLight.SetActive(false);
        };
    }

    public virtual void ObjectEvo()
    {
        HighLight.SetActive(false);
        animator.SetBool("JumpToLaptop", true);
    }

    public void ActivateAppIcon()
    {
        print( "activate app");
        if (AppIcon.activeSelf==true)
        {
            AppIcon.SetActive(false);
            AppIcon.SetActive(true);
        }
        else
        {
            AppIcon.SetActive(true);
        }
        //Dead();
        
    }
    
    public static void ResetScene()
    {
        //print("reset scene");
        //var tableScene = GameObject.Find("TableScene");
        //var officeScene = GameObject.Find("OfficeScene");
        //var controller = GameObject.Find("Controller");
        //var gameManagers = GameObject.Find("GameManagers");

        //Destroy(tableScene);
        //Destroy(officeScene);
        //Destroy(controller);
        //Destroy(gameManagers);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name); //TODO: fix the reset crash problem
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
