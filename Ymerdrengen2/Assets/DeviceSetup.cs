// <copyright file="DeviceSetup.cs" company="Team4">
// Creative Commons
// </copyright>
// <summary>Generic class the Grid structure.</summary>
using System.Collections;
using UnityEngine;

/// <summary>
/// Script used for device orientation and not going to sleep
/// </summary>
public class DeviceSetup : MonoBehaviour
{
    /// <summary>
    /// This happens first
    /// </summary>
    public void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
