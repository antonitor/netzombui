using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected int damage = 10;

    public int Damage => damage;

    [SerializeField]
    public float bulletLife = 3.0f;

    [SerializeField]
    public float bulletSpeed = 15.0f;

    [SerializeField]
    public float weaponCooldown = 1.0f;

    [SerializeField]
    public int weaponAmmo = 30;

    [SerializeField]
    protected SpriteRenderer spriteRenderer;

    [SerializeField]
    public Bullet bulletPrefab;

    [SerializeField]
    public Transform weaponfirePosition;

    private Transform parent;

    private void Awake()
    {
        parent = gameObject.GetComponentInParent<Transform>();
    }

    private void Update()
    {
        //Debug.Log(parent.rotation.eulerAngles.z);
        if (parent.rotation.eulerAngles.z > 90 && parent.rotation.eulerAngles.z < 270)
        {
            spriteRenderer.flipY = true;
        }
        else
        {
            spriteRenderer.flipY = false;
        }
        if (parent.rotation.eulerAngles.z > 0 && parent.rotation.eulerAngles.z < 180)
        {
            spriteRenderer.sortingOrder = -1;
        }
        else
        {
            spriteRenderer.sortingOrder = 2;
        }
    }

}
