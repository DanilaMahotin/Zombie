using UnityEngine;

public class granade : MonoBehaviour
{
    //public bool bang = false;

    public void OnTriggerEnter(Collider other)
    {
        GameObject childObject = other.gameObject;
        GameObject parentObject = childObject.transform.root.gameObject;
        if (parentObject.CompareTag("zombieDefault"))
        {

            var zombie = parentObject.GetComponent<ZombieMove>();
            zombie.healthPoint = 0;
        }
        else if (parentObject.CompareTag("zombiePolice"))
        {
            var zombie = parentObject.GetComponent<ZombiePolice>();
            zombie.healthPoint = 0;
        }
        else if (parentObject.CompareTag("zombieBuilder"))
        {
            var zombie = parentObject.GetComponent<ZombieBuilder>();
            zombie.healthPoint = 0;
        }
        else if (parentObject.CompareTag("ZombieBoss"))
        {
            var zombie = parentObject.GetComponent<ZombieBoss>();
            zombie.healthPoint = 0;
        }
    }
    

}
