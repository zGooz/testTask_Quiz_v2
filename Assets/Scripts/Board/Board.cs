
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;


[RequireComponent(typeof(RectTransform))]

public class Board : MonoBehaviour
{
    public event UnityAction EffectComplite;

    [SerializeField]
    private float _duration = 6f;

    [SerializeField, Min(1)]
    private float _scaleFactor = 1.4f;

    private void Awake()
    {
        var rectangle = GetComponent<RectTransform>();
        var sequence = DOTween.Sequence();

        sequence.Append(rectangle.DOScale(_scaleFactor, _duration))
                .AppendCallback(() => EffectComplite?.Invoke());
    }
}
