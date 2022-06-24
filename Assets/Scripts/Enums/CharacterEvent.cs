using System;

[Serializable]
public enum CharacterEvent
{
    UpdateHealth,
    UpdateHealthUI,
    EnemyAITurnStart,
    EnemyAIWordFinished,
    EnemyAITurnEnd,
}