using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference interactAction;
    [SerializeField] private InputActionReference throwAction;

    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform carryPoint;

    [Header("Interaction Settings")]
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private float throwForce = 10f;

    private ICarryable carriedObject;

    public bool IsCarrying => carriedObject != null;

    private void OnEnable()
    {
        interactAction.action.Enable();
        throwAction.action.Enable();
    }

    private void OnDisable()
    {
        interactAction.action.Disable();
        throwAction.action.Disable();
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        if (interactAction.action.WasPressedThisFrame())
        {
            if (carriedObject != null)
            {
                DropCarriedObject(false);
            }
            else
            {
                TryInteract();
            }
        }

        if (throwAction.action.WasPressedThisFrame() &&
            carriedObject != null)
        {
            DropCarriedObject(true);
        }
    }

    private void TryInteract()
    {
        if (playerCamera == null)
        {
            return;
        }

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (!Physics.Raycast(
                ray,
                out RaycastHit hit,
                interactionRange,
                ~0,
                QueryTriggerInteraction.Ignore))
        {
            return;
        }

        IInteractable interactable =
            FindInteractable(hit.transform);

        interactable?.Interact(this);
    }

    private IInteractable FindInteractable(Transform start)
    {
        Transform current = start;

        while (current != null)
        {
            MonoBehaviour[] behaviours =
                current.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour behaviour in behaviours)
            {
                if (behaviour is IInteractable interactable)
                {
                    return interactable;
                }
            }

            current = current.parent;
        }

        return null;
    }

    public void TryCarry(ICarryable carryable)
    {
        if (carryable == null ||
            carriedObject != null)
        {
            return;
        }

        carriedObject = carryable;

        carriedObject.BeginCarry(carryPoint);
    }

    private void DropCarriedObject(bool throwObject)
    {
        if (carriedObject == null)
        {
            return;
        }

        Vector3 velocity = throwObject
            ? playerCamera.transform.forward * throwForce
            : Vector3.zero;

        carriedObject.Drop(velocity);

        carriedObject = null;
    }
}
