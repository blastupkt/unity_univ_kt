using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
//using UnityEngine.EventSystems;
//using UnityEngine.Serialization;
//레거시 애니 다루는 법 - 나름 귀중한 자료다

public class Maincontroller : MonoBehaviour/*,IPointerClickHandler*/ {
   
    public Button Main_Start_B;
    public Button Main_About_B;
    public Button Exit_B;
    public Button About_Start_B;
    public Button About_Back_B;
    public GameObject About;

	public class AnimatedGraphic {

		public Animation _anim;
		public Graphic[] graphics;

		public AnimatedGraphic(Animation _anim, Graphic[] graphics) {
			this._anim = _anim;
			this.graphics = graphics;
		}

	}
   
    Animation _anim;
   

   
	void Start () {
     _anim = GetComponent<Animation>();
        _anim.Play("MainUI");

    }

	public void Main_start()
    {
        _anim.Play("Start");
        _anim.CrossFade("Starting", 1f);
        StartCoroutine(SceneShift());
    }
    public IEnumerator SceneShift()
    {
        yield return new WaitForSeconds(_anim["Starting"].length);
        SceneManager.LoadScene("PlayScene");
    }
  
    public void About_start()
    {
        _anim.Play("AboutStart");
        _anim.CrossFade("Starting", 0.5f);
        StartCoroutine(SceneShift());
        
    }
    public void exit()
    {
        Application.Quit();
    }
    public void About_back()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Main_about()
    {
        About.SetActive(true);
        //_anim.Play("Start");
        _anim.Play("about");
        
        //GetComponent<Animation>().Play("UIStart");
       //    GetComponent<Animation>().CrossFade("UIAbout");
      
       
    }


	// list of relationships
	private List<AnimatedGraphic> m_AnimatedGraphics = new List<AnimatedGraphic>();

	void Awake() {
		// find all legacy animated UI elements
		Animation[] animations = GetComponentsInChildren<Animation>(true);
		foreach (Animation anim in animations) {
			Graphic[] graphics = anim.GetComponentsInChildren<Graphic>(true);

			if (graphics.Length > 0) {
				m_AnimatedGraphics.Add(new AnimatedGraphic(anim, graphics));
			}
		}

		if (m_AnimatedGraphics.Count == 0) {
			Debug.LogWarning("LegacyAnimationUI couldn't find any Legacy Animated UI components in hierarchy object", gameObject);
			enabled = false;
			return;
		}
	}

	void Update() {
		// set all dirty flags on any animating graphics
		foreach (AnimatedGraphic animatedGraphic in m_AnimatedGraphics) {
			if (animatedGraphic._anim.isPlaying) {
				foreach (Graphic graphic in animatedGraphic.graphics) {
					graphic.SetAllDirty();

					// NOTE - One of these may be all that's needed to animate color/alpha, but I made this script as simple and general-purpose as possible
					//graphic.SetMaterialDirty();
					//graphic.SetVerticesDirty();
				}
			}
		}
	}
}
