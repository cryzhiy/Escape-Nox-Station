using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCableFall : MonoBehaviour
{
    Transform player;
    Animator anim;
    Transform light;    Light pointLight;
    Transform ps;
    float timer = 0;
    AudioSource audioSource;

    [SerializeField]
    AudioClip audioExplosion;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        ps = transform.Find("ParticleSystem");
        light = transform.Find("PointLight");
        pointLight = light.GetComponent<Light>();

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (timer == 0)
        {
            if (player && anim.enabled == false && Vector3.Distance(player.position, transform.position) < 6)
            {
                anim.enabled = true;
                ps.gameObject.SetActive(true);
                light.gameObject.SetActive(true);
                timer += Time.deltaTime;
                audioSource.clip = audioExplosion;
                audioSource.loop = false;
                audioSource.Play();
                
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > 0.8f)
            {
                light.gameObject.SetActive(false);                this.enabled = false;
            }            else
            {
                pointLight.intensity = Random.Range(0.2f, 0.7f);
            }
        }
    }
}
