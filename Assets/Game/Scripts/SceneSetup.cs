using System.Collections;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    [Header("First Load Setup")]
    [SerializeField] private UserInterface _ui;
    [SerializeField] private MoveByPoints _boat;
    [SerializeField] private int _startMoney;

    [Header("Player Setup")]
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _spawnPoint;

    private async void Start()
    {
        await StorageService.Instance.LoadDataAsync();

        if (!StorageService.Instance.Initialized)
        {
            ProgressionManager.Instance.Wallet.AddDollars(_startMoney);

            StartCoroutine(FirstLoadCutscene());
        }
        else
        {
            ProgressionManager.Instance.InitializeData();

            _player.gameObject.SetActive(false);

            _player.transform.position = _spawnPoint.position;

            _player.gameObject.SetActive(true);

            _ui.ShowUI();

            _ui.FadeOut();
        }
    }

    private IEnumerator FirstLoadCutscene()
    {
        _ui.HideUI();

        CameraController.Instance.FollowFirstLoadScene();

        yield return new WaitForSeconds(1);

        _ui.FadeOut();

        _boat.StartWay();

        yield return new WaitForSeconds(12);

        _ui.FadeIn();

        yield return new WaitForSeconds(3);

        CameraController.Instance.FollowPlayer();

        yield return new WaitForSeconds(1);

        _ui.ShowUI();

        _ui.FadeOut();
    }
}
