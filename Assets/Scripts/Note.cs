using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Note : MonoBehaviour {

    double timeInstantiated;
    public float assignedTime;
    private Note note;
    private Color noteColor;


    // Start is called before the first frame update
    void Start() {
        timeInstantiated = SongManager.GetAudioSourceTime();
        noteColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update() {
        // sprite flash white when its a 100% hitscore
        if ( transform.position.y < -2.4 && transform.position.y > -3.03) {
            GetComponent<SpriteRenderer>().color = Color.white;
        } else {
            GetComponent<SpriteRenderer>().color = noteColor;
        }

        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        float t = (float) (timeSinceInstantiated / (SongManager.Instance.noteTime * 2));

        if (t > 1) {
            Destroy(gameObject);
        } else {
            transform.localPosition = Vector3.Lerp(Vector3.up * SongManager.Instance.noteSpawnY, Vector3.up * SongManager.Instance.noteDespawnY, t);
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
