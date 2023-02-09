using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public float speed = 1f;
    public float scale = 1f;

    public static WaveManager instance;
    private float offset;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        offset = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed;
    }

    public float GetWaveLength(float x, float y)
    {
        return Mathf.PerlinNoise(offset + x / scale, offset + y / scale);
    }
}
