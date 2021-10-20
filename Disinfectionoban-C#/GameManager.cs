using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public LevelBuilder m_LevelBuilder;
    public GameObject m_NextButton;
    public GameObject m_UpButton;
    public GameObject m_DownButton;
    public GameObject m_RightButton;
    public GameObject m_LeftButton;
    public Button mainMenuButton;
    private bool m_ReadyForInput;
    private Player m_Player;
    public Text moveCounter;
    public AudioSource correctPositionSound;
    public AudioSource backgroundMusic;
    public Button Reset_button;
    
    void Start()
    {
        backgroundMusic.time = PlayerPrefs.GetFloat("bg_music_time");
        backgroundMusic.Play();
        m_NextButton.SetActive(false);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainScene"));

        backgroundMusic.volume = PlayerPrefs.GetFloat("volume");
        if (!PlayerPrefs.HasKey("last_Level"))
        {
            PlayerPrefs.SetInt("last_Level", 0);
        }

        ResetScene();
        //m_LevelBuilder.Build();
        //m_Player = FindObjectOfType<Player>();

        if (PlayerPrefs.GetInt("language") == 1)
        {
            mainMenuButton.GetComponentInChildren<Text>().text = "Main Menu";
            Reset_button.GetComponentInChildren<Text>().text = "Reset";
            m_NextButton.GetComponentInChildren<Text>().text = "Next";

        }
        else if (PlayerPrefs.GetInt("language") == 2)
        {
            mainMenuButton.GetComponentInChildren<Text>().text = "Ana Menü";
            Reset_button.GetComponentInChildren<Text>().text = "Sıfırla";
            m_NextButton.GetComponentInChildren<Text>().text = "Sonraki";
        }
        else if (PlayerPrefs.GetInt("language") == 3)
        {
            mainMenuButton.GetComponentInChildren<Text>().text = "Menu Principal";
            Reset_button.GetComponentInChildren<Text>().text = "Réinitialiser";
            m_NextButton.GetComponentInChildren<Text>().text = "Suivant";
        }

    }

    public void MainMenuButton()
    {
        PlayerPrefs.SetFloat("bg_music_time", backgroundMusic.time);
        SceneManager.LoadScene("MainMenu");
    }

    public void MoveUp()
    {
        Vector2 upInput;
        upInput.x = 0;
        upInput.y = 1;

        if (m_ReadyForInput)
        {
            m_ReadyForInput = false;
            m_Player.Move(upInput);
            m_NextButton.SetActive(IsLevelComplete());
            PlayerPrefs.SetInt("moveCount", PlayerPrefs.GetInt("moveCount") + 1);
            if (IsLevelComplete() && !PlayerPrefs.HasKey("VictorySound"))
            {
                correctPositionSound.volume = PlayerPrefs.GetFloat("volume");
                correctPositionSound.Play();
                PlayerPrefs.SetInt("VictorySound", 1);
            }
        }

        m_ReadyForInput = true;
        
    }

    public void MoveRight()
    {
        Vector2 rightInput;
        rightInput.x = 1;
        rightInput.y = 0;

        if (m_ReadyForInput)
        {
            m_ReadyForInput = false;
            m_Player.Move(rightInput);
            m_NextButton.SetActive(IsLevelComplete());
            PlayerPrefs.SetInt("moveCount", PlayerPrefs.GetInt("moveCount") + 1);
            if (IsLevelComplete() && !PlayerPrefs.HasKey("VictorySound"))
            {
                correctPositionSound.volume = PlayerPrefs.GetFloat("volume");
                correctPositionSound.Play();
                PlayerPrefs.SetInt("VictorySound", 1);
            }
        }

        m_ReadyForInput = true;

    }

    public void MoveLeft()
    {
        Vector2 leftInput;
        leftInput.x = -1;
        leftInput.y = 0;

        if (m_ReadyForInput)
        {
            m_ReadyForInput = false;
            m_Player.Move(leftInput);
            m_NextButton.SetActive(IsLevelComplete());
            PlayerPrefs.SetInt("moveCount", PlayerPrefs.GetInt("moveCount") + 1);
            if (IsLevelComplete() && !PlayerPrefs.HasKey("VictorySound"))
            {
                correctPositionSound.volume = PlayerPrefs.GetFloat("volume");
                correctPositionSound.Play();
                PlayerPrefs.SetInt("VictorySound", 1);
            }
        }

        m_ReadyForInput = true;

    }

    public void MoveDown()
    {
        Vector2 downInput;
        downInput.x = 0;
        downInput.y = -1;

        if (m_ReadyForInput)
        {
            m_ReadyForInput = false;
            m_Player.Move(downInput);
            m_NextButton.SetActive(IsLevelComplete());
            PlayerPrefs.SetInt("moveCount", PlayerPrefs.GetInt("moveCount") + 1);
            if (IsLevelComplete() && !PlayerPrefs.HasKey("VictorySound"))
            {
                correctPositionSound.volume = PlayerPrefs.GetFloat("volume");
                correctPositionSound.Play();
                PlayerPrefs.SetInt("VictorySound", 1);
            }
        }

        m_ReadyForInput = true;

    }

    void Update()
    {

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        moveInput.Normalize();

        moveCounter.text = PlayerPrefs.GetInt("moveCount").ToString();

        if (moveInput.sqrMagnitude > 0.5)
        {
            if (m_ReadyForInput)
            {
                m_ReadyForInput = false;
                m_Player.Move(moveInput);
                m_NextButton.SetActive(IsLevelComplete());
                if (IsLevelComplete() && !PlayerPrefs.HasKey("VictorySound"))
                {
                    correctPositionSound.volume = PlayerPrefs.GetFloat("volume");
                    correctPositionSound.Play();
                    PlayerPrefs.SetInt("VictorySound", 1);
                }
            }
        }
        
        else
        {
            m_ReadyForInput = true;
        }
        
    }

   
    public void NextLevel()
    {

        PlayerPrefs.DeleteKey("VictorySound");
        PlayerPrefs.SetInt("last_Level", PlayerPrefs.GetInt("last_Level") + 1);
        m_NextButton.SetActive(false);
        m_LevelBuilder.NextLevel();
        //m_LevelBuilder.Build();
        StartCoroutine(ResetSceneAsync());

    }

    public void ResetScene()
    {
        StartCoroutine(ResetSceneAsync());
    }

    bool IsLevelComplete()
    {
        Box[] boxes = FindObjectsOfType<Box>();
        foreach (var box in boxes)
        {
            if (!box.m_OnCross) return false;
        }
        return true;
    }

    
    IEnumerator ResetSceneAsync()
    {
        if (SceneManager.sceneCount > 1)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("LevelScene");
            while (!asyncUnload.isDone)
            {
                yield return null;
                //Debug.Log("Unloading");
            }
            //Debug.Log("Unloading done.");
            Resources.UnloadUnusedAssets();
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelScene", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
            //Debug.Log("Loading");
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene"));
        m_LevelBuilder.Build();
        m_Player = FindObjectOfType<Player>();
        //Debug.Log("Level loaded.");
    }
    
}
