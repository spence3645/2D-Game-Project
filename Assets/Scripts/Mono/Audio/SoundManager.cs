using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioClip shooting;
    public AudioClip enemy_damaged;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string action)
    {
        switch (action)
        {
            case "Shooting":
                source.PlayOneShot(shooting);
                break;
            case "Enemy Shot":
                source.PlayOneShot(enemy_damaged);
                break;
        }
    }
}
