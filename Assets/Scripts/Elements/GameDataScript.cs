using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game Data", order = 51)]
public class GameDataScript : ScriptableObject
{
    [SerializeField] private int _level = 1;
    [SerializeField] private int _countBalls = 6;
    [SerializeField] private int _points = 0;
    [SerializeField] private int _pointsForBonusBall = 0;
    [SerializeField] private bool _isResetOnStart;
    [SerializeField] private bool _music = true;
    [SerializeField] private bool _sound = true;

    public void Load()
    {
        _level = PlayerPrefs.GetInt("level", 1);
        _countBalls = PlayerPrefs.GetInt("countBalls", 6);
        _points = PlayerPrefs.GetInt("points", 0);
        _pointsForBonusBall = PlayerPrefs.GetInt("pointsForBonusBall", 0);
        _music = PlayerPrefs.GetInt("music", 1) == 1;
        _sound = PlayerPrefs.GetInt("sound", 1) == 1;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("level", _level);
        PlayerPrefs.SetInt("countBalls", _countBalls);
        PlayerPrefs.SetInt("points", _points);
        PlayerPrefs.SetInt("pointsForBonusBall", _pointsForBonusBall);
        PlayerPrefs.SetInt("music", _music ? 1 : 0);
        PlayerPrefs.SetInt("sound", _sound ? 1 : 0);
    }


    public void AddPoints(int points)
    {
        if (points > 0)
        {
            _points += points;
        }
    }

    public void AddPointsForBonusBall(int points)
    {
        if (points > 0)
        {
            _points += points;
            _pointsForBonusBall += points;
        }
    }

    public void ReducePointsForBonusBall(int value)
    {
        if (value > 0)
        {
            _pointsForBonusBall -= value;
        }
    }

    public void IncreaseBalls()
    {
        ++_countBalls;
    }

    public int GetPontsForBonusBall()
    {
        return _pointsForBonusBall;
    }

    public bool IsEnebledMusic()
    {
        return _music;
    }
    public bool IsEnebledSound()
    {
        return _sound;
    }

    public void ChangeMusic()
    {
        _music = !_music;
    }

    public void ChangeSound()
    {
        _sound = !_sound;
    }

    public void ReduceBolls()
    {
        --_countBalls;
    }

    public void IncreaseLevel()
    {
        ++_level;
    }

    public void Reset()
    {
        _level = 1;
        _countBalls = 2;
        _points = 0;
        _pointsForBonusBall = 0;
    }

    public int GetLevel()
    {
        return _level;
    }

    public int GetPoints()
    {
        return _points;
    }
    public int GetBalls()
    {
        return _countBalls;
    }

    public bool isResetOnStart()
    {
        return _isResetOnStart;
    }
}