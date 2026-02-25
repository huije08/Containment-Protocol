using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Character : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Control()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        direction.Normalize();
    }

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
