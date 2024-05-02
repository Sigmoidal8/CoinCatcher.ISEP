using UnityEngine;

static class FinalJsonSerializer
{
    public static string SerializeData(FinalData data)
    {
        string playerDataJson = JsonUtility.ToJson(data.playerData);

    string metricsDataJson = "{";

    // Serialize moral dilemma data
    metricsDataJson += "\"MoralDilemmaData\":[";
    foreach (MoralDilemmaData moralDilemmaData in data.metricsData.moralDilemmaData)
    {
        string moralDilemmaDataJson = JsonUtility.ToJson(moralDilemmaData);
        metricsDataJson += moralDilemmaDataJson + ",";
    }
    metricsDataJson = metricsDataJson.TrimEnd(',') + "],";

    // Serialize gameTime
    string timeMeasurementJson = JsonUtility.ToJson(data.metricsData.gameTime);
    metricsDataJson += "\"TimeMeasurement\":" + timeMeasurementJson;

    // Close MetricsData object
    metricsDataJson += "}";

    string personalityDataJson = "[";
    foreach (Trait trait in data.personalityData.traits)
    {
        string traitJson = JsonUtility.ToJson(trait);
        personalityDataJson += traitJson + ",";
    }
    personalityDataJson = personalityDataJson.TrimEnd(',') + "]";

    return "{\"PlayerData\":" + playerDataJson +
        ",\"MetricsData\":" + metricsDataJson +
        ",\"PersonalityData\":" + personalityDataJson + "}";
    }
}
