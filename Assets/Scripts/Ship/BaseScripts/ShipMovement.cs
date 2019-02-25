using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody))]
public class ShipMovement : NetworkBehaviour
{
    //Moving config

	[Header("Movement")]
	public float maxSpin = 30;
	public float spinAccel =  20;
	public float MaxRCSSpeed = 2;

    
    [Header("Input")]
    [SyncVar]
    public float inputThrottle;
    [SyncVar]
    public float inputAngle;
    [SyncVar]
    public float targetDepth;
    [SyncVar]
    public Vector2 rcsInput;
    
    
    [Header("Values")]
    public float perfectMaxSpeed;
    public float perfectMaxDive;

    [HideInInspector]
    [SyncVar]
    public float speed;
    [HideInInspector]
    public Vector2 direction;
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

    //[HideInInspector]
    public Rigidbody body;
    //[HideInInspector]
    public Vessel vessel;
    //[HideInInspector]
    public EngineMananger engineManager;
    //[HideInInspector]
    public BallastPumpsMananger ballastPumpsMananger;

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
        ballastPumpsMananger = GetComponentInChildren<BallastPumpsMananger>();
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

    void UpdateDive()
    {
        if (ballastPumpsMananger != null)
        {
            

            float diveSpeed = body.velocity.y;
            diveSpeed -= diveSpeed * 0.1f;
            diveSpeed += 1 / 10;

            //thisDiveVelocity = Vector3.up * diveSpeed; //set velocity to new value

        }
    }

    void UpdateImpulse() {
        float impulseAccel = engineThrust / 5; //engine.acceleration;
        maxSpeed = engineThrust; //  thrust / mass or somthing
        perfectMaxSpeed = maxEngineThrust;
        if (isServer) { //if we arn't the server we feed of the sync var
            speed = body.velocity.magnitude;
        }

		Vector2 velocity = (Vector2)(Quaternion.Euler (0, 0, angle) * Game.V3toV2(body.velocity)); //ship relitive speed. (right,up)
		Vector2 targetRCS = Mathf.Max(MaxRCSSpeed - speed, 0) * rcsInput;
		Vector2 targetVelocity = (Vector2.up * inputThrottle * perfectMaxSpeed) + targetRCS; //what velocity does the ship want to go?
		Vector2 diffVelocity = targetVelocity - velocity; //how off we are
		float frameMaxAccel = impulseAccel * Time.fixedDeltaTime; //delta for this frame
        //how much acceleration for this frame?
        float frameImpulse = Mathf.Min (diffVelocity.magnitude, frameMaxAccel, engineManager.energy / (engineWattage * Time.fixedDeltaTime));
		frameImpulse = Mathf.Max(frameImpulse, 0); //dont go backwards!

        if (frameMaxAccel > 0) //if we can't move, dont devide by 0
        {
            Vector2 frameThrust = diffVelocity.normalized * frameImpulse; //force to correct for this frame;
            velocity += frameThrust;
            engineManager.energy -= (frameImpulse / frameMaxAccel) * engineWattage * Time.fixedDeltaTime; //consume power
        }
        body.velocity = (Quaternion.Euler(0, angle, 0) * Game.V2toV3(velocity)); //set velocity to new value
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
