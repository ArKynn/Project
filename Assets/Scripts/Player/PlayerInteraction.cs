using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionRange;
    [SerializeField] private LayerMask interactionLayerMask;
    
    public bool CanInteract => CheckCanInteract();
    private bool _interactLock = false;
    
    private PlayerBodyCarry _playerBodyCarry;

    private void Start()
    {
        _playerBodyCarry = GetComponent<PlayerBodyCarry>();
    }

    private bool CheckCanInteract()
    {
        return !_interactLock && _playerBodyCarry.CarryingBody;
    }

    public void Interact()
    {
        if (!CanInteract) return;

        var interactive = Detector.GetClosestInArea<InteractiveObject>(transform, interactionRange, interactionLayerMask);
        if(interactive.Interact()) InteractionSuccessful(interactive);
    }

    private void InteractionSuccessful(InteractiveObject interactive)
    {
        if(interactive is Body body) _playerBodyCarry.CarryBody(body);
    }
}