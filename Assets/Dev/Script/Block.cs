using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private SpriteRenderer BlockSprite;
    [SerializeField]
    private BlockSO blockSO;
    private List<Sprite> BlockSpriteList = new List<Sprite>();
    private Animator BlockAnim;

    private void Awake()
    {
        BlockAnim = GetComponent<Animator>();
    }

    public void SetBlockSO(BlockSO _blockSO)
    {
        blockSO = _blockSO;
        BlockSpriteList = blockSO.BlockSpriteList;
        SetSprite(0);
    }

    public void SetSprite(int linkCount)
    {
        if (linkCount < BoardManager.Instance.GetColorWayPoint(0))
        {
            BlockSprite.sprite = blockSO.BlockSpriteList[0];
        }
        else if (linkCount < BoardManager.Instance.GetColorWayPoint(1))
        {
            BlockSprite.sprite = blockSO.BlockSpriteList[1];
        }
        else if (linkCount < BoardManager.Instance.GetColorWayPoint(2))
        {
            BlockSprite.sprite = blockSO.BlockSpriteList[2];
        }
        else
        {
            BlockSprite.sprite = blockSO.BlockSpriteList[3];
        }
    }

    public Animator GetAnimator()
    {
        return BlockAnim;
    }

    public BlockSO GetBlockSO()
    {
        return blockSO;
    }

    public void DisableObject()
    {
        //animation event
        gameObject.SetActive(false);
    }

    public void Shake()
    {
        BlockAnim.Play("Shake");
    }
}
