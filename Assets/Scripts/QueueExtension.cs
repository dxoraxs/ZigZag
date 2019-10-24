using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QueueExtension
{
    public static Transform CharCount(this Queue<GameObject> queueGameObjects)
    {
        return queueGameObjects.Peek().transform;
    }
}
