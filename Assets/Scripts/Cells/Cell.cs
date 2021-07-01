
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]

public class Cell : MonoBehaviour
{
    private Image _image;
    private Button _button;
    private string _prompt;
    private int _value = -1;
    private Loader _loader;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();

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
        _prompt = content.sought;
        _value = content.Value;
    }

    private void React()
    {
        if (_value == _loader.Answer.Value)
        {
            _loader.ReactToChoosingCorrectAnswer();
        }
    }

}
