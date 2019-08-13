using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOpenCheck : MonoBehaviour {
    public static GameOpenCheck instance = null;

    void Awake() {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
