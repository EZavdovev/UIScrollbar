using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Events;
public class ProductsSpawner : MonoBehaviour {

    [SerializeField]
    private Transform _productsPos;

    [SerializeField]
    private GameObject _product;

    [SerializeField]
    private List<AssetReference> _foodAssets = new List<AssetReference>();

    [SerializeField]
    private List<AssetReference> _avatarAssets = new List<AssetReference>();

    private List<Sprite> _foodSprites = new List<Sprite>();
    private List<Sprite> _avatarSprites = new List<Sprite>();
    
    [SerializeField]
    private EventListener _allDownloadListener;

    [SerializeField]
    private EventDispatcher _downloadDispatcher;

    private List<ProductInfoJson> _productsInfo = new List<ProductInfoJson>();

    private void OnEnable() {
        _allDownloadListener.OnEventHappened += ProductSpawn;
        DownloadAssets(_foodAssets, _foodSprites);
        DownloadAssets(_avatarAssets, _avatarSprites);
    }

    private void ProductSpawn() {
        var wrapper = JsonUtility.FromJson<JsonProductWrapper>(File.ReadAllText(Application.streamingAssetsPath + "/TeletapeList.json"));
        _productsInfo = wrapper._productsInfo;
        for (int i = 0; i < _productsInfo.Count; i++) {
            var prod = Instantiate(_product, _productsPos);
            prod.GetComponent<ProductInfo>()._iconProduct.sprite = _foodSprites[_productsInfo[i]._iconProductID];
            prod.GetComponent<ProductInfo>()._iconAvatar.sprite = _avatarSprites[_productsInfo[i]._iconAvatarID];
            prod.GetComponent<ProductInfo>()._costProduct.text = _productsInfo[i]._costProduct.ToString();
            prod.GetComponent<ProductInfo>()._countProduct.text = _productsInfo[i]._countProduct.ToString();
            prod.GetComponent<ProductInfo>()._lvlAvatar.text = _productsInfo[i]._lvlAvatar.ToString();
            prod.GetComponent<ProductInfo>()._nameAvatar.text = _productsInfo[i]._nameAvatar;
            prod.GetComponent<ProductInfo>()._nameProduct.text = _productsInfo[i]._nameProduct;
        }
    }

    private async void DownloadAssets(List<AssetReference> assetReferences, List<Sprite> sprites) {
        for (int i = 0; i < assetReferences.Count; i++) {
            AsyncOperationHandle<Sprite> handle = assetReferences[i].LoadAssetAsync<Sprite>();
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded) {
                sprites.Add(handle.Result);
                Addressables.Release(handle);
            }
        }
        _downloadDispatcher.Dispatch();
    }

    
}


   
