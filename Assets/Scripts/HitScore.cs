using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HitScore : MonoBehaviour {

    // create a hit score popup
    public static HitScore Create(Vector3 position, int hitScoreAmount) {
        Transform hitScorePopupTransform = Instantiate(GameAssets.Instance.HitScore, position, Quaternion.identity);

        HitScore hitScore = hitScorePopupTransform.GetComponent<HitScore>();
        hitScore.Setup(hitScoreAmount);

        return hitScore;
    }
    
    private TextMeshPro textMesh;
    private float disappearTimer;
    private float disappearTimerMax = 1f;
    private Color textColor;

    public void Awake() {
        textMesh = transform.GetComponent<TextMeshPro>();
    } // end Awake

    public void Setup(int hitScoreAmount) {
        textMesh.SetText(hitScoreAmount.ToString()); 
        if (hitScoreAmount < 50) {          // bad hit
            textMesh.fontSize = 4;
            textColor = Color.red;
            disappearTimer = 0.4f;
        } else if (hitScoreAmount < 90) {  // normal hit
            textMesh.fontSize = 4;
            textColor = Color.yellow;
            disappearTimer = 0.6f;
        } else if (hitScoreAmount < 100) {  // good hit hit
            textMesh.fontSize = 4;
            textColor = Color.green;
            disappearTimer = 0.8f;
        } else {                            // perfect hit
            textMesh.fontSize = 5;
            textColor = Color.cyan;
            disappearTimer = 0.8f;
        }
        textMesh.color = textColor;
        disappearTimer = 0.5f;
    } // end Setup

    private void Update() {
        float moveYSpeed = 0.5f;
        transform.position += new UnityEngine.Vector3(0, moveYSpeed) * Time.deltaTime;

        if (disappearTimer > disappearTimerMax * 0.5) { // first half of timer
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        } else { // second hald of timer
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0) {
            // start disappearing
            float disappearSpeed = 5f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a< 0) {
                Destroy(gameObject);
            }
        }

    } // end Update

} // end HitScore
