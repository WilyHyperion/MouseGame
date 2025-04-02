
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GridManager : MonoBehaviour
{
    Collider col;
    /// <summary>
    /// The size of a grid cell
    /// </summary>
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
    public GridItem TestPlacer;

    public GridItem TestPlacer2;
    void Update()
    {
        meshRenderer.forceRenderingOff = !Cam.InPlaceMode;
        if (Cam.InPlaceMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                (int, int) index;
                if (GetMouseHoverIndex(out index))
                {
                    TryPlaceIn(index, TestPlacer);
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                (int, int) index;
                if (GetMouseHoverIndex(out index))
                {
                    TryPlaceIn(index, TestPlacer2);
                }
            } 
        }
    }
    /// <summary>
    /// Places an object into the grid as long as no object exists at that location currently
    /// </summary>
    /// <param name="index"></param>
    /// <param name="tryPlace"></param>
    /// <returns>If the object placed (i.e, if it the index was empty)</returns>
    public bool TryPlaceIn((int, int) index, GridItem tryPlace)
    {
        if (Grid[index.Item1, index.Item2] == null)
        {
            ForcePlaceGridItem(index, tryPlace);
            return true;
        }
        return false;
    }

    public void ForcePlaceGridItem((int, int) index, GridItem place)
    {
        if (Grid[index.Item1, index.Item2] != null)
        {
            //TODO remove old object
        }
        Grid[index.Item1, index.Item2] = place;
        //TODO rotation
        Instantiate(place.Placed, IndexToWorld(index), transform.rotation, transform );

    }
    public Vector3 IndexToWorld((int, int) index)
    {
        return col.bounds.min + new Vector3(index.Item1 * GridSize + (GridSize/2), 0, index.Item2 * GridSize + (GridSize / 2));
    }
    public Camera cam;
    public bool GetMouseHoverIndex(out (int, int) index)
    {
        Physics.queriesHitTriggers = true;
        index = (-1, -1);
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider == this.col)
            {
                var loc = new Vector2(hit.point.x, hit.point.z);
                //first get distance bottom left, then divide by cell size
                index.Item1 = (int)((loc.x - col.bounds.min.x)/GridSize);
                index.Item2 = (int)((loc.y - col.bounds.min.z) / GridSize);

                return true;
            }
        }
        return false;
    }
    public GridItem[,] Grid;
}
