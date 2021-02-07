using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    public GameObject Trail;
    public Bird TargetBird;

    private List<GameObject> _trail;

    // Start is called before the first frame update
    void Start()
    {
        _trail = new List<GameObject>();
    }

    public void SetBird(Bird bird)
    {
        TargetBird = bird;

        for(int i = 0; i < _trail.Count; i++)
        {
            Destroy(_trail[i].gameObject);
        }

        _trail.Clear();
    }

    public IEnumerator SpawnTrail()
    {
        yield return new WaitForSeconds(0.1f);

        _trail.Add(Instantiate(Trail, TargetBird.transform.position, Quaternion.identity));
        
        if(TargetBird != null && TargetBird.State != Bird.BirdState.HitSomething)
        {
            StartCoroutine(SpawnTrail());
        }
    }
}
