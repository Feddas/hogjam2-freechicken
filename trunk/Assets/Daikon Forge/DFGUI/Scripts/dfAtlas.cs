﻿/* Copyright 2013 Daikon Forge */
using UnityEngine;

using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

// TODO: Add ability to dynamically add new Sprites to the Texture Atlas - Use case: Multi-player avatars loaded via web, etc.

/// <summary>
/// Implements a Texture Atlas (also known as a <a href="http://www.gamasutra.com/view/feature/130940/practical_texture_atlases.php" target="_blank">Sprite Sheet</a>) which will be used by <see cref="dfSprite"/> and derived classes
/// to display textures on-screen while requiring a minimum number of draw calls. 
/// See this <a href="http://download.nvidia.com/developer/NVTextureSuite/Atlas_Tools/Texture_Atlas_Whitepaper.pdf" target="_blank">NVIDIA whitepaper</a> for a complete overview 
/// of texture atlas use and benefits.
/// </summary>
[ExecuteInEditMode]
[Serializable]
[AddComponentMenu( "Daikon Forge/User Interface/Texture Atlas" )]
#pragma warning disable 0661
#pragma warning disable 0659
public class dfAtlas : MonoBehaviour
{

	#region Nested public classes

	/// <summary>
	/// Encapsulates the information needed to render a single sprite
	/// from the Texture Atlas
	/// </summary>
	[Serializable]
	public class ItemInfo : IComparable<ItemInfo>, IEquatable<ItemInfo>
	{

		#region Public serialized fields

		/// <summary>
		/// The name of the sprite
		/// </summary>
		public string name;

		/// <summary>
		/// The area within the texture atlas where the sprite can be found, specified
		/// in UV coordinates
		/// </summary>
		public Rect region;

		/// <summary>
		/// Specifies the border sizes used in 9-slice scaling (May be empty)
		/// </summary>
		public RectOffset border = new RectOffset();

		/// <summary>
		/// Will be set to true if the sprite is rotated within the texture atlas
		/// </summary>
		public bool rotated;

		/// <summary>
		/// Returns the size of the sprite
		/// </summary>
		public Vector2 sizeInPixels = Vector2.zero;

		/// <summary>
		/// Used by the design-time editor. Do not use.
		/// </summary>
		[SerializeField]
		public string textureGUID = "";

		#endregion

		#region Public non-serialized fields

		public bool deleted = false;

		#endregion

		#region Obsolete fields

		/// <summary>
		/// Exists for backward compatibility only. *WILL* be deleted soon.
		/// </summary>
		[SerializeField]
		public Texture2D texture = null;

		#endregion

		#region IComparable<ItemInfo> Members

		/// <summary>
		/// Compare this instance against another ItemInfo instance
		/// </summary>
		/// <param name="other">The other <see cref="ItemInfo"/> instance to compare against</param>
		/// <returns></returns>
		/// <returns>
		/// A signed number indicating the relative values of this instance and value: 
		/// Less than zero if this instance should be sorted before <paramref name="other"/>, 
		/// greater than zero if this instance should be sorted after <paramref name="other"/>,
		/// and zero if both instances have the same sort order
		/// </returns>
		public int CompareTo( ItemInfo other )
		{
			return name.CompareTo( other.name );
		}

		#endregion

		#region Equality members

		// @cond DOXY_IGNORE
		public override int GetHashCode()
		{
			return name.GetHashCode();
		}
		// @endcond

		// @cond DOXY_IGNORE
		public override bool Equals( object obj )
		{

			if( !( obj is ItemInfo ) )
				return false;

			return this.name.Equals( ( (ItemInfo)obj ).name );

		}
		// @endcond

		// @cond DOXY_IGNORE
		public bool Equals( ItemInfo other )
		{
			return this.name.Equals( other.name );
		}
		// @endcond

		// @cond DOXY_IGNORE
		public static bool operator ==( ItemInfo lhs, ItemInfo rhs )
		{

			// If both are null, or both are same instance, return true.
			if( System.Object.ReferenceEquals( lhs, rhs ) )
			{
				return true;
			}

			// If one is null, but not both, return false.
			if( ( (object)lhs == null ) || ( (object)rhs == null ) )
			{
				return false;
			}

			return lhs.name.Equals( rhs.name );

		}
		// @endcond

		// @cond DOXY_IGNORE
		public static bool operator !=( ItemInfo lhs, ItemInfo rhs )
		{
			return !( lhs == rhs );
		}
		// @endcond

