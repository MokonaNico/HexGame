using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    //Le nom du joueur
    private string name;
    public string Name{
        get{return name;}
        set{name = value;}
    }

    //La couleur assigné au joueur (A CHANGER EN MATERIAL)
    private Color playerColor;
    public Color PlayerColor{
        get{return playerColor;}
    }

    /*Le numéro assigné au joueur,
        -Si c'est 0, c'est le joueur qui doit aller du haut vers bas
        -Si c'est 1, c'est le joueur qui doit aller de droite à gauche
    */
    private int num; 
    public int Num{
        get{return num;}
    }

    public Player (string name, Color playerColor, int num){
        this.name = name;
        this.playerColor = playerColor;
        this.num = num;
    }

    public override bool Equals(object obj){
        if (obj == null) return false;
        Player p = obj as Player;
        if ((object) p == null) return false;
        return (num == p.num);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
