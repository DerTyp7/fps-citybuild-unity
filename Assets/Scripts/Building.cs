using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Header("Main-Info")]
    [SerializeField] string Title = "Building";
    [SerializeField] string Description = "This is a building!";
    [SerializeField] int Level = 1;
    [SerializeField] int Health = 100;


    [Header("Build-Ressources")]
    [SerializeField] int Wood = 0;
    [SerializeField] int Stone = 0;
    [SerializeField] int Clay = 0;
    [SerializeField] int Straw = 0;

    [Header("Building")]
    private Material oldMaterial;
    private Color originalColor;
    private bool isPlacingBuilding = false;
    public int isColliding;
    private new Renderer renderer;

    // Start is called before the first frame update
    void Start() {
        BuildingStart();

    }

    // Update is called once per frame
    void Update() {
        BuildingUpdate();

    }


    public void BuildingStart()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
    }
    public void BuildingUpdate()
    {
        if (!isPlacingBuilding)
        {
            renderer.material = oldMaterial;
            isColliding = 0;
        }
        if (isColliding > 0)
        {
            changeMaterialToTransparent(renderer.material);
            renderer.material.color = new Color(1, 0, 0, 0.3f);
        }
        else
        {
            if (isPlacingBuilding)
            {
                changeMaterialToTransparent(renderer.material);
                renderer.material.color = new Color(0, 0, 1, 0.3f);
            }
            else
            {
                GetComponent<Renderer>().material.color = originalColor;
            }
        }
    }
    public void setTitle(string newTitle) { // Sets Title for building 
        Title = newTitle;
    }
    public string getTitle() { // Returns Title of building
        return Title;
    }
    public void setDescription(string newDescription) { // Sets Description for building 
        Description = newDescription;
    }
    public string getDescription() { // Returns Description of building
        return Description;
    }
    public void setHealth(int newHealth) { // Sets Health for building 
        Health = newHealth;
    }
    public int getHealth() { // Returns Health of building
        return Health;
    }
    public void setLevel(int newLevel) {
        Level = newLevel;
    }
    public int getLevel() {
        return Level;
    }
    public bool delete() {
        // if delete successfully return true else return false
        return true;
    }

    public void isPlacing(bool isCurrentlyPlacing)
    {
        if (isCurrentlyPlacing)
        {
            isPlacingBuilding = true;
            return;
        }
        changeMaterialToOpaque(renderer.material); 
        renderer.material.color = new Color(1, 1, 1, 1);
        isPlacingBuilding = false;
    }
    void OnCollisionEnter(Collision col)
    {
        isColliding += 1;
    }
    void OnCollisionExit(Collision col)
    {
        isColliding -= 1;
    }

    void changeMaterialToOpaque(Material material)
    {
        material.SetOverrideTag("RenderType", "");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
    }
    void changeMaterialToTransparent(Material material)
    {
        material.SetOverrideTag("RenderType", "Transparent");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }
}
