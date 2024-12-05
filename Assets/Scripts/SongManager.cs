using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;

public class SongManager : MonoBehaviour {
    public static SongManager Instance;
    public AudioSource audioSource;

    public Lane[] lanes;

    public float songDelayinSeconds;
    public double marginOfError; // in seconds

    public int inputDelayInMilliseconds;

    public string fileLocation;
    
    public float noteTime;
    public float noteTapY;
    public float noteSpawnY;
    public float noteDespawnY {
        get { return noteTapY - (noteSpawnY - noteTapY); }
    }

    // where midi file is loaded and parsed
    public static MidiFile midiFile;

    // Start is called before the first frame update
    void Start() {
        Instance = this;
        ReadFromFile();

    } // end Start

    // reads from a file stored in application
    private void ReadFromFile() {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    } // end ReadFromFile

    private void GetDataFromMidi(){
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);

        foreach (var lane in lanes) lane.SetTimeStamps(array);

        Invoke(nameof(StartSong), songDelayinSeconds);
    } // end GetDataFromMidi

    public void StartSong() {
        audioSource.Play();
    }

    public static double GetAudioSourceTime() {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }
    
} // end song manager
