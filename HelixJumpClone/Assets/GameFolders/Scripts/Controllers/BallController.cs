using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    [SerializeField] Material _breakModeMat;
    [SerializeField] Gradient _breakModeColorGradient;
    [SerializeField] float _jumpForce = 100;
    [SerializeField] int _breakModeEnterComboCount = 3;
    [SerializeField] CameraController _mainCam;


    int _comboCounter;
    bool _isInBreakMode;    

    Rigidbody _rb;
    Animator _anim;
    Material _normalModeMat;
    Vector3 zeroVector = Vector3.zero;  // better perf?
    Vector3 upVector = Vector3.up;      // better perf?


    private void Awake()
    {
        _anim= GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _rb.sleepThreshold = 0f;
        _normalModeMat = GetComponent<MeshRenderer>().material;
    }
    private void Update()
    {
        
        _anim.SetFloat("VelocityY", Mathf.Abs(_rb.velocity.y));
    }
    private void OnCollisionEnter(Collision collision)
    {
        _comboCounter = 0;
        ContactPoint contact = collision.contacts[0];
        if (contact.normal.y < 0.73f)
        {
            //Prevent input
            return;
        }
 
        if (collision.collider.CompareTag("Safe"))
        {
            SafePlatformActions(collision);
        }
        else if(collision.collider.CompareTag("Unsafe"))
        {
            if (_isInBreakMode)
            {
                SafePlatformActions(collision);
            }
            else
            {
                GameManager.Instance.GameOver();         

            }
        }
        else if(collision.collider.CompareTag("LastRing"))
        {
            GameManager.Instance.GameCompleted();
        }
        if (_isInBreakMode)
        {
            Destroy(collision.transform.parent.gameObject);
            _isInBreakMode = false;
            GetComponent<MeshRenderer>().material = _normalModeMat;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        
        _mainCam.FollowTheBall = true;
        Destroy(other.transform.gameObject);
        if (!_isInBreakMode)
        {
            GameManager.Instance.IncreaseScore(10);
            _comboCounter++;
            if (_comboCounter == _breakModeEnterComboCount)
            {
                _isInBreakMode = true;
                GetComponent<MeshRenderer>().material = _breakModeMat;
                GetComponent<TrailRenderer>().colorGradient = _breakModeColorGradient;
            }

        }
        else
        {

            GameManager.Instance.IncreaseScore(20);
        }
    }
    void SafePlatformActions(Collision collision)
    {
        _anim.SetTrigger("OnHit");
        _mainCam.FollowTheBall = false;
        _mainCam.CurrentPos = this.transform.position;

        _rb.velocity = zeroVector;
        _rb.velocity = upVector * _jumpForce;

        GameObject splashObject = ObjectPoolManager.Instance.GetSplashFromPool();
        splashObject.transform.position = collision.contacts[0].point + new Vector3(0,0.2f,0);
        splashObject.transform.SetParent(collision.transform);
    }
}
