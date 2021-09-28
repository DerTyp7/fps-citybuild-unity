using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{

    PathMap Map;
    private GameObject[,] ball;
    public List<PathNode> path;

    // Start is called before the first frame update
    void Start()
    {
        ball = new GameObject[30, 30];
        Map = new PathMap(new Vector3(0, 0, 0), 30, 30, 30);
        for (int r = 0; r < 30; r++)
        {
            for (int c = 0; c < 30; c++)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Cube);
                sphere.transform.position = Map.map[r, c].Position;
                ball[r, c] = sphere;


            }
        }
        path = Map.QueryNodes(new Vector3(0,0, 0), new Vector3(100, 0,100));
        //Debug.Log("yeet");
        Debug.Log(path.Count);
        Debug.Log((int)path[0].index.x + " "+ (int)path[0].index.y);
        for (int i = 0; i < path.Count - 1; i++) {
            int x = path[i].index.x;
            int y = path[i].index.y;
            Destroy(ball[x, y]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
