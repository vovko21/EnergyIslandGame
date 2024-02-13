using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private Text _fpsText;

    private int _ticksPerSecond = 4;
    private float _time = 0;

    void Update()
    {
        float unscaledDeltaTime = Time.unscaledDeltaTime;
        float duration = 1f / _ticksPerSecond;
        _time += unscaledDeltaTime;
        while (_time > duration)
        {
            _time -= duration;
            _fpsText.text = "FPS " + (int)(1f / unscaledDeltaTime);
        }
    }
}
