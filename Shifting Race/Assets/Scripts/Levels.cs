using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Levels")]
public class Levels : ScriptableObject
{
    public Level[] levels;
}

[System.Serializable]
public class Level
{
    public string name;
    public string[] items;
}