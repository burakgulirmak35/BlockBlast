using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Scriptable/LevelSO", order = 0)]
public class LevelSO : ScriptableObject
{
    public List<BlockSO> BlockList;
    public int width;
    public int height;
    [Space]
    public int moveAmount;
    public int targetScore;
}
