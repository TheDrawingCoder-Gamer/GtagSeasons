using System;
using BepInEx;
using UnityEngine;
using Utilla;

namespace Seasons
{
	/// <summary>
	/// This is your mod's main class.
	/// </summary>

	/* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
	[ModdedGamemode]
	[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
	{
		public static bool inRoom;

		void OnEnable() {
			/* Set up your mod here */
			/* Code here runs at the start and whenever your mod is enabled*/

			Utilla.Events.GameInitialized += OnGameInitialized;
		}

		void OnDisable() {
			/* Undo mod setup here */
			/* This provides support for toggling mods with ComputerInterface, please implement it :) */
			/* Code here runs whenever your mod is disabled (including if it disabled on startup)*/
			Utilla.Events.GameInitialized -= OnGameInitialized;
		}

		void OnGameInitialized(object sender, EventArgs e)
		{
			/* Code here runs after the game initializes (i.e. GorillaLocomotion.Player.Instance != null) */
			SeasonSettings.Deserialize();
			SeasonChanger.realSeason = Season.Summer;
			GameObject snowman = GameObject.Find("Level/Forest/snowman");
			if (snowman.activeInHierarchy) {
				SeasonChanger.realSeason = Season.Winter;
			}
			GameObject snow = GameObject.Find("Level/Forest/snow");
			if (snow.activeInHierarchy) {
				SeasonChanger.realSeason = Season.Christmas;
			}
			GameObject leavesParticles = GameObject.Find("Level/Forest/SmallTreeWithTreehouse/Leaf Particles (19)");
			if (leavesParticles.activeInHierarchy) {
				SeasonChanger.realSeason = Season.Fall;
			}
			// SeasonChanger.SetSeason(SeasonSettings.season);
			
		}

		void Update()
		{
			/* Code here runs every frame when the mod is enabled */
		}

		/* This attribute tells Utilla to call this method when a modded room is joined */
		[ModdedGamemodeJoin]
		public void OnJoin(string gamemode)
		{
			/* Activate your mod here */
			/* This code will run regardless of if the mod is enabled*/

			inRoom = true;
			SeasonChanger.SetSeason(SeasonSettings.season);
		}

		/* This attribute tells Utilla to call this method when a modded room is left */
		[ModdedGamemodeLeave]
		public void OnLeave(string gamemode)
		{
			/* Deactivate your mod here */
			/* This code will run regardless of if the mod is enabled*/

			inRoom = false;
			SeasonChanger.SetSeason(Season.None);
		}
	}
}
