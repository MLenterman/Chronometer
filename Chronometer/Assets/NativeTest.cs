using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class NativeTest : MonoBehaviour
{

    [DllImport("ChronometerNative")]
    public static extern int GetRandom();

    void Start()
    {
        Debug.Log(GetRandom());
    }
}
