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

[CustomEditor(typeof(TPSpriteAnimationEx))]
public class TPSpriteAnimationEditor : TPBaseAnimationEditor {

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------
	
	

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	void Awake() {
		anim.OnEditorFrameChange ();
	}
	
	
	public override void OnInspectorGUI() {


		base.DrawAnimationInfo();
		
		anim.GizmosColor = EditorGUILayout.ColorField ("Gismos Color", anim.GizmosColor);


		base.DrawBaseAnimationGUI();


		anim.IsForceSelected = EditorGUILayout.Toggle ("Force Selection", anim.IsForceSelected);
		
		
		base.DrawButtonsSection();
		
	}
	
	void OnSceneGUI() {

    }
	
	
	

	
	//--------------------------------------
	// GET / SET
	//--------------------------------------

	public TPSpriteAnimation anim {
		get {
			return target as TPSpriteAnimation;
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
