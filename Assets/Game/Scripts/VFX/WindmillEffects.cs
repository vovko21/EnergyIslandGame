using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillEffects : MonoBehaviour
{
    [Header("Main controol")]
    [SerializeField] private ProductionBuilding _productionBuilding;

    [Header("Animation controol")]
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        _productionBuilding.OnStatusChanged += OnStatusChanged;
    }

    private void OnDisable()
    {
        _productionBuilding.OnStatusChanged -= OnStatusChanged;
    }

    private void OnStatusChanged(BuildingStatus status)
    {
        if(status == BuildingStatus.Producing)
        {
            StartCoroutine(TrunOnTurbine());
        }
        else
        {
            StartCoroutine(TrunOffTurbine());
        }
    }

    private IEnumerator TrunOnTurbine()
    {
        _animator.SetFloat("RotationSpeed", 0.01f);

        yield return new WaitForSeconds(0.1f);

        _animator.SetFloat("RotationSpeed", 0.1f);

        yield return new WaitForSeconds(0.2f);

        _animator.SetFloat("RotationSpeed", 0.5f);

        yield return new WaitForSeconds(0.4f);

        _animator.SetFloat("RotationSpeed", 1f);

        yield return new WaitForSeconds(0.5f);

        _animator.SetFloat("RotationSpeed", 2f);
    }

    private IEnumerator TrunOffTurbine()
    {
        yield return new WaitForSeconds(0.1f);

        _animator.SetFloat("RotationSpeed", 2f);

        yield return new WaitForSeconds(0.2f);

        _animator.SetFloat("RotationSpeed", 1f);

        yield return new WaitForSeconds(0.4f);

        _animator.SetFloat("RotationSpeed", 0.5f);

        yield return new WaitForSeconds(0.5f);

        _animator.SetFloat("RotationSpeed", 0.0f);         
    }
}