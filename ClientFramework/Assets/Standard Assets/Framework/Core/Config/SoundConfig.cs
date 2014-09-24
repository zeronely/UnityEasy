using UnityEngine;
using System.Collections.Generic;

[CustomEditClass]
public class SoundConfig : MonoBehaviour
{
    [CustomEditField(Sections = "Music")]
    public float m_SecondsBetweenMusicTracks = 10f;
    [CustomEditField(Sections = "System Audio Sources")]
    public AudioSource m_PlayClipTemplate;
    [CustomEditField(Sections = "System Audio Sources")]
    public AudioSource m_PlaceholderSound;
}
