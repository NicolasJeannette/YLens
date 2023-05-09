using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Subsystems;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEditor.Localization.Addressables;
using UnityEngine;
using UnityEngine.XR;

public class Shooter : MonoBehaviour
{
    // --- Audio ---
    public AudioClip GunShotClip;
    public AudioSource source;
    public Vector2 audioPitch = new Vector2(.9f, 1.1f);

    // Start is called before the first frame update
    public GameObject bullet;
    public float timeBeforeFire = .5f;
    public float DefaultTimeBeforeFire = .5f;
    public GameObject reloader;
    private Reload reload;
    bool handIsValid;
    public TrackedHandJoint trackedJoint = TrackedHandJoint.IndexTip;
    [SerializeField]
    [Tooltip("The hand on which to track the joint.")]
    private Handedness hand;
    private XRNode? node;

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
    void Awake()
    {
        reload = reloader.GetComponent<Reload>();
        node = Hand.ToXRNode();
    }

    // Update is called once per frame
    void Update()
    {
        if (node.HasValue && HandsAggregator != null && HandsAggregator.TryGetJoint(joint, node.Value, out var jointPose))
        {
            transform.SetPositionAndRotation(jointPose.Position, jointPose.Rotation);
            if (timeBeforeFire > 0)
            {
                timeBeforeFire -= Time.deltaTime;
            }
            else if (reload.bulletLoader > 0 && !reload.leftHandChecked)
            {
                Debug.Log("Poogie fired a bullet");
                GameObject bullet = ObjectPooling.SharedInstance.GetPooledObject();
                if (bullet != null && checkHandInput())
                {
                    bullet.transform.up = -transform.up;
                    bullet.transform.position = transform.position;
                    reload.bulletLoader--;
                    bullet.SetActive(true);

                    // --- Handle Audio ---
                    if (source != null)
                    {
                        // --- Sometimes the source is not attached to the weapon for easy instantiation on quick firing weapons like machineguns, 
                        // so that each shot gets its own audio source, but sometimes it's fine to use just 1 source. We don't want to instantiate 
                        // the parent gameobject or the program will get stuck in a loop, so we check to see if the source is a child object ---
                        if (source.transform.IsChildOf(transform))
                        {
                            source.Play();
                        }
                        else
                        {
                            // --- Instantiate prefab for audio, delete after a few seconds ---
                            AudioSource newAS = Instantiate(source);
                            if ((newAS = Instantiate(source)) != null && newAS.outputAudioMixerGroup != null && newAS.outputAudioMixerGroup.audioMixer != null)
                            {
                                // --- Change pitch to give variation to repeated shots ---
                                newAS.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", UnityEngine.Random.Range(audioPitch.x, audioPitch.y));
                                newAS.pitch = UnityEngine.Random.Range(audioPitch.x, audioPitch.y);

                                // --- Play the gunshot sound ---
                                newAS.PlayOneShot(GunShotClip);

                                // --- Remove after a few seconds. Test script only. When using in project I recommend using an object pool ---
                                Destroy(newAS.gameObject, 4);
                            }
                        }
                    }
                }

                timeBeforeFire = DefaultTimeBeforeFire;
            }
        }
        else
        {
            // If we have no valid tracked joint, reset to local zero.
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        //UIloaderContainer.text = bulletLoader.ToString();
    }

    bool checkHandInput()
    {
        handIsValid = handsAggregator.TryGetPalmFacingAway(UnityEngine.XR.XRNode.RightHand, out bool isPalmFacingAway);
        return handIsValid;
    }
}