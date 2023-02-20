using UnityEngine;

public class DamagableInitializer : MonoBehaviour
{
    private void Awake()
    {
        DamagableProvider damageable = GetComponent<DamagableProvider>();
        HealthProvider healthProvider = GetComponent<HealthProvider>();
        healthProvider.GetData().Value = 1f;
        damageable.GetData().Transform = transform;
    }
}
