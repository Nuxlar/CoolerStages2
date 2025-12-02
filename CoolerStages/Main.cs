using BepInEx;
using BepInEx.Configuration;
using RoR2;
using RoR2.ContentManagement;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace CoolerStages
{
  [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
  public class Main : BaseUnityPlugin
  {
    public const string PluginGUID = PluginAuthor + "." + PluginName;
    public const string PluginAuthor = "Nuxlar";
    public const string PluginName = "CoolerStages";
    public const string PluginVersion = "2.0.0";

    internal static Main Instance { get; private set; }
    public static string PluginDirectory { get; private set; }

    private System.Random rng;

    private static readonly Material[] solusTheme = [];
    // Iron Alluvium matIADrillMachineLowEm matIAIron matIAProps matIAPipes matIATerrain
    // texIALandscapeWallDiffuse _RedChannelSideTex
    // _GreenChannelTex texRCConcreteTopDiffuse
    // Repurposed Crater matRCMetalTrim01 matRCPipes03
    // Pretender's Presipice matPPCablesEmissive matPPShipDebrisSnow matPPSolusSpheresPanels matPPTerrain matPPTree
    // Conduit Canyon matCCTerrain_REDROCK matCCMetal
    // Solutional Haunt matSHTerrain matSHMetal01A matSHMetalTEMP matSHMetal matSHConcreteWall matSHConcreteFloor
    // Computational Exchange matCEVoxelPath matCEVoxelObject 
    // Prime Meridian matPMTerrainPlayZone matPMTerrainPlayZoneWall matPMGold matPMTerrainPlayZoneDark
    private static readonly Material nightTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/wispgraveyard/matWPTerrain.mat").WaitForCompletion();
    private static readonly Material nightTerrainMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/wispgraveyard/matWPTerrainRocky.mat").WaitForCompletion();
    private static readonly Material nightDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/wispgraveyard/matTempleObelisk.mat").WaitForCompletion();
    private static readonly Material nightDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimsheetGraveyardTempleWhiteGrassy.mat").WaitForCompletion();
    private static readonly Material nightDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/foggyswamp/matFSTreeTrunkLightMoss.mat").WaitForCompletion();

    private static readonly Material danTerrain = Addressables.LoadAssetAsync<Material>("RoR2/Base/arena/matArenaTerrainVerySnowy.mat").WaitForCompletion();
    private static readonly Material danDetail = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidFoam.mat").WaitForCompletion();
    private static readonly Material danDetail2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/TrimSheets/matTrimSheetAlien1BossEmission.mat").WaitForCompletion();
    private static readonly Material danDetail3 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidTrim.mat").WaitForCompletion();

    private static readonly Material ruinTerrainTest = new Material(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itancientloft/matAncientLoft_TempleProjectedInfiniteTower.mat").WaitForCompletion());
    private static readonly Material ruinTerrain = new Material(Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itancientloft/matAncientLoft_TempleProjectedInfiniteTower.mat").WaitForCompletion());
    private static readonly Material ruinDetail = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itancientloft/matAncientLoft_BoulderInfiniteTower.mat").WaitForCompletion();
    private static readonly Material ruinDetail2 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itancientloft/matAncientLoft_TempleProjectedInfiniteTower.mat").WaitForCompletion();
    private static readonly Material ruinDetail3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/rootjungle/matRJTree.mat").WaitForCompletion();

    private static readonly Material smSimTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itskymeadow/matSMTerrainInfiniteTower.mat").WaitForCompletion();
    private static readonly Material smSimDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itskymeadow/matSMRockInfiniteTower.mat").WaitForCompletion();
    private static readonly Material smSimDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itskymeadow/matTrimSheetMeadowRuinsProjectedInfiniteTower.mat").WaitForCompletion();
    private static readonly Material smSimtDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itskymeadow/matSMSpikeBridgeInfinitetower.mat").WaitForCompletion();

    private static readonly Material dcSimTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itdampcave/matDCTerrainFloorInfiniteTower.mat").WaitForCompletion();
    private static readonly Material dcSimDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itdampcave/matDCBoulderInfiniteTower.mat").WaitForCompletion();
    private static readonly Material dcSimDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itdampcave/matTrimSheetLemurianRuinsLightInfiniteTower.mat").WaitForCompletion();
    private static readonly Material dcSimDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itdampcave/matDCStalagmiteInfiniteTower.mat").WaitForCompletion();

    private static readonly Material gpSimTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itgolemplains/matGPTerrainInfiniteTower.mat").WaitForCompletion();
    private static readonly Material gpSimDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itgolemplains/matGPBoulderMossyProjectedInfiniteTower.mat").WaitForCompletion();
    private static readonly Material gpSimDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itgolemplains/matTrimSheetMossyProjectedHugeInfiniteTower.mat").WaitForCompletion();
    private static readonly Material gpSimDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/foggyswamp/matFSTreeTrunkLightMoss.mat").WaitForCompletion();

    private static readonly Material gooSimTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itgoolake/matGoolakeTerrainInfiniteTower.mat").WaitForCompletion();
    private static readonly Material gooSimDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itgoolake/matGoolakeRocksInfiniteTower.mat").WaitForCompletion();
    private static readonly Material gooSimDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itgoolake/matGoolakeStoneTrimLightSandInfiniteTower.mat").WaitForCompletion();
    private static readonly Material gooSimDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/itgoolake/matEelSkeleton2InfiniteTower.mat").WaitForCompletion();

    private static readonly Material moonTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/moon/matMoonTerrain.mat").WaitForCompletion();
    private static readonly Material moonDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/moon/matMoonBoulder.mat").WaitForCompletion();
    private static readonly Material moonDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/moon/matMoonBridge.mat").WaitForCompletion();
    private static readonly Material moonDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/moon/matMoonBaseStandTriplanar.mat").WaitForCompletion();

    private static readonly Material bazaarTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/bazaar/matBazaarTerrainTriplanar.mat").WaitForCompletion();
    private static readonly Material bazaarDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/bazaar/matBazaarWoodSandy.mat").WaitForCompletion();
    private static readonly Material bazaarDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/Base/intro/matColonyShipStandard.mat").WaitForCompletion();
    private static readonly Material bazaarDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/Base/mysteryspace/matMSObeliskHeart.mat").WaitForCompletion();

    private static readonly Material verdantTerrainMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC2/lakes/Assets/matTLTerrainStone.mat").WaitForCompletion();
    private static readonly Material verdantDetailMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC2/lakes/Assets/matTLRocks.mat").WaitForCompletion();
    private static readonly Material verdantDetailMat2 = Addressables.LoadAssetAsync<Material>("RoR2/DLC2/lakes/Assets/matTLShip.mat").WaitForCompletion();
    private static readonly Material verdantDetailMat3 = Addressables.LoadAssetAsync<Material>("RoR2/DLC2/lakes/Assets/matTLGVine.mat").WaitForCompletion();

    private static readonly Material skybox1 = Addressables.LoadAssetAsync<Material>("RoR2/Junk/slice1/matSkybox2.mat").WaitForCompletion();
    private static readonly Material skybox3 = Addressables.LoadAssetAsync<Material>("RoR2/DLC2/villagenight/Assets/matLVnightSkybox.mat").WaitForCompletion();
    private static readonly Material skybox4 = Addressables.LoadAssetAsync<Material>("RoR2/DLC2/village/Assets/matLVSkybox.mat").WaitForCompletion();
    private static readonly Material skybox7 = Addressables.LoadAssetAsync<Material>("RoR2/DLC2/lakes/Assets/matTLGFlowerSkybox.mat").WaitForCompletion();
    private static readonly Material skybox8 = Addressables.LoadAssetAsync<Material>("RoR2/DLC2/habitatfall/Assets/matBHFallSkybox.mat").WaitForCompletion();
    private static readonly Material skybox11 = Addressables.LoadAssetAsync<Material>("RoR2/DLC2/meridian/matSkyboxPMActivate.mat").WaitForCompletion();
    private static readonly Material skybox13 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/voidstage/matVoidStageSkyboxSphere, Scrolling Caustics.mat").WaitForCompletion();
    private static readonly Material skybox14 = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/ancientloft/matSkyboxAncientLoft.mat").WaitForCompletion();

    private static Material terrainMatTesting;
    private static Material detailMatTesting;
    private static Material detailMat2Testing;
    private static Material detailMat3Testing;

    private static readonly List<Material> skyboxMats = new List<Material>()
    {
      skybox1,
      skybox3,
      skybox4,
      skybox7,
      skybox8,
      skybox11,
      skybox13,
      skybox14,
    };
    /*
        private static readonly List<Material[]> themeMaterials1 = new List<Material[]> {
          new Material[] { verdantTerrainMat, verdantDetailMat, verdantDetailMat2, verdantDetailMat3},
          new Material[] { smSimTerrainMat, smSimDetailMat, smSimDetailMat2, smSimtDetailMat3},
          new Material[] {danTerrain, danDetail, danDetail2, danDetail3 },
          new Material[] { ruinTerrain, ruinDetail, ruinDetail2, ruinDetail3 },
          new Material[] {dcSimTerrainMat, dcSimDetailMat, dcSimDetailMat2, dcSimDetailMat3 },
          new Material[] {gpSimTerrainMat, gpSimDetailMat, gpSimDetailMat2, gpSimDetailMat3 },
          new Material[] {gooSimTerrainMat, gooSimDetailMat, gooSimDetailMat2, gooSimDetailMat3 },
          new Material[] {moonTerrainMat, moonDetailMat, moonDetailMat2, moonDetailMat3 },
          new Material[] {bazaarTerrainMat, bazaarDetailMat, bazaarDetailMat2, bazaarDetailMat3 },
        };

        private static readonly List<Material[]> themeMaterials2 = new List<Material[]> {
          new Material[] {verdantTerrainMat, verdantDetailMat, verdantDetailMat2, verdantDetailMat3 },
          new Material[] {danTerrain, danDetail, danDetail2, danDetail3 },
          new Material[] {ruinTerrain, ruinDetail, ruinDetail2, ruinDetail3 },
          new Material[] {dcSimTerrainMat, dcSimDetailMat, dcSimDetailMat2, dcSimDetailMat3 }
        };
    */

    private static readonly string[] whitelistedMaps = new string[] {
      "lakes",
      "lakesnight",
      "snowyforest",
      "blackbeach",
      "blackbeach2",
      "golemplains",
      "golemplains2",
      "goolake",
      "foggyswamp",
      "ancientloft",
      "frozenwall",
      "wispgraveyard",
      "sulfurpools",
      "shipgraveyard",
      "rootjungle",
      "dampcavesimple",
      "skymeadow",
      "moon2",
      "village",
      "villagenight"
      //"helminthroost"
};

    public static ConfigEntry<bool> enableWinter;
    public static ConfigEntry<bool> enableFantasy;
    public static ConfigEntry<bool> enableAuburn;
    public static ConfigEntry<bool> enableAfternoon;
    public static ConfigEntry<bool> enableDrowned;
    public static ConfigEntry<bool> enableDreary;
    private static ConfigFile CSConfig { get; set; }

    /*
      Ancient Loft
      Temple Material
      _BlueChannelTex RoR2/DLC1/ancientloft/texAncientLoft_LichenGreenLighter.tga
      _GreenChannelTex RoR2/DLC1/ancientloft/texAncientLoft_LichenGreenOvergrownGroundStone.tga
      _NormalTex RoR2/Base/Common/texNormalTiles.jpg
      _SplatmapTex RoR2/DLC1/ancientloft/texAncientLoft_splat.png
      _RedChannelSideTex RoR2/DLC1/ancientloft/texAncientLoft_BaseWhiteBrick.png
      _RedChannelTopTex RoR2/DLC1/ancientloft/texAncientLoft_BaseWhiteBrick.png
    */

    private Texture2D tlCliffTex = Addressables.LoadAssetAsync<Texture2D>("RoR2/DLC2/lakes/Assets/texTLTerrainCliff.tga").WaitForCompletion();
    private Texture2D tlDirtTex = Addressables.LoadAssetAsync<Texture2D>("RoR2/DLC2/lakes/Assets/texTLTerrainDirt.tga").WaitForCompletion();
    private Texture2D rockNormal = Addressables.LoadAssetAsync<Texture2D>("RoR2/Base/Common/texNormalBumpyRock.jpg").WaitForCompletion();

    PostProcessProfile pp1 = ScriptableObject.CreateInstance<PostProcessProfile>();
    PostProcessProfile pp2 = ScriptableObject.CreateInstance<PostProcessProfile>();
    PostProcessProfile pp3 = ScriptableObject.CreateInstance<PostProcessProfile>();
    PostProcessProfile pp4 = ScriptableObject.CreateInstance<PostProcessProfile>();
    PostProcessProfile pp5 = ScriptableObject.CreateInstance<PostProcessProfile>();
    PostProcessProfile pp6 = ScriptableObject.CreateInstance<PostProcessProfile>();
    PostProcessProfile ppbb = Addressables.LoadAssetAsync<PostProcessProfile>("RoR2/Base/title/PostProcessing/ppSceneGolemplainsFoggy.asset").WaitForCompletion();

    public void Awake()
    {
      Instance = this;

      Stopwatch stopwatch = Stopwatch.StartNew();

      Log.Init(Logger);
      LoadAssets();

      rng = new System.Random();

      ruinTerrain.color = new Color(0.701f, 0.623f, 0.403f, 1);
      ruinTerrain.SetTexture("_SplatmapTex", null);
      ruinTerrain.SetTexture("_NormalTex", rockNormal);
      ruinTerrain.SetTexture("_RedChannelSideTex", tlCliffTex);
      ruinTerrain.SetTexture("_RedChannelTopTex", tlDirtTex);

      ruinTerrainTest.color = Color.white;
      // BINARYBLEND DOUBLESAMPLE MIX_VERTEX_COLORS USE_ALPHA_AS_MASK USE_VERTEX_COLORS
      // DOUBLESAMPLE USE_ALPHA_AS_MASK USE_VERTICAL_BIAS
      // ruinTerrainTest.shaderKeywords = new string[] { "BINARYBLEND", "DOUBLESAMPLE", "MIX_VERTEX_COLORS", "USE_ALPHA_AS_MASK", "USE_VERTEX_COLORS", "USE_VERTICAL_BIAS" };
      /*
      ruinTerrainTest.SetTexture("_SplatmapTex", null);
      ruinTerrainTest.SetTexture("_NormalTex", bbNormalTex);
      ruinTerrainTest.SetTexture("_BlueChannelTex", gpBlueTex);
      ruinTerrainTest.SetTexture("_GreenChannelTex", gpGreenTex);
      ruinTerrainTest.SetTexture("_RedChannelSideTex", tlCliffTex);
      ruinTerrainTest.SetTexture("_RedChannelTopTex", bbRedTex);
      */
      pp1.name = "Winter";
      pp2.name = "Fantasy";
      pp3.name = "Auburn";
      pp4.name = "Afternoon";
      pp5.name = "Drowned";
      pp6.name = "Dreary";

      RampFog rf1 = pp1.AddSettings<RampFog>();
      RampFog rf2 = pp2.AddSettings<RampFog>();
      RampFog rf3 = pp3.AddSettings<RampFog>();
      RampFog rf4 = pp4.AddSettings<RampFog>();
      RampFog rf5 = pp5.AddSettings<RampFog>();
      RampFog rf6 = pp6.AddSettings<RampFog>();
      RampFog rfbb = ppbb.GetSetting<RampFog>();

      rf1.SetAllOverridesTo(true);
      rf2.SetAllOverridesTo(true);
      rf3.SetAllOverridesTo(true);
      rf4.SetAllOverridesTo(true);
      rf5.SetAllOverridesTo(true);
      rf6.SetAllOverridesTo(true);
      /*
           fog.fogColorStart.value = new Color32(127, 127, 153, 25);
            fog.fogColorMid.value = new Color32(0, 106, 145, 150);
            fog.fogColorEnd.value = new Color32(0, 115, 119, 255);
            fog.fogZero.value = -0.01f;
            fog.fogOne.value = 0.15f;
            fog.fogPower.value = 2f;
            fog.skyboxStrength.value = 0.1f;
      */
      // Winter
      rf1.fogColorStart.value = new Color32(225, 225, 225, 15);
      rf1.fogColorMid.value = new Color32(160, 207, 255, 150);
      rf1.fogColorEnd.value = new Color32(135, 150, 200, 255);
      rf1.fogHeightStart.value = 0;
      rf1.fogHeightEnd.value = 100;
      rf1.fogHeightIntensity.value = 0;
      rf1.fogIntensity.value = 0.75f;
      rf1.fogOne.value = 0.15f;
      rf1.fogPower.value = 1.5f;
      rf1.fogZero.value = -0.01f;
      rf1.skyboxStrength.value = 0.1f;
      // Fantasy
      rf2.fogColorStart.value = new Color32(229, 164, 203, 10);
      rf2.fogColorMid.value = new Color32(145, 120, 120, 125);
      rf2.fogColorEnd.value = new Color32(120, 75, 100, 225);
      rf2.fogHeightStart.value = 0;
      rf2.fogHeightEnd.value = 100;
      rf2.fogHeightIntensity.value = 0;
      rf2.fogIntensity.value = 1f;
      rf2.fogOne.value = 0.15f;
      rf2.fogPower.value = 2f;
      rf2.fogZero.value = -0.01f;
      rf2.skyboxStrength.value = 0.1f;
      // Auburn
      rf3.fogColorStart.value = new Color32(190, 154, 150, 15); // 249,210,200
      rf3.fogColorMid.value = new Color32(110, 73, 69, 100); // 204,139,134
      rf3.fogColorEnd.value = new Color32(90, 47, 44, 200); // 125,79,80
      rf3.fogHeightStart.value = 0;
      rf3.fogHeightEnd.value = 100;
      rf3.fogHeightIntensity.value = 0;
      rf3.fogIntensity.value = 1f;
      rf3.fogOne.value = 0.15f;
      rf3.fogPower.value = 2;
      rf3.fogZero.value = -0.01f;
      rf3.skyboxStrength.value = 0f;
      // Afternoon
      rf4.fogColorStart.value = new Color32(235, 235, 205, 25);
      rf4.fogColorMid.value = new Color32(252, 253, 196, 150);
      rf4.fogColorEnd.value = new Color32(14, 212, 255, 225);
      rf4.fogHeightStart.value = 0;
      rf4.fogHeightEnd.value = 100;
      rf4.fogHeightIntensity.value = 0;
      rf4.fogIntensity.value = 0.5f;
      rf4.fogOne.value = 0.4f;
      rf4.fogPower.value = 2f;
      rf4.fogZero.value = 0f;
      rf4.skyboxStrength.value = 0.1f;
      // Placeholder
      rf5.fogColorStart.value = new Color32(171, 151, 191, 25);
      rf5.fogColorMid.value = new Color32(120, 120, 175, 150);
      rf5.fogColorEnd.value = new Color32(100, 75, 150, 225);
      rf5.fogHeightStart.value = 0;
      rf5.fogHeightEnd.value = 100;
      rf5.fogHeightIntensity.value = 0;
      rf5.fogIntensity.value = 0.9f;
      rf5.fogOne.value = 0.15f;
      rf5.fogPower.value = 1.75f;
      rf5.fogZero.value = -0.01f;
      rf5.skyboxStrength.value = 0.1f;
      // Midnight Dreary
      rf6.fogColorStart.value = new Color32(129, 148, 168, 25);
      rf6.fogColorMid.value = new Color32(68, 104, 116, 150);
      rf6.fogColorEnd.value = new Color32(55, 63, 81, 255);
      rf6.fogHeightStart.value = 0;
      rf6.fogHeightEnd.value = 100;
      rf6.fogHeightIntensity.value = 0;
      rf6.fogIntensity.value = 1f;
      rf6.fogOne.value = 0.15f;
      rf6.fogPower.value = 1.75f;
      rf6.fogZero.value = 0f;
      rf6.skyboxStrength.value = 0.1f;

      On.RoR2.SceneDirector.Start += CoolifyStage;
    }

    private void CoolifyStage(On.RoR2.SceneDirector.orig_Start orig, SceneDirector self)
    {

      int seed = (int)(RoR2.Run.instance.GetStartTimeUtc().Ticks ^ (long)(RoR2.Run.instance.stageClearCount << 16));
      rng = new System.Random(seed);
      string sceneName = SceneManager.GetActiveScene().name;
      SceneInfo currentScene = SceneInfo.instance;
      if (currentScene && whitelistedMaps.Contains(sceneName))
      {
        var themeMaterials1 = new List<Material[]>
{
    new Material[] {terrainMatTesting, detailMatTesting, detailMat2Testing, detailMat3Testing}
};
        var themeMaterials2 = new List<Material[]>
{
    new Material[] {terrainMatTesting, detailMatTesting, detailMat2Testing, detailMat3Testing}
};
        Log.Warning("In whitelisted Map");
        int idx5 = rng.Next(skyboxMats.Count);
        UnityEngine.Debug.LogWarning("Skybox name: " + skyboxMats[idx5].name);
        RenderSettings.skybox = skyboxMats[idx5];
        PostProcessVolume volume = currentScene.GetComponent<PostProcessVolume>();
        if (!volume || !volume.isActiveAndEnabled)
        {
          GameObject alt = GameObject.Find("PP + Amb");
          if (!alt || (!alt?.GetComponent<PostProcessVolume>()?.isActiveAndEnabled ?? true))
            alt = GameObject.Find("PP, Global");
          if (!alt || (!alt?.GetComponent<PostProcessVolume>()?.isActiveAndEnabled ?? true))
            alt = GameObject.Find("GlobalPostProcessVolume, Base");
          if (!alt || (!alt?.GetComponent<PostProcessVolume>()?.isActiveAndEnabled ?? true))
            alt = GameObject.Find("PP+Amb");
          if (!alt || (!alt?.GetComponent<PostProcessVolume>()?.isActiveAndEnabled ?? true))
            alt = GameObject.Find("MapZones")?.transform?.Find("PostProcess Zones")?.Find("SandOvercast")?.gameObject;
          if (alt)
            volume = alt.GetComponent<PostProcessVolume>();
          if (sceneName == "moon2")
          {
            volume = currentScene.gameObject.AddComponent<PostProcessVolume>();
            volume.profile.AddSettings<RampFog>();

            volume.enabled = true;
            volume.isGlobal = true;
            volume.priority = 9999f;
          }
        }
        if (volume)
        {

          int idx = rng.Next(themeMaterials1.Count);
          Material[] themeMaterials = themeMaterials1[0];
          int idx2 = rng.Next(themeMaterials2.Count);
          Material[] themeMaterialsAlt = themeMaterials2[0];

          Material testTerrainMat = themeMaterials[0];
          Material testDetailMat = themeMaterials[1];
          Material testDetailMat2 = themeMaterials[2];
          Material testDetailMat3 = themeMaterials[3];

          Material testTerrainMatAlt = themeMaterialsAlt[0];
          Material testDetailMatAlt = themeMaterialsAlt[1];
          Material testDetailMat2Alt = themeMaterialsAlt[2];
          Material testDetailMat3Alt = themeMaterialsAlt[3];

          List<PostProcessProfile> profiles = new List<PostProcessProfile>();

          // if (enableWinter.Value)
          profiles.Add(pp1);
          //   if (enableFantasy.Value)
          profiles.Add(pp2);
          //  if (enableAuburn.Value)
          profiles.Add(pp3);
          // if (enableAfternoon.Value)
          profiles.Add(pp4);
          //  if (enableDrowned.Value)
          profiles.Add(pp5);
          //  if (enableDreary.Value)
          profiles.Add(pp6);

          int idx7 = rng.Next(profiles.Count);
          PostProcessProfile testProfile = profiles[idx7];

          if (sceneName != "moon2")
            volume.profile = testProfile;

          RampFog testFog = testProfile.GetSetting<RampFog>();
          GameObject sun = GameObject.Find("Directional Light (SUN)");
          Color lightColor = Color.gray;
          if (sun)
          {
            Light mainLight = sun.GetComponent<Light>();
            if (mainLight)
            {
              if (testProfile.name == "Midnight Dreary")
                mainLight.color = new Color(testFog.fogColorStart.value.r + 0.1f, testFog.fogColorStart.value.g + 0.1f, testFog.fogColorStart.value.b + 0.1f, 1f);
              else
                mainLight.color = new Color(testFog.fogColorStart.value.r, testFog.fogColorStart.value.g, testFog.fogColorStart.value.b, 1f);
              if (sceneName == "lakes" || sceneName == "wispgraveyard" || sceneName == "ancientloft" || sceneName == "golemplains" || sceneName == "golemplains2" || sceneName == "goolake")
              {
                SetAmbientLight ambLight = volume.GetComponent<SetAmbientLight>();
                if (ambLight)
                {
                  ambLight.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
                  ambLight.ambientIntensity = 0.75f;
                  ambLight.ambientSkyColor = Color.gray;
                  ambLight.ApplyLighting();
                }
                lightColor = mainLight.color;
              }
              else
              {
                SetAmbientLight ambLight = mainLight.gameObject.AddComponent<SetAmbientLight>();
                if (ambLight)
                {
                  ambLight.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
                  ambLight.setAmbientLightColor = true;
                  ambLight.ambientIntensity = 0.75f;
                  ambLight.ambientSkyColor = Color.gray;
                  ambLight.ApplyLighting();
                }
              }
              mainLight.shadowStrength = 0.75f;

              bool isAnnoyingTerrain;
              if (sceneName == "foggyswamp" || sceneName == "sulfurpools" || sceneName == "shipgraveyard" || sceneName == "dampcavesimple")
              {
                isAnnoyingTerrain = testTerrainMatAlt.name.Contains("matDC");
              }
              else
              {
                isAnnoyingTerrain = testTerrainMat.name.Contains("matDC");
              }

              if (sceneName == "snowyforest" || sceneName == "skymeadow" || sceneName.Contains("blackbeach") || sceneName == "shipgraveyard")
                mainLight.intensity = 1.5f;
              else if (sceneName == "dampcavesimple" || isAnnoyingTerrain)
              {
                mainLight.intensity = 2f;
                mainLight.shadowStrength = 0.5f;
              }
              else
                mainLight.intensity = 1f;

              if (sceneName == "frozenwall")
                mainLight.intensity = 1f;

              if (testProfile.name == "Midnight Dreary")
              {
                mainLight.intensity *= 1.25f;
                mainLight.shadowStrength = 0.5f;
              }

              if (sceneName == "goolake" || sceneName == "frozenwall")
              {
                Light sunLight2 = GameObject.Instantiate(GameObject.Find("Directional Light (SUN)")).GetComponent<Light>();
                if (sunLight2)
                {
                  sunLight2.color = mainLight.color;
                  sunLight2.intensity = mainLight.intensity;
                  sun.SetActive(false);
                  sun.name = "Fake Sun";
                }
              }
            }


            if (sceneName == "foggyswamp" || sceneName == "sulfurpools" || sceneName == "shipgraveyard" || sceneName == "dampcavesimple")
            {
              Log.Warning($"Terrain Alt Material: {testTerrainMatAlt.name}");
              Log.Warning($"Detail Alt (Rocks) Material: {testDetailMatAlt.name}");
              Log.Warning($"Detail2 Alt (Ruins) Material: {testDetailMat2Alt.name}");
              Log.Warning($"Detail3 Alt (Trees) Material: {testDetailMat3Alt.name}");
              Log.Warning($"Post Process Profile : {testProfile.name}");
            }
            else
            {
              Log.Warning($"Terrain Material: {testTerrainMat.name}");
              Log.Warning($"Detail (Rocks) Material: {testDetailMat.name}");
              Log.Warning($"Detail2 (Ruins) Material: {testDetailMat2.name}");
              Log.Warning($"Detail3 (Trees) Material: {testDetailMat3.name}");
              Log.Warning($"Post Process Profile: {testProfile.name}");
            }

            switch (sceneName)
            {
              case "lakes":
                Stage1.Falls(testTerrainMatAlt, testDetailMatAlt, testDetailMat2Alt, testDetailMat3Alt, mainLight.color);
                break;
              case "lakesnight":
                Stage1.NightFalls(testTerrainMatAlt, testDetailMatAlt, testDetailMat2Alt, testDetailMat3Alt, mainLight.color);
                break;
              case "village":
                Stage1.Shattered(testTerrainMatAlt, testDetailMatAlt, testDetailMat2Alt, testDetailMat3);
                break;
              case "villagenight":
                Stage1.NightShattered(testTerrainMatAlt, testDetailMatAlt, testDetailMat2Alt, testDetailMat3);
                break;
              case "blackbeach":
                Stage1.Roost1(testTerrainMat, testDetailMat, testDetailMat2);
                break;
              case "blackbeach2":
                Stage1.Roost2(testTerrainMat, testDetailMat, testDetailMat2);
                break;
              case "golemplains":
                Stage1.Plains(testTerrainMat, testDetailMat, testDetailMat2);
                break;
              case "golemplains2":
                Stage1.Plains(testTerrainMat, testDetailMat, testDetailMat2);
                break;
              case "snowyforest":
                Stage1.Forest(testTerrainMat, testDetailMat, testDetailMat2, testDetailMat3);
                break;
              case "goolake":
                Stage2.Aqueduct(testTerrainMat, testDetailMat, testDetailMat2, testDetailMat3);
                break;
              case "ancientloft":
                GameObject deepFog = GameObject.Find("DeepFog");
                if (deepFog)
                  deepFog.SetActive(false);
                Stage2.Aphelian(testTerrainMat, testDetailMat, testDetailMat2, testDetailMat3);
                break;
              case "foggyswamp":
                Stage2.Wetland(testTerrainMatAlt, testDetailMatAlt, testDetailMat2Alt, testDetailMat3Alt);
                break;
              case "frozenwall":
                Stage3.RPD(testTerrainMat, testDetailMat, testDetailMat2);
                break;
              case "wispgraveyard":
                Stage3.Acres(testTerrainMat, testDetailMat, testDetailMat2);
                break;
              case "sulfurpools":
                GameObject.Find("SPCavePP").SetActive(false);
                GameObject.Find("CameraRelative").transform.GetChild(1).gameObject.SetActive(false);
                Stage3.Pools(testTerrainMatAlt, testDetailMatAlt, testDetailMat2Alt);
                break;
              case "rootjungle":
                Stage4.Grove(testTerrainMat, testDetailMat, testDetailMat2, testDetailMat3);
                break;
              case "shipgraveyard":
                Stage4.Sirens(testTerrainMatAlt, testDetailMatAlt, testDetailMat2Alt);
                break;
              case "dampcavesimple":
                GameObject camera = GameObject.Find("Main Camera(Clone)");
                if (camera)
                  camera.transform.GetChild(0).GetComponent<PostProcessLayer>().breakBeforeColorGrading = true;
                if (testTerrainMatAlt.name.Contains("Snowy"))
                  Stage4.Abyssal(testTerrainMatAlt, testDetailMat3Alt, testDetailMatAlt, testDetailMat2Alt);
                else
                  Stage4.Abyssal(testTerrainMatAlt, testDetailMatAlt, testDetailMat3Alt, testDetailMat2Alt);
                AbyssalLighting(lightColor);
                if (GameObject.Find("DCPPInTunnels"))
                  GameObject.Find("DCPPInTunnels").SetActive(false);
                break;
              case "helminthroost":
                Stage5.Roost(testTerrainMat, testDetailMat, testDetailMat3, testDetailMat2, mainLight.color);
                break;
              case "skymeadow":
                Stage5.SkyMeadow(testTerrainMat, testDetailMat, testDetailMat3, testDetailMat2, mainLight.color);
                break;
              case "moon2":
                try
                {
                  GameObject[] sceneobjs = SceneManager.GetActiveScene().GetRootGameObjects();
                  Transform es = sceneobjs[0].transform.GetChild(0);
                  es.GetChild(0).GetComponent<PostProcessVolume>().priority = 10001;
                  es.GetChild(6).GetComponent<PostProcessVolume>().weight = 0.47f;
                  es.GetChild(6).GetComponent<PostProcessVolume>().sharedProfile.settings[0].active = false;

                  RampFog fog = volume.sharedProfile.GetSetting<RampFog>();
                  fog.fogIntensity.value = testFog.fogIntensity.value;
                  fog.fogPower.value = testFog.fogPower.value;
                  fog.fogZero.value = testFog.fogZero.value;
                  fog.fogOne.value = testFog.fogOne.value;
                  fog.fogColorStart.value = testFog.fogColorStart.value;
                  fog.fogColorMid.value = testFog.fogColorMid.value;
                  fog.fogColorEnd.value = testFog.fogColorEnd.value;
                  fog.skyboxStrength.value = testFog.skyboxStrength.value;

                  PostProcessVolume bruh = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).Find("FX").GetChild(0).GetComponent<PostProcessVolume>();
                  bruh.weight = 0.28f;
                  HookLightingIntoPostProcessVolume bruh2 = GameObject.Find("HOLDER: Gameplay Space").transform.GetChild(0).Find("Quadrant 4: Starting Temple").GetChild(0).Find("FX").GetChild(0).GetComponent<HookLightingIntoPostProcessVolume>();
                  // 0.1138 0.1086 0.15 1
                  // 0.1012 0.1091 0.1226 1
                  bruh2.overrideAmbientColor = new Color(0.4138f, 0.4086f, 0.45f, 1);
                  bruh2.overrideDirectionalColor = new Color(0.4012f, 0.4091f, 0.4226f, 1);
                }
                catch { Log.Error("Couldn't set one or more of the Materials for the Terrain. Please report to the mod maintainer on Thunderstore."); }
                if (testTerrainMat.name.Contains("Snowy"))
                  Stage6.Moon(testTerrainMat, testDetailMat3, testDetailMat2, testDetailMat);
                else
                  Stage6.Moon(testTerrainMat, testDetailMat, testDetailMat2, testDetailMat3);
                break;
            }
          }
        }
      }
      orig(self);
    }

    public static void AbyssalLighting(Color color)
    {
      Light[] lightList = Object.FindObjectsOfType(typeof(Light)) as Light[];
      foreach (Light light in lightList)
      {
        GameObject lightBase = light.gameObject;
        if (lightBase != null)
        {
          Transform lightParent = lightBase.transform.parent;
          if (lightParent != null)
          {
            if (light.gameObject.transform.parent.gameObject.name.Equals("DCCoralPropMediumActive"))
            {
              light.color = color;
              var lightLP = light.transform.localPosition;
              lightLP.z = 4;
            }
            else if (light.gameObject.transform.parent.gameObject.name.Contains("DCCrystalCluster Variant"))
            {
              light.color = color;
              light.intensity *= 1.5f;
            }
            else if (light.gameObject.transform.parent.gameObject.name.Contains("DCCrystalLarge"))
              light.color = color;
          }
          if (light.gameObject.name.Equals("CrystalLight")) light.color = color;
        }
      }
    }

    private static void LoadAssets()
    {
      AssetReferenceT<Material> matRef = new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_35_0.RoR2_DLC2_meridian_Assets.matPMTerrainPlayZone_mat);
      AssetAsyncReferenceManager<Material>.LoadAsset(matRef).Completed += (x) => terrainMatTesting = x.Result;

      AssetReferenceT<Material> mat2Ref = new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_35_0.RoR2_DLC2_meridian_Assets.matPMGold_mat);
      AssetAsyncReferenceManager<Material>.LoadAsset(mat2Ref).Completed += (x) => detailMatTesting = x.Result;

      AssetReferenceT<Material> mat3Ref = new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_35_0.RoR2_DLC2_meridian_Assets.matPMTerrainPlayZoneWall_mat);
      AssetAsyncReferenceManager<Material>.LoadAsset(mat3Ref).Completed += (x) => detailMat2Testing = x.Result;

      AssetReferenceT<Material> mat4Ref = new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_35_0.RoR2_DLC2_meridian_Assets.matPMTerrainPlayZoneDark_mat);
      AssetAsyncReferenceManager<Material>.LoadAsset(mat4Ref).Completed += (x) => detailMat3Testing = x.Result;
      // Iron Alluvium matIADrillMachineLowEm matIAIron matIAProps matIAPipes matIATerrain
      // texIALandscapeWallDiffuse _RedChannelSideTex
      // _GreenChannelTex texRCConcreteTopDiffuse
      // Repurposed Crater matRCMetalTrim01 matRCPipes03
      // Pretender's Presipice matPPCablesEmissive matPPShipDebrisSnow matPPSolusSpheresPanels matPPTerrain matPPTree
      // Conduit Canyon matCCTerrain_REDROCK matCCMetal
      // Solutional Haunt matSHTerrain matSHMetal01A matSHMetalTEMP matSHMetal matSHConcreteWall matSHConcreteFloor
      // Computational Exchange matCEVoxelPath matCEVoxelObject 
      // Prime Meridian matPMTerrainPlayZone matPMTerrainPlayZoneWall matPMGold matPMTerrainPlayZoneDark
      /*
      AssetReferenceT<Material> matRef = new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_35_0.RoR2_DLC3_conduitcanyon.matCCTerrain_mat);
      AssetAsyncReferenceManager<Material>.LoadAsset(matRef).Completed += (x) => terrainMatTesting = x.Result;

      AssetReferenceT<Material> mat2Ref = new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_35_0.RoR2_DLC3_ironalluvium.matIAIron_mat);
      AssetAsyncReferenceManager<Material>.LoadAsset(mat2Ref).Completed += (x) => detailMatTesting = x.Result;
     
      AssetReferenceT<Material> mat2Ref = new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_35_0.RoR2_DLC3_computationalexchange.matCEVoxelObject_mat);
      AssetAsyncReferenceManager<Material>.LoadAsset(mat2Ref).Completed += (x) => detailMatTesting = x.Result;

      AssetReferenceT<Material> mat4Ref = new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.Version_1_35_0.RoR2_DLC3_nest.matPPCablesEmissive_mat);
      AssetAsyncReferenceManager<Material>.LoadAsset(mat4Ref).Completed += (x) => detailMat3Testing = x.Result;
      */
    }

    /*
        private static void TweakAssets()
        {
          Example for how to edit an asset once it finishes loading
          AssetAsyncReferenceManager<GameObject>.LoadAsset(new AssetReferenceT<Material>(RoR2BepInExPack.GameAssetPaths.RoR2_DLC2_Items_LowerPricedChests.PickupSaleStar_prefab)).Completed += delegate (AsyncOperationHandle<GameObject> obj)
          {
            MeshCollider collider = obj.Result.transform.find("SaleStar")?.GetComponent<MeshCollider>();
            if (collider)
            {
              collider.convex = true;
            }
          };
        }
    */
  }
}