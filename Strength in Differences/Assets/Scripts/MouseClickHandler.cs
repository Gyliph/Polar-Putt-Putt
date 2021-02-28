using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseClickHandler : MonoBehaviour
{
    public GameObject levelManager;
    public AudioSource audioSource;
    private LevelManager levelManagerScript;
    private Tilemap tilemap;
    public AnimatedTile[] wallBot;
    public AnimatedTile[] wallTop;
    public AnimatedTile[] wallLeft;
    public AnimatedTile[] wallRight;
    public AnimatedTile[] cornerTopRight;
    public AnimatedTile[] cornerTopLeft;
    public AnimatedTile[] cornerBotLeft;
    public AnimatedTile[] cornerBotRight;
    private Dictionary<string, AnimatedTile[]> tileNames;

    // Start is called before the first frame update
    void Start()
    {
        levelManagerScript = levelManager.GetComponent<LevelManager>();
        refreshTileDict();
    }

    public void refreshTileDict()
    {
        tilemap = levelManagerScript.getCurrentLevelWalls();
        tileNames = new Dictionary<string, AnimatedTile[]>();
        tileNames.Add("WallBot", wallBot);
        tileNames.Add("WallTop", wallTop);
        tileNames.Add("WallLeft", wallLeft);
        tileNames.Add("WallRight", wallRight);
        tileNames.Add("CornerTopRight", cornerTopRight);
        tileNames.Add("CornerTopLeft", cornerTopLeft);
        tileNames.Add("CornerBotLeft", cornerBotLeft);
        tileNames.Add("CornerBotRight", cornerBotRight);

        BoundsInt bounds = tilemap.cellBounds;

        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);

                TileBase tile = tilemap.GetTile(pos);
                if (tile == null)
                {
                    continue;
                }
                string wallType = GetWallType(tile.name);
                Debug.Log("Wall type: " + wallType);
                if (tileNames.ContainsKey(wallType))
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileNames[wallType][1]);
            }
        }
    }

    string GetWallType(string tileName)
    {
        string chargeType = tileName.Substring(tileName.Length - 3);
        string wallType = tileName.Substring(0, tileName.Length - 3);
        if (chargeType == "Neg" || chargeType == "Pos")
        {
            return wallType;
        }
        return tileName;
    }

    AnimatedTile GetNextSprite(string tileName)
    {
        string chargeType = tileName.Substring(tileName.Length - 3);
        string wallType = tileName.Substring(0, tileName.Length - 3);
        Debug.Log("Charge: " + chargeType + ", Wall: " + wallType);
        int currentCharge = 1;
        if (chargeType == "Neg")
        {
            currentCharge = 0;
        }
        else if (chargeType == "Pos")
        {
            currentCharge = 2;
        }

        int nextCharge = (currentCharge + 1) % 3;
        if (currentCharge == 1)
        {
            return tileNames[tileName][nextCharge];
        }
        return tileNames[wallType][nextCharge];
    }

    void SetIndexSprite(int index)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cell = tilemap.WorldToCell(mousePos);

        TileBase tileBase = tilemap.GetTile(cell);
        if (tileBase == null)
        {
            return;
        }

        audioSource.Play();
        string tileName = tileBase.name;

        string chargeType = tileName.Substring(tileName.Length - 3);
        string wallType = tileName.Substring(0, tileName.Length - 3);
        Debug.Log("Charge: " + chargeType + ", Wall: " + wallType);
        int currentCharge = 1;
        if (chargeType == "Neg")
        {
            currentCharge = 0;
        }
        else if (chargeType == "Pos")
        {
            currentCharge = 2;
        }

        int nextCharge = index;
        if (currentCharge == index)
        {
            nextCharge = 1;
        }

        if (currentCharge == 1)
        {
            tilemap.SetTile(cell, tileNames[tileName][nextCharge]);
            return; 
        }
        tilemap.SetTile(cell, tileNames[wallType][nextCharge]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            SetIndexSprite(0);
        }

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            SetIndexSprite(2);
        }
    }
}
