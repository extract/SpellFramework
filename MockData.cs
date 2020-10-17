using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using ThunderRoad;
using UnityEngine;

namespace SpellFramework
{
    [Serializable]
    public class CatalogData
    {
        [JsonProperty(Order = -2)]
        public string @type;

        //[JsonProperty(Order = -2)]
        public string id;

        // Token: 0x04000CCF RID: 3279
        [JsonProperty(Order = -2)]

        public int hashId;

        // Token: 0x04000CD0 RID: 3280
        [JsonProperty(Order = -2)]
        public int version;

        // Token: 0x04000CD1 RID: 3281
        [JsonProperty(Order = -2)]
        public bool exludeFromBuild;
    }
    [Serializable]
    public class CreatureData : CatalogData
    {

        public int pooledCount;

        // Token: 0x04000EDC RID: 3804

        public string displayName = "{Unknown}";

        // Token: 0x04000EDD RID: 3805

        public string prefabName;

        // Token: 0x04000EDE RID: 3806


        [ReadOnly]
        protected Creature prefab;

        // Token: 0x04000EDF RID: 3807

        public short health = 50;

        // Token: 0x04000EE0 RID: 3808

        public float mana = 50f;

        // Token: 0x04000EE1 RID: 3809

        public float manaRegen = 5f;

        // Token: 0x04000EE2 RID: 3810

        public float focus = 30f;

        // Token: 0x04000EE3 RID: 3811

        public float focusRegen = 2f;

        // Token: 0x04000EE4 RID: 3812

        public int baseXP = 20;

        // Token: 0x04000EE5 RID: 3813


        public string containerID;

        // Token: 0x04000EE6 RID: 3814


        public int factionId;

        // Token: 0x04000EE7 RID: 3815


        public string animationControllerPath;


        // Token: 0x04000EE9 RID: 3817

        public bool paintable = true;

        // Token: 0x04000EEA RID: 3818

        public List<Paintable.MaterialProperty> paintableMaterialProperties;

        // Token: 0x04000EEB RID: 3819
        public static bool autoAlternateRace;

        // Token: 0x04000EEC RID: 3820


        public string umaPresetID;

        // Token: 0x04000EED RID: 3821

        public UMAPreset umaPreset;

        // Token: 0x04000EEE RID: 3822

        public bool usePlayerAtlasResolutionscale;

        // Token: 0x04000EEF RID: 3823

        public List<string> headSlots = new List<string>();

        // Token: 0x04000EF0 RID: 3824

        public bool wristStatsEnabled;

        // Token: 0x04000EF1 RID: 3825


        public string wristHealthEffectId;

        // Token: 0x04000EF2 RID: 3826

        public EffectData wristHealthEffectData;

        // Token: 0x04000EF3 RID: 3827


        public string wristManaEffectId;

        // Token: 0x04000EF4 RID: 3828

        public EffectData wristManaEffectData;

        // Token: 0x04000EF5 RID: 3829


        public string wristFocusEffectId;

        // Token: 0x04000EF6 RID: 3830

        public EffectData wristFocusEffectData;

        // Token: 0x04000EF7 RID: 3831


        public float wristShowDistance = 0.31f;

        // Token: 0x04000EF8 RID: 3832


        public float wristShowAngle = 40f;

        // Token: 0x04000EF9 RID: 3833


        public Vector3 wristRightLocalPosition;

        // Token: 0x04000EFA RID: 3834


        public Vector3 wristRightLocalRotation;

        // Token: 0x04000EFB RID: 3835


        public Vector3 wristLeftLocalPosition;

        // Token: 0x04000EFC RID: 3836


        public Vector3 wristLeftLocalRotation;

        // Token: 0x04000EFD RID: 3837


        public string ragdollID;

        // Token: 0x04000EFE RID: 3838

        public RagdollData ragdollData;

        // Token: 0x04000EFF RID: 3839

        public bool ragdollUsePhysics = true;

        // Token: 0x04000F00 RID: 3840


        public Vector2 ragdollKillVelocityRange = new Vector2(5f, 20f);

        // Token: 0x04000F01 RID: 3841


        public Vector2Int ragdollKillDamageRange = new Vector2Int(2, 100);

        // Token: 0x04000F02 RID: 3842


        public string sliceFillPath;

