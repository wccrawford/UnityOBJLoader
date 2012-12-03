using System.Collections;

namespace Kelahn.OBJ {
	public class Model {
		protected float[][] vertices;
		protected float[][] vertexnormals;
		protected float[][] vertextextures;
		
		protected int[][] faces;
		//protected int[][] vertexfaces;
		//protected int[][] vertexnormalfaces;
		//protected int[][] vertextexturefaces;
		
		public Model(string source) {
			parse(source);
		}
		
		public float[][] getVertices() {
			return vertices;
		}
		
		public float[][] getNormals() {
			return vertexnormals;
		}
		
		public float[][] getUVs() {
			return vertextextures;
		}
		
		public int[][] getFaces() {
			return faces;
		}
		
		public void parse(string source) {
			ArrayList vertArray = new ArrayList();
			ArrayList vertnormalArray = new ArrayList();
			ArrayList verttextureArray = new ArrayList();
			
			ArrayList vertexfaceArray = new ArrayList();
			ArrayList vertexnormalfaceArray = new ArrayList();
			ArrayList vertextexturefaceArray = new ArrayList();
			
			string[] lines = source.Split('\r', '\n');
			foreach (string line in lines) {
				// Comments start with #, and blank lines are useless
				if ((line.Length > 0) && (line[0] == '#')) {
					continue;
				}
				string[] pieces = line.Split(' ');
				switch(pieces[0]) {
					case "v": // Vertices
						vertArray.Add(new float[3] { float.Parse(pieces[1]), float.Parse(pieces[2]), float.Parse(pieces[3])});
						break;
					case "vt": // Texture Vertices
						verttextureArray.Add(new float[2] { float.Parse(pieces[1]), float.Parse(pieces[2])});
						break;
					case "vn": // Vertex Normals
						vertnormalArray.Add(new float[3] { float.Parse(pieces[1]), float.Parse(pieces[2]), float.Parse(pieces[3])});
						break;
					case "f": // Faces
						// Faces have 3 or more vertexes.  Since Unity wants triangles, we assume that the face is convex
						// and split it into triangles.
						for(int faceIndex = 3; faceIndex < pieces.Length; faceIndex++) {
							string[][] face = new string[3][];
							face[0] = pieces[1].Split('/');
							face[1] = pieces[faceIndex - 1].Split('/');
							face[2] = pieces[faceIndex].Split('/');
							
							vertexfaceArray.Add(new int[3] { int.Parse(face[0][0]), int.Parse(face[1][0]), int.Parse(face[2][0])});
							if((face[0].Length > 1) && (face[0][1] != "")) {
								vertextexturefaceArray.Add(new int[3] { int.Parse(face[0][1]), int.Parse(face[1][1]), int.Parse(face[2][1])});
							}
							if((face[0].Length > 2) && (face[0][2] != "")) {
								vertexnormalfaceArray.Add(new int[3] { int.Parse(face[0][2]), int.Parse(face[1][2]), int.Parse(face[2][2])});
							}
						}
						break;
				}
			}
			
			vertices = new float[vertexfaceArray.Count * 3][];
			if(vertexnormalfaceArray.Count > 0) {
				vertexnormals = new float[vertexfaceArray.Count * 3][];
			} else {
				vertexnormals = null;
			}
			if(vertextexturefaceArray.Count > 0) {
				vertextextures = new float[vertexfaceArray.Count * 3][];
			} else {
				vertextextures = null;
			}
			faces = new int[vertexfaceArray.Count][];
			for(int index = 0; index < vertexfaceArray.Count; index++) {
				faces[index] = new int[3] {0, 0, 0};
				int[] facevertex = (int[])vertexfaceArray[index];
				
				vertices[(index * 3) + 0] = (float[])vertArray[facevertex[0]-1];
				vertices[(index * 3) + 1] = (float[])vertArray[facevertex[1]-1];
				vertices[(index * 3) + 2] = (float[])vertArray[facevertex[2]-1];
				
				if((vertextextures != null) && (vertextexturefaceArray.Count > index)) {
					int[] facetexture = (int[])vertextexturefaceArray[index];
					vertextextures[(index * 3) + 0] = (float[])verttextureArray[facetexture[0]-1];
					vertextextures[(index * 3) + 1] = (float[])verttextureArray[facetexture[1]-1];
					vertextextures[(index * 3) + 2] = (float[])verttextureArray[facetexture[2]-1];
				}
				
				if((vertexnormals != null) && (vertexnormalfaceArray.Count > index)) {
					int[] facenormal = (int[])vertexnormalfaceArray[index];
					vertexnormals[(index * 3) + 0] = (float[])vertnormalArray[facenormal[0]-1];
					vertexnormals[(index * 3) + 1] = (float[])vertnormalArray[facenormal[1]-1];
					vertexnormals[(index * 3) + 2] = (float[])vertnormalArray[facenormal[2]-1];
				}
				
				faces[index][0] = (index * 3) + 0;
				faces[index][1] = (index * 3) + 1;
				faces[index][2] = (index * 3) + 2;
			}
		}
	}
}
