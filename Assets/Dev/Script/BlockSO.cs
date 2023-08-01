using UnityEngine;

[CreateAssetMenu(fileName = "BlockSO", menuName = "Scriptable/BlockSO", order = 0)]
public class BlockSO : ScriptableObject
{
    public string blockName;
    public Sprite Sprite_Default;
    public Sprite Sprite_A;
    public Sprite Sprite_B;
    public Sprite Sprite_C;
}