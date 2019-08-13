using UnityEngine;

public class ItemsWindowController : MonoBehaviour
{
    private GameObject bodyShop;
    private GameObject dependentShop;
    private GameObject independentShop;
    private GameObject flagShop;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var gameObject = transform.GetChild(i).GetComponent<GameObject>();
            ShopType type = gameObject.GetComponent<ShopTypeController>().type;
            switch (type)
            {
                case ShopType.Body:
                    bodyShop = gameObject.GetComponent<GameObject>();
                    break;
                case ShopType.Front:
                    dependentShop = gameObject.GetComponent<GameObject>();
                    break;
                    
                case ShopType.Special:
                    independentShop = gameObject.GetComponent<GameObject>();
                    break;
                case ShopType.Flag:
                    flagShop = gameObject.GetComponent<GameObject>();
                    break;
                default:
                    throw new System.Exception("Type is null.");
            }
        }
        bodyShop.SetActive(true);
        dependentShop.SetActive(false);
        independentShop.SetActive(false);
        flagShop.SetActive(false);
    }

    public void OpenShop(ShopType type)
    {
        switch (type)
        {
            case ShopType.Body:
                bodyShop.SetActive(true);
                dependentShop.SetActive(false);
                independentShop.SetActive(false);
                flagShop.SetActive(false);
                break;
            case ShopType.Front:
                bodyShop.SetActive(false);
                dependentShop.SetActive(true);
                independentShop.SetActive(false);
                flagShop.SetActive(false);
                break;
            case ShopType.Special:
                bodyShop.SetActive(false);
                dependentShop.SetActive(false);
                independentShop.SetActive(true);
                flagShop.SetActive(false);
                break;
            case ShopType.Flag:
                bodyShop.SetActive(false);
                dependentShop.SetActive(false);
                independentShop.SetActive(false);
                flagShop.SetActive(true);
                break;
            default:
                throw new System.Exception("Type is wrong.");
        }
    }
}
