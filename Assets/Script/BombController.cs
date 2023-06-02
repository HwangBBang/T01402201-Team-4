using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BombController : MonoBehaviour
{


    [Header("Bomb")]
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3f;
    public int bombAmount = 1;
    public int bombsRemaining;

    public Box boxPrefab;

    

    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }

    private void Update()
    {
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey))
        {
            //bombPrefab.GetComponent<Bomb>().PlaceBomb();
            string path = "Assets/Prefabs/"  + "Bomb" + ".prefab";
            GameObject prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)).GameObject();
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
     

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false; 
        }
    }

    


}
