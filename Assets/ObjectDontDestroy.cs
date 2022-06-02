using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDontDestroy : MonoBehaviour
{
    public List<GameObject> objects;
    private void Awake()
    {
        foreach (GameObject obj in objects)
        {
            DontDestroyOnLoad(obj);
        }
    }
}
