using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRenderer : MonoBehaviour
{
    public GameObject previousRoom;
    public GameObject nextRoom;
    public GameObject doorFacade;
    public GameObject[] previousRoomWalls;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            previousRoom.SetActive(false);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);

            nextRoom.SetActive(true);
            doorFacade.SetActive(true);


    }
}
