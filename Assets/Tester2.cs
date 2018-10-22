using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester2 : MonoBehaviour {
	public MeshFilter meshFilter;

	void OnEnable() {
		meshFilter = GetComponent<MeshFilter>();
		meshFilter.mesh.MarkDynamic();
	}

	[ContextMenu("Boudle")]
	void Double() {
		var mesh = meshFilter.mesh;
		var verts = mesh.vertices;
		var tris = mesh.triangles;

		var vertList = new List<Vector3>();
		var triList = new List<int>();
		vertList.AddRange(verts);
		vertList.AddRange(verts);
		triList.AddRange(tris);
		triList.AddRange(tris);

		mesh.Clear();

		mesh.SetVertices(vertList);
		mesh.SetTriangles(triList, 0);
	}
}
