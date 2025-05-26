using Autohand;
using UnityEngine;

/// <summary>
/// The <see cref="TunnelingSystem"/> object is used to control access to the XR Origin. This system enforces that only one
/// Locomotion Provider can move the XR Origin at one time. This is the only place that access to an XR Origin is controlled,
/// having multiple instances of a <see cref="TunnelingSystem"/> drive a single XR Origin is not recommended.
/// </summary>
public class TunnelingSystem : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The AutohandPlayer object to provide access control to.")]
    AutoHandPlayer m_autohandPlayer;

    /// <summary>
    /// The XR Origin object to provide access control to.
    /// </summary>
    public AutoHandPlayer AutohandPlayer
    {
        get => m_autohandPlayer;
        set => m_autohandPlayer = value;
    }

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    protected void Awake()
    {
        if (m_autohandPlayer == null)
        {
            m_autohandPlayer = GetComponentInParent<AutoHandPlayer>();
            if (m_autohandPlayer == null)
            {
                m_autohandPlayer = FindFirstObjectByType<AutoHandPlayer>();
            }
        }
    }
}
