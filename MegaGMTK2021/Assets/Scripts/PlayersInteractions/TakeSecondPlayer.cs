using UnityEngine;

public class TakeSecondPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform turretPlacer;
    public KeyCode takingPlayerKey = KeyCode.E;
    public KeyCode playerThrowing = KeyCode.Space;
    private bool inSecondPlayer = false;
    private Transform secondPlayer = null;
    private Rigidbody secondPlayerRigidbody = null;
    private CapsuleCollider secondPlayerCapsuleCollider = null;
    private BoxCollider secondPlayerBoxCollider = null;
    public MeshCollider secondPlayerMesh = null;
    public MeshCollider secondPlayerMesh2 = null;
    private RotatingOfWeapon secondPlayerRot = null;
    private string secondPlayerName = "SecondPerson";
    private bool taked = false;
    private Transform parentOfSecondPlayer;
    public float forceValue;
    private Camera playerCamera;

    public GameObject text;
    public GameObject restartButton;

    private Vector3 velocityOfSecondPlayer;
    private float smoothTime = 0.35f;
    private float takeTime = 0.03f;

    public Animator anim;
    private Vector3 velocity;

    public GameObject playersList;

    private void Awake()
    {
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Enemy")
        {
            Destroy(player);
            playersList.GetComponent<PlayerList>().GameOver();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.name == secondPlayerName)
        {
            if (secondPlayer == null)
            {
                secondPlayer = col.transform;

                parentOfSecondPlayer = secondPlayer.parent;
                secondPlayerRigidbody = secondPlayer.GetComponent<Rigidbody>();
                secondPlayerCapsuleCollider = secondPlayer.GetComponent<CapsuleCollider>();
                secondPlayerBoxCollider = secondPlayer.GetComponent<BoxCollider>();
                secondPlayerRot = secondPlayer.GetComponent<RotatingOfWeapon>();
            }

            inSecondPlayer = true;
        }
        else if (col.transform.tag == "Enemy")
        {
            Destroy(player);
            playersList.GetComponent<PlayerList>().GameOver();
        }
        else if (col.transform.tag == "Button")
        {
            col.transform.GetComponent<ButtonDown>().entered = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.transform.name == secondPlayerName)
        {
            inSecondPlayer = false;
        }
    }

    private float doingNorm(float value)
    {
        if (value > 0.5f)
            return 1f;
        if (value < -0.5f)
            return -1f;
        return 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(takingPlayerKey) && inSecondPlayer && !taked)
        {
            taked = true;
            secondPlayerRot.AmIGrounded = false;
            anim.SetBool("TurretTake", true);
            secondPlayerRigidbody.useGravity = false;
            secondPlayerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            secondPlayerCapsuleCollider.enabled = false;
            secondPlayerBoxCollider.enabled = false;
            secondPlayerMesh.enabled = false;
            secondPlayerMesh2.enabled = false;
            secondPlayer.parent = gameObject.transform;
            secondPlayer.position = turretPlacer.position;
        }
        else if (Input.GetKeyDown(takingPlayerKey) && taked)
        {
            taked = false;
            inSecondPlayer = false;
            secondPlayerRot.AmIGrounded = true;
            anim.SetBool("TurretTake", false);
            secondPlayerRigidbody.useGravity = true;
            secondPlayerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            secondPlayerCapsuleCollider.enabled = true;
            secondPlayerBoxCollider.enabled = true;
            secondPlayerMesh.enabled = true;
            secondPlayerMesh2.enabled = true;
            secondPlayer.parent = parentOfSecondPlayer;
        }
        else if (Input.GetKeyDown(playerThrowing) && taked)
        {
            taked = false;
            inSecondPlayer = false;
            secondPlayerRot.AmIGrounded = true;
            anim.SetBool("TurretTake", false);
            secondPlayerRigidbody.useGravity = true;
            secondPlayerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            secondPlayerCapsuleCollider.enabled = true;
            secondPlayerBoxCollider.enabled = true;
            secondPlayerMesh.enabled = true;
            secondPlayerMesh2.enabled = true;
            secondPlayer.parent = parentOfSecondPlayer;
            secondPlayerRigidbody.AddForce(playerCamera.transform.forward * forceValue);
        }
    }
}