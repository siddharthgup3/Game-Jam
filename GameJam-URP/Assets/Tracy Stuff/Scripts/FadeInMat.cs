using UnityEngine;

public class FadeInMat : MonoBehaviour
{
    // Blends between two materials
    public bool fading;
    Material material1;
    public Material material2;
    float duration = 2.0f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

        // At start, use the first material
        material1 = rend.material;
    }

    void Update()
    {
        // ping-pong between the materials over the duration
            float lerp = Mathf.PerlinNoise(Time.time, duration) / duration;
            rend.material.Lerp(material1, material2, lerp);
    }
}
