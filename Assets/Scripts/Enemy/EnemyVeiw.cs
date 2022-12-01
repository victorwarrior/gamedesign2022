/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVeiw : MonoBehaviour
{
    public float radius = 20f;
    [Range(1, 360)] public float angle = 360f;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;

    public GameObject[] players;

    public bool canSeePlayers { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        StartCoroutine(FOVCheck());
    }

    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (true)
        {
            FOV();
            yield return wait;
        }

    }

    private void FOV()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        float vinkel = 0;
        float distanceToTarget = 0;
        canSeePlayers = false;

        if (rangeCheck.Length > 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            vinkel = Vector2.Angle(transform.up, directionToTarget);
            //Debug.Log($"Vinkel: {vinkel}");
            //if (Vector2.Angle(transform.up, directionToTarget) < angle * 0.5)
            //{
                distanceToTarget = Vector2.Distance(transform.position, target.position);
                canSeePlayers = distanceToTarget < 20;

            //    if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
            //        canSeePlayers = true;
            //else
            //    canSeePlayers = false;
            //}
            //else
            //    canSeePlayers = false;
        }
        //else if (canSeePlayers)
        //    canSeePlayers = false;

        Debug.Log($"Vinkel: {vinkel}    DistanceToTarget: {distanceToTarget}   CanSeePlayer: {canSeePlayers}");

        var enemyEye = this.gameObject.GetComponentInChildren<EnemyEye>();

        if (canSeePlayers == true)
        {
          //  enemyEye.CurrentPlayer = transform.position;
           // enemyEye.foundPlayer = true;

            //enemyEye.eyeFollow(transform.position);
        }
        //else
        //    enemyEye.foundPlayer = false;

        System.Threading.Thread.Sleep(100);
        }


}
*/