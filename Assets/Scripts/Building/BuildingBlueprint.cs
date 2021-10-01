using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingBlueprint : MonoBehaviour
{
    public bool isColliding;
    public bool followMouse = true;
    public GameObject constructionPrefab;
    
    private GameObject terrain;


    Ray ray;

    public abstract void Init();

    public abstract void WhileColliding();
    public abstract void WhileNotColliding();

    private void Start()
    {
        GameObject[] blueprints = GameObject.FindGameObjectsWithTag("Blueprint");
        foreach (GameObject blueprint in blueprints)
        GameObject.Destroy(blueprint);

        gameObject.tag = "Blueprint";

        terrain = GameObject.FindGameObjectWithTag("Terrain");
        Init();
    }


    //Collision
    public void OnCollisionEnter(Collision collision)
    {
        isColliding = true;
        
        Debug.Log("Colliding True");
    }
    public void OnCollisionStay(Collision collision)
    {
        isColliding = true;
        Debug.Log("Colliding True");
    }
    public void OnCollisionExit(Collision collision)
    {
        isColliding = false;
        Debug.Log("Colliding False");
    }

    //Placing
    public void Update()
    {
        if (followMouse)
        {
            //Following Mouse
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;
            if (terrain.GetComponent<Collider>().Raycast(ray, out hitData, Mathf.Infinity))
            {
                transform.position = hitData.point;
            }
        }

        if (Input.GetMouseButtonDown(0) && !isColliding)
        {
            Instantiate(constructionPrefab, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }

        //Collinding Callbacks
        if (isColliding)
        {
            WhileColliding();
        }
        else
        {
            WhileNotColliding();
        }


        //Rotate
        if (Input.GetButtonDown("Rotate"))
        {
            Debug.Log("Rotate+");
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Rotate(0, 5, 0);
            }else if (Input.GetKey(KeyCode.LeftControl))
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
            }else if (Input.GetKey(KeyCode.LeftControl))
            {
                transform.Rotate(0, -45, 0);
            }
            else
            {
                transform.Rotate(0, -22.5f, 0);
            }

        }


    }

}
