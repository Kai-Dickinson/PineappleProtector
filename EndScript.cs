using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class EndScript : MonoBehaviour
{
    public GameObject endScreen; bool ended = false;
    ScoreScript score; public TextMeshProUGUI fscore;
    // Start is called before the first frame update
    void Start()
    {
        score = FindObjectOfType<ScoreScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void EndGame()
    {
        if(!ended)
        {
            StartCoroutine(End());
        }
        
    }

    IEnumerator End()
    {
        endScreen.SetActive(true);
        fscore.text = "Final Score: " + score.currScore.ToString();
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
    }

    public void Return()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
