using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DronesListsScript : MonoBehaviour
{
    [SerializeField] private GameObject redPanel;
    [SerializeField] private GameObject bluePanel;

    protected Dictionary<GameObject, GameObject> _panelToDrone = new Dictionary<GameObject, GameObject>(); 

    private Transform redContent;
    private Transform blueContent;

    private void Start()
    {
        redContent = redPanel.transform.parent;
        blueContent = bluePanel.transform.parent;
    }
    public void ClearList()
    {
        foreach (Transform child in redContent.transform)
        {
            if(child.name.Contains("Clone")) Destroy(child.gameObject);
        }
        foreach (Transform child in blueContent.transform)
        {
            if (child.name.Contains("Clone")) Destroy(child.gameObject);
        }

        _panelToDrone.Clear();
    }

    public IEnumerator CreateDronePanel(Side side, GameObject drone)
    {
        yield return new WaitForEndOfFrame();

        if (side == Side.Blue)
        {
            GameObject newBluePanel = Instantiate(bluePanel,blueContent);

            newBluePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = drone.name;

            EventTrigger.Entry eventTrigger = new EventTrigger.Entry();

            eventTrigger.eventID = EventTriggerType.PointerClick;
            eventTrigger.callback.AddListener((data) =>
            {
                OnPointerClick(newBluePanel);
            });

            newBluePanel.GetComponent<EventTrigger>().triggers.Add(eventTrigger);

            newBluePanel.SetActive(true);

            _panelToDrone.Add(newBluePanel, drone);
        }
        if(side == Side.Red)
        {
            GameObject newRedPanel = Instantiate(redPanel, redContent);

            newRedPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = drone.name;

            EventTrigger.Entry eventTrigger = new EventTrigger.Entry();

            eventTrigger.eventID = EventTriggerType.PointerClick;
            eventTrigger.callback.AddListener((data) =>
            {
                OnPointerClick(newRedPanel);
            });

            newRedPanel.GetComponent<EventTrigger>().triggers.Add(eventTrigger);

            newRedPanel.SetActive(true);


            _panelToDrone.Add(newRedPanel, drone);
        }
    }

    public void OnPointerClick(GameObject panel)
    {
        CameraModeScript.ChangeCameraState(_panelToDrone[panel]);
    }
}
