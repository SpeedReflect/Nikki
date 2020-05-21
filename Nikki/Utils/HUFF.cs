using System;
using System.Collections.Generic;
using System.Text;
using CoreExtensions.Text;



namespace Nikki.Utils
{
	/// <summary>
	/// Collection with HUFF compresssor and decompressor.
	/// </summary>
	public static class HUFF
	{
		private class Node
		{
			public byte? Character { get; set; } = null;
			public int Frequency { get; set; }
			public Node Left { get; set; }
			public Node Right { get; set; }

			public Node() { }

			public Node(byte value, int freq)
			{
				this.Character = value;
				this.Frequency = freq;
			}

			public Node(byte value, int freq, Node left, Node right)
			{
				this.Character = value;
				this.Frequency = freq;
				this.Left = left;
				this.Right = right;
			}

			public bool Empty() => this.Left == null && this.Right == null;
		}

		private static void GenEncodingMap(Node root, string str, Dictionary<byte, string> map)
		{
			if (root == null) return;

			if (root.Left == null && root.Right == null)
				map[(byte)root.Character] = str;

			GenEncodingMap(root.Left, str + "0", map);
			GenEncodingMap(root.Right, str + "1", map);
		}

		private static int Decode(Node root, int index, string str)
		{
			if (root == null) return index;

			if (root.Left == null && root.Right == null)
			{
				return index;
			}

			++index;

			index = str[index] == '0'
				? Decode(root.Left, index, str)
				: Decode(root.Right, index, str);

			return index;
		}
	
		/// <summary>
		/// Attempts to decompress HUFF byte array. It is not precise when node table is missing.
		/// </summary>
		/// <param name="buffer">Byte buffer to decompress.</param>
		/// <returns>Byte array of decompressed data.</returns>
		public static byte[] Decompress(byte[] buffer)
		{
			if (buffer == null || buffer.Length == 0) return null;

			int encodelen = 0;
			int decodelen = 0;

			unsafe
			{
				fixed (byte* byteptr_t = &buffer[0])
				{
					if (*(uint*)byteptr_t != 0x46465548 &&
						*(int*)(byteptr_t + 4) != 0x00001001)
						return buffer;
					decodelen = *(int*)(byteptr_t + 0x8);
					encodelen = *(int*)(byteptr_t + 0xC);
				}
			}

			string encoded = String.Empty;
			for (int a1 = 0x10; a1 < encodelen + 0x10; ++a1)
			{
				for (int a2 = 7; a2 >= 0; --a2)
					encoded += ((buffer[a1] >> a2) & 1).ToString();
			}

			var table = new List<Node>();
			var root = new Node();
			table.Add(root);

			Node node = null;
			int index = 1;

			while (table.Count != 0 && index < encoded.Length)
			{
				if (encoded[index] == '0') node = new Node();
				else if (encoded[index] == '1')
				{

					string sub = encoded.Substring(index + 1, 8);
					byte value = Convert.ToByte(sub, 2);
					node = new Node(value, 0);
					index += 8;
				}

				if (table[^1].Left == null) table[^1].Left = node;
				else
				{
					table[^1].Right = node;
					table.RemoveAt(table.Count - 1);
				}

				if (node.Character == null) table.Add(node);

				index++;
			}

			var overflow = encoded[index..];
			int overlen = overflow.Length;

			var result = new byte[decodelen];
			int count = 0;
			index = 0;

			for (int x = 0; x < decodelen; x++)
			{
				var current = root;
				while (!current.Empty() && index < overlen)
				{
					current = overflow[index] == '0'
						? current.Left
						: current.Right;
					++index;
				}
				result[count++] = (byte)current.Character;
			}

			return result;
		}

		/// <summary>
		/// Compresses byte array into HUFF-compressed one.
		/// </summary>
		/// <param name="buffer">Byte buffer to compress.</param>
		/// <returns>HUFF-compressed byte array.</returns>
		public static byte[] Compress(byte[] buffer)
		{
			if (buffer == null || buffer.Length == 0) return null;

			var freq = new Dictionary<byte, int>();
			for (int a1 = 0; a1 < buffer.Length; ++a1)
			{
				var ch = buffer[a1];
				freq[ch] = !freq.ContainsKey(buffer[a1])
					? 1
					: freq[ch] + 1;
			}

			var queue = new List<Node>(freq.Count);

			foreach (var pair in freq)
				queue.Add(new Node(pair.Key, pair.Value));

			queue.Sort((x, y) => x.Frequency - y.Frequency);

			while (queue.Count != 1)
			{
				var left = queue[0];
				var right = queue[1];
				queue.RemoveRange(0, 2);

				int sum = left.Frequency + right.Frequency;
				queue.Add(new Node(0, sum, left, right));
				queue.Sort((x, y) => x.Frequency - y.Frequency);
			}

			var root = queue[^1];

			var huffcode = new Dictionary<byte, string>();
			GenEncodingMap(root, String.Empty, huffcode);

			var builder = new StringBuilder(buffer.Length * 8);
			for (int a1 = 0; a1 < buffer.Length; ++a1)
				builder.Append(huffcode[buffer[a1]]);

			while (builder.Length % 8 != 0) builder.Append("0");

			var bytes = builder.ToString().SplitByLength(8);
			var result = new byte[builder.Length / 8 + 0x10];

			unsafe
			{
				fixed (byte* byteptr_t = &result[0])
				{
					*(uint*)byteptr_t = 0x46465548;
					*(int*)(byteptr_t + 4) = 0x00001001;
					*(int*)(byteptr_t + 8) = buffer.Length;
					*(int*)(byteptr_t + 12) = result.Length - 0x10;
				}
			}

			int count = 0x10;
			foreach (var sub in bytes)
			{
				int entry = 0;
				for (int a1 = 0; a1 < 8; ++a1)
				{
					var b = (byte)sub[7 - a1] & 1;
					entry |= b << a1;
				}
				result[count++] = (byte)entry;
			}

			return result;
		}
	}
}
