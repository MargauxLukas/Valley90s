using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorBehavior : MonoBehaviour
{
    [SerializeField]
    private Balise currentBalise, ancientBalise;
    [SerializeField]
    private Vector2 speedRange = new Vector2(2, 4);
    private float speed;
    [SerializeField]
    private float viewDistance;

    private void Start()
    {
        speed = Random.Range(speedRange.x, speedRange.y);
        SearchForBalise();
    }

    // Update is called once per frame
    void Update()
    {
        //Balise en vue : Déplacement
        //Pas de balise en vue : Rieng pendant X secondes + déplacements aléatoire
    }

    private void AskToMove(Vector3 newDirection)
    {
        StopAllCoroutines();
        StartCoroutine(MoveCharacter(newDirection));
    }

    /*private void EndMovement()
    {
        StartCoroutine("WaitNextMovement");
    }*/

    private void SearchForBalise()
    {
        List<Balise> baliseInSight = new List<Balise>();

        if(ancientBalise!=null)
            Debug.Log(ancientBalise.gameObject);

        Collider[] allOverlappingColliders = Physics.OverlapSphere(transform.position, viewDistance);
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

        AskToMove(currentBalise.transform.position);
    }

    IEnumerator WaitNextToBalise()
    {
        yield return new WaitForSeconds(Random.Range(.5f, 2f));
        SearchForBalise();
    }

    IEnumerator MoveCharacter(Vector3 targetPoint)
    {
        float movedDistance = 0;
        targetPoint += new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f));
        Vector2 direction = GetDirectionFor3DObjects(transform.position, targetPoint);

        float titubage = 0;
        Vector3 titubageDirection = Vector3.zero;

        while (Vector3.Distance(targetPoint, transform.position) > 1)//3*speed*Time.deltaTime)
        {
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

    /*IEnumerator WaitNextMovement()
    {
        yield return new WaitForSeconds(2f);
        if (currentBalise != null)
        {
            AskToMove(GetDirectionFor3DObjects(transform.position, currentBalise.transform.position));
        }
    }*/

    /*private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Allo ?");
        if(other.GetComponent<Balise>() != null)
        {
            currentBalise = other.GetComponent<Balise>();
            AskToMove(GetDirectionFor3DObjects(transform.position, other.transform.position));
        }
    }*/

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
