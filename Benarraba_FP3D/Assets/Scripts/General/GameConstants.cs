using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public static class GameConstants
{
    #region Scenes
    public const int sceneMainMenu = 0;
    public const int sceneBenarraba = 1;
    #endregion
    #region Layers
    public const string layerGround = "Ground";
    #endregion
    #region MainMenu objects
    public const string mainMenuBenarrabaImageBackgroundPanel = "BenarrabaImageBackgroundPanel";
    public const string mainMenuFreezeCameraImageBackgroundPanel = "CameraFreezePanel";
    public const string mainMenuTitleAndAnyPress = "Title&KeyPress";
    public const string mainMenuVerticalButtonContainer = "VerticalButtonContainerMainMenu";
    public const string mainMenuBtnPlayYText = "BtnPlay&Text";
    public const string mainMenuBtnControlsYText = "BtnControls&Text";
    public const string mainMenuBtnExitYText = "BtnExit&Text";
    #endregion
    #region Methods
    public static bool GeneralDetection(List<Vector3> positions, Vector3 direction, float distance, List<LayerMask> masks)
    {
        foreach (LayerMask mask in masks)
        {
            foreach (Vector3 position in positions)
            {
                Debug.DrawRay(position, direction * distance, Color.red);
                if (Physics.Raycast(position, direction, distance, mask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool GeneralDetectionUnique(Vector3 rOPosition, Vector3 rDirection, float rDistance)
    {
        Debug.DrawRay(rOPosition, rDirection * rDistance, Color.yellow);
        return Physics.Raycast(rOPosition, rDirection, rDistance);
    }
    
    public static bool GeneralDetectionUnique(Vector3 rOPosition, Vector3 rDirection, float rDistance, string mask)
    {
        Debug.DrawRay(rOPosition, rDirection * rDistance, Color.yellow);
        return Physics.Raycast(rOPosition, rDirection, rDistance, LayerMask.GetMask(mask));
    }

    public static RaycastHit RaycastInformation(Vector3 rOPosition, Vector3 rDirection, float rDistance)
    {
        RaycastHit hit;
        Physics.Raycast(rOPosition, rDirection,out hit, rDistance);
        return hit;
    }
    
    public static RaycastHit RaycastInformation(Vector3 rOPosition, Vector3 rDirection, float rDistance, string mask)
    {
        RaycastHit hit;
        Physics.Raycast(rOPosition, rDirection,out hit, rDistance, LayerMask.GetMask(mask));
        return hit;
    }
    #endregion
}
