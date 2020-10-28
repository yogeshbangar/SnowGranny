using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    Transform mTrans_Granny;
    Transform mTrans_GrannyBig;
    // Start is called before the first frame update
    void Start()
    {
        mTrans_Granny = GameObject.Find("Granny").transform;
        mTrans_GrannyBig = GameObject.Find("GrannyBig").transform;
        setGranny(M.NAME[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)||((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began)))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
            {
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            }
            if (Physics.Raycast(ray, out hit))
            {
                setGranny(hit.collider.name);
            }
            
            
        }
    }
    void setGranny(string str) {
        for (int i = 0; i < mTrans_GrannyBig.childCount; i++)
        {
            if(mTrans_GrannyBig.GetChild(i).name == str)
            {
                for (int j = 0; j < mTrans_GrannyBig.childCount; j++)
                {
                    mTrans_GrannyBig.GetChild(j).gameObject.SetActive(false);
                }
                mTrans_GrannyBig.GetChild(i).gameObject.SetActive(true);
                setcost(i);
            }
        }
    }
    void setcost(int no)
    {
        Transform shopMid = transform.Find("Shop").Find("Middile");
        if (M.GRANNYCOST[no] > 0)
        {
            shopMid.Find("cost").Find("Text").GetComponent<Text>().text = "    " + M.GRANNYCOST[no];
            shopMid.Find("cost").Find("Image").gameObject.SetActive(true);
        }
        else
        {
            shopMid.Find("cost").Find("Text").GetComponent<Text>().text = "PLAY";
            shopMid.Find("cost").Find("Image").gameObject.SetActive(false);
        }
        shopMid.Find("Name").GetComponent<Text>().text = ""+ M.NAME[no];
    }
    public void onClick(string val) {
        switch (val) {
            case "selNoti":
                break;
            case "selCoin":
                break;
            case "selLeader":
                break;
            case "selGranny":
                setScreen(1);
                break;
            case "selSki":
                break;
            case "selShop":
                break;
        }
    }
    void setScreen(int scr) {
        for (int i = 0; i < transform.childCount;i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(scr).gameObject.SetActive(true);
    }
}
