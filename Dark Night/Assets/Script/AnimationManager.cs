﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator anim;
    public Objects Door;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void openDoor () {
        anim.Play(Door.doorOpenParameter);
    }

    public void closeDoor() {
        anim.Play(Door.doorCloseParameter);
    }

    public void OpenPuzzleDoor() {
        anim.Play(Door.openPuzzleDoorParameter);
    }

    public void ClosePuzzleDoor() {
        anim.Play(Door.closePuzzleDoorParameter);
    }
}