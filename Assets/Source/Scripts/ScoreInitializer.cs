using UnityEngine;
using TMPro;

public class ScoreInitializer : MonoBehaviour
{
    private const string Score = "Score";

    [SerializeField] private ScoreViewProvider _scoreViewProvider;
    [SerializeField] private TMP_Text _text;

    [SerializeField] private ScoreProvider _score;

    private void Start()
    {
        _scoreViewProvider.GetData().Text = _text;
        ref Score score = ref _score.GetData();

        if (PlayerPrefs.HasKey(Score))
        {
            Score loadedScore = PlayerPrefsSaver<Score>.Load(Score);
            score = loadedScore;
        }
        else
        {
            score.Key = Score;
        }
    }
}
