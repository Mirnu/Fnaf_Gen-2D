using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room")]
public class Room : ScriptableObject
{
    public int Camera;
    public List<Sprite> BackGrounds;
}
