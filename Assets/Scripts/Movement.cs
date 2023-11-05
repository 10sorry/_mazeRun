using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] private Level _level;
    public bool isOnLevel3;

    private Vector2 _swipeStartPos;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private float _step;
    private SwipeController _swipeController;
    private bool _isMoving = false;
    private Vector3 _destPos;
    public bool SFXPlay { get; private set; } = false;


    private void Awake()
    {
        _swipeController = GetComponent<SwipeController>();
        _swipeController.SwipeEvent += OnSwipe;
    }

    private void OnSwipe(Vector2 direction)
    {
        Vector3 dir = direction == Vector2.up ? Vector3.forward : direction == Vector2.down ? Vector3.back : direction;

        _isMoving = TryMove(dir);
    }

    private void Update()
    {
        if (_isMoving)
        {
            float step = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _destPos, step);
            if (transform.position == _destPos)
            {
                _isMoving = false;
                SFXPlay = false;
            }
            else
            {
                SFXPlay = true;
            }
        }
        else
        {
            HandleKeyboardInput();
        }
    }

    private void HandleKeyboardInput()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            moveDirection = Vector3.forward;
        else if (Input.GetKey(KeyCode.A))
            moveDirection = Vector3.left;
        else if (Input.GetKey(KeyCode.S))
            moveDirection = Vector3.back;
        else if (Input.GetKey(KeyCode.D))
            moveDirection = Vector3.right;

        if (moveDirection != Vector3.zero)
        {
            _isMoving = TryMove(moveDirection);
        }
    }

    bool TryMove(Vector3 direction)
    {
        transform.forward = direction;
        _destPos = transform.position + direction * _step;
        Ray forwardRay = new(transform.position, direction);
        if (Physics.Raycast(forwardRay, out _, _step, _obstacleMask))
        {
            return false;
        }
        else
        {
            if (MusicControllerScript.Instance != null)
                MusicControllerScript.Instance.PlaySound("step");
            return true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("false"))
        {
            _level.Lose();
        }
    }

}
