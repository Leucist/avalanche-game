# Coding Conventions for Avalanche Game Project

## General Principles
- **Readability:** Code should be clear and easy to understand.
- **Consistency:** Follow the same naming and formatting style throughout the project.

## Naming Conventions

### Classes and Interfaces
- Use **PascalCase** for class names and interface names.
  - Examples: `Player`, `Enemy`, `IInteractable`

### Methods
- Use **PascalCase** for method names.
  - Examples: `MovePlayer()`, `AttackEnemy()`

### Variables and Fields
- Use **camelCase** for local variables.
- For class fields, use **camelCase** with a prefix `_`.
  - Examples: `playerHealth`, `_currentLevel`

### Properties
- Use **PascalCase** for property names.
  - Examples: `Health`, `Level`, `IsAlive`

### Constants
- Use **PascalCase** for constant names.
  - Examples: `MaxHealth`, `StartGold`

### Method Parameters
- Use **camelCase** for method parameters.
  - Examples: `damageAmount`, `targetEnemy`

## Code Formatting
- **Indentation:** Use 4 spaces for indentation.
- **Line Length:** Limit line length to 80-120 characters.
- **Whitespace:** Use spaces around operators and after commas for improved readability.

## Comments
- Use XML comments to describe classes and methods. This helps generate documentation and enhances code understanding.

### Example Code

```csharp
/// <summary>
/// Represents the player character in the game.
/// </summary>
public class Player
{
    private int _health;
    private int _gold;

    /// <summary>
    /// Gets or sets the player's health.
    /// </summary>
    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    /// <summary>
    /// Moves the player in the specified direction.
    /// </summary>
    /// <param name="direction">The direction to move.</param>
    public void Move(Direction direction)
    {
        // Logic for moving the player
    }

    /// <summary>
    /// Attacks the specified enemy.
    /// </summary>
    /// <param name="targetEnemy">The enemy to attack.</param>
    public void AttackEnemy(Enemy targetEnemy)
    {
        int damageAmount = 10; // Example damage
        targetEnemy.TakeDamage(damageAmount);
    }
}

/// <summary>
/// Represents an enemy character in the game.
/// </summary>
public class Enemy
{
    public int Health { get; private set; } = 20;

    /// <summary>
    /// Inflicts damage on the enemy.
    /// </summary>
    /// <param name="damage">The amount of damage to inflict.</param>
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            // Logic for when the enemy is defeated
        }
    }
}
```

## Namespace Usage
- Apply a logical hierarchy for namespaces reflecting the project's structure.
  - Examples: Avalanche.Core.Entities.Player, Avalanche.Core.Entities.Skeleton

By following these conventions, we ensure a cohesive and maintainable codebase.
