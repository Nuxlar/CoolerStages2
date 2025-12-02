using UnityEngine;

namespace CoolerStages
{
    public class Stage6
    {

        public static void Moon(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3)
        {
            if (terrainMat && detailMat && detailMat2 && detailMat3)
            {
                MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer renderer in meshList)
                {
                    GameObject meshBase = renderer.gameObject;
                    if (meshBase != null)
                    {
                        if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
                        {
                            GameObject.Destroy(meshBase);
                        }
                        if ((meshBase.name.Contains("Terrain") || meshBase.name.Contains("terrain") || meshBase.name.Contains("OuterRing_Cave")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = terrainMat;
                        if (meshBase.name.Contains("Rock") && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat;
                        if ((meshBase.name.Contains("Bridge") || meshBase.name.Contains("MoonPillar") || meshBase.name.Contains("Roof") || meshBase.name.Contains("Chain") || meshBase.name.Contains("Arena") || meshBase.name.Contains("otherside") || meshBase.name.Contains("MoonPlatform") || meshBase.name.Equals("ramp up") || meshBase.name.Equals("railing") || (meshBase.name.Contains("Quarry") && !meshBase.name.Contains("Fog") && !meshBase.name.Contains("Rock")) || meshBase.name.Contains("Connector") || meshBase.name.Contains("Octagon") || meshBase.name.Contains("Temple") || meshBase.name.Contains("Column") || meshBase.name.Contains("CoolingBowlLarge") || (meshBase.name.Contains("Wall") && !meshBase.name.Contains("CylinderWall") && !meshBase.name.Contains("Godray")) || meshBase.name.Contains("Floor") || meshBase.name.Contains("Barrier") || meshBase.name.Contains("WaterTrough") || meshBase.name.Contains("Busted") || meshBase.name.Contains("disc") || meshBase.name == "Base") && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat2;
                        if ((meshBase.name.Contains("mdlroot") || meshBase.name.Contains("Stib")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat3;
                    }
                }
            }
        }

    }
}
