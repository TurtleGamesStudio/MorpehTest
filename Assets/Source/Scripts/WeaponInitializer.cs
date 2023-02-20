using UnityEngine;

public class WeaponInitializer : MonoBehaviour
{
    [SerializeField] private WeaponProvider _weapon;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _damage;
    [SerializeField] private float _intervalBetweenShoots;

    private void Start()
    {
        ref Weapon weapon = ref _weapon.GetData();
        weapon.ShootPoint = _shootPoint;
        weapon.Damage = _damage;
        weapon.IntervalBetweenShoots = _intervalBetweenShoots;
    }
}
