using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
#if UNITY_SWITCH
     using nn.hid;
#endif

namespace ageofsokoban

{

    public class PlayerSwitchControls : MonoBehaviour
    {

        public bool A_interactable;
        public bool B_interactable;
        public bool Y_interactable;
        public bool X_interactable;
        public bool L_interactable;
        public bool R_interactable;
        public bool Plus_interactable;

        public bool pause_menu_active;

        public Slider sfx_control;

        public Slider music_control;

        public ColorBlock select_colour;
        
        public ColorBlock unselect_colour;
        public int j;

        public bool moveable;
        public float time;

#if UNITY_SWITCH
    private NpadId npadId = NpadId.Invalid;
    private NpadStyle npadStyle = NpadStyle.Invalid;
    private NpadState npadState = new NpadState();
#endif

        void Start()
        {
            //player = GameManager.GetComponent<Player>();

            A_interactable = false;
            B_interactable = false;
            Y_interactable = false;
            X_interactable = false;
            L_interactable = false;
            R_interactable = false;
            Plus_interactable = false;

            
            pause_menu_active = false;
            j = 0;
            moveable = true;

            time = 0f;

#if UNITY_SWITCH
        Npad.Initialize();
        Npad.SetSupportedIdType(new NpadId[] { NpadId.Handheld, NpadId.No1 });
        Npad.SetSupportedStyleSet(NpadStyle.FullKey | NpadStyle.Handheld | NpadStyle.JoyDual);
#endif
        }

        void Update()
        {
            time = time + Time.deltaTime;

            if(time > 0.3f){
                A_interactable = true;
                B_interactable = true;
                Y_interactable = true;
                X_interactable = true;
                L_interactable = true;
                R_interactable = true;
                Plus_interactable = true;
            }
            //Debug.LogError("Time: "+time);

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                
                    if(pause_menu_active){
                        if(j > 0){
                            j--;
                        }
                        color_changer(j);
                        time = 0f;
                    }
                    else{
                        GameManager.instance.MoveUp();
                        time = 0f;    
                    }
                
               //time.delta time
               
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                
                    if(pause_menu_active){
                        if(j < 2){
                            j++;
                        }
                        color_changer(j);
                        time = 0f;
                    }
                    else{
                        GameManager.instance.MoveDown();
                        time = 0f;
                    }
                
               
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                
                    if(pause_menu_active){
                        if(j == 0){
                            sfx_control.value -= 0.1f; 
                            SaveManager.SetFloat("sfx_volume",sfx_control.value);
                            SaveManager.Save();
                            time = 0f;
                            //GameManager.instance.victory_sound.volume = sfx_control.value;
                        }
                        if(j == 1){
                            music_control.value -= 0.1f;
                            SaveManager.SetFloat("music_volume",music_control.value);
                            SaveManager.Save();
                            time = 0f;
                            //GameManager.instance.background_music.volume = music_control.value;

                        }
                    }
                    else{
                        GameManager.instance.MoveLeft();
                        time = 0f;
                    }
                
                
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                
                    if(pause_menu_active){
                        if(j == 0){
                            sfx_control.value += 0.1f; 
                            SaveManager.SetFloat("sfx_volume",sfx_control.value);
                            SaveManager.Save();
                            time = 0f;
                            //GameManager.instance.victory_sound.volume = sfx_control.value;
                        }
                        if(j == 1){
                            music_control.value += 0.1f;
                            SaveManager.SetFloat("music_volume",music_control.value);
                            SaveManager.Save();
                            //GameManager.instance.background_music.volume = music_control.value;
                            time = 0f;
                        }
                    }
                    else{
                        GameManager.instance.MoveRight();
                        time = 0f;
                    }
                
                
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                
                if(pause_menu_active){
                        if (j == 2){
                            GameManager.instance.MainMenuButton();
                        }

                    }
                    else{
                        if(GameManager.instance.IsLevelComplete())
                        {
                            GameManager.instance.NextLevel();
                        }

                        //is_A_up = false;
                    }
                
            }

            if (Input.GetKeyDown(KeyCode.U)){
                GameManager.instance.NextLevel();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                
                if(pause_menu_active){
                        GameManager.instance.Unpause();
                        SaveManager.SetFloat("sfx_volume", sfx_control.value);
                        SaveManager.SetFloat("music_volume", music_control.value);
                        SaveManager.Save();
                        pause_menu_active = false;
                        //is_B_up = false;
                }
                else{
                    GameManager.instance.MainMenuButton();
                        //is_B_up = false;
                }
                    
                
            }

