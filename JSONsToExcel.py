import os 
import json
import pandas as pd

def process_json_file(json_file):
    with open(json_file, 'r', encoding='utf-8') as f:
        data = json.load(f)
    
    all_data = []
    for user_data in data["userData"]:
        player_data = user_data.get("PlayerData", {})
        metrics_data = user_data.get("MetricsData", {})
        moral_dilemma_data = metrics_data.get("MoralDilemmaData", [])
        personality_data = user_data.get("PersonalityData", {})
        username = player_data.get("Username", "")
        morality_value_normalized = personality_data.get("Traits", [{}])[0].get("ValueNormalized", "")
        morality_value = personality_data.get("Traits", [{}])[0].get("Value", "")
        start_time = metrics_data.get("GameTime", {}).get("TimeStarted","")
        end_time = metrics_data.get("GameTime", {}).get("TimeEnded","")
        
        user_dict = {"Username": username, "MoralityValue": morality_value, "MoralityNormalized": morality_value_normalized, "StartTime": start_time, "EndTime": end_time}

        for dilemma in moral_dilemma_data:
            scene_name = dilemma.get("SceneName", "")
            if scene_name not in ["MainMenu", "EndScene", "EntryScene"]:
                user_dict[f"Completed_{scene_name}"] = dilemma.get("Completed", "")
                user_dict[f"ChosenAnswerIndex_{scene_name}"] = dilemma.get("MoralDilemmaChosenOption", "")
                user_dict[f"ChosenAnswerDescription_{scene_name}"] = dilemma.get("MoralDilemmaChosenOptionDescription", "")
                user_dict[f"DecisionValue_{scene_name}"] = dilemma.get("DecisionValue", "")
                user_dict[f"CoinsCollected_{scene_name}"] = dilemma.get("CoinsCollected", "")
                user_dict[f"StartTime_{scene_name}"] = dilemma["Timestamps"].get("StartTime", "")
                user_dict[f"EndTime_{scene_name}"] = dilemma["Timestamps"].get("EndTime", "")
                user_dict[f"TimeTaken_{scene_name}"] = dilemma["Timestamps"].get("TimeTaken", "")

        all_data.append(user_dict)


    return all_data

def json_to_excel(json_file):
    try:
        all_data = process_json_file(json_file)
        if all_data:
            output_excel = json_file.replace('.json', '.xlsx')
            if not os.path.isfile(output_excel):
                # If the file doesn't exist, create it with the first set of data
                df = pd.DataFrame(all_data)
                df.to_excel(output_excel, index=False, sheet_name='Sheet1')
                print(f"Created {output_excel}")
            else:
                # If the file exists, append the data to a new sheet
                with pd.ExcelWriter(output_excel, engine='openpyxl', mode='a') as writer:
                    df = pd.DataFrame(all_data)
                    df.to_excel(writer, index=False, sheet_name=f'Sheet{len(writer.book.worksheets) + 1}')
                print(f"Appended to {output_excel}")
        else:
            print("No user data found in the JSON file.")
    except Exception as e:
        print(f"Error processing JSON file: {e}")

if __name__ == "__main__":
    json_file = r"C:\Users\Raul\AppData\LocalLow\RaulCoelho\CoinCatcher\userfiles.json"
    json_to_excel(json_file)
