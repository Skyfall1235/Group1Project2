using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAndLock : MonoBehaviour
{
    [SerializeField] private GameObject invisWall;
    [SerializeField] private bool Key;
    [SerializeField] private KeyItem keyID;
    //if not key, then lock
    [SerializeField] private int lockID;
    [SerializeField] private bool unlocked = false;
    [SerializeField] private GameObject textBox;

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

    private IEnumerator WaitSecondsThenTurnOff(float seconds, GameObject go)
    {
        yield return new WaitForSeconds(seconds);
        go.SetActive(false);
    }


    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("colliding?");
        if (other.gameObject.tag != "Player")
        {
            Debug.Log("not a player");
            return;
        }
        //Debug.Log("object is hitting wall");
        PlayerInteraction interaction = other.gameObject.GetComponent<PlayerInteraction>();
        Debug.Log("finding interaction");
        if (interaction != null)
        {
            Debug.Log("searching for key");
            if (interaction.collectedKeyItems.Count <= 0) 
            {
                textBox.SetActive(true);
                StartCoroutine(WaitSecondsThenTurnOff(5f, textBox));
                return;
            }
            foreach (KeyItem keyItem in interaction.collectedKeyItems)
            {
                Debug.Log("checking each item in the colelcted keys");
                if (keyItem.ID == lockID)
                {
                    unlocked = true;
                    invisWall.SetActive(false);
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
