using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMap
{


    private Vector3 position;
    public PathNode[,] map;

    private int rows = 30;
    private int cols = 30;
    private float spacing = 1f;
    private float height = 0;
    public Terrain t;
    private float w, h;

    private PathNode[] uncheckedNodes;
    private List<PathNode> openList;
    private List<PathNode> closedList;
    private List<PathNode> nextList;

    private List<PathNode> path;


    public PathMap(Vector3 Position, int Rows, int Cols, float Width)
    {
        position = Position;
        rows = Rows;
        cols = Cols;
        w = Width;

        //Array of all pathnodes in this chunk.
        map = new PathNode[rows, cols];


        //only for debugging
        


        openList = new List<PathNode>();
        closedList = new List<PathNode>();

        //Path that will be returned at the end.
        path = new List<PathNode>();

        //Add all nodes into the map.
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                PathNode node = new PathNode(new Vector3(position.x + r * spacing, position.y + height, position.z + c * spacing));
                node.index = new Vector2Int(r, c);
                map[r, c] = node;

            }
        }

        //Add the references to the neighbors of all nodes
        AddAllNeighbors();
    }

    private PathNode FindClosestNode(Vector3 pos)
    {
        if (pos.x > 0 && pos.x < rows * spacing && pos.z > 0 && pos.z < cols * spacing)
        {

            return map[Mathf.RoundToInt(pos.x / spacing), Mathf.RoundToInt(pos.z / spacing)];
        }
        Vector2Int best = new Vector2Int(0,0);
        
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (Vector3.Distance(map[best.x, best.y].Position, pos) > Vector3.Distance(map[r, c].Position, pos)) {
                    best.Set(r,c);
                }

            }
        }
        return map[best.x,best.y];
    }
    public List<PathNode> QueryNodes(Vector3 Vstart, Vector3 Vend)
    {
        bool finished = false;

        PathNode start = FindClosestNode(Vstart);
        start.Gscore = 0;
        PathNode end = FindClosestNode(Vend);

        Debug.Log("Searching a path from " + start.index + " to " + end.index);
        openList.Add(start);

        PathNode current;
        int d = 0;
        while (!finished)
        {
            d++;
            if (d > 1000)
            {
                Debug.Log("Mist! Has not found a path");
                return null;
            }
            int winner = 0;
            for (int i = 0; i < openList.Count; i++)
            {

                if (openList[i].Fscore < openList[winner].Fscore) winner = i;
            }
            current = openList[winner];
            openList.RemoveAt(winner);
            closedList.Add(current);



            if (current != end)
            {
                foreach (PathNode p in current.neigbors)
                {
                    if (!closedList.Contains(p))
                    {
                        float tempG = current.Gscore + heuristic(p.Position, current.Position);
                        bool newPath = false;
                        if (openList.Contains(p))
                        {
                            if (tempG < p.Gscore)
                            {
                                p.Gscore = tempG;
                                newPath = true;
                            }
                        }
                        else
                        {
                            p.Gscore = tempG;
                            newPath = true;
                            openList.Add(p);
                        }
                        if (newPath)
                        {
                            p.Hscore = heuristic(p.Position, end.Position);
                            p.Fscore = p.Gscore + p.Hscore;
                            p.Previous = current;
                        }

                    }

                }
            }
            else
            {
                Debug.Log("Path Has Been Found");
                PathNode temp = end;
                path.Add(temp);
                while (temp.Previous != null)
                {
                    path.Add(temp.Previous);
                    temp = temp.Previous;
                }
                path.Add(start);
                finished = true;
                return path;
            }
        }
        return null;
    }


    private float heuristic(Vector3 pos1, Vector3 pos2)
    {
        //Calculates the HScore for a node.
        return Vector3.Distance(pos1, pos2);

    }
    private void AddAllNeighbors()
    {
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                AddNeighbors(new Vector2(r, c));
            }
        }

    }
    private void AddNeighbors(Vector2 index)
    {
        //Adds references to all neigbors of a node.
        if ((int)index.x - 1 >= 0 && (int)index.y - 1 >= 0)
        {
            map[(int)index.x, (int)index.y].neigbors.Add(map[(int)index.x - 1, (int)index.y - 1]);
        }
        if ((int)index.y - 1 >= 0)
        {
            map[(int)index.x, (int)index.y].neigbors.Add(map[(int)index.x, (int)index.y - 1]);
        }
        if ((int)index.x + 1 < rows && (int)index.y - 1 >= 0)
        {
            map[(int)index.x, (int)index.y].neigbors.Add(map[(int)index.x + 1, (int)index.y - 1]);
        }

        if ((int)index.x - 1 >= 0)
        {
            map[(int)index.x, (int)index.y].neigbors.Add(map[(int)index.x - 1, (int)index.y]);
        }
        if ((int)index.x + 1 < rows && (int)index.y >= 0)
        {
            map[(int)index.x, (int)index.y].neigbors.Add(map[(int)index.x + 1, (int)index.y]);
        }



        if ((int)index.x - 1 >= 0 && (int)index.y + 1 < cols)
        {
            map[(int)index.x, (int)index.y].neigbors.Add(map[(int)index.x - 1, (int)index.y + 1]);
        }
        if ((int)index.y + 1 < cols)
        {
            map[(int)index.x, (int)index.y].neigbors.Add(map[(int)index.x, (int)index.y + 1]);
        }
        if ((int)index.x + 1 < rows && (int)index.y + 1 < cols)
        {
            map[(int)index.x, (int)index.y].neigbors.Add(map[(int)index.x + 1, (int)index.y + 1]);
        }


    }
}