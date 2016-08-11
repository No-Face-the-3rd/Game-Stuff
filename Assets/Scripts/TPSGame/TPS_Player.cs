using UnityEngine;

public class TPS_Player : TPS_BasePlayer
{

    // Awake is called before start and if the object exists
    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called when the object is first spawned
    protected override void Start()
    {
        base.Awake();
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

    protected override void GetInput()
    {
        base.GetInput();
    }

    protected override void ApplyMovement()
    {
        base.ApplyMovement();
    }

    protected override void ApplyGravity()
    {
        base.ApplyGravity();
    }
}
