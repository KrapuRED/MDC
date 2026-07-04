using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class KeywordHighlight
{
    public string keyword;
    public Color color = Color.yellow;
}

[CreateAssetMenu(menuName = "Dialogue/Keyword Highlight Set")]
public class KeywordHighlightSO : ScriptableObject
{
    public List<KeywordHighlight> keywords = new();
}
