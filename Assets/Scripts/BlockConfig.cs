using UnityEngine;

[CreateAssetMenu(fileName = "Block Config", menuName = "Blocks/Basic Block")]
public class BlockConfig : ScriptableObject
{
    public int Health;
    public int Points;
    public Material Material;
    public AudioClip HitClip;
    public AudioClip DestroyClip;
    public ParticleSystem DestroyParticle;
}