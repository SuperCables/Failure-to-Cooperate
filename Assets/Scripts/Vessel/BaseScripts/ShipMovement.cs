using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody))]
public class ShipMovement : NetworkBehaviour
{
    //Moving config

	[Header("Movement")]
	public float maxSpin = 30; //TODO: calculate these from engine
	public float spinAccel =  20;
	public float MaxRCSSpeed = 5; //max rcs speed, also max speed where rcs works; therfore Only used if speed < MaxRCSSpeed


    [Header("Input")]
    [SyncVar]
    public float inputThrottle;
    [SyncVar]
    public float inputAngle;
    [SyncVar]
    public float inputDive;
    [SyncVar]
    public Vector3 inputRCS;
    [Space(10)]
    public float perfectMaxSpeed;

    [HideInInspector]
    [SyncVar]
    public float speed;
    [HideInInspector]
    public Vector3 direction;
    [HideInInspector]
    [SyncVar]
    public float angle;
    [HideInInspector]
    public float maxSpeed;
    [HideInInspector]
    [SyncVar]
    float currentSpin;

    //engine stats
    float engineThrust = 0;
    float maxEngineThrust = 0;
    float engineWattage = 0;

    [HideInInspector]
    public Rigidbody body;
    [HideInInspector]
    public Vessel vessel;
    [HideInInspector]
    public EngineMananger engineManager;

    void Start () {
        GetComponentInParent<Entity>().FullRebuild += Rebuild;
        Rebuild();
        //inputAngle = Random.Range(0, 360);
    }

    void Rebuild()
    {
        body = GetComponent<Rigidbody>();
        vessel = GetComponent<Vessel>();
        engineManager = GetComponentInChildren<EngineMananger>();
    }
	
	void Update () {

	}

	void FixedUpdate() {

		UpdateMovement ();
    }

    void UpdateMovement()
    {

        if (engineManager != null) {

            engineThrust = engineManager.thrust;
            maxEngineThrust = engineManager.maxThrust;
            engineWattage = engineManager.maxWattage;

            angle = transform.rotation.eulerAngles.y % 360;
            direction = Game.DegreeToVector2(angle);
            UpdateImpulse();
            UpdateTurn();
        }
    }

    void UpdateImpulse() {
        float impulseAccel = engineThrust / 5; //FIXME change to engine.acceleration or somthing;
        maxSpeed = engineThrust; //  thrust / mass or somthing
        perfectMaxSpeed = maxEngineThrust; //max speed assuming no damage
        if (isServer) { //if we arn't the server we feed off the sync var
            speed = body.velocity.magnitude;
        }

		Vector3 velocity = (Quaternion.Euler (0, -angle, 0) * body.velocity); //ship relitive speed. (right, up, forward)
		Vector3 targetRCS = Mathf.Max(MaxRCSSpeed - speed, 0) * inputRCS; //add any strafing
		Vector3 targetVelocity = (Quaternion.Euler(inputDive, 0, 0) * Vector3.forward * inputThrottle * perfectMaxSpeed) + targetRCS; //what velocity does the ship want to go?
		Vector3 diffVelocity = targetVelocity - velocity; //how off we are
		float frameMaxAccel = impulseAccel * Time.fixedDeltaTime; //delta for this frame
        //how much acceleration for this frame?
        float frameImpulse = Mathf.Min (diffVelocity.magnitude, frameMaxAccel, engineManager.energy / (engineWattage * Time.fixedDeltaTime));
		frameImpulse = Mathf.Max(frameImpulse, 0); //dont go backwards!

        if (frameMaxAccel > 0) //if we can't move, bail out so we don't devide by 0
        {
            Vector3 frameThrust = diffVelocity.normalized * frameImpulse; //force to correct for this frame;
            velocity += frameThrust;
            engineManager.energy -= (frameImpulse / frameMaxAccel) * engineWattage * Time.fixedDeltaTime; //consume power
        }
        body.velocity = (Quaternion.Euler(0, angle, 0) * velocity); //set velocity to new value
    }

    void UpdateTurn() {
        float frameSpinAccelScaled = spinAccel * Time.fixedDeltaTime;
        if (isServer) { currentSpin = body.angularVelocity.y * Mathf.Rad2Deg; }
        float diff = Mathf.DeltaAngle(inputAngle, angle);
        float breakDelta = (currentSpin * currentSpin / (2 * spinAccel)) * Mathf.Sign(currentSpin); //if we break now, how much farther will we go?

        float steer = -diff - breakDelta; //what way should we steer
        if (Mathf.Abs(steer) > 180) { steer *= -1; } //wrap around circle correctly

        if (Mathf.Abs(breakDelta) > 180) //if spining out
        {
            steer = -Mathf.Sign(currentSpin); //stop spinning!
        }

        float targetSpin = steer * maxSpin; //and apply it

        float deltaSpin = Mathf.Clamp((targetSpin - currentSpin), -frameSpinAccelScaled, frameSpinAccelScaled); //calculate rotational acceleration
        body.angularVelocity = ((currentSpin + deltaSpin) * Mathf.Deg2Rad) * Vector3.up;
    }

	void OnDrawGizmos() {
		if (Application.isPlaying) {
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine (transform.position, transform.position + body.velocity);
		}
	}

}
