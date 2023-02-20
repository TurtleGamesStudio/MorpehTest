using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Newtonsoft.Json.Linq;
using System;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct Health : IComponent
{
    public float Value;

    public void Decrease(float damage)
    {
        if (damage < 0)
        {
            throw new ArgumentException($"{nameof(damage)} must be non negative.");
        }

        if (Value == 0)
        {
            throw new ArgumentException($"Health equal 0. You cannot decrease health more.");
        }

        if (damage >= Value)
        {
            Value = 0;
            //Die();
        }
        else
        {
            Value -= damage;
            //Changed?.Invoke(Value);
        }
    }
}