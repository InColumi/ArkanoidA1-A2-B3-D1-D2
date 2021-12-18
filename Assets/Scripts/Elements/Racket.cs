using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Racket : MonoBehaviour
{
    private const int _maxLevel = 30;
    [Range(1, _maxLevel)]

    [SerializeField] private int _level = 1;
    [SerializeField] private float _ballVelocityMult = 0.02f;

    [SerializeField] private GameObject _bluePrefab;
    [SerializeField] private GameObject _redPrefab;
    [SerializeField] private GameObject _greenPrefab;
    [SerializeField] private GameObject _yellowPrefab;
    [SerializeField] private GameObject _ballPrefab;

    [SerializeField] private Weapon _gun;
    [SerializeField] private Weapon _cannon;

    [SerializeField] private GameDataScript _gameData;
    [SerializeField] private static bool s_gameStarted = false;
    [SerializeField] private AudioClip _pointSound;
    private AudioSource _audioSrc;

    private static Collider2D[] s_colliders = new Collider2D[50];
    private static ContactFilter2D s_contactFilter = new ContactFilter2D();

    private static List<Ball> _balls;

    public int RequiredPointsToBall { get { return 400 + (_level - 1) * 20; } }

    private void Start()
    {
        _balls = new List<Ball>();
        _audioSrc = GameObject.FindGameObjectWithTag("Audio Sourse").GetComponent<AudioSource>();
        SetMusic();
        _level = _gameData.GetLevel();
        //Cursor.visible = false;

        if (!s_gameStarted)
        {
            s_gameStarted = true;
            if (_gameData.isResetOnStart())
            {
                _gameData.Load();
            }
        }
        _level = _gameData.GetLevel();
        StartLevel();
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            Vector3 mousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 position = transform.position;
            if (mousPos.x >= -52 && mousPos.x <= 52)
            {
                position.x = mousPos.x;
                transform.position = position;
            }
        }

        if (Input.GetButtonDown("Pause"))
        {
            Time.timeScale = Time.timeScale > 0 ? 0 : 1;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            _gameData.Reset();
            SceneManager.LoadScene("MainScene");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            _gameData.ChangeMusic();
            SetMusic();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _gameData.ChangeSound();
        }
    }

    private void CreateBlocks(GameObject prefab, float xMax, float yMax, int count, int maxCount)
    {
        count = count > maxCount ? maxCount : count;
        for (int i = 0; i < count; i++)
        {
            for (int k = 0; k < 20; k++)
            {
                var obj = Instantiate(prefab, new Vector3((Random.value * 2 - 1) * xMax, Random.value * yMax, 0), Quaternion.identity);
                if (obj.GetComponent<Collider2D>().OverlapCollider(s_contactFilter.NoFilter(), s_colliders) == 0)
                    break;
                Destroy(obj);
            }
        }
    }

    public void CreateBalls(int count, bool addForse = false)
    {
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(_ballPrefab);
            var ball = obj.GetComponent<Ball>();
            ball.InitialForce += new Vector2(10 * i, 0);
            ball.InitialForce *= 1 + _level * _ballVelocityMult;
            if (addForse)
            {
                var rb = ball.GetComponent<Rigidbody2D>();
                rb.isKinematic = false;
                rb.AddForce(ball.InitialForce);
            }
            _balls.Add(ball);
        }
    }

    private void CreateBalls()
    {
        int count = _gameData.GetBalls();
        if (_gameData.GetBalls() > 0)
        {
            CreateBalls(1);
        }
    }

    #region Background
    //void SetBackground() 
    //{
    //    var bg = GameObject.Find("Background").GetComponent<SpriteRenderer>();
    //    Debug.Log($"Images background/{(_level % 2).ToString("d2")}");
    //    bg.sprite = Resources.Load<Sprite>($"Images background/{_level % 2:d2}");
    //}
    #endregion

    private void StartLevel()
    {
        //SetBackground(); // not work
        var yMax = Camera.main.orthographicSize * 0.8f;
        var xMax = Camera.main.orthographicSize * Camera.main.aspect * 0.85f;
        CreateBlocks(_bluePrefab, xMax, yMax, _level, 8);
        CreateBlocks(_redPrefab, xMax, yMax, 1 + _level, 10);
        CreateBlocks(_greenPrefab, xMax, yMax, 1 + _level, 12);
        CreateBlocks(_yellowPrefab, xMax, yMax, 2 + _level, 15);
        CreateBalls();
    }

    public void BallDestroyed(Ball ball)
    {
        _balls.Remove(ball);
        _gameData.ReduceBolls();
        StartCoroutine(BallDestroyedCoroutine());
    }

    private IEnumerator BallDestroyedCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        if (_balls.Count == 0)
        {
            if (_gameData.GetBalls() > 0)
            {
                CreateBalls();
            }
            else
            {
                _gameData.Reset();
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    private IEnumerator BlockDestroyedCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        if (GameObject.FindGameObjectsWithTag("Block").Length == 0)
        {
            if (_level < _maxLevel)
            {
                _gameData.IncreaseLevel();
            }
            SceneManager.LoadScene("MainScene");
        }
    }

    public void AddPointsFromBlock(int points)
    {
        _gameData.AddPoints(points);
        if (_gameData.IsEnebledSound())
        {
            _audioSrc.PlayOneShot(_pointSound, 5);
        }

        _gameData.AddPointsForBonusBall(points);
        if (_gameData.GetPontsForBonusBall() >= RequiredPointsToBall)
        {
            _gameData.IncreaseBalls();
            _gameData.ReducePointsForBonusBall(RequiredPointsToBall);
        }

        StartCoroutine(BlockDestroyedCoroutine());
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 2, Screen.width, 100),
        string.Format(
        "<color=white>" +
        "<size=30>Level <b>{0}</b> Balls <b>{1}</b>" +
        " Score <b>{2}</b>" +
        "</size>" +
        "</color>", _gameData.GetLevel(), _gameData.GetBalls(), _gameData.GetPoints()));

        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.UpperRight;
        GUI.Label(new Rect(0, 14, Screen.width - 50, 100),
        string.Format(

        "<color=yellow><size=25><color=white>Space</color>-pause {0}" +
        " <color=white>N</color>-new" +
        " <color=white>J</color>-jump" +
        " <color=white>M</color>-music {1}" +
        " <color=white>S</color>-sound {2}" +
        " <color=white>Esc</color>-exit</size></color>",
        OnOff(Time.timeScale > 0), OnOff(_gameData.IsEnebledMusic() == false),
        OnOff(_gameData.IsEnebledSound() == false)), style);
    }

    private string OnOff(bool boolVal)
    {
        return boolVal ? "on" : "off";
    }

    private void SetMusic()
    {
        if (_gameData.IsEnebledMusic())
        {
            _audioSrc.Play();
        }
        else
        {
            _audioSrc.Stop();
        }
    }

    private void OnApplicationQuit()
    {
        _gameData.Save();
    }

    public void ActivateGun()
    {
        if (_cannon.gameObject.activeInHierarchy)
        {
            _cannon.gameObject.SetActive(false);
        }
        _gun.gameObject.SetActive(true);
    }

    public void ActivateCannon()
    {
        if (_gun.gameObject.activeInHierarchy)
        {
            _gun.gameObject.SetActive(false);
        }
        _cannon.gameObject.SetActive(true);
    }

    public void CancelWeapon()
    {
        _cannon.gameObject.SetActive(false);
        _gun.gameObject.SetActive(false);
    }

    public GameDataScript GetGameData()
    {
        return _gameData;
    }

    public void DoSlowBalls(float value)
    {
        if(value > 0)
        {
            value = 1.0f - value;
            foreach (var ball in _balls)
            {
                ball.InitialForce *= value;
            }
        }
    }

    public void DoFastBalls(float value)
    {
        if (value > 0)
        {
            foreach (var ball in _balls)
            {
                ball.InitialForce += ball.InitialForce * value;
            }
        }
    }
}