            //Resets the scene (Level)
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameManager.instance.Reset_button_func();
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                moveable = true;
                GameManager.instance.Pause_Menu();
                pause_menu_active = true;
                j = 0;
                color_changer(j);
                
            }
            

#if UNITY_SWITCH
        if (UpdatePadState())
        {
            AnalogStickState lStick = npadState.analogStickL;
            AnalogStickState rStick = npadState.analogStickR;

            if (Mathf.Abs(lStick.x) < 10000 && Mathf.Abs(lStick.y) < 10000)
            {
                    time = 100;
            }

            //Movements w/ analog stick, (checks both if the input is bigger than 10000 threshold && bigger than other axis)
            if(lStick.y > 10000 && Mathf.Abs(lStick.y) > Mathf.Abs(lStick.x))
            {
                if(time > 0.4f){
                    if(pause_menu_active){
                        if(j > 0){
                            j--;
                        }
                        color_changer(j);
                        time = 0f;
                    }
                    else{
                           
                        GameManager.instance.MoveUp();
                        time = 0f;    
                    }
                }
                
            }

            else if(lStick.y < -10000 && Mathf.Abs(lStick.y) > Mathf.Abs(lStick.x))
            {
                if(time > 0.4f){
                    if(pause_menu_active){
                        if(j < 2){
                            j++;
                        }
                        color_changer(j);
                        time = 0f;
                    }
                    else{
                        GameManager.instance.MoveDown();
                        time = 0f;
                    }
                }
                
            }
                else if(lStick.x > 10000 && Mathf.Abs(lStick.x) > Mathf.Abs(lStick.y))
            {
                if(time > 0.4f){
                    if(pause_menu_active){
                        if(j == 0){
                            sfx_control.value += 0.1f; 
                            SaveManager.SetFloat("sfx_volume",sfx_control.value);
                            SaveManager.Save();
                            time = 0f;
                            //GameManager.instance.victory_sound.volume = sfx_control.value;
                        }
                        if(j == 1){
                            music_control.value += 0.1f;
                            SaveManager.SetFloat("music_volume",music_control.value);
                            SaveManager.Save();
                            //GameManager.instance.background_music.volume = music_control.value;
                            time = 0f;
                        }
                    }
                    else{
                        GameManager.instance.MoveRight();
                        time = 0f;
                    }
                }
                
            }
                else if(lStick.x < -10000 && Mathf.Abs(lStick.x) > Mathf.Abs(lStick.y))
            {
                if(time > 0.4f){
                    if(pause_menu_active){
                        if(j == 0){
                            sfx_control.value -= 0.1f; 
                            SaveManager.SetFloat("sfx_volume",sfx_control.value);
                            SaveManager.Save();
                            time = 0f;
                            //GameManager.instance.victory_sound.volume = sfx_control.value;
                        }
                        if(j == 1){
                            music_control.value -= 0.1f;
                            SaveManager.SetFloat("music_volume",music_control.value);
                            SaveManager.Save();
                            time = 0f;
                            //GameManager.instance.background_music.volume = music_control.value;

                        }
                    }
                    else{
                        GameManager.instance.MoveLeft();
                        time = 0f;
                    }
                }
                
            }
            
            if (npadState.GetButtonDown(NpadButton.Up))
            {
                if(pause_menu_active){
                    if(j > 0){
                        j--;
                    }
                    color_changer(j);
                }
                else{
                    GameManager.instance.MoveUp(); 
                }
                

            }
            

            if (npadState.GetButtonDown(NpadButton.Down))
            {
                if(pause_menu_active){
                    if(j < 2){
                        j++;
                    }
                    color_changer(j);
                }
                else{
                    GameManager.instance.MoveDown();
                }
                
            }

            if (npadState.GetButtonDown(NpadButton.Left))
            {
                if(pause_menu_active){
                    if(j == 0){
                        sfx_control.value -= 0.1f; 
                        SaveManager.SetFloat("sfx_volume",sfx_control.value);
                        SaveManager.Save();
                        //GameManager.instance.victory_sound.volume = sfx_control.value;
                    }
                    if(j == 1){
                        music_control.value -= 0.1f;
                        SaveManager.SetFloat("music_volume",music_control.value);
                        SaveManager.Save();
                        //GameManager.instance.background_music.volume = music_control.value;

                    }
                }
                else{
                    GameManager.instance.MoveLeft();
                }
                
            }

            if (npadState.GetButtonDown(NpadButton.Right))
            {

                if(pause_menu_active){
                    if(j == 0){
                        sfx_control.value += 0.1f; 
                        SaveManager.SetFloat("sfx_volume",sfx_control.value);
                        SaveManager.Save();
                        //GameManager.instance.victory_sound.volume = sfx_control.value;
                    }
                    if(j == 1){
                        music_control.value += 0.1f;
                        SaveManager.SetFloat("music_volume",music_control.value);
                        SaveManager.Save();
                        //GameManager.instance.background_music.volume = music_control.value;
                        
                        
                    }
                }
                else{
                    GameManager.instance.MoveRight();
                }
               

            }

            if (npadState.GetButtonDown(NpadButton.A))
            {

                if(A_interactable){
                    A_interactable = false;
                    if(pause_menu_active){
                        if (j == 2){
                            GameManager.instance.MainMenuButton();
                        }

                    }
                    else{
                        if(GameManager.instance.IsLevelComplete())
                        {
                            GameManager.instance.NextLevel();
                        }
                    }
                    
                }
                
            }
            if (!npadState.GetButtonDown(NpadButton.A))
            {
                A_interactable = true;
            }

            if (npadState.GetButtonDown(NpadButton.B))
            {   
                
                if(B_interactable){
                    B_interactable = false;
                    if(pause_menu_active){
                    GameManager.instance.Unpause();
                    SaveManager.SetFloat("sfx_volume", sfx_control.value);
                    SaveManager.SetFloat("music_volume", music_control.value);
                    SaveManager.Save();
                    pause_menu_active = false;
                    
                    }
                    else{
                        GameManager.instance.MainMenuButton();
                        
                    }
                }  
                
                

            }
            if (!npadState.GetButtonDown(NpadButton.B))
            {
                B_interactable = true;
            }

            //Resets the scene (Level)
            if (npadState.GetButtonDown(NpadButton.X))
            {
                if(X_interactable){
                    GameManager.instance.Reset_button_func();
                }
                
            }
            if (!npadState.GetButtonDown(NpadButton.X))
            {
                X_interactable = true;
                
            }

            if (npadState.GetButtonDown(NpadButton.Y))
            {
                
            }

            if (npadState.GetButtonDown(NpadButton.ZR))
            {
               
            }
            else if (npadState.GetButtonUp(NpadButton.ZR))
            {
                
            }

            if (npadState.GetButtonDown(NpadButton.ZL))
            {
                
            }
            else if (npadState.GetButtonUp(NpadButton.ZL))
            {
                
            }


            if (npadState.GetButtonDown(NpadButton.Plus))
            {

                if(Plus_interactable){
                    //Pause Menu
                    moveable = true;
                    GameManager.instance.Pause_Menu();
                    pause_menu_active = true;
                    j = 0;
                    color_changer(j);
                }
                
                //GameManager.instance.MainMenuButton();
                
                
            }
            if (!npadState.GetButtonDown(NpadButton.Plus))
            {
                
                Plus_interactable = true;
            }
            
            // Returns Main Menu
            if (npadState.GetButtonDown(NpadButton.Minus))
            {
                
            }

            if (npadState.GetButtonDown(NpadButton.R))
            {
                
            }

            if (npadState.GetButtonDown(NpadButton.L))
            {
                
            }
        }
#endif
        }

         public void color_changer(int k){

            GameObject[] buttons = new GameObject[1];
            GameObject[] sliders = new GameObject[2];

            buttons[0] = GameObject.Find("ButtonMainMenu");
            sliders[0] = GameObject.Find("SFXSlider");
            sliders[1] = GameObject.Find("MusicSlider");

            if(k == 0){
                sliders[0].GetComponent<Slider>().colors = select_colour;
                sliders[1].GetComponent<Slider>().colors = unselect_colour;
                buttons[0].GetComponent<Button>().colors = unselect_colour;   
            }
            if(k == 1){
                sliders[0].GetComponent<Slider>().colors = unselect_colour;
                sliders[1].GetComponent<Slider>().colors = select_colour;
                buttons[0].GetComponent<Button>().colors = unselect_colour;
            }
            if(k == 2){
                sliders[0].GetComponent<Slider>().colors = unselect_colour;
                sliders[1].GetComponent<Slider>().colors = unselect_colour;
                buttons[0].GetComponent<Button>().colors = select_colour;
                
            }
            if(k == 3){
                sliders[0].GetComponent<Slider>().colors = unselect_colour;
                sliders[1].GetComponent<Slider>().colors = unselect_colour;
                buttons[0].GetComponent<Button>().colors = unselect_colour;
                
            }
         }


