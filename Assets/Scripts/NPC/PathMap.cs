using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMap
{


    private Vector3 position;
    public PathNode[,] map;
    public PathMap parent;
    public Vector2Int parentIndex;

    private int rows = 100;
    private int cols = 100;
    private float spacing = 1f;
    private float height = 0;
    public Terrain t;
    private float w, h;

    private List<PathNode> openList;
    private List<PathNode> closedList;

    private List<PathNode> path;







    public PathMap(Vector3 Position, int Rows, int Cols, float Width)
    {
        setup(Position, Rows, Cols, Width);


    }
    public void setupMap()
    {
        Generate();
        //Add the references to the neighbors of all nodes
        AddAllNeighbors();


    }
    private void setup(Vector3 Position, int Rows, int Cols, float Width)
    {
        position = Position;
        rows = Rows;
        cols = Cols;
        w = Width;
        spacing = w / rows;

        //Array of all pathnodes in this chunk.
        map = new PathNode[rows, cols];


        //only for debugging



        openList = new List<PathNode>();
        closedList = new List<PathNode>();

        //Path that will be returned at the end.
        path = new List<PathNode>();

    }
    public void setupMapWithNextLayer()
    {
        Generate();
        AddAllNeighbors();
        addNextLayer();

        //Add the references to the neighbors of all nodes


    }
    public void Generate()
    {
        //Add all nodes into the map.
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                PathNode node = new PathNode();
                node.Position = new Vector3(this.position.x + r * spacing, this.position.y + height, this.position.z + c * spacing);
                node.index = new Vector2Int(r, c);
                node.ConditionWeight = 1 + (float)Random.Range(1, 1000) / 1000;
                map[r, c] = node;

            }
        }


    }

    public List<PathNode> QueryNodes(Vector3 Vstart, Vector3 Vend)
    {
        openList = new List<PathNode>();
        closedList = new List<PathNode>();
        path = new List<PathNode>();
        bool finished = false;

        PathNode start = FindClosestNode(Vstart);
        start.Gscore = 0;
        PathNode end = FindClosestNode(Vend);
        Debug.Log("Searching a path from " + start.Position + " to " + end.Position);
        openList.Add(start);

        PathNode current;
        int d = 0;
        while (!finished)
        {
            d++;
            if (d > 2025)
            {
                Debug.Log("Too many trys");
                return null;
            }
            int winner = 0;
            for (int i = 0; i < openList.Count; i++)
            {

                if (openList[i].Fscore < openList[winner].Fscore) winner = i;
            }
            if (openList.Count != 0)
            {
                current = openList[winner];
            }
            else
            {
                Debug.Log("Fuck");
                return null;
            }
            openList.RemoveAt(winner);
            closedList.Add(current);



            if (current != end)
            {
                foreach (PathNode p in current.neigbors)
                {
                    if (!closedList.Contains(p) && !p.Blocked)
                    {
                        float tempG = current.Gscore + heuristic(p, current);
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
                            p.Hscore = heuristic(p, end);
                            p.Fscore = p.Gscore + p.Hscore;
                            p.Previous = current;
                        }

                    }

                }
            }
            else
            {
                PathNode temp = end;
                path.Add(temp);
                while (temp.Previous != null)
                {
                    path.Add(temp.Previous);
                    temp = temp.Previous;
                }
                path.Add(start);
                finished = true;
                Debug.Log("Path Has Been Found with " + path.Count + " nodes.");
                return path;
            }
        }
        return null;
    }
    private bool isInParent(List<PathNode> path, Vector2Int index)
    {
        for (int i = 0; i < path.Count; i++)
        {

            if (path[i].index == index) { return true; }
        }
        return false;
    }

    public List<PathNode> LowerQueryNodes(Vector3 Vstart, Vector3 Vend)
    {
        List<PathNode> excludeNodes = new List<PathNode>();


        List<PathNode> parentPath = QueryNodes(Vstart, Vend);
        if (parentPath == null)
        {
            Debug.Log("The Parent didnt find the end!");
            return null;
        }

        //Reseting all the lists.
        openList = new List<PathNode>();
        closedList = new List<PathNode>();
        path = new List<PathNode>();

        //Reseting some variables
        bool finished = false;
        PathNode current;

        //Setup for the start node
        PathNode start = parentPath[parentPath.Count - 1].lowerLevel.FindClosestNode(Vstart);
        start.Gscore = 0;
        openList.Add(start);
        //Setup for the end node
        PathNode end = parentPath[0].lowerLevel.FindClosestNode(Vend);


        Debug.Log("Searching a path from " + start.Position + " to " + end.Position + " but through the lower level.");



        int d = 0;
        while (!finished)
        {
            d++;
            if (d > 40*40*40*40)
            {
                Debug.Log("Too many trys");
                return null;
            }
            int winner = 0;
            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].Fscore < openList[winner].Fscore) winner = i;
            }
            if (openList.Count != 0)
            {
                current = openList[winner];
            }
            else {

                Debug.Log("openlist is empty!");
                return null;
            }
            openList.RemoveAt(winner);
            closedList.Add(current);

            if (current != end)
            {
                foreach (PathNode p in current.neigbors)
                {
                    if (!closedList.Contains(p) && !p.Blocked && isInParent(parentPath, p.parentIndex))
                    {

                        float tempG = current.Gscore + heuristic(p, current);
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
                            p.Hscore = heuristic(p, end);
                            p.Fscore = p.Gscore + p.Hscore;
                            p.Previous = current;
                        }

                    }

                }
            }
            else
            {

                PathNode temp = end;
                path.Add(temp);
                while (temp.Previous != null)
                {
                    path.Add(temp.Previous);
                    temp = temp.Previous;
                }
                path.Add(start);
                Debug.Log("Path Has Been Found with " + path.Count + " nodes.");
                finished = true;
                return path;
            }
            if (openList.Count == 0)
            {
                Debug.Log(d);
                excludeNodes.Add(current);
                Debug.Log("Could not find a path!");
                return null;
            }

        }
        return null;
    }




    public void addNextLayer()
    {


        PathNode[,] full = new PathNode[rows * rows, cols * cols];
        for (int i = 0; i < rows * rows; i++)
        {
            for (int j = 0; j < cols * cols; j++)
            {

                PathNode node = new PathNode();
                node.Position = new Vector3(this.position.x + i * spacing / rows, this.position.y, this.position.z + j * spacing / cols);
                full[i, j] = node;

            }

        }
        for (int i = 0; i < rows * rows; i++)
        {
            for (int j = 0; j < cols * cols; j++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (!(x == 0 && y == 0))
                        {
                            if (!indexOutOfBounds(i + x, j + y, rows * rows, cols * cols))
                            {
                                full[i, j].neigbors.Add(full[i + x, j + y]);

                            }
                        }
                    }
                }

            }

        }


        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                PathNode node = map[r, c];
                Vector3 pos = node.Position;
                node.lowerLevel = new PathMap(pos, rows, cols, spacing);
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        PathNode n = full[r * rows + i, c * cols + j];
                        n.index.Set(i, j);
                        n.parentIndex.Set(r, c);
                        n.ConditionWeight = 1 + (float)Random.Range(1, 1000) / 1000;
                        node.lowerLevel.map[i, j] = n;

                    }
                }
                node.lowerLevel.parent = this;

                node.HasLowerLevel = true;

            }
        }
    }
    private float heuristic(PathNode p1, PathNode p2)
    {
        //Calculates the HScore for a node.
        float dist = Vector3.Distance(p1.Position, p2.Position) / 2;
        float dist1 = dist * p1.ConditionWeight;
        float dist2 = dist * p2.ConditionWeight;

        return dist1 + dist2;

    }
    private PathNode FindClosestNode(Vector3 pos)
    {
        if (pos.x > 0 && pos.x < rows * spacing && pos.z > 0 && pos.z < cols * spacing && false)
        {

            return map[Mathf.RoundToInt(pos.x / spacing), Mathf.RoundToInt(pos.z / spacing)];
        }
        Vector2Int best = new Vector2Int(0, 0);

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (Vector3.Distance(map[best.x, best.y].Position, pos) > Vector3.Distance(map[r, c].Position, pos))
                {
                    best.Set(r, c);
                }
            }
        }
        return map[best.x, best.y];
    }
    private void AddAllNeighbors()
    {
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                AddNeighbors(new Vector2Int(r, c));
            }
        }

    }

    private void lookThroughLowerLevel()
    {


    }
    private bool indexOutOfBounds(int x, int y, int rows, int cols)
    {
        if (x < 0 || x >= rows || y < 0 || y >= cols)
        {
            return true;
        }
        return false;
    }
    private void AddNeighbors(Vector2Int index)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (!(x == 0 && y == 0))
                {
                    if (x == 0 || y == 0)
                    {
                        if (!indexOutOfBounds(index.x + x, index.y + y, rows, cols))
                        {
                            map[index.x, index.y].neigbors.Add(map[index.x + x, index.y + y]);

                        }
                    }
                }
            }
        }
    }

}