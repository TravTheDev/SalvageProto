using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarryableObject : MonoBehaviour, ICarryable
{
    private Rigidbody body;
    private Collider[] colliders;

    public bool IsCarried { get; private set; }

    public string InteractionPrompt =>
        IsCarried ? "Drop" : "Pick Up";

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
    }

    public void Interact(PlayerInteraction interactor)
    {
        if (!IsCarried)
        {
            interactor.TryCarry(this);
        }
    }

    public void BeginCarry(Transform carryPoint)
    {
        if (carryPoint == null)
        {
            return;
        }

        IsCarried = true;

        body.isKinematic = true;
        body.useGravity = false;

        body.linearVelocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;

        foreach (Collider objectCollider in colliders)
        {
            objectCollider.enabled = false;
        }

        transform.SetParent(carryPoint);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Drop(Vector3 velocity)
    {
        IsCarried = false;

        transform.SetParent(null);

        foreach (Collider objectCollider in colliders)
        {
            objectCollider.enabled = true;
        }

        body.isKinematic = false;
        body.useGravity = true;

        body.linearVelocity = velocity;
    }
}