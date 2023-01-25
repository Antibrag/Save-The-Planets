using Godot;
using Godot.Collections;
using System;

public class Data
{
    public static int money = 0;
    public static int wawe = 1;
    public static string shieldSkin = "default";

    public void LoadData()
    {
        File file = new File();
        file.Open("res://data_saves.json", File.ModeFlags.Read);
        string text = file.GetAsText();
        file.Close();
        JSONParseResult result = JSON.Parse(text);
        Dictionary jsonResult = result.Result as Dictionary;

        money = Convert.ToInt32(jsonResult["money"]);
        wawe = Convert.ToInt32(jsonResult["wawes"]);
        shieldSkin = jsonResult["skin"] as string;
    }

    public void SaveData()
    {
        File file = new File();
        file.Open("res://data_saves.json", File.ModeFlags.Write);
        Dictionary textToJson = new Dictionary();
        textToJson.Add("money", money);
        textToJson.Add("wawes", wawe);
        textToJson.Add("skin", shieldSkin);
        file.StoreString(JSON.Print(textToJson));
        file.Close();
    }
}
