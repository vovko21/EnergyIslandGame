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

    [Header("Systems Setup")]
    [SerializeField] private GameEventManager _gameEventManager;
    [SerializeField] private HintsManager _hintsManager;
    [SerializeField] private BuildingEffects _buildingEffects;
    [SerializeField] private UIEffects _uiEffects;
    [SerializeField] private DailyRewardsUI _dailyRewardsUI;
    [SerializeField] private TasksUI _tasksUI;

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
            InitializeAllLoadedGameData();

            _player.gameObject.SetActive(false);

            _player.transform.position = _spawnPoint.position;

            _player.gameObject.SetActive(true);

            _ui.Show();

            _ui.FadeOut();
        }

        //Start listen events
        _gameEventManager.Initialize();

        _buildingEffects.StartListen();

        _uiEffects.StartListen();
    }

    private IEnumerator FirstLoadCutscene()
    {
        _ui.Hide();

        CameraController.Instance.FollowFirstLoadScene();

        yield return new WaitForSeconds(1);

        _ui.FadeOut();

        _boat.StartWay();

        yield return new WaitForSeconds(6);

        _ui.FadeIn();

        yield return new WaitForSeconds(3);

        CameraController.Instance.FollowPlayer();

        yield return new WaitForSeconds(1);

        _ui.Show();

        _ui.FadeOut();

        yield return new WaitForSeconds(1);

        _hintsManager.StartFirstGameHints();
    }

    private void InitializeAllLoadedGameData()
    {
        //Time must be loaded first !!!
        GameTimeManager.Instance.Initialize(StorageService.Instance.InGameMinutesPassed);

        //Loading progression data
        ProgressionManager.Instance.Initialize();

        //Loading buildings data
        BuildingManager.Instance.Initialize();

        //Loading active tasks
        TaskManager.Instance.Initialize();

        //Loading Daily bonus progress
        _dailyRewardsUI.Initialize(StorageService.Instance.DaysBonusClaimedInRow, StorageService.Instance.LastClaimBonusTime);

        //Loading Taksks time progress
        _tasksUI.Initialize(StorageService.Instance.NextTasksTIme);
    }
}