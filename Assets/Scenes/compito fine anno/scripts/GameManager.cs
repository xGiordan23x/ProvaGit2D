using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISubscriber
{
    public static GameManager Instance { get; private set; }
    public TextMeshProUGUI scoreText;
    private string messageName = "GameManager";
    private int powerUpId =0;
    private int treasureId;
    private int keyId = 0;
  
   
        private void Awake()
        {         
          
        if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

        
    }
    
    public void OnNotify(object content)
    {
        if (content is PlayerTileController)
        {
            PubSub.Instance.SendMessage("Enemy", this);
        }

        else if (content is EnemyTileController)
        {

            PubSub.Instance.SendMessage("Player", this);
        }
        else if (content is Treasure)
        {
            Debug.Log("Treasure Found");
        }

        else if (content is Key)
        {
            Debug.Log("Key grabbed");
            PubSub.Instance.SendMessage("Player", content);


        }
        else if (content is PowerUp)
        {
            Debug.Log("enemy stunned");

            PubSub.Instance.SendMessage("Enemy", content);
        }
        else if(content is Exit)
        {
            Debug.Log("Gioco finito");
            SceneManager.LoadScene(1);
        }
    }

    public void RefreshScore()
    {
       scoreText.text = ScoreManager.Score.ToString();
    }

    void Start()
    {
        PubSub.Instance.RegisteredSubscriber(messageName, this);
        PubSub.Instance.SendMessage("Player", this);
        RefreshScore();
        

    }

    internal void GetPowerUpMessageName(PowerUp p)
    {              
        p.id = powerUpId;
        p.messageName = "PowerUp" + p.id;
        powerUpId++;
        
    }
    internal void GetTreasureMessageName(Treasure t)
    {
        t.id = treasureId;
        t.messageName = "Treasure" + t.id;
        treasureId++;

    }
    internal void GetKeyMessageName(Key k)
    {
        k.id = keyId;
        k.messageName = "Key" + k.id;
        keyId++;

    }

    internal void IncreaseScore(int scoreToGive)
    {
        ScoreManager.Score = PlayerPrefs.GetInt("Score");
        ScoreManager.Score += scoreToGive;
        PlayerPrefs.SetInt("Score",ScoreManager.Score);
        RefreshScore();
    }
}
