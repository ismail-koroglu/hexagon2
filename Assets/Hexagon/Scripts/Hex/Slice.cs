using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

public class Slice : MonoBehaviour
{
    public int no;
    public int rotation;

    public Vector3 RotVector3 {
        get {
            var v = new Vector3(0, 0, rotation);
            return v;
        }
    }

    public Slice GetMappedSlice()
    {
        return transform.parent.parent.GetComponent<HexMono>().GetMappedSlice(no);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
