
using UnityEngine;
using System.Collections.Generic;
using System.Linq;


[RequireComponent(typeof(RectTransform))]

public class Loader : MonoBehaviour
{
    [SerializeField]
    private Board _board;

    [SerializeField]
    private GameObject _cellPrefab;

    [SerializeField]
    private int _numberOfCellsInRow;

    [SerializeField]
    private LevelsProperty _level;

    private RectTransform _rectangle;
    private List<Content> _content = new List<Content>();
    private List<GameObject> _cells = new List<GameObject>();

    private void Awake()
    {
        _rectangle = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        _board.EffectComplite += LoadLevel;
    }

    private void OnDisable()
    {
        _board.EffectComplite += LoadLevel;
    }

    private void LoadLevel()
    {
        Clear();
        FillContent();
        SpawnContent();
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
        var position = _rectangle.anchoredPosition;

        var cellTransform = _cellPrefab.GetComponent<RectTransform>();
        var width = cellTransform.sizeDelta.x;
        var height = cellTransform.sizeDelta.y;

        var j = 0;
        var k = 0;

        for (var i = 0; i < levelSize; i++)
        {
            var x = position.x + width * j;
            var y = position.y + height * k;
            var cellPosition = new Vector2(x, y);

            j += 1;
            if (j == _numberOfCellsInRow)
            {
                j = 0;
                k += 1;
            }

            var cell = Instantiate(_cellPrefab, _rectangle);
            _cells.Add(cell);
        }
    }
}
