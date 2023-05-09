
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChargementScene : MonoBehaviour
{
    public string HubSceneName;
    public string ShooterSceneName;
    public string QRSceneName;
    public string HangManSceneName;

    public void LoadHub()
    {
        SceneManager.LoadScene(HubSceneName);
    }
    public void LoadShooter()
    {
        SceneManager.LoadScene(ShooterSceneName);
    }
    public void LoadQR()
    {
        SceneManager.LoadScene(QRSceneName);
    }
    public void LoadHangman()
    {
        SceneManager.LoadScene(HangManSceneName);
    }
}