        // Token: 0x04000F03 RID: 3843


        [ReadOnly]



        public Material sliceFill;

        // Token: 0x04000F04 RID: 3844


        public string handItemId;

        // Token: 0x04000F05 RID: 3845


        public string footItemId;

        // Token: 0x04000F06 RID: 3846


        public string footTrackedItemId;

        // Token: 0x04000F07 RID: 3847


        public Vector3 eyeCameraOffset = Vector3.zero;

        // Token: 0x04000F08 RID: 3848


        public bool handFixedScale = true;

        // Token: 0x04000F09 RID: 3849

        public Vector3 handGlobalScale = Vector3.one;

        // Token: 0x04000F0A RID: 3850

        public Vector3 fingerBoneRotation = Vector3.zero;

        // Token: 0x04000F0B RID: 3851

        public Vector3 fingerPositionOffset = Vector3.zero;

        // Token: 0x04000F0C RID: 3852

        public Vector3 thumbPositionOffset = Vector3.zero;

        // Token: 0x04000F0D RID: 3853


        public Vector3 handRightPalmAxis = Vector3.left;

        // Token: 0x04000F0E RID: 3854

        public Vector3 handRightThumbAxis = Vector3.up;

        // Token: 0x04000F0F RID: 3855

        public Vector3 handRightGripPosition = new Vector3(0.08f, -0.03f, 0.015f);

        // Token: 0x04000F10 RID: 3856

        public Vector3 handRightGripRotation = new Vector3(0f, 120f, 90f);

        // Token: 0x04000F11 RID: 3857

        public Vector3 handRightCastPosition = Vector3.zero;

        // Token: 0x04000F12 RID: 3858

        public Vector3 handRightCastRotation = Vector3.zero;


        public Vector3 handLeftPalmAxis = Vector3.left;
        public Vector3 handLeftThumbAxis = Vector3.down;
        public Vector3 handLeftCastPosition = Vector3.zero;
        public Vector3 handLeftCastRotation = Vector3.zero;

        public Vector3 toeDirection = new Vector3(0f, -90f, -90f);
        public string expressionAttackId;

        // Token: 0x04000F19 RID: 3865

        public string expressionPainId;

        // Token: 0x04000F1B RID: 3867

        public string expressionDeathId;

        // Token: 0x04000F1D RID: 3869

        public ExpressionData expressionDeathData;
        public string expressionChokeId;

        // Token: 0x04000F1F RID: 3871

        public ExpressionData expressionChokeData;
        public string expressionAngryId;
        // Token: 0x04000F21 RID: 3873

        public ExpressionData expressionAngryData;
        public string brainId;
        public int avoidancePriority = 10;
        public bool audioEnabled = true;
        public string audioAttackPath;

        public AudioContainer audioAttackContainer;
        public string audioAttackBigPath;

        public AudioContainer audioAttackBigContainer;
        public string audioHitPath;

        public AudioContainer audioHitContainer;
        public string audioDeathPath;

        public AudioContainer audioDeathContainer;

        public string audioDeathSlowPath;

        public AudioContainer audioDeathSlowContainer;

        public string audioFallingPath;

        public AudioContainer audioFallingContainer;
        public float audioVolume = 1f;

        public float audioFallVelocityThreshold = 5f;
        public float audioAttackChance = 0.7f;
        public float audioHitChance = 1f;
        public float audioDeathChance = 1f;
        public float audioLipSyncUpdateRate = 0.05f;
        public float audioLipSyncMaxValue = 0.3f;

        public int audioLipSampleDataLength = 1024;

        public float forceMaxPosition = 3000f;
        public float forceMaxRotation = 250f;
        public Vector2 forcePositionSpringDamper = new Vector2(5000f, 100f);
        public Vector2 forceRotationSpringDamper = new Vector2(1000f, 50f);

        public Vector2 forcePositionSpringDamper2HMult = Vector2.one;
        public Vector2 forceSpringDamper2HNoDomMult = Vector2.one;
        public Vector2 forceRotationSpringDamper2HMult = Vector2.one;

        public float climbingForceMaxPosition = 3000f;
        public float climbingForceMaxRotation = 100f;
        public Vector2 climbingForcePositionSpringDamperMult = new Vector2(1f, 3f);

