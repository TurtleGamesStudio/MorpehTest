using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MenuSystem))]
public sealed class MenuSystem : UpdateSystem
{
    private Filter _filter;
    private Stash<GameMenu> _stash;
    private Entity _entity;

    public override void OnAwake()
    {
        _filter = World.Filter.With<GameMenu>();
        _stash = World.GetStash<GameMenu>();
        _entity = _filter.First();

        ref GameMenu gameMenu = ref _stash.Get(_entity);
        Close(ref gameMenu);
    }

    public override void OnUpdate(float deltaTime)
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ref GameMenu gameMenu = ref _stash.Get(_entity);
            OnEscapePressed(ref gameMenu);
        }
    }

    private void OnEscapePressed(ref GameMenu gameMenu)
    {
        if (gameMenu.IsOpen)
            Close(ref gameMenu);
        else
            Open(ref gameMenu);
    }

    private void Close(ref GameMenu gameMenu)
    {
        gameMenu.Image.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameMenu.IsOpen = false;
    }

    private void Open(ref GameMenu gameMenu)
    {
        gameMenu.Image.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameMenu.IsOpen = true;
    }
}