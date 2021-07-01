
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(RectTransform))]

public class Cell : MonoBehaviour 
{
    [SerializeField, Header("scale")]
    private float _ScaleDuration;

    [SerializeField, Min(1f)]
    private float _scaleFactorOut;

    [SerializeField, Min(0.8f)]
    private float _scaleFactorIn;

    [SerializeField, Header("shake"), Space(10)]
    private float _shakeDuration;

    [SerializeField, Space(10)]
    private float _strength;

    [SerializeField]
    private int _vibrato;

    [SerializeField, Range(0, 180)]
    private float _randomness = 90;

    [SerializeField]
    private GameObject _particle;

    private bool IsCorrect => _value == _loader.Answer.Value;
    private bool IsItClickable { get; set; } = true;

    private Image _image;
    private Button _button;
    private int _value = -1;
    private Loader _loader;
    private RectTransform _rectangle;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _rectangle = GetComponent<RectTransform>();

        //  The "[SerializeField]" attribute does not work with prefabs 
        _loader = GameObject.FindObjectOfType<Loader>();
    }

    private void OnEnable()
    {
        _button.Click += React;
    }

    private void OnDisable()
    {
        _button.Click -= React;
    }

    public void ApplyChanges(Content content)
    {
        _image.sprite = content.icon;
        _value = content.Value;
    }

    private void React()
    {
        if (!IsItClickable)
        {
            return;
        }

        IsItClickable = false;
        var sequence = DOTween.Sequence();

        if (IsCorrect)
        {
            sequence.Append(_rectangle.DOScale(_scaleFactorOut, _ScaleDuration))
                    .AppendInterval(1f)
                    .Join(_rectangle.DOScale(_scaleFactorIn, _ScaleDuration))
                    .AppendInterval(1f)
                    .AppendCallback(() => _loader.ReactToChoosingCorrectAnswer());

            Instantiate(_particle, _rectangle);
        }
        else
        {
            sequence.Append(_rectangle.DOShakeAnchorPos(_shakeDuration, _strength, _vibrato, _randomness, false, false))
                    .AppendCallback(() => IsItClickable = true);
        }
    }

}
