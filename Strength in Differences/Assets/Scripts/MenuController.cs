using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Button playButton;
    public Animator menuAnimator;
    public float animationPeriodicity = 8f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PlayAnim", animationPeriodicity, animationPeriodicity);
        playButton.onClick.AddListener(PlayGame);
    }

    void PlayAnim()
    {
        menuAnimator.SetBool("Surge", true);
    }

    void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    // Update is called once per frame
    void Update()
    {
        menuAnimator.SetBool("Surge", false);
        
    }
}
