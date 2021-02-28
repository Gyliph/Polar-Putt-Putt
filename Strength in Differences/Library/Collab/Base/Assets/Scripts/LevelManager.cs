using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int level = 0;
    public GameObject levels;
    public GameObject ball;
    public GameObject hole;
    public GameObject mouseClickHandler;
    private MouseClickHandler mouseClickHandlerScript;
    private BallController ballControllerScript;
    
    void Start()
    {
        mouseClickHandlerScript = mouseClickHandler.GetComponent<MouseClickHandler>();
        ballControllerScript = ball.GetComponent<BallController>();

        ball.transform.position = getCurrentLevel().transform.Find("BallStart").gameObject.transform.position;
        hole.transform.position = getCurrentLevel().transform.Find("HolePosition").gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextLevel()
    {
        GameObject lastLevel = getCurrentLevel();
        lastLevel.SetActive(false);

        level += 1;

        refreshLevel();

        GameObject nextLevel = getCurrentLevel();
        nextLevel.SetActive(true);
    }

    public void refreshLevel()
    {
        mouseClickHandlerScript.refreshTileDict();
        ball.transform.position = getCurrentLevel().transform.Find("BallStart").gameObject.transform.position;
        hole.transform.position = getCurrentLevel().transform.Find("HolePosition").gameObject.transform.position;
    }

    public GameObject getCurrentLevel()
    {
        return getLevel(level);
    }

    public GameObject getLevel(int index)
    {
        return levels.transform.GetChild(index).gameObject;
    }

    public Tilemap getCurrentLevelWalls()
    {
        return getCurrentLevel().transform.Find("Wall").gameObject.GetComponent<Tilemap>();
    }
}
