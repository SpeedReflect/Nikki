using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
using Nikki.Utils;
using Nikki.Reflection.Enum;
using Nikki.Reflection.Enum.CP;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Attributes;
using Nikki.Support.Shared.Parts.CarParts;
using CoreExtensions.IO;
using CoreExtensions.Text;
using CoreExtensions.Reflection;
using CoreExtensions.Conversions;



namespace Nikki.Support.Carbon.Attributes
{
	/// <summary>
	/// A <see cref="CPAttribute"/> with in-built <see cref="CPStruct"/>.
	/// </summary>
	[DebuggerDisplay("Attribute: {AttribType} | Type: {Type} | Templated: {Templated}")]
	public class ModelTableAttribute : CPAttribute
	{
		/// <summary>
		/// <see cref="CarPartAttribType"/> type of this <see cref="ModelTableAttribute"/>.
		/// </summary>
		[Category("Main")]
		public override CarPartAttribType AttribType => CarPartAttribType.ModelTable;

		/// <summary>
		/// Type of this <see cref="ModelTableAttribute"/>.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public eAttribModelTable Type { get; set; }

		/// <summary>
		/// Key of the part to which this <see cref="CPAttribute"/> belongs to.
		/// </summary>
		[ReadOnly(true)]
		[TypeConverter(typeof(HexConverter))]
		[Category("Main")]
		public override uint Key
		{
			get => (uint)this.Type;
			set => this.Type = (eAttribModelTable)value;
		}

		internal int Index { get; set; }

		#region Template

		/// <summary>
		/// If true, all names are places in the string block; otherwise, all 
		/// hashes of the names are stored in the table.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public eBoolean Templated { get; set; } = eBoolean.False;

		/// <summary>
		/// Main concatenator string, if exists.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public string Concatenator { get; set; } = String.Empty;

		/// <summary>
		/// True if concatenator string exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("Main")]
		public eBoolean ConcatenatorExists { get; set; } = eBoolean.False;

		#endregion

		#region Geometry Lod Names

