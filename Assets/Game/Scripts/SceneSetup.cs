using System.Collections;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    [Header("First Load Setup")]
    [SerializeField] private UserInterface _ui;
    [SerializeField] private MoveByPoints _boat;

    private async void Start()
    {
        await StorageService.Instance.LoadDataAsync();

        if (!StorageService.Instance.Initialized)
        {
            StartCoroutine(FirstLoadCutscene());
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
