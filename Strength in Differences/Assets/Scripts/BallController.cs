using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BallController : MonoBehaviour
{

    private Tilemap walls;
    public float magnetStrength;
    public float animationPeriodicity = 8f;
    public float distanceStrength;
    public GameObject levelManager;
    private LevelManager levelManagerScript;

    public AudioClip[] bounceSounds;
    private AudioSource audioSource;
    private Animator animator;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        levelManagerScript = levelManager.GetComponent<LevelManager>();

        refreshWallForces();

        InvokeRepeating("PlayAnim", animationPeriodicity, animationPeriodicity);
    }

    void refreshWallForces()
    {
        walls = levelManagerScript.getCurrentLevelWalls();

        BoundsInt bounds = walls.cellBounds;

        TileBase[] allTiles = walls.GetTilesBlock(bounds);

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);

                TileBase tile = walls.GetTile(pos);
                if (tile == null) { continue; }
                Vector3 worldPos = walls.CellToWorld(pos);
                worldPos += walls.cellSize / 2;
                Vector3 force = transform.position - worldPos;
                int charge = GetCharge(tile.name);
                float mag = force.magnitude;
                force = force.normalized * charge * magnetStrength / (distanceStrength * mag);
                rb.AddForce(force);
            }
        }
    }
    void PlayAnim()
    {
        animator.SetBool("Surge", true);
    }

    public void SetPosition(Vector3 position)
    {
        rb.position = position;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
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
    void FixedUpdate()
    {
        animator.SetBool("Surge", false);

        refreshWallForces();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Hole"))
        {
            levelManagerScript.nextLevel();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // audioSource.clip =
        if (collision.gameObject.CompareTag("Wall"))
        {
            int clipIndex = Random.Range(0, bounceSounds.Length);
            audioSource.volume = rb.velocity.magnitude;
            audioSource.PlayOneShot(bounceSounds[clipIndex]);
        }


        if (collision.gameObject.CompareTag("DeathTile"))
        {
            Debug.DrawLine(transform.position, collision.transform.position);
            levelManagerScript.refreshLevel();
        }
    }
}
