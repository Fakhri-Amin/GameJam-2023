using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip sirene;
    [SerializeField] private AudioClip playerShoot;
    [SerializeField] private AudioClip bulletCollide;
    [SerializeField] private AudioClip playerCastSkill;
    [SerializeField] private AudioClip enemyCastSkill;

    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlayButtonClickSound()
    {
        PlaySound(buttonClick);
    }

    public void PlaySireneSound()
    {
        PlaySound(sirene);
    }

    public void PlayPlayerShootSound()
    {
        PlaySound(playerShoot);
    }

    public void PlayBulletCollideSound()
    {
        PlaySound(bulletCollide);
    }

    public void PlayPlayerCastSkillSound()
    {
        PlaySound(playerCastSkill);
    }

    public void PlayEnemyCastSkillSound()
    {
        PlaySound(enemyCastSkill);
    }
}
