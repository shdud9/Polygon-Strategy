using UnityEngine;

public class HelloTazik : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    SayHello();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SayHello()
    {
        Debug.Log("Hello!");
    }
}
