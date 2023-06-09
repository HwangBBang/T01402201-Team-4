using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class CSVMapMaker : MonoBehaviour
{
    [SerializeField] private string fileName;
    private string prefabName;
    private string prefabFolder;
    private float positionX;
    private float positionY;
    private float placementDelay = 0.1f;

    private GameObject prefab;
    private GameObject parent;
    private Coroutine routine;
    List<Dictionary<string, object>> dicList = new List<Dictionary<string, object>>();
    string[] itemName = new[] {"ItemCount", "ItemPower", "ItemSpeed", "ItemSuperPower", "Lucci" };
    int[] probability = new[] { 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 4, 5, 5, 5, 5, 5, 5 };

    
    
    private IEnumerator LoadCSVMap(int length)
    {
        while (GameManager.instance.statusGame != 1)
        {
            yield return null;
        }

        float currentX = float.MinValue;
        for (int i = 0; i < length; i++)
        {
            prefabName = dicList[i]["prefapName"].ToString();
            positionX = float.Parse(dicList[i]["positionX"].ToString());
            positionY = float.Parse(dicList[i]["positionY"].ToString());
            if (currentX < positionX)
            {
                currentX = positionX;
                yield return new WaitForSeconds(placementDelay);
            }
            
            if (prefabName.Contains("Box"))
            {
                prefabFolder = "Box/";
                parent = GameObject.FindWithTag("Box");
            }
            else if (prefabName.Contains("Solid"))
            {
                prefabFolder = "Solid/";
                parent = GameObject.FindWithTag("Solid");
            }
            string path = "Assets/Prefabs/" + prefabFolder + prefabName + ".prefab";
            prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)).GameObject();

            GameObject nowSpawn = Instantiate(prefab,
                new Vector3(positionX, positionY, 0), Quaternion.identity, parent.transform);
            inputItem(nowSpawn);
        }
        if (GameManager.instance.statusGame == 2)
        {
            GameManager.instance.statusGame = 3;
        }
        if (GameManager.instance.statusGame == 1)
        {
            GameManager.instance.statusGame = 2;
        }
        
    }

    private void Start()
    {
        List<Vector3> positions = new List<Vector3>();
        List<String> prefaps = new List<string>();
        dicList.Clear();

        dicList = CSVReader.Read(fileName);
        if (GameManager.instance.statusGame == 12)
        {
            Destroy(transform.gameObject);
        }
    }

    private void Update()
    {
        if (GameManager.instance.statusGame == 1 && routine.IsUnityNull())
        {
            routine = StartCoroutine(LoadCSVMap(dicList.Count));
        }
    }

    private void inputItem(GameObject parent)
    {
        int index = probability[Random.Range(0, probability.Length)];
        if (parent.gameObject.CompareTag("Box") && index < 5)
        {
            string nowSpawnName = itemName[index];
            string path = "Assets/Prefabs/Item/" + nowSpawnName + ".prefab";
            GameObject nowSpawn = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)).GameObject();
            Instantiate(nowSpawn, Vector3.zero, Quaternion.identity, parent.transform).SetActive(false);
        }
    }
}
