using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Joystick _joystick;
    private GameManager _gameManager;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float upgradeAmount;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _joystick = GetComponent<Joystick>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        _gameManager.ActiveObjects.Add(gameObject);
        _gameManager._player = gameObject;
    }

    private void Update()
    {
        Move();
        CheckActive();
    }

    private void Move()
    {
        var direction = _joystick.direction;
        Vector3 forward = new Vector3(direction.x, transform.position.y, direction.y);
        transform.Translate(forward * (movementSpeed * Time.deltaTime), Space.World);
        transform.forward = forward;
    }
    
    private void CheckActive()
    {
        if (transform.position.y < -2f)
            _gameManager.ActiveObjects.Remove(gameObject);
    }

    private void Rebounce(Vector3 direction)
    {
        rb.AddForce(75 * direction, ForceMode.Impulse);
    }
    
    private void GetUpgrade(GameObject other)
    {
        rb.mass += upgradeAmount;
        Destroy(other);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 9)
            Rebounce(other.transform.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
            GetUpgrade(other.gameObject);
    }
}
