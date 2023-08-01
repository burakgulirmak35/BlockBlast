using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grid : MonoBehaviour
{
    private Block block;
    private int xPos, yPos;
    private bool linked;

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

    public void PopBlock()
    {
        if (isEmpty()) { return; }
        block.gameObject.SetActive(false);
        block = null;
    }

    public void SetBlock(Block _block)
    {
        block = _block;
    }

    public Block GetBlock()
    {
        return block;
    }

    public bool isEmpty()
    {
        return block == null;
    }

    private List<Grid> LinkList = new List<Grid>();
    public void SetLinkList(List<Grid> _linklist)
    {
        LinkList.Clear();
        LinkList.AddRange(_linklist);
        block.SetSprite(_linklist.Count);
    }

    private void OnMouseDown()
    {
        if (isLinked())
        {
            BoardManager.Instance.PopLink(LinkList);
        }
        else
        {
            block.Shake();
        }
    }

    public void Fall()
    {
        /*
        if (BoardManager.Instance.IsValidMove(xPos, yPos - 1))
        {
            SetPos(xPos, yPos - 1);
            transform.DOMoveY(yPos, FallingSpeed).OnComplete(() => Fall());
            return;
        }
        */
    }

}
