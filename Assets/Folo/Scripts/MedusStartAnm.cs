using System.Collections;
using UnityEngine;

public class MedusStartAnm : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Animator>().Update(Random.Range(0.1f, 1.0f));
    }
}
