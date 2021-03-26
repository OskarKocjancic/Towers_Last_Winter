using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeatScriptableObject", menuName = "ScriptableObjects/beat")]
public class ScriptableBeat : ScriptableObject
{
    public ColorCoding.beatColor color; 
    public AudioClip audioGenerate;
    public AudioClip audioDissolve;
    public Sprite sprite;
}
