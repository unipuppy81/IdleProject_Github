using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableDownloader
{
    #region Fields

    private DownloadEvents events;
    private string labelToDownload;
    private long totalSize;

    #endregion

    #region Work Flow

    public DownloadEvents InitializeSystem(string label)
    {
        events = new DownloadEvents();

        Addressables.InitializeAsync().Completed += OnInitialized;

        labelToDownload = label;

        ResourceManager.ExceptionHandler += OnException;

        return events;
    }

    public void UpdateCatalog()
    {
        events.NotifyCatalogUpdated();
    }

    public void DownloadSize()
    {
        Addressables.GetDownloadSizeAsync(labelToDownload).Completed += OnSizeDownloaded;
    }

    public void StartDownload()
    {
        events.NotifyDownloadFinished(true);
    }

    public void Update()
    {
        
    }

    #endregion

    #region Events

    private void OnInitialized(AsyncOperationHandle<IResourceLocator> result)
    {
        events.NotifyInitialized();
    }

    void OnCatalogUpdated(AsyncOperationHandle<List<IResourceLocator>> result)
    {
        events.NotifyCatalogUpdated();
    }

    void OnSizeDownloaded(AsyncOperationHandle<long> result)
    {
        totalSize = result.Result;
        events.NotifySizeDownloaded(result.Result);
    }

    void OnDependenciesDownloaded(AsyncOperationHandle result)
    {
        events.NotifyDownloadFinished(result.Status == AsyncOperationStatus.Succeeded);
    }

    void OnException(AsyncOperationHandle handle, Exception exception)
    {
        Debug.LogError(exception.Message);
    }

    #endregion
}
