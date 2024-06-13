using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _groundParticle;
    [SerializeField] private float _speed;
    [SerializeField] private float _sideSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float[] _lines = new float[3];
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameController _gameController;

    private Rigidbody _rb;
    private int _currentLine = 1;
    private bool _isGrounded;
    private bool _isJumped = false;
    private AudioSource _audioSource;


    public int CurrentLine { get { return _currentLine; } }
    public GameObject _ragdoll;
    public bool _isLive = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (_isLive)
        {
            _rb.velocity = new Vector3(0, _rb.velocity.y, _speed);

            Vector3 targetXPosition = new Vector3(_lines[_currentLine], transform.position.y, transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, targetXPosition, _sideSpeed);
        }
    }

    private void Update()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.3f);

        if (Input.GetKeyDown(KeyCode.D))
        {
            Right();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Left();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        _currentLine = Mathf.Clamp(_currentLine, 0, 2);
    }

    private void LateUpdate()
    {
        _animator.SetFloat("Velocity", _rb.velocity.magnitude);
    }

    public void Left()
    {
        _currentLine--;
    }

    public void Right() 
    {
        _currentLine++;
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
            _isJumped = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && _isJumped)
        {
            GameObject newParticle = Instantiate(_groundParticle, transform.position, Quaternion.identity, null);
            Destroy(newParticle, 3);
            _isJumped = false;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            _audioSource.Play();
            float _yFistance = transform.position.y - collision.transform.position.y;

            if (_yFistance < 0.9f && _isLive)
            {
                _isLive = false;
                _animator.gameObject.SetActive(false);
                Instantiate(_ragdoll, transform.position, Quaternion.identity, null);
                GetComponent<CapsuleCollider>().enabled = false;
                GetComponent<Rigidbody>().useGravity = false;
                StartCoroutine(ActiveLosePanel());

                _gameController.Save();
            }
        }
    }

    IEnumerator ActiveLosePanel()
    {
        yield return new WaitForSeconds(1);
        _losePanel.SetActive(true);
    }
}
