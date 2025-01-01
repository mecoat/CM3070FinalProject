using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// https://docs.unity3d.com/Manual/class-ScriptableObject.html
[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Levels : ScriptableObject
{

    public string levelName;

    [Header("Level Goals")]
    public int timer;
    public List<GameObject> spawnObjects;
    public List<GameObject> targetObjects;

}
