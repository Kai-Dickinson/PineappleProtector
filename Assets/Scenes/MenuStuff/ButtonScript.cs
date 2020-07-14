using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class ButtonScript : MonoBehaviour
{
    bool controlPage = false; public GameObject playGamePage; public GameObject controlStuff; public GameObject mainButtons;

    public Animator animator;

    public Button playbutton; public Image hatPlay;
    public Button controlbutton; public Image hatControl;
    public Button quitbutton; public Image hatQuit;
    public Button backButton;
    public EventSystem eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        playbutton.interactable = true;
        controlbutton.interactable = true;
        quitbutton.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(eventSystem.currentSelectedGameObject != null)
        {
            if(eventSystem.currentSelectedGameObject.Equals(playbutton.gameObject)){hatPlay.gameObject.SetActive(true);} else {hatPlay.gameObject.SetActive(false);}

            if(eventSystem.currentSelectedGameObject.Equals(controlbutton.gameObject)){hatControl.gameObject.SetActive(true);} else {hatControl.gameObject.SetActive(false);}

            if(eventSystem.currentSelectedGameObject.Equals(quitbutton.gameObject)){hatQuit.gameObject.SetActive(true);} else {hatQuit.gameObject.SetActive(false);}
        }

    }

    public void PlayGame()
    {
        //Explain game
        StartCoroutine(Instructions());
    }

    public void Controls()
    {

        if(!controlPage)
        {
            StartCoroutine(WaitForAnim(mainButtons, false));
            controlStuff.SetActive(true);
            eventSystem.SetSelectedGameObject(backButton.gameObject);
            controlPage = true; 
            animator.SetBool("ControlPage", true);
        } else 
        {
            StartCoroutine(WaitForAnim(controlStuff,false));
            mainButtons.SetActive(true);
            controlPage = false;
            animator.SetBool("ControlPage", false);
            eventSystem.SetSelectedGameObject(playbutton.gameObject);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator Instructions()
    {
        playbutton.interactable = false;
        controlbutton.interactable = false;
        quitbutton.interactable = false;

        playGamePage.SetActive(true);
        animator.SetTrigger("Play");
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator WaitForAnim(GameObject page, bool onOrOff)
    {
        yield return new WaitForSeconds(1.5f);
        page.SetActive(onOrOff);
    }

}
