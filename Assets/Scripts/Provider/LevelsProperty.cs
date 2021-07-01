
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Provider))]

public class LevelsProperty : MonoBehaviour
{
    private IReadOnlyCollection<Content> Batch => _batch;

    [SerializeField]
    private List<int> _levelSizes;

    private Provider _provider;
    private IReadOnlyCollection<Content> _batch;

    private void Awake()
    {
        _provider = GetComponent<Provider>();
        _batch = _provider.GetRandomBatch();
    }
}
