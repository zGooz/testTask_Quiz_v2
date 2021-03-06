
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;


[RequireComponent(typeof(RectTransform))]

public class Loader : MonoBehaviour
{
    public event UnityAction LoadComplete;
    public event UnityAction NextLevel;

    public Content Answer { get; private set; }

    [SerializeField]
    private Board _board;

    [SerializeField]
    private GameObject _cellPrefab;

    [SerializeField]
    private Button _restartButton;

    [SerializeField]
    private Restarter _restarter;

    [SerializeField]
    private int _numberOfCellsInRow;

    [SerializeField]
    private LevelsProperty _level;

    private RectTransform _rectangle;
    private List<Content> _content = new List<Content>();
    private List<GameObject> _cells = new List<GameObject>();

    private const float CELL_WIDTH = 2f;
    private const float CELL_HEIGHT = 1.8f;

    private void Awake()
    {
        _rectangle = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        _board.EffectComplite += LoadLevel;
        _level.MovedToNewLevel += LoadLevel;
        _restarter.Restart += LoadLevel;
    }

    private void OnDisable()
    {
        _board.EffectComplite -= LoadLevel;
        _level.MovedToNewLevel -= LoadLevel;
        _restarter.Restart -= LoadLevel;
    }

    public void ReactToChoosingCorrectAnswer()
    {
        if (_level.HasNextLevel)
        {
            NextLevel?.Invoke();
        }
        else
        {
            Clear();
            _restartButton.gameObject.SetActive(true);
        }
    }

    private void LoadLevel()
    {
        Clear();
        FillContent();
        SpawnContent();
        FillCellsWithData();

        Answer = ChooseCorrectAnswer();

        LoadComplete?.Invoke();
    }

    private void Clear()
    {
        foreach (var item in _cells)
        {
            Destroy(item);
        }

        _cells.Clear();
        _content.Clear();
    }

    private void FillContent()
    {
        var batch = _level.AnswerBatch.ToArray();
        var answerCount = batch.Length;
        var levelSize = _level.CurrentLevelSize;
        var copyes = new List<int>();

        for (var i = 0; i < levelSize; i++)
        {
            var index = Random.Range(0, answerCount);

            while (copyes.Contains(index))
            {
                index = Random.Range(0, answerCount);
            }

            var item = batch[index];
            _content.Add(item);
            copyes.Add(index);
        }
    }

    private void SpawnContent()
    {
        var levelSize = _level.CurrentLevelSize;
        var j = 0;
        var k = 0;

        for (var i = 0; i < levelSize; i++)
        {
            var cellPos = new Vector2(CELL_WIDTH * (j - 1), - CELL_HEIGHT * (k - 0.5f));
            var cell = Instantiate(_cellPrefab, cellPos, Quaternion.identity, _rectangle);
            var cellTransform = _cellPrefab.GetComponent<RectTransform>();

            cellTransform.localScale = new Vector2(1, 1);
            _cells.Add(cell);

            j += 1;
            if (j == _numberOfCellsInRow)
            {
                j = 0;
                k += 1;
            }
        }
    }

    private void FillCellsWithData()
    {
        for (var i = 0; i < _content.Count; i++)
        {
            var data = _content[i];
            var cell = _cells[i].GetComponent<Cell>();

            data.Value = i;
            cell.ApplyChanges(data);
        }
    }

    private Content ChooseCorrectAnswer()
    {
        var index = Random.Range(0, _content.Count);
        return _content[index];
    }
}
