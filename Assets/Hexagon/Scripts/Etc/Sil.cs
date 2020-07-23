using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sil : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartIe());

        IEnumerator StartIe()
        {
            yield return new WaitForSeconds(1);
          
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}