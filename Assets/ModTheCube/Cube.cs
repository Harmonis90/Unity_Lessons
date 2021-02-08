using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;
    [SerializeField] [Range(0f, 1f)] float scaleFactor = .05f;
    Vector3 startScale;
    Vector3 currentSize;
    Material material;
    Color lerpedColor;
    void Start()
    {
        transform.position = new Vector3(3, 4, 1);
        transform.localScale = Vector3.one * 1.3f;
        startScale = Vector3.one;
        
        material = Renderer.material;

        lerpedColor = Color.white;
       
    }
    
    void Update()
    {
        transform.Rotate(10.0f * Time.deltaTime, 0.0f, 0.0f);
        //MouseSomething(); Grow On Mouse Over
        if (Input.GetMouseButtonDown(0))
        {
            MoveToMousePos();
        }
        LerpColor();
    }
   
    private void MouseSomething()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 scaleLerp = Vector3.Lerp(transform.localScale, new Vector3(1,1,1), .1f);
        if (Physics.Raycast(ray, out hit))
        {
           if (hit.collider.gameObject.name.StartsWith("Cube"))
            {
                GameObject cube = hit.collider.gameObject;
                cube.transform.localScale = new Vector3(cube.transform.localScale.x +scaleFactor, cube.transform.localScale.y + scaleFactor, 
                    cube.transform.localScale.z + scaleFactor);
                
            }
        }
        else
        {
            StartCoroutine(LerpDown());
        }
    }
    public IEnumerator LerpDown()
    {
            GameObject cube = this.gameObject;
            Transform cubeScale = cube.transform;
        cubeScale.localScale = Vector3.Lerp(startScale, cubeScale.localScale, 0);
        yield return new WaitForSeconds(.25f);
    }
    private void MoveToMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        currentSize = transform.localScale;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 placePos = new Vector3(currentSize.x + hit.point.x, currentSize.y + hit.point.y, currentSize.z + hit.point.z);
            transform.position = placePos;
        }
    }
    private void LerpColor()
    {
        lerpedColor = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));
        material.color = lerpedColor;
    }
   
}
