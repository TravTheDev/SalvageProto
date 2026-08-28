using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrenadeThrower : MonoBehaviour
{
    [Header("Input")]
    [SerializeField]
    private InputActionReference grenadeAction;

    [Header("References")]
    [SerializeField]
    private Grenade grenadePrefab;

    [SerializeField]
    private Transform throwOrigin;

    [Header("Throw Settings")]
    [SerializeField]
    private float throwForce = 12f;

    [Header("Inventory")]
    [SerializeField]
    private int startingGrenades = 3;

    public int GrenadeCount { get; private set; }

    private void Awake()
    {
        GrenadeCount = startingGrenades;
    }

    private void OnEnable()
    {
        grenadeAction.action.Enable();
    }

    private void OnDisable()
    {
        grenadeAction.action.Disable();
    }

    private void Update()
    {
        if (grenadeAction.action.WasPressedThisFrame())
        {
            TryThrowGrenade();
        }
    }

    public bool TryThrowGrenade()
    {
        if (grenadePrefab == null ||
            throwOrigin == null ||
            GrenadeCount <= 0)
        {
            return false;
        }

        Grenade grenade = Instantiate(
            grenadePrefab,
            throwOrigin.position,
            throwOrigin.rotation
        );

        Rigidbody body =
            grenade.GetComponent<Rigidbody>();

        body.linearVelocity =
            throwOrigin.forward * throwForce;

        GrenadeCount--;

        Debug.Log(
            $"Grenade thrown. Remaining: {GrenadeCount}"
        );

        return true;
    }

    public void AddGrenades(int amount)
    {
        if (amount > 0)
        {
            GrenadeCount += amount;
        }
    }
}
