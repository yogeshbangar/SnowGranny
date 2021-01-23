using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    int status = 0;
    public void set(float x,float y,float z) {
        transform.position = new Vector3(x, y,z);
        status = 0;
    }
    public int get()
    {
        return status;
    }
    public void changes(Vector3 vec,Transform particle) {
        if (status == 0)
        {
            transform.position += Vector3.forward * M.SPD;
            if(transform.position.z > -10 && transform.position.z < 15 && M.coinPower > 0)
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(vec.x, vec.y+3, 1), 60 * Time.deltaTime);
        }
        if (status == 1)
        {
            transform.position += Vector3.down * M.SPD;
            if(transform.position.y > 30)
            {
                transform.position = new Vector3(0, 0, -800);
            }
        }
        transform.Rotate(0, 10, 0, Space.World);
        gameObject.SetActive(true);
        if (status==0 && M.Rect2RectIntersection(transform.position.x, transform.position.z, 2, 2,vec.x, vec.z, 2, 2))
        {
            status = 1;
            M.GCOIN++;
            M.ParNO++;
            particle.GetChild(M.ParNO % 4).GetComponent<ParticleSystem>().Clear();
            particle.GetChild(M.ParNO % 4).GetComponent<ParticleSystem>().Play();
            particle.GetChild(M.ParNO % 4).position = vec+Vector3.forward;
            if (M.starPower > 0)
            {
                M.GCOIN++;
            }
            transform.position = new Vector3(0, 100, -800);
        }
    }
}
