using UnityEngine;

/// <summary>
/// Options for describing different phases of a locomotion.
/// </summary>
/// <remarks>
/// It can be used for connecting with the input actions for the locomotion.
/// </remarks>
/// <seealso cref="TunnelingProvider.LocomotionPhase"/>
public enum LocomotionPhase
{
    /// <summary>
    /// Describes the idle state of a locomotion, for example, when the user is standing still with no locomotion inputs.
    /// </summary>
    Idle,
    /// <summary>
    /// Describes the started state of a locomotion, for example, when the locomotion input action is started.
    /// </summary>
    Started,
    /// <summary>
    /// Describes the moving state of a locomotion, for example, when the user is continuously moving by pushing the joystick.
    /// </summary>
    Moving,
    /// <summary>
    /// Describes the done state of a locomotion, for example, when the user has ended moving.
    /// </summary>
    Done,
}

/// <summary>
/// The <see cref="TunnelingProvider"/> is the base class for various locomotion implementations.
/// This class provides simple ways to interrogate the <see cref="LocomotionSystem"/> for whether a locomotion can begin
/// and simple events for hooking into a start/end locomotion.
/// </summary>
//[DefaultExecutionOrder(XRInteractionUpdateOrder.k_LocomotionProviders)]
public abstract class TunnelingProvider : MonoBehaviour
{

    [SerializeField]
    [Tooltip("The Locomotion System that this locomotion provider communicates with for exclusive access to an XR Origin." +
        " If one is not provided, the behavior will attempt to locate one during its Awake call.")]
    TunnelingSystem m_System;

    /// <summary>
    /// The <see cref="LocomotionSystem"/> that this <see cref="TunnelingProvider"/> communicates with for exclusive access to an XR Origin.
    /// If one is not provided, the behavior will attempt to locate one during its Awake call.
    /// </summary>
    public TunnelingSystem System
    {
        get => m_System;
        set => m_System = value;
    }

    /// <summary>
    /// The <see cref="global::LocomotionPhase"/> of this <see cref="TunnelingProvider"/>.
    /// </summary>
    /// <remarks>
    /// Each <see cref="TunnelingProvider"/> instance can implement <see cref="global::LocomotionPhase"/> options
    /// based on their own logic related to locomotion, such as input actions and frames during the animation.
    /// </remarks>
    /// <seealso cref="global::LocomotionPhase"/>
    /// <seealso cref="TunnelingVignetteController"/>
    public LocomotionPhase LocomotionPhase { get; protected set; }

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    protected virtual void Awake()
    {
        if (m_System == null)
        {
            m_System = GetComponentInParent<TunnelingSystem>();
            if (m_System == null)
            {
                m_System = FindFirstObjectByType<TunnelingSystem>();
            }
        }
    }
}
