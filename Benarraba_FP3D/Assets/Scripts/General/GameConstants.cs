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
    #region Player
    //Name and Tag
    public const string playerNameAndTag = "Player";
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
    #region Benarraba objects
    //Objects
    public const string benarrabaBaltasarName = "Baltasar arreglado";
    public const string benarrabaMelchorPresent = "MelchorRegalo";
    public const string benarrabaGasparPresent = "GasparRegalo";
    public const string benarrabaBaltasarPresent = "BaltasarRegalo";

    //Panels
    public const string benarrabaFadeInPanel = "FadeIn";
    public const string benarrabaFadeOutPanel = "FadeOut";
    public const string benarrabaPDialog = "PDialog";

    //Animations
    public const string benarrabaPDialogSpawn = "PDialog_spawn";
    public const string benarrabaPDialogDespawn = "PDialog_despawn";
    public const string benarrabaFadeOutWhite = "FadeOut_white";
    public const string benarrabaBaltasarRotate = "BaltasarRotate";

    //Magic kings intro texts
    public const string gasparText1 = "¡Pero vaya desastre! El incienso que llevaba ha desaparecido en el camino.\n" +
        "Si sabes de alguien que pueda ayudarme a recuperarlo, te ruego que lo hagas.\n" +
        "El obsequio es vital para cumplir con mi deber.";
    public const string baltasarText1 = "(Entre murmuros) Como no encontremos los regalos a tiempo, seremos nosotros los que recibamos carb...";
    public const string baltasarText2 = "¡Hola! La mirra que llevaba se ha debido caer por el camino, si pudieras " +
        "ayudarme a encontrarlo... ¿Si? ¡Genial, me haces un gran favor!";
    public const string melchorText1 = "¡No me puedo creer que hayamos perdido los regalos!\n" +
        "Si pudieras ayudarnos a encontrarlos, te estaremos eternamente agradecidos.\n¡Que la estrella que nos guarda ilumine tu camino!";

    //Magic kings outro texts
    public const string melchorText2 = "Gracias por haber encontrado el regalo. " +
        "La estrella me ha guiado por un camino largo y lleno de dificultades, pero gracias a ti, el oro que llevaba para el niño ha vuelto a mi lado.";
    public const string gasparText2 = "Tienes mi profundo agradecimiento por haber encontrado el incienso. " +
        "Esta Navidad, quiero que sepas que, gracias a tu esfuerzo, podremos llevar los regalos a los niños de todas partes.";
    public const string baltasarText3 = "¡Gracias! Con tu ayuda tengo la mirra de vuelta en mis manos, y mi corazón rebosa de gratitud. " +
        "Tu esfuerzo ha hecho posible que mis regalos lleguen a su destino.";
    public const string todosText1 = "Desde lo profundo de nuestro ser, gracias y...";
    public const string todosText2 = "¡FELIZ NAVIDAD!";
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

    public static IEnumerator FadeInOut(Animator animator, bool deactiveOnEnd)
    {
        yield return null;

        animator.gameObject.SetActive(true);
        animator.enabled = true;

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f) yield return null;

        animator.enabled = false;
        if (deactiveOnEnd) animator.gameObject.SetActive(false);

        //GameManager.Instance.fadeEnd = true;

        yield return null;
    }
    #endregion
}
