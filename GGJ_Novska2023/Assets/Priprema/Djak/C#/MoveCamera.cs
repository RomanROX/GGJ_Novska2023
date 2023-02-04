using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform camPos; //child objekt playera koji samo prati poziciju

    private void Update()
    {
         transform.position = camPos.position; //pozicionira objekt s kamerom na position Player-ovog child-a.
    }
}
