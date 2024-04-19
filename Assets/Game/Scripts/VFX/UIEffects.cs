using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEffects : MonoBehaviour
{
    [Header("Setup settings")]
    [SerializeField] private GameObject _dollarsPrefab;
    [SerializeField] private GameObject _diamandsPrefab;
    [SerializeField] private Transform _endDollarsPosition;
    [SerializeField] private Transform _endDiamondsPosition;

    [Header("Move settings")]
    [SerializeField] private float _duration;

    [Header("Spawn settings")]
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;

    private List<GameObject> _dollars = new List<GameObject>();
    private List<GameObject> _diamands = new List<GameObject>();

    private bool _isCoroutineActive = false;

    public void StartListen()
    {
        ProgressionManager.Instance.Wallet.OnDollarsChanged += Wallet_OnDollarsChanged;
        ProgressionManager.Instance.Wallet.OnDiamandsChanged += Wallet_OnDiamandsChanged;
    }

    private void Wallet_OnDollarsChanged(int amount)
    {
        if(amount > 0 && !_isCoroutineActive)
        {
            StartCoroutine(CollectEffectCouroutine(amount, _dollars, _dollarsPrefab, _endDollarsPosition));
        }
    }

    private void Wallet_OnDiamandsChanged(int amount)
    {
        if (amount > 0 && !_isCoroutineActive)
        {
            StartCoroutine(CollectEffectCouroutine(amount, _diamands, _diamandsPrefab, _endDiamondsPosition));
        }
    }

    private IEnumerator CollectEffectCouroutine(int amount, List<GameObject> prefabsList, GameObject prefab, Transform endPosition)
    {
        _isCoroutineActive = true;

        if (prefabsList.Count != 0) prefabsList.Clear();

        var prefabsCount = CalculatePrefabCount(amount);

        for (int i = 0; i < prefabsCount; i++)
        {
            var spawnedPrefab = Instantiate(prefab, transform);
            float xPosition = _spawnPosition.position.x + Random.Range(_minX, _maxX);
            float yPosition = _spawnPosition.position.y + Random.Range(_minY, _maxY);
            spawnedPrefab.transform.position = new Vector3(xPosition, yPosition);
            spawnedPrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            spawnedPrefab.transform.DOPunchPosition(new Vector3(0, 30, 0), Random.Range(0, 1f)).SetEase(Ease.InBack);
            prefabsList.Add(spawnedPrefab);
            yield return new WaitForSeconds(0.015f);
        }

        foreach (var prefabItem in prefabsList)
        {
            prefabItem.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f).SetEase(Ease.InSine);
            yield return new WaitForSeconds(0.05f);
        }

        foreach (var prefabItem in prefabsList)
        {
            prefabItem.transform.DOMove(endPosition.position, _duration).SetEase(Ease.InBack);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(_duration);

        foreach (var item in prefabsList)
        {
            Destroy(item);
        }

        prefabsList.Clear();

        _isCoroutineActive = false;
    }

    private int CalculatePrefabCount(int amount)
    {
        return Mathf.Clamp(amount / 10, 1, 10);
    }
}