using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameMenuCreatorManager : MonoBehaviour
{
    public Button startButton;

    public static string player1Name;
    public static string player2Name;

    public InputField inputFieldPlayer1;
    public InputField inputFieldPlayer2;

    void Start(){
        startButton.onClick.AddListener(OnClickStart);
    }

    void OnClickStart(){
        player1Name = inputFieldPlayer1.text;
        player2Name = inputFieldPlayer2.text;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
