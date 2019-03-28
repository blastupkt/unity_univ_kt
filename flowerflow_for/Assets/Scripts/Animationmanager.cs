using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Animationmanager : MonoBehaviour
{
    
    public class AnimatedGraphic
    {

        public Animation _anim;
        public Graphic[] graphics;

        public AnimatedGraphic(Animation _anim, Graphic[] graphics)
        {
            this._anim = _anim;
            this.graphics = graphics;
        }

    }
    void Start()
    {
       // _anim.Play("Starting");
    }

    // list of relationships
    private List<AnimatedGraphic> m_AnimatedGraphics = new List<AnimatedGraphic>();

    void Awake()
    {
        // find all legacy animated UI elements
        Animation[] _anim = GetComponentsInChildren<Animation>(true);
        foreach (Animation anim in _anim)
        {
            Graphic[] graphics = anim.GetComponentsInChildren<Graphic>(true);

            if (graphics.Length > 0)
            {
                m_AnimatedGraphics.Add(new AnimatedGraphic(anim, graphics));
            }
        }

        if (m_AnimatedGraphics.Count == 0)
        {
            Debug.LogWarning("LegacyAnimationUI couldn't find any Legacy Animated UI components in hierarchy object", gameObject);
            enabled = false;
            return;
        }
    }
    void Update()
    {
        foreach (AnimatedGraphic animatedGraphic in m_AnimatedGraphics)
        {
            if (animatedGraphic._anim.isPlaying)
            {
                foreach (Graphic graphic in animatedGraphic.graphics)
                {
                    graphic.SetAllDirty();
                }
            }
        }
    }
}
