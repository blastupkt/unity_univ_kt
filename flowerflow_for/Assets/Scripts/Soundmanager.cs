using UnityEngine;
using System.Collections;


public class Soundmanager : MonoBehaviour {
      public GameObject music;
 //   public static Soundmanager instance;
        void Awake()
        {
        //When the scene loads it checks if there is an object called "MUSIC".
        music = GameObject.Find("BGMusic");
        if (music == null)
        {
            //If this object does not exist then it does the following:
            //1. Sets the object this script is attached to as the music player
            music = this.gameObject;
            //2. Renames THIS object to "MUSIC" for next time
            music.name = "MUSIC";
            //3. Tells THIS object not to die when changing scenes.
            DontDestroyOnLoad(music);
        }
        else {
            //If there WAS an object in the scene called "MUSIC" (because we have come back to
            //the scene where the music was started) then it just tells this object to
            //destroy itself
            if (this.gameObject.name != "MUSIC")
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void Update()
    {
        
      
    }


}

