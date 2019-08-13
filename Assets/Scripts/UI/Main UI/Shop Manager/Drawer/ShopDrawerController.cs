using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ShopDrawerController : MonoBehaviour {
    public void OpenShop() {
        GetComponent<Animator>().SetTrigger("IsOpen");
    }
    public void CloseShop() {
        GetComponent<Animator>().SetTrigger("IsClose");
    }
    public void SetShopTrigger() {

    }
}
