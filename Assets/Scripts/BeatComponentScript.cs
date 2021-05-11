using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BeatComponentScript : MonoBehaviour
{
    private GameObject m_beatController;
    [SerializeField] private GameObject pointLight;
    [SerializeField] private bool defaultOn;
    [SerializeField] private string type;
    private Light2D light2D;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSourceDrums;
    private void Awake()
    {
        m_beatController = GameObject.FindGameObjectWithTag("beatSpawner");
        audioSourceDrums = m_beatController.GetComponent<AudioSource>();

        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        m_beatController.GetComponent<PlayBeatOnTime>().changeState += BeatComponentScript_changeState;
        m_beatController.GetComponent<PlayBeatOnTime>().resetState += BeatComponentScript_resetState;

        light2D = pointLight.GetComponent<Light2D>();
    }

    private void BeatComponentScript_resetState(object sender, System.EventArgs e)
    {
        if (defaultOn)
            TurnOn();
        else
            TurnOff();
    }

    private void BeatComponentScript_changeState(object sender, PlayBeatOnTime.OnInput e)
    {
        SwitchState(e.beat);
    }

    private void PlayGenerate(ScriptableBeat beat)
    {
        audioSourceDrums.clip = beat.audioGenerate;
        audioSourceDrums.Play();
    }
    private void PlayDissolve(ScriptableBeat beat)
    {
        audioSourceDrums.clip = beat.audioDissolve;
        audioSourceDrums.Play();
    }
    private void SwitchState(ScriptableBeat beat)
    {
        if (tag.ToUpper() == beat.color.ToString().ToUpper())
        {
            if (light2D.intensity < 1f) light2D.intensity = 1f;
            else light2D.intensity = 0.5f;
            boxCollider.enabled = !boxCollider.enabled;

            if (boxCollider.enabled)
            {
                PlayGenerate(beat);
                spriteRenderer.color += new Color(0, 0, 0, .5f);
            }
            else
            {
                PlayDissolve(beat);
                spriteRenderer.color -= new Color(0, 0, 0, .5f);
            }
        }

    }
    private void TurnOff()
    {
        light2D.intensity = 0.5f;
        boxCollider.enabled = false;
        if (!(spriteRenderer.color.a < 0.9f))
            spriteRenderer.color -= new Color(0, 0, 0, .5f);
    }
    private void TurnOn()
    {
        light2D.intensity = 1f;
        boxCollider.enabled = true;
        if (spriteRenderer.color.a < 0.9f)
            spriteRenderer.color += new Color(0, 0, 0, .5f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (type.Equals("spikes"))
        {
            m_beatController.GetComponent<PlayBeatOnTime>().ResetLevel();
            collision.collider.transform.position = collision.collider.GetComponent<PlayerContoller>().startingPosition.position;
            FindObjectOfType<UIAudioScript>().PlayPlayerHurt();
        }
    }

}



