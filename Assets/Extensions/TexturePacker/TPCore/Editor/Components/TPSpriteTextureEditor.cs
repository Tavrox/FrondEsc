////////////////////////////////////////////////////////////////////////////////
//  
// @module Texture Packer Plugin for Unity3D 
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


[CustomEditor(typeof(TPSpriteTextureEx))]
public class TPSpriteTextureEditor : TPSpriteAnimationEditor {

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	public override void OnInspectorGUI() {

		DrawInfo();
		
		anim.GizmosColor = EditorGUILayout.ColorField ("Gismos Color", anim.GizmosColor);

		anim.IsForceSelected = EditorGUILayout.Toggle ("Force Selection", anim.IsForceSelected);



		base.DrawButtonsSection();
		
	}
	
	public void DrawInfo() {
		EditorGUILayout.Separator();
		
		string msg = string.Empty;
		
		if(tex.sprite.frames.Count > 0) {
			msg += "Atlas: " + tex.sprite.frames[0].atlasPath + "\n";
			msg += "Texture: " + tex.sprite.frames[0].textureName;
		} else {
			msg = "Sprite is empty";
		}
		
		EditorGUILayout.HelpBox(msg, MessageType.Info);
		
		EditorGUILayout.Separator();
	}
	
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	public TPSpriteTexture tex {
		get {
			return target as TPSpriteTexture;
		}
	}
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------


}
