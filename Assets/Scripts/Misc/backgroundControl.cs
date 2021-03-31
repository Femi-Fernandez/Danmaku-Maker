using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundControl : MonoBehaviour
{
    // Start is called before the first frame update
    public int anim;
    //public GameObject[] baseShapes;
    public GameObject[] morphShapes;
    public float timer;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0); //rotates 50 degrees per second around z axis
        timer += Time.deltaTime;

        if (timer > 5.0f)
        {
            StartCoroutine(triggerAnim(0));
            timer = -30.0f;
        }
    }

    IEnumerator triggerAnim(int morphNum)
    {
        //baseShapes[morphNum].SetActive(false);

        morphShapes[morphNum].GetComponent<Animator>().SetInteger("play_anim", 1);
        yield return StartCoroutine("animationDelay");

        //baseShapes[morphNum + 1].SetActive(true);
    }

    IEnumerator animationDelay()
    {
        yield return new WaitForSeconds(1);
    }
}
