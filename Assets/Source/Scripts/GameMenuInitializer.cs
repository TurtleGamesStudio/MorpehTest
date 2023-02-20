using UnityEngine;
using UnityEngine.UI;

public class GameMenuInitializer : MonoBehaviour
{
    [SerializeField] private GameMenuProvider _gameMenuProvider;
    [SerializeField] private Image _panel;

    private void Start()
    {
        ref GameMenu gameMenu = ref _gameMenuProvider.GetData();
        gameMenu.Image = _panel;
        gameMenu.IsOpen = false;
    }
}
