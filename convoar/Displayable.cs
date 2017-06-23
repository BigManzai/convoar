﻿/*
 * Copyright (c) 2017 Robert Adams
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenSim.Region.Framework.Scenes;

using OMV = OpenMetaverse;

namespace org.herbal3d.convoar {
    /// <summary>
    /// A set of classes that hold viewer displayable items. These can be
    /// meshes, procedures, or whatever.
    /// </summary>
    public class Displayable {
        public string name = "no name";
        public OMV.UUID baseUUID = OMV.UUID.Zero;   // the UUID of the original object that careated is displayable
        public OMV.Vector3 offsetPosition = OMV.Vector3.Zero;
        public OMV.Quaternion offsetRotation = OMV.Quaternion.Identity;
        public OMV.Vector3 scale = new OMV.Vector3(1,1,1);
        public DisplayableRenderable renderable = null;
        public List<Displayable> children = new List<Displayable>();
        public SceneObjectPart baseSOP = null;

        private GlobalContext _context;

        public Displayable(GlobalContext pContext) {
            _context = pContext;
        }

        public Displayable(DisplayableRenderable pRenderable, GlobalContext pContext) {
            _context = pContext;
            renderable = pRenderable;
        }

        public Displayable(DisplayableRenderable pRenderable, SceneObjectPart sop, GlobalContext pContext) {
            _context = pContext;
            name = sop.Name;
            baseSOP = sop;
            offsetPosition = baseSOP.OffsetPosition;
            offsetRotation = baseSOP.RotationOffset;
            if (_context.parms.DisplayTimeScaling) {
                scale = sop.Scale;
            }
            renderable = pRenderable;
        }
    }

    /// <summary>
    /// The parent class of the renderable parts of the displayable.
    /// Could be a mesh or procedure or whatever.
    /// </summary>
    public abstract class DisplayableRenderable {
    }

    /// <summary>
    /// A group of meshes that make up a renderable item.
    /// For OpenSimulator conversions, this is usually prim faces.
    /// </summary>
    public class RenderableMeshGroup : DisplayableRenderable {
        // The meshes that make up this Renderable
        public List<RenderableMesh> meshes = new List<RenderableMesh>();
    }
        
    public class RenderableMesh {
        public int num;                 // number of this face on the prim
        public EntityHandle mesh;
        public EntityHandle material;
    }



} 