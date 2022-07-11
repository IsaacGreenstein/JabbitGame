using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCamera : MonoBehaviour
{
    public void NextHole()
    {
        transform.Translate(21.77f, 0, 0);
    }
}
