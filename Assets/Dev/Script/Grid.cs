using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grid : MonoBehaviour, IClickable
{
    [SerializeField] private float BlockFallingSpeed;
    private Block block;
    private int xPos, yPos;
    private bool linked;
    private bool placed;

    public void SetPos(int _x, int _y)
    {
        xPos = _x;
        yPos = _y;
    }

    public int GetX()
    {
        return xPos;
    }

    public int GetY()
    {
        return yPos;
    }

    public void SetLink(bool state)
    {
        linked = state;
    }

    public bool isLinked()
    {
        return linked;
    }

    public void ClearLink()
    {
        LinkList = new List<Grid>();
        linked = false;
    }

    public void PopBlock()
    {
        if (isEmpty()) { return; }
        block.GetAnimator().Play("Pop");
        SoundManager.Instance.PlaySound(SoundName.PopSound);
        PoolManager.Instance.PutBackToPool(PoolTypes.BlockPool, block.gameObject);
        block = null;
    }


    public void SetBlock(Block _block, bool _placed)
    {
        block = _block;
        placed = _placed;
    }

    public Block GetBlock()
    {
        return block;
    }

    public bool isEmpty()
    {
        return block == null;
    }

    public void Clear()
    {
        block = null;
        linked = false;
        LinkList.Clear();
    }

    private List<Grid> LinkList = new List<Grid>();
    public void SetLinkList(List<Grid> _linklist)
    {
        LinkList.Clear();
        LinkList.AddRange(_linklist);
        block.SetSprite(_linklist.Count);
    }

    public void Click()
    {
        if (isLinked())
        {
            BoardManager.Instance.PopLink(LinkList);
        }
        else
        {
            SoundManager.Instance.PlaySound(SoundName.ShakeSound);
            block.Shake();
        }
    }

    public void FallBlock()
    {
        if (isEmpty()) { return; }
        if (placed) { return; }
        float fallingTime = BlockFallingSpeed * Mathf.Abs(block.transform.position.y - transform.position.y);
        block.transform.DOMove(transform.position, fallingTime).SetEase(Ease.OutBounce);
        placed = true;
    }

}