		/// <summary>
		/// Geometry 0 Lod A value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodA")]
		public string Geometry0LodA { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 0 Lod B value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodB")]
		public string Geometry0LodB { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 0 Lod C value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodC")]
		public string Geometry0LodC { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 0 Lod D value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodD")]
		public string Geometry0LodD { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 0 Lod E value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodE")]
		public string Geometry0LodE { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 1 Lod A value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodA")]
		public string Geometry1LodA { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 1 Lod B value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodB")]
		public string Geometry1LodB { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 1 Lod C value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodC")]
		public string Geometry1LodC { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 1 Lod D value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodD")]
		public string Geometry1LodD { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 1 Lod E value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodE")]
		public string Geometry1LodE { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 2 Lod A value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodA")]
		public string Geometry2LodA { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 2 Lod B value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodB")]
		public string Geometry2LodB { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 2 Lod C value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodC")]
		public string Geometry2LodC { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 2 Lod D value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodD")]
		public string Geometry2LodD { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 2 Lod E value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodE")]
		public string Geometry2LodE { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 3 Lod A value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodA")]
		public string Geometry3LodA { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 3 Lod B value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodB")]
		public string Geometry3LodB { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 3 Lod C value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodC")]
		public string Geometry3LodC { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 3 Lod D value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodD")]
		public string Geometry3LodD { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 3 Lod E value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodE")]
		public string Geometry3LodE { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 4 Lod A value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodA")]
		public string Geometry4LodA { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 4 Lod B value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodB")]
		public string Geometry4LodB { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 4 Lod C value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodC")]
		public string Geometry4LodC { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 4 Lod D value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodD")]
		public string Geometry4LodD { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 4 Lod E value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodE")]
		public string Geometry4LodE { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 5 Lod A value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodA")]
		public string Geometry5LodA { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 5 Lod B value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodB")]
		public string Geometry5LodB { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 5 Lod C value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodC")]
		public string Geometry5LodC { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 5 Lod D value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodD")]
		public string Geometry5LodD { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 5 Lod E value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodE")]
		public string Geometry5LodE { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 6 Lod A value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodA")]
		public string Geometry6LodA { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 6 Lod B value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodB")]
		public string Geometry6LodB { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 6 Lod C value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodC")]
		public string Geometry6LodC { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 6 Lod D value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodD")]
		public string Geometry6LodD { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 6 Lod E value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodE")]
		public string Geometry6LodE { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 7 Lod A value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodA")]
		public string Geometry7LodA { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 7 Lod B value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodB")]
		public string Geometry7LodB { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 7 Lod C value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodC")]
		public string Geometry7LodC { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 7 Lod D value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodD")]
		public string Geometry7LodD { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 7 Lod E value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodE")]
		public string Geometry7LodE { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 8 Lod A value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodA")]
		public string Geometry8LodA { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 8 Lod B value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodB")]
		public string Geometry8LodB { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 8 Lod C value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodC")]
		public string Geometry8LodC { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 8 Lod D value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodD")]
		public string Geometry8LodD { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 8 Lod E value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodE")]
		public string Geometry8LodE { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 9 Lod A value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodA")]
		public string Geometry9LodA { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 9 Lod B value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodB")]
		public string Geometry9LodB { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 9 Lod C value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodC")]
		public string Geometry9LodC { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 9 Lod D value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodD")]
		public string Geometry9LodD { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 9 Lod E value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodE")]
		public string Geometry9LodE { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 10 Lod A value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodA")]
		public string Geometry10LodA { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 10 Lod B value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodB")]
		public string Geometry10LodB { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 10 Lod C value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodC")]
		public string Geometry10LodC { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 10 Lod D value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodD")]
		public string Geometry10LodD { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 10 Lod E value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodE")]
		public string Geometry10LodE { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 11 Lod A value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodA")]
		public string Geometry11LodA { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 11 Lod B value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodB")]
		public string Geometry11LodB { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 11 Lod C value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodC")]
		public string Geometry11LodC { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 11 Lod D value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodD")]
		public string Geometry11LodD { get; set; } = String.Empty;

		/// <summary>
		/// Geometry 11 Lod E value.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodE")]
		public string Geometry11LodE { get; set; } = String.Empty;

		#endregion

		#region Geometry Lod Exist

		/// <summary>
		/// True if geometry 0 Lod A exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodAExists")]
		public eBoolean Geometry0LodAExists { get; set; }

		/// <summary>
		/// True if geometry 0 Lod B exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodBExists")]
		public eBoolean Geometry0LodBExists { get; set; }

		/// <summary>
		/// True if geometry 0 Lod C exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodCExists")]
		public eBoolean Geometry0LodCExists { get; set; }

		/// <summary>
		/// True if geometry 0 Lod D exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodDExists")]
		public eBoolean Geometry0LodDExists { get; set; }

		/// <summary>
		/// True if geometry 0 Lod E exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodEExists")]
		public eBoolean Geometry0LodEExists { get; set; }

		/// <summary>
		/// True if geometry 1 Lod A exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodAExists")]
		public eBoolean Geometry1LodAExists { get; set; }

		/// <summary>
		/// True if geometry 1 Lod B exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodBExists")]
		public eBoolean Geometry1LodBExists { get; set; }

		/// <summary>
		/// True if geometry 1 Lod C exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodCExists")]
		public eBoolean Geometry1LodCExists { get; set; }

		/// <summary>
		/// True if geometry 1 Lod D exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodDExists")]
		public eBoolean Geometry1LodDExists { get; set; }

		/// <summary>
		/// True if geometry 1 Lod E exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodEExists")]
		public eBoolean Geometry1LodEExists { get; set; }

		/// <summary>
		/// True if geometry 2 Lod A exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodAExists")]
		public eBoolean Geometry2LodAExists { get; set; }

		/// <summary>
		/// True if geometry 2 Lod B exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodBExists")]
		public eBoolean Geometry2LodBExists { get; set; }

		/// <summary>
		/// True if geometry 2 Lod C exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodCExists")]
		public eBoolean Geometry2LodCExists { get; set; }

		/// <summary>
		/// True if geometry 2 Lod D exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodDExists")]
		public eBoolean Geometry2LodDExists { get; set; }

		/// <summary>
		/// True if geometry 2 Lod E exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodEExists")]
		public eBoolean Geometry2LodEExists { get; set; }

		/// <summary>
		/// True if geometry 3 Lod A exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodAExists")]
		public eBoolean Geometry3LodAExists { get; set; }

		/// <summary>
		/// True if geometry 3 Lod B exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodBExists")]
		public eBoolean Geometry3LodBExists { get; set; }

		/// <summary>
		/// True if geometry 3 Lod C exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodCExists")]
		public eBoolean Geometry3LodCExists { get; set; }

		/// <summary>
		/// True if geometry 3 Lod D exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodDExists")]
		public eBoolean Geometry3LodDExists { get; set; }

		/// <summary>
		/// True if geometry 3 Lod E exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodEExists")]
		public eBoolean Geometry3LodEExists { get; set; }

		/// <summary>
		/// True if geometry 4 Lod A exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodAExists")]
		public eBoolean Geometry4LodAExists { get; set; }

		/// <summary>
		/// True if geometry 4 Lod B exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodBExists")]
		public eBoolean Geometry4LodBExists { get; set; }

		/// <summary>
		/// True if geometry 4 Lod C exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodCExists")]
		public eBoolean Geometry4LodCExists { get; set; }

		/// <summary>
		/// True if geometry 4 Lod D exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodDExists")]
		public eBoolean Geometry4LodDExists { get; set; }

		/// <summary>
		/// True if geometry 4 Lod E exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodEExists")]
		public eBoolean Geometry4LodEExists { get; set; }

		/// <summary>
		/// True if geometry 5 Lod A exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodAExists")]
		public eBoolean Geometry5LodAExists { get; set; }

		/// <summary>
		/// True if geometry 5 Lod B exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodBExists")]
		public eBoolean Geometry5LodBExists { get; set; }

		/// <summary>
		/// True if geometry 5 Lod C exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodCExists")]
		public eBoolean Geometry5LodCExists { get; set; }

		/// <summary>
		/// True if geometry 5 Lod D exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodDExists")]
		public eBoolean Geometry5LodDExists { get; set; }

		/// <summary>
		/// True if geometry 5 Lod E exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodEExists")]
		public eBoolean Geometry5LodEExists { get; set; }

		/// <summary>
		/// True if geometry 6 Lod A exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodAExists")]
		public eBoolean Geometry6LodAExists { get; set; }

		/// <summary>
		/// True if geometry 6 Lod B exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodBExists")]
		public eBoolean Geometry6LodBExists { get; set; }

		/// <summary>
		/// True if geometry 6 Lod C exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodCExists")]
		public eBoolean Geometry6LodCExists { get; set; }

		/// <summary>
		/// True if geometry 6 Lod D exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodDExists")]
		public eBoolean Geometry6LodDExists { get; set; }

		/// <summary>
		/// True if geometry 6 Lod E exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodEExists")]
		public eBoolean Geometry6LodEExists { get; set; }

		/// <summary>
		/// True if geometry 7 Lod A exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodAExists")]
		public eBoolean Geometry7LodAExists { get; set; }

		/// <summary>
		/// True if geometry 7 Lod B exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodBExists")]
		public eBoolean Geometry7LodBExists { get; set; }

		/// <summary>
		/// True if geometry 7 Lod C exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodCExists")]
		public eBoolean Geometry7LodCExists { get; set; }

		/// <summary>
		/// True if geometry 7 Lod D exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodDExists")]
		public eBoolean Geometry7LodDExists { get; set; }

		/// <summary>
		/// True if geometry 7 Lod E exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodEExists")]
		public eBoolean Geometry7LodEExists { get; set; }

		/// <summary>
		/// True if geometry 8 Lod A exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodAExists")]
		public eBoolean Geometry8LodAExists { get; set; }

		/// <summary>
		/// True if geometry 8 Lod B exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodBExists")]
		public eBoolean Geometry8LodBExists { get; set; }

		/// <summary>
		/// True if geometry 8 Lod C exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodCExists")]
		public eBoolean Geometry8LodCExists { get; set; }

		/// <summary>
		/// True if geometry 8 Lod D exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodDExists")]
		public eBoolean Geometry8LodDExists { get; set; }

		/// <summary>
		/// True if geometry 8 Lod E exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodEExists")]
		public eBoolean Geometry8LodEExists { get; set; }

		/// <summary>
		/// True if geometry 9 Lod A exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodAExists")]
		public eBoolean Geometry9LodAExists { get; set; }

		/// <summary>
		/// True if geometry 9 Lod B exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodBExists")]
		public eBoolean Geometry9LodBExists { get; set; }

		/// <summary>
		/// True if geometry 9 Lod C exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodCExists")]
		public eBoolean Geometry9LodCExists { get; set; }

		/// <summary>
		/// True if geometry 9 Lod D exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodDExists")]
		public eBoolean Geometry9LodDExists { get; set; }

		/// <summary>
		/// True if geometry 9 Lod E exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodEExists")]
		public eBoolean Geometry9LodEExists { get; set; }

		/// <summary>
		/// True if geometry 10 Lod A exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodAExists")]
		public eBoolean Geometry10LodAExists { get; set; }

		/// <summary>
		/// True if geometry 10 Lod B exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodBExists")]
		public eBoolean Geometry10LodBExists { get; set; }

		/// <summary>
		/// True if geometry 10 Lod C exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodCExists")]
		public eBoolean Geometry10LodCExists { get; set; }

		/// <summary>
		/// True if geometry 10 Lod D exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodDExists")]
		public eBoolean Geometry10LodDExists { get; set; }

		/// <summary>
		/// True if geometry 10 Lod E exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodEExists")]
		public eBoolean Geometry10LodEExists { get; set; }

		/// <summary>
		/// True if geometry 11 Lod A exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodAExists")]
		public eBoolean Geometry11LodAExists { get; set; }

		/// <summary>
		/// True if geometry 11 Lod B exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodBExists")]
		public eBoolean Geometry11LodBExists { get; set; }

		/// <summary>
		/// True if geometry 11 Lod C exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodCExists")]
		public eBoolean Geometry11LodCExists { get; set; }

		/// <summary>
		/// True if geometry 11 Lod D exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodDExists")]
		public eBoolean Geometry11LodDExists { get; set; }

		/// <summary>
		/// True if geometry 11 Lod E exists; false otherwise.
		/// </summary>
		[AccessModifiable()]
		[Category("GeometryLodEExists")]
		public eBoolean Geometry11LodEExists { get; set; }

		#endregion

		/// <summary>
		/// Initializes new instance of <see cref="ModelTableAttribute"/>.
		/// </summary>
		public ModelTableAttribute() { }

		/// <summary>
		/// Initializes new instance of <see cref="ModelTableAttribute"/> with value provided.
		/// </summary>
		/// <param name="value">Value to set.</param>
		public ModelTableAttribute(object value)
		{
			try
			{

				this.Templated = (int)value.ReinterpretCast(typeof(int)) == 0
					? eBoolean.False
					: eBoolean.True;

			}
			catch (Exception)
			{

				this.Templated = eBoolean.False;

			}
		}

		/// <summary>
		/// Initializes new instance of <see cref="ModelTableAttribute"/> by reading data using 
		/// <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="key">Key of the attribute's group.</param>
		public ModelTableAttribute(BinaryReader br, uint key)
		{
			this.Key = key;
			this.Disassemble(br, null);
		}

		/// <summary>
		/// Disassembles byte array into <see cref="ModelTableAttribute"/> using <see cref="BinaryReader"/> 
		/// provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with.</param>
		/// <param name="str_reader"><see cref="BinaryReader"/> to read strings with. 
		/// Since it is an Model Table Attribute, this value can be <see langword="null"/>.</param>
		public override void Disassemble(BinaryReader br, BinaryReader str_reader) =>
			this.Index = br.ReadInt32();

		/// <summary>
		/// Reads struct settings using <see cref="BinaryReader"/> provided.
		/// </summary>
		/// <param name="br"><see cref="BinaryReader"/> to read with model table with.</param>
		/// <param name="str_reader"><see cref="BinaryReader"/> to read strings with.</param>
		public void ReadStruct(BinaryReader br, BinaryReader str_reader)
		{
			if (this.Index > 0x864B89) return; // if index is too big and overflowing

			// Size of CPStruct is 0xF4. Advance position based on the index.
			br.BaseStream.Position = this.Index * 0xF4;
			if (br.BaseStream.Position + 0xF4 > br.BaseStream.Length) return;
			const uint negative = 0xFFFFFFFF;

			// Read data
			this.Templated = br.ReadInt16() == 0 ? eBoolean.False : eBoolean.True;

			if (this.Templated == eBoolean.True)
			{

				// Read concatenator
				long position = br.ReadUInt16();

				if (position != 0xFFFF)
				{

					str_reader.BaseStream.Position = position << 2;
					this.Concatenator = str_reader.ReadNullTermUTF8();
					this.ConcatenatorExists = eBoolean.True;

				}

				for (int lod = (byte)'A'; lod <= (byte)'E'; ++lod)
				{

					for (int index = 0; index <= 11; ++index)
					{

						position = br.ReadUInt32();

						if (position != negative)
						{

							str_reader.BaseStream.Position = position << 2;
							var lodname = $"Geometry{index}Lod{(char)lod}";
							var lodexists = $"{lodname}Exists";
							this.GetFastProperty(lodname).SetValue(this, str_reader.ReadNullTermUTF8());
							this.GetFastProperty(lodexists).SetValue(this, eBoolean.True);

						}

					}

				}

			}
			else
			{
				br.BaseStream.Position += 2; // skip concatenator

				for (int lod = (byte)'A'; lod <= (byte)'E'; ++lod)
				{

					for (int index = 0; index <= 11; ++index)
					{

						var key = br.ReadUInt32();

						if (key != negative)
						{

							var lodname = $"Geometry{index}Lod{(char)lod}";
							var lodexists = $"{lodname}Exists";
							this.GetFastProperty(lodname).SetValue(this, key.BinString(LookupReturn.EMPTY));
							this.GetFastProperty(lodexists).SetValue(this, eBoolean.True);

						}

					}

				}

			}
		}

		/// <summary>
		/// Assembles <see cref="ModelTableAttribute"/> and writes it using <see cref="BinaryWriter"/> 
		/// provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write with.</param>
		/// <param name="string_dict">Dictionary of string HashCodes and their offsets. 
		/// Since it is an Model Table Attribute, this value can be <see langword="null"/>.</param>
		public override void Assemble(BinaryWriter bw, Dictionary<int, int> string_dict)
		{
			bw.Write(this.Key);
			bw.Write(this.Index);
		}

		/// <summary>
		/// Writes struct settings using <see cref="BinaryWriter"/> provided.
		/// </summary>
		/// <param name="bw"><see cref="BinaryWriter"/> to write with model table with.</param>
		/// <param name="string_dict">Dictionary of string HashCodes and their offsets.</param>
		public void WriteStruct(BinaryWriter bw, Dictionary<int, int> string_dict)
		{
			uint neguint32 = 0xFFFFFFFF;
			int negint32 = -1;
			int empty = String.Empty.GetHashCode();

			if (this.Templated == eBoolean.True)
			{

				bw.Write((ushort)1);
				bw.Write(this.ConcatenatorExists == eBoolean.False
					? (ushort)negint32
					: (ushort)string_dict[this.Concatenator?.GetHashCode() ?? empty]);

				for (int lod = (byte)'A'; lod <= (byte)'E'; ++lod)
				{

					for (int index = 0; index <= 11; ++index)
					{

						var name = $"Geometry{index}Lod{(char)lod}";
						var lodname = (string)this.GetFastPropertyValue(name);
						var lodexists = (eBoolean)this.GetFastPropertyValue($"{name}Exists");
						bw.Write(lodexists == eBoolean.False
							? negint32
							: string_dict[lodname?.GetHashCode() ?? empty]);


					}

				}

			}
			else
			{

				bw.Write(0xFFFF0000);

				for (int lod = (byte)'A'; lod <= (byte)'E'; ++lod)
				{

					for (int index = 0; index <= 11; ++index)
					{

						var name = $"Geometry{index}Lod{(char)lod}";
						var lodname = (string)this.GetFastPropertyValue(name);
						var lodexists = (eBoolean)this.GetFastPropertyValue($"{name}Exists");
						bw.Write(lodexists == eBoolean.False
							? neguint32
							: lodname.BinHash());

					}

				}

			}
		}

		/// <summary>
		/// Returns attribute part label and its type as a string value.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString() => this.Type.ToString();

		/// <summary>
		/// Determines whether this instance and a specified object, which must also be a
		/// <see cref="ModelTableAttribute"/> object, have the same value.
		/// </summary>
		/// <param name="obj">The <see cref="ModelTableAttribute"/> to compare to this instance.</param>
		/// <returns>True if obj is a <see cref="ModelTableAttribute"/> and its value is the same as 
		/// this instance; false otherwise. If obj is null, the method returns false.
		/// </returns>
		public override bool Equals(object obj) =>
			obj is ModelTableAttribute attribute && this == attribute;

		/// <summary>
		/// Returns the hash code for this <see cref="ModelTableAttribute"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			int result = 0x25;
			var properties = this.GetAccessibles().ToList();
			properties.Sort();

			foreach (var property in properties)
			{

				result = HashCode.Combine(result, this.GetValue(property).GetSafeHashCode());

			}

			return result;
		}

		private bool ValueEquals(ModelTableAttribute other)
		{
			bool result = true;
			var properties = this.GetAccessibles();

			foreach (var property in properties)
			{

				var thisvalue = this.GetFastPropertyValue(property);
				var othervalue = other.GetFastPropertyValue(property);
				result &= thisvalue.Equals(othervalue);

			}

			return result;
		}

		/// <summary>
		/// Determines whether two specified <see cref="ModelTableAttribute"/> have the same value.
		/// </summary>
		/// <param name="at1">The first <see cref="ModelTableAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="ModelTableAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is the same as the value of c2; false otherwise.</returns>
		public static bool operator ==(ModelTableAttribute at1, ModelTableAttribute at2)
		{
			if (at1 is null) return at2 is null;
			else if (at2 is null) return false;

			return at1.ValueEquals(at2);
		}

		/// <summary>
		/// Determines whether two specified <see cref="ModelTableAttribute"/> have different values.
		/// </summary>
		/// <param name="at1">The first <see cref="ModelTableAttribute"/> to compare, or null.</param>
		/// <param name="at2">The second <see cref="ModelTableAttribute"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is different from the value of c2; false otherwise.</returns>
		public static bool operator !=(ModelTableAttribute at1, ModelTableAttribute at2) => !(at1 == at2);

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new ModelTableAttribute
			{
				Type = this.Type,
				Index = this.Index,
				Templated = this.Templated,
				Concatenator = this.Concatenator,
				ConcatenatorExists = this.ConcatenatorExists
			};

			var properties = this.GetAccessibles();
			
			foreach (var property in properties)
			{
			
				if (!property.StartsWith("Geometry")) continue;
				var resprop = result.GetFastProperty(property);
				var value = this.GetFastPropertyValue(property);
				resprop.SetValue(result, value);
			
			}

			return result;
		}

		/// <summary>
		/// Converts this <see cref="ModelTableAttribute"/> to an attribute of type provided.
		/// </summary>
		/// <param name="type">Type of a new attribute.</param>
		/// <returns>New <see cref="CPAttribute"/>.</returns>
		public override CPAttribute ConvertTo(CarPartAttribType type) =>
			type switch
			{
				CarPartAttribType.Boolean => new BoolAttribute(this.Templated),
				CarPartAttribType.Floating => new FloatAttribute(this.Templated),
				CarPartAttribType.Integer => new IntAttribute(this.Templated),
				CarPartAttribType.String => new StringAttribute(this.Templated),
				CarPartAttribType.TwoString => new TwoStringAttribute(this.Templated),
				CarPartAttribType.Color => new ColorAttribute(this.Templated),
				CarPartAttribType.CarPartID => new PartIDAttribute(this.Templated),
				CarPartAttribType.Key => new KeyAttribute(this.Templated),
				_ => this
			};

		/// <summary>
		/// Serializes instance into a byte array and stores it in the file provided.
		/// </summary>
		public override void Serialize(BinaryWriter bw)
		{
			bw.Write(this.Key);
			bw.WriteEnum(this.Templated);
			bw.WriteEnum(this.ConcatenatorExists);

			if (this.ConcatenatorExists == eBoolean.True)
			{

				bw.WriteNullTermUTF8(this.Concatenator);

			}

			for (int lod = (byte)'A'; lod <= (byte)'E'; ++lod)
			{

				for (int index = 0; index <= 11; ++index)
				{

					var name = $"Geometry{index}Lod{(char)lod}";
					var lodname = (string)this.GetFastPropertyValue(name);
					var lodexists = (eBoolean)this.GetFastPropertyValue($"{name}Exists");

					bw.WriteEnum(lodexists);

					if (lodexists == eBoolean.True)
					{

						bw.WriteNullTermUTF8(lodname);

					}

				}

			}
		}

		/// <summary>
		/// Deserializes byte array into an instance by loading data from the file provided.
		/// </summary>
		public override void Deserialize(BinaryReader br)
		{
			this.Templated = br.ReadEnum<eBoolean>();
			this.ConcatenatorExists = br.ReadEnum<eBoolean>();

			if (this.ConcatenatorExists == eBoolean.True)
			{

				this.Concatenator = br.ReadNullTermUTF8();

			}

			for (int lod = (byte)'A'; lod <= (byte)'E'; ++lod)
			{

				for (int index = 0; index <= 11; ++index)
				{

					var name = $"Geometry{index}Lod{(char)lod}";
					var lodexists = this.GetFastProperty($"{name}Exists");

					var exists = br.ReadEnum<eBoolean>();
					lodexists.SetValue(this, exists);

					if (exists == eBoolean.True)
					{

						this.GetFastProperty(name).SetValue(this, br.ReadNullTermUTF8());

					}

				}

			}
		}
	}
}
