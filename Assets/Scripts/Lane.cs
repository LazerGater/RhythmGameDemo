using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Melanchall.DryWetMidi.Interaction;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class Lane : MonoBehaviour {

    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    public GameObject notePrefab;
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();

    int spawnIndex = 0;
    int inputIndex = 0;

    // filter out what notes you dont need
    public void SetTimeStamps (Melanchall.DryWetMidi.Interaction.Note[] array) {
        foreach (var note in array) {
            if (note.NoteName == noteRestriction) {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan> (note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
    } // end SetTimeStamps

    // Update is called once per frame
    void Update() {
        // spawning notes
        if (spawnIndex < timeStamps.Count) {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime) {
                var note = Instantiate(notePrefab, transform);
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++;
            }
        }


        // inputs
        if (inputIndex < timeStamps.Count) {
        
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);
                
            // check if player hit/miss note
            if (timeStamp + marginOfError <= audioTime) {
                Miss();
                inputIndex++;
            }

            else if (Input.GetKeyDown(input)) {
                if (Math.Abs(audioTime - timeStamp) < marginOfError) { // player hit note
                    Hit( Math.Abs(audioTime - timeStamp), notes[inputIndex].transform.position );
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                } else { // player missed note
                }
            }
        }
        
    } // end Update

    private void Hit(double scoreChecker, UnityEngine.Vector3 location) { 
        // gets passed Math.Abs(audioTime - timeStamp) to decide how much score you should get
        // takes how many seconds the note is past or before the hit point, converts it to a number between 0 - 100, and subtracts it from 100 to get score per hit
        int hitScore = 100 - ( (int)(scoreChecker*100) * 2 );
        if (hitScore < 70) hitScore = hitScore/2; // punish bad hits more
        ScoreManager.Hit(hitScore);
        // display hitScore to user
        HitScore.Create(location, hitScore);
    }
    
    private void Miss() {
        ScoreManager.Miss();
    }
} // end Lane
