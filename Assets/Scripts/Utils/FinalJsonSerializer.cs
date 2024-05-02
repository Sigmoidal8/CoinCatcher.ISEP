using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

// This static class provides a method to serialize final game data into JSON format.
static class FinalJsonSerializer
{
    // Method to serialize a list of final game data into JSON format
    public static string SerializeDataList(FinalDataList dataList)
    {
        return JsonConvert.SerializeObject(dataList);
    }

    public static FinalDataList DeserializeFinalDataList(string json)
    {
        return JsonConvert.DeserializeObject<FinalDataList>(json);
    }
}
