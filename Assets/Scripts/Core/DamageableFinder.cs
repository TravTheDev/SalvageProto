using UnityEngine;

public static class DamageableFinder
{
    public static IDamageable Find(Transform start)
    {
        Transform current = start;

        while (current != null)
        {
            MonoBehaviour[] behaviours =
                current.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour behaviour in behaviours)
            {
                if (behaviour is IDamageable damageable)
                {
                    return damageable;
                }
            }

            current = current.parent;
        } 

        return null;
    }
}