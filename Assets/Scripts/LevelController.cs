using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Game/Level")]
public class LevelController : ScriptableObject
{
    public int level;   
    public int width;   
    public int height;  
}
