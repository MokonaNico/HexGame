using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    private int x; //La position x de la cellule
    public int X {
        get{return x;}
        set{x = value;}
    }
    private int y; //La position y de la cellule
    public int Y {
        get{return y;}
        set{y = value;}
    }
    private bool activated = false; //Le statut de la cellule (activé ou pas)
    public bool Activated {
        get{return activated;}
    }
    private Player playerActive; //Le player qui a activé la cellule
    public Player PlayerActive{
        get{return playerActive;}
    }
    private GameHandler game; //Le gestionnaire du jeu
    public GameHandler Game{
        set{game = value;}
    }

    //Méthode qui s'exécute seulement si le bouton de la souris est activé
    void OnMouseDown(){
        if(!activated && game.GameIsOn){
            activate(game.getActualPlayer());
            game.nextTurn();
        }
    }

    //Active la cellule
    public void activate(Player p) {
        this.activated = true;
        gameObject.GetComponent<SpriteRenderer>().color = p.PlayerColor;
        this.playerActive = p;
    }

    public override bool Equals(object obj){
        if (obj == null) return false;
        HexCell otherCell = obj as HexCell;
        if ((object) otherCell == null) return false;
        return(x == otherCell.x && y == otherCell.y);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}