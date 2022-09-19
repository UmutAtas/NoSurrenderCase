using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
   private Rigidbody _rigidbody;
   private GameManager _gameManager;
   private Vector3 randomForward;
   private float forwardTimer;
   [SerializeField] private float movementSpeed;
   [SerializeField] private float findObjectRadius;
   private bool chase;
   private Transform chaseTransform;
   [SerializeField] private float upgradeAmount;

   private void Awake()
   {
      _rigidbody = GetComponent<Rigidbody>();
      _gameManager = FindObjectOfType<GameManager>();
   }

   private void Start()
   {
      RandomForward();
      ChangeForwardTimer();
      _gameManager.ActiveObjects.Add(gameObject);
   }

   private void Update()
   {
      if (!Grounded())
         RandomForward();
      Move();
      FindObjects();
      ChaseObjects();
      CheckActive();
   }

   private void Move()
   {
      transform.Translate(randomForward * (movementSpeed * Time.deltaTime), Space.World);
      transform.forward = randomForward;
      forwardTimer -= Time.deltaTime;
      if (forwardTimer <= 0)
      {
         RandomForward();
         ChangeForwardTimer();
      }
   }

   private bool Grounded()
   {
      return Physics.Raycast(transform.position , Vector3.down,0.5f,1<<6);
   }

   private void FindObjects()
   {
      Collider[] colliders = Physics.OverlapSphere(transform.position, findObjectRadius);
      foreach (var collider in colliders)
      {
         if (collider.gameObject.layer == 7)
         {
            if (chaseTransform == null)
            {
               chaseTransform = collider.transform;
               chase = true;
            }
         }
         else if (collider.gameObject.layer == 8)
         {
            if (chaseTransform == null)
            {
               chaseTransform = collider.transform;
               chase = true;
            }
         }
      }
   }

   private void ChaseObjects()
   {
      if (chase)
      {
         transform.LookAt(chaseTransform);
         transform.position = Vector3.Lerp(transform.position, chaseTransform.position, Time.deltaTime * movementSpeed);
      }
   }

   private void GetUpgrade(GameObject other)
   {
      chase = false;
      _rigidbody.mass += upgradeAmount;
      Destroy(other);
      chaseTransform = null;
   }

   private void Rebounce(Vector3 direction)
   {
      chase = false;
      _rigidbody.AddForce(200 * direction, ForceMode.Impulse);
      chaseTransform = null;
   }

   private void RandomForward()
   {
      randomForward = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
   }

   private void ChangeForwardTimer()
   {
      forwardTimer = Random.Range(0.5f, 1.5f);
   }

   private void CheckActive()
   {
      if (transform.position.y < -2f)
      {
         _gameManager.ActiveObjects.Remove(gameObject);
         _gameManager.IncreaseScore();
         Destroy(gameObject);
      }
   }

   private void OnCollisionEnter(Collision other)
   {
      if (other.gameObject.layer == 7) 
         Rebounce(other.transform.forward);
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.layer == 8)
         GetUpgrade(other.gameObject);
   }
}
