using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// https://docs.unity3d.com/Manual/class-ScriptableObject.html
[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Levels : ScriptableObject
{
    //String for Level Name
    public string levelName;

    //Level Goals
    [Header("Level Goals")]
    //how much time for this level
    public int timer;
    //which objects to spawn
    public List<GameObject> spawnObjects;
    //which objects are targets
    public List<GameObject> targetObjects;

}
