//-----------------------------------------------------------------------------
// <copyright file="PlacesManager.cs" company="WheelMUD Development Team">
//   Copyright (c) WheelMUD Development Team.  See LICENSE.txt.  This file is 
//   subject to the Microsoft Public License.  All other rights reserved.
// </copyright>
//-----------------------------------------------------------------------------

namespace WheelMUD.Core
{
    using System;
    using WheelMUD.Interfaces;
    using WheelMUD.Utilities;

    /// <summary>High level manager that provides tracking and global collection of all places.</summary>
    public class PlacesManager : ManagerSystem
    {
        /// <summary>Prevents a default instance of the <see cref="PlacesManager"/> class from being created.</summary>
        private PlacesManager()
        {
            // TODO: Assign to ItemManager instance? is it needed? currently disabled...
            this.WorldBehavior = new WorldBehavior();
            this.World = new Thing(this.WorldBehavior)
            {
                Name = GameConfiguration.Name
            };
        }

        /// <summary>Gets the singleton instance of the <see cref="PlacesManager"/> system.</summary>
        public static PlacesManager Instance { get; } = new PlacesManager();

        /// <summary>Gets the world.</summary>
        public Thing World { get; private set; }

        /// <summary>Gets the world behavior.</summary>
        public WorldBehavior WorldBehavior { get; private set; }

        /// <summary>Starts this system's individual components.</summary>
        public override void Start()
        {
            this.SystemHost.UpdateSystemHost(this, "Starting...");
            this.WorldBehavior.Load();
            this.SystemHost.UpdateSystemHost(this, "Started");
        }

        /// <summary>Stops this system's individual components.</summary>
        public override void Stop()
        {
            this.SystemHost.UpdateSystemHost(this, "Stopping...");
            //// TODO: this.WorldBehavior.Areas.Clear();
            this.SystemHost.UpdateSystemHost(this, "Stopped");
        }

        /// <summary>Registers the <see cref="PlacesManager"/> system for export.</summary>
        /// <remarks>Assists with non-rebooting updates of the <see cref="PlacesManager"/> system through MEF.</remarks>
        [ExportSystem(0)]
        public class PlacesManagerExporter : SystemExporter
        {
            /// <summary>Gets the singleton system instance.</summary>
            public override ISystem Instance => PlacesManager.Instance;

            /// <summary>Gets the Type of this system.</summary>
            public override Type SystemType => typeof(PlacesManager);
        }
    }
}