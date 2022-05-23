using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MapObject {
    public GameObject mapWrap;
    public PolygonCollider2D mapBounce;
    public Transform playerSpawn;
    public Transform maxX;
    public Transform maxY;
    public Transform minX;
    public Transform minY;
}
