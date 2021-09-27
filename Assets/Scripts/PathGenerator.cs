using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{

    private Vector3 position;
    public Vector2 index;
    private float score;
    public PathNode(Vector3 Pos, float Score) {

        position = Pos;
        score = Score;
    }
    public Vector3 Position { get => position; set => position = value; }
}
