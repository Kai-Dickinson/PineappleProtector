using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingPin : MonoBehaviour
{
    Rigidbody2D rb; AudioSource source; public AudioClip hitSound;
    float spawned;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        source.clip = hitSound;
        rb.AddForce(transform.up * 5f, ForceMode2D.Impulse);
        spawned = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - spawned >= 3f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            source.Play();
            Destroy(gameObject, 0.1f);
        }
    }
}
