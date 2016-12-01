using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class GoogleMapController : MonoBehaviour {

    public bool render;
    public GameObject mapObject;
    private bool rotate;
    private MeshFilter mesh;
    private MeshPixelTools meshPixelTools;

    void Awake()
    {
        Messenger.AddListener(GameEvent.GOOGLEMAP_UPDATED, OnMapUpdated);
    }

    void Destroy()
    {
        Messenger.RemoveListener(GameEvent.GOOGLEMAP_UPDATED, OnMapUpdated);

    }

    void Start()
    {
        rotate = false;
        mesh = mapObject.GetComponent<MeshFilter>();
        meshPixelTools = new MeshPixelTools();
    }

    void Update()
    {
        if (mesh.name == "Sphere" && rotate == true)
        {
            mapObject.transform.Rotate(mapObject.transform.up, -5 * Time.deltaTime);
        }

    }

    private void OnMapUpdated()
    {
        if (render)
        {
            var markerPositions3D = new List<Vector3>();
            mapObject.GetComponent<Renderer>().material.mainTexture = Managers.Map.mapTexure;
            foreach (var pix in Managers.Map.markerPositionsPix)
            {
                //2. Calculate the UV coordinate of the pixel coordinate.
                //3. Calculate the world coordinate of the texel the UV coordinate resides at.
                markerPositions3D.Add(meshPixelTools.UvTo3D(PixelToUV(pix.x, pix.y), mapObject));
            }

            foreach (var position in markerPositions3D)
            {
                
                var marker = GameObject.CreatePrimitive(PrimitiveType.Cube);
                marker.GetComponent<Renderer>().material.color = Random.ColorHSV();
                marker.name = position.x.ToString() + ", " + position.y.ToString();

                //place the markers
                marker.transform.position = position;

                //scale the markers
                marker.transform.localScale = new Vector3(.1f, Random.Range(0, 1f), .1f);

                //if the marker is on a sphere, then the bars should be rotated so that they can stand on the surface of sphere
                if (mesh.name == "Sphere")
                {
                    //calculate the normal to the sphere's surface at the particular location the object placed
                    //then rotate the object so that the object's up vector is equal to the normal.
                    //https://forum.unity3d.com/threads/placing-objects-on-a-sphere.107055/, http://answers.unity3d.com/questions/541630/rotate-to-orient-to-sphere-surface-but-maintain-lo.html

                    var centerOfSphere = mapObject.transform.position;
                    var placePosition = position;
                    var normal = (placePosition - centerOfSphere).normalized;
                    marker.transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
                    rotate = true;

                }

                //adjust the position of the marker because the pivot point is in the middle of the marker
                marker.transform.position = marker.transform.position + marker.transform.up * marker.transform.localScale.y / 2;
                //finally attach to the holding object
                marker.transform.parent = mapObject.transform;

            }
        }

    }

    private Vector2 PixelToUV(float x, float y)
    {
        var width = (float)mapObject.GetComponent<Renderer>().material.mainTexture.width;
        var height = (float)mapObject.GetComponent<Renderer>().material.mainTexture.height;
        Debug.Log("texture width: " + width + ", height: " + height);
        Vector2 uv = new Vector2(x / width, 1f - y / height);

        Debug.Log("uv: " + uv.ToString());
        return uv;
    }
}

