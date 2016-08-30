using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Threading;

public class se_cube : MonoBehaviour
{

    public string scene;
    public Color myColor;
    public float damp;
    public Vector3 finalPositionVector;
    public Quaternion finalRotationVector;

    private Mesh room;

    public void PlayIntro()
    {
        GameObject.Find("BlackRoomStart").transform.FindChild("CanvasIntro").gameObject.SetActive(true);
        GameObject.Find("BlackRoomStart").GetComponent<AudioSource>().Play();
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        StopCoroutine(EnterLogo());
        StartCoroutine(EnterLogo());

    }
    public IEnumerator EnterLogo()
    {

        print("Entering logo.");
        yield return new WaitForSeconds(20);
        Initiate.Fade(scene, myColor, finalPositionVector, finalRotationVector, damp);
        yield return new WaitForSeconds(5);
        
    }
    public void EnterOffice()
    {

        print("Entering office.");
        
        Initiate.Fade(scene, myColor, finalPositionVector, finalRotationVector, damp);
        GameObject.Find("SE Logo").GetComponent<AudioSource>().Stop();
        //only when entering the office will the music start to play
        GameObject.Find("Phone").GetComponent<PhoneLogic>().ChangeMusic();
        GameObject.Find("BlackRoomStart").SetActive(false);
    }


    // Use this for initialization

    void Start()
    {

    }

    void Update()
    {
        //GameObject.Find("SE Logo").GetComponent<Collider>().enabled = !GameObject.Find("SE Logo").GetComponent<AudioSource>().isPlaying;
        if(GameObject.Find("BlackRoomStart") && GameObject.Find("BlackRoomStart").GetComponent<AudioSource>().isPlaying) GameObject.Find("BlackRoomStart").GetComponent<AudioSource>().volume -= .02f * Time.deltaTime;
    }
}
