using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class BuffTable
{
    public string buffName;
    public string buffDescription;
    public int change;
    public BuffSO BuffData;
}

    public class BuffManager : MonoBehaviour
{
    public static BuffManager Instance { get; private set; }

    [SerializeField] private List<BuffTable> buffList = new();
    [SerializeField] private BuffSO selectedBuff;

    public BuffSO SelectedBuff => selectedBuff; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        OnGetRandomBuff();
    }

    private void OnGetRandomBuff()
    {
        int cumulativeChance = 0;

        for (int i = 0; i < buffList.Count; i++)
        {
            cumulativeChance += buffList[i].change;
            int randomValue = Random.Range(0, 100);

            if (randomValue < cumulativeChance)
            {
                selectedBuff = buffList[i].BuffData;
                ApplyBuff(selectedBuff);
                return;
            }
        }
    }

    private void ApplyBuff(BuffSO selectedBuffSO)
    {
        Debug.Log($"Applying Buff: {selectedBuffSO.buffName}");
        BuffInstance instance = selectedBuffSO.CreateInstance();
        instance.Apply();
    }
}
