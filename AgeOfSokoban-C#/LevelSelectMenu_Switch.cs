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

    public class LevelSelectMenu_Switch : MonoBehaviour
    {

        public ColorBlock select_colour;
        
        public ColorBlock unselect_colour;

        
        int i;

        public bool A_interactable;
        public bool B_interactable;
        public bool Y_interactable;
        public bool X_interactable;
        public bool L_interactable;
        public bool R_interactable;
        public bool Plus_interactable;
        public bool moveable;
        public float time;
        

#if UNITY_SWITCH
    private NpadId npadId = NpadId.Invalid;
    private NpadStyle npadStyle = NpadStyle.Invalid;
    private NpadState npadState = new NpadState();
#endif

        void Start()
        {
            i = 0;
            color_changer(i);
            A_interactable = false;
            B_interactable = false;
            Y_interactable = false;
            X_interactable = false;
            L_interactable = false;
            R_interactable = false;
            Plus_interactable = false;
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

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //up
                StartCoroutine(MoveUp());
                
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
               //down
                StartCoroutine(MoveDown());
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                 //left
                StartCoroutine(MoveLeft());
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //right
                StartCoroutine(MoveRight());
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                
                if(i == 0){
                    LevelSelectManager.instance.Level1();
                }
                if(i == 1){
                     if(SaveManager.HasKey("Level0")){
                        LevelSelectManager.instance.Level2();
                    }
                }
                if(i == 2){
                     if(SaveManager.HasKey("Level1")){
                        LevelSelectManager.instance.Level3();
                    }
                }
                if(i == 3){
                     if(SaveManager.HasKey("Level2")){
                        LevelSelectManager.instance.Level4();
                    }
                }
                if(i == 4){
                     if(SaveManager.HasKey("Level3")){
                        LevelSelectManager.instance.Level5();
                    }
                }
                if(i == 5){
                     if(SaveManager.HasKey("Level4")){
                        LevelSelectManager.instance.Level6();
                    }
                }
                if(i == 6){
                     if(SaveManager.HasKey("Level5")){
                        LevelSelectManager.instance.Level7();
                    }
                }
                if(i == 7){
                     if(SaveManager.HasKey("Level6")){
                        LevelSelectManager.instance.Level8();
                    }
                }
                if(i == 8){
                     if(SaveManager.HasKey("Level7")){
                        LevelSelectManager.instance.Level9();
                    }
                }
                if(i == 9){
                     if(SaveManager.HasKey("Level8")){
                        LevelSelectManager.instance.Level10();
                    }
                }
                if(i == 10){
                     if(SaveManager.HasKey("Level9")){
                        LevelSelectManager.instance.Level11();
                    }
                }
                if(i == 11){
                     if(SaveManager.HasKey("Level10")){
                        LevelSelectManager.instance.Level12();
                    }
                }
                if(i == 12){
                     if(SaveManager.HasKey("Level11")){
                        LevelSelectManager.instance.Level13();
                    }
                }
                if(i == 13){
                     if(SaveManager.HasKey("Level12")){
                        LevelSelectManager.instance.Level14();
                    }
                }
                if(i == 14){
                     if(SaveManager.HasKey("Level3")){
                        LevelSelectManager.instance.Level15();
                    }
                }
                if(i == 15){
                     if(SaveManager.HasKey("Level14")){
                        LevelSelectManager.instance.Level16();
                    }
                }
                if(i == 16){
                     if(SaveManager.HasKey("Level15")){
                        LevelSelectManager.instance.Level17();
                    }
                }
                if(i == 17){
                     if(SaveManager.HasKey("Level16")){
                        LevelSelectManager.instance.Level18();
                    }
                }
                if(i == 18){
                    LevelSelectManager.instance.next_level_scene_button_func();
                }

                
            }
            //Returns main menu
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
                    
            }
            
            // Returns Main Menu
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene("MainMenu");
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("Levels_greek");
            }

