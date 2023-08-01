using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockSO", menuName = "Scriptable/BlockSO", order = 0)]
public class BlockSO : ScriptableObject
{
    public string blockName;
    public List<Sprite> BlockSpriteList = new List<Sprite>();
}