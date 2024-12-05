using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameAssets : MonoBehaviour {
    
    private static GameAssets _Instance;

    public static GameAssets Instance {
        get {
            if (_Instance == null) _Instance = Instantiate( Resources.Load("GameAssets") as GameObject ).GetComponent<GameAssets>();
            return _Instance;
        } // end get
    } // end GameAssets


    public Transform HitScore;

} // end GameAssets
