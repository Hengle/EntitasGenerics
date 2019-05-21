﻿using Entitas.MatchLine;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ComboWindow : EditorWindow
{
    private const string DefaultPath = "Assets/Resources/ComboDefinitions.json";
    
    [SerializeField]
    private WindowState _state = WindowState.NotInitialized;

    [SerializeField]
    private ComboDefinitions _definitions = new ComboDefinitions();

    [SerializeField]
    private ComboDataView _view;
    
    [SerializeField]
    private string _filePath = "";
    
    [MenuItem ("Window/Combo Editor")]
    public static void  ShowWindow () {
        GetWindow(typeof(ComboWindow),false, "Combo Editor", true);
    }

    private void OnEnable()
    {
        switch (_state)
        {
            case WindowState.NotInitialized:
                //Window was just opened
                _filePath = DefaultPath;
                break;
            case WindowState.Initialized:
                //Probably domain change happened, we should try to get data back
                TryReadJson();
                break;
        }
    }

    private void OnDisable()
    {
        switch (_state)
        {
            case WindowState.NotInitialized:
                break;
            case WindowState.Initialized:
                //Domain change will happen soon, try to save data
                TryCreateJson();
                break;
        }
    }

    private void OnGUI () {
        switch (_state)
        {
            case WindowState.NotInitialized:
                DrawNotInitializedScreen();
                break;
            case WindowState.Initialized:
                DrawInitializedScreen();
                break;
        }
    }

    private void DrawNotInitializedScreen()
    {
        DrawSaveLoadPanel();
    }

    private void DrawInitializedScreen()
    {
        DrawSaveLoadPanel();
        
        _view.Draw();
    }

    private void DrawSaveLoadPanel()
    {
        GUILayout.BeginVertical();
        _filePath = EditorGUILayout.TextField(_filePath);
        GUILayout.BeginHorizontal();
        
        var str = _state == WindowState.Initialized ? "Save" : "New";
        
        if (GUILayout.Button(str))
        {
            if (TryCreateJson())
            {
                _state = WindowState.Initialized;
            }
        }

        if (GUILayout.Button("Load"))
        {
            if (TryReadJson())
            {
                _state = WindowState.Initialized;
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }

    private bool TryCreateJson()
    {
        try
        {
            var json = JsonUtility.ToJson(_definitions);

            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            
            File.WriteAllText(_filePath, json);
            _definitions = JsonUtility.FromJson<ComboDefinitions>(json);
            
            ChangeView();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }
        
        AssetDatabase.Refresh ();
        return true;
    }
    
    private bool TryReadJson()
    {
        try
        {
            var json = File.ReadAllText(_filePath);
            _definitions = JsonUtility.FromJson<ComboDefinitions>(json);
                        
            ChangeView();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }

        return true;
    }

    private void ChangeView()
    {
        if(_view == null)
            _view = new ComboDataView();
            
        _view.Data = _definitions.Definitions;
        _view.InitializeList();
        _view.UpdateSelection();
    }

}

public enum WindowState
{
    NotInitialized,
    Initialized
} 