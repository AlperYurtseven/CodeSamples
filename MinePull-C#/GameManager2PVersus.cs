using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;


public class GameManager2PVersus : MonoBehaviour
{

    public static GameManager2PVersus instance = null;
    public LevelBuilder2P m_LevelBuilder;
    public LevelBuilder2PL m_LevelBuilder2;
    //public Camera meyn_camera;
    //public Button mainMenuButton;
    public bool m_ReadyForInput;
    public AudioSource victory_sound;
    public AudioSource box_destroyR;
    public AudioSource box_destroyL;
    public AudioSource wrong_boxR;
    public AudioSource wrong_boxL;
    public Player1 player1;
    public GameObject firework;

    public Player2 player2;

    public int number;

    public bool victory_sound_played = false;
    public bool camera_set = false;
    public Text winner_announcer_text;

    public Text left_curr_level;
    public Text right_curr_level;

    GameObject[] black_boxesP1;
    GameObject[] blue_boxesP1;
    GameObject[] green_boxesP1;
    GameObject[] red_boxesP1;

    GameObject[] black_boxesP2;
    GameObject[] blue_boxesP2;
    GameObject[] green_boxesP2;
    GameObject[] red_boxesP2;
    GameObject[] walls;
    GameObject[] ladders;
    GameObject[] not_move_ladders;
    GameObject[] players;

    List<int> random_levels = new List<int>();

    public bool thrown_at_least_onceL;
    public bool thrown_at_least_onceR;

    public string temp;
    List<string> lines;
    bool added_to_temp;

    float taymL;
    float taymR;

    public int levels_completedL;
    public int levels_completedR;

    bool counter_increasedL;
    bool counter_increasedR;

    bool firework_initiated;

    public Text resetL;
    public Text resetR;

    public GameObject hit_arrowL;

    public GameObject hit_arrowR;

    public GameObject destroy_arrowL;
    public GameObject destroy_arrowR;

    public bool hit_arrow_createdL;
    public bool hit_arrow_createdR;

    public bool arrow_instedL;

    public bool arrow_instedR;

    public float new_level_load_gapL;
    public float new_level_load_gapR;

    public bool new_level_load_resetL;
    public bool new_level_load_resetR;

    public Text new_level_loadingL;
    public Text new_level_loadingR;


    //public Text level_number;

    //public EventSystem event_sys;

    void Awake()
    {

        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }

        thrown_at_least_onceR = false;
        thrown_at_least_onceL = false;

