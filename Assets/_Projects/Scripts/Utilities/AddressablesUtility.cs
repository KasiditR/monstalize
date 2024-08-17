using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Utilities
{
    public static class AddressablesUtility
    {
        public static IEnumerator Load<T>(string keyOrLabel, Action<AsyncOperationHandle<T>, T> onComplete) where T : UnityEngine.Object
        {
            AsyncOperationHandle<T> loadHandle = Addressables.LoadAssetAsync<T>(keyOrLabel);

            yield return loadHandle;

            if (loadHandle.Status == AsyncOperationStatus.Succeeded)
            {
                onComplete?.Invoke(loadHandle, loadHandle.Result);
            }
            else
            {
                Debug.LogError("Failed to load Addressable Asset: " + loadHandle.Status);
                onComplete?.Invoke(loadHandle, null);
            }

        }

        public static IEnumerator Load<T>(AssetLabelReference labelReference, Action<AsyncOperationHandle<T>, T> onComplete) where T : UnityEngine.Object
        {
            AsyncOperationHandle<T> loadHandle = Addressables.LoadAssetAsync<T>(labelReference);

            yield return loadHandle;

            if (loadHandle.Status == AsyncOperationStatus.Succeeded)
            {
                onComplete?.Invoke(loadHandle, loadHandle.Result);
            }
            else
            {
                Debug.LogError("Failed to load Addressable Asset: " + loadHandle.Status);
                onComplete?.Invoke(loadHandle, null);
            }
        }
        public static IEnumerator LoadAll<T>(string assetLabel, Action<AsyncOperationHandle<IList<T>>, T[]> onComplete) where T : UnityEngine.Object
        {
            AsyncOperationHandle<IList<T>> loadHandle = Addressables.LoadAssetsAsync<T>(assetLabel, null);

            yield return loadHandle;

            if (loadHandle.Status == AsyncOperationStatus.Succeeded)
            {
                IList<T> loadedAssets = loadHandle.Result;
                onComplete?.Invoke(loadHandle, loadedAssets.ToArray());
            }
            else
            {
                Debug.LogError("Failed to load Addressable Assets: " + loadHandle.Status);
                onComplete?.Invoke(loadHandle, Array.Empty<T>());
            }
        }
        public static IEnumerator LoadAll<T>(AssetLabelReference assetLabelReference, Action<AsyncOperationHandle<IList<T>>, T[]> onComplete) where T : UnityEngine.Object
        {
            AsyncOperationHandle<IList<T>> loadHandle = Addressables.LoadAssetsAsync<T>(assetLabelReference, null);

            yield return loadHandle;

            if (loadHandle.Status == AsyncOperationStatus.Succeeded)
            {
                IList<T> loadedAssets = loadHandle.Result;
                onComplete?.Invoke(loadHandle, loadedAssets.ToArray());
            }
            else
            {
                Debug.LogError("Failed to load Addressable Assets: " + loadHandle.Status);
                onComplete?.Invoke(loadHandle, Array.Empty<T>());
            }
        }
    }
}
