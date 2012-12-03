using UnityEngine;
using System.Collections;
using Kelahn.OBJ;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class OBJLoader : MonoBehaviour {
	public void CreateMesh(string objContents) {
		Model obj = new Model(objContents);
		
		Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
		mesh.Clear();
		
		float[][] objVerts = obj.getVertices();
		Vector3[] vertices = new Vector3[objVerts.Length];
		for(int index = 0; index < objVerts.Length; index++) {
			vertices[index] = new Vector3((0 - objVerts[index][0]), (objVerts[index][1]), (objVerts[index][2]));
		}
		mesh.vertices = vertices;
		
		float[][] objUVs = obj.getUVs();
		if((objUVs != null) && (objUVs.Length > 0)) {
			Vector2[] uv = new Vector2[objUVs.Length];
			for(int index = 0; index < objUVs.Length; index++) {
				uv[index] = new Vector2((objUVs[index][0]), (objUVs[index][1]));
			}
			mesh.uv = uv;
		}
		
		float[][] objNorms = obj.getNormals();
		if((objNorms != null) && (objNorms.Length > 0)) {
			Vector3[] normals = new Vector3[objNorms.Length];
			for(int index = 0; index < objNorms.Length; index++) {
				Debug.Log(objNorms[index]);
				normals[index] = new Vector3((0 - objNorms[index][0]), (objNorms[index][1]), (objNorms[index][2]));
			}
			mesh.normals = normals;
		} else {
			mesh.RecalculateNormals();
		}
		
		int[][] objFaces = obj.getFaces();
		int[] faces = new int[objFaces.Length * 3];
		for(int index = 0; index < objFaces.Length; index++) {
			faces[(index * 3) + 0] = objFaces[index][2];
			faces[(index * 3) + 1] = objFaces[index][1];
			faces[(index * 3) + 2] = objFaces[index][0];
		}
		mesh.triangles = faces;
	}
}
