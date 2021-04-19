using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
//This class contains all the behaviours and data to edit the Car. 
public class CarEditor : MonoBehaviour
{
    public Mesh[] body;
    int body_index = 0;
    private void Start() {
        ChangeMesh();
    }
   public void NextBody()
   {
       if(body_index < body.Length-1)
       {
           body_index++;
       }else body_index = 0;
       ChangeMesh();
   }

   public void PrevBody()
   {
       if(body_index > 0)
       {
           body_index--;
       }else body_index = body.Length-1;
       ChangeMesh();
   }

//Applies the materials to the editing kart.
   void  ChangeMesh()
   {
       gameObject.GetComponentInChildren<MeshFilter>().mesh = body[body_index];
   }

//Saves and goes back to menu
   public void SaveAndExit()
   {
       EditorHelper save = new EditorHelper();
       save.mesh = gameObject.GetComponentInChildren<MeshFilter>().sharedMesh;
       string g = JsonUtility.ToJson(save);
       string jsonFilePath = Application.dataPath + "/edit.json";
       File.WriteAllText(jsonFilePath, g);
       SceneManager.LoadScene(0);
   }

}
