﻿using UnityEngine;
using System.Collections;
using System.Linq;


public class TurnSphereInsideOut : MonoBehaviour {

	private Mesh sphere;
	private Mesh room;


	// Use this for initialization
	void Start () {	

		sphere = GameObject.Find ("FaderSphere").GetComponent<MeshFilter>().mesh;
		sphere.uv = sphere.uv.Select(o => new Vector2(1 - o.x, o.y)).ToArray();
		sphere.triangles = sphere.triangles.Reverse().ToArray();
		sphere.normals = sphere.normals.Select(o => -o).ToArray();

		room = GameObject.Find ("BlackRoomLogo").GetComponent<MeshFilter>().mesh;
		room.uv = room.uv.Select(o => new Vector2(1 - o.x, o.y)).ToArray();
		room.triangles = room.triangles.Reverse().ToArray();
		room.normals = room.normals.Select(o => -o).ToArray();

		room = GameObject.Find ("BlackRoomStart").GetComponent<MeshFilter>().mesh;
		room.uv = room.uv.Select(o => new Vector2(1 - o.x, o.y)).ToArray();
		room.triangles = room.triangles.Reverse().ToArray();
		room.normals = room.normals.Select(o => -o).ToArray();
	}

	// Update is called once per frame
	void Update () {

	}
}
