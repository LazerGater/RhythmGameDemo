using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager Instance;
    public AudioSource audioSource;
    public AudioSource hitSFX;
    public AudioSource missSFX;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI comboText;
    public TMPro.TextMeshProUGUI multiplierText;

    // variables to calculate score
    static int combo;
    static int totalScore;
    static int scoreMult;
    
    // Start is called before the first frame update
    void Start() {
        Instance = this;
        combo = 0;
        scoreMult = 1;
    }

    public static void Hit(int hitScore) {
        // update combo with each hit and set score multiplier 
        combo += 1;
        if (combo >= 200) scoreMult = 20;
        else if (combo >= 100) scoreMult = 10;
        else if (combo >= 50) scoreMult = 5;
        else if (combo >= 30) scoreMult = 3;
        else if (combo >= 20) scoreMult = 2;
        // add score gained from hit
        totalScore += hitScore * scoreMult;
        Instance.hitSFX.Play();
    }
    public static void Miss() {
        combo = 0;
        scoreMult = 1;
        Instance.missSFX.Play();
    }

    // Update is called once per frame
    void Update() {
        scoreText.text = totalScore.ToString();
        comboText.text = combo.ToString();
        multiplierText.text = scoreMult.ToString() + "x";
    }
}
