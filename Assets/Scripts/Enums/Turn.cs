using System;

/// <summary>
/// Enum that defines in which turn the game currently is
/// <para/>
/// A turn can be defined as player's, enemy's, both and none turns
/// </summary>
[Serializable]
public enum Turn
{
    Player,
    Enemy,
    Both,
    None
}