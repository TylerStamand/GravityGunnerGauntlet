using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    Animator animator;
    void Awake() {
        animator = GetComponent<Animator>();
    }

    public void Activate() {
        animator.SetBool("OpenDoor", true);
    }

    public void Deactivate()
    {
        animator.SetBool("OpenDoor", false);
    }
}
