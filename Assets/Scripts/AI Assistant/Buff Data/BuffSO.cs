using UnityEngine;

public abstract class BuffSO : ScriptableObject
{
    public string buffName;
    public string buffDescription;
    public abstract BuffInstance CreateInstance();
}

public abstract class BuffInstance
{
    protected BuffSO data;
    public BuffInstance(BuffSO data) => this.data = data;
    public abstract void Apply();
    public abstract void Remove();
}
