using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelThreeController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] spawnPositions;
    public Transform[] targetPositions;
    public GameObject targetPre, restartButton, startGame;
    public float spawnSpeed, bulletSpeed;
    public float lastTime, lastBulletTime;
    public bool isShooting;
    public Animator UIAnim;
    public Text healthNum;
    public Image healthBar, bulletBar;
    public int gameStatus;

    public float healthLeft, bulletLeft, targetkilled;
    public GyroNew gyroSc;

    private float healthTotal, bulletTotal;

    Shoot shootSc;
    void Start()
    {
        lastTime = Time.time;
        shootSc = transform.GetComponent<Shoot>();
        healthTotal = healthLeft;
        bulletTotal = bulletLeft;
        healthBar.rectTransform.localScale = new Vector3(healthLeft / healthTotal, 1, 1);
        healthNum.text = healthLeft.ToString();
        gameStatus = 0;
        Time.timeScale = 0;
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        startGame.SetActive(false);
        gyroSc.allowedMove = true;
    }

    void WaitWin()
    {
        Time.timeScale = 0;
        UIAnim.SetTrigger("win");
        restartButton.SetActive(true);
    }
    void WaitLose()
    {
        Time.timeScale = 0;
        UIAnim.SetTrigger("lose");
        restartButton.SetActive(true);
    }

    public void SlowAndFocus(bool win)
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
    Vector3 pickPosition(Transform[] trans)
    {
        int num = Mathf.Clamp(Random.Range(0, trans.Length), 0, trans.Length - 1);
        return trans[num].position;
    }
    public void isShoot()
    {
        isShooting = true;
    }
    public void isnShoot()
    {
        isShooting = false;
    }

    public void HealthLose()
    {
        //Debug.Log("lose health");
        UIAnim.SetTrigger("hit");
        healthLeft -= 5;
        healthBar.rectTransform.localScale = new Vector3(healthLeft / healthTotal, 1, 1);
        healthNum.text = healthLeft.ToString();

        if (healthLeft <= 0)
        {
            SlowAndFocus(false);
        }
    }

    public void fillBullet()
    {
        bulletLeft = bulletTotal;
        bulletBar.fillAmount = bulletLeft / bulletTotal;
    }
    public void holdShoot()
    {
        if (Time.time - lastBulletTime > bulletSpeed && bulletLeft >= 1)
        {
            shootSc.ShootBullet();
            bulletLeft--;
            bulletBar.fillAmount = bulletLeft / bulletTotal;
            lastBulletTime = Time.time;
        }
    }
    void spawnNew()
    {
        if (Time.time - lastTime > spawnSpeed)
        {
            GameObject newtarget = Instantiate(targetPre, pickPosition(spawnPositions), Quaternion.identity);
            LevelThreeTarget targetSc = newtarget.GetComponent<LevelThreeTarget>();
            targetSc.speed = 3;
            targetSc.targetPosition = pickPosition(targetPositions);
            lastTime = Time.time + spawnSpeed * Random.Range(-0.5f, 0.5f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        spawnNew();

        if (isShooting)
        {
            holdShoot();
        }

        if (targetkilled >= 40 && gameStatus == 0)
        {
            gameStatus = 1;
            spawnSpeed *= 0.7f;
            lastTime += 5f;
        }

        if (targetkilled >= 70 && gameStatus == 1)
        {
            gameStatus = 2;
            spawnSpeed *= 0.6f;
            lastTime += 5f;
        }
        if (targetkilled >= 120 && gameStatus == 2)
        {
            gameStatus = 3;
            SlowAndFocus(true);
        }

    }
}
