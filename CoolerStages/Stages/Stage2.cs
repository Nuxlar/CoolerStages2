using UnityEngine;

namespace CoolerStages
{
    public class Stage2
    {

        public static void Wetland(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3)
        {
            if (terrainMat && detailMat && detailMat2 && detailMat3)
            {
                MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
                foreach (MeshRenderer renderer in meshList)
                {
                    GameObject meshBase = renderer.gameObject;
                    if (meshBase != null)
                    {
                        if ((meshBase.name.Contains("Boulder") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Blender") || meshBase.name.Contains("Trunk")) && renderer.sharedMaterial)
                        {
                            renderer.sharedMaterial = detailMat;
                            if (meshBase.transform.GetComponentInChildren<MeshRenderer>())
                                meshBase.transform.GetComponentInChildren<MeshRenderer>().sharedMaterial = detailMat;
                        }

                        if (meshBase.name.Contains("Floor") || meshBase.name.Contains("Terrain"))
                            renderer.sharedMaterial = terrainMat;

                        Transform meshParent = meshBase.transform.parent;
                        if (meshParent != null)
                        {
                            if (meshBase.name.Contains("Mesh") && meshParent.name.Contains("Wall"))
                                renderer.sharedMaterial = terrainMat;
                            if (meshBase.name.Contains("Mesh") && (meshParent.name.Contains("FSTree") || meshParent.name.Contains("FSRootBundle")))
                                renderer.sharedMaterial = detailMat3;
                            if (meshBase.name.Contains("Mesh") && meshParent.name.Contains("FSRuinPillar"))
                                renderer.sharedMaterial = detailMat2;
                            if ((meshBase.name.Contains("RootBundleLargeCards") || meshBase.name.Contains("RootBundleSmallCards")) && (meshParent.name.Contains("FSRootBundleLarge") || meshParent.name.Contains("FSRootBundleSmall")))
                                meshBase.gameObject.SetActive(false);
                            if ((meshBase.name.Contains("RootBundleLarge_LOD0") || meshBase.name.Contains("RootBundleLarge_LOD1") || meshBase.name.Contains("RootBundleLarge_LOD2") || meshBase.name.Contains("RootBundleSmall_LOD0") || meshBase.name.Contains("RootBundleSmall_LOD1") || meshBase.name.Contains("RootBundleSmall_LOD2")) && (meshParent.name.Contains("FSRootBundleLarge") || meshParent.name.Contains("FSRootBundleSmall")))
                                renderer.sharedMaterial = detailMat3;
                        }
                        if ((meshBase.name.Contains("Door") || meshBase.name.Contains("Frame")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat2;
                        if (meshBase.name.Contains("Ruin") && meshBase.name != "FSGiantRuinDoorCollision" && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat2;
                    }
                }
            }
        }

        public static void Aqueduct(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3)
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
                        if (meshBase.name.Contains("Terrain") && renderer.sharedMaterial)
                            renderer.sharedMaterial = terrainMat;
                        if (meshBase.name.Contains("SandDune") && renderer.sharedMaterial)
                            renderer.sharedMaterial = terrainMat;
                        if ((meshBase.name.Contains("SandstonePillar") || meshBase.name.Contains("Dam") || meshBase.name.Contains("AqueductFullLong") || meshBase.name.Contains("AqueductPartial") || meshBase.name.Contains("RuinGate")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat2;
                        if ((meshBase.name.Contains("Bridge") && !meshBase.name.Contains("Decal") || meshBase.name.Contains("RuinedRing") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("Eel")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat;
                        if ((meshBase.name.Contains("FlagPoleMesh") || meshBase.name.Contains("RuinTile")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat3;
                        if (meshBase.name.Contains("AqueductCap"))
                        {
                            try
                            {
                                Material[] sharedMaterials = renderer.sharedMaterials;
                                for (int i = 0; i < sharedMaterials.Length; i++)
                                    sharedMaterials[i] = detailMat2;
                                renderer.sharedMaterials = sharedMaterials;
                            }
                            catch (System.Exception e) { Debug.LogWarning(e.Message + "\n" + e.StackTrace); }
                            ;
                        }
                    }
                }
            }
        }

        public static void Aphelian(Material terrainMat, Material detailMat, Material detailMat2, Material detailMat3)
        {
            MeshRenderer[] meshList = Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
            SkinnedMeshRenderer[] sMeshList = Object.FindObjectsOfType(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer[];

            if (terrainMat && detailMat && detailMat2 && detailMat3)
            {
                foreach (MeshRenderer renderer in meshList)
                {
                    GameObject meshBase = renderer.gameObject;
                    Transform meshParent = meshBase.transform.parent;
                    if (meshBase != null)
                    {
                        if (meshParent != null)
                        {
                            if ((meshParent.name.Contains("TempleTop") && meshBase.name.Contains("RuinBlock") || meshBase.name.Contains("GPRuinBlockQuarter")) && renderer.sharedMaterial)
                                renderer.sharedMaterial = detailMat2;
                        }
                        if (meshBase.name.Contains("Grass") && renderer.sharedMaterial)
                        {
                            GameObject.Destroy(meshBase);
                        }
                        if (meshBase.name.Contains("CirclePot"))
                            renderer.sharedMaterials = new Material[3] { terrainMat, detailMat3, terrainMat };
                        if (meshBase.name.Equals("Terrain") && renderer.sharedMaterials.Length > 0)
                            renderer.sharedMaterials = new Material[] { terrainMat, terrainMat, terrainMat };
                        if ((meshBase.name.Contains("Terrain") && !meshBase.name.Equals("Terrain") || meshBase.name.Contains("Dirt") || meshBase.name.Contains("TerrainPlatform")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = terrainMat;
                        if ((meshBase.name.Contains("Platform") || meshBase.name.Contains("Temple") || meshBase.name.Contains("Bridge")) && renderer.sharedMaterial)
                            renderer.sharedMaterials = new Material[] { terrainMat, terrainMat };
                        bool biggerProps = meshBase.name.Contains("BrokenPot") || meshBase.name.Contains("Planter") || meshBase.name.Contains("AW_Cube") || meshBase.name.Contains("Mesh, Cube") || meshBase.name.Contains("AncientLoft_WaterFenceType") || meshBase.name.Contains("Pillar") || meshBase.name.Equals("LightStatue") || meshBase.name.Equals("FountainLG") || meshBase.name.Equals("Shrine") || meshBase.name.Equals("Sculpture");
                        if ((biggerProps || meshBase.name.Contains("AncientLoft_SculptureSM") || meshBase.name.Contains("FountainSM")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat3;
                        if ((meshBase.name.Contains("Tile") || meshBase.name.Contains("Step") || meshBase.name.Contains("Rock") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rubble") || meshBase.name.Contains("Boulder") || meshBase.name.Equals("LightStatue_Stone")) && renderer.sharedMaterial)
                            renderer.sharedMaterial = detailMat;
                        if (meshBase.name.Contains("CircleArchwayAnimatedMesh"))
                        {
                            Material[] sharedMaterials = renderer.sharedMaterials;
                            for (int i = 0; i < renderer.sharedMaterials.Length; i++)
                            {
                                sharedMaterials[i] = detailMat2;
                                if (i == 1)
                                    sharedMaterials[i] = detailMat3;
                            }
                            renderer.sharedMaterials = sharedMaterials;
                        }
                    }
                }
                foreach (SkinnedMeshRenderer sRenderer in sMeshList)
                {
                    GameObject meshBase = sRenderer.gameObject;
                    if (meshBase != null)
                    {
                        bool biggerProps = meshBase.name.Contains("CirclePot") || meshBase.name.Contains("Planter") || meshBase.name.Contains("AW_Cube") || meshBase.name.Contains("Mesh, Cube") || meshBase.name.Contains("AncientLoft_WaterFenceType") || meshBase.name.Contains("Pillar") || meshBase.name.Equals("LightStatue") || meshBase.name.Equals("LightStatue_Stone") || meshBase.name.Equals("FountainLG") || meshBase.name.Equals("Shrine") || meshBase.name.Equals("Sculpture");
                        if (biggerProps)
                            sRenderer.sharedMaterial = detailMat3;
                        if ((meshBase.name.Contains("Rock") || meshBase.name.Contains("Tile") || meshBase.name.Contains("Boulder") || meshBase.name.Contains("Step") || meshBase.name.Contains("Pebble") || meshBase.name.Contains("Rubble")) && sRenderer.sharedMaterial)
                            sRenderer.sharedMaterial = detailMat;
                    }
                }
            }
        }
    }
}
