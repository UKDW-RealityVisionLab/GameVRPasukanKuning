using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PrefabLoaderAddressables : MonoBehaviour
{
    // Opsi 1: Menggunakan AssetReference (Direkomendasikan untuk referensi langsung di Inspector)
    // Seret prefab Addressable ke slot ini di Inspector
    [SerializeField] // Membuat variabel private terlihat di Inspector
    private AssetReferenceGameObject _prefabReference;

    // Opsi 2: Menggunakan String (Jika Anda hanya memiliki alamat/path sebagai string)
    // Anda bisa mendapatkan alamat dari Inspector setelah prefab dijadikan Addressable,
    // atau jika Anda memiliki konvensi penamaan alamat yang jelas.
    public string prefabAddress = "MyAwesomePrefab"; // Ganti dengan alamat prefab Anda

    void Start()
    {
        // Panggil salah satu metode pemuatan di sini, tergantung pilihan Anda
        LoadPrefabUsingAssetReference();
        // LoadPrefabUsingStringAddress(); // Atau yang ini
    }

    /// <summary>
    /// Memuat prefab menggunakan AssetReference yang sudah diassign di Inspector.
    /// Ini adalah cara paling umum dan disarankan.
    /// </summary>
    private void LoadPrefabUsingAssetReference()
    {
        if (_prefabReference.RuntimeKeyIsValid()) // Pastikan referensi valid
        {
            Debug.Log($"Memulai pemuatan prefab dari AssetReference: {_prefabReference.RuntimeKey}");

            // LoadAssetAsync akan mengembalikan AsyncOperationHandle
            // yang akan menahan hasil pemuatan secara asynchronous.
            AsyncOperationHandle<GameObject> loadHandle = _prefabReference.LoadAssetAsync<GameObject>();

            // Daftarkan callback untuk dijalankan saat operasi selesai
            loadHandle.Completed += OnPrefabLoaded;
        }
        else
        {
            Debug.LogError("AssetReference untuk prefab belum diassign di Inspector atau tidak valid!");
        }
    }

    /// <summary>
    /// Memuat prefab menggunakan alamat (string). Berguna jika alamat ditentukan secara dinamis.
    /// </summary>
    private void LoadPrefabUsingStringAddress()
    {
        if (!string.IsNullOrEmpty(prefabAddress))
        {
            Debug.Log($"Memulai pemuatan prefab dari alamat: {prefabAddress}");

            // PENTING: Pastikan alamat ini persis sama dengan alamat yang Anda atur di Addressables Groups.
            // Addressables.LoadAssetAsync<TObject>(object key)
            AsyncOperationHandle<GameObject> loadHandle = Addressables.LoadAssetAsync<GameObject>(prefabAddress);

            // Daftarkan callback untuk dijalankan saat operasi selesai
            loadHandle.Completed += OnPrefabLoaded;
        }
        else
        {
            Debug.LogError("Alamat prefab (string) kosong atau tidak valid!");
        }
    }

    /// <summary>
    /// Callback yang dipanggil setelah operasi pemuatan prefab selesai.
    /// </summary>
    /// <param name="handle">Handle operasi asynchronous.</param>
    private void OnPrefabLoaded(AsyncOperationHandle<GameObject> handle)
    {
        // Periksa status operasi
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            // Ambil hasil objek yang dimuat
            GameObject loadedPrefab = handle.Result;

            // Instansiasi prefab yang dimuat
            GameObject instance = Instantiate(loadedPrefab, Vector3.zero, Quaternion.identity);
            instance.name = loadedPrefab.name + "_Instance"; // Beri nama untuk identifikasi

            Debug.Log($"Prefab '{loadedPrefab.name}' berhasil dimuat dan di-instantiate!");

            // Penting: Anda TIDAK perlu memanggil Addressables.Release(handle) di sini
            // jika Anda memuatnya melalui AssetReference.
            // AssetReference akan mengelola siklus hidup aset secara otomatis ketika ReleaseAsset() dipanggil
            // atau GameObject yang mengandung AssetReference dihancurkan.
            // Jika Anda memuat menggunakan Addressables.LoadAssetAsync(string address),
            // Anda HARUS memanggil Addressables.Release(handle) ketika Anda selesai menggunakan aset tersebut
            // untuk mencegah kebocoran memori.
            // Contohnya bisa di OnDestroy() jika instance objek yang dimuat akan dihancurkan.
        }
        else if (handle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogError($"Gagal memuat prefab. Status: {handle.Status}, Exception: {handle.OperationException}");
        }
        else
        {
            // Ini bisa terjadi jika operasi masih berjalan (jarang terjadi di callback Completed)
            Debug.LogWarning($"Status pemuatan prefab tidak sesuai: {handle.Status}");
        }
    }

    /// <summary>
    /// Penting: Merilis aset yang dimuat dari Addressables.
    /// Jika Anda memuat menggunakan `Addressables.LoadAssetAsync(string)`,
    /// Anda harus memanggil `Addressables.Release(handle)` atau `Addressables.Release(object)`
    /// untuk melepaskan aset dari memori ketika tidak lagi dibutuhkan.
    /// Jika Anda menggunakan `AssetReference`, ini dikelola lebih otomatis.
    /// </summary>
    void OnDestroy()
    {
        // Contoh untuk merilis jika Anda menggunakan `Addressables.LoadAssetAsync(string address)`
        // Pastikan Anda menyimpan handle-nya di suatu tempat (misalnya, variabel anggota)
        // dan merilisnya di sini.
        // if (myManualLoadHandle.IsValid() && myManualLoadHandle.IsDone)
        // {
        //     Addressables.Release(myManualLoadHandle);
        //     Debug.Log("Handle prefab manual telah dirilis.");
        // }

        // Jika Anda menggunakan AssetReference, panggil ReleaseAsset() pada AssetReference tersebut.
        // Ini akan melepaskan aset yang dimuat oleh referensi ini.
        if (_prefabReference.IsValid())
        {
            _prefabReference.ReleaseAsset();
            Debug.Log("Aset dari AssetReference telah dirilis.");
        }
    }
}
