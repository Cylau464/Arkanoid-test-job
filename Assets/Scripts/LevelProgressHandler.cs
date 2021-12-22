using UnityEngine;

public class LevelProgressHandler : MonoBehaviour
{
    [SerializeField] private Block[] _blocks;
    private int _blocksCount;

    public int LevelScore { get; private set; }

    public static LevelProgressHandler Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _blocksCount = _blocks.Length;

        foreach(Block block in _blocks)
        {
            block.OnDestroy += OnBlockDestroy;
        }
    }

    private void OnBlockDestroy(int score)
    {
        LevelScore += score;
        _blocksCount--;

        if (_blocksCount <= 0)
            GameController.Instance.LevelEnd(true);
    }

    private void OnDestroy()
    {
        foreach (Block block in _blocks)
        {
            if(block != null)
                block.OnDestroy -= OnBlockDestroy;
        }
    }
}