using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody rb;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;
    public int pointValue;

    private float minSpeed = 13;
    private float maxSpeed = 17;
    private float maxTorque = 10;
    private float xRange = 4;
    private float spawnY = -6;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        rb.AddForce(RandomForce(), ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), spawnY);
    }

    void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter()
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bomb")) gameManager.GameOver();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
