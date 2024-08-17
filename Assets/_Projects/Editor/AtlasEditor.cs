using UnityEngine;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine.U2D;
using System;
using Utilities;

[CustomEditor(typeof(Atlas))]
public class AtlasEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(10);
        Atlas atlas = (Atlas)target;
        if (GUILayout.Button("Open Sprite Editor"))
        {
            // Open the custom Editor window and pass the sprites
            if (atlas.spriteAtlas != null)
            {
                Sprite[] sprites = new Sprite[atlas.spriteAtlas.spriteCount];
                UnityEngine.Object[] objects = atlas.spriteAtlas.GetPackables();
                for (int i = 0; i < sprites.Length; i++)
                {
                    sprites[i] = atlas.spriteAtlas.GetSprite(objects[i].name);
                    sprites[i].name = sprites[i].name.Replace("(Clone)", string.Empty);
                }

                AtlasWindowEditor.ShowWindow(atlas, sprites, AtlasPacked);
            }
            else
            {
                Debug.Log($"{atlas.name} SpriteAtlas is null");
            }
        }
        if (GUILayout.Button("Init Sprite"))
        {
            if (!string.IsNullOrEmpty(atlas.spriteName))
            {
                Sprite sprite = atlas.spriteAtlas.GetSprite(atlas.spriteName);
                if (sprite != null)
                {
                    sprite.name = sprite.name.Replace("(Clone)", string.Empty);
                    AtlasPacked(sprite);
                }
                else
                {
                    Debug.Log($"{atlas.spriteName} is not found in {atlas.spriteAtlas}");
                }
            }
            else
            {
                Debug.Log($"{atlas.spriteName} is null or empty");
            }
        }
    }

    private void AtlasPacked(Sprite sprite)
    {
        Atlas atlas = (Atlas)target;
        var atlasesGUID = AssetDatabase.FindAssets("t:spriteatlas");

        foreach (var atlasGUID in atlasesGUID)
        {
            SpriteAtlas spriteAtlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(AssetDatabase.GUIDToAssetPath(atlasGUID));

            if (spriteAtlas.CanBindTo(sprite))
            {
                UnityEngine.Object[] objs = spriteAtlas.GetPackables();
                UnityEngine.Object asset = Array.Find(objs, x => x.name == sprite.name);
                var path = AssetDatabase.GetAssetPath(asset);
                Sprite spriteUse = (Sprite)AssetDatabase.LoadAssetAtPath(path, typeof(Sprite));
                atlas.SetSpriteOnEditMode(spriteUse);
            }
        }

    }
}
