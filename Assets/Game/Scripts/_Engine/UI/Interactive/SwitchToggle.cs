using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class SwitchToggle : MonoBehaviour
{
    [SerializeField] private RectTransform _uiHandle;
    [SerializeField] private Color _handleActiveColor;

    private Toggle _toggle;
    private Vector2 _handlePosition;
    private Color _handleDefaultColor;
    private Image _handleImage;

    public event Action<bool> OnToggleChanged;
    public bool IsOn => _toggle.isOn;

    void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _handleImage = _uiHandle.GetComponent<Image>();

        _handlePosition = _uiHandle.anchoredPosition;
        _handleDefaultColor = _handleImage.color;

        if (_toggle.isOn) OnSwitch(true);
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnSwitch);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnSwitch);
    }

    private void OnSwitch(bool isOn)
    {
        //_uiHandle.anchoredPosition = isOn ? _handlePosition * -1 : _handlePosition;
        //_handleImage.color = isOn ? _handleActiveColor : _handleDefaultColor;
        _uiHandle.DOAnchorPos(isOn ? _handlePosition * -1 : _handlePosition, 0.4f).SetEase(Ease.InOutBack);
        _handleImage.DOColor(isOn ? _handleActiveColor : _handleDefaultColor, 0.6f);

        OnToggleChanged?.Invoke(isOn);
    }
}