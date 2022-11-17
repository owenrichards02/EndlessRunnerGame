using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBackg : MonoBehaviour
{
    public float sizeMultiplier;
    private float defx;
    private float defy;
    private Transform tf;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        defx = tf.localScale.x;
        defy = tf.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        tf.localScale += new Vector3((defx * sizeMultiplier) - tf.localScale.x, (defy * sizeMultiplier) - tf.localScale.y, 0) ;
    }
}
