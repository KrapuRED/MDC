using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ScenarioDataSO", menuName = "Scriptable Objects/ScenarioDataSO")]
public class ScenarioDataSO : ScriptableObject
{
    [TextArea(10,20)]
    public string ScenarioText;
    public List<Sprite> CompanyHeirarchySprites;
}
