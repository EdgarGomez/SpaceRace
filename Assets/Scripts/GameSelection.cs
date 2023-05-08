using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSelection : MonoBehaviour
{

    private bool isAbout = false;
    public GameObject Buttons;
    public GameObject AboutText;

    public TMP_Dropdown player1ShipDropdown;
    public TMP_Dropdown player2ShipDropdown;
    public TMP_Dropdown difficultyDropdown;
    public TMP_Dropdown playTimeDropdown;

    private void Start()
    {
        player1ShipDropdown.value = PlayerPrefs.HasKey("Player1Ship") ? PlayerPrefs.GetInt("Player1Ship") : 0;
        player2ShipDropdown.value = PlayerPrefs.HasKey("Player2Ship") ? PlayerPrefs.GetInt("Player2Ship") : 0;
        difficultyDropdown.value = PlayerPrefs.HasKey("DifficultyLevel") ? PlayerPrefs.GetInt("DifficultyLevel") : 0;
        playTimeDropdown.value = PlayerPrefs.HasKey("PlayTime") ? PlayerPrefs.GetInt("PlayTime") : 0;
    }


    public void player1Ship(int mode)
    {
        PlayerPrefs.SetInt("Player1Ship", mode);
    }

    public void player2Ship(int mode)
    {
        PlayerPrefs.SetInt("Player2Ship", mode);
    }

    public void difficultyLevel(int mode)
    {
        PlayerPrefs.SetInt("DifficultyLevel", mode);
    }

    public void playTime(int mode)
    {
        PlayerPrefs.SetInt("PlayTime", mode);
    }

    public void StartGame()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("SpaceRace");
    }

    public void IsAbout()
    {
        isAbout = !isAbout;
        if (isAbout)
        {
            Buttons.SetActive(false);
            AboutText.SetActive(true);
        }
        else
        {
            Buttons.SetActive(true);
            AboutText.SetActive(false);
        }
    }
}

