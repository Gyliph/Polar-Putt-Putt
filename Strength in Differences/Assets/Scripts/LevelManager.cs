﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

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

        GameObject currentLevel = getCurrentLevel();
        if (currentLevel)
        {
            ball.transform.position = currentLevel.transform.Find("BallStart").gameObject.transform.position;
            hole.transform.position = currentLevel.transform.Find("HolePosition").gameObject.transform.position;
        }
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
        GameObject currentLevel = getCurrentLevel();
        if (currentLevel)
        {
            mouseClickHandlerScript.refreshTileDict();
            ballControllerScript.SetPosition(currentLevel.transform.Find("BallStart").gameObject.transform.position);
            hole.transform.position = currentLevel.transform.Find("HolePosition").gameObject.transform.position;
        }
    }

    public GameObject getCurrentLevel()
    {
        return getLevel(level);
    }

    public GameObject getLevel(int index)
    {
        try
        {
            Debug.Log("test");
            Debug.Log(levels.transform.GetChild(index).gameObject);
            return levels.transform.GetChild(index).gameObject;
        }
        catch (Exception e)
        {
            level = 0;
            refreshLevel();
            SceneManager.LoadScene("End");
            return null;
        }


    }

    public Tilemap getCurrentLevelWalls()
    {
        GameObject currentLevel = getCurrentLevel();
        if (currentLevel)
        {
            return currentLevel.transform.Find("Wall").gameObject.GetComponent<Tilemap>();
        }
        else
        {
            return null;
        }
    }
}
