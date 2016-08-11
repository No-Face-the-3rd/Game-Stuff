using UnityEngine;
using System.Collections;

public class TPS_Camera : TPS_BaseCamera
{

    // Awake is called before start and if the object exists
    protected override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    // Fixed update is called a certain amount of timed per second.
    // It's better for physics calculations.
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // Late update happens after Update and FixedUpdate.
    // Camera logic usually goes here.
    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    protected override void RotatePivot()
    {
        base.RotatePivot();
    }
}
