using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrans : MonoBehaviour {
    [SerializeField] GameObject openSceneTransitionPrefab;
    [SerializeField] GameObject closeSceneTransitionPrefab;
    public ShopDrawerController shopController;

    void Start() {
        OpenSceneTransition();
    }
    void Update() {
    }
    public void OpenSceneTransition() {
        if(GameOpenCheck.instance != null) {
            GameObject sceneObject = Instantiate(openSceneTransitionPrefab, transform.position, Quaternion.identity) as GameObject;
            sceneObject.transform.parent = gameObject.transform;
            StartCoroutine(DestroyGameObject(sceneObject, 2f));
        } else {
            return;
        }
    }
    public void CloseSceneTransition(string newSceneName) {

        StartCoroutine(OpenScene(newSceneName));
        GameObject sceneObject = Instantiate(closeSceneTransitionPrefab, transform.position, Quaternion.identity) as GameObject;
        sceneObject.transform.parent = gameObject.transform;
    }
    public IEnumerator DestroyGameObject(GameObject openPrefab, float time) {
        yield return new WaitForSeconds(time);
        Destroy(openPrefab);
    }

    public IEnumerator OpenScene(string sceneName) {
        yield return new WaitForSeconds(0.8f);
        //   Destroy(Scene )
        //  SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        //      SceneManager.UnloadScenenloadScene(string sceneName);
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void PanelTrans() {
        Invoke("OpenNewPanel", 1f);
        GameObject sceneObject = Instantiate(closeSceneTransitionPrefab, transform.position, Quaternion.identity) as GameObject;
        sceneObject.transform.parent = gameObject.transform;
        shopController.OpenShop();
        StartCoroutine(DestroyGameObject(sceneObject, 1f));


    }
    public void OpenNewPanel() {
        GameObject sceneObject = Instantiate(openSceneTransitionPrefab, transform.position, Quaternion.identity) as GameObject;
        sceneObject.transform.parent = gameObject.transform;
        StartCoroutine(DestroyGameObject(sceneObject, 1f));

    }
    public void PanelTrans2() {
        Invoke("OpenNewPanel", 1f);
        GameObject sceneObject = Instantiate(closeSceneTransitionPrefab, transform.position, Quaternion.identity) as GameObject;
        sceneObject.transform.parent = gameObject.transform;
        shopController.CloseShop();
        StartCoroutine(DestroyGameObject(sceneObject, 1f));

    }

}
