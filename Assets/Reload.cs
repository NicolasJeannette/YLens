using Microsoft.MixedReality.Toolkit.Subsystems;
using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class Reload : MonoBehaviour
{
    public int bulletLoader = 20;
    public int defaultBulletLoader = 20;
    public bool leftHandChecked;
    public TextMeshProUGUI UIloaderContainer;
    bool handIsValid;
    public TrackedHandJoint trackedJoint = TrackedHandJoint.IndexTip;
    [SerializeField]
    [Tooltip("The hand on which to track the joint.")]
    private Handedness hand;

    /// <summary>
    /// The hand on which to track the joint.
    /// </summary>
    protected Handedness Hand { get => hand; set => hand = value; }

    [SerializeField]
    [Tooltip("The specific joint to track.")]
    private TrackedHandJoint joint;

    /// <summary>
    /// The specific joint to track.
    /// </summary>
    protected TrackedHandJoint Joint { get => joint; set => joint = value; }

    private HandsAggregatorSubsystem handsAggregator;

    /// <summary>
    /// Cached reference to hands aggregator for efficient per-frame use.
    /// </summary>
    protected HandsAggregatorSubsystem HandsAggregator
    {
        get
        {
            if (handsAggregator == null)
            {
                handsAggregator = XRSubsystemHelpers.GetFirstRunningSubsystem<HandsAggregatorSubsystem>();
            }
            return handsAggregator;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        XRNode? node = Hand.ToXRNode();
        if (node.HasValue && HandsAggregator != null && HandsAggregator.TryGetJoint(joint, node.Value, out var jointPose))
        {
            transform.SetPositionAndRotation(jointPose.Position, jointPose.Rotation);
            {
                if (checkHandInput())
                {
                    bulletLoader = defaultBulletLoader;
                }
            }
        }
        else
        {
            // If we have no valid tracked joint, reset to local zero.
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        UIloaderContainer.text = "Munitions restantes : " + bulletLoader.ToString();
    }
    bool checkHandInput()
    {
        /*        HandJointPose pose;
                leftHandIsValid = handsAggregator.TryGetJoint(trackedJoint, XRNode.LeftHand, out pose);*/
        handIsValid = handsAggregator.TryGetPalmFacingAway(XRNode.LeftHand, out bool isPalmFacingAway);
        leftHandChecked = handIsValid;
        return handIsValid;
    }
}
