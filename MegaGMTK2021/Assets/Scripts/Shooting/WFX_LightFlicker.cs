using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class WFX_LightFlicker : MonoBehaviour
{
    public float time = 0.05f;

    private float timer;

    private Light lightSame;

    void Awake()
    {
        lightSame = GetComponent<Light>();
        timer = time;
    }

    public void Play()
    {
        StartCoroutine("Flicker");
    }

    IEnumerator Flicker()
    {
        lightSame.enabled = !lightSame.enabled;

        do
        {
            timer -= Time.deltaTime;
            yield return null;
        } while (timer > 0);

        timer = time;

        lightSame.enabled = !lightSame.enabled;
    }
}