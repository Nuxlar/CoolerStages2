using UnityEngine;

namespace CoolerStages
{
    public class Stage4
    {

        public static void Grove(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3)
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
                        if ((meshBase.name.Contains("Terrain") || meshBase.name.Contains("Rock Holder") || meshBase.name.Contains("Vine Tree") || meshBase.name.Contains("Gianticus") || meshBase.name.Contains("Tree Big Bottom") || meshBase.name.Contains("Root Bridge") || meshBase.name.Contains("Tree D") || meshBase.name.Contains("Wall") || meshBase.name.Contains("RJRoot") || meshBase.name.Contains("RJShroomShelf")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = terrainMat;
                        if ((meshBase.name.Contains("RJTriangle") || meshBase.name.Contains("BbRuinArch")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat2;
                        if (((meshBase.name.Contains("Rock") && !meshBase.name.Contains("Holder")) || meshBase.name.Contains("Boulder") || meshBase.name.Contains("Pebble")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat;
                        if ((meshBase.name.Contains("Moss Cover") || meshBase.name.Contains("RJShroomShelf") || meshBase.name.Contains("RJShroomBig") || meshBase.name.Contains("RJShroomSmall") || meshBase.name.Contains("RJMossPatch")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat3;
                    }
                }
            }

        }

        public static void Sirens(Material terrainMat, Material detailMat, Material detailMat2)
        {
            if (terrainMat && detailMat && detailMat2)
            {
                MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer renderer in meshList)
                {
                    GameObject meshBase = renderer.gameObject;
                    Transform meshParent = meshBase.transform.parent;
                    if (meshBase != null)
                    {
                        if (meshParent != null)
                        {
                            if ((meshParent.name.Contains("Terrain") || meshBase.name.Contains("Mesh") || meshBase.name.Contains("Spikes")) && renderer.sharedMaterial)
                                renderer.sharedMaterial = terrainMat;
                            if ((meshBase.name.Contains("Stalactite") || meshBase.name.Contains("Stalagmite")) && (meshParent.name.Contains("Cave") || meshParent.name.Contains("Stalagmite")) && renderer.sharedMaterial)
                                renderer.sharedMaterial = detailMat;
                        }
                        if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
                        {
                            GameObject.Destroy(meshBase);
                        }
                        if ((meshBase.name.Contains("Terrain") || meshBase.name.Contains("Stalactite") || meshBase.name.Contains("Level Wall") || meshBase.name.Contains("Cave") || meshBase.name.Contains("Floor") || meshBase.name.Contains("Spikes")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = terrainMat;
                        if ((meshBase.name.Contains("Ship") || meshBase.name.Contains("Stalagmite") || meshBase.name.Contains("mdlGeyser")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat2;
                        if ((meshBase.name.Contains("Rock") || meshBase.name.Contains("Boulder")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat;
                    }
                }
            }
        }
        public static void Abyssal(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3)
        {
            if (terrainMat && detailMat && detailMat2 && detailMat3)
            {
                MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer renderer in meshList)
                {
                    GameObject meshBase = renderer.gameObject;
                    Transform meshParent = meshBase.transform.parent;
                    if (meshBase != null)
                    {
                        if (meshParent != null)
                        {
                            if (meshBase.name.Contains("Mesh") && meshParent.name.Contains("Ruin") && renderer.sharedMaterial)
                                renderer.sharedMaterial = detailMat3;
                            if (meshBase.name.Contains("RuinBowl") && meshParent.name.Contains("RuinMarker") && renderer.sharedMaterial)
                                renderer.sharedMaterial = detailMat3;
                        }
                        if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
                        {
                            GameObject.Destroy(meshBase);
                        }
                        if ((meshBase.name.Contains("Hero") || meshBase.name.Contains("Ceiling") || meshBase.name.Contains("Column")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = terrainMat;
                        if ((meshBase.name.Contains("Boulder") || meshBase.name.Contains("mdlGeyser") || meshBase.name.Contains("Pebble") || (meshBase.name.Contains("GiantRock") && !meshBase.name.Contains("Slab"))) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat;
                        if (meshBase.name.Contains("Ruin") && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat3;
                        if (meshBase.name.Contains("LightMesh") && renderer.sharedMaterial)
                        {
                            if (meshBase.transform.childCount >= 1 && meshBase.transform.GetChild(0).name.Contains("Crystal"))
                                meshBase.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = detailMat2;
                        }
                        if ((meshBase.name.Contains("Spike") || meshBase.name.Contains("Stalagmite") || meshBase.name.Contains("Coral") || meshBase.name.Contains("Crystal") || meshBase.name.Contains("Heatvent")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat2;
                        if ((meshBase.name.Contains("DCGiantRockSlab") || meshBase.name.Contains("GiantStoneSlab") || meshBase.name.Contains("TerrainBackwall") || meshBase.name.Contains("Chain") || meshBase.name.Contains("Wall")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = terrainMat;
                    }
                }
            }
        }
    }
}
