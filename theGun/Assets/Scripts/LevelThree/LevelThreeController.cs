using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelThreeController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] way1, way2, way3, way4;
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

    void Pause()
    {
        Time.timeScale = 0;
    }

    void WaitWin()
    {
        Time.timeScale = 1;
        UIAnim.SetTrigger("win");
        restartButton.SetActive(true);
        Invoke("Pause", 0.5f);
    }
    void WaitLose()
    {
        Time.timeScale = 1;
        UIAnim.SetTrigger("lose");
        restartButton.SetActive(true);
        Invoke("Pause", 0.5f);
    }

    public void SlowAndFocus(bool win)
    {
        gyroSc.allowedMove = false;
        Time.timeScale = 0.2f;
        if (win)
        {
            Invoke("WaitWin", 0.2f);
        }
        else
        {
            Invoke("WaitLose", 0.2f);
        }
    }
    // Vector3 pickPosition(Transform[] trans)
    // {
    //     int num = Mathf.Clamp(Random.Range(0, trans.Length), 0, trans.Length - 1);
    //     return trans[0].position;
    //     //return trans[num].position;
    // }

    Transform[] PickWay()
    {
        switch (Mathf.Clamp(Random.Range(1, 5), 1, 4))
        {
            case 1:
                return way1;
            case 2:
                return way2;
            case 3:
                return way3;
            default:
                return way1;
        }
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
            bulletLeft--;
            bulletBar.fillAmount = bulletLeft / bulletTotal;
            lastBulletTime = Time.time;
            shootSc.ShootBullet();
        }
    }
    void spawnNew()
    {
        if (Time.time - lastTime > spawnSpeed)
        {
            GameObject newtarget = Instantiate(targetPre);
            LevelThreeTarget targetSc = newtarget.GetComponent<LevelThreeTarget>();
            targetSc.speed = 3 + Random.Range(-1, 1);
            targetSc.force = 20 + Random.Range(-5, 5);
            targetSc.positions = PickWay();
            newtarget.transform.position = targetSc.positions[0].position;
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
