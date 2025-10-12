using NUnit.Framework;
using UnityEngine;

[System.Serializable]
public class Category
{
    public string category;
    public string total;
}

[System.Serializable]
public class Categories
{
    public Category[] items;
}
