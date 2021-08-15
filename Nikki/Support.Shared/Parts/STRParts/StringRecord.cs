using System;
using Nikki.Reflection.Abstract;
using Nikki.Reflection.Exception;
using Nikki.Support.Shared.Class;
using CoreExtensions.Text;



namespace Nikki.Support.Shared.Parts.STRParts
{
	/// <summary>
	/// Represents unit string record, which consists of key, label and text.
	/// </summary>
	public class StringRecord : SubPart
	{
		/// <summary>
		/// Key of the label as a BinHash.
		/// </summary>
		public uint Key { get; set; }

		/// <summary>
		/// Label of this <see cref="StringRecord"/>.
		/// </summary>
		public string Label { get; set; }

		/// <summary>
		/// Text of this <see cref="StringRecord"/>.
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Length of the label with null-termination.
		/// </summary>
		public int NulledLabelLength => (this.Label == null) ? 0 : this.Label.Length + 1;

		/// <summary>
		/// Length of the text with null-termination.
		/// </summary>
		public int NulledTextLength => (this.Text == null) ? 0 : this.Text.Length + 1;

		/// <summary>
		/// Constant string value "Key".
		/// </summary>
		public const string key = "Key";

		/// <summary>
		/// Constant string value "Label".
		/// </summary>
		public const string label = "Label";

		/// <summary>
		/// Constant string value "Text".
		/// </summary>
		public const string text = "Text";

		/// <summary>
		/// <see cref="STRBlock"/> to which this <see cref="StringRecord"/> belongs to.
		/// </summary>
		public STRBlock ThisSTRBlock { get; set; }

		/// <summary>
		/// Initializes new instance of <see cref="StringRecord"/>.
		/// </summary>
		/// <param name="block"><see cref="STRBlock"/> to which this 
		/// <see cref="StringRecord"/> belongs to.</param>
		public StringRecord(STRBlock block)
		{
			this.Label = String.Empty;
			this.ThisSTRBlock = block;
		}

		/// <summary>
		/// Determines whether this instance and a specified object, which must also be a
		/// <see cref="StringRecord"/> object, have the same value.
		/// </summary>
		/// <param name="obj">The <see cref="StringRecord"/> to compare to this instance.</param>
		/// <returns>True if obj is a <see cref="StringRecord"/> and its value is the same as 
		/// this instance; false otherwise. If obj is null, the method returns false.
		/// </returns>
		public override bool Equals(object obj) => obj is StringRecord record && this == record;

		/// <summary>
		/// Returns the hash code for this <see cref="StringRecord"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode() =>
			Tuple.Create(this.Key, this.Label ?? string.Empty, this.Text ?? string.Empty).GetHashCode();

		/// <summary>
		/// Determines whether two specified <see cref="StringRecord"/> have the same value.
		/// </summary>
		/// <param name="s1">The first <see cref="StringRecord"/> to compare, or null.</param>
		/// <param name="s2">The second <see cref="StringRecord"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is the same as the value of c2; false otherwise.</returns>
		public static bool operator== (StringRecord s1, StringRecord s2)
		{
			if (s1 is null) return s2 is null;
			else if (s2 is null) return false;
			return s1.Key == s2.Key;
		}

		/// <summary>
		/// Determines whether two specified <see cref="StringRecord"/> have different values.
		/// </summary>
		/// <param name="s1">The first <see cref="StringRecord"/> to compare, or null.</param>
		/// <param name="s2">The second <see cref="StringRecord"/> to compare, or null.</param>
		/// <returns>True if the value of c1 is different from the value of c2; false otherwise.</returns>
		public static bool operator!= (StringRecord s1, StringRecord s2) => !(s1 == s2);

		/// <summary>
		/// Returns key, label and text of this <see cref="StringRecord"/> as a string.
		/// </summary>
		/// <returns>String value.</returns>
		public override string ToString()
		{
			return $"{key}: 0x{this.Key:X8} | {label}: {this.Label} | {text}: {this.Text}";
		}

		/// <summary>
		/// Sets value provided at the <see cref="StringRecord"/> property specified. 
		/// </summary>
		/// <param name="PropertyName">Property of the <see cref="StringRecord"/>. Range: 
		/// Key, Label, Text.</param>
		/// <param name="value">Value to set.</param>
		public void SetValue(string PropertyName, string value)
		{
			switch (PropertyName)
			{
				case key:
					if (!value.IsHexString())
					{

						throw new ArgumentException("Unable to convert key passed to a hex-hash, or it equals 0");

					}

					var hash = Convert.ToUInt32(value, 16);

					if (hash == this.Key) return;

					if (this.ThisSTRBlock.GetRecord(hash) != null)
					{

						throw new ArgumentException($"StringRecord with key {value} already exist");

					}

					this.Key = hash;
					return;

				case label:
					this.Label = value;
					return;

				case text:
					this.Text = value;
					return;

				default:
					throw new InfoAccessException(PropertyName);
			}
		}

		/// <summary>
		/// Creates a plain copy of the objects that contains same values.
		/// </summary>
		/// <returns>Exact plain copy of the object.</returns>
		public override SubPart PlainCopy()
		{
			var result = new StringRecord(this.ThisSTRBlock)
			{
				Key = this.Key,
				Label = this.Label,
				Text = this.Text
			};

			return result;
		}
	}
}