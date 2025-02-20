using UnityEngine;

public abstract class EntityInput : MonoBehaviour
{
    public event System.Action<Vector3> OnMove;
    public event System.Action<bool> OnShootingChanged;

    //Can be changed to epsilon comparison
    public bool IsMoving => _moveDir != Vector3.zero;
    
    protected Vector3 _moveDir = Vector3.zero;
    public Vector3 MoveDir
    {
        get => _moveDir;
        set
        {
            _moveDir = value;
            OnMove?.Invoke(_moveDir);
        }
    }
    
    protected bool _isShooting = false;
    public bool IsShooting
    {
        get => _isShooting;
        set
        {
            _isShooting = value;
            OnShootingChanged?.Invoke(_isShooting);
        }
    }
}
