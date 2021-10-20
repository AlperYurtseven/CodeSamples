using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool m_OnCross;

    public AudioSource onCrossAudio;
    public AudioSource moveAudio;

    public Animator movement;

    public AudioSource snake_killing_sound;

    public AudioSource minotaur_killing_sound;

    public AudioSource teleport_sound;

    void Start(){

        movement.enabled = false;
        snake_killing_sound.volume = SaveManager.GetFloat("sfx_volume");
        minotaur_killing_sound.volume = SaveManager.GetFloat("sfx_volume");
        moveAudio.volume = SaveManager.GetFloat("sfx_volume");
        onCrossAudio.volume = SaveManager.GetFloat("sfx_volume");
        teleport_sound.volume = SaveManager.GetFloat("sfx_volume");


    }
    
    public bool Move(Vector2 direction)
    {

        GameObject[] snakes = GameObject.FindGameObjectsWithTag("Snake");

        GameObject[] minotaurs = GameObject.FindGameObjectsWithTag("Minotaur");

        if (BoxBlocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            
            transform.Translate(direction); 
            movement.enabled = true;
            movement.Play("");
            moveAudio.Play();
            //this.GetComponent<Animator>().enabled = false;
            if(TestForOnTransportationCenter()){
                teleport_sound.Play();
                Transportation();
            }
            foreach(var snake in snakes){
                if(transform.position.x == snake.transform.position.x && transform.position.y == snake.transform.position.y){
                    if(!snake.GetComponent<Snake>().isDead){
                        snake_killing_sound.Play();
                    }  
                    snake.GetComponent<Snake>().isDead = true;
                }
            }

            foreach(var minotaur in minotaurs){
                if(transform.position.x == minotaur.transform.position.x && transform.position.y == minotaur.transform.position.y){
                    if(!minotaur.GetComponent<Minotaur>().isDead){
                        minotaur.GetComponent<Minotaur>().killing_animation.Play("");
                        minotaur_killing_sound.Play();
                    }  
                    
                    minotaur.GetComponent<Minotaur>().isDead = true;
                    
                }
            }
            TestForOnCross();
            if(m_OnCross){
                onCrossAudio.Play();
            }
            return true;
        }
    }

    void TestForOnCross()
    {

        GameObject[] crosses = GameObject.FindGameObjectsWithTag("Cross");

        foreach (var cross in crosses)
        {
            if (transform.position.x == cross.transform.position.x && transform.position.y == cross.transform.position.y)
            {
                GetComponent<SpriteRenderer>().color = Color.green;
                m_OnCross = true;
                return;
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        m_OnCross = false;

    }

    public bool TestForOnTransportationCenter(){

        GameObject[] transportationCenters = GameObject.FindGameObjectsWithTag("TransportationCenter");

        foreach (var transportationCenter in transportationCenters)
        {
            if (transform.position.x == transportationCenter.transform.position.x && transform.position.y == transportationCenter.transform.position.y)
            {
                return true;
            }
        }
        
        return false;
    }

    private bool BoxBlocked(Vector3 position, Vector2 direction)
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
                
                return true;
            }
        }
        GameObject[] twopushboxes = GameObject.FindGameObjectsWithTag("TwoPushBox");
        foreach (var twopushbox in twopushboxes)
        {
            if (twopushbox.transform.position.x == newPos.x && twopushbox.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        GameObject[] uptocollisionboxes = GameObject.FindGameObjectsWithTag("UptoCollisionBox");
        foreach (var uptocollisionbox in uptocollisionboxes)
        {
            if (uptocollisionbox.transform.position.x == newPos.x && uptocollisionbox.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        GameObject[] sevenpushboxes = GameObject.FindGameObjectsWithTag("SevenPushBox");
        foreach (var sevenpushbox in sevenpushboxes)
        {
            if (sevenpushbox.transform.position.x == newPos.x && sevenpushbox.transform.position.y == newPos.y)
            {
                return true;
            }
        }

        return false;
    }

    void Transportation(){

        float x1,y1,x2,y2;

        GameObject[] transportationCenters = GameObject.FindGameObjectsWithTag("TransportationCenter");

        x1 = transportationCenters[0].transform.position.x;
        y1 = transportationCenters[0].transform.position.y;
        x2 = transportationCenters[1].transform.position.x;
        y2 = transportationCenters[1].transform.position.y;


        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        GameObject[] twopushboxes = GameObject.FindGameObjectsWithTag("TwoPushBox");
        GameObject[] uptocollisionboxes = GameObject.FindGameObjectsWithTag("UptoCollisionBox");
        GameObject[] sevenpushboxes = GameObject.FindGameObjectsWithTag("SevenPushBox");

       

        if(this.transform.position.x == x1 && this.transform.position.y == y1){

            foreach(var b in boxes){
                if(b.transform.position.x == x2 && b.transform.position.y == y2){
                    return;
                }
            }

            foreach(var tpb in twopushboxes){
                if(tpb.transform.position.x == x2 && tpb.transform.position.y == y2){
                    return;
                }
            }

            foreach(var ucb in uptocollisionboxes){
                if(ucb.transform.position.x == x2 && ucb.transform.position.y == y2){
                    return;
                }
            }

            foreach(var spb in sevenpushboxes){
                if(spb.transform.position.x == x2 && spb.transform.position.y == y2){
                    return;
                }
            }
            movement.enabled = false;
            transportationCenters[0].GetComponent<PortalEffect>().Teleport();

            Vector3 pos = transform.position;
            pos.x = x2;
            pos.y = y2;
            
            transform.position = pos;

            transportationCenters[1].GetComponent<PortalEffect>().Teleport();
            StartCoroutine(FadeImage(false));
            return;

        }

        else if(this.transform.position.x == x2 && this.transform.position.y == y2){


            foreach(var b in boxes){
                if(b.transform.position.x == x1 && b.transform.position.y == y1){
                    return;
                }
            }

            foreach(var tpb in twopushboxes){
                if(tpb.transform.position.x == x1 && tpb.transform.position.y == y1){
                    return;
                }
            }

            foreach(var ucb in uptocollisionboxes){
                if(ucb.transform.position.x == x1 && ucb.transform.position.y == y1){
                    return;
                }
            }

            foreach(var spb in sevenpushboxes){
                if(spb.transform.position.x == x1 && spb.transform.position.y == y1){
                    return;
                }
            }
            movement.enabled = false;
            transportationCenters[1].GetComponent<PortalEffect>().Teleport();
            Vector3 pos = transform.position;
            pos.x = x1;
            pos.y = y1;
            
            transform.position = pos;
            transportationCenters[0].GetComponent<PortalEffect>().Teleport();
            StartCoroutine(FadeImage(false));
            return;

        }
    }
    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, i);
                yield return null;
            }
        }
    }


}
