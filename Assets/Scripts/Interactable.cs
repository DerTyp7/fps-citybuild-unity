using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public enum InteractionType //Interaction Types (Enum heiﬂt enumeration also Aufz‰hlung)
    {
        Click,
        Hold
    }

    public InteractionType interactionType;

    public abstract string GetDescription();
    public abstract void Interact();

}
