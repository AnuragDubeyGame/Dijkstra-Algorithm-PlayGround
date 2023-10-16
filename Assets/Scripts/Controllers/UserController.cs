using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    private Camera cam;
    private Vector2 m_Position = Vector2.zero;
    private Graph graph;
    public GameObject VerticesPrefab, EdgePrefab;
    private uint verticesIndex;

    public Vertices selectedVertices;
    public Edges TempEdge;
    public LineRenderer EdgeRenderer;

    void Start()
    {
        cam = Camera.main;
        graph = FindAnyObjectByType<Graph>();
    }

    void Update()
    {
        m_Position = Input.mousePosition;
        m_Position = cam.ScreenToWorldPoint(m_Position);


        if(Input.GetMouseButtonDown(1) && selectedVertices == null)
        {
            handlePlacement();
        }
        if(selectedVertices != null && TempEdge != null && Input.GetMouseButtonDown(1))
        {
            Destroy(TempEdge.gameObject);
            TempEdge = null;
            selectedVertices = null;
        }
        if(EdgeRenderer != null)
        {
            EdgeRenderer.SetPosition(1,m_Position);
        }
    }
    private void handlePlacement()
    {
        GameObject spawnedObject = Instantiate(VerticesPrefab, m_Position, Quaternion.identity);
        spawnedObject.GetComponent<Vertices>().setIndex(verticesIndex);
        spawnedObject.transform.name = $"Vertice {verticesIndex}";
        graph.verticesList.Add(spawnedObject.GetComponent<Vertices>());
        if (verticesIndex == 0)
        {
           graph.Source_vertices = spawnedObject.GetComponent<Vertices>();
        }
        else
        {
           graph.Destination_vertices = spawnedObject.GetComponent<Vertices>();
        }

        verticesIndex++;
    }

    public void SpawnEdge(Vector2 startPos)
    {
        GameObject tempEdge = Instantiate(EdgePrefab);
        TempEdge = tempEdge.GetComponent<Edges>();
        EdgeRenderer = TempEdge.GetComponent<LineRenderer>();
        TempEdge.GetComponent<LineRenderer>().SetPosition(0, startPos);
    }
}

