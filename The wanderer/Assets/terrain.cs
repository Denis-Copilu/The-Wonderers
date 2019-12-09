using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrain : MonoBehaviour
{
    public const int MIN = 20;
    public const int MAX = 50;
    public const int LENGTH = 100;
    public const int SCAN_RADIUS = 2;
    public const int SMOOTH_COUNT = 3;

    float[] heightmap = new float[LENGTH];
    int offset = 0;

    public MeshFilter meshFilter;
    public Mesh mesh;

    void Awake()
    {
        Camera.main.transform.position = new Vector3(LENGTH / 2, LENGTH / 2, -LENGTH);

        mesh = new Mesh();
        mesh.name = "Terrain mesh";
        meshFilter.mesh = mesh;

        GenerateHeightmap(offset);
        BuildMesh();
    }

    void Update()
    {
        int scroll = 0;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) scroll--;
        if (Input.GetKeyDown(KeyCode.RightArrow)) scroll++;

        if (scroll != 0)
        {
            offset += scroll;

            GenerateHeightmap(offset);
            BuildMesh();
        }
    }

    void GenerateHeightmap(int offset)
    {
        UnityEngine.Random.seed = 0;
        for (int j = 0; j < offset + LENGTH; j++)
        {
            float rnd = UnityEngine.Random.Range(MIN, MAX);
            if (j >= offset)
            {
                int i = j - offset;
                heightmap[i] = rnd;
            }
        }

        for (int s = 0; s < SMOOTH_COUNT; s++)
            Smooth();
    }

    void Smooth()
    {
        for (int i = 0; i < heightmap.Length; i++)
        {
            float height = heightmap[i];

            float heightSum = 0;
            float heightCount = 0;

            for (int n = i - SCAN_RADIUS;
                     n < i + SCAN_RADIUS + 1;
                     n++)
            {
                if (n >= 0 &&
                    n < heightmap.Length)
                {
                    float heightOfNeighbour = heightmap[n];

                    heightSum += heightOfNeighbour;
                    heightCount++;
                }
            }

            float heightAverage = heightSum / heightCount;
            heightmap[i] = heightAverage;
        }
    }

    void BuildMesh()
    {
        mesh.Clear();
        List<Vector3> positions = new List<Vector3>();
        List<int> triangles = new List<int>();

        int offset = 0;
        for (int i = 0; i < LENGTH - 1; i++)
        {
            offset = i * 4;

            float h = heightmap[i];
            float hn = heightmap[i + 1];
            positions.Add(new Vector3(i + 0, 0, 0)); //lower left - at index 0
            positions.Add(new Vector3(i + 1, 0, 0)); //lower right - at index 1
            positions.Add(new Vector3(i + 0, h, 0)); //upper left - at index 2
            positions.Add(new Vector3(i + 1, hn, 0)); //upper right - at index 3

            triangles.Add(offset + 0);
            triangles.Add(offset + 2);
            triangles.Add(offset + 1);

            triangles.Add(offset + 1);
            triangles.Add(offset + 2);
            triangles.Add(offset + 3);
        }

        mesh.vertices = positions.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }
}