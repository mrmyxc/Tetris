using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour {

    public GameObject[] shapes;
    public GameObject[] nextShapes;

    GameObject nextShape = null;
    int nextShapeIndex;

    public void SpawnShape ()
    {
        //7 shapes
        int shapeIndex = nextShapeIndex;   
        Instantiate(shapes[shapeIndex], transform.position, Quaternion.identity);
        nextShapeIndex = Random.Range(0, 7);
        Vector3 nextShapePosition = new Vector3(-6.5f, 15f, 0f );
        
        if (nextShape != null)
        {
            Destroy(nextShape);
        }
        nextShape = Instantiate(nextShapes[nextShapeIndex], nextShapePosition, Quaternion.identity);
    }

	// Use this for initialization
	void Start () {
        nextShapeIndex = Random.Range(0, 7);
        SpawnShape();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
