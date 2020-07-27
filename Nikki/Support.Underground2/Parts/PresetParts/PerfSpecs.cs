using System.IO;
using System.ComponentModel;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using CoreExtensions.IO;



namespace Nikki.Support.Underground2.Parts.PresetParts
{
	/// <summary>
	/// A unit <see cref="PerfSpecs"/> used in preset rides.
	/// </summary>
	public class PerfSpecs : SubPart
	{
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("WeightReduction")]
		public eBoolean WT_REMOVE_REAR_SEATS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("WeightReduction")]
		public eBoolean WT_REMOVE_INTERIOR_PANELS { get; set; }
		
		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("WeightReduction")]
		public eBoolean WT_LIGHTWEIGHT_WINDOWS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("WeightReduction")]
		public eBoolean WT_LIGHTWEIGHT_SEATS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("WeightReduction")]
		public eBoolean WT_LIGHTWEIGHT_DOORS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("WeightReduction")]
		public eBoolean WT_FOAM_FILLED_INTERIOR { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Drivetrain")]
		public eBoolean TR_SHORT_THROW_SHIFT_KIT { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Drivetrain")]
		public eBoolean TR_LIGHT_FLYWHEEL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Drivetrain")]
		public eBoolean TR_DIFFERENTIAL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Drivetrain")]
		public eBoolean TR_LIMITED_SLIP_DIFFERENTIAL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Drivetrain")]
		public eBoolean TR_HIGH_PERFORMANCE_CLUTCH { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Drivetrain")]
		public eBoolean TR_6_SPEED_TRANSMISSION { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("NOS")]
		public eBoolean NO_DRY_SHOT_OF_NITROUS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("NOS")]
		public eBoolean NO_WET_SHOT_OF_NITROUS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("NOS")]
		public eBoolean NO_DIRECT_PORT_NITROUS_OXIDE { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Engine")]
		public eBoolean EN_MUFFLER { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Engine")]
		public eBoolean EN_COLD_AIR_INTAKE_SYSTEM { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Engine")]
		public eBoolean EN_REPLACE_HEADERS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Engine")]
		public eBoolean EN_MILD_CAMSHAFT_AND_CAM_GEARS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Engine")]
		public eBoolean EN_CAT_BACK_EXHAUST_SYSTEM { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Engine")]
		public eBoolean EN_HIGH_FLOW_INTAKE_MANIFOLD { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Engine")]
		public eBoolean EN_LARGER_DIAMETER_DOWNPIPE { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Engine")]
		public eBoolean EN_RACING_CAMSHAFT_AND_GEARS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Engine")]
		public eBoolean EN_PORT_AND_POLISH_HEADS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Engine")]
		public eBoolean EN_BLUEPRINT_THE_BLOCK { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Engine")]
		public eBoolean EN_HIGH_FLOW_HEADERS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Turbo")]
		public eBoolean TU_STAGE_1_TURBO_KIT { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Turbo")]
		public eBoolean TU_STAGE_2_TURBO_KIT { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Turbo")]
		public eBoolean TU_STAGE_3_TURBO_KIT { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Suspension")]
		public eBoolean SU_SPORT_SPRINGS_AND_SHOCKS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Suspension")]
		public eBoolean SU_STRUT_TOWER_BAR { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Suspension")]
		public eBoolean SU_PERFORMANCE_SPRINGS_AND_SHOCKS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Suspension")]
		public eBoolean SU_FRONT_AND_REAR_SWAY_BARS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Unused")]
		public eBoolean PERF_UNUSED_1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Unused")]
		public eBoolean PERF_UNUSED_2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Suspension")]
		public eBoolean SU_COIL_OVER_SUSPENSION_SYSTEM { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Suspension")]
		public eBoolean SU_CAMBER_KIT { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Suspension")]
		public eBoolean SU_LARGE_DIAMETER_SWAY_BARS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Brakes")]
		public eBoolean BR_STREET_COMPOUND_BRAKE_PADS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Brakes")]
		public eBoolean BR_STEEL_BRAIDED_BRAKE_LINES { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Brakes")]
		public eBoolean BR_CROSS_DRILLED_ROTORS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Brakes")]
		public eBoolean BR_LARGE_DIAMETER_ROTORS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Brakes")]
		public eBoolean BR_RACE_COMPOUND_BRAKE_PADS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Brakes")]
		public eBoolean BR_CROSS_DRILLED_AND_SLOTTED_ROTORS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Brakes")]
		public eBoolean BR_6_PISTON_RACING_CALIPERS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("ECU")]
		public eBoolean EC_REMOVE_SPEED_LIMITER { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("ECU")]
		public eBoolean EC_PERFORMANCE_CHIP { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("ECU")]
		public eBoolean EC_HIGH_FLOW_FUEL_PUMP { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("ECU")]
		public eBoolean EC_FUEL_PRESSURE_REGULATOR { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("ECU")]
		public eBoolean EC_FUEL_RAIL { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("ECU")]
		public eBoolean EC_FUEL_FILTER { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("ECU")]
		public eBoolean EC_ENGINE_MANAGEMENT_UNIT { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("ECU")]
		public eBoolean EC_FUEL_INJECTORS { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Tires")]
		public eBoolean TI_STREET_PERFORMANCE_TIRES { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Tires")]
		public eBoolean TI_PRO_PERFORMANCE_TIRES { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Tires")]
		public eBoolean TI_EXTREME_PERFORMANCE_TIRES { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Aero")]
		public eBoolean AE_FRONT_BUMPER_L1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Aero")]
		public eBoolean AE_FRONT_BUMPER_L2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Aero")]
		public eBoolean AE_FRONT_BUMPER_L3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Aero")]
		public eBoolean AE_SIDESKIRTS_L1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Aero")]
		public eBoolean AE_SIDESKIRTS_L2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Aero")]
		public eBoolean AE_SIDESKIRTS_L3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Aero")]
		public eBoolean AE_REAR_BUMPER_L1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Aero")]
		public eBoolean AE_REAR_BUMPER_L2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Aero")]
		public eBoolean AE_REAR_BUMPER_L3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Aero")]
		public eBoolean AE_SPOILER_L1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Aero")]
		public eBoolean AE_SPOILER_L2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AccessModifiable()]
		[Category("Aero")]
		public eBoolean AE_SPOILER_L3 { get; set; }

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new PerfSpecs();
			result.CloneValuesFrom(this);
			return result;
		}

		/// <summary>
		/// Reads data using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read data with.</param>
		public void Read(BinaryReader br)
		{
			this.WT_REMOVE_REAR_SEATS = br.ReadEnum<eBoolean>();
			this.WT_REMOVE_INTERIOR_PANELS = br.ReadEnum<eBoolean>();
			this.WT_LIGHTWEIGHT_WINDOWS = br.ReadEnum<eBoolean>();
			this.WT_LIGHTWEIGHT_SEATS = br.ReadEnum<eBoolean>();
			this.WT_LIGHTWEIGHT_DOORS = br.ReadEnum<eBoolean>();
			this.WT_FOAM_FILLED_INTERIOR = br.ReadEnum<eBoolean>();
			this.TR_SHORT_THROW_SHIFT_KIT = br.ReadEnum<eBoolean>();
			this.TR_LIGHT_FLYWHEEL = br.ReadEnum<eBoolean>();
			this.TR_DIFFERENTIAL = br.ReadEnum<eBoolean>();
			this.TR_LIMITED_SLIP_DIFFERENTIAL = br.ReadEnum<eBoolean>();
			this.TR_HIGH_PERFORMANCE_CLUTCH = br.ReadEnum<eBoolean>();
			this.TR_6_SPEED_TRANSMISSION = br.ReadEnum<eBoolean>();
			this.NO_DRY_SHOT_OF_NITROUS = br.ReadEnum<eBoolean>();
			this.NO_WET_SHOT_OF_NITROUS = br.ReadEnum<eBoolean>();
			this.NO_DIRECT_PORT_NITROUS_OXIDE = br.ReadEnum<eBoolean>();
			this.EN_MUFFLER = br.ReadEnum<eBoolean>();
			this.EN_COLD_AIR_INTAKE_SYSTEM = br.ReadEnum<eBoolean>();
			this.EN_REPLACE_HEADERS = br.ReadEnum<eBoolean>();
			this.EN_MILD_CAMSHAFT_AND_CAM_GEARS = br.ReadEnum<eBoolean>();
			this.EN_CAT_BACK_EXHAUST_SYSTEM = br.ReadEnum<eBoolean>();
			this.EN_HIGH_FLOW_INTAKE_MANIFOLD = br.ReadEnum<eBoolean>();
			this.EN_LARGER_DIAMETER_DOWNPIPE = br.ReadEnum<eBoolean>();
			this.EN_RACING_CAMSHAFT_AND_GEARS = br.ReadEnum<eBoolean>();
			this.EN_PORT_AND_POLISH_HEADS = br.ReadEnum<eBoolean>();
			this.EN_BLUEPRINT_THE_BLOCK = br.ReadEnum<eBoolean>();
			this.EN_HIGH_FLOW_HEADERS = br.ReadEnum<eBoolean>();
			this.TU_STAGE_1_TURBO_KIT = br.ReadEnum<eBoolean>();
			this.TU_STAGE_2_TURBO_KIT = br.ReadEnum<eBoolean>();
			this.TU_STAGE_3_TURBO_KIT = br.ReadEnum<eBoolean>();
			this.SU_SPORT_SPRINGS_AND_SHOCKS = br.ReadEnum<eBoolean>();
			this.SU_STRUT_TOWER_BAR = br.ReadEnum<eBoolean>();
			this.SU_PERFORMANCE_SPRINGS_AND_SHOCKS = br.ReadEnum<eBoolean>();
			this.SU_FRONT_AND_REAR_SWAY_BARS = br.ReadEnum<eBoolean>();
			this.PERF_UNUSED_1 = br.ReadEnum<eBoolean>();
			this.PERF_UNUSED_2 = br.ReadEnum<eBoolean>();
			this.SU_COIL_OVER_SUSPENSION_SYSTEM = br.ReadEnum<eBoolean>();
			this.SU_CAMBER_KIT = br.ReadEnum<eBoolean>();
			this.SU_LARGE_DIAMETER_SWAY_BARS = br.ReadEnum<eBoolean>();
			this.BR_STREET_COMPOUND_BRAKE_PADS = br.ReadEnum<eBoolean>();
			this.BR_STEEL_BRAIDED_BRAKE_LINES = br.ReadEnum<eBoolean>();
			this.BR_CROSS_DRILLED_ROTORS = br.ReadEnum<eBoolean>();
			this.BR_LARGE_DIAMETER_ROTORS = br.ReadEnum<eBoolean>();
			this.BR_RACE_COMPOUND_BRAKE_PADS = br.ReadEnum<eBoolean>();
			this.BR_CROSS_DRILLED_AND_SLOTTED_ROTORS = br.ReadEnum<eBoolean>();
			this.BR_6_PISTON_RACING_CALIPERS = br.ReadEnum<eBoolean>();
			this.EC_REMOVE_SPEED_LIMITER = br.ReadEnum<eBoolean>();
			this.EC_PERFORMANCE_CHIP = br.ReadEnum<eBoolean>();
			this.EC_HIGH_FLOW_FUEL_PUMP = br.ReadEnum<eBoolean>();
			this.EC_FUEL_PRESSURE_REGULATOR = br.ReadEnum<eBoolean>();
			this.EC_FUEL_RAIL = br.ReadEnum<eBoolean>();
			this.EC_FUEL_FILTER = br.ReadEnum<eBoolean>();
			this.EC_ENGINE_MANAGEMENT_UNIT = br.ReadEnum<eBoolean>();
			this.EC_FUEL_INJECTORS = br.ReadEnum<eBoolean>();
			this.TI_STREET_PERFORMANCE_TIRES = br.ReadEnum<eBoolean>();
			this.TI_PRO_PERFORMANCE_TIRES = br.ReadEnum<eBoolean>();
			this.TI_EXTREME_PERFORMANCE_TIRES = br.ReadEnum<eBoolean>();
			this.AE_FRONT_BUMPER_L1 = br.ReadEnum<eBoolean>();
			this.AE_FRONT_BUMPER_L2 = br.ReadEnum<eBoolean>();
			this.AE_FRONT_BUMPER_L3 = br.ReadEnum<eBoolean>();
			this.AE_SIDESKIRTS_L1 = br.ReadEnum<eBoolean>();
			this.AE_SIDESKIRTS_L2 = br.ReadEnum<eBoolean>();
			this.AE_SIDESKIRTS_L3 = br.ReadEnum<eBoolean>();
			this.AE_REAR_BUMPER_L1 = br.ReadEnum<eBoolean>();
			this.AE_REAR_BUMPER_L2 = br.ReadEnum<eBoolean>();
			this.AE_REAR_BUMPER_L3 = br.ReadEnum<eBoolean>();
			this.AE_SPOILER_L1 = br.ReadEnum<eBoolean>();
			this.AE_SPOILER_L2 = br.ReadEnum<eBoolean>();
			this.AE_SPOILER_L3 = br.ReadEnum<eBoolean>();
		}

		/// <summary>
		/// Writes data using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write data with.</param>
		public void Write(BinaryWriter bw)
		{
			bw.WriteEnum(this.WT_REMOVE_REAR_SEATS);
			bw.WriteEnum(this.WT_REMOVE_INTERIOR_PANELS);
			bw.WriteEnum(this.WT_LIGHTWEIGHT_WINDOWS);
			bw.WriteEnum(this.WT_LIGHTWEIGHT_SEATS);
			bw.WriteEnum(this.WT_LIGHTWEIGHT_DOORS);
			bw.WriteEnum(this.WT_FOAM_FILLED_INTERIOR);
			bw.WriteEnum(this.TR_SHORT_THROW_SHIFT_KIT);
			bw.WriteEnum(this.TR_LIGHT_FLYWHEEL);
			bw.WriteEnum(this.TR_DIFFERENTIAL);
			bw.WriteEnum(this.TR_LIMITED_SLIP_DIFFERENTIAL);
			bw.WriteEnum(this.TR_HIGH_PERFORMANCE_CLUTCH);
			bw.WriteEnum(this.TR_6_SPEED_TRANSMISSION);
			bw.WriteEnum(this.NO_DRY_SHOT_OF_NITROUS);
			bw.WriteEnum(this.NO_WET_SHOT_OF_NITROUS);
			bw.WriteEnum(this.NO_DIRECT_PORT_NITROUS_OXIDE);
			bw.WriteEnum(this.EN_MUFFLER);
			bw.WriteEnum(this.EN_COLD_AIR_INTAKE_SYSTEM);
			bw.WriteEnum(this.EN_REPLACE_HEADERS);
			bw.WriteEnum(this.EN_MILD_CAMSHAFT_AND_CAM_GEARS);
			bw.WriteEnum(this.EN_CAT_BACK_EXHAUST_SYSTEM);
			bw.WriteEnum(this.EN_HIGH_FLOW_INTAKE_MANIFOLD);
			bw.WriteEnum(this.EN_LARGER_DIAMETER_DOWNPIPE);
			bw.WriteEnum(this.EN_RACING_CAMSHAFT_AND_GEARS);
			bw.WriteEnum(this.EN_PORT_AND_POLISH_HEADS);
			bw.WriteEnum(this.EN_BLUEPRINT_THE_BLOCK);
			bw.WriteEnum(this.EN_HIGH_FLOW_HEADERS);
			bw.WriteEnum(this.TU_STAGE_1_TURBO_KIT);
			bw.WriteEnum(this.TU_STAGE_2_TURBO_KIT);
			bw.WriteEnum(this.TU_STAGE_3_TURBO_KIT);
			bw.WriteEnum(this.SU_SPORT_SPRINGS_AND_SHOCKS);
			bw.WriteEnum(this.SU_STRUT_TOWER_BAR);
			bw.WriteEnum(this.SU_PERFORMANCE_SPRINGS_AND_SHOCKS);
			bw.WriteEnum(this.SU_FRONT_AND_REAR_SWAY_BARS);
			bw.WriteEnum(this.PERF_UNUSED_1);
			bw.WriteEnum(this.PERF_UNUSED_2);
			bw.WriteEnum(this.SU_COIL_OVER_SUSPENSION_SYSTEM);
			bw.WriteEnum(this.SU_CAMBER_KIT);
			bw.WriteEnum(this.SU_LARGE_DIAMETER_SWAY_BARS);
			bw.WriteEnum(this.BR_STREET_COMPOUND_BRAKE_PADS);
			bw.WriteEnum(this.BR_STEEL_BRAIDED_BRAKE_LINES);
			bw.WriteEnum(this.BR_CROSS_DRILLED_ROTORS);
			bw.WriteEnum(this.BR_LARGE_DIAMETER_ROTORS);
			bw.WriteEnum(this.BR_RACE_COMPOUND_BRAKE_PADS);
			bw.WriteEnum(this.BR_CROSS_DRILLED_AND_SLOTTED_ROTORS);
			bw.WriteEnum(this.BR_6_PISTON_RACING_CALIPERS);
			bw.WriteEnum(this.EC_REMOVE_SPEED_LIMITER);
			bw.WriteEnum(this.EC_PERFORMANCE_CHIP);
			bw.WriteEnum(this.EC_HIGH_FLOW_FUEL_PUMP);
			bw.WriteEnum(this.EC_FUEL_PRESSURE_REGULATOR);
			bw.WriteEnum(this.EC_FUEL_RAIL);
			bw.WriteEnum(this.EC_FUEL_FILTER);
			bw.WriteEnum(this.EC_ENGINE_MANAGEMENT_UNIT);
			bw.WriteEnum(this.EC_FUEL_INJECTORS);
			bw.WriteEnum(this.TI_STREET_PERFORMANCE_TIRES);
			bw.WriteEnum(this.TI_PRO_PERFORMANCE_TIRES);
			bw.WriteEnum(this.TI_EXTREME_PERFORMANCE_TIRES);
			bw.WriteEnum(this.AE_FRONT_BUMPER_L1);
			bw.WriteEnum(this.AE_FRONT_BUMPER_L2);
			bw.WriteEnum(this.AE_FRONT_BUMPER_L3);
			bw.WriteEnum(this.AE_SIDESKIRTS_L1);
			bw.WriteEnum(this.AE_SIDESKIRTS_L2);
			bw.WriteEnum(this.AE_SIDESKIRTS_L3);
			bw.WriteEnum(this.AE_REAR_BUMPER_L1);
			bw.WriteEnum(this.AE_REAR_BUMPER_L2);
			bw.WriteEnum(this.AE_REAR_BUMPER_L3);
			bw.WriteEnum(this.AE_SPOILER_L1);
			bw.WriteEnum(this.AE_SPOILER_L2);
			bw.WriteEnum(this.AE_SPOILER_L3);
		}
	}
}
