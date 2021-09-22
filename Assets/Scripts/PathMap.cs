using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMap : MonoBehaviour
{
    private Vector3 position;
    private PathNode[,] map;
    private GameObject[,] ball;
    private int rows = 40;
    private int cols = 40;
    private float spacing = 1f;
    private float height = 0;

    private PathNode[] uncheckedNodes;
    private List<PathNode> openList;
    private List<PathNode> closedList;
    private List<PathNode> nextList;

    void Start()
    {
        map = new PathNode[40, 40];
        ball = new GameObject[rows, cols];
        openList = new List<PathNode>();
        closedList = new List<PathNode>();
        nextList = new List<PathNode>();

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                PathNode node = new PathNode(new Vector3(r * spacing, height, c * spacing), 1f);
                node.index = new Vector2(r,c);
                map[r, c] = node;

            }
        }  


        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = map[r,c].Position;
                ball[r,c] = sphere;

            }
        }
        FindClosestNode(new Vector3(0.7f,2,0.7f));
    }
    private void AddAllNeigbors(Vector2 index) {
        if ((int)index.x - 1 >= 0 && (int) index.y - 1 >= 0 && !openList.Contains(map[(int)index.x - 1, (int)index.y - 1])) {
            openList.Add(map[(int)index.x - 1, (int)index.y - 1]);
        }
        if ((int)index.y - 1 >= 0 && !openList.Contains(map[(int)index.x, (int)index.y - 1]))
        {
            openList.Add(map[(int)index.x, (int)index.y - 1]);
        }
        if ((int)index.x + 1 <= rows && (int)index.y - 1 >= 0 && !openList.Contains(map[(int)index.x + 1, (int)index.y - 1]))
        {
            openList.Add(map[(int)index.x + 1, (int)index.y - 1]);
        }

        if ((int)index.x - 1 >= 0 && !openList.Contains(map[(int)index.x - 1, (int)index.y]))
        {
            openList.Add(map[(int)index.x - 1, (int)index.y]);
        }
        if ((int)index.x + 1 <= rows && (int)index.y - 1 >= 0 && !openList.Contains(map[(int)index.x + 1, (int)index.y]))
        {
            openList.Add(map[(int)index.x + 1, (int)index.y]);
        }



        if ((int)index.x - 1 >= 0 && (int)index.y + 1 <= cols && !openList.Contains(map[(int)index.x - 1, (int)index.y + 1]))
        {
            openList.Add(map[(int)index.x - 1, (int)index.y + 1]);
        }
        if ((int)index.y + 1 <= cols && !openList.Contains(map[(int)index.x, (int)index.y + 1]))
        {
            openList.Add(map[(int)index.x, (int)index.y + 1]);
        }
        if ((int)index.x + 1 <= rows && (int)index.y + 1 <= cols && !openList.Contains(map[(int)index.x + 1, (int)index.y + 1]))
        {
            openList.Add(map[(int)index.x + 1, (int)index.y + 1]);
        }

    }
    private PathNode FindClosestNode(Vector3 pos) {
        if (pos.x > 0 && pos.x < rows * spacing && pos.z > 0 && pos.z < cols * spacing)
        {
            Destroy(ball[Mathf.RoundToInt(pos.x / spacing), Mathf.RoundToInt(pos.z / spacing)]);
            return map[Mathf.RoundToInt(pos.x / 2), Mathf.RoundToInt(pos.z / 2)];
        }
        else {
            return null;
        }
    }
    public void QueryNodes() {
        PathNode currentNode;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                currentNode = map[r,c];

            }
        }

    }

    // Update is called once per frame
    void Update()
    {


    }
}
