using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using System;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct Score : IComponent
{
    public string Key;
    public int Value;

    public void Increase(int value)
    {
        if (Value < 0)
        {
            throw new ArgumentException($"{nameof(value)} must be positive");
        }

        Value += value;
        PlayerPrefsSaver<Score>.Save(Key, this);
    }
}