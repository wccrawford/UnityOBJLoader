using UnityEngine;
using System.Collections;

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
		GameObject newObject = new GameObject("newObject");
		newObject.AddComponent("OBJLoader");
		OBJLoader objLoader = newObject.GetComponent<OBJLoader>();
		objLoader.CreateMesh(fileContents);
				
		newObject.GetComponent<MeshRenderer>().material = testMaterial;
	}
}
