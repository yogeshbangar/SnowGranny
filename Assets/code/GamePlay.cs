using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform mGranny;
    public Transform mScene;
    public Transform mPreHardles;
    public Transform mPreVehicles;
    public Transform mPreCoin;
    public Transform mHardles;
    public Transform mParticle;
    public Transform mCop;
    public Transform mObjSki = null;
    public Texture[] textures;
    public Renderer renderer;
    public Transform mPowers = null;
    Menu mMenu;
    GameObject mStopper = null;
    GameObject mVehicles = null;
    GameObject mSlowper = null;
    GameObject mCoin = null;
    

    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;
    Vector3 mPos = new Vector3();
    Vector3 valocity = new Vector3();
    int roll = 0;
    int dir = 0;
    public float last = 0;
    public float SWIPE_THRESHOLD = 20f;
    int pNo = 0;
    public float sdis = 0;
    float MINDIS = -80;
    int overCount = 0;
    float goBack = 0;
    bool isSlide = false;
    bool isJump = false;
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;
    void Start(){
        mMenu = GameObject.Find("Canvas").GetComponent<Menu>();
        if (mStopper == null)
        {
            mStopper = new GameObject();
            mVehicles = new GameObject();
            mSlowper = new GameObject();
            mCoin = new GameObject();
            mStopper.name = "STOPPER";
            mVehicles.name = "VEHICLES";
            mSlowper.name = "SLOWPER";
            mCoin.name = "COIN";
            for (int i = 0; i < 20; i++)
            {
                int x = UnityEngine.Random.Range(-1, 2) * 6;
                GameObject obj = (GameObject)Instantiate(mPreHardles.GetChild(i % mPreHardles.childCount).gameObject, new Vector3(x, 0, 10 + i * 80), Quaternion.identity);
                obj.transform.parent = mStopper.transform;
            }
            for (int i = 0; i < 20; i++)
            {
                int x = UnityEngine.Random.Range(-1, 2) * 6;
                GameObject obj = (GameObject)Instantiate(mPreVehicles.GetChild(0).gameObject, new Vector3(x, 0, -800), Quaternion.identity);
                obj.transform.parent = mVehicles.transform;
            }
            for (int i = 0; i < 5; i++)
            {
                int x = UnityEngine.Random.Range(-1, 2) * 6;
                GameObject obj = (GameObject)Instantiate(mPreVehicles.GetChild(1).gameObject, new Vector3(x, 0, -800), Quaternion.identity);
                obj.transform.parent = mSlowper.transform;
            }


            for (int i = 0; i < 100; i++)
            {
                int x = UnityEngine.Random.Range(-1, 2) * 6;
                GameObject obj = (GameObject)Instantiate(mPreCoin.gameObject, new Vector3(x, 0, -800), Quaternion.identity);
                obj.transform.parent = mCoin.transform;
            }

            mStopper.transform.parent = mHardles.transform;
            mVehicles.transform.parent = mHardles.transform;
            mSlowper.transform.parent = mHardles.transform;
            mCoin.transform.parent = transform;

            

            gameReset();
        }
    }
    public void gameResume()
    {
        mGranny.GetChild(pNo).GetComponent<Animator>().enabled = true;
        mGranny.GetChild(pNo).GetComponent<Animator>().SetInteger("state", 0);
    }

    public void gameReset() {
        pNo = M.PNO;
        renderer.material.mainTexture = textures[M.MATNO % textures.Length];
        M.MATNO++;
        Debug.Log("M.SKINO " + M.SKINO);

        for (int i = 0; i < mGranny.childCount; i++)
        {
            mGranny.GetChild(i).gameObject.SetActive(pNo == i);
        }

        mGranny.GetChild(pNo).transform.rotation = Quaternion.Euler(Vector3.zero);
        mGranny.GetChild(pNo).GetComponent<Animator>().enabled = true;
        mGranny.GetChild(pNo).GetComponent<Player>().setSki(mObjSki.GetChild(M.SKINO).gameObject);
        
        setSKI(true);
        overCount = 0;
        M.GCOIN = 0;
        M.GSOCRE = 0;
        
        mPos = Vector3.zero;
        mCop.position += mPos + Vector3.back*4;
        if (mStopper == null)
        {
            Start();
        }

        for(int i =0;i< mScene.childCount; i++)
        {
            mScene.GetChild(i).transform.position = new Vector3(0, 0, i * 80);
        }

        for (int i = 0; i < mStopper.transform.childCount; i++)
        {
            mStopper.transform.GetChild(i).position = new Vector3(0, 0, -800);
            //mStopper.transform.GetChild(i).transform.position = new Vector3(0, 0, 100+i * 80);
        }
        for (int i = 0; i < mVehicles.transform.childCount; i++)
        {
            //mVehicles.transform.GetChild(i).position = new Vector3(-6.5f, 0, 100 + i * 80);
            mVehicles.transform.GetChild(i).position = new Vector3(0, 0, -800);
        }
        for (int i = 0; i < mSlowper.transform.childCount; i++)
        {
            mSlowper.transform.GetChild(i).position = new Vector3(0, 0, -800);
        }
        for (int i = 0; i < mCoin.transform.childCount; i++)
        {
            mCoin.transform.GetChild(i).position = new Vector3(0, 0, -800);
        }
        for(int i =0; i< mPowers.childCount; i++)
        {
            mPowers.GetChild(i).position = new Vector3(0, 0, -800);
        }
        int z = 0;
        for (int i = 0; i < 0; i++)
        {
            int x = UnityEngine.Random.Range(-1, 2) * 6;
            z += 80;

            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                Transform obj = getStoper();
                if (obj != null)
                    obj.position = new Vector3(x, 0, z);
            }
            else
            {
                Transform obj = getVehicle();
                if(obj!=null)
                    obj.position = new Vector3(x, 0, z);
            }
        }
        setHurdle(100);
        goBack = 0;
    }

    void setHurdle(int z) {
        loopBus(-6.5f, z);
        setCoin(-6.5f, 6, z);
        Transform tr = getVehicle();
        if (tr != null)
        {
            tr.position = new Vector3(0, 0, z);
        }
        int x = UnityEngine.Random.Range(0, 2) * 6;
        for (int i = 0; i < 5; i++) {
            Transform stoper = getStoper();
            if (stoper != null)
            {
                x = UnityEngine.Random.Range(0, 2) * 6;
                stoper.position = new Vector3(x, 0, z + i * 30);
                setCoin(x, 0, 5+z + i * 30);
            }
        }
        z += 200;
        loopBus(6.5f, z);
        setCoin(6.5f, 6, z);
        
        for (int i = 0; i < 5; i++)
        {
            Transform stoper = getStoper();
            if (stoper != null)
            {
                x = UnityEngine.Random.Range(-1, 1) * 6;
                stoper.position = new Vector3(x, 0, z + i * 30);
                setCoin(x, 0, 5 + z + i * 30);
            }
        }

        z += 200;
        loopBus(0f, z);
        setCoin(0,6, z);
        for (int i = 0; i < 5; i++)
        {
            Transform stoper = getStoper();
            if (stoper != null){
                x = UnityEngine.Random.Range(-1, 1);
                x = (x == 0) ? 6 : -6;
                stoper.position = new Vector3(x, 0, z + i * 30);
                setCoin(x, 0, 5 + z + i * 30);
            }
        }
        Transform tr2 = getVehicle();
        if (tr2 != null)
        {
            tr2.position = new Vector3(0, 0, z+100);
        }
        int rand = UnityEngine.Random.Range(0, 2);
        if (mPowers.GetChild(rand).position.z <= MINDIS)
        {
            mPowers.GetChild(rand).position = new Vector3(-1 * x, 0, z + 100);
        }
        else
        {
            rand++;
            rand %= 2;
            if (mPowers.GetChild(rand).position.z <= MINDIS)
                mPowers.GetChild(rand).position = new Vector3(-1 * x, 0, z + 100);
        }
    }

    void setCoin(float x,float y, float z)
    {
        for (int i = 0,j=0; j<4 && i < mCoin.transform.childCount; i++)
        {
            if (mCoin.transform.GetChild(i).position.z < MINDIS)
            {
                mCoin.transform.GetChild(i).GetComponent<Coin>().set(x, y + 1, z + j * 5);
                //mCoin.transform.GetChild(i).position = new Vector3(x,y+1,z+j*5);
                j++;
            }
        }
    }
    Transform getStoper() {
        for (int i = 0; i < mStopper.transform.childCount; i++)
        {
            if (mStopper.transform.GetChild(i).position.z < MINDIS)
            {
                return mStopper.transform.GetChild(i).transform;
            }
        }
        return null;
    }

    Transform getVehicle()
    {
        for (int i = 0; i < mVehicles.transform.childCount; i++)
        {
            if (mVehicles.transform.GetChild(i).position.z < MINDIS)
            {
                return mVehicles.transform.GetChild(i).transform;
            }
        }
        return null;
    }
    Transform getSlowper()
    {
        for (int i = 0; i < mSlowper.transform.childCount; i++)
        {
            if (mSlowper.transform.GetChild(i).position.z < MINDIS)
            {
                return mSlowper.transform.GetChild(i).transform;
            }
        }
        return null;
    }
    void loopBus(float x,float z) {
        for (int i = 0; i < 3; i++)
        {
            Transform tr = getVehicle();
            if(tr != null)
            {
                tr.position = new Vector3(x, 0, z + i * 20);
            }
        }
        Transform slor = getSlowper();
        slor.position = new Vector3(x, 0, z - 20);
    }

    // Update is called once per frame
    void Update()
    {
        last = 0;
        //M.SPD = 0;
        for (int i = 0; i < mScene.childCount; i++)
        {
            mScene.GetChild(i).transform.position += Vector3.forward * M.SPD;

        }
        for (int i = 0; i < mPowers.childCount; i++)
        {
            if (mPowers.GetChild(i).position.z >= MINDIS)
            {
                mPowers.GetChild(i).position += Vector3.forward * M.SPD;
                if (M.Rect2RectIntersection(mPowers.GetChild(i).position.x, mPowers.GetChild(i).position.z, 3, 2
                    , mGranny.GetChild(pNo).transform.position.x, mGranny.GetChild(pNo).transform.position.z, 2, 2))
                {

                    mPowers.GetChild(i).position = Vector3.forward*-800;
                    if(mPowers.GetChild(i).name == "Gold_Star")
                    {
                        M.starPower = M.MAXPSTAR * M.MAXPOWER;
                    }
                    if (mPowers.GetChild(i).name == "megnet")
                    {
                        M.coinPower = M.MAXPCOIN * M.MAXPOWER;
                    }
                    Debug.Log(M.starPower+", "+ M.coinPower + " mPowers.GetChild(" +i+").name = "+ mPowers.GetChild(i).name);
                }
            }

        }
        for (int i = 0; i < mHardles.childCount; i++)
        {
            for (int j = 0; j < mHardles.GetChild(i).childCount; j++) {
                if(mHardles.GetChild(i).GetChild(j).transform.position.z >= MINDIS)
                {
                    mHardles.GetChild(i).GetChild(j).transform.position += Vector3.forward * M.SPD;
                    mHardles.GetChild(i).GetChild(j).gameObject.SetActive(true);
                }
                else
                {
                    mHardles.GetChild(i).GetChild(j).gameObject.SetActive(false);
                }
                if(last< mHardles.GetChild(i).GetChild(j).transform.position.z)
                {
                    last = mHardles.GetChild(i).GetChild(j).transform.position.z;
                }
            }
        }
        if(last < 200) {
            setHurdle((int)last+40);
        }

        for (int i = 0; i < mCoin.transform.childCount; i++)
        {
                if (mCoin.transform.GetChild(i).transform.position.z >= MINDIS)
                {
                    mCoin.transform.GetChild(i).GetComponent<Coin>().changes(mGranny.GetChild(pNo).transform.position,mParticle);
                }
                else
                {
                    mCoin.transform.GetChild(i).gameObject.SetActive(false);
                }
        }

        sdis = 0;
        for (int i = 0; i < mHardles.childCount && overCount == 0; i++)
        {
            for (int j = 0; j < mHardles.GetChild(i).childCount && overCount == 0; j++)
            {
                if(mHardles.GetChild(i).GetChild(j).tag == "bus")
                {
                    if (M.Rect2RectIntersection(mHardles.GetChild(i).GetChild(j).transform.position.x, mHardles.GetChild(i).GetChild(j).transform.position.z, 5, 20
                    , mGranny.GetChild(pNo).transform.position.x, mGranny.GetChild(pNo).transform.position.z, 5, 2))
                    {

                        if (mPos.y < 4)
                        {
                            setGameOver();
                        }
                        else
                        {
                            sdis = 6.4f;
                            if (mPos.y < sdis)
                                mPos.y = sdis;
                        }

                            
                    }
                }
                if (mHardles.GetChild(i).GetChild(j).tag == "container")
                {
                    if(M.Rect2RectIntersection(mHardles.GetChild(i).GetChild(j).transform.position.x,mHardles.GetChild(i).GetChild(j).transform.position.z,5,34
                    ,mGranny.GetChild(pNo).transform.position.x, mGranny.GetChild(pNo).transform.position.z, 5, 2)){
                        //M.SPD = 0;
                        if (mHardles.GetChild(i).GetChild(j).transform.position.z > 7.4)
                            sdis = (18 - mHardles.GetChild(i).GetChild(j).transform.position.z)*.7f;
                        else
                        {
                            sdis = 6.4f;
                        }
                        if (mPos.y < sdis)
                            mPos.y = sdis;

                    }
                }
                if (mHardles.GetChild(i).GetChild(j).tag == "siv1")
                {
                    if (M.Rect2RectIntersection(mHardles.GetChild(i).GetChild(j).transform.position.x, mHardles.GetChild(i).GetChild(j).transform.position.z, 5, 2
                    , mGranny.GetChild(pNo).transform.position.x, mGranny.GetChild(pNo).transform.position.z, 5, 2))
                    {

                        if (roll <= 0 && overCount == 0)
                        {
                            Debug.Log(mHardles.GetChild(i).GetChild(j).transform.position.x+", "+ mHardles.GetChild(i).GetChild(j).transform.position.z+"  setGameOver   aaaa" + mGranny.GetChild(pNo).transform.position.x+", "+ mGranny.GetChild(pNo).transform.position.z);
                            setGameOver();
                        }
                    }
                }
                if (mHardles.GetChild(i).GetChild(j).tag == "siv2")
                {
                    if (M.Rect2RectIntersection(mHardles.GetChild(i).GetChild(j).transform.position.x, mHardles.GetChild(i).GetChild(j).transform.position.z, 5, 2
                    , mGranny.GetChild(pNo).transform.position.x, mGranny.GetChild(pNo).transform.position.z, 5, 2))
                    {

                        if (mPos.y <= sdis && overCount == 0)
                        {
                            Debug.Log(mHardles.GetChild(i).GetChild(j).transform.position.x + ", " + mHardles.GetChild(i).GetChild(j).transform.position.z + " setGameOver   bbb " + mGranny.GetChild(pNo).transform.position.x + ", " + mGranny.GetChild(pNo).transform.position.z);
                            setGameOver();
                        }
                    }
                }
            }
        }

        for (int i = 0; i < mScene.childCount; i++)
        {
            if(mScene.GetChild(i).transform.position.z < -80)
            {
                mScene.GetChild(i).transform.position = new Vector3(0,0, mScene.GetChild((i == 0 ? mScene.childCount : i)-1).transform.position.z+80);
            }
             
        }
        mGranny.GetChild(pNo).transform.position = mPos;

        if(goBack > 50)
            mCop.position = new Vector3(mPos.x, mPos.y, mCop.position.z-.1f);
        else
        {
            goBack++;
            mCop.position = new Vector3(mPos.x, mPos.y, mCop.position.z);
        }
        //


        Camera.main.transform.position = new Vector3(mPos.x * .8f, mPos.y + 11f, -20);

        //Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(pos.x * .8f, pos.y + 12f, -12), Time.deltaTime * 20);

        if (Input.GetMouseButtonDown(0))
        {
            this.fingerDown = Input.mousePosition;
            this.fingerUp = Input.mousePosition;
            clicked++;
            if (clicked == 1) clicktime = Time.time;

            if (clicked > 1 && Time.time - clicktime < clickdelay)
            {
                clicked = 0;
                clicktime = 0;
                setSKI(true);
            }
            else if (clicked > 2 || Time.time - clicktime > 1)
                clicked = 0;
        


        }
        if (Input.GetMouseButtonUp(0))
        {
            this.fingerDown = Input.mousePosition;
            //this.fingerUpTime = DateTime.Now;
            this.checkSwipe();
        }


        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                if (Input.GetTouch(i).tapCount == 2)
                {
                    Debug.Log("Double Tap");
                }
            }
        }




        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
            }
        }
        
        if(mPos.y > sdis)
        {
            mPos.y += valocity.y;
            valocity.y -= .1f;
            if (mPos.y <= sdis)
            {
                mPos.y = sdis;
                if(isSlide)
                    mGranny.GetChild(pNo).GetComponent<Animator>().SetInteger("state", 3);
                else
                    mGranny.GetChild(pNo).GetComponent<Animator>().SetInteger("state", 0);
                Debug.Log(isSlide+ " state  1");
            }
        }
        else{
            isJump = false;
        }
        if(roll > 0)
        {
            roll--;
            if (roll <= 0)
            {
                if (isSlide)
                    mGranny.GetChild(pNo).GetComponent<Animator>().SetInteger("state", 3);
                else
                    mGranny.GetChild(pNo).GetComponent<Animator>().SetInteger("state", 0);
                Debug.Log(isSlide + " state  2");
            }
        }
        if (valocity.x != 0 && overCount == 0)
        {
            mPos.x += valocity.x;
            if (valocity.x < 0)
            {
                if (mPos.x < dir)
                {
                    mPos.x = dir;
                    valocity.x = 0;
                }
            }
            if (valocity.x > 0)
            {
                if (mPos.x > dir)
                {
                    mPos.x = dir;
                    valocity.x = 0;
                }
            }
        }
        if (M.SPD > -1.9)
        {
            if (M.SPD > -.9)
                M.SPD -= .01f;
            if (M.SPD > -1.9)
            {
                M.SPD -= .0001f;
            }
        }
        if (overCount > 0)
        {
            M.SPD = 0;
            overCount++;
            if (overCount > 20)
            {
                mMenu.setScreen(M.GAMEOVER);
            }
            if((mGranny.GetChild(pNo).transform.transform.eulerAngles.x + 360) % 360 > 280)
                mGranny.GetChild(pNo).transform.transform.Rotate(-10, 0, 0, Space.World);
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnSwipeUp();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnSwipeDown();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnSwipeLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnSwipeRight();
        }
        

    }
    void setSKI(bool val) {
        mGranny.GetChild(pNo).transform.rotation = Quaternion.Euler(Vector3.zero);
        mGranny.GetChild(pNo).GetComponent<Player>().SKI = val;
        mGranny.GetChild(pNo).GetComponent<Animator>().SetInteger("state", val ? 3 : 0);
        isSlide = val;
        Debug.Log(isSlide + " state  3");

    }
    void setGameOver() {
        
        //if (isSlide)
        //{
        //    setSKI(false);



        //    for (int i = 0; i < mHardles.childCount; i++)
        //    {
        //        for (int j = 0; j < mHardles.GetChild(i).childCount; j++)
        //        {
        //            if (mHardles.GetChild(i).GetChild(j).transform.position.z < 100)
        //            {
        //                mHardles.GetChild(i).GetChild(j).transform.position = new Vector3(0,0,-800);
        //                mHardles.GetChild(i).GetChild(j).gameObject.SetActive(false);
        //            }
                   
        //        }
        //    }

        //}
        //else
        {
            overCount = 1;
            mGranny.GetChild(pNo).transform.transform.Rotate(-10, 0, 0, Space.World);
            mGranny.GetChild(pNo).GetComponent<Animator>().enabled = false;
        }
        
    }

    public IEnumerator CheckAnimationCompleted(string CurrentAnim, Action Oncomplete)
     {
        Debug.Log(isSlide + " state  4");
        while (!mGranny.GetChild(pNo).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(CurrentAnim))
             yield return null;
        if (Oncomplete != null)
        {
            Oncomplete();
        }
     }

    void checkSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                OnSwipeUp();
            }
            else if (fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                OnSwipeDown();
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        else
        {
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    void OnSwipeUp()
    {
        //mGranny.GetChild(pNo).GetComponent<Animator>().SetInteger("state", 1);
        if(isJump == false)
        {
            valocity.y = .81f;
            mPos.y += valocity.y;
            isJump = true;
        }
        
        //StartCoroutine(CheckAnimationCompleted("Jump", () =>{
        //    mGranny.GetChild(pNo).GetComponent<Animator>().SetInteger("state", 0);
        //}
        // ));
    }

    void OnSwipeDown()
    {
        mGranny.GetChild(pNo).GetComponent<Animator>().SetInteger("state", 2);
        roll = 20;
        Debug.Log(isSlide + " state  5");
        //StartCoroutine(CheckAnimationCompleted("Slide", () => {
        //    mGranny.GetChild(pNo).GetComponent<Animator>().SetInteger("state", 0);
        //    }
        //));
    }

    void OnSwipeLeft()
    {
        if (dir > -6)
        {
            dir -= 6;
            valocity.x = -1f;
        }
        
    }

    void OnSwipeRight()
    {
        if (dir < 6)
        {
            dir += 6;
            valocity.x = 1f;
        }

    }
    


}
