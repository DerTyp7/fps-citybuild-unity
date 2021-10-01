using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingBlueprint : MonoBehaviour
{
    public bool isColliding;
    public GameObject constructionPrefab;
    
    private GameObject terrain;
    private Canvas hud;

    Ray ray;

    public abstract void Init();

    public abstract void WhileColliding();
    public abstract void WhileNotColliding();

    private void Start()
    {
        hud = GameObject.Find("HUD").GetComponent<Canvas>(); //Get HUD Canvas
        terrain = GameObject.FindGameObjectWithTag("Terrain"); //Get Terrain

        //Bug Fix Blueprints already existing
        //Delete/CleanUp all objs with tag "Blueprint"
        GameObject[] blueprints = GameObject.FindGameObjectsWithTag("Blueprint");
        foreach (GameObject blueprint in blueprints)
            Destroy(blueprint);

  
        gameObject.tag = "Blueprint"; //Give Gameobject the tag "Blueprint" (after deleting all objs with this tag)

        hud.enabled = false; //Hide HUD

        Init(); //Call init callback function for children
    }

    public void Update()
    {
        FollowMouse();
        Rotate();

        //Collinding Callbacks
        if (isColliding)
        {            
            WhileColliding();
        }
        else
        {
            WhileNotColliding();
        }

        //PLACE
        if (Input.GetMouseButtonDown(0) && !isColliding)
        {
            Place();
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
    void Place()
    {
        Instantiate(constructionPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
        hud.enabled = true;
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
