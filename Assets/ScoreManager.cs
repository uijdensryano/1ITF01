using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int stars_collected = 0;

    public void AddPoint()
    {
        stars_collected++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        Debug.Log("score:" + stars_collected);
    }
}
