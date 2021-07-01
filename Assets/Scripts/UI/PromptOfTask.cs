
using TMPro;
using DG.Tweening;
using UnityEngine;


[RequireComponent(typeof(TextMeshProUGUI))]

public class PromptOfTask : MonoBehaviour
{
    [SerializeField]
    private float _duration;

    private TextMeshProUGUI _field;

    private void Awake()
    {
        _field = GetComponent<TextMeshProUGUI>();

        var sequence = DOTween.Sequence();
        sequence.Append(_field.DOFade(1f, _duration));
    }
}
