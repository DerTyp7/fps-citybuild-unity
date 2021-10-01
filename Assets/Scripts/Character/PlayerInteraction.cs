using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    public float interactionDistance;

    public TMPro.TextMeshProUGUI interactionText;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        bool successfulHit = false;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                HandleInteraction(interactable);
                interactionText.text = interactable.GetDescription();
                successfulHit = true;
            }
        }

        // if we miss, hide the UI
        if (!successfulHit)
        {
            interactionText.text = "";
        }
    }

    void HandleInteraction(Interactable interactable)
    {
        switch (interactable.interactionType)
        {
            case Interactable.InteractionType.Click:
                // interaction type is click and we clicked the button -> interact
                if (Input.GetButtonDown("Interact"))
                {
                    Debug.Log("INTERACT");
                    interactable.Interact();
                }
                break;
           
            default:
                throw new System.Exception("Unsupported type of interactable.");
        }
    }
}