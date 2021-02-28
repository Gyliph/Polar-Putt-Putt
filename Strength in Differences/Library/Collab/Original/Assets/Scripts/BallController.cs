using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BallController : MonoBehaviour
{

    public Tilemap walls;
    public float magnetStrength;
    public float animationPeriodicity = 8f;
    private Animator animator;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        BoundsInt bounds = walls.cellBounds;


        TileBase[] allTiles = walls.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile == null) { continue; }
                Vector3 pos = walls.CellToWorld(new Vector3Int(x, y, 0));
                Vector3 force = transform.position - pos;
                int charge = GetCharge(tile.name);
                force = force * charge * magnetStrength;

                rb.AddForce(force.normalized);
            }
        }

        InvokeRepeating("PlayAnim", animationPeriodicity, animationPeriodicity);
    }

    void PlayAnim()
    {
        animator.SetBool("Surge", true);
    }

    int GetCharge(string tileName)
    {
        string chargeType = tileName.Substring(tileName.Length - 3);

        switch (chargeType)
        {
            case "Neg": return -1;
            case "Pos": return 1;
            default: return 0;
        }

    }
    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Surge", false);
    }
}
