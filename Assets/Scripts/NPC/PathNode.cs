using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{

    private Vector3 position;
    public Vector2Int index;
    public Vector2Int parentIndex;
    private float scoreF;
    private float scoreG;
    private float scoreH;
    public List<PathNode> neigbors;
    public PathMap lowerLevel;
    private bool hasLowerLevel = false;
    private float conditionWeight = 1f;
    private PathNode previous;
    private bool blocked;


    public PathNode()
    {
        neigbors = new List<PathNode>();
        position = Vector3.zero;
        scoreG = Mathf.Infinity;
        scoreF = Mathf.Infinity;
        scoreH = Mathf.Infinity;
    }

    public void activateNextLevel()
    {

    }

    public Vector3 Position { get => position; set => position = value; }
    public float Hscore { get => scoreH; set => scoreH = value; }
    public float Gscore { get => scoreG; set => scoreG = value; }
    public float Fscore { get => scoreF; set => scoreF = value; }
    public PathNode Previous { get => previous; set => previous = value; }
    public float ConditionWeight { get => conditionWeight; set => conditionWeight = value; }
    public bool Blocked { get => blocked; set => blocked = value; }
    public bool HasLowerLevel { get => hasLowerLevel; set => hasLowerLevel = value; }
}
