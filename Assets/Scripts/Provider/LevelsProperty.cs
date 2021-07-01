
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Provider))]

public class LevelsProperty : MonoBehaviour
{
    public IReadOnlyCollection<Content> AnswerBatch => _batch;
    public int CurrentLevel { get; private set; } = 0;
    public bool HasNextLevel => CurrentLevel != _levelSizes.Count;
    public int CurrentLevelSize => _levelSizes[CurrentLevel];

    [SerializeField]
    private List<int> _levelSizes;

    private Provider _provider;
    private IReadOnlyCollection<Content> _batch;

    private void Start()
    {
        _provider = GetComponent<Provider>();
        _batch = _provider.GetRandomBatch();
    }
}
