using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum State
{
    Setup,
    Busy,
    WaitingForUser,
    End,
}

public class BoardManager : MonoBehaviour
{
    public event EventHandler OnMoveUsed;
    public event EventHandler OnScoreChanged;
    public event EventHandler OnOutOfMoves;
    public event EventHandler OnWin;

    public static BoardManager Instance { get; private set; }
    [SerializeField] private Transform pfGrid;
    [SerializeField] private List<LevelSO> levelList;
    [SerializeField] private Grid[,] GridList;

    private int boardWidth;
    private int boardHeight;
    private int score;
    private int moveCount;
    private int targetScore;

    private LevelSO levelSO;
    private PoolManager pool;

    [HideInInspector] public State state;
    private void Awake()
    {
        state = State.Setup;
        Instance = this;
        pool = GetComponent<PoolManager>();
        SetLevelSO(levelList[PlayerPrefs.GetInt("Level", 0)]);
        Setup();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (state.Equals(State.WaitingForUser))
            {
                RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
                if (rayHit.collider.GetComponent<IClickable>() != null)
                {
                    rayHit.collider.GetComponent<IClickable>().Click();
                }
            }
        }
    }

    private void SetLevelSO(LevelSO _levelSO)
    {
        levelSO = _levelSO;
        boardWidth = levelSO.width;
        boardHeight = levelSO.height;
        GridList = new Grid[boardWidth, boardHeight];
        pool.GenerateBlockPool(boardWidth * boardHeight * 2);

        moveCount = levelSO.moveAmount;
        targetScore = levelSO.targetScore;
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
        transform.position = new Vector2(-(boardWidth / 2) + 0.5f, -(boardHeight / 2) + 0.5f) + levelSO.BoardOffset;
        CheckAllMatch3Links();
        state = State.WaitingForUser;
    }

    private void CreateGrid(int xPos, int yPos)
    {
        GridList[xPos, yPos] = Instantiate(pfGrid, new Vector2(xPos, yPos), Quaternion.identity, transform).GetComponent<Grid>();
    }

    private void CreateBlock(int xPos, int yPos)
    {
        Block newBlock = pool.GetFromPool(PoolTypes.BlockPool).GetComponent<Block>();
        newBlock.transform.position = new Vector2(xPos, yPos);
        newBlock.SetBlockSO(GetRandomBlock());
        GridList[xPos, yPos].SetBlock(newBlock);
        newBlock.gameObject.SetActive(true);
    }

    private BlockSO GetRandomBlock()
    {
        int rnd = UnityEngine.Random.Range(0, levelSO.BlockList.Count);
        return levelSO.BlockList[rnd];
    }

    private Block SpawnNewBlock(int x, int y, Vector3 offset)
    {
        Block newBlock = pool.GetFromPool(PoolTypes.BlockPool).GetComponent<Block>();
        newBlock.SetBlockSO(GetRandomBlock());
        newBlock.transform.position = GridList[x, y].transform.position + offset;
        newBlock.gameObject.SetActive(true);
        return newBlock;
    }

    //-------------------------------------

    public void PopLink(List<Grid> _linkList)
    {
        StartCoroutine(PopLinkTimer(_linkList));
    }

    private IEnumerator PopLinkTimer(List<Grid> _linkList)
    {
        UseMove();
        state = State.Busy;
        foreach (Grid item in _linkList)
        {
            item.PopBlock();
            yield return new WaitForSeconds(0.1f);
            AddScore(100);
        }
        yield return new WaitForSeconds(0.2f);
        ReplaceBlocks();
        MoveBlocks();
        yield return new WaitForSeconds(.2f);
        SpawnNewBlocks();
        MoveBlocks();
        yield return new WaitForSeconds(.4f);
        CheckAllMatch3Links();
        CheckGameStatus();
    }

    private void SpawnNewBlocks()
    {
        for (int x = 0; x < boardWidth; x++)
        {
            int emptyBlockCount = 0;
            for (int y = 0; y < boardHeight; y++)
            {
                if (GridList[x, y].isEmpty())
                {
                    emptyBlockCount++;
                    GridList[x, y].SetBlock(SpawnNewBlock(x, boardHeight - 1, new Vector3(0, emptyBlockCount, 0)));
                }
            }
        }
    }

    public void ReplaceBlocks()
    {
        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                if (GridList[x, y].isEmpty())
                {
                    for (int i = y; i < boardHeight; i++)
                    {
                        if (!GridList[x, i].isEmpty())
                        {
                            GridList[x, y].SetBlock(GridList[x, i].GetBlock());
                            GridList[x, i].Clear();
                            break;
                        }
                    }
                }
            }
        }
    }

    private void MoveBlocks()
    {
        foreach (Grid grid in GridList)
        {
            grid.FallBlock();
        }
    }

    //-------------------------------------

    private List<Grid> linkedGridList = new List<Grid>();
    private void CheckAllMatch3Links()
    {
        foreach (Grid grid in GridList)
        {
            grid.ClearLink();
        }

        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                if (!GridList[x, y].isLinked())
                {
                    linkedGridList.Clear();
                    CheckMatch3Link(x, y);
                    if (linkedGridList.Count > 1)
                    {
                        foreach (Grid grid in linkedGridList)
                        {
                            grid.SetLinkList(linkedGridList);
                        }
                    }
                    else
                    {
                        linkedGridList[0].SetLink(false);
                    }
                }
            }
        }
    }


    private void CheckMatch3Link(int x, int y)
    {
        if (GridList[x, y].isLinked()) { return; }
        GridList[x, y].SetLink(true);
        linkedGridList.Add(GridList[x, y]);

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

    public bool IsValidMove(int x, int y)
    {
        if (x < 0 || y < 0 || x >= boardWidth || y >= boardHeight)
        {
            return false;
        }
        else if (GridList[x, y].isEmpty())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private BlockSO GetBlockSO(int x, int y)
    {
        return GridList[x, y].GetBlock().GetBlockSO();
    }

    //-----------------

    private void UseMove()
    {
        moveCount--;
        OnMoveUsed?.Invoke(this, EventArgs.Empty);
    }

    private void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetScore()
    {
        return score;
    }

    public int GetTargetScore()
    {
        return targetScore;
    }

    public int GetMoveCount()
    {
        return moveCount;
    }

    public int GetColorWayPoint(int id)
    {
        return levelSO.ColorWaypoints[id];
    }

    public void CheckGameStatus()
    {
        if (score >= targetScore)
        {
            OnWin?.Invoke(this, EventArgs.Empty);
        }
        else if (moveCount <= 0)
        {
            OnOutOfMoves?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            state = State.WaitingForUser;
        }
    }
}
