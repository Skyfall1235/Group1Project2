using System.Collections;

using System.Linq;
using UnityEngine;

public class KeyAndLock : MonoBehaviour
{
    [SerializeField] private bool Key = false;
    [SerializeField] private KeyItem keyID;
    [SerializeField] private LockItem lockItem;
    [SerializeField] private GameObject textBox;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    //id associated with the key or lock it will be


    //public IEnumerator MoveTextBoxUp(GameObject textBox, float moveUpDuration, float waitDuration, float moveDownDuration)
    //{
    //    // Move the text box up from the bottom.
    //    float elapsedTime = 0f;
    //    while (elapsedTime < moveUpDuration)
    //    {
    //        textBox.transform.position = new Vector3(textBox.transform.position.x, textBox.transform.position.y + (Time.deltaTime / moveUpDuration), textBox.transform.position.z);
    //        yield return null;
    //        elapsedTime += Time.deltaTime;
    //    }

    //    // Wait for the predefined time.
    //    yield return new WaitForSeconds(waitDuration);

    //    // Move the text box back down.
    //    elapsedTime = 0f;
    //    while (elapsedTime < moveDownDuration)
    //    {
    //        textBox.transform.position = new Vector3(textBox.transform.position.x, textBox.transform.position.y - (Time.deltaTime / moveDownDuration), textBox.transform.position.z);
    //        yield return null;
    //        elapsedTime += Time.deltaTime;
    //    }

    //    // Disable the text box so that it cannot be activated again.
    //    textBox.SetActive(false);
    //

    private void Start()
    {
        StartCoroutine(WaitTilLoad());
        
    }

    private IEnumerator WaitTilLoad()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject[] inactiveTextBoxes = Resources.FindObjectsOfTypeAll<GameObject>().Where(gameObject => gameObject.tag == "TEMP" && !gameObject.activeInHierarchy).ToArray();

        textBox = inactiveTextBoxes[0];
        if (textBox == null)
        {
            Debug.LogWarning("temp was not found");
        }
    }

    private IEnumerator WaitSecondsThenTurnOff(float seconds, GameObject go)
    {
        yield return new WaitForSeconds(seconds);
        go.SetActive(false);
    }

    //for what a key is
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            Debug.Log("not a player");
            return;
        }
        //Debug.Log("object is hitting wall");
        PlayerInteraction interaction = other.gameObject.GetComponent<PlayerInteraction>();
        Debug.Log("finding interaction");

        if (interaction != null && Key)
        {
            if (source != null)
            {
                source.PlayOneShot(clip);
            }
            interaction.collectedKeyItems.Add(keyID);
            gameObject.SetActive(false);
        }
    }

    //FOR WHAT A LOCK IS
    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("colliding?");
        if (other.gameObject.tag != "Player")
        {
            //Debug.Log("not a player");
            return;
        }
        //Debug.Log("object is hitting wall");
        PlayerInteraction interaction = other.gameObject.GetComponent<PlayerInteraction>();
        //Transform parent = transform.parent;
        //Transform ssUI = parent.transform.GetChild(0);
        //Transform gamePanel = ssUI.transform.GetChild(0);
        //GameObject doorResponse = gamePanel.transform.GetChild(0).gameObject;
        
        //Debug.Log("finding interaction");
        if (interaction != null)
        {
            //Debug.Log("searching for key");
            if (interaction.collectedKeyItems.Count <= 0) 
            {
                textBox.SetActive(true);
                StartCoroutine(WaitSecondsThenTurnOff(5f, textBox));
                return;
            }
            foreach (KeyItem keyItem in interaction.collectedKeyItems)
            {
                //Debug.Log("checking each item in the colelcted keys");
                if (keyItem.ID == lockItem.ID)
                {
                    if (source != null)
                    {
                        source.PlayOneShot(clip);
                    }
                    lockItem.unlocked = true;
                    lockItem.invisWall.SetActive(false);
                }
                else
                {
                    textBox.SetActive(true);
                    StartCoroutine(WaitSecondsThenTurnOff(5f, textBox));
                }
            }
        }
    }


}
