﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public Transform mTrans_GrannyShop;
    Transform mTrans_Granny;
    Transform mTrans_GrannyBig;
    Transform mTrans_SkiBig;
    Transform mTrans_Ski;
    Transform mTrans_Particle;
    Transform mCanvasMenu;
    
    public Sprite[] butSprite;
    public Transform mGame;
    int shopPNO = 0;
    int skiNo = 0;
    Text gScore, gCoin;
    Image iStar, iMegnet;
    bool isResume = false;
    // Start is called before the first frame update
    void Start()
    {
        mTrans_Granny = mTrans_GrannyShop.Find("Granny").transform;
        mTrans_GrannyBig = mTrans_GrannyShop.Find("GrannyBig").transform;
        mTrans_SkiBig = mTrans_GrannyShop.Find("SkiBig").transform;
        mTrans_Ski = mTrans_GrannyShop.Find("Ski").transform;
        mCanvasMenu = GameObject.Find("MenuCanvas").transform;
        mTrans_Particle = GameObject.Find("ParticleSystem").transform;
        for (int i=0;i< mTrans_GrannyShop.childCount; i++){
            mTrans_GrannyShop.GetChild(i).gameObject.SetActive(false);
        }
        mTrans_GrannyShop.gameObject.SetActive(false);
        gScore = transform.GetChild(5).Find("top").Find("Score").Find("score").GetComponent<Text>();
        gCoin = transform.GetChild(5).Find("top").Find("Coin").Find("coin").GetComponent<Text>();
        iMegnet= transform.GetChild(5).Find("base").Find("magnet").Find("power").GetComponent<Image>();
        iStar = transform.GetChild(5).Find("base").Find("star").Find("power").GetComponent<Image>();
        M.Open();
        AdManager.Instance.Init();
        setScreen(M.GAMEMENU);
        
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
                if(M.GameScreen == M.GAMEGRANY)
                    setGranny(hit.collider.name);
                else
                    setSki(hit.collider.name);
            }
        }
        if(M.GameScreen == M.GAMESKI)
        {
            mTrans_SkiBig.Rotate(Vector3.up * Time.deltaTime*30);
        }
        if(M.GameScreen == M.GAMEPLAY)
        {
            
            updateGameplay();
        }
    }
    void updateGameplay() {
        gCoin.text = M.GCOIN+"";
        gScore.text = M.GSOCRE + "";
        M.GSOCRE++;
        if(M.coinPower > 0)
        {
            iMegnet.fillAmount = M.coinPower / (M.MAXPCOIN * M.MAXPOWER);
            M.coinPower--;
            iMegnet.transform.parent.gameObject.SetActive(M.coinPower > 0);
        }
        if (M.starPower> 0)
        {
            iStar.fillAmount = M.starPower / (M.MAXPSTAR * M.MAXPOWER);
            M.starPower--;
            iStar.transform.parent.gameObject.SetActive(M.starPower > 0);
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
                break;
            }
        }
    }
    void setcost(int no)
    {
        shopPNO = no;
        Transform GrannyMid = transform.Find("Granny").Find("Middile");
        if (M.GRANNYCOST[no] > 0)
        {
            GrannyMid.Find("cost").Find("Text").GetComponent<Text>().text = "    " + M.GRANNYCOST[no];
            GrannyMid.Find("cost").Find("Image").gameObject.SetActive(true);
        }
        else
        {
            GrannyMid.Find("cost").Find("Text").GetComponent<Text>().text = "PLAY";
            GrannyMid.Find("cost").Find("Image").gameObject.SetActive(false);
        }
        GrannyMid.Find("Name").GetComponent<Text>().text = ""+ M.NAME[no];
        GrannyMid.Find("granny").GetComponent<Text>().text = "Granny";
    }
    void setSki(string str)
    {
        for (int i = 0; i < mTrans_SkiBig.childCount; i++)
        {
            if (mTrans_SkiBig.GetChild(i).name == str)
            {
                for (int j = 0; j < mTrans_SkiBig.childCount; j++)
                {
                    mTrans_SkiBig.GetChild(j).gameObject.SetActive(false);
                }
                mTrans_SkiBig.GetChild(i).gameObject.SetActive(true);
                setSkiCost(i);
            }
        }
    }
    void setSkiCost(int no)
    {
        skiNo = no;
        Debug.Log("skiNo "+ skiNo);
        Transform GrannyMid = transform.Find("Granny").Find("Middile");
        if (M.SKICOST[no] > 0)
        {
            GrannyMid.Find("cost").Find("Text").GetComponent<Text>().text = "    " + M.SKICOST[no];
            GrannyMid.Find("cost").Find("Image").gameObject.SetActive(true);
        }
        else
        {
            GrannyMid.Find("cost").Find("Text").GetComponent<Text>().text = "PLAY";
            GrannyMid.Find("cost").Find("Image").gameObject.SetActive(false);
        }
        GrannyMid.Find("Name").GetComponent<Text>().text = "" + M.SKINAME[no];
        GrannyMid.Find("granny").GetComponent<Text>().text = "" + M.SKISUBNAME[no];
    }
    public void onClick(string val) {
        switch (val) {
            case "selNoti":
                setScreen(M.GAMEOVER);
                break;
            case "selCoin":
                break;
            case "selLeader":
                setScreen(M.GAMEACHIV);
                break;
            case "selGranny":
                setScreen(M.GAMEGRANY);
                break;
            case "selSki":
                setScreen(M.GAMESKIUP);
                break;
            case "selShop":
                setScreen(M.GAMEBUY);
                break;
            case "Taptoplay":
                isResume = false;
                setScreen(M.GAMEPLAY);
                break;
        }
    }
    public void onClickGranny(string val)
    {
        switch (val)
        {
            case "setting":
                break;
            case "doller":
                break;
            case "plus":
                setScreen(M.GAMEBUY);
                break;
            case "sel1":
                setScreen(M.GAMEGRANY);
                break;
            case "sel2":
                break;
            case "sel3":
                break;
            case "cost":
                if (M.GameScreen == M.GAMESKI)
                {
                    if (M.SKICOST[skiNo] > M.COINS)
                    {
                        setPopUp(true);
                    }
                    else
                    {
                        if (M.SKICOST[skiNo] > 0)
                        {
                            M.COINS -= M.SKICOST[skiNo];
                            M.SKICOST[skiNo] = 0;
                            setSkiCost(skiNo);
                            M.Save();
                        }
                        else
                        {
                            M.SKINO = skiNo;
                            setScreen(M.GAMEPLAY);
                        }
                    }
                }
                else
                {
                    if (M.GRANNYCOST[shopPNO] > M.COINS)
                    {
                        setPopUp(true);
                    }
                    else
                    {
                        if (M.GRANNYCOST[shopPNO] > 0)
                        {
                            M.COINS -= M.GRANNYCOST[shopPNO];
                            M.GRANNYCOST[shopPNO] = 0;
                            M.Save();
                            setcost(shopPNO);
                        }
                        else
                        {
                            M.PNO = shopPNO;
                            setScreen(M.GAMEPLAY);
                        }
                       
                    }
                }
                break;
            case "help":
                break;
            case "leader":
                break;
            case "ski":
                setScreen(M.GameScreen == M.GAMESKI ? M.GAMEGRANY : M.GAMESKI);
                break;
            case "home":
                setScreen(M.GAMEMENU);
                break;
            case "later":
            case "buy":
                setPopUp(false);
                if (val == "buy")
                {
                    setScreen(M.GAMEBUY);
                }
                break;
                
        }
    }
    public void setPopUp(bool visible)
    {
        transform.Find("PopUp").gameObject.SetActive(visible);
    }
    public void onClickSkiUp(string val)
    {
        switch (val)
        {
            case "Home":
                setScreen(M.GAMEMENU);
                break;
            case "doller":
                break;
            case "plus":
                setScreen(M.GAMEBUY);
                break;
            case "ROSSIGNO":
                break;
            case "VOLKL RTM":
                if (M.SKICOST[1] > M.COINS)
                {
                    setPopUp(true);
                }
                else
                {
                    M.COINS -= M.SKICOST[1];
                    M.SKICOST[1] = 0;
                    setScreen(M.GAMESKIUP);
                    M.Save();
                }
                break;
            case "Atomic":
                if (M.SKICOST[2] > M.COINS)
                {
                    setPopUp(true);
                }
                else
                {
                    M.COINS -= M.SKICOST[2];
                    M.SKICOST[2] = 0;
                    setScreen(M.GAMESKIUP);
                    M.Save();
                }
                break;
            case "Kit Deco":
                if (M.SKICOST[3] > M.COINS)
                {
                    setPopUp(true);
                }
                else
                {
                    M.COINS -= M.SKICOST[3];
                    M.SKICOST[3] = 0;
                    setScreen(M.GAMESKIUP);
                    M.Save();
                }
                break;
            case "Nitro":
                if (M.SKICOST[4] > M.COINS)
                {
                    setPopUp(true);
                }
                else
                {
                    M.COINS -= M.SKICOST[4];
                    M.SKICOST[4] = 0;
                    setScreen(M.GAMESKIUP);
                    M.Save();
                }
                break;
        }
    }
    public void onClickPause(string val) {
        switch (val)
        {
            case "pause":
                setScreen(M.GAMEPAUSE);
                break;
            case "resume":
                isResume = true;
                setScreen(M.GAMEPLAY);
                break;
        }
    }
    public void callBuy() {
        setScreen(M.GAMEBUY);
    }
    public void setScreen(int scr) {
        int no = scr;
        M.GameScreen = scr;
        for (int i = 0; i < transform.childCount;i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < mTrans_GrannyShop.childCount; i++)
        {
            mTrans_GrannyShop.GetChild(i).gameObject.SetActive(false);
        }
        mTrans_GrannyShop.gameObject.SetActive(false);
        mTrans_Particle.gameObject.SetActive(false);
        mCanvasMenu.gameObject.SetActive(false);
        mGame.gameObject.SetActive(false);
        Debug.Log("~~~~~~GAMESKIUP~~~"+M.GameScreen);
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        Camera.main.transform.position = new Vector3(0, 1.7f, -10);
        switch (M.GameScreen)
        {
            case M.GAMEMENU:
                no = 0;
                mCanvasMenu.gameObject.SetActive(true);
                mTrans_Particle.gameObject.SetActive(true);
                Transform MGrannyBig = mCanvasMenu.Find("GrannyBig").transform;
                int rand = Random.Range(0, MGrannyBig.childCount);
                for (int i=0;i< MGrannyBig.childCount; i++)
                {
                    MGrannyBig.GetChild(i).gameObject.SetActive(i == rand);
                }
                Transform MGrannySki = mCanvasMenu.Find("GrannySki").transform;
                rand = Random.Range(0, MGrannySki.childCount);
                for (int i = 0; i < MGrannySki.childCount; i++)
                {
                    MGrannySki.GetChild(i).gameObject.SetActive(i == rand);
                }

                break;
            case M.GAMESKIUP:
                no = 2;
                mTrans_Particle.gameObject.SetActive(true);
                transform.GetChild(no).GetChild(1).GetChild(2).GetChild(1).GetChild(1).GetComponent<Text>().text = "" + M.COINS;
                Transform value = transform.GetChild(no).GetChild(2).GetChild(1);
                for(int i =1; i< value.childCount; i++)
                {
                    if (M.SKICOST[i] == 0)
                    {
                        value.GetChild(i).GetChild(2).GetChild(1).GetComponent<Text>().text = "Use";
                    }
                }
                break;
            case M.GAMEGRANY:
                no = 1;
                mTrans_GrannyShop.gameObject.SetActive(true);
                mTrans_GrannyShop.GetChild(0).gameObject.SetActive(true);
                mTrans_Granny.gameObject.SetActive(true);
                mTrans_GrannyBig.gameObject.SetActive(true);
                transform.GetChild(no).Find("bottom").Find("ski").Find("Image").GetComponent<Image>().sprite = butSprite[0];
                setGranny(M.NAME[0]);
                transform.GetChild(no).GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>().text = "" + M.COINS;
                break;
            case M.GAMESKI:
                no = 1;
                mTrans_GrannyShop.GetChild(0).gameObject.SetActive(true);
                mTrans_GrannyShop.gameObject.SetActive(true);
                mTrans_Ski.gameObject.SetActive(true);
                mTrans_SkiBig.gameObject.SetActive(true);
                transform.GetChild(no).Find("bottom").Find("ski").Find("Image").GetComponent<Image>().sprite = butSprite[1];
                setSki(M.SKINAME[0]);
                break;
            case M.GAMEBUY:
                no = 3;
                break;
            case M.GAMEACHIV:
                no = 4;
                break;
            case M.GAMEPLAY:
                Camera.main.transform.position = new Vector3(0, 12f, -12);
                Camera.main.transform.rotation = Quaternion.Euler(25, 0, 0);
                mGame.gameObject.SetActive(true);
                if(isResume == false)
                    mGame.GetComponent<GamePlay>().gameReset();
                no = 5;
                isResume = false;
                
                break;
            case M.GAMEOVER:
                no = 6;
                mCanvasMenu.gameObject.SetActive(true);
                mTrans_Particle.gameObject.SetActive(true);
                Transform MGrannyBig0 = mCanvasMenu.Find("GrannyBig").transform;
                int rand0 = Random.Range(0, MGrannyBig0.childCount);
                for (int i = 0; i < MGrannyBig0.childCount; i++)
                {
                    MGrannyBig0.GetChild(i).gameObject.SetActive(i == rand0);
                }
                Transform MGrannySki0 = mCanvasMenu.Find("GrannySki").transform;
                rand = Random.Range(0, MGrannySki0.childCount);
                for (int i = 0; i < MGrannySki0.childCount; i++)
                {
                    MGrannySki0.GetChild(i).gameObject.SetActive(i == rand);
                }
                transform.GetChild(no).Find("Middile").Find("Score").Find("no").GetComponent<Text>().text = M.GSOCRE + "";
                transform.GetChild(no).Find("Middile").Find("Coin").Find("no").GetComponent<Text>().text = M.GCOIN+ "";
                transform.GetChild(no).GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>().text = "" + M.COINS;
                AdManager.Instance.ShowInterstitial();
                break;
            case M.GAMEPAUSE:
                AdManager.Instance.ShowInterstitial();
                no = 7;
                mCanvasMenu.gameObject.SetActive(true);
                mTrans_Particle.gameObject.SetActive(true);
                transform.GetChild(no).GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>().text = "" + M.COINS;
                transform.GetChild(no).Find("Middile").Find("Score").Find("no").GetComponent<Text>().text = M.GSOCRE + "";
                transform.GetChild(no).Find("Middile").Find("Coin").Find("no").GetComponent<Text>().text = M.GCOIN + "";
                break;

        }
        transform.GetChild(no).gameObject.SetActive(true);

    }
}
