using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameHandler : MonoBehaviour
{
    private bool gameIsOn = true; //Variable qui indique si le jeu est en cours ou non
    public bool GameIsOn{
        get { return gameIsOn; }
    }
    [SerializeField]
    private int size = 11; //La taille du plateau
    private int currentPlayer = 0; //Le numero du joueur qui est entrain de jouer
    private bool rulePlayer2 = true; //La règle spécial du joueur 2
    private Player[] playerTab = new Player[2]; //Un tableau des deux joueurs de la partie
    private GameObject[,] board; //Tableau à deux dimensions qui stock les cellules du plateau

    [SerializeField]
    private GameObject Hexagon = null; //Le prefab des cellules
    [SerializeField]
    private GameObject cameraObject = null; //Le prefab de la camera
    [SerializeField]
    private GameObject borderSquare = null; //Le prefab des bordures du plateau
    [SerializeField]
    private GameMenuManager gameMenu = null; //Le gestionnaire de l'interface du plateau

    //Methode qui crée un plateau en fonction d'une taille donnée :
    private void createBoard(){

        //Création du plateau, instancie les cellules et les assignes dasn le tableau board
        board = new GameObject[size,size];
        double decalage = 0; 
        for(int y = 0; y < size;y++){
            for(int x = 0; x < size;x++){
                float posx = (float) ((x * Mathf.Sqrt(3) * 0.5) + decalage) ;
                float posy = (float) ((y * 0.75)) ;
                GameObject actualObject = Instantiate(Hexagon, new Vector2(posx,posy),Quaternion.identity) as GameObject;
                board[(size-1-y),x] = actualObject;
                actualObject.name = "Hex " +x + " " + (size-1-y);
                actualObject.GetComponent<HexCell>().X = x;
                actualObject.GetComponent<HexCell>().Y = (size-1-y);
                actualObject.GetComponent<HexCell>().Game = this;
            }
            decalage += Mathf.Sqrt(3) * 0.25;
        }

        //Recherche du milieu du plateau
        int mid = (size/2);
        float midx = (mid * Mathf.Sqrt(3) * 0.75f);
        float midy = mid * 0.75f;
        if(size%2 == 0){
            midx -= (Mathf.Sqrt(3) * 0.75f) / 2;
            midy -= 0.75f / 2;
        }

        //Instanciation de la caméra
        GameObject cam =  Instantiate(cameraObject, new Vector3(midx,midy,-10f),Quaternion.identity) as GameObject;
        cam.GetComponent<Camera>().orthographicSize  = (size*0.85f)/2.0f; 

        //Instanciation des bordures du plateau
        GameObject boarderOne = Instantiate(borderSquare, new Vector2(midx,midy),Quaternion.Euler(0,0,60)) as GameObject;
        boarderOne.GetComponent<SpriteRenderer>().color = playerTab[0].PlayerColor;
        GameObject boarderTwo = Instantiate(borderSquare, new Vector2(midx,midy),Quaternion.identity) as GameObject;
        boarderTwo.GetComponent<SpriteRenderer>().color = playerTab[1].PlayerColor;
    }

    //Méthode qui indique si la position est à l'extérieur du plateau
    private bool inBound(Vector2IntHex position){
        return ((0 <= position.X && position.X < size) && (0 <= position.Y && position.Y < size));
    }

    //Vérifie si un joueur est dans une position de victoire
    private bool checkWins(Player player){
        Vector2IntHex pos;
        ArrayList checkedCells = new ArrayList();
        for(int i = 0; i < size;i++){
            if(player.Num == 0){
                pos = new Vector2IntHex(i,0);
            } else {
                pos = new Vector2IntHex(0,i);
            }
            HexCell cell = board[pos.Y,pos.X].GetComponent<HexCell>();
            if (voyager(cell,pos,checkedCells,player)){
                return true;
            }
        }
        return false;
    }

    //Voyage de cellule en cellule pour essayer de parvenir à la fin du plateau
    private bool voyager(HexCell cell, Vector2IntHex pos, ArrayList checkedCells, Player player){
        bool condition = cell.Activated && player.Equals(cell.PlayerActive) && !checkedCells.Contains(pos);
        if (condition &&  ((player.Num == 0 && pos.Y == size-1) || (player.Num == 1 && pos.X == size-1))) 
            return true;
        else if(condition){
            checkedCells.Add(pos);
            foreach(Vector2IntHex direction in Vector2IntHex.getAllDirections(player)){
                Vector2IntHex currentPos = pos + direction;
                if(inBound(currentPos)){
                    cell = board[currentPos.Y,currentPos.X].GetComponent<HexCell>();
                    if (voyager(cell, currentPos, checkedCells, player))
                        return true;
                    else continue;
                } else continue;
            }
        } else return false;
        return false;
    }

    //Passe au joueur suivant
    private void nextPlayer(){
        if(currentPlayer == 0) {
            currentPlayer = 1;
        } else {
            currentPlayer = 0;
        }
        gameMenu.SetCurrentPlayerText(getActualPlayer().Name); //Mets à jour le texte de l'ui
    }

    //Retourne le joueur qui joue actuellement
    public Player getActualPlayer(){
        return playerTab[currentPlayer];
    }

    public void stopRulePlayerTwo(bool isChanging){
        if(isChanging){
            string tempPlayerName = playerTab[0].Name;
            playerTab[0].Name = playerTab[1].Name;
            playerTab[1].Name = tempPlayerName;
        }
        gameIsOn = true;
        rulePlayer2 = false;
        gameMenu.SetCurrentPlayerText(getActualPlayer().Name);
    }

    //Passe au tour suivant
    public void nextTurn(){
        //Vérifie si un joueur est en position de victoire
        if(checkWins(getActualPlayer())){
            gameMenu.ShowWinMessage("GG " + getActualPlayer().Name);
            gameIsOn = false;
            return;
        }

        nextPlayer(); //Passe au joueur suivant

        //Vérifie si on doit appliquer la règle du joueur 2
        if(rulePlayer2 && currentPlayer == 1){
            gameIsOn = false;
            gameMenu.StartRulePlayerTwo();
        }
    }

    //Lancement de la partie
    void Start()
    {
        playerTab[0] = new Player(GameMenuCreatorManager.player1Name,Color.black,0);
        playerTab[1] = new Player(GameMenuCreatorManager.player2Name,Color.white,1);
        createBoard();
        gameMenu.SetCurrentPlayerText(getActualPlayer().Name); //Mets le texte de l'ui au premier joueur
    }
}
