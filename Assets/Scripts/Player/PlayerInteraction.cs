using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionRange;
    [SerializeField] private LayerMask interactionLayerMask;
    
    public bool canInteract = true;

    public void Interact()
    {
        if (!canInteract) return;
        
        Detector.GetClosestInArea<InteractiveObject>(transform, interactionRange, interactionLayerMask).Interact();
    }
    
}
