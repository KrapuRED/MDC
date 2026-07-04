using UnityEngine;

[CreateAssetMenu(fileName = "ExtraHeartBuffSO", menuName = "Buffs/ExtraHeartBuffSO")]
public class ExtraHeartBuffSO : BuffSO
{
    public int extraHearts;
    public override BuffInstance CreateInstance() => new ExtraHeartBuffInstance(this);
}

public class ExtraHeartBuffInstance : BuffInstance
{
    private ExtraHeartBuffSO Data => (ExtraHeartBuffSO)data;
    public ExtraHeartBuffInstance(ExtraHeartBuffSO data) : base(data) { }

    public override void Apply()
    {
        PlayerHealthManager.Instance.OnGetExtraHeart(Data.extraHearts);
    }

    public override void Remove()
    {

    }
}