		#endregion

	}

	#endregion

	#region Serialized fields

	/// <summary>
	/// The Material that will be used to render any sprites rendered from this Texture Atlas
	/// </summary>
	[SerializeField]
	protected Material material;

	/// <summary>
	/// The list of sprites available in this Texture Atlas
	/// </summary>
	[SerializeField]
	protected List<ItemInfo> items = new List<ItemInfo>();

	#endregion

	#region Private instance variables

	private Dictionary<string, ItemInfo> map = new Dictionary<string, ItemInfo>();

	private dfAtlas replacementAtlas = null;

	#endregion

	#region Public properties

	/// <summary>
	/// The Texture2D instance containing all of the sprites in this Texture Atlas
	/// </summary>
	public Texture2D Texture
	{
		get { return replacementAtlas != null ? replacementAtlas.Texture : material.mainTexture as Texture2D; }
	}

	/// <summary>
	/// Returns the number of sprites available in this Texture Atlas
	/// </summary>
	public int Count
	{
		get { return replacementAtlas != null ? replacementAtlas.Count : items.Count; }
	}

	/// <summary>
	/// Returns the list of sprites defined in this Texture Atlas
	/// </summary>
	public List<ItemInfo> Items
	{
		get { return replacementAtlas != null ? replacementAtlas.Items : items; }
	}

	/// <summary>
	/// Returns a reference to the Material that will be used to render sprites 
	/// in this Texture Atlas
	/// </summary>
	public Material Material
	{
		get { return replacementAtlas != null ? replacementAtlas.Material : this.material; }
		set
		{
			if( replacementAtlas != null )
			{
				replacementAtlas.Material = value;
			}
			else
			{
				this.material = value;
			}
		}
	}

	/// <summary>
	/// Gets or sets a replacement atlas, allowing requests from sprites in this 
	/// atlas to be forwarded to the replacement.
	/// </summary>
	public dfAtlas Replacement
	{
		get { return this.replacementAtlas; }
		set { this.replacementAtlas = value; }
	}

	#endregion

	#region Indexers

	/// <summary>
	/// Retrieves sprite information by searching by sprite name
	/// </summary>
	/// <param name="key">The name of the sprite to be returned</param>
	/// <returns>An ItemInfo instance representing the desired sprite, if found. NULL otherwise</returns>
	public ItemInfo this[ string key ]
	{
		get
		{

			if( replacementAtlas != null )
				return replacementAtlas[ key ];

			if( string.IsNullOrEmpty( key ) )
				return null;

			if( map.Count == 0 )
				RebuildIndexes();

			ItemInfo result = null;
			if( map.TryGetValue( key, out result ) )
				return result;

			return null;

		}
	}

	#endregion

	#region Public methods

	/// <summary>
	/// Returns true if both <see cref="dfAtlas"/> instances are 
	/// equivalent.
	/// </summary>
	internal static bool Equals( dfAtlas lhs, dfAtlas rhs )
	{

		if( object.ReferenceEquals( lhs, rhs ) )
			return true;

		if( lhs == null || rhs == null )
			return false;

		return lhs.material == rhs.material;

	}

	/// <summary>
	/// Add a new sprite to the Texture Atlas
	/// </summary>
	/// <param name="item">The sprite data to be stored</param>
	public void AddItem( ItemInfo item )
	{
		items.Add( item );
		RebuildIndexes();
	}

	/// <summary>
	/// Add a collection of sprites to the Texture Atlas
	/// </summary>
	/// <param name="items">The sprite data to be stored</param>
	public void AddItems( IEnumerable<ItemInfo> items )
	{
		this.items.AddRange( items );
		RebuildIndexes();
	}

	/// <summary>
	/// Remove the named sprite from the Texture Atlas
	/// </summary>
	/// <param name="name">The name of the sprite to be removed</param>
	public void Remove( string name )
	{

		for( int i = items.Count - 1; i >= 0; i-- )
		{
			if( items[ i ].name == name )
			{
				items.RemoveAt( i );
			}
		}

		RebuildIndexes();

	}

	/// <summary>
	/// Rebuilds the runtime indexes that the <see cref="dfAtlas"/> class
	/// uses to speed up lookups.
	/// </summary>
	public void RebuildIndexes()
	{

		if( map == null )
			map = new Dictionary<string, ItemInfo>();
		else
			map.Clear();

		for( int i = 0; i < items.Count; i++ )
		{
			var item = items[ i ];
			map[ item.name ] = item;
		}

	}

	#endregion

}
