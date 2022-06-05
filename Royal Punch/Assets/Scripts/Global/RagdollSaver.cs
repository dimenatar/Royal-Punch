using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Ragdoll Saver", fileName = "New Ragdoll Saver", order = 43)]
public class RagdollSaver : ScriptableObject
{
    public List<Bone> _bones = new List<Bone>();

    public List<Bone> Bones => _bones;

    public void Rewrite()
    {
        _bones = new List<Bone>();
    }

    public void WriteValue(Bone bone)
    {
        _bones.Add(bone);
    }
}
