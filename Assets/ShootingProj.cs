using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingProj : MonoBehaviour
{

    public float shootDelay;
    private float timeSinceLast;
    public GameObject projectile;
    private Transform dronetf;
    // Start is called before the first frame update
    void Start()
    {
        timeSinceLast = 0.0f;
        dronetf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLast += Time.deltaTime;
        if(timeSinceLast > shootDelay){
            shootProj();
            timeSinceLast = 0.0f;
        }
    }

    void shootProj(){
        var positionShot = new Vector2 (dronetf.position.x - 1.1f, dronetf.position.y - 0.4f);
        GameObject proj = Instantiate(projectile);
        proj.transform.position = positionShot;
        proj.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.0f * ((GetComponent<GroundMovement>().groundSpeed) + 3), 0);
    }
}
