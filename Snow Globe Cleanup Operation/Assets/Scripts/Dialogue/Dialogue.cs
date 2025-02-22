using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    [Tooltip("캐릭터 이름")]
    public string name;

    [HideInInspector]
    public string[] context;
    
    [HideInInspector]
    public string[] spriteName;

}