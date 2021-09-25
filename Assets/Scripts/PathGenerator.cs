using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{

    private Vector3 position;
    public Vector2 index;
    private float scoreF;
    private float scoreG;
    private float scoreH;
    public List<PathNode>  neigbors;

    private PathNode previous;
    
    public PathNode(Vector3 Pos) {
        neigbors = new List<PathNode>();
        position = Pos;
        scoreG = Mathf.Infinity;
        scoreF = Mathf.Infinity;
        scoreH = Mathf.Infinity;
    }
    
    public Vector3 Position { get => position; set => position = value; }
    public float Hscore { get => scoreH; set => scoreH = value; }
    public float Gscore { get => scoreG; set => scoreG = value; }
    public float Fscore { get => scoreF; set => scoreF = value; }
    public PathNode Previous { get => previous; set => previous = value; }
}
