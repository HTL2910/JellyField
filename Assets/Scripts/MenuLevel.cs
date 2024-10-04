using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MenuLevels", menuName = "Game/Menu")]

public class MenuLevel :ScriptableObject
{
    public LevelController[] levelMenu;
}
