#pragma warning disable 649

using UnityEngine;
using UnityEngine.UI;
using ScrollItemData;

public class PlayController : MonoBehaviour
{
    public PlayerInputController input;
    [SerializeField] GameObject sceneTrans;

    private UIDataBinder binder;
    private ItemScrollManager levelScrollManager;
    private ItemSpawner container;
    private SnapManager snapManager;

    [SerializeField] GameObject buttonPlay;
    [SerializeField] GameObject levelInfo;

    [SerializeField] Text textLocked;
    // Start is called before the first frame update
    void Start()
    {
        Item item;
        levelScrollManager = GetComponent<ItemScrollManager>();
        container = GetComponent<ItemSpawner>();
        snapManager = GetComponent<SnapManager>();
        binder = GetComponentInParent<UIDataBinder>();
    }
    
    // Update is called once per frame
    void Update() {
        ControlPlayable();
    }

    public void ControlPlayable()
    {
        Item[] items = levelScrollManager.GetItems();
        int index = snapManager.SnapIndex;
        Item centerItem = items[index].GetComponent<Item>();
        MapItemData itemData = centerItem.Data.mapData;

        switch (itemData.basicData.state)
        {
            case ItemState.Locked:
                buttonPlay.SetActive(false);

                string levelName = centerItem.name;
                levelName = levelName.TrimStart(container.ItemName.ToCharArray());
                int levelNo;
                if (System.Int32.TryParse(levelName, out levelNo))
                {
                    textLocked.text = "reach level " + levelNo.ToString();
                }

                levelInfo.SetActive(true);
                break;
            case ItemState.Unlocked:
                binder.SetMapIndex(index);
                levelInfo.SetActive(false);
                buttonPlay.SetActive(true);
                break;
            default: throw new System.Exception("Something is wrong at PlayController.");
        }
    }
}
