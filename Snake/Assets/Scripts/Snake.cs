using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    private Snake next;

    public void setNext(Snake inside)
    {
        next = inside;
    }

    public Snake getNext()
    {
        return next;
    }

    public void RemoveTail()
    {
        Destroy(this.gameObject);
    }
}
