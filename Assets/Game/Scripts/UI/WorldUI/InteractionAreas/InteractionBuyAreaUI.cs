using System.Collections;
using TMPro;
using UnityEngine;

public class InteractionBuyAreaUI : InteractionAreaUI
{
    [Header("Progress settings")]
    [SerializeField] private GameObject _progressBackground;
    [SerializeField] private TextMeshProUGUI _dataText;
    [SerializeField] private float _startPosition;
    [SerializeField] private float _endPosition;

    private BuyArea _interactionBuyArea;
    private float _currentPosition;
    private float _progress01;

    protected override void Initialize()
    {
        base.Initialize();

        _interactionBuyArea = (BuyArea)_interactionArea;
        _currentPosition = _startPosition;
        _dataText.text = _interactionBuyArea.ValueLeft.ToString();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _interactionBuyArea.OnBought += OnBought;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _interactionBuyArea.OnBought -= OnBought;
    }

    protected override void OnCharacterTrigger(bool inside)
    {
        base.OnCharacterTrigger(inside);

        if (inside)
        {
            StartCoroutine(ProgressCoroutineAnimation());
        }
    }

    private IEnumerator ProgressCoroutineAnimation()
    {
        bool finished = false;
        while (!finished)
        {
            yield return new WaitForSeconds(_interactionBuyArea.SpendRate);

            var valueLeft = _interactionBuyArea.ValueLeft;

            _progress01 = (1f - (((float)valueLeft) / ((float)_interactionBuyArea.ValueToSpend)));

            _dataText.text = valueLeft.ToString();

            _currentPosition = _progress01 * _endPosition;

            if (valueLeft == 0)
            {
                finished = true;
            }

            _progressBackground.transform.localPosition = CurrentPosition();
        }
    }

    private void OnBought()
    {
        _currentPosition = _startPosition;
        _dataText.text = _interactionBuyArea.ValueToSpend.ToString();
        _progressBackground.transform.localPosition = CurrentPosition();
    }

    private Vector3 CurrentPosition()
    {
        return new Vector3(0, _currentPosition - 1, 0);
    }
}