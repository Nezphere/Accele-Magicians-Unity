using UnityEngine;
using Futilef.V2.Render;

public class MeshTester : MonoBehaviour {
	Mesh mesh;
	Batch batch;

	void OnEnable() {
		mesh = GetComponent<MeshFilter>().mesh;
		mesh.MarkDynamic();

		batch = new Batch();
	}

	void Update() {
		batch.Request(4, 2);
		int vertI = batch.vertI, triI = batch.triI;
		var verts = batch.verts;
		verts[vertI].Set(0, 1, 0);
		verts[vertI + 1].Set(1, 1, 0);
		verts[vertI + 2].Set(1, 0, 0);
		verts[vertI + 3].Set(0, 0, 0);
		var tris = batch.tris;
		tris[triI] = vertI;
		tris[triI + 1] = vertI + 1;
		tris[triI + 2] = vertI + 2;
		tris[triI + 3] = vertI + 2;
		tris[triI + 4] = vertI + 3;
		tris[triI + 5] = vertI;

		batch.Request(4, 2);
		vertI = batch.vertI; triI = batch.triI;
		verts = batch.verts; tris = batch.tris;
		verts[vertI].Set(0, 0, 100);
		verts[vertI + 1].Set(1, 0, 100);
		verts[vertI + 2].Set(1, 0, 0);
		verts[vertI + 3].Set(0, 0, 0);
		tris[triI] = vertI;
		tris[triI + 1] = vertI + 1;
		tris[triI + 2] = vertI + 2;
		tris[triI + 3] = vertI + 2;
		tris[triI + 4] = vertI + 3;
		tris[triI + 5] = vertI;

		batch.End();
		mesh.vertices = batch.verts;
		mesh.triangles = batch.tris;
		mesh.RecalculateNormals();
	}
}
