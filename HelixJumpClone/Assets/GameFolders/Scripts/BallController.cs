using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;

    float _limitedVelocityY;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _limitedVelocityY = Mathf.Clamp(_rb.velocity.y, -5f,5f);
        
    }
    private void FixedUpdate()
    {
        SetVelocityY(_limitedVelocityY);
    }
    void SetVelocityY(float value)
    {
        _rb.velocity = new Vector3(0, value, 0);
    }
}
