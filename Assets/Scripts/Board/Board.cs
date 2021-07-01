
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;


[RequireComponent(typeof(RectTransform))]

public class Board : MonoBehaviour
{
    public event UnityAction EffectComplite;

    [SerializeField]
    private float _duration;

    private void Awake()
    {
        var rectangle = GetComponent<RectTransform>();
        var sequence = DOTween.Sequence();

        sequence.Append(rectangle.DOScale(1, _duration))
                .AppendCallback(() => EffectComplite?.Invoke());
    }
}
