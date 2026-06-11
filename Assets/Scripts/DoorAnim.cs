using System.Collections;
using UnityEngine;

public class DoorAnim : MonoBehaviour
{
    [SerializeField] private float openAngel;
    [SerializeField] private float closedAngel;
    [SerializeField] private float doorOpenTime;
    
    private void Start()
    {
        StartCoroutine(DoorTimer());
    }

    private IEnumerator DoorTimer()
    {
        transform.Rotate(new Vector3(0, openAngel, 0));

        yield return new WaitForSeconds(1);
        transform.Rotate(new Vector3(0, closedAngel, 0));
        
        yield return new WaitForSeconds(1);
        StartCoroutine(DoorTimer());
    }
}
