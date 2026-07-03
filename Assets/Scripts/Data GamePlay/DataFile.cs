using UnityEngine;
using System.Collections;

public class DataFile : MonoBehaviour, IDragable, IHoverable
{

    [SerializeField] private DataType dataType;

    [Header("Data File Immune Crash Config")]
    [SerializeField] private float immuneTime; // Time during which the data file is immune to crashes after being spawned
    [SerializeField] private bool isImmune;
    [SerializeField] private float currentImmuneTimer;

    [Header("Data Destroy config")]
    [SerializeField] private float crashDestroyDelay;
    [SerializeField] private float dropDataDestroyDelay;
    private float _destroyTimer;

    [Header("Data File Sprites")]
    [SerializeField] private Sprite falseSprite;
    [SerializeField] private Sprite violationSprite;
    [SerializeField] private Sprite educationSprite;
    [SerializeField] private Sprite entertainmentSprite;
    [SerializeField] private Sprite politicSprite;
    [SerializeField] private Sprite personalSprite;

    Coroutine destroyCoroutine;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private GroupData _ownerData;
    private bool _isDragging;

    public DataType DataType => dataType;

    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _ownerData = GetComponentInParent<GroupData>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (currentImmuneTimer >= immuneTime)
        {
            isImmune = false;
            return;
        }

        if (_isDragging && isImmune)
        {
            currentImmuneTimer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isDragging) return; // only crash while actively being dragged

        if (collision.TryGetComponent(out DataFile otherData))
        {
            if (!isImmune)
                DataCrash(otherData);
        }
    }


    public void SetDataType(DataType type)
    {
        dataType = type;
        if (_spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is not assigned. Please ensure the component is attached.");
            return;
        }

        _spriteRenderer.sprite = type switch
        {
            DataType.False => falseSprite,
            DataType.Violation => violationSprite,
            DataType.Education => educationSprite,
            DataType.Entertainment => entertainmentSprite,
            DataType.Polictic => politicSprite,
            DataType.Personal => personalSprite,
            _ => null
        };
    }

    private void DataCrash(DataFile other)
    {
        if (destroyCoroutine != null)
        {
            return; // Already in the process of being destroyed
        }

        Debug.Log($"[{gameObject.name}] crashed into [{other.gameObject.name}]");
        PlayerHealthManager.Instance.OnTakingDamage();

        other.destroyCoroutine = other.StartCoroutine(other.DestroyAfterDelay(other.crashDestroyDelay));
        destroyCoroutine = StartCoroutine(DestroyAfterDelay(crashDestroyDelay));
        // e.g. destroy both, play VFX, reset drag, penalize player, etc.
    }

    public void OnDrag()
    {
        //_boxCollider2D.isTrigger = false;
        _isDragging = true;

        transform.SetParent(null, worldPositionStays: true);
    }

    public void OnDrop()
    {
        _isDragging = false;
        // if put in recycler, destroy this object
        //_boxCollider2D.isTrigger = true;
    }

    public void DestroyDataFileByDropFile()
    {
        if (destroyCoroutine != null)
        {
            return; // Already in the process of being destroyed
        }

        //Fade out animation, VFX, etc.
        Debug.Log($"[{gameObject.name}] is destroyed after being dropped into the drop box.");
        destroyCoroutine = StartCoroutine(DestroyAfterDelay(dropDataDestroyDelay));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        Debug.Log($"[{gameObject.name}] will be destroyed after {delay} seconds.");

        //Detach from cursor

        transform.SetParent(null, worldPositionStays: true);
        _boxCollider2D.enabled = false;

        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
