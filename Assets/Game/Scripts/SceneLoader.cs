using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private LoadSceneMode _loadMode;
    [SerializeField] private Slider _loadingSlider;
    [SerializeField] private string _sceneNameToLoad;
    [SerializeField] private GameObject _ui;

    private void Start()
    {
        StartCoroutine(LoadSceneAsync(_sceneNameToLoad));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, _loadMode);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            _loadingSlider.value = progressValue;
            yield return null;
        }

        loadOperation.allowSceneActivation = true;

        _ui.SetActive(false);
    }
}
