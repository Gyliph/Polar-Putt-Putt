using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour
{
    public Animator menuAnimator;
    public GameObject[] textObjects;
    public float animationPeriodicity = 8f;
    public int textCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PlayAnim", animationPeriodicity, animationPeriodicity);
        InvokeRepeating("ShowNextText", 0f, 3f);

        Invoke("ShowMenu", 15f);
    }

    void PlayAnim()
    {
        menuAnimator.SetBool("Surge", true);
    }

    void ShowNextText()
    {
        if (textCounter == textObjects.Length)
            return;
        textObjects[textCounter].SetActive(true);
        textCounter += 1;
    }

    void ShowMenu()
    {
        SceneManager.LoadScene("Menu");
        foreach (GameObject textObj in textObjects)
        {
            textObj.SetActive(false);
        }
        textCounter = 0;

    }

    // Update is called once per frame
    void Update()
    {
        menuAnimator.SetBool("Surge", false);
    }
}
