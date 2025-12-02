using UnityEngine;

namespace CoolerStages
{
    public class Stage3
    {

        public static void RPD(Material terrainMat, Material detailMat, Material detailMat2)
        {
            if (terrainMat && detailMat && detailMat2)
            {
                MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer renderer in meshList)
                {
                    GameObject meshBase = renderer.gameObject;
                    if (meshBase != null)
                    {
                        if ((meshBase.name.Contains("Terrain") || meshBase.name.Contains("Snow")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = terrainMat;
                        if ((meshBase.name.Contains("Glacier") || meshBase.name.Contains("Stalagmite") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("CavePillar")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat;
                        if ((meshBase.name.Contains("GroundMesh") || meshBase.name.Contains("GroundStairs") || meshBase.name.Contains("VerticalPillar") || meshBase.name.Contains("Barrier") || (meshBase.name.Contains("Human") && !meshBase.name.Contains("ChainLink"))) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat2;
                    }
                }
            }
        }

        public static void Acres(Material terrainMat, Material detailMat, Material detailMat2)
        {
            if (terrainMat && detailMat && detailMat2)
            {
                MeshRenderer[] meshList = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer renderer in meshList)
                {
                    GameObject meshBase = renderer.gameObject;
                    if (meshBase != null)
                    {
                        if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
                        {
                            GameObject.Destroy(meshBase);
                        }
                        if ((meshBase.name.Contains("Terrain") || meshBase.name.Contains("Dirt")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = terrainMat;
                        if ((meshBase.name.Contains("Rock") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("Crystal")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat;
                        if ((meshBase.name.Contains("Ring") || meshBase.name.Contains("Bridge") || meshBase.name.Contains("Platform") || meshBase.name.Contains("Planter")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat2;
                    }
                }
            }
        }

        public static void Pools(Material terrainMat, Material detailMat, Material detailMat2)
        {
            if (terrainMat && detailMat && detailMat2)
            {
                MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer renderer in meshList)
                {
                    var meshBase = renderer.gameObject;
                    if (meshBase != null)
                    {
                        if (((meshBase.name.Contains("Terrain") && !meshBase.name.Contains("Fog")) || meshBase.name.Contains("Mountain")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = terrainMat;
                        if ((meshBase.name.Contains("meshSPSphere") || meshBase.name.Contains("SPHeatVent") || meshBase.name.Contains("Crystal") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Eel")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat;
                        if ((meshBase.name.Contains("Dome") || meshBase.name.Contains("Spikes") || meshBase.name.Contains("Cave")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat2;
                    }
                }
            }
        }

    }
}
