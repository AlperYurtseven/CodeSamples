using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Sprite looking_left;

    public bool looks_left;

    public Sprite looking_right;

    public bool looks_right;

    public RuntimeAnimatorController animator_right;

    public RuntimeAnimatorController animator_left;

    //public bool is_movable;

    //public Sprite going_from_right_to_left;

    //public Sprite going_from_right_to_right;

    //public Sprite going_from_left_to_right;

    //public Sprite going_from_left_to_left;

    //public Sprite going_up;

    //public Sprite going_down;

    void Start()
    {
        looks_left = false;

        looks_right = true;

    }
    public bool Move(Vector2 direction)
    {

        if (Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            
            if(direction.x < 0){
                moved_left();
            }
            else if(direction.x > 0){
                moved_right();
            }

            else if(direction.y < 0){
                moved_down();
            }
            else if(direction.y > 0){
                moved_up();
            }
            
            transform.Translate(direction);

            return true;
        }
              

    }


    bool Blocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var wall in walls)
        {
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (var box in boxes)
        {
            if (box.transform.position.x == newPos.x && box.transform.position.y == newPos.y)
            {
                Box bx = box.GetComponent<Box>();
                if (bx && bx.Move(direction))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        GameObject[] twopushboxes = GameObject.FindGameObjectsWithTag("TwoPushBox");
        foreach (var twopushbox in twopushboxes)
        {
            if (twopushbox.transform.position.x == newPos.x && twopushbox.transform.position.y == newPos.y)
            {
                TwoPushBox tbx = twopushbox.GetComponent<TwoPushBox>();
                if (tbx && tbx.Move(direction))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        GameObject[] uptocollisionboxes = GameObject.FindGameObjectsWithTag("UptoCollisionBox");
        foreach (var uptocollisionbox in uptocollisionboxes)
        {
            if (uptocollisionbox.transform.position.x == newPos.x && uptocollisionbox.transform.position.y == newPos.y)
            {
                UptoCollisionBox ucbx = uptocollisionbox.GetComponent<UptoCollisionBox>();
                if (ucbx && ucbx.Move(direction))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        GameObject[] seven_push_boxes = GameObject.FindGameObjectsWithTag("SevenPushBox");
        foreach (var seven_push_box in seven_push_boxes)
        {
            if (seven_push_box.transform.position.x == newPos.x && seven_push_box.transform.position.y == newPos.y)
            {
                SevenPushBox spbx = seven_push_box.GetComponent<SevenPushBox>();
                if (spbx && spbx.Move(direction))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        GameObject[] snakes = GameObject.FindGameObjectsWithTag("Snake");
        foreach (var snake in snakes)
        {
            if (snake.transform.position.x == newPos.x && snake.transform.position.y == newPos.y)
            {
    
                if(snake.GetComponent<Snake>().isDead){
                    return false;
                }
                
                return true;
            }
        }

        GameObject[] minotaurs = GameObject.FindGameObjectsWithTag("Minotaur");
        foreach (var minotaur in minotaurs)
        {
            if (minotaur.transform.position.x == newPos.x && minotaur.transform.position.y == newPos.y)
            {
    
                if(minotaur.GetComponent<Minotaur>().isDead){
                    return false;
                }
                
                return true;
            }
        }

        return false;
    }

    void moved_left(){

        this.GetComponent<SpriteRenderer>().sprite = looking_left;
        this.GetComponent<Animator>().runtimeAnimatorController = animator_left;
        looks_right = false;
        looks_left = true;
        
        
    }

    void moved_right(){

        this.GetComponent<SpriteRenderer>().sprite = looking_right; 
        this.GetComponent<Animator>().runtimeAnimatorController = animator_right;
        looks_right = true;
        looks_left = false;
        
        //this.GetComponent<Animator>().enabled = true;

    }

    void moved_up(){

        if(looks_left){

            this.GetComponent<SpriteRenderer>().sprite = looking_left;
            this.GetComponent<Animator>().runtimeAnimatorController = animator_left;
            
        }
        else{
            this.GetComponent<SpriteRenderer>().sprite = looking_right;
            this.GetComponent<Animator>().runtimeAnimatorController = animator_right;
            //this.GetComponent<Animator>().enabled = true;
        }

    }

    void moved_down(){

        if(looks_left){
            this.GetComponent<SpriteRenderer>().sprite = looking_left;
            this.GetComponent<Animator>().runtimeAnimatorController = animator_left;
        }
        else{
            this.GetComponent<SpriteRenderer>().sprite = looking_right;
            this.GetComponent<Animator>().runtimeAnimatorController = animator_right;
            //this.GetComponent<Animator>().enabled = true;
        }
        
    }


}
