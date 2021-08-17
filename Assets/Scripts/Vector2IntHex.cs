using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector2IntHex 
{
    //La coordonnée x du vecteur
    private int x;
    public int X{
        get{return x;}
    }

    //La coordonnée y du vecteur
    private int y;
    public int Y{
        get{return y;}
    }

    //Différents vecteurs qui représente les différentes directions autour d'un hexagon
    private static Vector2IntHex right = new Vector2IntHex(1,0);
    public static Vector2IntHex Right {
        get{return right;}
    }
    private static Vector2IntHex left = new Vector2IntHex(-1,0);
    public static Vector2IntHex Left {
        get{return left;}
    }
    private static Vector2IntHex upLeft = new Vector2IntHex(-1,-1);
    public static Vector2IntHex UpLeft {
        get{return upLeft;}
    }
    private static Vector2IntHex upRight = new Vector2IntHex(0,-1);
    public static Vector2IntHex UpRight {
        get{return upRight;}
    }
    private static Vector2IntHex downLeft = new Vector2IntHex(0,1);
    public static Vector2IntHex DownLeft {
        get{return downLeft;}
    }
    private static Vector2IntHex downRight = new Vector2IntHex(1,1);
    public static Vector2IntHex DownRight{
        get{return downRight;}
    }


    public Vector2IntHex(int x, int y){
        this.x = x;
        this.y = y;
    }    
    
    public static bool operator == (Vector2IntHex v1, Vector2IntHex v2) {
        return (v1.x == v2.x && v1.y == v2.y);
    }

    public static bool operator != (Vector2IntHex v1, Vector2IntHex v2) {
        return (v1.x != v2.x && v1.y != v2.y);
    }

    public static Vector2IntHex operator + (Vector2IntHex v1, Vector2IntHex v2) {
        return new Vector2IntHex(v1.x + v2.x, v1.y + v2.y);
    }

    public static Vector2IntHex operator - (Vector2IntHex v1, Vector2IntHex v2) {
        return new Vector2IntHex(v1.x - v2.x, v1.y - v2.y);
    }

    public static Vector2IntHex operator * (Vector2IntHex v1, Vector2IntHex v2) {
        return new Vector2IntHex(v1.x * v2.x, v1.y * v2.y);
    }

    public static Vector2IntHex operator / (Vector2IntHex v1, Vector2IntHex v2) {
        return new Vector2IntHex(v1.x / v2.x, v1.y / v2.y);
    }

    //Méthode qui donne toutes les directions possibles autour d'un hexagon
    public static Vector2IntHex[] getAllDirections(){
        Vector2IntHex[] tab = {right,left,upLeft,upRight,downLeft,downRight};
        return tab;
    }

    //Version optimisée qui retourne une liste par rapport à la direction du joueur
    public static Vector2IntHex[] getAllDirections(Player p){
        Vector2IntHex[] tab = new Vector2IntHex[6];
        if (p.Num == 0){
            tab[0] = downLeft; tab[1] = downRight; tab[2] = right; tab[3] = left; tab[4] = upLeft; tab[5] = upRight;
        } else {
            tab[0] = left; tab[1] = upLeft; tab[2] = downLeft; tab[3] = upRight; tab[4] = downRight; tab[5] = right;
        }
        return tab;
    }

    public override bool Equals(object obj){
        if (obj == null) return false;
        Vector2IntHex v = obj as Vector2IntHex;
        if ((object) v == null) return false;
        return (x == v.x && y == v.y);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
