using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class RunnerGameManager : MonoBehaviour
{

    [Inject] private ObstacleGenerator _obstacleGenerator;
    [Inject] private FollowEnemyCreator _followEnemyCreator;
    [Inject] private RangeEnemyCreator _rangeEnemyCreator;
    [SerializeField] private List<RunnerState>_gameEvents;
    [SerializeField] Slider _slider;
    [SerializeField] private GameObject _tutorialPopup;
    [SerializeField] private Image _back;
    [SerializeField] private float _waitTime;
    private int _index = 0;
    private void Start()
    {
        _tutorialPopup.transform.localScale = Vector3.zero;
        FadeIn();
        StartGameState();
    }
    public void StartGameState()
    {
        _slider.value = (float)_index/_gameEvents.Count;

        if( _index >= _gameEvents.Count ){
            Debug.Log("End Game");
            StartCoroutine(EndGameEvent());
            return;
        }
        RunnerState gameEvent = _gameEvents[_index];

        switch (gameEvent)
        {
            case RunnerState.Wait:
                StartCoroutine(WaitEvent());
                break;
            case RunnerState.Obstacles:
                StartCoroutine(ObstacleEvent());
                break;
            case RunnerState.FollowEnemies:
                StartCoroutine(FollowEnemiesEvent());
                break;
            case RunnerState.RangeEnemies:
                StartCoroutine(RangeEnemiesEvent());
                break;
            case RunnerState.Lose:
                StartCoroutine(LoseEvent());
                break;
            case RunnerState.Boss:
                StartCoroutine(BossEvent());
                break;
            case RunnerState.Start:
                StartCoroutine(StartEvent());
                break;

        }
    }



    public void EndEvent()
    {
        _index++;
        StartGameState();

    }

        public void ShowPopup()
    {
        _tutorialPopup.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack)
        .OnComplete(() => Time.timeScale = 0);
    }

    public void HidePopup()
    {
        _tutorialPopup.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
    }
    private IEnumerator StartEvent()
    {
        
        ShowPopup();
        EndEvent();
        yield return null;
    }
    private IEnumerator EndGameEvent()
    {
        Debug.Log("LEvel finished");
        FadeOut();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        yield break;
    }
    private IEnumerator WaitEvent()
    {
        yield return new WaitForSeconds(_waitTime);
        Debug.Log("Chill for seconds");

        EndEvent();
    }

    private IEnumerator ObstacleEvent()
    {
        int count = Random.Range(5, 7);
        for(int i = 0; i < count; i++)
        {
            _obstacleGenerator.SpawnObject();
            yield return new WaitForSeconds(1f);
        }
        EndEvent();
    }
    private IEnumerator FollowEnemiesEvent()
    {
        int count = Random.Range(2, 4);
        _followEnemyCreator.Create(count);
        
        yield break;
    }
    private IEnumerator RangeEnemiesEvent()
    {
        int count = Random.Range(2, 3);
        _rangeEnemyCreator.Create(count);

        yield break;
    }
    private void FadeOut()
    {
        var newColor =_back.color;
        newColor.a = 0f;
        _back.color = newColor;
        _back.DOFade(1f, 1f);
    }
    private void FadeIn()
    {
        var newColor =_back.color;
        newColor.a = 1f;
        _back.color = newColor;
        _back.DOFade(0, 1f);
    }
    
    public IEnumerator LoseEvent()
    {
        FadeOut();
        yield return new WaitForSeconds(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield break;
    }
    private IEnumerator BossEvent()
    {
        yield break;

    }

}

public enum RunnerState
{
    Wait = 0,
    Obstacles = 1,
    FollowEnemies = 2,
    RangeEnemies = 3,
    EndGame = 4,
    Boss = 5,
    Lose = 6,
    Start,
}
