using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;


public class JohnMovement : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float JumpForce;
    public float Speed;
    public int vida = 5;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private bool Grounded;
    private Animator Animator;
    private float LastShoot;
    public GameObject Reiniciar;
    public Image GamerOver;

    RaycastHit2D hit;
    public Vector3 v3;
    public LayerMask layer;
    public float distance;
    // Start is called before the first frame update


    private void Start()
    {
        GamerOver.enabled = false;
        Reiniciar.gameObject.SetActive(false);
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    bool Checkcollision
    {
        get
        {
            hit = Physics2D.Raycast(transform.position + v3, transform.up * -1, distance , layer);
            return hit.collider != null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Detector_Plataforma();

        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("running",Horizontal != 0.0f) ;

        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.2f))
        {
            Grounded = true;
        }

        else Grounded = false;

        if (Input.GetKeyDown(KeyCode.W) && Grounded) 
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f) 
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Jump() 
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<Bullet_Scripts>().SetDirection(direction);
    }
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal ,Rigidbody2D.velocity.y);
    }
    
    public void Hit()
    {
        vida = vida - 1;
        if (vida == 0) Destroy(gameObject) ;
        Reiniciar.gameObject.SetActive(true);
        GamerOver.enabled = true;
    }

    public void Detector_Plataforma()
    {
        if (Checkcollision)
        {
            transform.parent = hit.collider.transform;
        }
        else
        {
            transform.parent = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + v3, Vector3.up * -1 * distance);
    }

}
