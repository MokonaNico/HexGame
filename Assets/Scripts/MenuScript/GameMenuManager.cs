using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameMenuManager : MonoBehaviour
{

    public Button backButton; //Bouton de retour au menu principal

    //Panel, bouton et toggle pour la règle du joueur 2
    public GameObject rulePlayerTwoPanel;
    public Button rulePlayerTwoButton;
    public Toggle toggleColorChange;

    //Panel et texte de l'écran de victoire
    public GameObject winPanel;
    public Text winText;

    public Text playerText; //Texte qui affiche le joueur qui joue actuellement
    
    public GameHandler game; 

    void Start()
    {
        backButton.onClick.AddListener(OnClickBackButton);
    }

    void OnClickBackButton(){
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void ShowWinMessage(string message){
        winText.text = message;
        winPanel.SetActive(true);
    }

    public void SetCurrentPlayerText(string playerName){
        playerText.text = playerName;
    }

    public void StartRulePlayerTwo(){
        rulePlayerTwoPanel.SetActive(true);
        rulePlayerTwoButton.onClick.AddListener(OnClickRulePlayerTwoButton);
    }

    void OnClickRulePlayerTwoButton(){
        rulePlayerTwoPanel.SetActive(false);
        game.stopRulePlayerTwo(toggleColorChange.isOn);
    }

}
