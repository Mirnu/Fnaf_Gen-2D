using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Walk Point")]
public class WalkPoint : ScriptableObject
{
    public int Point;
    public bool isNearSecurity = false;
    public WalkPoint LastPoint;
    public List<WalkPoint> NextPaths;
}
