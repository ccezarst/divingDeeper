using UnityEngine;

public class HighVertexPlaneGenerator : MonoBehaviour
{
    public int gridSize = 100; // Adjust this value to control the number of vertices
    public float scale = 1.0f; // Adjust this value to control the overall size

    void Start()
    {
        GenerateHighVertexPlane();
    }
    void FixedUpdate()
    {
        GenerateHighVertexPlane();
    }
    private void GenerateHighVertexPlane()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();

        int verticesPerSide = gridSize + 1;
        int numVertices = verticesPerSide * verticesPerSide;

        Vector3[] vertices = new Vector3[numVertices];
        Vector2[] uv = new Vector2[numVertices];
        int[] triangles = new int[gridSize * gridSize * 6];

        for (int z = 0; z < verticesPerSide; z++)
        {
            for (int x = 0; x < verticesPerSide; x++)
            {
                int index = z * verticesPerSide + x;
                float xPos = ((float)x / gridSize - 0.5f) * scale;
                float zPos = ((float)z / gridSize - 0.5f) * scale;

                vertices[index] = new Vector3(xPos, 0.0f, zPos);
                uv[index] = new Vector2((float)x / gridSize, (float)z / gridSize);

                if (x < gridSize && z < gridSize)
                {
                    int triangleIndex = (z * gridSize + x) * 6;

                    triangles[triangleIndex] = index;
                    triangles[triangleIndex + 1] = index + verticesPerSide;
                    triangles[triangleIndex + 2] = index + 1;
                    triangles[triangleIndex + 3] = index + 1;
                    triangles[triangleIndex + 4] = index + verticesPerSide;
                    triangles[triangleIndex + 5] = index + verticesPerSide + 1;
                }
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        meshFilter.mesh = mesh;
    }
}