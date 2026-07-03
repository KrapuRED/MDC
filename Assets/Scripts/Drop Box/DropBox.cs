using UnityEngine;

public class DropBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"[{gameObject.name}] is interact with [{collision.gameObject.name}]");
        DataFile dataFile = collision.gameObject.GetComponent<DataFile>();
        if (dataFile != null)
        {
            DropDataFile(dataFile);
        }
    }

    public void DropDataFile(DataFile data)
    {
        Debug.Log($"Data file {data.name} dropped into the drop box.");

        data.DestroyDataFileByDropFile();

        TaskManager.Instance.CheckDropFile(data);
    }
}
