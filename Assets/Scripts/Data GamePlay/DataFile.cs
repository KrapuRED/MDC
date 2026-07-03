using UnityEngine;

public class DataFile : MonoBehaviour, IDragable, IHoverable
{
    private Vector2 dragOffset2D;

    private GroupData _ownerData;

    private void OnEnable()
    {
        _ownerData = GetComponentInParent<GroupData>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"[{gameObject.name}] is interact with cursor");
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
