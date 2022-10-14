using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sgooting : MonoBehaviour
{
    public float speed = 2f;
    [SerializeField] private float Returnspeed;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform _target;

    private Collider2D colliderthing;

    private bool dir = false;

    public float maxTimer = 4f;
    private float returnTimer = 0f;

    private void Start()
    {
        _target = PlayerMovement.Instance.transform;

        dir = PlayerMovement.Instance._renderer.flipX;

        colliderthing = GetComponent<Collider2D>();

        StartCoroutine(DelayedTriggerEnable());
    }
    private void Update()
    {
        returnTimer += Time.deltaTime;

        if (returnTimer < maxTimer)
        {
            if (dir)
            {
                transform.Translate(-transform.right * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(transform.right * speed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position + offset, speed * Time.deltaTime);
        }
    }

    private IEnumerator DelayedTriggerEnable()
    {
        colliderthing.enabled = false;
        yield return new WaitForSeconds(1f);

        colliderthing.enabled = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(_target.position + offset, 0.5f);
    }
}
