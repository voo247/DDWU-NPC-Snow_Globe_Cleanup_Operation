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

    [HideInInspector]
    public string[] soundName;
    
    [HideInInspector]
    public string[] eventName;
    
    [HideInInspector]
    public string[] backgroundName;
}