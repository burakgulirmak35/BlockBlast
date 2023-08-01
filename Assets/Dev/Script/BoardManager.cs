using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; private set; }

    [SerializeField] private Transform pfBlockGrid;
    [SerializeField] private Transform pfBackgroundGrid;

    [SerializeField] private List<LevelSO> levelList;
    [SerializeField] private Block[,] BlockList;

    private int boardWidth;
    private int boardHeight;
    private int score;
    private int moveCount;
    private LevelSO levelSO;

    private void Awake()
    {
        Instance = this;
        SetLevelSO(levelList[PlayerPrefs.GetInt("Level", 0)]);
        Setup();
    }

    private void SetLevelSO(LevelSO _levelSO)
    {
        levelSO = _levelSO;
        boardWidth = levelSO.width;
        boardHeight = levelSO.height;
        BlockList = new Block[boardWidth, boardHeight];
    }

    private void Setup()
    {
        for (int width = 0; width < boardWidth; width++)
        {
            for (int height = 0; height < boardHeight; height++)
            {
                CreateGrid(width, height);
                CreateBlock(width, height);
            }
        }
        transform.position = new Vector2(-(boardWidth / 2f), -(boardHeight / 2f));
        CheckAllMatch3Links();
    }

    private void CreateGrid(int xPos, int yPos)
    {
        GameObject new_Grid = Instantiate(pfBackgroundGrid, new Vector2(xPos, yPos), Quaternion.identity, transform).gameObject;
    }

    private void CreateBlock(int xPos, int yPos)
    {
        Block newBlock = Instantiate(pfBlockGrid, new Vector2(xPos, yPos), Quaternion.identity, transform).GetComponent<Block>();
        newBlock.SetPos(xPos, yPos);
        newBlock.SetBlock(GetRandomBlock());
        BlockList[xPos, yPos] = newBlock;
    }

    private BlockSO GetRandomBlock()
    {
        int rnd = Random.Range(0, levelSO.BlockList.Count);
        return levelSO.BlockList[rnd];
    }

    private void SpawnNewBlock(int xPos)
    {
        Block newBlock = Instantiate(pfBlockGrid, new Vector2(xPos, boardHeight), Quaternion.identity, transform).GetComponent<Block>();
        newBlock.SetBlock(GetRandomBlock());
        for (int height = 0; height < BlockList.GetLength(1); height++)
        {
            if (BlockList[xPos, height] == null)
            {
                BlockList[xPos, height] = newBlock;
                newBlock.SetPos(xPos, height);
                break;
            }
        }
    }

    //-------------------------------------

    public void PopLink(List<Block> _linkList)
    {
        foreach (Block item in _linkList)
        {
            Destroy(item.gameObject);
        }
    }

    //-------------------------------------

    private void CheckAllMatch3Links()
    {
        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                if (!BlockList[x, y].isLinked())
                {
                    CheckMatch3Link(x, y);
                    if (linkedBlockPositionList.Count > 1)
                    {
                        foreach (Block block in linkedBlockPositionList)
                        {
                            block.SetLinkList(linkedBlockPositionList);
                        }
                    }
                    else
                    {
                        linkedBlockPositionList[0].SetLink(false);
                    }
                    linkedBlockPositionList.Clear();
                }
            }
        }
    }

    private List<Block> linkedBlockPositionList = new List<Block>();
    private void CheckMatch3Link(int x, int y)
    {
        if (BlockList[x, y].isLinked()) { return; }
        BlockList[x, y].SetLink(true);
        linkedBlockPositionList.Add(BlockList[x, y]);

        BlockSO blockSO = GetBlockSO(x, y);

        if (IsValidPosition(x + 1, y) && (GetBlockSO(x + 1, y) == blockSO))
        {
            CheckMatch3Link(x + 1, y);
        }

        if (IsValidPosition(x - 1, y) && (GetBlockSO(x - 1, y) == blockSO))
        {
            CheckMatch3Link(x - 1, y);
        }

        if (IsValidPosition(x, y + 1) && (GetBlockSO(x, y + 1) == blockSO))
        {
            CheckMatch3Link(x, y + 1);
        }

        if (IsValidPosition(x, y - 1) && (GetBlockSO(x, y - 1) == blockSO))
        {
            CheckMatch3Link(x, y - 1);
        }

    }

    private bool IsValidPosition(int x, int y)
    {
        if (x < 0 || y < 0 || x >= boardWidth || y >= boardHeight)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private BlockSO GetBlockSO(int x, int y)
    {
        return BlockList[x, y].GetBlockSO();
    }


    //-----------------
}
