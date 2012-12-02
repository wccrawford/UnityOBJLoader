using UnityEngine;
using System.Collections;
using Kelahn.OBJ;

public class TestParse : MonoBehaviour {
	public Material testMaterial;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		if(GUILayout.Button("Test")) {
			ImportOBJ();
		}
	}
	
	void ImportOBJ() {
		string fileContents = "# Blender v2.64 (sub 0) OBJ File: 'testpiece.blend'\n" +
"# www.blender.org\n" +
"mtllib testpiece.mtl\n" +
"o Cube\n" +
"v -1.000000 -1.000000 1.000000\n" +
"v -1.000000 -1.000000 -1.000000\n" +
"v 1.000000 -1.000000 -1.000000\n" +
"v 1.000000 -1.000000 1.000000\n" +
"v -0.860198 1.000000 0.382311\n" +
"v -0.860198 1.000000 -0.382311\n" +
"v -0.095576 1.000000 -0.382311\n" +
"v -0.095576 1.000000 0.382311\n" +
"vt 0.000000 0.358953\n" +
"vt 0.341039 0.357906\n" +
"vt 0.236759 0.700100\n" +
"vt 0.106377 0.700500\n" +
"vt 0.657143 0.357906\n" +
"vt 0.526761 0.357506\n" +
"vt 0.341039 0.000000\n" +
"vt 0.682077 0.001046\n" +
"vt 0.812860 0.253152\n" +
"vt 0.812460 0.122769\n" +
"vt 0.998954 0.016868\n" +
"vt 1.000000 0.357906\n" +
"vt 0.341039 0.356860\n" +
"vt 0.000000 0.357906\n" +
"vt 0.185722 0.000400\n" +
"vt 0.316105 0.000000\n" +
"vt 0.342085 0.699991\n" +
"vt 0.341039 0.358953\n" +
"vt 0.682077 0.357906\n" +
"vt 0.683124 0.698945\n" +
"vt 0.682477 0.253552\n" +
"vt 0.682077 0.123169\n" +
"usemtl \n" +
"s off\n" +
"f 2/1 1/2 5/3 6/4\n" +
"f 6/5 7/6 3/7 2/8\n" +
"f 7/9 8/10 4/11 3/12\n" +
"f 1/13 4/14 8/15 5/16\n" +
"f 1/17 2/18 3/19 4/20\n" +
"f 8/10 7/9 6/21 5/22\n" +
"";
		Model obj = new Model(fileContents);
		
		GameObject newObject = new GameObject("newObject");
		newObject.AddComponent("MeshFilter");
		newObject.AddComponent("MeshRenderer");
		
		Mesh mesh = newObject.GetComponent<MeshFilter>().mesh;
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
		
		newObject.GetComponent<MeshRenderer>().material = testMaterial;
	}
}
