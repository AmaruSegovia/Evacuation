using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character")]

public class Characters : ScriptableObject
{
    public GameObject characterJugable;
    public Sprite imagen;               // Imagen que se mostrara en la UI
    public string characterName;
    
    [TextArea(3, 5)]
    public string description;
}
