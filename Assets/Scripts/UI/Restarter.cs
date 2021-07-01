
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]

public class Restarter : MonoBehaviour
{
    public event UnityAction Reset;
    public event UnityAction Restart;

    [SerializeField]
    private float _duration;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private Loader _loader;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.Click += RunLoadScreen;
    }

    private void OnDisable()
    {
        _button.Click -= RunLoadScreen;
    }

    private void RunLoadScreen()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(_image.DOFade(1, _duration))
                .AppendInterval(1f)
                .AppendCallback(() => Reset?.Invoke())
                .AppendInterval(1f)
                .Join(_image.DOFade(0, _duration))
                .AppendCallback(() => Restart?.Invoke())
                .AppendCallback(() => gameObject.SetActive(false));
    }
}
