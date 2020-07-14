using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, PlayerControls.IGameActions
{
    public PlayerControls controls; int fireCount; int pickupCount; int putdownCount;

    AudioSource source; public AudioClip throwSound;
    KitchenTableScript kitchenTable; TableScript tableScript; RunScript runScript;
    bool hasPizza = false; 
    public Image pizzaImage;

    public GameObject firingPoint; public GameObject rollingPin;
    float firingRate = 0.25f; float lastFire; //last time fired
    Rigidbody2D rb;
    public float moveSpeed = 10f; bool isRunning; float runningSpeed = 20f;
    float lastMove; //Last input from player
    Vector2 move;

    public GameObject pauseScreen; bool paused = false; bool pausepressed = false;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Game.SetCallbacks(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        kitchenTable = FindObjectOfType<KitchenTableScript>();
        tableScript = FindObjectOfType<TableScript>();
        runScript = FindObjectOfType<RunScript>();
        source = GetComponent<AudioSource>();
        source.clip = throwSound;
        runScript.SetMaxRun(100f);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _origPos = transform.position;

        Vector2 movement = new Vector2(move.x, move.y) * Time.deltaTime;
        rb.MovePosition(rb.position + movement * moveSpeed);

        if (movement != Vector2.zero) 
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle -90f, Vector3.forward);
            lastMove = angle;
        } else 
        {
            transform.rotation = Quaternion.AngleAxis(lastMove -90f, Vector3.forward);
        }


        if(Time.time - lastFire >= firingRate)
        {
            fireCount = 0;
        }
            

        rb.velocity = Vector2.zero;

        if(hasPizza)
        {
            pizzaImage.gameObject.SetActive(true);
        } else {pizzaImage.gameObject.SetActive(false);}

        CalculateRunningLeft(isRunning);

    }


    void Shoot()
    {
        source.Play();
        Instantiate(rollingPin, firingPoint.transform.position, gameObject.transform.rotation);
        lastFire = Time.time;
    }

    void PickupItem()
    {        
        if(kitchenTable.playerInRangePizza)
        {
            if(!hasPizza)
            {
                hasPizza = true;
                kitchenTable.PizzaPickedUp();
            }
            
        }
    }

    void PutDownItem()
    {        
        if(hasPizza && tableScript.playerInRangePizza1 && !tableScript.pizzaInSpawn1)
        {
            //Spawn in pizza1spot
            tableScript.SpawnPizza();
            hasPizza = false;
        } else if(hasPizza && tableScript.playerInRangePizza2 && !tableScript.pizzaInSpawn2)
        {
            //Spawn in pizza2spot
            tableScript.SpawnPizza();
            hasPizza = false;
        }

    }


    void CalculateRunningLeft(bool running)
    {
        if(running)
        {
            if(runScript.runSlider.value > 0)
            {
                runScript.DecreaseTime(100f*Time.deltaTime);
            } else {isRunning = false; moveSpeed = 10f;}
            
        }else 
        {
            runScript.IncreaseTime(20f*Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        controls.Game.Enable();
    }
    private void OnDisable()
    {
        controls.Game.Disable();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed && fireCount < 1){ ++fireCount; Shoot();}
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed){ move = context.ReadValue<Vector2>();}

        if(context.canceled){move = Vector2.zero;}
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started && !isRunning){isRunning = true; moveSpeed = 25f;}
        if(context.canceled){isRunning = false; moveSpeed = 10f;}
    }
    public void OnPickup(InputAction.CallbackContext context)
    {
        if (context.performed && pickupCount < 1){ ++pickupCount; PickupItem();}
        if(context.canceled){pickupCount = 0;}
    }

    public void OnPutDown(InputAction.CallbackContext context)
    {
        if (context.performed && putdownCount < 1){ ++putdownCount; PutDownItem();}
        if(context.canceled){putdownCount = 0;}
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed && !paused && !pausepressed){pausepressed = true; paused = true; pauseScreen.SetActive(true); Time.timeScale = 0f;}
        if(context.performed && paused && !pausepressed){pausepressed = true; paused = false; pauseScreen.SetActive(false); Time.timeScale = 1f;}

        if(context.canceled){pausepressed = false;}
    }

}
