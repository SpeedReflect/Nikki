namespace Nikki.Support.Shared.Parts.PresetParts
{
    /// <summary>
    /// Class with strings to build labels for preset parts.
    /// </summary>
    internal class Concatenator
    {
        // Strings in both NFSC, NFSMW and NFSUG2
        public string _BASE = "_BASE";                                        // has nothing
        public string _BASE_KIT = "_BODY";                                    // has _KIT00/KITW01-KITW05 attached
        public string _FRONT_BRAKE = "_FRONT_BRAKE";                          // has nothing
        public string _FRONT_LEFT_WINDOW = "_FRONT_LEFT_WINDOW";              // has nothing
        public string _FRONT_RIGHT_WINDOW = "_FRONT_RIGHT_WINDOW";            // has nothing
        public string _FRONT_WINDOW = "_FRONT_WINDOW";                        // has nothing
        public string _INTERIOR = "_INTERIOR";                                // has nothing
        public string _LEFT_BRAKELIGHT = "_LEFT_BRAKELIGHT";                  // has nothing
        public string _LEFT_BRAKELIGHT_GLASS = "_LEFT_BRAKELIGHT_GLASS";      // has nothing
        public string _LEFT_HEADLIGHT = "_LEFT_HEADLIGHT";                    // has _ON/_OFF attached
        public string _LEFT_HEADLIGHT_GLASS = "_LEFT_HEADLIGHT_GLASS";        // has _OFF attached
        public string _LEFT_SIDE_MIRROR = "_LEFT_SIDE_MIRROR";                // has nothing
        public string _REAR_BRAKE = "_REAR_BRAKE";                            // has nothing
        public string _REAR_LEFT_WINDOW = "_REAR_LEFT_WINDOW";                // has nothing
        public string _REAR_RIGHT_WINDOW = "_REAR_RIGHT_WINDOW";              // has nothing
        public string _REAR_WINDOW = "_REAR_WINDOW";                          // has nothing
        public string _RIGHT_BRAKELIGHT = "_RIGHT_BRAKELIGHT";                // has nothing
        public string _RIGHT_BRAKELIGHT_GLASS = "_RIGHT_BRAKELIGHT_GLASS";    // has nothing
        public string _RIGHT_HEADLIGHT = "_RIGHT_HEADLIGHT";                  // has _ON/_OFF attached
        public string _RIGHT_HEADLIGHT_GLASS = "_RIGHT_HEADLIGHT_GLASS";      // has _OFF attached
        public string _RIGHT_SIDE_MIRROR = "_RIGHT_SIDE_MIRROR";              // has nothing
        public string _DRIVER = "_DRIVER";                                    // has nothing
        public string _SPOILER = "_SPOILER";                                  // has _STYLE#, type, and _CF attached
        public string _UNIVERSAL_SPOILER_BASE = "_UNIVERSAL_SPOILER_BASE";    // has nothing
        public string _DAMAGE0_FRONT = "_DAMAGE0_FRONT";                      // has KITW01-KITW05 or KIT00-KIT10 attached
        public string _DAMAGE0_FRONTLEFT = "_DAMAGE0_FRONTLEFT";              // has KITW01-KITW05 or KIT00-KIT10 attached
        public string _DAMAGE0_FRONTRIGHT = "_DAMAGE0_FRONTRIGHT";            // has KITW01-KITW05 or KIT00-KIT10 attached
        public string _DAMAGE0_REAR = "_DAMAGE0_REAR";                        // has KITW01-KITW05 or KIT00-KIT10 attached
        public string _DAMAGE0_REARLEFT = "_DAMAGE0_REARLEFT";                // has KITW01-KITW05 or KIT00-KIT10 attached
        public string _DAMAGE0_REARRIGHT = "_DAMAGE0_REARRIGHT";              // has KITW01-KITW05 or KIT00-KIT10 attached
        public string ROOF_STYLE = "ROOF_STYLE";                              // has #, _DUAL, _AUTOSCULPT, _CF attached
        public string _HOOD = "_HOOD";                                        // has _STYLE#, _AS, _CF attached
        public string _WHEEL = "_WHEEL";                                      // has RimBrand/AUTOSCLPT, _STYLE#, RimSize attached
        public string LICENSE_PLATE_STYLE01 = "LICENSE_PLATE_STYLE01";        // has nothing
        public string WINDOW_TINT_STOCK = "WINDOW_TINT_STOCK";                // has WINDSHIELD_TINT, _L#, _PEARL, _COLOR attached
        public string PAINT = "GLOSS";                                        // has nothing

        // String exclusive to NFSC
        public string _FRONT_ROTOR = "_FRONT_ROTOR";                          // has nothing
        public string _REAR_ROTOR = "_REAR_ROTOR";                            // has nothing
        public string _STEERINGWHEEL = "_STEERINGWHEEL";                      // has nothing
        public string _KIT00_EXHAUST = "_KIT00_EXHAUST";                      // has _STYLE#, _CENTER and _LEVEL1 attached
        public string _FRONT_BUMPER = "_FRONT_BUMPER";                        // has KIT00-KIT10 attached
        public string _FRONT_BUMPER_BADGING_SET = "_FRONT_BUMPER_BADGING_SET";// has KIT00-KIT08 attached
        public string _REAR_BUMPER = "_REAR_BUMPER";                          // has KIT00-KIT10 attached
        public string _REAR_BUMPER_BADGING_SET = "_REAR_BUMPER_BADGING_SET";  // has KIT00-KIT08 attached
        public string _SKIRT = "_SKIRT";                                      // has KIT00-KIT14
        public string _ROOF = "_ROOF";                                        // can be changed to _CHOP_TOP
        public string _DOOR_LEFT = "_DOOR_LEFT";                              // has KIT00 and KITW01-KITW05 attached
        public string _DOOR_RIGHT = "_DOOR_RIGHT";                            // has KIT00 and KITW01-KITW05 attached
        public string _KIT00_DOORLINE = "_KIT00_DOORLINE";                    // has nothing
        public string SWATCH_COLOR = "SWATCH_COLOR";                          // has # attached

        // String exclusive to NFSMW
        public string _DAMAGE_0_FRONT_WINDOW = "_DAMAGE_0_FRONT_WINDOW";
        public string _DAMAGE_0_BODY = "_DAMAGE_0_BODY";
        public string _DAMAGE_0_COP_LIGHTS = "_DAMAGE_0_COP_LIGHTS";
        public string _DAMAGE_0_SPOILER = "_DAMAGE_0_SPOILER";
        public string _DAMAGE_0_FRONT_WHEEL = "_DAMAGE_0_FRONT_WHEEL";
        public string _DAMAGE_0_LEFT_BRAKELIGHT = "_DAMAGE_0_LEFT_BRAKELIGHT";
        public string _DAMAGE_0_RIGHT_BREAKLIGHT = "_DAMAGE_0_RIGHT_BREAKLIGHT";
        public string _DAMAGE_0_LEFT_HEADLIGHT = "_DAMAGE_0_LEFT_HEADLIGHT";
        public string _DAMAGE_0_RIGHT_HEADLIGHT = "_DAMAGE_0_RIGHT_HEADLIGHT";
        public string _DAMAGE_0_HOOD = "_DAMAGE_0_HOOD";
        public string _DAMAGE_0_BUSHGUARD = "_DAMAGE_0_BUSHGUARD";
        public string _DAMAGE_0_FRONT_BUMPER = "_DAMAGE_0_FRONT_BUMPER";
        public string _DAMAGE_0_RIGHT_DOOR = "_DAMAGE_0_RIGHT_DOOR";
        public string _DAMAGE_0_RIGHT_REAR_DOOR = "_DAMAGE_0_RIGHT_REAR_DOOR";
        public string _DAMAGE_0_TRUNK = "_DAMAGE_0_TRUNK";
        public string _DAMAGE_0_REAR_BUMPER = "_DAMAGE_0_REAR_BUMPER";
        public string _DAMAGE_0_REAR_LEFT_WINDOW = "_DAMAGE_0_REAR_LEFT_WINDOW";
        public string _DAMAGE_0_FRONT_LEFT_WINDOW = "_DAMAGE_0_FRONT_LEFT_WINDOW";
        public string _DAMAGE_0_FRONT_RIGHT_WINDOW = "_DAMAGE_0_FRONT_RIGHT_WINDOW";
        public string _DAMAGE_0_REAR_RIGHT_WINDOW = "_DAMAGE_0_REAR_RIGHT_WINDOW";
        public string _DAMAGE_0_LEFT_DOOR = "_DAMAGE_0_LEFT_DOOR";
        public string _DAMAGE_0_REAR_DOOR = "_DAMAGE_0_REAR_DOOR";
        public string _ATTACHMENT = "_ATTACHMENT";
        public string _DECAL_FRONT_WINDOW_WIDE_MEDIUM = "_DECAL_FRONT_WINDOW_WIDE_MEDIUM";
        public string _DECAL_REAR_WINDOW_WIDE_MEDIUM = "_DECAL_REAR_WINDOW_WIDE_MEDIUM";
        public string _DECAL_LEFT_DOOR_RECT_MEDIUM = "_DECAL_LEFT_DOOR_RECT_MEDIUM";
        public string _DECAL_RIGHT_DOOR_RECT_MEDIUM = "_DECAL_RIGHT_DOOR_RECT_MEDIUM";
        public string _DECAL_LEFT_QUARTER_RECT_MEDIUM = "_DECAL_LEFT_QUARTER_RECT_MEDIUM";
        public string _DECAL_RIGHT_QUARTER_RECT_MEDIUM = "_DECAL_RIGHT_QUARTER_RECT_MEDIUM";
        public string RIM_PAINT = "";
        public string VINYL_LAYER = "";
        public string[] SWATCH = new string[4] { "", "", "", "" };
        public string HUD = "STOCK";
        public string HUD_BACKING = "WHITE";
        public string HUD_NEEDLE = "WHITE";
        public string HUD_CHARS = "WHITE";

        // Strings exclusive to NFSUG2
        public string _KITW_BODY = "_BODY";
        public string _TOP = "_TOP";
        public string _TRUNK = "_TRUNK";
        public string _ENGINE = "_ENGINE";
        public string _HEADLIGHT = "_HEADLIGHT";
        public string _BRAKELIGHT = "_BRAKELIGHT";
        public string _DOOR_PANEL_LEFT = "_DOOR_PANEL_LEFT";
        public string _DOOR_PANEL_RIGHT = "_DOOR_PANEL_RIGHT";
        public string _DOOR_SILL_LEFT = "_DOOR_SILL_LEFT";
        public string _DOOR_SILL_RIGHT = "_DOOR_SILL_RIGHT";
        public string _HOOD_UNDER = "_HOOD_UNDER";
        public string _TRUNK_UNDER = "_TRUNK_UNDER";
        public string _WING_MIRROR = "_WING_MIRROR";
        public string _TRUNK_AUDIO = "_TRUNK_AUDIO";
        public string _DECAL_HOOD_RECT_MEDIUM = "_DECAL_HOOD_RECT_MEDIUM";
        public string _DECAL_HOOD_RECT_SMALL = "_DECAL_HOOD_RECT_SMALL";
        public string _DECAL_LEFT_QUARTER_RECT_SMALL = "_DECAL_LEFT_QUARTER_RECT_SMALL";
        public string _DECAL_RIGHT_QUARTER_RECT_SMALL = "_DECAL_RIGHT_QUARTER_RECT_SMALL";
        public string _DECAL_HOOD_RECT_ = "_DECAL_HOOD_RECT_";
        public string _DECAL_LEFT_QUARTER_RECT_ = "_DECAL_LEFT_QUARTER_RECT_";
        public string _DECAL_RIGHT_QUARTER_RECT_ = "_DECAL_RIGHT_QUARTER_RECT_";
        public string _DECAL_LEFT_DOOR_RECT_ = "_DECAL_LEFT_DOOR_RECT_MEDIUM";
        public string _DECAL_RIGHT_DOOR_RECT_ = "_DECAL_RIGHT_DOOR_RECT_MEDIUM";
        public string CARBON_FIBRE = "CARBON FIBRE";
        public string CARBON_FIBRE_NONE = "CARBON FIBRE NONE";
        public string KIT_CARBON = string.Empty;
        public string HOOD_CARBON = string.Empty;
        public string DOOR_CARBON = string.Empty;
        public string TRUNK_CARBON = string.Empty;
        public string NEON_NONE = "NEON_NONE";
        public string CABIN_NEON_STYLE0 = "CABIN_NEON_STYLE0";
        public string NO_HYDRAULICS = "NO HYDRAULICS";
        public string _CV = "_CV";
    }
}
