using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{

    public question[] questions;
    private static List<question> unanswered;
    private question current;
    [SerializeField]
    private Text facttext;

    [SerializeField]
    private Text trueAnswerText; 

    [SerializeField]
    private Text falseAnswerText;

    [SerializeField]
    private float delay = 1f;

    [SerializeField]
    private Animator animator;

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void Start()
    
    {
      if(unanswered == null || unanswered.Count == 0)
        {
            unanswered = questions.ToList<question>();
        }

        GetRandomQuestion();
    }
    void GetRandomQuestion()
    {
        int index = Random.Range(0, unanswered.Count);
        current = unanswered[index];
        facttext.text = current.fact;
        if(current.truth)
        {
            trueAnswerText.text = "Corect! Felicitari!";
            falseAnswerText.text = "Gresit! Mai cauta..";
        }
        else
        { 
            trueAnswerText.text = "Gresit! Mai cauta..";
            falseAnswerText.text = "Corect! Felicitari!";

        }
    }

    IEnumerator TransitionToNextQuestion()
    {
        
        unanswered.Remove(current);
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void UserSelectTrue ()
    {
        
        if(current.truth)
        {
            Debug.Log("coarda 1"); //Corect
        }
        else
        {
            Debug.Log("coarda 2"); //gresit
        }
        StartCoroutine(TransitionToNextQuestion());
        animator.SetTrigger("True");
    }
    public void UserSelectFalse()
    {

        if (current.truth)
        {
            Debug.Log("coarda 2"); //gresit
        }
        else
        {
            Debug.Log("coarda 1"); //corect
        }
        StartCoroutine(TransitionToNextQuestion());
        animator.SetTrigger("False");
    }

}
