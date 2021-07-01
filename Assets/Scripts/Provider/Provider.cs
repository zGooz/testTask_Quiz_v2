
using System;
using UnityEngine;
using System.Collections.Generic;


public class Provider : MonoBehaviour
{
    private List < List<Content> > _batchs = new List < List<Content> >();

    // FOR EXAMPLE :
    //      [SerializeField, Header("Animals"), Space(10)]
    //      private List<Content> _animals;

    [SerializeField, Header("Number"), Space(10)]
    private List<Content> _numbers;

    [SerializeField, Header("Letters"), Space(10)]
    private List<Content> _letters;

    private void Awake()
    {
        Init();
    }

    public IReadOnlyCollection<Content> GetRandomBatch()
    {
        var index = UnityEngine.Random.Range(0, _batchs.Count);
        var batch = _batchs[index];

        return batch;
    }

    private void Init()
    {
        _batchs.Add(_numbers);
        _batchs.Add(_letters);
    }
}

[Serializable]
public class Content
{
    public Sprite icon;
    public string sought;

    public int Value { get; set; }
}
