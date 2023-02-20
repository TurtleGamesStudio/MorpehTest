using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct MovementParametrs : IComponent
{
    public float MaxSpeed;
    public Vector3 Direction;

    //public MovementParametrs(float maxSpeed, Vector3 direction)
    //{
    //    MaxSpeed = 1000f;
    //    Direction = Vector3.zero;
    //}
}