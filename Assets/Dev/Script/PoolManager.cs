using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [Header("Block")]
    [SerializeField] private GameObject pfBlock;
    [SerializeField] private Transform BlockHolder;
    private Queue<GameObject> blockPool = new Queue<GameObject>();
    private GameObject tempObject;

    private void Awake()
    {
        Instance = this;
    }

    public void GenerateBlockPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            tempObject = Instantiate(pfBlock, BlockHolder);
            tempObject.SetActive(false);
            blockPool.Enqueue(tempObject);
        }
    }

    public GameObject GetFromPool(PoolTypes type)
    {
        switch (type)
        {
            case PoolTypes.BlockPool:
                tempObject = blockPool.Dequeue();
                blockPool.Enqueue(tempObject);
                return tempObject;
            default:
                tempObject = null;
                return tempObject;
        }
    }
}

public enum PoolTypes
{
    BlockPool,
}
