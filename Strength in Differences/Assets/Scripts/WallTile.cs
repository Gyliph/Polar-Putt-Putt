using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu]
public class WallTile : TileBase
{
    public Sprite[] neutral;
    public Sprite[] negative;
    public Sprite[] positive;
    public float m_AnimationSpeed = 1f;
    public float m_AnimationStartTime;
    public int charge = 0;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        Debug.Log("test2 - " + charge);
        switch (charge)
        {
            case -1: tileData.sprite = negative[0]; return;
            case 0: tileData.sprite = neutral[0]; return;
            case 1: tileData.sprite = positive[0]; return;
        }
    }

    // public override bool GetTileAnimationData(Vector3Int location, ITilemap tileMap, ref TileAnimationData tileAnimationData)
    // {
    //     if (m_AnimatedSprites != null && m_AnimatedSprites.Length > 0)
    //     {
    //         tileAnimationData.animatedSprites = m_AnimatedSprites;
    //         tileAnimationData.animationSpeed = m_AnimationSpeed;
    //         tileAnimationData.animationStartTime = m_AnimationStartTime;
    //         return true;
    //     }
    //     return false;
    // }

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);
    }
}
