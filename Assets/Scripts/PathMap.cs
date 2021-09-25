using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMap : MonoBehaviour
{
    private Vector3 position;
    private PathNode[,] map;
    private GameObject[,] ball;
    private int rows = 200;
    private int cols = 200;
    private float spacing = 1f;
    private float height = 0;

    private PathNode[] uncheckedNodes;
    private List<PathNode> openList;
    private List<PathNode> closedList;
    private List<PathNode> nextList;

    private List<PathNode> path;

    void Start()
    {
        path = new List<PathNode>();
        map = new PathNode[1000, 1000];
        ball = new GameObject[rows, cols];
        openList = new List<PathNode>();
        closedList = new List<PathNode>();
        nextList = new List<PathNode>();

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                PathNode node = new PathNode(new Vector3(r * spacing, height, c * spacing));
                node.index = new Vector2(r, c);
                map[r, c] = node;

            }
        }

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                AddAllNeighbors(new Vector2(r, c));

            }
        }


        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = map[r, c].Position;
                ball[r, c] = sphere;

            }
        }

        QueryNodes(new Vector3(0.7f, 2, 0.7f), new Vector3(2.5f, 0, 2.5f));
    }
    private void AddAllNeighbors(Vector2 index)
    {
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
    private PathNode FindClosestNode(Vector3 pos)
    {
        if (pos.x > 0 && pos.x < rows * spacing && pos.z > 0 && pos.z < cols * spacing)
        {
            //Destroy(ball[Mathf.RoundToInt(pos.x / spacing), Mathf.RoundToInt(pos.z / spacing)]);
            return map[Mathf.RoundToInt(pos.x / 2), Mathf.RoundToInt(pos.z / 2)];
        }
        else
        {
            return null;
        }
    }
    public void QueryNodes(Vector3 Vstart, Vector3 Vend)
    {

        bool finished = false;

        PathNode start = FindClosestNode(Vstart);
        start.Gscore = 0;
        PathNode end = map[199, 150];
        Debug.Log(end.index);

        openList.Add(start);

        PathNode current;
        Debug.Log(Time.time);
        int d = 0;
        while (!finished)
        {
            d++;
            if (d > 10000)
            {
                Debug.Log("Mist");
                return;
            }
            int winner = 0;
            for (int i = 0; i < openList.Count; i++)
            {

                if (openList[i].Fscore < openList[winner].Fscore) winner = i;
            }
            current = openList[winner];
            openList.RemoveAt(winner);
            closedList.Add(current);
            Destroy(ball[(int)current.index.x, (int)current.index.y]);



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
                PathNode temp = end;
                path.Add(temp);
                while (temp.Previous != null)
                {
                    path.Add(temp.Previous);
                    temp = temp.Previous;
                }
                Debug.Log(path.Count);
                Debug.Log("yeet it finished");
                Debug.Log(Time.time);
                for  (int i = path.Count - 1; i >= 0;i-- )
                {
                    //Debug.Log(path[i].index);
                }
                finished = true;
            }
        }

    }

    private float heuristic(Vector3 pos1, Vector3 pos2)
    {
        return Vector3.Distance(pos1, pos2);

    }
    // Update is called once per frame
    void Update()
    {


    }
}

