using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected float damage = 10;

    [SerializeField]
    protected float range = 300f;

    [SerializeField]
    protected SpriteRenderer spriteRenderer;

    [SerializeField]
    public Bullet bulletPrefab;

    [SerializeField]
    public Transform fireSpawnPoint;

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
