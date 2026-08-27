using UnityEngine;

public interface ICarryable : IInteractable
{
    bool IsCarried { get; }

    void BeginCarry(Transform carryPoint);
    void Drop(Vector3 velocity);
}
