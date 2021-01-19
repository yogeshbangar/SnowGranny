using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject mObjSki;
    public bool SKI
    {
        get { return mObjSki.activeInHierarchy; }
        set { mObjSki.SetActive(value); }
    }
}
