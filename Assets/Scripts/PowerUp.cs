using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp")]
public class PowerUp : ScriptableObject
{
    public string name;
    public float time;
    public bool timeable = false;
}
