using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionRange;
    [SerializeField] private LayerMask interactionLayerMask;
    
    public bool CanInteract => CheckCanInteract();
    private bool _interactLock = false;
    
    private PlayerBodyGrab _playerBodyGrab;

    private void Start()
    {
        _playerBodyGrab = GetComponent<PlayerBodyGrab>();
    }

    private bool CheckCanInteract()
    {
        return !_interactLock && !_playerBodyGrab.CarryingBody;
    }

    public void Interact()
    {
        if (!CanInteract) return;

        var interactive = Detector.GetClosestInArea<InteractiveObject>(transform, interactionRange, interactionLayerMask);
        if(interactive != null && interactive.Interact()) InteractionSuccessful(interactive);
    }

    private void InteractionSuccessful(InteractiveObject interactive)
    {
        if(interactive is Body body) _playerBodyGrab.GrabBody(body);
    }
}