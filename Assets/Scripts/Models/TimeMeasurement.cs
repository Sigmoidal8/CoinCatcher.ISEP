using UnityEngine;

[System.Serializable]
public class TimeMeasurement
{
    public float startTime;
    public float endTime;
    public float timeTaken;

    public void StartTimer()
    {
        // Record the start time when the action begins
        startTime = Time.time;
    }

    public void StopTimer()
    {
        // Record the end time when the action completes
        endTime = Time.time;
        
        // Calculate the time taken by subtracting the start time from the end time
        timeTaken = endTime - startTime;
    }
}
