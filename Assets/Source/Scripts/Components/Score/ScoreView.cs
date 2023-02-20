using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using TMPro;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct ScoreView : IComponent
{
    public TMP_Text Text;

    public void Update(int value)
    {
        Text.text = "Score: " + value.ToString();
    }
}