#if UNITY_SWITCH
        if (UpdatePadState())
        {
            AnalogStickState lStick = npadState.analogStickL;
            AnalogStickState rStick = npadState.analogStickR;


                if (Mathf.Abs(lStick.x) < 10000 && Mathf.Abs(lStick.y) < 0.1f)
                {
                    time = 100;
                }

                //Movements w/ analog stick, (checks both if the input is bigger than 10000 threshold && bigger than other axis)
                if (lStick.y > 10000 && Mathf.Abs(lStick.y) > Mathf.Abs(lStick.x))
            {
                //up
                if(time > 0.5f){
                    if(i == 18){
                        if(SaveManager.HasKey("Level3")){
                            i = 5;
                        } 
                    }
                    else if(i>5){
                        i -= 6;
                    }

                    color_changer(i);
                    time = 0f;
                }
                 
            }

            if(lStick.y < -10000 && Mathf.Abs(lStick.y) > Mathf.Abs(lStick.x))
            {
                //down
                if(time > 0.5f){
                    string temp1 = "Level" + (i+5);
                
                    if(i == 18){
                        if(SaveManager.HasKey("Level16")){
                            i = 17;
                        }
                        
                    }
                    else if(i < 12){
                        if(SaveManager.HasKey(temp1)){
                            i += 6;
                        }  
                    }

                    color_changer(i);

                    time = 0f;
                }
                

            }
            if(lStick.x > 10000 && Mathf.Abs(lStick.x) > Mathf.Abs(lStick.y))
            {

                //right
                if(time > 0.5f){
                    string temp2 = "Level" + i;
                    if(i == 5 || i == 11 || i == 17){
                        i=18;
                    }
                    else if(i < 18) {
                        if(SaveManager.HasKey(temp2)){
                            i++;
                        }
                        else{
                            i = 18;
                        }
                    }
                    
                    color_changer(i);
                    
                    time = 0f;
                }
                
                
            }
            if(lStick.x < -10000 && Mathf.Abs(lStick.x) > Mathf.Abs(lStick.y))
            {

                //left
                if(time > 0.5f){
                    if(i == 18){
                    
                        if(SaveManager.HasKey("Level10")){
                            i = 11;
                        }

                        else if(SaveManager.HasKey("Level4")){
                        i = 5; 
                        }

                        else if(SaveManager.HasKey("Level3")){
                        i = 4; 
                        }
                        else if(SaveManager.HasKey("Level2")){
                        i = 3; 
                        }
                        else if(SaveManager.HasKey("Level1")){
                        i = 2; 
                        }
                        else if(SaveManager.HasKey("Level0")){
                        i = 1; 
                        }
                        else{
                            i = 0;
                        }
                    
                    }
                    else if(i>0){
                        i--;
                    }

                    color_changer(i);
                    time = 0f;
                }
                
               
                
            }

            if (npadState.GetButtonDown(NpadButton.Up))
            {
                //up
                if(i == 18){
                    if(SaveManager.HasKey("Level3")){
                        i = 5;
                    } 
                }
                else if(i>5){
                    i -= 6;
                }

                color_changer(i);
                
            }

            if (npadState.GetButtonDown(NpadButton.Down))
            {
               //down
                string temp1 = "Level" + (i+5);
                
                if(i == 18){
                    if(SaveManager.HasKey("Level16")){
                        i = 17;
                    }
                    
                }
                else if(i < 12){
                    if(SaveManager.HasKey(temp1)){
                        i += 6;
                    }  
                }

                color_changer(i);
            }

            if (npadState.GetButtonDown(NpadButton.Left))
            {
                 //left
                if(i == 18){
                    
                    if(SaveManager.HasKey("Level10")){
                        i = 11;
                    }

                    else if(SaveManager.HasKey("Level4")){
                       i = 5; 
                    }

                    else if(SaveManager.HasKey("Level3")){
                       i = 4; 
                    }
                    else if(SaveManager.HasKey("Level2")){
                       i = 3; 
                    }
                    else if(SaveManager.HasKey("Level1")){
                       i = 2; 
                    }
                    else if(SaveManager.HasKey("Level0")){
                       i = 1; 
                    }
                    else{
                        i = 0;
                    }
                    
                }
                else if(i>0){
                    i--;
                }

                color_changer(i);
            }

            if (npadState.GetButtonDown(NpadButton.Right))
            {
                //right
                string temp2 = "Level" + i;
                if(i == 5 || i == 11 || i == 17){
                    i=18;
                }
                else if(i < 18) {
                    if(SaveManager.HasKey(temp2)){
                        i++;
                    }
                    else{
                        i = 18;
                    }
                }
                
                color_changer(i);
            }

            if (npadState.GetButtonDown(NpadButton.A))
            {
                if(A_interactable){
                    if(i == 0){
                        LevelSelectManager.instance.Level1();
                    }
                    if(i == 1){
                        if(SaveManager.HasKey("Level0")){
                            LevelSelectManager.instance.Level2();
                        }
                    }
                    if(i == 2){
                        if(SaveManager.HasKey("Level1")){
                            LevelSelectManager.instance.Level3();
                        }
                    }
                    if(i == 3){
                        if(SaveManager.HasKey("Level2")){
                            LevelSelectManager.instance.Level4();
                        }
                    }
                    if(i == 4){
                        if(SaveManager.HasKey("Level3")){
                            LevelSelectManager.instance.Level5();
                        }
                    }
                    if(i == 5){
                        if(SaveManager.HasKey("Level4")){
                            LevelSelectManager.instance.Level6();
                        }
                    }
                    if(i == 6){
                        if(SaveManager.HasKey("Level5")){
                            LevelSelectManager.instance.Level7();
                        }
                    }
                    if(i == 7){
                        if(SaveManager.HasKey("Level6")){
                            LevelSelectManager.instance.Level8();
                        }
                    }
                    if(i == 8){
                        if(SaveManager.HasKey("Level7")){
                            LevelSelectManager.instance.Level9();
                        }
                    }
                    if(i == 9){
                        if(SaveManager.HasKey("Level8")){
                            LevelSelectManager.instance.Level10();
                        }
                    }
                    if(i == 10){
                        if(SaveManager.HasKey("Level9")){
                            LevelSelectManager.instance.Level11();
                        }
                    }
                    if(i == 11){
                        if(SaveManager.HasKey("Level10")){
                            LevelSelectManager.instance.Level12();
                        }
                    }
                    if(i == 12){
                        if(SaveManager.HasKey("Level11")){
                            LevelSelectManager.instance.Level13();
                        }
                    }
                    if(i == 13){
                        if(SaveManager.HasKey("Level12")){
                            LevelSelectManager.instance.Level14();
                        }
                    }
                    if(i == 14){
                        if(SaveManager.HasKey("Level3")){
                            LevelSelectManager.instance.Level15();
                        }
                    }
                    if(i == 15){
                        if(SaveManager.HasKey("Level14")){
                            LevelSelectManager.instance.Level16();
                        }
                    }
                    if(i == 16){
                        if(SaveManager.HasKey("Level15")){
                            LevelSelectManager.instance.Level17();
                        }
                    }
                    if(i == 17){
                        if(SaveManager.HasKey("Level16")){
                            LevelSelectManager.instance.Level18();
                        }
                    }
                    if(i == 18){
                        LevelSelectManager.instance.next_level_scene_button_func();
                    }

                }
                
                
                
                
            }
            if (!npadState.GetButtonDown(NpadButton.A))
            {
                A_interactable = true;
                
            }

            //Returns main menu
            if (npadState.GetButtonDown(NpadButton.B))
            {

                if(B_interactable){
                    SceneManager.LoadScene("MainMenu");
                }
                
                   
                
            }
            if (!npadState.GetButtonDown(NpadButton.B))
            {

                B_interactable = true;
                
            }

            
            if (npadState.GetButtonDown(NpadButton.X))
            {
                
            }
            else if (npadState.GetButtonUp(NpadButton.X))
            {
                
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

            // Returns Main Menu
            if (npadState.GetButtonDown(NpadButton.Plus))
            {
                if(Plus_interactable){
                    SceneManager.LoadScene("MainMenu");
                }
                
            }
            if (!npadState.GetButtonDown(NpadButton.Plus))
            {
                Plus_interactable = true;
            }
            
            
            if (npadState.GetButtonDown(NpadButton.Minus))
            {
                
            }

            if (npadState.GetButtonDown(NpadButton.R))
            {
                if(R_interactable){
                    SceneManager.LoadScene("Levels_greek");
                }
                
            }
            if (!npadState.GetButtonDown(NpadButton.R))
            {
                R_interactable = true;
            }
            

            if (npadState.GetButtonDown(NpadButton.L))
            {
                
            }
            if (!npadState.GetButtonDown(NpadButton.L))
            {
                L_interactable = true;
            }
        }
#endif
        }

        public void color_changer(int k){

            //GameObject[] buttons = GameObject.FindGameObjectsWithTag("button");
            
            GameObject[] buttons = new GameObject[19];

            buttons[0] = GameObject.Find("Level_Button_1");
            buttons[1] = GameObject.Find("Level_Button_2");
            buttons[2] = GameObject.Find("Level_Button_3");
            buttons[3] = GameObject.Find("Level_Button_4");
            buttons[4] = GameObject.Find("Level_Button_5");
            buttons[5] = GameObject.Find("Level_Button_6");
            buttons[6] = GameObject.Find("Level_Button_7");
            buttons[7] = GameObject.Find("Level_Button_8");
            buttons[8] = GameObject.Find("Level_Button_9");
            buttons[9] = GameObject.Find("Level_Button_10");
            buttons[10] = GameObject.Find("Level_Button_11");
            buttons[11] = GameObject.Find("Level_Button_12");
            buttons[12] = GameObject.Find("Level_Button_13");
            buttons[13] = GameObject.Find("Level_Button_14");
            buttons[14] = GameObject.Find("Level_Button_15");
            buttons[15] = GameObject.Find("Level_Button_16");
            buttons[16] = GameObject.Find("Level_Button_17");
            buttons[17] = GameObject.Find("Level_Button_18");
            buttons[18] = GameObject.Find("next_level_scene_Button");

            for(int a = 0; a < buttons.Length; a++){
                if(a == k){
                    buttons[a].GetComponent<Button>().colors = select_colour;
                }
                else{
                    buttons[a].GetComponent<Button>().colors = unselect_colour;
                }
            }
        }

        IEnumerator MoveUp(){
            
            while(!moveable){
                yield return new WaitForSeconds(0.2f);
                moveable = true;
            } 

            while(moveable){
                moveable = false;
                if(i == 18){
                    if(SaveManager.HasKey("Level3")){
                        i = 5;
                    } 
                }
                else if(i>5){
                    i -= 6;
                }

                color_changer(i);
            }
        }

        IEnumerator MoveDown(){
            while(!moveable){
                yield return new WaitForSeconds(0.2f);
                moveable = true;
            } 

            while(moveable){
                moveable = false;
                string temp1 = "Level" + (i+5);
                
                if(i == 18){
                    if(SaveManager.HasKey("Level16")){
                        i = 17;
                    }
                    
                }
                else if(i < 12){
                    if(SaveManager.HasKey(temp1)){
                        i += 6;
                    }  
                }

                color_changer(i);
            }

        }

        IEnumerator MoveRight(){
            while(!moveable){
                yield return new WaitForSeconds(0.2f);
                moveable = true;
            } 

            while(moveable){
                moveable = false;
                string temp2 = "Level" + i;
                if(i == 5 || i == 11 || i == 17){
                    i=18;
                }
                else if(i < 18) {
                    if(SaveManager.HasKey(temp2)){
                        i++;
                    }
                    else{
                        i = 18;
                    }
                }
                
                color_changer(i);
            }

        }

        IEnumerator MoveLeft(){
            while(!moveable){
                yield return new WaitForSeconds(0.2f);
                moveable = true;
            } 

            while(moveable){
                moveable = false;
                if(i == 18){
                    
                    if(SaveManager.HasKey("Level10")){
                        i = 11;
                    }

                    else if(SaveManager.HasKey("Level4")){
                       i = 5; 
                    }

                    else if(SaveManager.HasKey("Level3")){
                       i = 4; 
                    }
                    else if(SaveManager.HasKey("Level2")){
                       i = 3; 
                    }
                    else if(SaveManager.HasKey("Level1")){
                       i = 2; 
                    }
                    else if(SaveManager.HasKey("Level0")){
                       i = 1; 
                    }
                    else{
                        i = 0;
                    }
                    
                }
                else if(i>0){
                    i--;
                }

                color_changer(i);
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
