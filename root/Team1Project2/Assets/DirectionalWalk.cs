using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DirectionalWalk : MonoBehaviour
{
    [SerializeField] private movmentControl m_movementController; //populate it when we collide
    [SerializeField] private Transform walkOffset;
    [SerializeField] private float duration;
    [SerializeField] private Vector3 initialWalkVector;
    private Coroutine lerpCoroutine;
    [SerializeField] bool is_masterWalk = false;
    [SerializeField] DirectionalWalk masterWalk;
    [SerializeField] bool m_isEnd = false;
    public bool isLerpRunning = false;

    private void Start()
    {
        if(is_masterWalk)
        {
            masterWalk = this;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hitting something");
        Debug.Log($"{other.gameObject.CompareTag("Player")}  {!masterWalk.isLerpRunning}");
        Debug.Log($"{other.gameObject}");
        SetIntitalWalk();
        if (other.gameObject.CompareTag("Player") && !masterWalk.isLerpRunning)
        {
            Debug.Log("setting movement controller");
            m_movementController = other.gameObject.GetComponent<movmentControl>();
            Debug.Log(m_movementController);
            if (!m_isEnd)
            {
                if (walkOffset == null)
                {
                    Debug.LogWarning("walkoffset is missing on a startpeice");
                    return;
                }
                masterWalk.isLerpRunning = true;
                lerpCoroutine = StartCoroutine(MoveToPosition(walkOffset.position, true));
            }
            else
            {
                masterWalk.isLerpRunning = true;
                lerpCoroutine = StartCoroutine(MoveToPosition(initialWalkVector, false));
            }
        }
    }

    private IEnumerator MoveToPosition(Vector3 locationToMoveTo, bool walk)
    {
        //z axis to lerp to, using the urrent position
        SetIntitalWalk();
        Vector3 NewWalkVector = new Vector3(initialWalkVector.x, initialWalkVector.y, locationToMoveTo.z);
        m_movementController.offAxisWalkValue = locationToMoveTo.z;
        Debug.Log(NewWalkVector);
        Debug.Log(m_movementController.offAxisWalkValue);
        


        float elapsedTime = 0f;
        float t = 0f;

        while (t < 1f)
        {
            t += elapsedTime / duration;
            elapsedTime += Time.deltaTime;

            // Lerp the vector from startVector to endVector based on the elapsed time and the duration.
            Vector3 lerpedVector = Vector3.Lerp(m_movementController.gameObject.transform.position, NewWalkVector, t);

            // Set the position to the lerped vector.
            m_movementController.gameObject.transform.position = lerpedVector;
            m_movementController.offAxisWalkValue = lerpedVector.z;
            Debug.Log(t);
            yield return null;
        }
        m_movementController.directionalWalk = walk;
        masterWalk.isLerpRunning = false;
        SetIntitalWalk();
        Debug.Log(m_movementController.directionalWalk);

    }

    private void SetIntitalWalk()
    {
        if (m_movementController != null)
        {
            Vector3 playerPos = m_movementController.transform.position;
            initialWalkVector = new Vector3(playerPos.x, playerPos.y, 0);
        }
    }


    //when the player enters, lerp the position of it to the specified gameobject transform, and lock it there

    //when they leave, lerp it back to 0 and turn the players bool off
}
