using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float awaySpeed;
    [SerializeField] GameObject powerUpIndicator;

    public  bool hasPowerUp;
    GameObject focalPoint;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    private void Update()
    {
        Movement();
        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    void Movement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        rb.AddForce(focalPoint.transform.forward * speed * verticalInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpTimer());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            // called enemy rigidbody
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRigidBody.AddForce(awayFromPlayer * awaySpeed, ForceMode.Impulse);
            Debug.Log("Collidede with :" + collision.gameObject.name);
        }
    }

    IEnumerator PowerUpTimer()
    {
        yield return new WaitForSeconds(3);
        hasPowerUp = false;
        powerUpIndicator.gameObject.SetActive(false);
    }
}
