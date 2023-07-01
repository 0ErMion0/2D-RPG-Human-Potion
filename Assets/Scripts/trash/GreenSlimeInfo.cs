using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlimeInfo : MonoBehaviour
{
   //public string slimeName {get; set;}
   // public float hp {get; set;}
   // public float dmg {get; set;}
}

interface Slime
{
    public string SlimeName { get; set; }
    public float Hp { get; set; }
    public float Dmg { get; set; }
}

public class GreenSlime : Slime
{
    private string name;
    public string SlimeName
    {
        get { return name; }
        set { name = value; }
    }

    private float hp;
    public float Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    private float dmg;
    public float Dmg
    {
        get { return dmg; }
        set { dmg = value; }
    }
}
