using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Scriptable/LevelSO", order = 0)]
public class LevelSO : ScriptableObject
{
    public List<BlockSO> gemList;
    public int width;
    public int height;
    public List<LevelGridPosition> levelGridPositionList;
    public int moveAmount;
    public int targetScore;


    [System.Serializable]
    public class LevelGridPosition
    {

        public BlockSO gemSO;
        public int x;
        public int y;
        public bool hasGlass;

    }

}
