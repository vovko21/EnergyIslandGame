using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class AesEncryptorDataContext : DataContext
{
    private string _filepath = Application.persistentDataPath + "/contex.data";
    private string Key = "aGRvbmRnaHRzZG11eXRyOw=="; // must be a valid base64 format from string in 16 char
    private string IV = "bWNiZGdzcGVnZGhmbmdqZA==";

    public override async System.Threading.Tasks.Task SaveAsync()
    {
        using FileStream stream = File.Create(_filepath);
        _data.Initialized = true;
        await EncryptAsync(stream);
    }

    public override async System.Threading.Tasks.Task LoadAsync()
    {
        if (!File.Exists(_filepath)) return;
        await DecryptAsync();
    }

    public async System.Threading.Tasks.Task EncryptAsync(FileStream stream)
    {
        using Aes aesProvider = Aes.Create();

        try
        {
            aesProvider.Key = Convert.FromBase64String(Key);
            aesProvider.IV = Convert.FromBase64String(IV);
        }
        catch (Exception ex)
        {
            throw;
        }

        using ICryptoTransform cpyptoTransform = aesProvider.CreateEncryptor();
        using CryptoStream cryptoStream = new CryptoStream(
            stream,
            cpyptoTransform,
            CryptoStreamMode.Write
        );

        await cryptoStream.WriteAsync(Encoding.ASCII.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(_data)));
    }

    public async System.Threading.Tasks.Task DecryptAsync()
    {
        byte[] fileBytes = File.ReadAllBytes(_filepath);
        using Aes aesProvider = Aes.Create();

        try
        {
            aesProvider.Key = Convert.FromBase64String(Key);
            aesProvider.IV = Convert.FromBase64String(IV);
        }
        catch (Exception ex)
        {
            throw;
        }

        using ICryptoTransform cpyptoTransform = aesProvider.CreateDecryptor(
             aesProvider.Key,
             aesProvider.IV
        );

        using MemoryStream decryptionStream = new MemoryStream(fileBytes);
        using CryptoStream cryptoStream = new CryptoStream(
            decryptionStream,
            cpyptoTransform,
            CryptoStreamMode.Read
        );

        using StreamReader reader = new StreamReader(cryptoStream);
        string result = await reader.ReadToEndAsync();

        _data = JsonConvert.DeserializeObject<GameData>(result);
    }
}
