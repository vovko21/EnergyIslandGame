using System.Collections;
using UnityEngine;

public class AdTimerUI : MonoBehaviour
{
    [SerializeField] private ProgressBarView _progressBar;
    [SerializeField] private float _showInTime;

    public float ShowInTime => _showInTime;

    public void Initialize()
    {
        _progressBar.SetMaxValue(_showInTime);
        StartCoroutine(StartTimer());
    }

    public void Deinitialize()
    {
        StopAllCoroutines();
    }

    private IEnumerator StartTimer()
    {
        float timeElapsed = 0;
        while (true)
        {
            timeElapsed += Time.deltaTime;

            _progressBar.SetValue(timeElapsed);
            _progressBar.SetDataText($"{timeElapsed.ToString("0")}/{_showInTime}");

            if (timeElapsed > _showInTime)
            {
                break;
            }

            yield return null;
        }
    }
}
