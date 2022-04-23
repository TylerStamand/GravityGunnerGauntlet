using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;
    void Awake() {
        animator = GetComponent<Animator>();
    }

    public void Activate() {
        animator.SetBool("OpenDoor", true);
    }
}
