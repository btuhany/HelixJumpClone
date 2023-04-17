using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleController : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;

    Transform _transform;
    private void Awake()
    {
        _transform= transform;
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            float inputX = Input.GetAxis("Mouse X");
            _transform.Rotate(0, _rotationSpeed * Time.deltaTime * -inputX, 0);
        }
        
    }

}
