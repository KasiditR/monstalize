using UnityEngine;
using UnityEditor;
using System;
using Utilities;

public class AtlasWindowEditor : EditorWindow
{
    private Atlas atlas;
    private Sprite[] sprites;
    private Vector2 scrollPosition;
    private const float itemMinWidth = 100f;
    private const float horizontalGap = 10f;
    private const float verticalGap = 40f;
    private string searchQuery = string.Empty;
    private Sprite[] preSprites;
    private Action<Sprite> onSelectSprite;
    public static void ShowWindow(Atlas atlas, Sprite[] sprites, Action<Sprite> onSelectSprite)
    {
        AtlasWindowEditor window = GetWindow<AtlasWindowEditor>("Atlas Editor");
        window.preSprites = sprites;
        window.sprites = sprites;
        window.atlas = atlas;
        window.onSelectSprite = onSelectSprite;
    }

    void OnGUI()
    {
        GUILayout.Label("Sprite Data", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Search:", EditorStyles.boldLabel, GUILayout.Width(50));
        GUILayout.Space(5);
        searchQuery = GUILayout.TextField(searchQuery, GUILayout.Width(250));
        GUILayout.EndHorizontal();


        if (!string.IsNullOrEmpty(searchQuery))
        {
            sprites = Array.FindAll(sprites, x => x.name.ToLower().Contains(searchQuery.ToLower()));
        }
        else
        {
            sprites = preSprites;
        }


        if (sprites != null && sprites.Length > 0)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField("Sprites:");
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            float availableWidth = EditorGUIUtility.currentViewWidth - GUI.skin.verticalScrollbar.fixedWidth;
            int columns = Mathf.FloorToInt(availableWidth / (itemMinWidth + horizontalGap));
            columns = Mathf.Max(1, columns);

            for (int i = 0; i < sprites.Length; i++)
            {

                if (i % columns == 0)
                    EditorGUILayout.BeginHorizontal();
                Rect objectFieldRect = GUILayoutUtility.GetRect(itemMinWidth, itemMinWidth, GUI.skin.box);

                Color transparentColor = new Color(0f, 0f, 0f, 0f); // Full transparency (0 alpha)
                EditorGUI.DrawRect(objectFieldRect, transparentColor);

                Event currentEvent = Event.current;
                if (currentEvent.type == EventType.MouseDown && objectFieldRect.Contains(currentEvent.mousePosition))
                {
                    // UseSprite(atlas, sprites[i]);
                    onSelectSprite?.Invoke(sprites[i]);
                    Repaint(); // Forces window to repaint immediately after use
                }

                // Draw outline
                Handles.DrawSolidRectangleWithOutline(new Vector3[]
                {
        new Vector3(objectFieldRect.x, objectFieldRect.y),
        new Vector3(objectFieldRect.x + objectFieldRect.width, objectFieldRect.y),
        new Vector3(objectFieldRect.x + objectFieldRect.width, objectFieldRect.y + objectFieldRect.height + 25),
        new Vector3(objectFieldRect.x, objectFieldRect.y + objectFieldRect.height + 25)
                }, Color.clear, Color.white); // Adjust the colors as needed

                GUI.DrawTexture(objectFieldRect, sprites[i].texture, ScaleMode.ScaleToFit);

                // Calculate position for the label
                Vector2 labelSize = GUI.skin.label.CalcSize(new GUIContent(sprites[i].name));
                Vector2 labelPosition = new Vector2(
                    objectFieldRect.x + (objectFieldRect.width - labelSize.x) / 2,
                    objectFieldRect.y + objectFieldRect.height + 10f // Adjust vertical offset
                );

                // Display label centered over the rectangle
                Handles.Label(new Vector3(labelPosition.x, labelPosition.y, 0f), sprites[i].name);

                GUILayout.Space(horizontalGap); // Apply custom horizontal gap between items

                if ((i + 1) % columns == 0 || i == sprites.Length - 1)
                {
                    EditorGUILayout.EndHorizontal();
                    GUILayout.Space(verticalGap);
                }
            }


            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

            if (Event.current.type == EventType.MouseMove)
            {
                Repaint(); // Trigger repaint on mouse move to update hover effect
            }
        }
        else
        {
            EditorGUILayout.HelpBox("No sprites found.", MessageType.Info);
        }
    }

    private void UseSprite(Atlas atlas, Sprite sprite)
    {
        atlas.SetSprite(sprite.name);
    }
}
