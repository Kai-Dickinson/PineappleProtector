using UnityEngine;
using TMPro;
public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI score;
    public int currScore;
    // Start is called before the first frame update
    void Start()
    {
        currScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = currScore.ToString();
    }

    public void AddScore(float personHP, float personMaxHP, float currSR, float maxSR)
    {
        currScore += (int)(20 * (personHP/personMaxHP) * (maxSR/currSR));
    }

    public void AddPlainScore(int score)
    {
        currScore += score;
    }
}
