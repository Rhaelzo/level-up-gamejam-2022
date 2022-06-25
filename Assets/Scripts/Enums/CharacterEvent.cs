using System;

/// <summary>
/// Enum with all of the character related message events
/// </summary>
[Serializable]
public enum CharacterEvent
{
    UpdateHealth,
    UpdateHealthUI,
    EnemyAITurnStart,
    EnemyAIWordFinished,
    EnemyAITurnEnd,
}