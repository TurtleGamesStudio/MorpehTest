using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.UI;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct GameMenu : IComponent
{
    public bool IsOpen;
    public Image Image;
}