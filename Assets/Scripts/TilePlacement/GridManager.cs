
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GridManager : MonoBehaviour
{
    Collider col;
    public float GridSize = 1f;
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    void Start()
    {

        col = GetComponent<Collider>();
        int numCells = (int)(col.bounds.size.x / GridSize);
        Grid = new GridItem[numCells, numCells];
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        Vector3 offset = col.bounds.extents;
        var gridmesh = new Mesh();
        List<Vector3> vecs = new List<Vector3>();
        List<int> Lines = new List<int>();
        for (int i = 0; i <= numCells; i++)
        {
            vecs.Add(new Vector3(i * GridSize, 0, 0) - offset);
            vecs.Add(new Vector3(i * GridSize, 0, numCells * GridSize) - offset);
            Lines.Add(4 * i + 0);
            Lines.Add(4 * i + 1);
            vecs.Add(new Vector3(0, 0, i * GridSize) - offset);
            vecs.Add(new Vector3(numCells * GridSize, 0, i * GridSize) - offset);
            Lines.Add(4 * i + 2);
            Lines.Add(4 * i + 3);
        }
       
        gridmesh.vertices = vecs.ToArray();
        gridmesh.SetIndices(Lines.ToArray(), MeshTopology.Lines, 0);
        meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
        meshRenderer.material.color = Color.green;
        meshFilter.mesh = gridmesh;
    }
    void Update()
    {

    }
    public GridItem[,] Grid;
}
