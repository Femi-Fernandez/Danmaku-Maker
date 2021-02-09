using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsCommands : MonoBehaviour
{
    public void bossDefeated()
    {
        Analytics.CustomEvent("Player Won");
    }

    public void playerDefeated()
    {
        Analytics.CustomEvent("Player Lost");
    }


    public void saveTurretPressed()
    {
        Analytics.CustomEvent("Save Turret Pressed");
    }
    public void saveBulletPressed()
    {
        Analytics.CustomEvent("Save Bullet Pressed");
    }
    public void saveDurationPressed()
    {
        Analytics.CustomEvent("Save Subwave Duration Pressed");
    }

    public void testBossPressed()
    {
        Analytics.CustomEvent("Test boss Pressed");
    }
}