        winner_announcer_text.enabled = false;


    }
    void Start()
    {
        
        levels_completedL = 0;
        levels_completedR = 0;
        counter_increasedL = false;
        counter_increasedR = false;
        firework_initiated = false;
        resetL.enabled = false;
        resetR.enabled = false;
        taymR = 0.0f;
        taymL = 0.0f;
        arrow_instedL = false;
        arrow_instedR = false;
        hit_arrow_createdL = false;
        hit_arrow_createdR = false;
        new_level_load_gapL = 0.0f;
        new_level_load_gapR = 0.0f;
        new_level_load_resetL = false;
        new_level_load_resetR = false;
        new_level_loadingL.enabled = false;
        new_level_loadingR.enabled = false;
        



        int how_many_levels_wanted = SaveManager.GetInt("how_many_levels");
        
        if(how_many_levels_wanted == 3){
            Level_Picker3();
        }
        else{

            Level_Picker5();

        }

        right_curr_level.text = "Level " + random_levels[levels_completedR].ToString();
        left_curr_level.text = "Level " + random_levels[levels_completedL].ToString();
    
    

        ResetScene();

        player1 = GameObject.FindObjectOfType<Player1>();
        player2 = GameObject.FindObjectOfType<Player2>();

      
        
        
    }

    public void Pause_Menu(){

        m_ReadyForInput = false;
        
    }

    public void Unpause(){

        m_ReadyForInput = true;
        

    }

    public void Level_Picker3(){

        //int howmanylevels = SaveManager.GetInt("how_much");


        int add_val1 = Randomizer6();

        random_levels.Add(add_val1);

        int add_val2 = Randomizer7();

        random_levels.Add(add_val2);

        int add_val3 = Randomizer8();

        random_levels.Add(add_val3);

    }

    public void Level_Picker5(){

        //int howmanylevels = SaveManager.GetInt("how_much");

        int add_val = Randomizer1();

        random_levels.Add(add_val);

        int add_val2 = Randomizer2();

        random_levels.Add(add_val2);

        int add_val3 = Randomizer3();

        random_levels.Add(add_val3);

        int add_val4 = Randomizer4();

        random_levels.Add(add_val4);

        int add_val5 = Randomizer5();

        random_levels.Add(add_val5);


    }

    public int Randomizer1(){
        System.Random rand = new System.Random(Guid.NewGuid().GetHashCode());
        int value = rand.Next(2,9);

        return value;
    }

    public int Randomizer2(){
        System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
        int value = rnd.Next(10,19);

        return value;
    }

    public int Randomizer3(){
        System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
        int value = rnd.Next(20,29);

        return value;
    }
    public int Randomizer4(){
        System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
        int value = rnd.Next(30,39);

        return value;
    }
    public int Randomizer5(){
        System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
        int value = rnd.Next(40,49);

        return value;
    }
    public int Randomizer6(){
        System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
        int value = rnd.Next(2,9);

        return value;
    }
    public int Randomizer7(){
        System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
        int value = rnd.Next(10,19);

        return value;
    }
    public int Randomizer8(){
        System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
        int value = rnd.Next(20,49);

        return value;
    }

    

    void Update()
    {

        black_boxesP2 = GameObject.FindGameObjectsWithTag("blackL");
        blue_boxesP2 = GameObject.FindGameObjectsWithTag("blueL");
        green_boxesP2 = GameObject.FindGameObjectsWithTag("greenL");
        red_boxesP2 = GameObject.FindGameObjectsWithTag("redL");
        
        black_boxesP1 = GameObject.FindGameObjectsWithTag("blackR");
        blue_boxesP1 = GameObject.FindGameObjectsWithTag("blueR");
        green_boxesP1 = GameObject.FindGameObjectsWithTag("greenR");
        red_boxesP1 = GameObject.FindGameObjectsWithTag("redR");

        if(GameObject.FindGameObjectsWithTag("hit_arrowL").Length > 0){
            destroy_arrowL = GameObject.FindGameObjectsWithTag("hit_arrowL")[0];
        }

        if(GameObject.FindGameObjectsWithTag("hit_arrowR").Length > 0){
            destroy_arrowR = GameObject.FindGameObjectsWithTag("hit_arrowR")[0];
        }

        
        player1 = FindObjectOfType<Player1>();
        player2 = FindObjectOfType<Player2>();
        //Debug.Log(remained_bottles.text);

        

        //Debug.Log(m_ReadyForInput);

        int temp_level_number = m_LevelBuilder.m_CurrentLevel +1;
        string temp_level_num = "Level " + temp_level_number.ToString();
        //level_number.text = temp_level_num;

        //background_music.volume = SaveManager.GetFloat("music_volume");

        //victory_sound.volume = SaveManager.GetFloat("sfx_volume");

        if(Input.GetKeyDown(KeyCode.UpArrow)){
            player1.MoveUp();
        }
        
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            player1.MoveDown();
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            
            player1.Throw();
            thrown_at_least_onceR = true;
            taymR = 0.0f;
        }

        if(Input.GetKeyDown(KeyCode.W)){
            if(player2.can_move){
                player2.MoveUp();
                counter_increasedL = false;
            }
            
        }
        
        if(Input.GetKeyDown(KeyCode.S)){
            if(player2.can_move){
                player2.MoveDown();
                counter_increasedL = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.F)){
            
            player2.Throw();
            thrown_at_least_onceL = true;
            taymL = 0.0f;
        }

       /*  if(Input.GetKeyDown(KeyCode.Y)){
            Debug.Log(black_boxesP1.Count);
        } */
        

        if(Input.GetKeyDown(KeyCode.R)){
            
            Player1_reset();
            
        }

        if(Input.GetKeyDown(KeyCode.P)){
            
            Player2_reset();
            
        }

        if(Input.GetKeyDown(KeyCode.Y)){

            Debug.Log(levels_completedR);
            
            for(int i= 0; i < random_levels.Count; i++){

                Debug.Log(random_levels[i]);

            }
            
        }

        if(thrown_at_least_onceL && player2.can_move && taymL > 3.0f){
            if(!NotPossibleL()){

                player2.can_move = false;
                resetL.enabled = true;

            }
        }

        if(thrown_at_least_onceR && player1.can_move && taymR > 3.0f){
            if(!NotPossibleR()){

                player1.can_move = false;
                resetR.enabled = true;

            }
        }

        if(Input.GetKeyDown(KeyCode.L)){

            
            victory_sound.Play();
            
            winner_announcer_text.text = "Player 1 Wins";
            winner_announcer_text.enabled = true;
            Instantiate(firework, new Vector3(0, 0, 5), Quaternion.identity);
            
        }
        if(Input.GetKeyDown(KeyCode.U)){

            Debug.Log(random_levels[0]);
            Debug.Log(random_levels[1]);
            Debug.Log(random_levels[2]);
        }


        if(IsLevelCompleteL()){
            if(!counter_increasedL){
                levels_completedL += 1;
                counter_increasedL = true;
            }
            if(levels_completedL == random_levels.Count){
                LeftWins();
            }
            if(counter_increasedL){
                if(!new_level_load_resetL){
                    player2.can_move = false;
                    new_level_load_resetL = true;
                    new_level_loadingL.enabled = true;
                }
                else if(new_level_load_gapL > 1.00f){
                    NextLevelL();
                    //Player1_reset();
                    counter_increasedL = false;
                    new_level_load_gapL = 0.0f;
                    player2.can_move = true;
                    new_level_load_resetL = false;
                    new_level_loadingL.enabled = false;

                    left_curr_level.text = "Level " + random_levels[levels_completedL].ToString(); 
                }
                new_level_load_gapL += Time.deltaTime;
            }
                
            
        }

        if(IsLevelCompleteR()){
            if(!counter_increasedR){
                levels_completedR += 1;
                counter_increasedR = true;
            }
            if(levels_completedR == random_levels.Count){
                RightWins();
            }
            if(counter_increasedR){
                if(!new_level_load_resetR){
                    player1.can_move = false;
                    new_level_load_resetR = true;
                    new_level_loadingR.enabled = true;
                }
                else if(new_level_load_gapR > 1.00f){
                    NextLevelR();
                    counter_increasedR = false;
                    new_level_load_gapR = 0.0f; 
                    player1.can_move = true;
                    new_level_load_resetR = false;
                    new_level_loadingR.enabled = false;
                    
                    right_curr_level.text = "Level " + random_levels[levels_completedR].ToString();
                    

                }
                new_level_load_gapR += Time.deltaTime;
            }
        }


        if(arrow_instedR){
            Destroy(destroy_arrowR);
            arrow_instedR = false;
            hit_arrow_createdR = false;
        }

        if(arrow_instedL){
            Destroy(destroy_arrowL);
            arrow_instedL = false;
            hit_arrow_createdL = false;
        }

        if(player1 != null){
            if(player1.transform.position.y > -3){

                hit_arrow_R();
            }
        }
        
        
        
        if(player2 != null){
            if(player2.transform.position.y > -3){
                hit_arrow_L();
            
            }

        }
        
        if(Input.GetKeyDown(KeyCode.Escape)){
            MainMenu();
        }
        
        

        taymR += Time.deltaTime;
        taymL += Time.deltaTime;

    }

    public void MainMenu(){

        SceneManager.LoadScene("MainMenu");
    }

    public bool can_be_hittable_that_yL(int a){

        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");
        Player2 playeriki = GameObject.FindObjectOfType<Player2>();

        foreach(var wall in walls){

            if(wall.transform.position.y == playeriki.transform.position.y && wall.transform.position.x < 0){

                if(wall.transform.position.x == a){
                    return false;
                }

            }

        }

        return true;

    }

    public bool can_be_hittable_that_yR(int a){

        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");
        Player1 playerbir = GameObject.FindObjectOfType<Player1>();

        foreach(var wall in walls){

            if(wall.transform.position.y == playerbir.transform.position.y && wall.transform.position.x > 0){

                if(wall.transform.position.x == a){
                    return false;
                }

            }

        }

        return true;

    }

    public void put_arrow_on_topL(int coor){

        GameObject[] black_boxes = GameObject.FindGameObjectsWithTag("blackL");
        GameObject[] blue_boxes = GameObject.FindGameObjectsWithTag("blueL");
        GameObject[] green_boxes = GameObject.FindGameObjectsWithTag("greenL");
        GameObject[] red_boxes = GameObject.FindGameObjectsWithTag("redL");

        Box2P box_in_hand = GameObject.FindObjectOfType<Box2P>();
        

        foreach(var black_bx in black_boxes){
            if(black_bx.transform.position.x != box_in_hand.transform.position.x && black_bx.transform.position.y != box_in_hand.transform.position.y){

                if(black_bx.transform.position.x == coor){
                    if(is_on_topL(black_bx)){

                        if(is_it_hittable_from_topL(black_bx)){

                            if(can_be_hittable_that_yL((int)black_bx.transform.position.x)){

                                
                                Instantiate(hit_arrowL, new Vector3(black_bx.transform.position.x, black_bx.transform.position.y + 1, 5), Quaternion.identity);
                                arrow_instedL = true;

                                
                                
                                
                            }

                        }

                    }
                }

                

            }
        }

        foreach(var blue_bx in blue_boxes){
            if(blue_bx.transform.position.x != box_in_hand.transform.position.x && blue_bx.transform.position.y != box_in_hand.transform.position.y){

                if(blue_bx.transform.position.x == coor){
                    if(is_on_topL(blue_bx)){

                        if(is_it_hittable_from_topL(blue_bx)){

                            if(can_be_hittable_that_yL((int)blue_bx.transform.position.x)){

                                

                                Instantiate(hit_arrowL, new Vector3(blue_bx.transform.position.x, blue_bx.transform.position.y + 1, 5), Quaternion.identity);
                                arrow_instedL = true;
                                

                            }

                        }

                    }
                }

            }
        }

        foreach(var green_bx in green_boxes){
            if(green_bx.transform.position.x != box_in_hand.transform.position.x && green_bx.transform.position.y != box_in_hand.transform.position.y){

                if(green_bx.transform.position.x == coor){
                    if(is_on_topL(green_bx)){

                        if(is_it_hittable_from_topL(green_bx)){

                            if(can_be_hittable_that_yL((int)green_bx.transform.position.x)){

                                
                            
                                Instantiate(hit_arrowL, new Vector3(green_bx.transform.position.x, green_bx.transform.position.y + 1, 5), Quaternion.identity);
                                arrow_instedL = true;
                            

                            }

                        }

                    }

                }
            }
        }

        foreach(var red_bx in red_boxes){
            if(red_bx.transform.position.x != box_in_hand.transform.position.x && red_bx.transform.position.y != box_in_hand.transform.position.y){

                if(red_bx.transform.position.x == coor){
                    if(is_on_topL(red_bx)){

                        if(is_it_hittable_from_topL(red_bx)){

                            if(can_be_hittable_that_yL((int)red_bx.transform.position.x)){
                                
                                Instantiate(hit_arrowL, new Vector3(red_bx.transform.position.x, red_bx.transform.position.y + 1, 5), Quaternion.identity);
                                arrow_instedL = true;
                                

                            }

                        }

                    }
                }

            }
        }



    }

    public void put_arrow_on_topR(int coor){

        GameObject[] black_boxes = GameObject.FindGameObjectsWithTag("blackR");
        GameObject[] blue_boxes = GameObject.FindGameObjectsWithTag("blueR");
        GameObject[] green_boxes = GameObject.FindGameObjectsWithTag("greenR");
        GameObject[] red_boxes = GameObject.FindGameObjectsWithTag("redR");

        Box2P1 box_in_hand = GameObject.FindObjectOfType<Box2P1>();

        foreach(var black_bx in black_boxes){
            if(black_bx.transform.position.x != box_in_hand.transform.position.x && black_bx.transform.position.y != box_in_hand.transform.position.y){

                if(black_bx.transform.position.x == coor){
                    if(is_on_topR(black_bx)){

                        if(is_it_hittable_from_topR(black_bx)){

                            if(can_be_hittable_that_yR((int)black_bx.transform.position.x)){

                                
                            
                                Instantiate(hit_arrowR, new Vector3(black_bx.transform.position.x, black_bx.transform.position.y + 1, 5), Quaternion.identity);
                                arrow_instedR = true;
                                


                            }

                        }

                    }
                }

                

            }
        }

        foreach(var blue_bx in blue_boxes){
            if(blue_bx.transform.position.x != box_in_hand.transform.position.x && blue_bx.transform.position.y != box_in_hand.transform.position.y){

                if(blue_bx.transform.position.x == coor){
                    if(is_on_topR(blue_bx)){

                        if(is_it_hittable_from_topR(blue_bx)){

                            if(can_be_hittable_that_yR((int)blue_bx.transform.position.x)){
                                

                                Instantiate(hit_arrowR, new Vector3(blue_bx.transform.position.x, blue_bx.transform.position.y + 1, 5), Quaternion.identity);
                                arrow_instedR = true;
                                

                            }

                        }

                    }
                }

            }
        }

        foreach(var green_bx in green_boxes){
            if(green_bx.transform.position.x != box_in_hand.transform.position.x && green_bx.transform.position.y != box_in_hand.transform.position.y){

                if(green_bx.transform.position.x == coor){
                    if(is_on_topR(green_bx)){

                        if(is_it_hittable_from_topR(green_bx)){

                            if(can_be_hittable_that_yR((int)green_bx.transform.position.x)){
                                
                            
                                Instantiate(hit_arrowR, new Vector3(green_bx.transform.position.x, green_bx.transform.position.y + 1, 5), Quaternion.identity);
                                arrow_instedR = true;
                                

                            }

                        }

                    }

                }
            }
        }

        foreach(var red_bx in red_boxes){
            if(red_bx.transform.position.x != box_in_hand.transform.position.x && red_bx.transform.position.y != box_in_hand.transform.position.y){

                if(red_bx.transform.position.x == coor){
                    if(is_on_topR(red_bx)){

                        if(is_it_hittable_from_topR(red_bx)){

                            if(can_be_hittable_that_yR((int)red_bx.transform.position.x)){

                                

                                Instantiate(hit_arrowR, new Vector3(red_bx.transform.position.x, red_bx.transform.position.y + 1, 5), Quaternion.identity);
                                arrow_instedR = true;
                                

                            }

                        }

                    }
                }

            }
        }



    }

    bool is_there_wall_on_left(GameObject a){

        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");

        foreach(var wall in walls){
            if(wall.transform.position.x == a.transform.position.x-1 && wall.transform.position.y == a.transform.position.y){
                return true;
            }
        }

        return false;
    }

    bool is_there_wall_on_right(GameObject a){

        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");

        foreach(var wall in walls){
            if(wall.transform.position.x == a.transform.position.x+1 && wall.transform.position.y == a.transform.position.y){
                return true;
            }
        }

        return false;
    }

    public void Player1_reset(){

        thrown_at_least_onceL = false;
        resetL.enabled = false;
        StartCoroutine(ResetP1());

    }
   
    public void Player2_reset(){

        thrown_at_least_onceR = false;
        resetR.enabled = false;
        StartCoroutine(ResetP2());
        
    }


    public void ResetScene()
    {
        StartCoroutine(ResetSceneAsyncP1());      
        StartCoroutine(ResetSceneAsyncP2());    
    }

    public void ReloadScene()
    {

        int curr_lvl = SaveManager.GetInt("curr_level");
        SaveManager.SetInt("wanted_Level",curr_lvl);
        SaveManager.Save();
        //StartCoroutine(ResetSceneAsync());

    }

    public bool NotPossibleL(){

        int counter = 0;


        GameObject[] black_boxes = GameObject.FindGameObjectsWithTag("blackL");
        GameObject[] blue_boxes = GameObject.FindGameObjectsWithTag("blueL");
        GameObject[] green_boxes = GameObject.FindGameObjectsWithTag("greenL");
        GameObject[] red_boxes = GameObject.FindGameObjectsWithTag("redL");

        Box2P box_in_hand = GameObject.FindObjectOfType<Box2P>();   

        if(box_in_hand.tag == "blackL"){

            foreach(var black_bx in black_boxes){
                if(!is_there_box_on_topL(black_bx) && is_it_hittable_from_topL(black_bx)){
                    counter += 1;
                }
                else if(!is_there_box_on_left(black_bx)){
                    counter += 1;
                }
            }

            if(counter > 1){
                return true;
            }

            /* Debug.Log(counter); */

            counter = 0;

        }

        if(box_in_hand.tag == "blueL"){

            foreach(var blue_bx in blue_boxes){
                if(!is_there_box_on_topL(blue_bx) && is_it_hittable_from_topL(blue_bx)){
                    counter += 1;
                }
                else if(!is_there_box_on_left(blue_bx)){
                    counter += 1;
                }
            }

            if(counter > 1){
                return true;
            }

            /* Debug.Log(counter); */
            
            counter = 0;
        }

        if(box_in_hand.tag == "greenL"){

            foreach(var green_bx in green_boxes){
                if(!is_there_box_on_topL(green_bx) && is_it_hittable_from_topL(green_bx)){
                    counter += 1;
                }
                else if(!is_there_box_on_left(green_bx)){
                    counter += 1;
                }
            }

            if(counter > 1){
                return true;
            }

            /* Debug.Log(counter); */

            counter = 0;
            
        }

        if(box_in_hand.tag == "redL"){

            foreach(var red_bx in red_boxes){
                if(!is_there_box_on_topL(red_bx) && is_it_hittable_from_topL(red_bx)){
                    counter += 1;
                }
                else if(!is_there_box_on_left(red_bx)){
                    counter += 1;
                }
            }

            if(counter > 1){
                return true;
            }

            /* Debug.Log(counter);
 */
            counter = 0;
            
        }

        return false;

    }
    public bool NotPossibleR(){

        int counter = 0;


        GameObject[] black_boxes = GameObject.FindGameObjectsWithTag("blackR");
        GameObject[] blue_boxes = GameObject.FindGameObjectsWithTag("blueR");
        GameObject[] green_boxes = GameObject.FindGameObjectsWithTag("greenR");
        GameObject[] red_boxes = GameObject.FindGameObjectsWithTag("redR");

        Box2P1 box_in_hand = GameObject.FindObjectOfType<Box2P1>();

        if(box_in_hand.tag == "blackR"){

            foreach(var black_bx in black_boxes){
                if(!is_there_box_on_topR(black_bx) && is_it_hittable_from_topR(black_bx)){
                    counter += 1;
                }
                else if(!is_there_box_on_right(black_bx)){
                    counter += 1;
                }
            }

            if(counter > 1){
                return true;
            }

            counter = 0;

        }

        if(box_in_hand.tag == "blueR"){

            foreach(var blue_bx in blue_boxes){
                if(!is_there_box_on_topR(blue_bx) && is_it_hittable_from_topR(blue_bx)){
                    counter += 1;
                }
                else if(!is_there_box_on_right(blue_bx)){
                    counter += 1;
                }
            }

            if(counter > 1){
                return true;
            }
            
            counter = 0;
        }

        if(box_in_hand.tag == "greenR"){

            foreach(var green_bx in green_boxes){
                if(!is_there_box_on_topR(green_bx) && is_it_hittable_from_topR(green_bx)){
                    counter += 1;
                }
                else if(!is_there_box_on_right(green_bx)){
                    counter += 1;
                }
            }

            if(counter > 1){
                return true;
            }
            counter = 0;
            
        }

        if(box_in_hand.tag == "redR"){

            foreach(var red_bx in red_boxes){
                if(!is_there_box_on_topR(red_bx) && is_it_hittable_from_topR(red_bx)){
                    counter += 1;
                }
                else if(!is_there_box_on_right(red_bx)){
                    counter += 1;
                }
            }

            if(counter > 1){
                return true;
            }
            counter = 0;
            
        }

        return false;

    }

    bool is_it_hittable_from_topL(GameObject a){

        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");

        foreach(var wall in walls){
            if(a.transform.position.x + 1 == wall.transform.position.x && wall.transform.position.y > 0){
                
                if(is_there_wall_on_left(wall)){
                    continue;
                }
                else{
                    return true;
                }
                
            }
        } 

        return false;

    }
    public void hit_arrow_R(){

        Player1 playerbir = GameObject.FindObjectOfType<Player1>();

        if(arrow_instedR){
            Destroy(destroy_arrowR);
            arrow_instedR = false;
            hit_arrow_createdR = false;
        }

        if(playerbir.transform.position.y == -2){
            if(!is_there_wall_in_betweenR()){
                put_arrow_on_topR(2);
                hit_arrow_createdL = true;
            }
        }

        else if(playerbir.transform.position.y == -1){
            if(!is_there_wall_in_betweenR()){
                put_arrow_on_topR(2);
                hit_arrow_createdL = true;
            }
        }

        else if(playerbir.transform.position.y == 0){
            if(!is_there_wall_in_betweenR()){
                put_arrow_on_topR(2);
                hit_arrow_createdR = true;
            }
        }

        else if(playerbir.transform.position.y == 1){
            if(!is_there_wall_in_betweenR()){
                put_arrow_on_topR(2);
                hit_arrow_createdR = true;
            }
        }

        else if(playerbir.transform.position.y == 2){
            
            put_arrow_on_topR(3);
            if(!arrow_instedR){
                    put_arrow_on_topR(2);
            }
            hit_arrow_createdR = true;
            
        }

        else if(playerbir.transform.position.y == 3){
            
            put_arrow_on_topR(4);
            if(!arrow_instedR){
                    put_arrow_on_topR(3);
                if(!arrow_instedR){
                    put_arrow_on_topR(2);
                }
            }
            hit_arrow_createdR = true;
            
        }

        else if(playerbir.transform.position.y == 4){
            
            put_arrow_on_topR(5);
            if(!arrow_instedR){
                    put_arrow_on_topR(4);
                    if(!arrow_instedR){
                        put_arrow_on_topR(3);
                        if(!arrow_instedR){
                            put_arrow_on_topR(2);
                        }
                    }
                }
            hit_arrow_createdR = true;
            
        }

        

        else if(playerbir.transform.position.y == 5){
            
            put_arrow_on_topR(6);
            if(!arrow_instedR){
                put_arrow_on_topR(5);
                if(!arrow_instedR){
                    put_arrow_on_topR(4);
                    if(!arrow_instedR){
                        put_arrow_on_topR(3);
                        if(!arrow_instedR){
                            put_arrow_on_topR(2);
                            if(!arrow_instedR){
                                put_arrow_on_topR(1);
                            }
                        }
                    }
                    
                }
            }
            hit_arrow_createdR = true;
            
        }
    }

    public void hit_arrow_L(){

        Player2 playeriki = GameObject.FindObjectOfType<Player2>();

        if(arrow_instedL){
            Destroy(destroy_arrowL);
            arrow_instedL = false;
            hit_arrow_createdL = false;
        }

        if(playeriki.transform.position.y == -2){
            if(!is_there_wall_in_betweenL()){
                put_arrow_on_topL(-2);
                hit_arrow_createdL = true;
            }
        }

        else if(playeriki.transform.position.y == -1){
            if(!is_there_wall_in_betweenL()){
                put_arrow_on_topL(-2);
                hit_arrow_createdL = true;
            }
        }

        else if(playeriki.transform.position.y == 0){
            if(!is_there_wall_in_betweenL()){
                put_arrow_on_topL(-2);
                hit_arrow_createdL = true;
            }
        }

        else if(playeriki.transform.position.y == 1){
            if(!is_there_wall_in_betweenL()){
                put_arrow_on_topL(-2);
                hit_arrow_createdL = true;
            }
        }

        else if(playeriki.transform.position.y == 2){
            
            put_arrow_on_topL(-3);
            if(!arrow_instedL){
                    put_arrow_on_topL(-2);
            }
            hit_arrow_createdL = true;
            
        }

        else if(playeriki.transform.position.y == 3){
            
            put_arrow_on_topL(-4);
            if(!arrow_instedL){
                    put_arrow_on_topL(-3);
                if(!arrow_instedL){
                    put_arrow_on_topL(-2);
                }
            }
            hit_arrow_createdL = true;
            
        }

        else if(playeriki.transform.position.y == 4){
            
            put_arrow_on_topL(-5);
            if(!arrow_instedL){
                    put_arrow_on_topL(-4);
                    if(!arrow_instedL){
                        put_arrow_on_topL(-3);
                        if(!arrow_instedL){
                            put_arrow_on_topL(-2);
                        }
                    }
                }
            hit_arrow_createdL = true;
            
        }

        

        else if(playeriki.transform.position.y == 5){
            
            put_arrow_on_topL(-6);
            if(!arrow_instedL){
                put_arrow_on_topL(-5);
                if(!arrow_instedL){
                    put_arrow_on_topL(-4);
                    if(!arrow_instedL){
                        put_arrow_on_topL(-3);
                        if(!arrow_instedL){
                            put_arrow_on_topL(-2);
                            if(!arrow_instedL){
                                put_arrow_on_topL(-1);
                            }
                        }
                    }
                    
                }
            }
            hit_arrow_createdL = true;
            
        }
    }

    bool is_it_hittable_from_topR(GameObject a){

        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");

        foreach(var wall in walls){
            if(a.transform.position.x - 1 == wall.transform.position.x && wall.transform.position.y > 0){
                
                if(is_there_wall_on_right(wall)){
                    continue;
                }
                else{
                    return true;
                }
                
            }
        } 

        return false;

    }

    public bool is_there_wall_in_betweenR(){

        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");

        foreach(var wall in walls){
            if(wall.transform.position.y == player1.transform.position.y){
                if(wall.transform.position.x > 2 && wall.transform.position.x < 11){
                    return true;
                }
            }
        }

        return false;

    }
    public bool is_there_wall_in_betweenL(){

        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");

        foreach(var wall in walls){
            if(wall.transform.position.y == player2.transform.position.y){
                if(wall.transform.position.x < -2 && wall.transform.position.x > -11){
                    return true;
                }
            }
        }

        return false;

    }

    public bool is_on_topL(GameObject a){

        if(is_there_box_on_topL(a)){
            return false;
        }
        else{
            return true;
        }
       
    }

    public bool is_on_topR(GameObject a){

        if(is_there_box_on_topR(a)){
            return false;
        }
        else{
            return true;
        }
       
    }

    bool is_there_box_on_topL(GameObject a){

        GameObject[] black_boxes = GameObject.FindGameObjectsWithTag("blackL");
        GameObject[] blue_boxes = GameObject.FindGameObjectsWithTag("blueL");
        GameObject[] green_boxes = GameObject.FindGameObjectsWithTag("greenL");
        GameObject[] red_boxes = GameObject.FindGameObjectsWithTag("redL");
        
        foreach(var black_box2 in black_boxes){
            if(a.transform.position.x == black_box2.transform.position.x && a.transform.position.y + 1 == black_box2.transform.position.y){
                return true;
            }
        }
        foreach(var blue_box2 in blue_boxes){
            if(a.transform.position.x == blue_box2.transform.position.x && a.transform.position.y + 1 == blue_box2.transform.position.y){
                return true;
            }
        }
        foreach(var red_box2 in red_boxes){
            if(a.transform.position.x == red_box2.transform.position.x && a.transform.position.y + 1 == red_box2.transform.position.y){
                return true;
            }
        }
        foreach(var green_box2 in green_boxes){
            if(a.transform.position.x == green_box2.transform.position.x && a.transform.position.y + 1 == green_box2.transform.position.y){
                return true;
            }
        }
        
        return false;
    }

    bool is_there_box_on_left(GameObject a){

        GameObject[] black_boxes = GameObject.FindGameObjectsWithTag("blackL");
        GameObject[] blue_boxes = GameObject.FindGameObjectsWithTag("blueL");
        GameObject[] green_boxes = GameObject.FindGameObjectsWithTag("greenL");
        GameObject[] red_boxes = GameObject.FindGameObjectsWithTag("redL");
        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");
        Player2 playeriki = GameObject.FindObjectOfType<Player2>();

         foreach(var wall in walls){

            if(wall.transform.position.x > playeriki.transform.position.x){

                if(a.transform.position.x - 1 == wall.transform.position.x && a.transform.position.y == wall.transform.position.y){
                    return true;
                }
                else if(a.transform.position.x - 2 == wall.transform.position.x && a.transform.position.y == wall.transform.position.y){
                    return true;
                }
                else if(a.transform.position.x - 3 == wall.transform.position.x && a.transform.position.y == wall.transform.position.y){
                    return true;
                }
                else if(a.transform.position.x - 4 == wall.transform.position.x && a.transform.position.y == wall.transform.position.y){
                    return true;
                }
                else if(a.transform.position.x - 5 == wall.transform.position.x && a.transform.position.y == wall.transform.position.y){
                    return true;
                }
            }

            
        }
        
        foreach(var black_box2 in black_boxes){

            if(black_box2.transform.position.x - 1 == playeriki.transform.position.x){
                continue;
            }
            else if(a.transform.position.x - 1 == black_box2.transform.position.x && a.transform.position.y == black_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x - 2 == black_box2.transform.position.x && a.transform.position.y == black_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x - 3 == black_box2.transform.position.x && a.transform.position.y == black_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x - 4 == black_box2.transform.position.x && a.transform.position.y == black_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x - 5 == black_box2.transform.position.x && a.transform.position.y == black_box2.transform.position.y){
                return true;
            }
        }

        foreach(var blue_box2 in blue_boxes){

            if(blue_box2.transform.position.x - 1 == playeriki.transform.position.x){
                continue;
            }
            else if(a.transform.position.x - 1 == blue_box2.transform.position.x && a.transform.position.y == blue_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x - 2 == blue_box2.transform.position.x && a.transform.position.y == blue_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x - 3 == blue_box2.transform.position.x && a.transform.position.y == blue_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x - 4 == blue_box2.transform.position.x && a.transform.position.y == blue_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x - 5 == blue_box2.transform.position.x && a.transform.position.y == blue_box2.transform.position.y){
                return true;
            }

        }

        foreach(var red_box2 in red_boxes){

            if(red_box2.transform.position.x - 1 == playeriki.transform.position.x){
                continue;
            }

            else if(a.transform.position.x - 1 == red_box2.transform.position.x && a.transform.position.y == red_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x - 2 == red_box2.transform.position.x && a.transform.position.y == red_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x - 3 == red_box2.transform.position.x && a.transform.position.y == red_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x - 4 == red_box2.transform.position.x && a.transform.position.y == red_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x - 5 == red_box2.transform.position.x && a.transform.position.y == red_box2.transform.position.y){
                return true;
            }


        }
        foreach(var green_box2 in green_boxes){

            if(green_box2.transform.position.x - 1 == playeriki.transform.position.x){
                continue;
            }

            else if(a.transform.position.x - 1 == green_box2.transform.position.x && a.transform.position.y == green_box2.transform.position.y){
                return true;
            }

            else if(a.transform.position.x - 2 == green_box2.transform.position.x && a.transform.position.y == green_box2.transform.position.y){
                return true;
            }

            else if(a.transform.position.x - 3 == green_box2.transform.position.x && a.transform.position.y == green_box2.transform.position.y){
                return true;
            }

            else if(a.transform.position.x - 4 == green_box2.transform.position.x && a.transform.position.y == green_box2.transform.position.y){
                return true;
            }

            else if(a.transform.position.x - 5 == green_box2.transform.position.x && a.transform.position.y == green_box2.transform.position.y){
                return true;
            }

        }
          
        return false;
    }

    bool is_there_box_on_topR(GameObject a){

        GameObject[] black_boxes = GameObject.FindGameObjectsWithTag("blackR");
        GameObject[] blue_boxes = GameObject.FindGameObjectsWithTag("blueR");
        GameObject[] green_boxes = GameObject.FindGameObjectsWithTag("greenR");
        GameObject[] red_boxes = GameObject.FindGameObjectsWithTag("redR");
        
        foreach(var black_box2 in black_boxes){
            if(a.transform.position.x == black_box2.transform.position.x && a.transform.position.y + 1 == black_box2.transform.position.y){
                return true;
            }
        }
        foreach(var blue_box2 in blue_boxes){
            if(a.transform.position.x == blue_box2.transform.position.x && a.transform.position.y + 1 == blue_box2.transform.position.y){
                return true;
            }
        }
        foreach(var red_box2 in red_boxes){
            if(a.transform.position.x == red_box2.transform.position.x && a.transform.position.y + 1 == red_box2.transform.position.y){
                return true;
            }
        }
        foreach(var green_box2 in green_boxes){
            if(a.transform.position.x == green_box2.transform.position.x && a.transform.position.y + 1 == green_box2.transform.position.y){
                return true;
            }
        }
        
        return false;
    }

    bool is_there_box_on_right(GameObject a){

        GameObject[] black_boxes = GameObject.FindGameObjectsWithTag("blackR");
        GameObject[] blue_boxes = GameObject.FindGameObjectsWithTag("blueR");
        GameObject[] green_boxes = GameObject.FindGameObjectsWithTag("greenR");
        GameObject[] red_boxes = GameObject.FindGameObjectsWithTag("redR");
        
        Player1 playeriki = GameObject.FindObjectOfType<Player1>();
        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");

         foreach(var wall in walls){

            if(wall.transform.position.x < playeriki.transform.position.x){
                
                if(a.transform.position.x + 1 == wall.transform.position.x && a.transform.position.y == wall.transform.position.y){
                    return true;
                }
                else if(a.transform.position.x + 2 == wall.transform.position.x && a.transform.position.y == wall.transform.position.y){
                    return true;
                }
                else if(a.transform.position.x + 3 == wall.transform.position.x && a.transform.position.y == wall.transform.position.y){
                    return true;
                }
                else if(a.transform.position.x + 4 == wall.transform.position.x && a.transform.position.y == wall.transform.position.y){
                    return true;
                }
                else if(a.transform.position.x + 5 == wall.transform.position.x && a.transform.position.y == wall.transform.position.y){
                    return true;
                }
            }

            
        }
        
        foreach(var black_box2 in black_boxes){

            if(black_box2.transform.position.x + 1 == playeriki.transform.position.x){
                continue;
            }
            else if(a.transform.position.x + 1 == black_box2.transform.position.x && a.transform.position.y == black_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x + 2 == black_box2.transform.position.x && a.transform.position.y == black_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x + 3 == black_box2.transform.position.x && a.transform.position.y == black_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x + 4 == black_box2.transform.position.x && a.transform.position.y == black_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x + 5 == black_box2.transform.position.x && a.transform.position.y == black_box2.transform.position.y){
                return true;
            }
        }

        foreach(var blue_box2 in blue_boxes){

            if(blue_box2.transform.position.x + 1 == playeriki.transform.position.x){
                continue;
            }
            else if(a.transform.position.x + 1 == blue_box2.transform.position.x && a.transform.position.y == blue_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x + 2 == blue_box2.transform.position.x && a.transform.position.y == blue_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x + 3 == blue_box2.transform.position.x && a.transform.position.y == blue_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x + 4 == blue_box2.transform.position.x && a.transform.position.y == blue_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x + 5 == blue_box2.transform.position.x && a.transform.position.y == blue_box2.transform.position.y){
                return true;
            }

        }

        foreach(var red_box2 in red_boxes){

            if(red_box2.transform.position.x + 1 == playeriki.transform.position.x){
                continue;
            }

            else if(a.transform.position.x + 1 == red_box2.transform.position.x && a.transform.position.y == red_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x + 2 == red_box2.transform.position.x && a.transform.position.y == red_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x + 3 == red_box2.transform.position.x && a.transform.position.y == red_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x + 4 == red_box2.transform.position.x && a.transform.position.y == red_box2.transform.position.y){
                return true;
            }
            else if(a.transform.position.x + 5 == red_box2.transform.position.x && a.transform.position.y == red_box2.transform.position.y){
                return true;
            }


        }
        foreach(var green_box2 in green_boxes){

            if(green_box2.transform.position.x + 1 == playeriki.transform.position.x){
                continue;
            }

            else if(a.transform.position.x + 1 == green_box2.transform.position.x && a.transform.position.y == green_box2.transform.position.y){
                return true;
            }

            else if(a.transform.position.x + 2 == green_box2.transform.position.x && a.transform.position.y == green_box2.transform.position.y){
                return true;
            }

            else if(a.transform.position.x + 3 == green_box2.transform.position.x && a.transform.position.y == green_box2.transform.position.y){
                return true;
            }

            else if(a.transform.position.x + 4 == green_box2.transform.position.x && a.transform.position.y == green_box2.transform.position.y){
                return true;
            }

            else if(a.transform.position.x + 5 == green_box2.transform.position.x && a.transform.position.y == green_box2.transform.position.y){
                return true;
            }

        }
          
        return false;
    }

    public void LeftWins(){

        if(!victory_sound_played){
            victory_sound.Play();
            victory_sound_played = true;
        }

        winner_announcer_text.text = "Player 1 Wins";
        winner_announcer_text.enabled = true;
        player1.can_move = false;
        player2.can_move = false;
        
        if(!firework_initiated){
            Instantiate(firework, new Vector3(0, 0, 0), Quaternion.identity);
            firework_initiated = true;
        }
        


    }

    public void RightWins(){

        if(!victory_sound_played){
            victory_sound.Play();
            victory_sound_played = true;
        }

        winner_announcer_text.text = "Player 2 Wins";
        winner_announcer_text.enabled = true;
        player1.can_move = false;
        player2.can_move = false;

        if(!firework_initiated){
            Instantiate(firework, new Vector3(0, 0, 0), Quaternion.identity);
            firework_initiated = true;
        }
    }

    public void NextLevelL(){
        
        Player1_reset();
    }

    public void NextLevelR(){
        
        Player2_reset();
    }

    public bool IsLevelCompleteL()
    {
        
        if(black_boxesP2.Length < 2 && blue_boxesP2.Length < 2 && green_boxesP2.Length < 2 && red_boxesP2.Length < 2 && thrown_at_least_onceL){
            
            
            return true;
        }


        return false;
    }

    public bool IsLevelCompleteR()
    {
        
        if(black_boxesP1.Length < 2 && blue_boxesP1.Length < 2 && green_boxesP1.Length < 2 && red_boxesP1.Length < 2 && thrown_at_least_onceR){
            
            
            return true;
        }


        return false;

    }
    
    IEnumerator ResetSceneAsync()
    {
        if (SceneManager.sceneCount > 1)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("LevelScene");
            while (!asyncUnload.isDone)
            {
                yield return null;
                
            }
            
            Resources.UnloadUnusedAssets();
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelScene", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
            
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene"));

    
        m_LevelBuilder2.BuildSpecificLevel(2);
        m_LevelBuilder.BuildSpecificLevel(2);
        player1 = FindObjectOfType<Player1>();
        player2 = FindObjectOfType<Player2>();
        

        
    }

    IEnumerator ResetSceneAsyncP1()
    {
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelScene", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
            
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene"));

    
        m_LevelBuilder2.BuildSpecificLevel(random_levels[0]);
        player1 = FindObjectOfType<Player1>();
        player2 = FindObjectOfType<Player2>();
        

        
    }

    IEnumerator ResetSceneAsyncP2()
    {
    
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelScene2", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
            
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene2"));

    
        m_LevelBuilder.BuildSpecificLevel(random_levels[0]);
        player1 = FindObjectOfType<Player1>();
        player2 = FindObjectOfType<Player2>();
        

        
    }

    IEnumerator ResetP1()
    {
        
        if (SceneManager.sceneCount > 1)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("LevelScene");
            while (!asyncUnload.isDone)
            {
                yield return null;
                
            }
            
            Resources.UnloadUnusedAssets();
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelScene", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
            
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene"));

    
        m_LevelBuilder2.BuildSpecificLevel(random_levels[levels_completedL]);
        player1 = FindObjectOfType<Player1>();
        player2 = FindObjectOfType<Player2>();
        

        
    }

    IEnumerator ResetP2()
    {

        if (SceneManager.sceneCount > 1)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("LevelScene2");
            while (!asyncUnload.isDone)
            {
                yield return null;
                
            }
            
            Resources.UnloadUnusedAssets();
        }
    
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelScene2", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
            
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene2"));

    
        m_LevelBuilder.BuildSpecificLevel(random_levels[levels_completedR]);
        player1 = FindObjectOfType<Player1>();
        player2 = FindObjectOfType<Player2>();
        

        
    }


    
}
