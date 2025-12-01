
using System.Collections.Generic;
using UnityEngine;
public static class DeadEnemyData_t
{
    private static List<string> deadEnemies = new();
    public static void RegisterDeadEnemy(string enemyName)
    {
        if (!deadEnemies.Contains(enemyName))
        {
            deadEnemies.Add(enemyName);
        }
    }
    public static void SaveDeadEnemies()
    {
        string saveData = string.Join(",", deadEnemies);
        PlayerPrefs.SetString("DeadEnemies", saveData);
    }
    public static void LoadDeadEnemiesAndDestroy()
    {
        string data = PlayerPrefs.GetString("DeadEnemies", "");
        if (!string.IsNullOrEmpty(data))
        {
            deadEnemies = new List<string>(data.Split(","));
        }
    }
    public static void LoadDeadEnemiesAndDestroyNow()
    {
        string data = PlayerPrefs.GetString("DeadEnemies", "");
        if (!string.IsNullOrEmpty(data))
        {
            string[] list = data.Split(",");
            foreach (string enemy in list)
            {
                GameObject obj = GameObject.Find(enemy);
                if (obj != null)
                {
                    Object.Destroy(obj);
                    Debug.Log("€–SÏ‚İ“G " + enemy + " ‚ğíœ‚µ‚Ü‚µ‚½");
                }
            }
        }
    }
    public static void LoadDeadEnemiesAndDestro()
    {
        deadEnemies.Clear();
        LoadDeadEnemiesAndDestroyNow();
    }
}