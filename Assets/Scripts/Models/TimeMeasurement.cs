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
}
