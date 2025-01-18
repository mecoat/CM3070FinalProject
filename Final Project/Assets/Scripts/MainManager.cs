using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//from https://learn.unity.com/tutorial/implement-data-persistence-between-scenes# accessed 18/1/24
public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
