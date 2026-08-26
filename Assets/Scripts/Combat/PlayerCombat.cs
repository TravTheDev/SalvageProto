using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference fireAction;
    [SerializeField] private InputActionReference reloadAction;

    [Header("Weapon")]
    [SerializeField] private HitscanWeapon equippedWeapon;

    private void OnEnable()
    {
        fireAction.action.Enable();
        reloadAction.action.Enable();
    }

    private void OnDisable()
    {
        fireAction.action.Disable();
        reloadAction.action.Disable();
    }

    private void Update()
    {
        if (equippedWeapon == null)
        {
            return;
        }

        if (fireAction.action.IsPressed())
        {
            equippedWeapon.TryFire(gameObject);
        }

        if (reloadAction.action.WasPressedThisFrame())
        {
            equippedWeapon.BeginReload();
        }
    }
}
