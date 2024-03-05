using System.Collections;
using UnityEngine;

public class ProgressAreaUI : InteractionAreaUI
{
    [Header("Progress settings")]
    [SerializeField] private GameObject _progressBackground;
    [SerializeField] private float _startPosition;
    [SerializeField] private float _endPosition;

    private ProgressArea _interactionProgressArea;
    private float _currentPosition;
    private float _progress01;

    protected override void Initialize()
    {
        base.Initialize();

        _interactionProgressArea = (ProgressArea)_interactionArea;
        _currentPosition = _startPosition;
    }

    protected override void OnCharacterTrigger(bool inside)
    {
        base.OnCharacterTrigger(inside);

        if (inside)
        {
            StartCoroutine(ProgressCoroutineAnimation());
        }
        else
        {
            StopAllCoroutines();
            _progress01 = 0;
            _currentPosition = _startPosition;
            _progressBackground.transform.localPosition = CurrentPosition();
        }
    }

    protected IEnumerator ProgressCoroutineAnimation()
    {
        bool finished = false;
        while (!finished)
        {
            yield return null;

            var timeLeft = _interactionProgressArea.TimeLeft;

            _progress01 = (1f - (((float)timeLeft) / ((float)_interactionProgressArea.TimeToProgress)));

            _currentPosition = _progress01 * _endPosition;

            _progressBackground.transform.localPosition = CurrentPosition();

            if (timeLeft <= 0)
            {
                finished = true;
                _currentPosition = _startPosition;
                _progressBackground.transform.localPosition = CurrentPosition();
            }
        }
    }

    private Vector3 CurrentPosition()
    {
        return new Vector3(0, _currentPosition - 1, 0);
    }
}
