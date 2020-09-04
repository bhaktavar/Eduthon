// Copyright 2017 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using UnityEngine.UI;

using PolyToolkit;

/// <summary>
/// Simple example that loads and displays one asset.
/// 
/// This example requests a specific asset and displays it.
/// </summary>
public class HelloPoly : MonoBehaviour {
    public InputField inputname;
    public GameObject PrefabZone;
    public GameObject whiteboard;
    private Bounds newbound;
    public GameObject penobject;
    public GameObject Leantouch;
    public bool boardflag = false;
    // ATTENTION: Before running this example, you must set your API key in Poly Toolkit settings.
    //   1. Click "Poly | Poly Toolkit Settings..."
    //      (or select PolyToolkit/Resources/PtSettings.asset in the editor).
    //   2. Click the "Runtime" tab.
    //   3. Enter your API key in the "Api key" box.
    //
    // This example does not use authentication, so there is no need to fill in a Client ID or Client Secret.

    // Text where we display the current status.
    public Text statusText;
    public bool flag = false;
    public void Search()
    {
        print(inputname.text);
        SearchAssetandSpawn(inputname.text);
    }
    public void SearchAssetandSpawn(string name)
    {
        PolyListAssetsRequest req = new PolyListAssetsRequest();
        // Search by keyword:
        req.keywords = name;
        // Only curated assets:
        req.curated = true;
        // Limit complexity to medium.
      //  req.maxComplexity = PolyMaxComplexityFilter.MEDIUM;
        // Only Blocks objects.
     //  req.formatFilter = PolyFormatFilter.;
       
        // Order from best to worst.
        req.orderBy = PolyOrderBy.BEST;
        // Up to 20 results per page.
       // req.pageSize = 20;
        // Send the request.
        PolyApi.ListAssets(req, MyCallback);
    }
  private void Start() {
        penobject.SetActive(false);
    // Request the asset.
    Debug.Log("Requesting asset...");
    PolyApi.GetAsset("assets/5vbJ5vildOq", GetAssetCallback);
    statusText.text = "Requesting...";
   // PolyApi.ListAssets(PolyListAssetsRequest.Featured(), MyCallback);

    }
    void MyCallback(PolyStatusOr<PolyListAssetsResult> result)
    {
        if (!result.Ok)
        {
            // Handle error.
            return;
        }
        // Success. result.Value is a PolyListAssetsResult and
        // result.Value.assets is a list of PolyAssets.
        foreach (PolyAsset asset in result.Value.assets)
        {
            PolyApi.GetAsset(asset.name, GetAssetCallback);
            break;
           // print();
            // Do something with the asset here.
        }
    }

    // Callback invoked when the featured assets results are returned.
    private void GetAssetCallback(PolyStatusOr<PolyAsset> result) {
    if (!result.Ok) {
      Debug.LogError("Failed to get assets. Reason: " + result.Status);
      statusText.text = "ERROR: " + result.Status;
      return;
    }
    Debug.Log("Successfully got asset!");

    // Set the import options.
    PolyImportOptions options = PolyImportOptions.Default();
    // We want to rescale the imported mesh to a specific size.
    options.rescalingMode = PolyImportOptions.RescalingMode.FIT;
    // The specific size we want assets rescaled to (fit in a 5x5x5 box):
    options.desiredSize = 5.0f;
    // We want the imported assets to be recentered such that their centroid coincides with the origin:
    options.recenter = true;
        

    statusText.text = "Importing...";
    PolyApi.Import(result.Value, options, ImportAssetCallback);
  }
    public void destroyblobs()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("paint");

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
    public void Board()
    {
        boardflag = !boardflag;
    }
    public void disablepen()
    {
        flag=!flag;
        penobject.SetActive(flag);
    }
    // Callback invoked when an asset has just been imported.
    private void ImportAssetCallback(PolyAsset asset, PolyStatusOr<PolyImportResult> result) {
    if (!result.Ok) {
      Debug.LogError("Failed to import asset. :( Reason: " + result.Status);
      statusText.text = "ERROR: Import failed: " + result.Status;
      return;
    }
    Debug.Log("Successfully imported asset!");

    // Show attribution (asset title and author).
    statusText.text = asset.displayName + "\nby " + asset.authorName;
    // Here, you would place your object where you want it in your scene, and add any
    // behaviors to it as needed by your app. As an example, let's just make it
    // slowly rotate:
   // result.Value.gameObject.AddComponent<Rotate>();
       var object1= result.Value.gameObject.transform.GetChild(0).gameObject.AddComponent<BoxCollider>();
         var pz = Instantiate(PrefabZone, result.Value.gameObject.transform);
         object1.transform.parent = pz.transform;
        BoxCollider colli = pz.GetComponent<BoxCollider>();
        newbound = object1.GetComponent<BoxCollider>().bounds;
        colli.size = new Vector3((newbound.size.x / object1.transform.localScale.x)+0.1f, (newbound.size.y / object1.transform.localScale.y)+0.1f, (newbound.size.z / object1.transform.localScale.z)+0.1f);
        colli.center = pz.transform.localPosition;

        /// var transl=result.Value.gameObject.AddComponent<Lean.Touch.LeanDragTranslate>();
        // var selec = result.Value.gameObject.AddComponent<Lean.Touch.LeanSelectable>();

    }
    private void Update()
    {
        Leantouch.SetActive(!flag);
        whiteboard.SetActive(boardflag);
    }
}