using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Block : MonoBehaviour
{
    [SerializeField] private SpriteRenderer BlockSprite;

    private BlockSO blockSO;
    private List<Sprite> BlockSpriteList = new List<Sprite>();
    private Animator BlockAnim;
    private float xPos, yPos;
    private bool linked;
    private List<Block> LinkList = new List<Block>();

    private void Awake()
    {
        BlockAnim = GetComponent<Animator>();
    }

    public void SetPos(float _x, float _y)
    {
        xPos = _x;
        yPos = _y;
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

    private void OnMouseDown()
    {
        if (linked)
        {
            BoardManager.Instance.PopLink(LinkList);
        }
        else
        {
            BlockAnim.Play("Shake");
        }
    }

    public void SetLink(bool state)
    {
        linked = state;
    }

    public bool isLinked()
    {
        return linked;
    }

    public void SetLinkList(List<Block> _linklist)
    {
        LinkList.Clear();
        LinkList.AddRange(_linklist);
        SetSprite(_linklist.Count);
    }
}
