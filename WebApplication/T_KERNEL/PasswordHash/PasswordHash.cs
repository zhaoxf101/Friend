using System;
using System.Security.Cryptography;

namespace PasswordHash
{
	public class PasswordHash
	{
		public const int SALT_BYTE_SIZE = 24;

		public const int HASH_BYTE_SIZE = 24;

		public const int PBKDF2_ITERATIONS = 1000;

		public const int ITERATION_INDEX = 0;

		public const int SALT_INDEX = 1;

		public const int PBKDF2_INDEX = 2;

		public static string CreateHash(string password)
		{
			RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider();
			byte[] numArray = new byte[24];
			cryptoServiceProvider.GetBytes(numArray);
			byte[] inArray = PasswordHash.PBKDF2(password, numArray, 1000, 24);
			return string.Concat(new object[]
			{
				1000,
				":",
				Convert.ToBase64String(numArray),
				":",
				Convert.ToBase64String(inArray)
			});
		}

		public static bool ValidatePassword(string password, string correctHash)
		{
			char[] chArray = new char[]
			{
				':'
			};
			string[] strArray = correctHash.Split(chArray);
			int iterations = int.Parse(strArray[0]);
			byte[] salt = Convert.FromBase64String(strArray[1]);
			byte[] a = Convert.FromBase64String(strArray[2]);
			byte[] b = PasswordHash.PBKDF2(password, salt, iterations, a.Length);
			return PasswordHash.SlowEquals(a, b);
		}

		private static bool SlowEquals(byte[] a, byte[] b)
		{
			uint num = (uint)(a.Length ^ b.Length);
			int index = 0;
			while (index < a.Length && index < b.Length)
			{
				num |= (uint)(a[index] ^ b[index]);
				index++;
			}
			return num == 0u;
		}

		private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
		{
			return new Rfc2898DeriveBytes(password, salt)
			{
				IterationCount = iterations
			}.GetBytes(outputBytes);
		}
	}
}