#if UNITY_SWITCH
    private bool UpdatePadState()
    {
        NpadStyle handheldStyle = Npad.GetStyleSet(NpadId.Handheld);
        NpadState handheldState = npadState;
        if (handheldStyle != NpadStyle.None)
        {
            Npad.GetState(ref handheldState, NpadId.Handheld, handheldStyle);
            if (handheldState.buttons != NpadButton.None)
            {
                npadId = NpadId.Handheld;
                npadStyle = handheldStyle;
                npadState = handheldState;
                return true;
            }
        }

        NpadStyle no1Style = Npad.GetStyleSet(NpadId.No1);
        NpadState no1State = npadState;
        if (no1Style != NpadStyle.None)
        {
            Npad.GetState(ref no1State, NpadId.No1, no1Style);
            if (no1State.buttons != NpadButton.None)
            {
                npadId = NpadId.No1;
                npadStyle = no1Style;
                npadState = no1State;
                return true;
            }
        }

        if ((npadId == NpadId.Handheld) && (handheldStyle != NpadStyle.None))
        {
            npadId = NpadId.Handheld;
            npadStyle = handheldStyle;
            npadState = handheldState;
        }
        else if ((npadId == NpadId.No1) && (no1Style != NpadStyle.None))
        {
            npadId = NpadId.No1;
            npadStyle = no1Style;
            npadState = no1State;
        }
        else
        {
            npadId = NpadId.Invalid;
            npadStyle = NpadStyle.Invalid;
            npadState.Clear();
            return false;
        }
        return true;
    }
#endif
    }

}
