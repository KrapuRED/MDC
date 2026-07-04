using UnityEngine;

[CreateAssetMenu(fileName = "SlowDataFlowLineBuffSO", menuName = "Buffs/SlowDataFlowLineBuffSO")]
public class SlowDataFlowLineBuffSO : BuffSO
{
    public DataFlowLineSpeed speedMode;
    public override BuffInstance CreateInstance() => new SlowDataFlowLineBuffInstance(this);
}

public class SlowDataFlowLineBuffInstance : BuffInstance
{
    private SlowDataFlowLineBuffSO Data => (SlowDataFlowLineBuffSO)data;
    public SlowDataFlowLineBuffInstance(BuffSO buffSO) : base(buffSO)
    {
    }

    public override void Apply()
    {
        // Implement the effect of slowing down data flow lines here
        Debug.Log("Applying Slow Data Flow Line Buff Effect");
        DataFlowLineManager.instance.ApplySpeedModifier(Data.speedMode);
    }

    public override void Remove()
    {
        // Implement the logic to remove the effect here
        Debug.Log("Removing Slow Data Flow Line Buff Effect");
    }
}
