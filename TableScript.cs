using UnityEngine;

public class TableScript : MonoBehaviour
{
    SpawnScript spawner; ScoreScript scoreScript; EndScript endScript;
    public EaterScript eater1; public EaterScript eater2;
    public GameObject player;
    public GameObject pizzaFab;
    public GameObject eater1PizzaSpawn; public GameObject pizzaInSpawn1;
    public GameObject eater2PizzaSpawn; public GameObject pizzaInSpawn2;

    public bool playerInRangePizza1; public bool playerInRangePizza2;
    bool eatingTimeSet1; bool eatingTimeSet2;
    float eatingStarted1; float eatingStarted2;
    // Start is called before the first frame update
    void Start()
    {
        playerInRangePizza1 = false; playerInRangePizza2 = false;
        eatingTimeSet1 = false; eatingTimeSet2 = false;
        scoreScript = FindObjectOfType<ScoreScript>();
        spawner = FindObjectOfType<SpawnScript>();
        endScript = FindObjectOfType<EndScript>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInRangePizza1 = false;
        playerInRangePizza2 = false;

        if(Vector2.Distance(player.transform.position, eater1PizzaSpawn.transform.position) <= Vector2.Distance(player.transform.position, eater2PizzaSpawn.transform.position))
        {
            if(Vector2.Distance(player.transform.position, eater1PizzaSpawn.transform.position) <= 2f)
            {
                playerInRangePizza1 = true;
            }
            
        } else 
        {
            if(Vector2.Distance(player.transform.position, eater2PizzaSpawn.transform.position) <= 2f)
            {
                playerInRangePizza2 = true;
            }
        }



        if(pizzaInSpawn1 != null){eater1.currentState = EaterState.Eating;} else {eater1.currentState = EaterState.Waiting;}
        if(pizzaInSpawn2 != null){eater2.currentState = EaterState.Eating;} else {eater2.currentState = EaterState.Waiting;}


        if(eater1.currentState.Equals(EaterState.Eating))
        {
            if(!eatingTimeSet1){eater1.eatingTime = Random.Range(10f,20f); eatingStarted1 = Time.time; eatingTimeSet1 = true;}

            if(Time.time - eatingStarted1 >= eater1.eatingTime)
            {
                Destroy(pizzaInSpawn1);
                eatingTimeSet1 = false; 
                eater1.currentState = EaterState.Waiting;
                eater1.Heal(15f);
                spawner.spawnRate *= 0.95f;
                scoreScript.AddScore(eater1.health, eater1.maxHealth, spawner.spawnRate, spawner.startSpawnRate);
            }
        }

        if(eater2.currentState.Equals(EaterState.Eating))
        {
            if(!eatingTimeSet2){eater2.eatingTime = Random.Range(10f,20f); eatingStarted2 = Time.time; eatingTimeSet2 = true;}

            if(Time.time - eatingStarted2 >= eater2.eatingTime)
            {
                Destroy(pizzaInSpawn2); 
                eatingTimeSet2 = false; 
                eater2.currentState = EaterState.Waiting;
                eater2.Heal(15f);
                spawner.spawnRate *= 0.95f;
                scoreScript.AddScore(eater2.health, eater2.maxHealth, spawner.spawnRate, spawner.startSpawnRate);
            }
        }

        if(eater1.Equals(null) && eater2.Equals(null))
        {
            //End
            endScript.EndGame();
        }

    }


    public void SpawnPizza()
    {
        if(playerInRangePizza1 && eater1)
        {
            pizzaInSpawn1 = Instantiate(pizzaFab, eater1PizzaSpawn.transform.position, Quaternion.identity);
            pizzaInSpawn1.GetComponent<SpriteRenderer>().sortingOrder = 2;
        } else if (playerInRangePizza2 && eater2)
        {
            pizzaInSpawn2 = Instantiate(pizzaFab, eater2PizzaSpawn.transform.position, Quaternion.identity);
            pizzaInSpawn2.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }

}
