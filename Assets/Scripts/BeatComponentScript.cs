using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BeatComponentScript : MonoBehaviour
{
    private GameObject m_beatController;
    [SerializeField] private GameObject pointLight;
    private Light2D light2D;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private void Awake()
    {
        m_beatController = GameObject.FindGameObjectWithTag("beatSpawner");
        audioSource = m_beatController.GetComponent<AudioSource>();

        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        m_beatController.GetComponent<PlayBeatOnTime>().changeState += BeatComponentScript_changeState;
        m_beatController.GetComponent<PlayBeatOnTime>().resetState += BeatComponentScript_resetState;

        light2D = pointLight.GetComponent<Light2D>();
    }

    private void BeatComponentScript_resetState(object sender, System.EventArgs e)
    {
        TurnOff();
    }

    private void BeatComponentScript_changeState(object sender, PlayBeatOnTime.OnInput e)
    {

        SwitchState(e.beat);
    }

    private void PlayGenerate(ScriptableBeat beat)
    {
        audioSource.clip = beat.audioGenerate;
        audioSource.Play();
    }
    private void PlayDissolve(ScriptableBeat beat)
    {
        audioSource.clip = beat.audioDissolve;
        audioSource.Play();
    }
    private void SwitchState(ScriptableBeat beat)
    {
            if (tag.ToUpper() == beat.color.ToString().ToUpper())
            {
            if (light2D.intensity<1f)light2D.intensity =1f;
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
                if (boxCollider.enabled)
                {
                     boxCollider.enabled = false;
                }

                if (!(spriteRenderer.color.a < 0.9f))
                {
                    spriteRenderer.color -= new Color(0, 0, 0, .5f);
                }
            }
        

}



