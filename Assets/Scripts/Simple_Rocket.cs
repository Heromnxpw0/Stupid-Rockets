using System.Collections.Generic;
using UnityEngine;

public class Simple_Rocket : MonoBehaviour
{
    public float thrust = 100.0f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private PolygonCollider2D bc;
    public List<Vector2> forces = new List<Vector2>();
    public float fitness;
    public int numForces;
    private bool isDead = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<PolygonCollider2D>();
        sr.color = new Color(Random.value, Random.value, Random.value, 1.0f);
        rb.gravityScale = 0;
    }
    void Update()
    {
        if (isDead) return;
        float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg - 120; // should be checked later
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    public void applyFroce(int forceIndex)
    {
        if (isDead) return;
        if (forceIndex >= numForces)
        {
            die();
            return;
        }
        rb.AddForce(forces[forceIndex] * thrust);
    }
    public float evaluate()
    {
        float distance = Vector2.Distance(transform.position, Target.instance.transform.position);
        fitness = 1 / distance;
        return fitness;
    }


    void die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        die();
        sr.color = new Color(0, 0, 0, 1);
        Debug.Log("Rocket Collided with " + collision.gameObject.name);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Vector2 center = bc.bounds.center;
        Vector2 size = bc.bounds.size;
        Gizmos.DrawWireCube(center, size);
        Vector2 p1 = transform.position;
        Vector2 p2 = Target.instance.transform.position;
        Gizmos.DrawLine(p1, p2);
        Vector2 p3 = rb.linearVelocity.normalized;
        Gizmos.DrawLine(p1, p1 + p3);       
    }
}
