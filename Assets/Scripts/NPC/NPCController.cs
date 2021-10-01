using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{

    PathMap Map;
    private GameObject[,] ball;
    public List<PathNode> path;
    private int rows;
    [SerializeField] Vector3 startPoint;
    [SerializeField] Vector3 endPoint;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = new Vector3(0,0,0);
        endPoint = new Vector3(100,0,100);
        rows = 45;
        Map = new PathMap(new Vector3(0, 0, 0), rows, rows, 100);
        Debug.Log("yeet");
        Map.setupMapWithNextLayer();
        //Looking through the low res search
        //path = Map.QueryNodes(startPoint,endPoint);
        if (path != null) {
            for (int i = 0; i < path.Count - 1; i++) {
                int x = path[i].index.x;
                int y = path[i].index.y;
                Debug.Log(path[i].index);
                Debug.Log(path[i].Position);
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Cube);
                sphere.transform.position = Map.map[x, y].Position;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
