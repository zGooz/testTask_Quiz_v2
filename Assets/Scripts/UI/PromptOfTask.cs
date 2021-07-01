
using TMPro;
using DG.Tweening;
using UnityEngine;


[RequireComponent(typeof(TextMeshProUGUI))]

public class PromptOfTask : MonoBehaviour
{
    [SerializeField]
    private float _duration = 3f;

    [SerializeField]
    private Loader _loader;

    private TextMeshProUGUI _field;

    private void Awake()
    {
        _field = GetComponent<TextMeshProUGUI>();

        var sequence = DOTween.Sequence();
        sequence.Append(_field.DOFade(1f, _duration));
    }

    private void OnEnable()
    {
        _loader.LoadComplete += SetText;
    }

    private void OnDisable()
    {
        _loader.LoadComplete -= SetText;
    }

    private void SetText()
    {
        _field.text = "Find " + _loader.Answer.sought;
    }
}
