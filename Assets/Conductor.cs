using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.InputSystem;
using Cinemachine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class Conductor : MonoBehaviour
{
    public AudioSource musicSource;
    [Inject] GameInputAction _input;
    private InputAction _spaceAction;

    public List<LevelData> levels;
    public Vector3 initPos;
    public float speed;
    public GameObject canvas;
    public CinemachineVirtualCamera vCam;
    public GameObject targetGroup;
    public GameObject contestants;
    Vector3 newPosition;
    public float spb;
    public GameObject note;
    public GameObject bar;
    List<GameObject> notes;


    public int curBeat;
    public float curTime;
    public int curLevel;
    public List<float> beatTimes;
    public float balance;
    public GameObject tryAgainText;

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
        _spaceAction = _input.PlayerInput.Rhythm;
        _spaceAction.performed += SpaceAction_performed;
        _spaceAction.Enable();

        vCam.Priority = 100;
        vCam.Follow = null;
        vCam.LookAt = targetGroup.transform;
        newPosition = contestants.transform.position;

        notes = new List<GameObject>();
        beatTimes = new List<float>();
        bar.SetActive(true);
        StartCoroutine(Go());
        balance = 0;

    }
    IEnumerator Go()
    {
        yield return new WaitForSeconds(spb * 2);
        for (int i = 0; i < levels.Count; ++i)
        {
            curLevel = i;
            for (int j = 0; j < notes.Count; ++j)
            {
                Destroy(notes[j]);
            }
            notes.Clear();
            beatTimes.Clear();
            curTime = 0;
            curBeat = 0;
            bar.GetComponent<RectTransform>().anchoredPosition = new Vector3(-900, -220, 0);
            bar.GetComponent<RectTransform>().DOAnchorPos(new Vector3(300, -220, 0), 8 * spb).SetEase(Ease.Linear); ;
            StartCoroutine(Display(levels[i].level));
            yield return StartCoroutine(PlayFirst(levels[i].level));
            yield return new WaitForSeconds(spb * 4);
            //playMode = false;
            for (int j = curBeat; j < beatTimes.Count; ++j)
            {
                notes[j].transform.DOScale(new Vector3(0.5f, -0.5f, 0.5f), spb * 0.25f);
                balance -= 0.5f;
            }
            if (balance < -4 || balance <= 0 && i == levels.Count - 1)
            {
                yield return StartCoroutine(GameOver());
                balance = 0;
                i = -1;
            }
            if (balance >= 4) {
            }
            yield return new WaitForSeconds(spb * 2);

        }
        SceneManager.LoadScene("Village Main");
    }

    IEnumerator GameOver()
    {
        tryAgainText.transform.DOScale(new Vector3(1, 1, 1), 1f).SetLoops(2, LoopType.Yoyo);
        yield return new WaitForSeconds(1f);
    }


    IEnumerator Display(List <float> level)
    {
        Vector3 pos = new Vector3(-300, -220, 0);
        float t = 4*spb;
        for (int i = 0; i < level.Count; ++i)
        {
            GameObject newNote = Instantiate(note, canvas.transform);
            notes.Add(newNote);
            beatTimes.Add(t);
            newNote.GetComponent<RectTransform>().anchoredPosition = pos;
            pos += new Vector3(level[i] * 600/4f, 0, 0);
            t += level[i] * spb;
            yield return null;
        }
    }
    IEnumerator PlayFirst(List <float> level)
    {
        for (int i = 0; i < level.Count; ++i)
        {
            musicSource.time = 0f;
            //musicSource.pitch = pentatonics[Random.Range(0, 5)];
            musicSource.Play();
            yield return new WaitForSeconds(spb*level[i]);
        }
    }

    IEnumerator PlaySecond(List<float> level)
    {
        for (int i = 0; i < level.Count; ++i)
        {
            musicSource.time = 0f;
            musicSource.Play();
            yield return new WaitForSeconds(spb * level[i]);
        }
    }
    private void Update()
    {
           curTime += Time.deltaTime;
        newPosition = new Vector3(-4 + balance, 0.61f, 0);
        contestants.transform.position += new Vector3(Mathf.Min(5f, newPosition.x-contestants.transform.position.x)*Time.deltaTime,0, 0);
    }

    public float distTime(float b)
    {
        return Mathf.Abs(curTime - b);
    }

    private void SpaceAction_performed(InputAction.CallbackContext obj)
    {
        musicSource.Play();
        if (curBeat >= beatTimes.Count)
        {
            balance -= 1f;
            return;
        }
        while (curBeat+1 < beatTimes.Count && (distTime(beatTimes[curBeat]) > distTime(beatTimes[curBeat + 1]))) {
            notes[curBeat].transform.DOScale(new Vector3(0.5f, -0.5f, 0.5f), spb * 0.25f);
            curBeat += 1;
            balance -= 0.5f;
        }
        float x = distTime(beatTimes[curBeat]);
        float z = beatTimes[curBeat];
        Debug.Log($"Delta: {x}, Beat {curBeat}, {curTime}, {z}");
        if (distTime(beatTimes[curBeat]) <= 0.2f)
        {
            notes[curBeat].transform.DOScale(new Vector3(0,0,0), spb * 0.25f);
            if (balance < 4)
                balance += 0.3f;
            //balance = Mathf.Min(4, balance);
            curBeat += 1;
        }
        else
        {
            balance -= 0.5f;
        }
       
    }

}
[System.Serializable]
public class LevelData
{
    public List<float> level;
    
    public float this[int key]
    {
        get
        {
            return level[key];
        }
        set
        {
            level[key] = value;
        }
    }
//

}