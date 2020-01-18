using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int ID { get; set; } 
    public string LevelName { get; set; } 
    public bool Completed { get; set; } 
    public int Stars { get; set; }
    public bool Locked { get; set; }
    public Level(int id,string levelName, bool completed, int stars,bool locked)
    {

    }

}
