using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Button startButton;

    void Start(){
        startButton.onClick.AddListener(OnClickStart);
    }

    void OnClickStart(){
        SceneManager.LoadScene("GameCreator", LoadSceneMode.Single);
    }
}
