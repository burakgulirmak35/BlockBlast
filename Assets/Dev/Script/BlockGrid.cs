using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockGrid
{

    public event EventHandler OnDestroyed;

    private BlockSO block;
    private int x;
    private int y;
    private bool isDestroyed;

    public BlockGrid(BlockSO block, int x, int y)
    {
        this.block = block;
        this.x = x;
        this.y = y;

        isDestroyed = false;
    }

    public BlockSO GetBlock()
    {
        return block;
    }

    public Vector3 GetWorldPosition()
    {
        return new Vector3(x, y);
    }

    public void SetBlockXY(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void Destroy()
    {
        isDestroyed = true;
        OnDestroyed?.Invoke(this, EventArgs.Empty);
    }

    public override string ToString()
    {
        return isDestroyed.ToString();
    }
}
