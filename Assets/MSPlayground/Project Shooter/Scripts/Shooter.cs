using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using UnityEngine.InputSystem;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Subsystems;

public class Shooter : MonoBehaviour
{
    public GameObject bullet;
    public float pinchThreshold = 0.05f; // distance threshold for pinch detection
    public GameObject pinchObject; // object to manipulate when pinch is detected

    //private HandTrackingInputEventData handInputEventData;
    //private  handTrackingService;
    private HandsAggregatorSubsystem handTrackingService;
    ///*private HandsAggregatorSubsystem handTrackingService;*/
    //private Microsoft.MixedReality.Toolkit.Input handJointInteractor;


    private void Start()
    {
        //handTrackingService = Microsoft.MixedReality.Toolkit.Input.HandTrackingInputSystem as HandTrackingService;
        handTrackingService = XRSubsystemHelpers.GetFirstRunningSubsystem<HandsAggregatorSubsystem>();
        
    }

    private void Update()
    {
        if (handTrackingService != null)
        {
            //// Get the hand input event data
            //var handInputEventData = handTrackingService..InputSystemProfile.DataProvider.GetData<HandTrackingInputEventData>();

            // Check if both thumb and index tips are being tracked

            
            //if (handInputEventData.HandData.HandJoints[TrackedHandJoint.ThumbTip].IsTracked
            //    && handInputEventData.HandData.HandJoints[TrackedHandJoint.IndexTip].IsTracked)
            //{
            //    // Get the positions of the thumb and index tips
            //    Vector3 thumbTipPos = handInputEventData.HandData.HandJoints[TrackedHandJoint.ThumbTip].Position;
            //    Vector3 indexTipPos = handInputEventData.HandData.HandJoints[TrackedHandJoint.IndexTip].Position;

            //    // Calculate the distance between the two tips
            //    float distance = Vector3.Distance(thumbTipPos, indexTipPos);

            //    // If the distance is below the pinch threshold, activate the pinch action
            //    if (distance < pinchThreshold)
            //    {
            //        //pinchObject.GetComponent<Renderer>().material.color = Color.green;
            //        //Code pour l'action souhaité
            //    }
            //    else
            //    {
            //        //pinchObject.GetComponent<Renderer>().material.color = Color.white;
            //    }
            //}
        }
    }
}
