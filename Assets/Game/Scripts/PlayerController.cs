using Mirror;
using TMPro;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    private float moveSpeed = 200f;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SpriteRenderer model;

    //player movement
    private Vector2 movement;

    [SerializeField] 
    private int _playerNumber = 1;
    public int PlayerNumber => _playerNumber;

    private SceneScript sceneScript;

    public GameObject floatingInfo;
    public TextMeshPro playerNameText;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;
    private void OnNameChanged(string _Old, string _New)
    {
        playerNameText.text = playerName;
    }


    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor = Color.white;
    private void OnColorChanged(Color _Old, Color _New)
    {
        playerNameText.color = _New;
    }

    //sprite flipX
    [SyncVar(hook = nameof(OnChangeLookingRight))]
    private bool lookingRight = false;
    private void OnChangeLookingRight(bool _, bool newLookingRightState)
    {
        model.flipX = newLookingRightState;
    }

    //Client chache
    private bool oldLookingRight = false;
    private bool oldWalkingValue = false;
    private bool oldBackwardsValue = false;

    // Start is called before the first frame update

    public override void OnStartLocalPlayer()
    {
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);

        string name = "Player" + Random.Range(100, 999);
        Color color = new Color(Random.Range(.7f, 1f), Random.Range(.7f, 1f), Random.Range(.7f, 1f));
        CmdSetupPlayer(name, color);

        sceneScript.playerController = this;
    }

    [Command]
    public void CmdSetupPlayer(string _name, Color _col)
    {
        playerName = _name;
        playerColor = _col;
        sceneScript.statusText = $"{playerName} joined.";
    }

    private void Awake()
    {
        sceneScript = GameObject.Find("SceneReference").GetComponent<SceneReference>().sceneScript;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;

        PlayerInput();
        AnimateCharacter();

    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer) return;

        if (movement.x != 0 && movement.y != 0)
        {
            rb2d.velocity = new Vector2(movement.x * Time.fixedDeltaTime * moveSpeed * 0.7f, movement.y * Time.fixedDeltaTime * moveSpeed * 0.7f);
        }
        else
        {
            rb2d.velocity = new Vector2(movement.x * Time.fixedDeltaTime * moveSpeed, movement.y * Time.fixedDeltaTime * moveSpeed);
        }
    }

    private void PlayerInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }


    private void AnimateCharacter()
    {
        bool lookingRightValue = movement.x > 0 ? true : movement.x < 0 ? false : oldLookingRight;
        if (oldLookingRight != lookingRightValue)
        {
            oldLookingRight = lookingRightValue;
            CmdUpdateFacingDir(lookingRightValue);
        }
        bool isWalkingValue = movement.sqrMagnitude != 0;
        if (oldWalkingValue != isWalkingValue)
        {
            oldWalkingValue = isWalkingValue;
            animator.SetBool("Walking", isWalkingValue);
        }
        bool isBackwardsValue = movement.y > 0 ? true : movement.y < 0 ? false : oldBackwardsValue;
        if (isBackwardsValue != oldBackwardsValue)
        {
            oldBackwardsValue = isBackwardsValue;
            animator.SetBool("Backwards", isBackwardsValue);
        }
    }


    [Command]
    private void CmdUpdateFacingDir(bool isLookingRight)
    {
        lookingRight = isLookingRight;
    }

    [Command]
    public void CmdSendPlayerMessage()
    {
        if (sceneScript)
            sceneScript.statusText = $"{playerName} says hello";
    }
}
