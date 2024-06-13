using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Range(0, 100)] public int _applesRandomChance;
    public int _apples;
    public int _score;
    public int _highScore;

    [Header("UI")]
    [SerializeField] private TMP_Text _applesText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highScoreText;

    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        _highScore = PlayerPrefs.GetInt("Highscore");
        _apples = PlayerPrefs.GetInt("Apples");

        _highScoreText.text = _highScore.ToString();
        _applesText.text = _apples.ToString();
    }

    public void CollectCoin()
    {
        _apples++;
        _applesText.text = _apples.ToString();
    }

    private void Update()
    {
        if (_score < _player.transform.position.z)
        {
            _score = (int)_player.transform.position.z;
        }
        _scoreText.text = _score.ToString();
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Save()
    {
        if (_highScore < _score)
        {
            PlayerPrefs.SetInt("Highscore", _score);
        }

        PlayerPrefs.SetInt("Apples", _apples);
    }
}
