#pragma warning disable 649

using System.Collections;
using UnityEngine;

public enum SceneType {
    Main,
    Game
}

public class LevelBarController : MonoBehaviour {
    [Header("Behaviour")]
    public SceneType sceneType = SceneType.Main;

    [Header("Data")]
    [SerializeField] private UIDataBinder binder;
    private ExperiencePointsData experiencePoints;
    private IIndicate levelIndicator;

    [Header("Level Bar")]
    [SerializeField] private BarFillController controller;
    [SerializeField] private GameObject levelIndicatorObject;
    [Header("Experience")]
    public int m_experience;


    private void Start() {
        if(sceneType == SceneType.Main) {
            controller.DisableXPText();
            Apply();
        }
    }

    public void Apply() {
        binder = GetComponentInParent<UIDataBinder>();
        experiencePoints = binder.GetExperiencePoints();

        levelIndicator = levelIndicatorObject.GetComponent<LevelIndicator>();
        levelIndicator.ApplyChanges(experiencePoints.level);

        controller.SetFill(experiencePoints.currentProgress);

        switch(controller.FillType) {
            case BarFillType.Static:
                binder.SaveExperience();
                controller.SetFill(experiencePoints.currentProgress);
                controller.UpdateIndicator(experiencePoints.level);
                break;
            case BarFillType.Animated:
                if(experiencePoints.nextProgress != experiencePoints.currentProgress) {
                    StartCoroutine(AnimateBar());
                }
                break;
            default:
                throw new System.Exception("LevelBarController::Apply() called BarFillController::FillType and it returned null.");
        }
    }

    private IEnumerator AnimateBar() {
        //Wait half seconds
        yield return new WaitForSeconds(0.5f);

        //Start XP Text Animation
        controller.SetXPText(experiencePoints.claimedExperiencePoints);

        //Set Next Data
        binder.SaveExperience();

        //Update XP Cache
        experiencePoints = binder.GetExperiencePoints();

        //Wait half seconds
        yield return new WaitForSeconds(0.5f);

        //Call Animation
        StartCoroutine(controller.AnimateFill(experiencePoints.currentProgress));
    }

    public void SetVariable(int experience) {
        PlayerData.instance.SetPlayerXpAfterLevel(experience);
    }
}
