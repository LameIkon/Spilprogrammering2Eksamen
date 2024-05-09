using Mirror;
using TMPro;


public class EnemyScore : NetworkBehaviour
{
    public TextMeshProUGUI enemyScore;

    [SyncVar(hook = nameof(DisplayEnemyScore))]
    public int enemyScoreValue = 0;

    public void DisplayEnemyScore(int oldScore, int newScore)
    {
        enemyScore.text = "Enemy score: " + newScore;
    }

    private void Start()
    {
        enemyScore.text = "Enemy score: 0";
    }
}
