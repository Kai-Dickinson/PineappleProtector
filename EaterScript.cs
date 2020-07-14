using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum EaterState
{
    Eating,
    Waiting
}

public class EaterScript : MonoBehaviour
{
    public EaterState currentState;
    public HealthBar hpBar;
    public TimeScript timeBar;

    public float maxHealth = 100f;
    public float health;

    public float eatingTime;
    public float waitingTimeLength; //Gets smaller as you continue
    public float timeWaited; public float TimeSetTime; float maxWait = 15f;

    float hitWait = 1f; float lastHit;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        hpBar.SetMaxHealth(maxHealth);
        currentState = EaterState.Waiting;

        timeBar.SetMaxTime(maxWait);
        TimeSetTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState.Equals(EaterState.Eating))
        {
            TimeSetTime = Time.time;
        }
        timeBar.SetTime(maxWait - (Time.time - TimeSetTime));
        if(maxWait - (Time.time - TimeSetTime) <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void TakeDamage(float dmg)
    {
        health -= dmg;
        hpBar.SetHealth(health);
        if(health <= 0f){ Destroy(gameObject); }
    }

    public void Heal(float hp)
    {
        health += hp;
        hpBar.SetHealth(health);
        if(health > maxHealth){health = maxHealth;}
    }


    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            if(Time.time - lastHit >= hitWait)
            {
                TakeDamage(20f);
                lastHit = Time.time;
            }
        }
    }
}
