using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlueprint : MonoBehaviour
{
    
    public Material collisionMat;
    public Material blueprintMat;


    private GameObject terrain;
    private Canvas hud;
    private bool isColliding;

    Ray ray;

    private void Start()
    {
        hud = GameObject.Find("HUD").GetComponent<Canvas>(); //Get HUD Canvas
        terrain = GameObject.FindGameObjectWithTag("Terrain"); //Get Terrain

        hud.enabled = false; //Hide HUD
    }

    public void Update()
    {
        FollowMouse();
        Rotate();

        //COLLISION
        if (isColliding)
        {
            MeshRenderer[] mr = gameObject.GetComponent<Building>().FindChildByTag("Blueprint").GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer r in mr)
            {
                r.material = collisionMat;
            }
            
        }
        else
        {
            MeshRenderer[] mr = gameObject.GetComponent<Building>().FindChildByTag("Blueprint").GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer r in mr)
            {
                r.material = blueprintMat;
            }
        }


        //PLACE
        if (Input.GetMouseButtonDown(0) && !isColliding)
        {
            gameObject.GetComponent<Building>().EndBlueprint(true);
            hud.enabled = true;
        }

        if (Input.GetButtonDown("Build"))
        {
            gameObject.GetComponent<Building>().EndBlueprint(false);
            hud.enabled = true;
        }
    }

    void FollowMouse()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        if (terrain.GetComponent<Collider>().Raycast(ray, out hitData, Mathf.Infinity))
        {
            transform.position = hitData.point;
        }
    }
    void Rotate()
    {
        if (Input.GetButtonDown("Rotate"))
        {
            Debug.Log("Rotate+");
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Rotate(0, 5, 0);
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                transform.Rotate(0, 45, 0);
            }
            else
            {
                transform.Rotate(0, 22.5f, 0);
            }

        }

        if (Input.GetButtonDown("CounterRotate"))
        {
            Debug.Log("Rotate-");
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Rotate(0, -5, 0);
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                transform.Rotate(0, -45, 0);
            }
            else
            {
                transform.Rotate(0, -22.5f, 0);
            }

        }
    }

    //Collision
    public void OnCollisionEnter(Collision c)
    {
        isColliding = true;
        Debug.Log("Colliding True");
    }
    public void OnCollisionStay(Collision c)
    {
        isColliding = true;
        Debug.Log("Colliding True");
    }
    public void OnCollisionExit(Collision c)
    {
        isColliding = false;
        Debug.Log("Colliding False");
    }
}
