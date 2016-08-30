using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI; //Required when using UI Elements.
using UnityEngine.EventSystems; // Required when using event data.
using System.Linq;

public class Fader : MonoBehaviour
{


    public bool UseAsync;
    private AsyncOperation async = null;
    private bool isLoadingScene = false;

    public bool start = false;
    public float fadeDamp = 0.5f;
    public string fadeScene;
    public float alpha = 0.0f;
    public Color fadeColor;
    public bool isFadeIn = false;
    private int delayBeforeFadeIn = 0;
    public Vector3 finalPositionVector;
    public Quaternion finalRotationVector;

    private Mesh sphere;
    private GameObject fadingSphere;
    private GameObject room;

    // Use this for initialization
    void Start()
    {

        //		sphere = GameObject.Find ("Sphere").GetComponent<MeshFilter>().mesh;
        //		sphere.uv = sphere.uv.Select(o => new Vector2(1 - o.x, o.y)).ToArray();
        //		sphere.triangles = sphere.triangles.Reverse().ToArray();
        //		sphere.normals = sphere.normals.Select(o => -o).ToArray();

        fadingSphere = GameObject.Find("FaderSphere");
    }

    //	public IEnumerator SceneLoad() {
    //
    //		isLoadingScene = true;
    //		AsyncOperation async = SceneManager.LoadSceneAsync(fadeScene);
    //		yield return async;
    //	}

    void Update()
    {
        if (!start)
            return;

        if (isFadeIn)
        {
            
            //if (delayBeforeFadeIn > 50) {
            alpha = Mathf.Lerp(alpha, -2f, fadeDamp * Time.deltaTime);
            //} else {
            // Move CardboardMain
            GameObject cardboard = GameObject.Find("CardboardMain");
            //print (cardboard.transform.position.x + " " +cardboard.transform.position.y + " " +cardboard.transform.position.z + " " );
            cardboard.transform.position = finalPositionVector;
            cardboard.transform.rotation = finalRotationVector;
            fadingSphere.transform.position = finalPositionVector;
            fadingSphere.transform.rotation = finalRotationVector;

            //delayBeforeFadeIn++;
            //}
            if (alpha <= 0)
            {
                isFadeIn = false;
                start = false;
                Destroy(gameObject);
            }

        }
        else
        {
            alpha = Mathf.Lerp(alpha, 2f, fadeDamp * Time.deltaTime);
            if (alpha > 1)
            {
                isFadeIn = true;
                GameObject.Find("ButtonToEnterLogo").SetActive(false);
                GameObject.Find("SE Logo").GetComponent<AudioSource>().Play();
            }

        }

        Color color = fadingSphere.GetComponent<MeshRenderer>().material.color;
        color.a = alpha;
        fadingSphere.GetComponent<MeshRenderer>().material.color = color;

        if (alpha >= 1 && !isFadeIn /*&& !isLoadingScene */)
        {

            //DontDestroyOnLoad(fadingSphere);		
            //DontDestroyOnLoad(gameObject);			
            //DontDestroyOnLoad(room);	

            //SceneManager.LoadScene ("begin_dummy");

            //StartCoroutine("SceneLoad");

        }
        else
            if (alpha <= 0 && isFadeIn)
        {
            //Destroy(fadingSphere);		
        }

    }
    //	void OnLevelWasLoaded (int level){
    //		// turn sphere inside out
    //		//		var mesh = GameObject.Find ("Sphere").GetComponent<MeshFilter>().mesh;
    //
    //		// Main action is to start fading in
    //		isFadeIn = true;
    //	}
    //
}
