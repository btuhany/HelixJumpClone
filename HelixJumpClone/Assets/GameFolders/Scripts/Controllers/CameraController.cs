using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float _lerpSpeed;
    [SerializeField] Transform _ballTransform;

    Vector3 _offset;
    public Vector3 CurrentPos;
   
    public bool FollowTheBall = true;
    private void Awake()
    {
        _offset = transform.position - _ballTransform.position;
        // CurrentPos = _ballTransform.position;
        FollowTheBall = true;
    }
    private void LateUpdate()
    {

        if (FollowTheBall)
        {
            transform.position = Vector3.Lerp(transform.position, _ballTransform.position + _offset, Time.fixedDeltaTime * _lerpSpeed);
        }
        else
        {
            
            transform.position = Vector3.Lerp(transform.position, CurrentPos + _offset, Time.fixedDeltaTime * _lerpSpeed);
        }

    }

}
