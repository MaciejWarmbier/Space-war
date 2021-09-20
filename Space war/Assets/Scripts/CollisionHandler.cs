using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] GameObject[] shipParts;

    private void OnCollisionEnter(Collision other) {
        CrashIntoTerrain();
    }

    private void OnTriggerEnter(Collider other) {
       CrashIntoTerrain();
    }

    private void CrashIntoTerrain(){
        GetComponent<PlayerControls>().enabled=false;
        foreach(GameObject part in shipParts){
            part.GetComponent<MeshRenderer>().enabled = false;
        }
        
        GetComponent<BoxCollider>().enabled = false;
        explosion.Play();
        Invoke("ReloadScene", delayTime);
    }

    private void ReloadScene(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
