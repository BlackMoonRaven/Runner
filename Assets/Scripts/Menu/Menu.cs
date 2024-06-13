using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public int _currentSkin;

    public int _apples;

    [SerializeField] private TMP_Text _applesText;

    [SerializeField] private GameObject _shopPanel;

    [SerializeField] private Button[] _buttons;
    [SerializeField] private TMP_Text[] _skinNames;
    [SerializeField] private TMP_Text[] _skinPrices;
    [SerializeField] private Image[] _image;
    [SerializeField] private Image[] _activeSkin;
    [SerializeField] private GameObject[] _info;

    [SerializeField] private Skin[] _skinCollection;

    private void Awake()
    {
        _apples = PlayerPrefs.GetInt("Apples");
        Load();
    }

    private void Start()
    {
        Refresh();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void ShopActive(bool state)
    {
        _shopPanel.SetActive(state);

        if (state)
        {
            Load();
        }
        else
        {
            Save();
        }

        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _skinNames[i].text = _skinCollection[i]._name;
            _skinPrices[i].text = _skinCollection[i]._price.ToString();
            _image[i].sprite = _skinCollection[i]._icon;

            if (_skinCollection[i]._have == 1 || _apples >= _skinCollection[i]._price)
            {
                _buttons[i].interactable = true;

                if (_skinCollection[i]._have == 1 && i != _currentSkin)
                {
                    _activeSkin[i].color = Color.yellow;
                    _info[i].SetActive(false);
                }
                else if (_skinCollection[i]._have == 1 && i == _currentSkin)
                {
                    _activeSkin[i].color = Color.green;
                    _info[i].SetActive(false);
                }
                else //if (_skinCollection[i]._have != 1 && _apples >= _skinCollection[i]._price)
                {
                    _activeSkin[i].color = Color.white;
                    _info[i].SetActive(true);
                }
            }
            else //if(_skinCollection[i]._have != 1 && _apples < _skinCollection[i]._price)
            {
                _buttons[i].interactable = false;
                _activeSkin[i].enabled = false;
                _info[i].SetActive(true);
            }
        }

        _applesText.text = _apples.ToString();
    }

    public void SelectSkin(int _index)
    {
        if (_skinCollection[_index]._have == 1)
        {
            _currentSkin = _index;
        }
        else
        {
            _apples -= _skinCollection[_index]._price;
            _skinCollection[_index]._have = 1;
            _currentSkin = _index;
        }

        PlayerPrefs.SetInt("CurrentSkin", _currentSkin);

        Refresh();
    } 

    private void Load()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _skinCollection[i]._have = PlayerPrefs.GetInt("SkinHave" + i);
        }

        _currentSkin = PlayerPrefs.GetInt("CurrentSkin");
    }

    private void Save()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            PlayerPrefs.SetInt("SkinHave" + i, _skinCollection[i]._have);
        }

        PlayerPrefs.SetInt("Apples", _apples);
    }
}

[System.Serializable]
public class Skin
{
    public string _name;
    public Sprite _icon;
    public int _price;
    public int _have;
}
