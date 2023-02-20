using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(WeaponHammerSystem))]
public sealed class WeaponHammerSystem : UpdateSystem
{
    private Filter _weaponsFilter;

    public override void OnAwake()
    {
        _weaponsFilter = World.Filter.With<Weapon>();
    }

    public override void OnUpdate(float deltaTime)
    {
        Test();
    }

    private void Test()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Press();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Release();
        }
    }

    private void Press()
    {
        foreach (Entity entity in _weaponsFilter)
        {
            entity.AddComponent<HammerPressed>();
        }
    }

    private void Release()
    {
        foreach (Entity entity in _weaponsFilter)
        {
            entity.RemoveComponent<HammerPressed>();
        }
    }
}