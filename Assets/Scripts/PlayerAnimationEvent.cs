using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public void OnLandingEnd()
    {
        Debug.Log("Animan Event");
        MessageBroker.Instance.Publish(MessageEventName.ON_LANDING_END, null);
    }

    public void OnJumpAnimEnd()
    {
        MessageBroker.Instance.Publish(MessageEventName.ON_JUMP_ANIM_END, null);
    }
}
