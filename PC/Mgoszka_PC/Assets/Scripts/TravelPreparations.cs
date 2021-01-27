using UnityEngine;

public class TravelPreparations : MonoBehaviour
{
    public int TravelTime;
    public int NewWorldId;
    public GameObject travelDest;
    
    public void PrepareTravel(int time, int worldId, GameObject travelDestination)
    {
        TravelTime = time;
        NewWorldId = worldId;
        travelDest = travelDestination;
    }

    public void Travel()
    {
        GameObject.FindGameObjectWithTag("controller").GetComponent<TimeCountingSystem>().Travel(travelDest, TravelTime);
        GameObject.FindGameObjectWithTag("controller").GetComponent<TimeCountingSystem>().MoveToWorld(NewWorldId);
    }
}
