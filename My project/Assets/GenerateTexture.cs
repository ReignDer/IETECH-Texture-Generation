using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class GenerateTexture : MonoBehaviour
{
    public List<Texture> m_Textures = new List<Texture>();
    [SerializeField]
    private bool activate = false;
    private int index = 0;
    private int counter = 0;
    private int totalTextures = 0;
    [SerializeField]
    private GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Amount in List: " + m_Textures.Count);
    }

    public void GenerateMaterials()
    {
        int counterTexture = 0;
        for (int i =0; i <= m_Textures.Count - 1; i++)
        {
            
            Debug.Log("Creating material from: " + m_Textures[i]);

            Texture selected = m_Textures[i];
            Debug.Log("Texture Number: " + i);

            Material material = new Material(Shader.Find("Standard"));
            GenerateCube(selected,material, counterTexture);
            

            counterTexture++;
            

        }
        AssetDatabase.SaveAssets();
        this.activate = false;
        Debug.Log("Done!");
        Debug.Log(totalTextures);
    }

    private void GenerateCube(Texture texture, Material material, int i)
    {
        int counterGeneration = 0;
        for (int index = 1; index <= m_Textures.Count - 1; index++)
        {
            int nextMat = index + i;
            //yield return new WaitForSeconds(0.5f);

            if ((nextMat) > m_Textures.Count - 1)
                break;

            Texture detailTexture = m_Textures[nextMat];
            Material newMaterial = new Material(material);
            Debug.Log( "Index2ndMap: " + (nextMat));
            newMaterial.SetTexture("_DetailAlbedoMap", detailTexture);
            newMaterial.mainTexture = texture;

            //yield return new WaitForSeconds(0.5f);
            cube.gameObject.GetComponent<Renderer>().material = newMaterial;
            Instantiate(cube);
            saveMaterial(newMaterial);
            Debug.Log("Textures made:" + "[" + counterGeneration++ + "]");
            totalTextures++;
        };
        Debug.Log("TextureIndex: " + counter++);
        

    }

    void saveMaterial(Material material)
    {
        var materialName = "material_" + totalTextures + ".mat";
        AssetDatabase.CreateAsset(material, "Assets/CreateTexture/" + materialName);
    }
    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            GenerateMaterials();
        }
    }
}
