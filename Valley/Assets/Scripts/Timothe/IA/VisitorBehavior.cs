using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorBehavior : MonoBehaviour
{
    [SerializeField]
    private Balise currentBalise, ancientBalise;
    [SerializeField]
    private Vector2 speedRange = new Vector2(2, 4);
    [SerializeField]
    private float speed;
    [SerializeField]
    private float viewDistance;

    [SerializeField]
    private LayerMask baliseLayer;

    public Animator animator;

    private void Start()
    {
        speed = Random.Range(speedRange.x, speedRange.y);
        SearchForBalise();
    }

    private void AskToMove(Vector3 newDirection)
    {
        PathRequestManager.RequestPath(transform.position, newDirection, 99, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            animator.SetBool("isWalking", true);
            StopAllCoroutines();
            StartCoroutine(MoveCharacter(newPath));
        }
    }

    private void SearchForBalise()
    {
        List<Balise> baliseInSight = new List<Balise>();

        Collider[] allOverlappingColliders = Physics.OverlapSphere(transform.position, viewDistance, baliseLayer);
        foreach(Collider col in allOverlappingColliders)
        {
            if(col.GetComponent<Balise>()!=null && ancientBalise != col.GetComponent<Balise>() && currentBalise != col.GetComponent<Balise>())
            {
                baliseInSight.Add(col.GetComponent<Balise>());
            }
        }

        if (baliseInSight.Count > 0)
        {
            ancientBalise = currentBalise;

            currentBalise = baliseInSight[Random.Range(0, baliseInSight.Count)];
        }
        else
        {
            Balise tmp = currentBalise;
            currentBalise = ancientBalise;
            ancientBalise = tmp;
        }

        if (currentBalise != null)
        {
            AskToMove(currentBalise.transform.position);
        }
        else
        {
            StartCoroutine(WaitForEploration());
        }
    }

    IEnumerator WaitNextToBalise()
    {
        animator.SetBool("isWalking", false);
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        SearchForBalise();
    }

    IEnumerator MoveCharacter(Vector3[] path)
    {
        float movedDistance = 0;
        int targetIndex = 0;

        if(path.Length<=0)
        {
            StartCoroutine(WaitNextToBalise());
            yield break;
        }

        Vector3 targetPoint = path[0];
        Vector2 direction = GetDirectionFor3DObjects(transform.position, targetPoint);

        float titubage = 0;
        Vector3 titubageDirection = Vector3.zero;

        Vector3 finalPoint = path[path.Length - 1] + new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f));

        while (Vector3.Distance(finalPoint, transform.position) > 1)//3*speed*Time.deltaTime)
        {
            if (Vector3.Distance(targetPoint, transform.position) <= 1)//2* speed * Time.deltaTime)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    break;
                }
                targetPoint = path[targetIndex];
            }
            transform.position += (new Vector3(direction.x, 0, direction.y) + titubageDirection).normalized * speed * Time.deltaTime;
            movedDistance += speed * Time.deltaTime;
            titubage += speed * Time.deltaTime;
            if (titubage >= 1.5f)
            {
                titubage = 0;
                direction = GetDirectionFor3DObjects(transform.position, targetPoint);
                titubageDirection = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            }
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(WaitNextToBalise());
    }

    IEnumerator WaitForEploration()
    {
        animator.SetBool("isWalking", false);
        Vector3 destination = GetRandomPositionWithDistance(5);
        while(!Grid.instance.NodeFromWorldPoint(destination).walkable)
        {
            yield return new WaitForSeconds(0.2f);
            destination = GetRandomPositionWithDistance(5);
        }

        AskToMove(destination);
    }

    private Vector3 GetRandomPositionWithDistance(float maxDistance)
    {
        Vector4 bound = Grid.instance.WorldBounds;
        Vector3 destination = transform.position + new Vector3(Random.Range(-maxDistance, maxDistance), 0, Random.Range(-maxDistance, maxDistance));
        destination = new Vector3(Mathf.Clamp(destination.x, bound.x, bound.y), 0, Mathf.Clamp(destination.z, bound.z, bound.w));

        return destination;
    }

    private Vector2 GetDirectionFor3DObjects(Vector3 objStart, Vector3 objEnd)
    {
        Vector3 direction3D = objEnd - objStart;

        return new Vector2(direction3D.x, direction3D.z).normalized;
    }

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.blue;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, viewDistance);
    }
}
