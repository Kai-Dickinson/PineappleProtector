using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    ScoreScript scoreScript;
    int health;
    List<GameObject> eaters = new List<GameObject>();
    Rigidbody2D rb; Vector2 diff; float lastMove;
    Transform target; int rando;
    // Start is called before the first frame update
    void Start()
    {   
        scoreScript = FindObjectOfType<ScoreScript>();

        health = 100;
        foreach(GameObject eater in GameObject.FindGameObjectsWithTag("Eater"))
        {
            eaters.Add(eater);
        }
        ChooseTarget();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        eaters.Clear();
        foreach(GameObject eater in GameObject.FindGameObjectsWithTag("Eater"))
        {
            eaters.Add(eater);
        }

        
        if(eaters.Count>0)
        {
            if(target.Equals(null))
            {   
                ChooseTarget();
            }

            diff = target.position - transform.position;
            diff = diff.normalized;

            rb.MovePosition(rb.position + diff * Time.deltaTime * 3f);

            if (diff != Vector2.zero) 
            {
                float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle -90f, Vector3.forward);
                lastMove = angle;
            } else 
            {
                transform.rotation = Quaternion.AngleAxis(lastMove -90f, Vector3.forward);
            }
        
        
            if(target)
            {
                if(rando > 0){ rando = 0;} else {rando = 1;}
            }         
        }
    }

    void TakeDamage(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            scoreScript.AddPlainScore(20);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("RollingPin"))
        {
            TakeDamage(40);
        }
    }

    void ChooseTarget()
    {
        if(eaters.Count <= 0)
        {
            return;
        }
        int rando = Random.Range(0,eaters.Count);
        target = eaters[rando].transform;

    }
}
