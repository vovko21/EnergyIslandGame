using UnityEngine;

public class Wiggle : MonoBehaviour
{
    [SerializeField] private bool _usePosition;
    [SerializeField] private bool _useRotation;
    [SerializeField] private float _positionSpeed;
    [SerializeField] private float _rotaionSpeed;

    private float _time;

    private void FixedUpdate()
    {
        if (_usePosition)
        {
            _time += Time.fixedDeltaTime * _positionSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Sin(_time), transform.localPosition.z);
        }

        if (_useRotation)
        {
            transform.Rotate(new Vector3(0, Time.fixedDeltaTime * _rotaionSpeed, 0), Space.Self);
        }
    }
}