        public float gripForceMaxPosition = 3000f;
        public float gripForceMaxRotation = 100f;
        public Vector2 gripForcePositionSpringDamperMult = new Vector2(1f, 1f);
        public Vector2 gripForceRotationSpringDamperMult = new Vector2(0.5f, 1f);

        public List<CreatureData.Holder> holders;
        public float locomotionMass = 70f;

        public float locomotionSpeed = 3f;
        public float locomotionAirSpeed = 150f;
        public float locomotionJumpForce = 0.3f;
        public float locomotionJumpClimbMultiplier = 0.8f;
        public float locomotionJumpMaxDuration = 0.6f;
        public float locomotionBackwardSpeedMult = 0.8f;
        public float locomotionStrafeSpeedMult = 0.8f;
        public float locomotionRunSpeedMult = 2f;

        public string playerFallDamageEffectId;

        // Token: 0x04000F53 RID: 3923

        public EffectData playerFallDamageEffectData;

        // Token: 0x0200079D RID: 1949
        public enum Sex
        {
            // Token: 0x040035EA RID: 13802
            None,
            // Token: 0x040035EB RID: 13803
            Male,
            // Token: 0x040035EC RID: 13804
            Female
        }

        // Token: 0x0200079E RID: 1950
        [Serializable]
        public class Holder
        {

            public string interactableHolderId;
            [JsonMergeKey]
            public string name = "Default";

            public float axisLength;

            public float touchRadius = 0.1f;

            public Vector3 touchCenter;

            // Token: 0x040035F2 RID: 13810
            public UnityEngine.HumanBodyBones bone;

            // Token: 0x040035F3 RID: 13811
            public Vector3 localPosition;

            // Token: 0x040035F4 RID: 13812
            public Vector3 localRotation;
        }
    }
    [Serializable]
    public class SpellData : CatalogData
    {

        public float manaConsumption;


        public float manaWaste;

        public int level;
    }
    [Serializable]
    public class SpellCastData : SpellData
    {

        public string caption;

        public float minMana;

        public float aiMinDistanceCancel;

        public float aiMinDistanceToCast;

        public float aiMaxDistanceToCast;

        public float aiCastDistance;

        public float loopMaxDuration;

        public int aiCastType;

        public float aiMaxDistanceToCancel;
        public string audioContainerPath;
        public string runeString;

        public float audioPitch;
    }

    [Serializable]
    public class SpellCastCharge : SpellCastData
    {


        public string chargeEffectId;

        public bool imbueEnabled;

        public bool imbueAllowMetal;

        public float imbueRate;

        public float imbueRadius;

        public float imbueManaConsumption;

        public bool imbueHitUseDamager;


        public float imbueHitMinVelocity;

        public float imbueWhooshMinSpeed;

        public float imbueWhooshHapticMultiplier;


        public string imbueMetalEffectId;
        public float currentCharge;


        public string imbueCrystalEffectId;


        public string imbueTransferEffectId;


        public string imbueMaterialId;


        public string imbueBladeEffectId;
        public bool isSpraying;

        public float imbueWhooshMaxSpeed;


        public float sprayHeadToFireMaxAngle;


        public string openHandPoseId;

        public float chargeSpeed;

        public float stopSpeed;

        public bool stopIfManaDepleted;

        public float grabbedFireMaxCharge;

        public float chargeMinHaptic;


        public string closeHandPoseId;

        public float chargeMaxHaptic;

        public bool allowThrow;



        public string throwEffectId;


        public float throwMinCharge;

        public bool allowSpray;



        public string sprayHandPoseId;




        public float sprayMinCharge;


        public string fingerEffectId;
    }
    [Serializable]
    public class SpellCastProjectile : SpellCastCharge
    {
        public string projectileEffectId;

        public float imbueUseConsumption;


        public string projectileDeflectEffectId;

        public bool projectileAllowDeflect;

        public bool projectilePlayerGuided;

        public float projectileVelocity;



        public string imbueUseEffectId;


        public string projectileDamagerId;


        public string projectileId;

        public float projectileImbueEnergyTransfered;
    }

    public class MaterialData : CatalogData
    {

        [ReadOnly]
        public int physicMaterialHash;

        public string xpReference;



        public List<Collision> collisions;

        public class Collision
        {




            public string targetMaterialId;


            public bool forceIfSecondary;


        }
    }
}
