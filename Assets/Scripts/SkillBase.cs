using UnityEngine;
using UnityEngine.Playables;

public abstract class SkillBase : ScriptableObject
{
    public string skillName;
    public Sprite icon;
    public PlayableDirector skillCuiscene;

    public abstract float cooldown{ get; }

    public abstract void Activate(GameObject user);
    
}
