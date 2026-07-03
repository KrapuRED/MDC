using UnityEngine;

public class DataFile : MonoBehaviour, IDragable, IHoverable
{

    [SerializeField] private DataType dataType;

    private GroupData _ownerData;

    public DataType DataType => dataType;

    private void OnEnable()
    {
        _ownerData = GetComponentInParent<GroupData>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"[{gameObject.name}] is interact with cursor");
    }

    public void SetDataType(DataType type)
    {
        dataType = type;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log($"[{gameObject.name}] is interact with cursor");
    }

    private void DataCrash()
    {

    }

    public void OnDrag()
    {
        transform.SetParent(null, worldPositionStays: true);
    }

    public void OnDrop()
    {
        // if put in recycler, destroy this object
    }
}
