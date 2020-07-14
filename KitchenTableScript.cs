using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenTableScript : MonoBehaviour
{
    public EaterScript eater1;
    public EaterScript eater2;
    public GameObject player;
    
    public GameObject pizzaSpawn;
    public GameObject pizzaFab; GameObject pizzaOnTable;
    bool spawningPizza = false; bool pizzaExists = false;
    public bool playerInRangePizza;
    // Start is called before the first frame update
    void Start()
    {
        playerInRangePizza = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(player.transform.position,pizzaSpawn.transform.position) <= 2f)
        {
            playerInRangePizza = true;
        } else {playerInRangePizza = false;}

        CheckForEaterWaiting();
    }


    void CheckForEaterWaiting()
    {
        if((eater1.currentState.Equals(EaterState.Waiting) || eater2.currentState.Equals(EaterState.Waiting)) && !pizzaExists)
        {
            //Need to spawn pizza soon
            if(!spawningPizza)
            {
                spawningPizza = true;
                StartCoroutine(SpawnPizza());
            }
            
        }
    }

    IEnumerator SpawnPizza()
    {
        yield return new WaitForSeconds(4f);
        pizzaOnTable = Instantiate(pizzaFab, pizzaSpawn.transform.position, Quaternion.identity);
        pizzaOnTable.GetComponent<SpriteRenderer>().sortingOrder = 2;
        spawningPizza = false;
        pizzaExists = true;
    }


    public void PizzaPickedUp()
    {
        if(pizzaExists){Destroy(pizzaOnTable); pizzaExists = false;}
        
    }
}
