using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int Score = 0;

    private void Start()
    {
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.SetInt("Score", Score = 0);
        GameManager.Instance.RefreshScore();
    }
}
