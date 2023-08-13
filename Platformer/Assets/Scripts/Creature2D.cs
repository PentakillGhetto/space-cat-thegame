using UnityEngine;

/// <summary>
/// Ассет существ в игре. 
/// </summary>
[CreateAssetMenu(fileName = "creature_name", menuName = "Creature")]
public class Creature2D : ScriptableObject
{
    public Vector2 groundPosition;
    public Vector2 ceilingPosition;
    public LayerMask layersWhatIsGround;
    public float maxSpeed = 10f;
    public float jumpForce = 400f;
    public bool AirControlled = false;
    public int maxHealth = 100;

    /// <summary>
    /// Радиус "ног", использующийся для проверки, стоит ли существо.
    /// </summary>
    // 0.2f
    public float GroundCheckRadius = 0.2f;

    /// <summary>
    /// Радиус "головы", использующийся проверки, не уперлась ли голова в потолок.
    /// </summary>
    // 0.01f
    public float CeilingCheckRadius = 0.01f;
}
