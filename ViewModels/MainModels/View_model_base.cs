using CommunityToolkit.Mvvm.ComponentModel;
using ReactiveUI;
using System.Text;
using System.Security.Cryptography;
using System;

namespace appointment_management_system.ViewModels;

public abstract class View_model_base : ReactiveObject{
    //functionality functions
    public string SHA256HashCreator(string unhashed_password){
        string hash;
        using(SHA256 SHA256_hash = SHA256.Create()){
             // Byte array representation of source string
            byte[] source_bytes = Encoding.UTF8.GetBytes(unhashed_password);

            // Generate hash value(Byte Array) for input data
            byte[] hash_bytes = SHA256_hash.ComputeHash(source_bytes);

            // Convert hash byte array to string
            hash = BitConverter.ToString(hash_bytes).Replace("-", string.Empty);
        }
        return hash;
    }
}
