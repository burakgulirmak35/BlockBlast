using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [Header("DamageText")]
    [SerializeField] private Transform pfBlockGrid;
    private Queue<GameObject> blockPool = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void GeneratePool(GameObject prefab, int count, Queue<GameObject> pool, Transform holder)
    {
        for (int i = 0; i < count; i++)
        {
            // tempObject = Instantiate(prefab, holder);
            // tempObject.SetActive(false);
            // pool.Enqueue(tempObject);
        }
    }
}
