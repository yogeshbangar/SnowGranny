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

    public void setSki(GameObject ski)
    {
        switch (M.PNO)
        {
            case 0:
                setSki_0(ski);
                break;
            case 1:
                setSki_1(ski);
                break;
            case 2:
                setSki_2(ski);
                break;
            case 3:
                setSki_3(ski);
                break;
            case 4:
                setSki_4(ski);
                break;
            case 5:
                setSki_5(ski);
                break;
        }
    }
    public void setSki_0(GameObject ski) {
        Debug.Log("mObjSki.transform.childCount = " + mObjSki.transform.childCount);
        for (int i = 0; i < mObjSki.transform.childCount; i++)
        {
            Destroy(mObjSki.transform.GetChild(i).gameObject);
        }
        GameObject obj = (GameObject)Instantiate(ski, new Vector3(0, 0, 0), Quaternion.identity);
        obj.transform.parent = mObjSki.transform;
        obj.transform.localPosition = Vector3.zero;
        if (M.SKINO == 2 || M.SKINO == 4)
            obj.transform.localPosition = new Vector3(-.24f, -.01f, 0.06f);
        obj.transform.localRotation = Quaternion.Euler(new Vector3(80, 180, -193));
        obj.transform.localScale = new Vector3(1, 1, 1);

    }
    public void setSki_1(GameObject ski)
    {
        Debug.Log("mObjSki.transform.childCount = " + mObjSki.transform.childCount);
        for (int i = 0; i < mObjSki.transform.childCount; i++)
        {
            Destroy(mObjSki.transform.GetChild(i).gameObject);
        }
        GameObject obj = (GameObject)Instantiate(ski, new Vector3(0, 0, 0), Quaternion.identity);
        obj.transform.parent = mObjSki.transform;
        obj.transform.localPosition = new Vector3(-.08f, -.163f, -.028f);
        if (M.SKINO == 2 || M.SKINO == 4)
            obj.transform.localPosition = new Vector3(0, -.15f, 0);
        obj.transform.localRotation = Quaternion.Euler(new Vector3(-170, 0, -135));
        obj.transform.localScale = new Vector3(.2f, .2f, .2f);
    }
    public void setSki_2(GameObject ski)
    {
        Debug.Log("mObjSki.transform.childCount = " + mObjSki.transform.childCount);
        for (int i = 0; i < mObjSki.transform.childCount; i++)
        {
            Destroy(mObjSki.transform.GetChild(i).gameObject);
        }
        GameObject obj = (GameObject)Instantiate(ski, new Vector3(0, 0, 0), Quaternion.identity);
        obj.transform.parent = mObjSki.transform;
        obj.transform.localPosition = new Vector3(-.08f, -.163f, -.028f);
        if (M.SKINO == 2 || M.SKINO == 4)
            obj.transform.localPosition = new Vector3(0, -.15f, 0);
        obj.transform.localRotation = Quaternion.Euler(new Vector3(-170, 0, -135));
        obj.transform.localScale = new Vector3(.4f, .4f, .4f);
    }
    public void setSki_4(GameObject ski)
    {
        Debug.Log("mObjSki.transform.childCount = " + mObjSki.transform.childCount);
        for (int i = 0; i < mObjSki.transform.childCount; i++)
        {
            Destroy(mObjSki.transform.GetChild(i).gameObject);
        }
        GameObject obj = (GameObject)Instantiate(ski, new Vector3(0, 0, 0), Quaternion.identity);
        obj.transform.parent = mObjSki.transform;
        obj.transform.localPosition = new Vector3(-.08f, -.153f, -.028f);
        if (M.SKINO == 2 || M.SKINO == 4)
            obj.transform.localPosition = new Vector3(0, -.15f, 0);
        obj.transform.localRotation = Quaternion.Euler(new Vector3(-170, 0, -135));
        obj.transform.localScale = new Vector3(.15f, .15f, .15f);
    }
    public void setSki_3(GameObject ski)
    {
        Debug.Log("mObjSki.transform.childCount = " + mObjSki.transform.childCount);
        for (int i = 0; i < mObjSki.transform.childCount; i++)
        {
            Destroy(mObjSki.transform.GetChild(i).gameObject);
        }
        GameObject obj = (GameObject)Instantiate(ski, new Vector3(0, 0, 0), Quaternion.identity);
        obj.transform.parent = mObjSki.transform;
        obj.transform.localPosition = new Vector3(-.21f, -.7f, -.07f);
        if (M.SKINO == 2 || M.SKINO == 4)
            obj.transform.localPosition = new Vector3(-.1f, -.9f, -.2f);
        obj.transform.localRotation = Quaternion.Euler(new Vector3(-4, 180, 45));
        obj.transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
    }
    public void setSki_5(GameObject ski)
    {
        Debug.Log("mObjSki.transform.childCount = " + mObjSki.transform.childCount);
        for (int i = 0; i < mObjSki.transform.childCount; i++)
        {
            Destroy(mObjSki.transform.GetChild(i).gameObject);
        }
        GameObject obj = (GameObject)Instantiate(ski, new Vector3(0, 0, 0), Quaternion.identity);
        obj.transform.parent = mObjSki.transform;
        obj.transform.localPosition = new Vector3(-.13f, -.17f, -.0f);
        if (M.SKINO == 2 || M.SKINO == 4)
            obj.transform.localPosition = new Vector3(-.06f, -.2f, -.03f);
        obj.transform.localRotation = Quaternion.Euler(new Vector3(-4, 180, 45));
        obj.transform.localScale = new Vector3(.4f, .4f, .4f);
    }

}
