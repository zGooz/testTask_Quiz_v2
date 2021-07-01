
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Provider))]

public class LevelsProperty : MonoBehaviour
{
    public event UnityAction MovedToNewLevel;

    public IReadOnlyCollection<Content> AnswerBatch => _batch;
    public int CurrentLevel { get; private set; } = 0;
    public bool HasNextLevel => CurrentLevel != _levelSizes.Count - 1;
    public int CurrentLevelSize => _levelSizes[CurrentLevel];

    [SerializeField]
    private Loader _loader;

    [SerializeField]
    private List<int> _levelSizes;

    [SerializeField]
    private Restarter _restarter;

    private Provider _provider;
    private IReadOnlyCollection<Content> _batch;

    private void Start()
    {
        _provider = GetComponent<Provider>();
        SelectButch();
    }

    private void OnEnable()
    {
        _loader.NextLevel += Next;
        _restarter.Reset += Reset;
    }

    private void OnDisable()
    {
        _loader.NextLevel -= Next;
        _restarter.Reset -= Reset;
    }

    private void Reset()
    {
        CurrentLevel = 0;
    }

    private void Next()
    {
        CurrentLevel += 1;
        SelectButch();

        MovedToNewLevel?.Invoke();
    }

    private void SelectButch()
    {
        _batch = _provider.GetRandomBatch();
    }
}
