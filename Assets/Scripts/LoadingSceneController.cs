using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    private static LoadingSceneController instance;
    public static LoadingSceneController Instance { get { return instance; } }

    private bool loadingFlag = false;

    [SerializeField] private GameObject visualGroupObj;
    [SerializeField] private Slider progressBar;
    [SerializeField] private DOTweenAnimation coverAnimation;

    private void Awake()
    {
        if(instance == null)
        { instance = this; }
        else
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        GetComponent<CanvasGroup>().alpha = 1.0f;
        coverAnimation.gameObject.SetActive(false);
        visualGroupObj.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        if (loadingFlag) return;

        StopAllCoroutines();
        StartCoroutine(Cor_LoadNewScene(sceneName));
    }

    public IEnumerator YieldLoadScene(string sceneName)
    {
        if (loadingFlag) yield return null;

        StopAllCoroutines();
        yield return StartCoroutine(Cor_LoadNewScene(sceneName));
    }

    private IEnumerator Cor_LoadNewScene(string SceneName)
    {
        loadingFlag = true;

        coverAnimation.gameObject.SetActive(true);
        coverAnimation.DORestartById("Loader_Close");
        var tw = coverAnimation.GetTweens()[0];
        yield return tw.WaitForCompletion();

        visualGroupObj.SetActive(true);

        // 실제 로딩 처리
        var async = SceneManager.LoadSceneAsync(SceneName);

        while (!async.isDone)
        {
            progressBar.value = Mathf.Lerp(progressBar.value, 1f, async.progress); // 90% 이후 실제 로딩 진행도 반영
            yield return null;
        }

        yield return new WaitForEndOfFrame();

        visualGroupObj.SetActive(false);
        coverAnimation.DORestartById("Loader_Open");
        tw = coverAnimation.GetTweens()[1];
        yield return tw.WaitForCompletion();
        coverAnimation.gameObject.SetActive(false);

        loadingFlag = false;
    }
}

