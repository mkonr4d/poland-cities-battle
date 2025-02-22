﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeActor : MonoBehaviour
{
    public WeaponController controller;
    public DamageDealer dealer;

    Action callback;
    public Animation animator;
    bool isReversed;

    private void Awake()
    {
        controller.OnUseTriggered += HandleUse;
        dealer.OnHit += HandleHit;
    }

    private void HandleHit()
    {
        animator[animator.clip.name].speed = -1;
        isReversed = true;
    }

    private void HandleUse(Action callback)
    {
        dealer.isDealing = true;
        this.callback = callback;
        animator.Play();
    }

    public void AnimationStart()
    {
        if (!isReversed)
            return;
        isReversed = false;
        animator[animator.clip.name].speed = 1;
        animator.Stop();
        FinishMove();
    }

    public void FinishMove()
    {
        callback?.Invoke();
        callback = null;
        dealer.isDealing = false;
    }
}
