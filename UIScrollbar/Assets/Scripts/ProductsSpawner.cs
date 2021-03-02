using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductsSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _productsPos;
    [SerializeField]
    private GameObject _product;
    [SerializeField]
    private int _countProductsSpawn;
    

    private void OnEnable() {
        for(int i = 0; i < _countProductsSpawn; i++) {
            var prod = Instantiate(_product, _productsPos);
        }
    }
    
}
