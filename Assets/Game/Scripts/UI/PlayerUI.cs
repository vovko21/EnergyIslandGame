using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private Camera _camera;
    private bool _isTextActive;

    private void Start()
    {
        _player.Hands.OnStackChanged += OnCarryChange;

        _camera = Camera.main;

        _textMeshPro.gameObject.SetActive(false);

        _textMeshPro.transform.LookAt(_camera.transform, Vector3.up);
    }

    private void LateUpdate()
    {
        if(!_isTextActive)
        {
            return;
        }

        _textMeshPro.transform.LookAt(_camera.transform, Vector3.up);
    }

    private void OnCarryChange(CarrySystem carrySystem)
    {
        if(carrySystem.IsMax)
        {
            _textMeshPro.gameObject.SetActive(true);

            _isTextActive = true;
        }
        else
        {
            _textMeshPro.gameObject.SetActive(false);

            _isTextActive = false;
        }
    }
}
