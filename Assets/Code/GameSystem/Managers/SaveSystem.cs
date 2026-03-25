#region Project Details
/*
* Project: Prodigy-Mantle
* Author:Christof Kloninger / gme.24.kloninger@gmail.com
* Issue: Link: https://github.com/Wasted-Resources/Prodigy-Mantle/issues/21
* Source: Older project: [Insert the link to the project]
* Date: 2026-03-23
*/
#endregion


#region Basic Instruction
/*
Structure the class into regions as appropriate for their use case.
The regions should separate what is viewed or used in an inspector, class intern relevant fields, Public Getters if necessary, 
Use top comments above methods to describe them and explain their parameters.
TODO comments above a method or codeblock
Use side comments in line to describe lines that obfuscate their function as explanation
*/
#endregion


#region Development remarks
/// <remarks>
/// <para>
/// This class handles [Core Responsibility]. It must maintain [Architecture Constraint, e.g., Singleton].
/// </para>
/// </remarks>
/// <summary>
/// Description: [Describe what this class does].
/// Coordination: [How it communicates with APIs or other Components].
/// Deployment: [Where it should live in the Scene, Project, Assets'].
/// </summary>
#endregion


using UnityEngine;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Copied from older project, probably needs some adjustments
/// </summary>
public static class SaveSystem
{
#region Encryption/Decryption
    private static readonly byte[] Key = Encoding.UTF8.GetBytes( "0123456789abcdef" ) ;
    private static readonly byte[] Iv = Encoding.UTF8.GetBytes( "abcdef0123456789" ) ;
    public static byte[] Encrypt( string plainText )
    {
        if ( string.IsNullOrEmpty( plainText ))  throw new ArgumentNullException( nameof( plainText ) );

        using Aes aes = Aes.Create();
        aes.Key = Key;
        aes.IV = Iv;
        using MemoryStream memoryStream = new() ;
        ICryptoTransform encryptor = aes.CreateEncryptor( aes.Key , aes.IV ) ;
        using (CryptoStream cryptoStream = new( memoryStream , encryptor , CryptoStreamMode.Write ) )
        using (StreamWriter writer = new( cryptoStream , Encoding.UTF8 ) )
        {
        writer.Write( plainText ) ;
        }
        return memoryStream.ToArray();
    }

    public static string Decrypt(byte[] cipherData)
    {
        if (cipherData == null || cipherData.Length == 0)
            throw new ArgumentNullException(nameof(cipherData));

        using Aes aes = Aes.Create();
        aes.Key = Key;
        aes.IV = Iv;
        using MemoryStream memoryStream = new(cipherData);
        using CryptoStream cryptoStream = new(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using StreamReader reader = new(cryptoStream, Encoding.UTF8);
        return reader.ReadToEnd();
    }
#endregion


#region De-/Serialization
    public static string SerializeData( object obj ) { return JsonUtility.ToJson( obj ) ; }
    public static T DeserializeData<T>(string jsonData) { return JsonUtility.FromJson<T>(jsonData) ; }
#endregion
}