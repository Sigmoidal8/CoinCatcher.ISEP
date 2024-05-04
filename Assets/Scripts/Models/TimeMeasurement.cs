using System;
using UnityEngine;

/// <summary>
/// Represents the measurement of time for an action.
/// </summary>
[System.Serializable]
public class TimeMeasurement
{
    // The time when the action starts.
    public float StartTime;
    // The time when the action ends.
    public float EndTime;
    // The duration of time taken to complete the action.
    public float TimeTaken;
    // Real time the action started
    #nullable enable
    public string? TimeStarted;
    // Real time the action ended
    public string? TimeEnded;
    #nullable disable

    public void StartTimer()
    {
        // Record the start time when the action begins
        StartTime = Time.time;
    }

    public void StopTimer()
    {
        // Record the end time when the action completes
        EndTime = Time.time;

        // Calculate the time taken by subtracting the start time from the end time
        TimeTaken = EndTime - StartTime;
    }

    public void MeasureInitialTime(){
        this.TimeStarted = DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss");
    }

    public void MeasureFinalTime(){
        this.TimeEnded = DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss");
    }
}
