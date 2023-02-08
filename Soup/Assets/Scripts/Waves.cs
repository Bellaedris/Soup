
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vert;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vert = mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] newVert = mesh.vertices;
        for (int i = 0; i < vert.Length; i++)
        {
            newVert[i] = vert[i] + mesh.normals[i] * WaveManager.instance.GetWaveLength(vert[i].x, vert[i].z);
        }

        mesh.vertices = newVert;
        mesh.RecalculateNormals();
    }
}
