using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MovementEvent : UnityEvent<Vector2>
{
}

public class InputSystem : MonoBehaviour
{
    [SerializeField] private KeyCode keyLeft = KeyCode.A;
    public MovementEvent OnMoveLeftPressedDown;
    public MovementEvent OnMoveLeftPressed;
    public MovementEvent OnMoveLeftPressedUp;

    [SerializeField] private KeyCode keyRight = KeyCode.D;
    public MovementEvent OnMoveRightPressedDown;
    public MovementEvent OnMoveRightPressed;
    public MovementEvent OnMoveRightPressedUp;

    [SerializeField] private KeyCode keyJump = KeyCode.Space;
    public UnityEvent OnJumpPressedDown;
    public UnityEvent OnJumpPressed;
    public UnityEvent OnJumpPressedUp;

    [SerializeField] private KeyCode keyFire = KeyCode.RightControl;
    public UnityEvent OnFirePressedDown;
    public UnityEvent OnFirePressed;
    public UnityEvent OnFirePressedUp;

    [SerializeField] private KeyCode keyInteract = KeyCode.E;
    public UnityEvent OnMoveInteractPressedDown;
    public UnityEvent OnMoveInteractPressed;
    public UnityEvent OnMoveInteractPressedUp;

    [SerializeField] private KeyCode keyDrop = KeyCode.G;
    public UnityEvent OnDropItemPressedDown;
    public UnityEvent OnDropItemPressed;
    public UnityEvent OnDropItemPressedUp;

    [SerializeField] private KeyCode keyHome = KeyCode.M;
    public UnityEvent OnHomePressedDown;

    private void Update()
    {
        if (Input.GetKeyDown(keyLeft)) OnMoveLeftPressedDown.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), 0));
        if (Input.GetKey(keyLeft)) OnMoveLeftPressed.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), 0));
        //else OnMoveLeftPressed.Invoke(new Vector2(Input.GetAxis("Horizontal"), 0));
        if (Input.GetKeyUp(keyLeft)) OnMoveLeftPressedUp.Invoke(new Vector2(0, 0));

        if (Input.GetKeyDown(keyRight)) OnMoveRightPressedDown.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), 0));
        if (Input.GetKey(keyRight)) OnMoveRightPressed.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), 0));
        //else OnMoveLeftPressed.Invoke(new Vector2(Input.GetAxis("Horizontal"), 0));
        if (Input.GetKeyUp(keyRight)) OnMoveRightPressedUp.Invoke(new Vector2(0, 0));

        if (Input.GetKeyDown(keyJump)) OnJumpPressedDown.Invoke();
        if (Input.GetKey(keyJump)) OnJumpPressed.Invoke();
        if (Input.GetKeyUp(keyJump)) OnJumpPressedUp.Invoke();

        if (Input.GetKeyDown(keyFire)) OnFirePressedDown.Invoke();
        if (Input.GetKey(keyFire)) OnFirePressed.Invoke();
        if (Input.GetKeyUp(keyFire)) OnFirePressedUp.Invoke();

        if (Input.GetKeyDown(keyInteract)) OnMoveInteractPressedDown.Invoke();
        if (Input.GetKey(keyInteract)) OnMoveInteractPressed.Invoke();
        if (Input.GetKeyUp(keyInteract)) OnMoveInteractPressedUp.Invoke();

        if (Input.GetKeyDown(keyDrop)) OnDropItemPressedDown.Invoke();
        if (Input.GetKey(keyDrop)) OnDropItemPressed.Invoke();
        if (Input.GetKeyUp(keyDrop)) OnDropItemPressedUp.Invoke();

        if (Input.GetKeyDown(keyHome)) OnHomePressedDown.Invoke();
    }
}
