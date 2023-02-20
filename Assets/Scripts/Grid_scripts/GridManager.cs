using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{

    public Tilemap grid;

    Graph graph;

    private void Awake()
    {
        InitializeGraph();
    }

    private void InitializeGraph()
    {
        graph = new Graph();
        Debug.Log("se crea el grafo");
        for(int x = grid.cellBounds.xMin; x < grid.cellBounds.xMax; x++)
        {
            for(int y = grid.cellBounds.yMin; y < grid.cellBounds.yMax; y++)
            {
                Vector3Int localPosition = new Vector3Int(x, y, 0);
                if (grid.HasTile(localPosition))
                {
                    Vector3 worldPosition = grid.CellToWorld(localPosition);
                    graph.AddNode(worldPosition);
                }
            }
        }

        var allNodes = graph.Nodes;

        foreach(Node from in allNodes)
        {
            Debug.Log(from.worldPosition);
            foreach(Node to in allNodes)
            {
                if(Vector3.Distance(from.worldPosition, to.worldPosition) <= 1f && from != to)
                {
                    graph.AddEdge(from, to);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (graph == null)
            return;

        var allEdges = graph.Edges;
        if (allEdges == null)
            return;

        foreach (Edge e in allEdges)
        {
            Debug.DrawLine(e.from.worldPosition, e.to.worldPosition, Color.black, 100);
        }

        var allNodes = graph.Nodes;
        if (allNodes == null)
            return;

        foreach (Node n in allNodes)
        {
            Gizmos.color = n.IsOccupied ? Color.red : Color.green;
            Gizmos.DrawSphere(n.worldPosition, 0.1f);

        }

        //if (fromIndex >= allNodes.Count || toIndex >= allNodes.Count)
        //    return;

        //List<Node> path = graph.GetShortestPath(allNodes[fromIndex], allNodes[toIndex]);
        //if (path.Count > 1)
        //{
        //    for (int i = 1; i < path.Count; i++)
        //    {
        //        Debug.DrawLine(path[i - 1].worldPosition, path[i].worldPosition, Color.red, 10);
        //    }
        //}
    }
}
