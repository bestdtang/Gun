using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneControl : MonoBehaviour
{
    // Start is called before the first frame update
    public int bulletsLeft, targetLeft;
    public GyroNew gyroSc;
    public Animator uiAnim;
    void Start()
    {

    }
    void WaitWin()
    {
        Time.timeScale = 1;
        //sequence pause
        uiAnim.SetTrigger("win");
    }
    void WaitLose()
    {
        Time.timeScale = 1;
        uiAnim.SetTrigger("lose");
    }

    private void SlowAndFocus(bool win)
    {
        gyroSc.allowedMove = false;
        Time.timeScale = 0.2f;
        if (win)
        {
            Invoke("WaitWin", 0.5f);
        }
        else
        {
            Invoke("WaitLose", 0.5f);
        }
    }

    public void CheckIfWinOrLose()
    {
        Debug.Log(bulletsLeft + "  " + targetLeft);
        if (targetLeft == 0)
        {
            //win
            SlowAndFocus(true);
        }
        else if (bulletsLeft < targetLeft)
        {
            //lose
            SlowAndFocus(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
