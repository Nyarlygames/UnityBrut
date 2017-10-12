﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFind
{
    // The API you should use to get path
    // grid: grid to search in.
    // startPos: starting position.
    // targetPos: ending position.
    public static List<Vector3> FindPath(Grid grid, Vector3 startPos, Vector3 targetPos)
    {
        // find path
        List<Node> nodes_path = _ImpFindPath(grid, startPos, targetPos);
        // convert to a list of points and return
        List<Vector3> ret = new List<Vector3>();
        if (nodes_path != null)
        {
            foreach (Node node in nodes_path)
            {
                ret.Add(new Vector3(node.gridX, node.gridY));
            }
        }
        return ret;
    }

    // internal function to find path, don't use this one from outside
    private static List<Node> _ImpFindPath(Grid grid, Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.nodes[(int) startPos.x, (int)startPos.y];
        Node targetNode = grid.nodes[(int)targetPos.x, (int)targetPos.y];

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(grid, startNode, targetNode);
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour) * (int)(10.0f * neighbour.penalty);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        return null;
    }

    private static List<Node> RetracePath(Grid grid, Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    private static int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
