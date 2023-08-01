using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private SpriteRenderer BlockSprite;
    private BlockSO blockSO;
    private List<Sprite> BlockSpriteList = new List<Sprite>();
    private Animator BlockAnim;
    private int xPos, yPos;
    private Grid myGrid;

    private void Awake()
    {
        BlockAnim = GetComponent<Animator>();
    }

    public void SetBlock(BlockSO _blockSO)
    {
        blockSO = _blockSO;
        BlockSpriteList = blockSO.BlockSpriteList;
        SetSprite(0);
    }

    public void SetSprite(int linkCount)
    {
        if (linkCount < 5)
        {
            BlockSprite.sprite = blockSO.BlockSpriteList[0];
        }
        else if (linkCount < 8)
        {
            BlockSprite.sprite = blockSO.BlockSpriteList[1];
        }
        else if (linkCount < 10)
        {
            BlockSprite.sprite = blockSO.BlockSpriteList[2];
        }
        else
        {
            BlockSprite.sprite = blockSO.BlockSpriteList[2];
        }
    }

    public BlockSO GetBlockSO()
    {
        return blockSO;
    }

    public void Shake()
    {
        BlockAnim.Play("Shake");
    }
}
