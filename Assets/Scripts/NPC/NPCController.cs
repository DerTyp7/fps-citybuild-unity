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
    bool first = false;
    bool second = false;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = new Vector3(13, 0, 23);
        endPoint = new Vector3(41, 0, 50);
        rows = 17;
        Map = new PathMap(new Vector3(0, 0, 0), rows, rows, 1000);
        Map.setupMapWithNextLayer();
        //Looking through the low res search
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !first)
        {
            first = true;
            Debug.Log(Time.time * 1000);
            path = Map.LowerQueryNodes(startPoint, endPoint);
            

        }
        else if(!second && first && true){
            second = true;
            Debug.Log(Time.time * 1000);
            if (path != null && false)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    float x = path[i].Position.x;
                    float y = path[i].Position.y;

                    //Debug.Log(path[i].index);
                    //Debug.Log(path[i].Position);

                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    sphere.transform.position = path[i].Position;
                }
            }
        }
    }
}
