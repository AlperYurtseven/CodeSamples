using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public LevelBuilder m_LevelBuilder;

    public Camera meyn_camera;
    public GameObject m_NextButton;
    public Button mainMenuButton;
    public bool m_ReadyForInput;
    private Player m_Player;

    public GameObject pause_menu_Panel;

    public GameObject sfx_text;
    public GameObject music_text;
    public GameObject sfx_slider;

    public GameObject music_slider;

    public GameObject main_menu_button;

    public Image background_image;
    public Sprite background_image0;
    public Color egytptian_color;
    public Sprite background_image1;

    public Color greek_color;
    public Sprite background_image2;

    public Color space_color;
    public Button Reset_button;

    public AudioSource background_music;
    public AudioClip background_music0;
    public AudioClip background_music1;
    public AudioClip background_music2;

    public AudioSource victory_sound;
    public AudioClip victory_sound0;
    public AudioClip victory_sound1;
    public AudioClip victory_sound2;

    public int number;

    public bool victory_sound_played = false;
    public bool camera_set = false;

    public Text level_number;

    public EventSystem event_sys;

    void Awake()
    {

        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
    }
    void Start()
    {
        

        sfx_slider.GetComponent<Slider>().value = SaveManager.GetFloat("sfx_volume");
        music_slider.GetComponent<Slider>().value = SaveManager.GetFloat("music_volume");

        background_music.volume = SaveManager.GetFloat("music_volume");

        victory_sound.volume = SaveManager.GetFloat("sfx_volume");

        if(SaveManager.GetInt("level_type") == 0){
            background_music.clip = background_music0;
            victory_sound.clip = victory_sound0;
        }

        else if(SaveManager.GetInt("level_type") == 1){        
            background_music.clip = background_music1;
            victory_sound.clip = victory_sound1;
        }

        else if(SaveManager.GetInt("level_type") == 2){
            background_music.clip = background_music2;
            victory_sound.clip = victory_sound2;

        }

        if(SaveManager.HasKey("bg_music_time")){
            background_music.time = SaveManager.GetFloat("bg_music_time");
            SaveManager.DeleteKey("bg_music_time");
        }
        background_music.Play();
        
        pause_menu_Panel.SetActive(false);
        sfx_text.SetActive(false);
        music_text.SetActive(false);
        sfx_slider.SetActive(false);
        music_slider.SetActive(false);
        main_menu_button.SetActive(false);

        m_NextButton.SetActive(false);
     
        if (!SaveManager.HasKey("last_Level"))
        {
            SaveManager.SetInt("last_Level", 0);
            SaveManager.Save();
        }
        ResetScene();

        
    }

    public void Pause_Menu(){

        m_ReadyForInput = false;
        //Time.timeScale = 0.01f;
        pause_menu_Panel.SetActive(true);
        sfx_text.SetActive(true);
        music_text.SetActive(true);
        sfx_slider.SetActive(true);
        music_slider.SetActive(true);
        main_menu_button.SetActive(true);

    }

    public void Unpause(){

        m_ReadyForInput = true;
        //Time.timeScale = 1;
        pause_menu_Panel.SetActive(false);
        sfx_text.SetActive(false);
        music_text.SetActive(false);
        sfx_slider.SetActive(false);
        music_slider.SetActive(false);
        main_menu_button.SetActive(false);

    }

    public void MainMenuButton()
    {
        
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
           
        }

        m_ReadyForInput = true;

    }

    void Update()
    {

        //Debug.Log(m_ReadyForInput);

        int temp_level_number = m_LevelBuilder.m_CurrentLevel +1;
        string temp_level_num = "Level " + temp_level_number.ToString();
        level_number.text = temp_level_num;

        background_music.volume = SaveManager.GetFloat("music_volume");

        victory_sound.volume = SaveManager.GetFloat("sfx_volume");

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        moveInput.Normalize();

        if(m_LevelBuilder.m_CurrentLevel == 0){
            meyn_camera.orthographicSize = 5;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 1){
            meyn_camera.orthographicSize = 3;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 2){
            meyn_camera.orthographicSize = 4;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 3){
            meyn_camera.orthographicSize = 4;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 4){
            meyn_camera.orthographicSize = 4;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 5){
            meyn_camera.orthographicSize = 4;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 6){
            meyn_camera.orthographicSize = 5;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 7){
            meyn_camera.orthographicSize = 5;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 8){
            meyn_camera.orthographicSize = 4;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 9){
            meyn_camera.orthographicSize = 5;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 10){
            meyn_camera.orthographicSize = 6;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 11){
            meyn_camera.orthographicSize = 7;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 12){
            meyn_camera.orthographicSize = 6;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 13){
            meyn_camera.orthographicSize = 7;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 14){
            meyn_camera.orthographicSize = 7;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 15){
            meyn_camera.orthographicSize = 7;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 16){
            meyn_camera.orthographicSize = 7;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 17){
            meyn_camera.orthographicSize = 8;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 18){
            meyn_camera.orthographicSize = 7;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 19){
            meyn_camera.orthographicSize = 5;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 20){
            meyn_camera.orthographicSize = 7;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 21){
            meyn_camera.orthographicSize = 6;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 22){
            meyn_camera.orthographicSize = 6;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 23){
            meyn_camera.orthographicSize = 6;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 24){
            meyn_camera.orthographicSize = 6;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 25){
            meyn_camera.orthographicSize = 7;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 26){
            meyn_camera.orthographicSize = 5;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 27){
            meyn_camera.orthographicSize = 5;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 28){
            meyn_camera.orthographicSize = 5;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 29){
            meyn_camera.orthographicSize = 5;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 30){
            meyn_camera.orthographicSize = 5;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 31){
            meyn_camera.orthographicSize = 5;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 32){
            meyn_camera.orthographicSize = 7;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 33){
            meyn_camera.orthographicSize = 5;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 34){
            meyn_camera.orthographicSize = 5;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 35){
            meyn_camera.orthographicSize = 5;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 36){
            meyn_camera.orthographicSize = 5;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 37){
            meyn_camera.orthographicSize = 5;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 38){
            meyn_camera.orthographicSize = 8;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 39){
            meyn_camera.orthographicSize = 7;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 40){
            meyn_camera.orthographicSize = 9;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 41){
            meyn_camera.orthographicSize = 9;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 42){
            meyn_camera.orthographicSize = 9;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 43){
            meyn_camera.orthographicSize = 9;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 44){
            meyn_camera.orthographicSize = 8;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 45){
            meyn_camera.orthographicSize = 7;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 46){
            meyn_camera.orthographicSize = 8;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 47){
            meyn_camera.orthographicSize = 9;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 48){
            meyn_camera.orthographicSize = 8;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 49){
            meyn_camera.orthographicSize = 9;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 50){
            meyn_camera.orthographicSize = 10;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 51){
            meyn_camera.orthographicSize = 10;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 52){
            meyn_camera.orthographicSize = 9;
            camera_set = true;
        }
        if(m_LevelBuilder.m_CurrentLevel == 53){
            meyn_camera.orthographicSize = 8;
            camera_set = true;
        }


       if(IsLevelComplete()){
           m_ReadyForInput =false;
       }
       else{
           m_ReadyForInput = true;
       }
        
       /*
        if (moveInput.sqrMagnitude > 0.5)
        {
            if (m_ReadyForInput)
            {
                m_ReadyForInput = false;
                m_Player.Move(moveInput);
                m_NextButton.SetActive(IsLevelComplete());
                if(IsLevelComplete() && !victory_sound_played){
                    victory_sound.Play();
                    //Debug.Log("anan");
                }
            }
        }
        
        else
        {
            m_ReadyForInput = true;
        }*/
        
    }
   
    public void NextLevel()
    {

        if(m_LevelBuilder.m_CurrentLevel == 17){
            background_music.clip = background_music1;
            SaveManager.SetInt("level_type",1);
            SaveManager.Save();
            background_music.Play();
        }

        if(m_LevelBuilder.m_CurrentLevel == 35){
            background_music.clip = background_music2;
            SaveManager.SetInt("level_type",2);
            SaveManager.Save();
            background_music.Play();
        }

        victory_sound_played = false;
        
        if(m_LevelBuilder.m_CurrentLevel == 53){
            SceneManager.LoadScene("GameOver");
        }
        else{

            SaveManager.SetInt("last_Level", m_LevelBuilder.m_CurrentLevel + 1);
            SaveManager.Save();
            m_NextButton.SetActive(false);
            camera_set = false;
            m_LevelBuilder.NextLevel();
            //m_LevelBuilder.Build();
            StartCoroutine(ResetSceneAsync());
        }
      

    }

    public void ResetScene()
    {
        StartCoroutine(ResetSceneAsync());       
    }

    public void ReloadScene()
    {

        int curr_lvl = SaveManager.GetInt("curr_level");
        SaveManager.SetInt("wanted_Level",curr_lvl);
        SaveManager.Save();
        StartCoroutine(ResetSceneAsync());

    }

    public bool IsLevelComplete()
    {
        bool boxesOnCross = true;
        bool twoPushBoxesOnCross = true;
        bool upToCollisionBoxesOnCross = true;
        bool sevenpushboxesOnCross = true;

        Box[] boxes = FindObjectsOfType<Box>();
        TwoPushBox[] twopushboxes = FindObjectsOfType<TwoPushBox>();
        UptoCollisionBox[] uptocollisionboxes = FindObjectsOfType<UptoCollisionBox>();
        SevenPushBox[] sevenpushboxes = FindObjectsOfType<SevenPushBox>();
        foreach (var box in boxes)
        {
            if (!box.m_OnCross){
                boxesOnCross = false;
            } 
            
        }

        foreach (var twopushbox in twopushboxes)
        {
            if (!twopushbox.m_OnCross){
                twoPushBoxesOnCross = false;
            } 
            
        }

        foreach (var uptocollisionbox in uptocollisionboxes)
        {
            if (!uptocollisionbox.m_OnCross){
                upToCollisionBoxesOnCross = false;
            } 
            
        }

        foreach (var sevenpushbox in sevenpushboxes)
        {
            if (!sevenpushbox.m_OnCross){
                sevenpushboxesOnCross = false;
            } 
            
        }

        if(boxesOnCross && twoPushBoxesOnCross && upToCollisionBoxesOnCross && sevenpushboxesOnCross){
            return true;
        }
        return false;
    }

    public void Reset_button_func(){

        SaveManager.SetFloat("bg_music_time", background_music.time);
        SaveManager.Save();
        //SaveManager.SetInt("resetted",1);
        SceneManager.LoadScene("MainScene");


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

        int curr_lvl = m_LevelBuilder.m_CurrentLevel;

        //Debug.Log("curr_level = " + curr_lvl);

        if(curr_lvl < 18){

            background_image.sprite = background_image0;
            meyn_camera.backgroundColor = egytptian_color;
                    
        }

        else if(curr_lvl > 17 && curr_lvl < 36){

            background_image.sprite = background_image1;
            meyn_camera.backgroundColor = greek_color;
                 
        }

        else if(curr_lvl > 35){

            background_image.sprite = background_image2;
            meyn_camera.backgroundColor = space_color;
                    
        }
        //Debug.Log("Level loaded.");
    }
    
}
