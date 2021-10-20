using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.Serializable]
public class LevelElement
{
    public string m_Character;
    public GameObject m_Prefab;
    public GameObject m_Prefab_gopnik;
}

public class LevelBuilder : MonoBehaviour
{
    public int m_CurrentLevel;
    public List<LevelElement> m_LevelElements;
    private Level m_Level;

    /*
    public int GetCurrentLevel()
    {
        return m_CurrentLevel;
    }
    */

    GameObject GetPrefab(char c)
    {
        
        LevelElement levelElement = m_LevelElements.Find(le => le.m_Character == c.ToString());
        if(PlayerPrefs.GetString("Username") == "rasputin"){
           if (levelElement != null)
            return levelElement.m_Prefab_gopnik;
        else
            return null;
        }
        else{
            if (levelElement != null)
                return levelElement.m_Prefab;
            else
                return null;
        }
       

    }

    public void NextLevel()
    {
        string temp = "Level" + m_CurrentLevel.ToString();
        PlayerPrefs.SetInt(temp, 1);
        m_CurrentLevel++;
        /*
        if(m_CurrentLevel >= GetComponent<Levels>().m_Levels.Count)
        {
            m_CurrentLevel = 0;
        }
        */
    }

    public void Build()
    {
        if(m_CurrentLevel == 18)
        {
            SceneManager.LoadScene("GameOver");
        }
        
        if (PlayerPrefs.HasKey("wanted_Level")){

            m_CurrentLevel = PlayerPrefs.GetInt("wanted_Level");

        }

        else if (PlayerPrefs.HasKey("last_Level"))
        {
            m_CurrentLevel = PlayerPrefs.GetInt("last_Level");
        }

        PlayerPrefs.DeleteKey("wanted_Level");
        
        m_Level = GetComponent<Levels>().m_Levels[m_CurrentLevel];

        int startx = (-m_Level.Width / 2)+1;
        int x = startx;
        int y = -m_Level.Height / 2;

        foreach(var row in m_Level.m_Rows)
        {

            foreach(var ch in row)
            {
                //Debug.log(ch);

                GameObject prefab = GetPrefab(ch);
                if (prefab)
                {
                    //Debug.log(prefab.name);

                    Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);

                }
                x++;


            }
            y++;
            x = startx;

        }
    }

}
