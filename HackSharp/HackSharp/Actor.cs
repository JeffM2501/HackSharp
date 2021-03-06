﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Drawing;

using Resources;
using Maps;

namespace HackSharp
{
    public class ActorRenderFactory
    {
        protected static Dictionary<Type, ActorRenderer> RenderCache = new Dictionary<Type, ActorRenderer>();
        protected static Dictionary<Type, Type> Renderers = new Dictionary<Type, Type>();

        public static void LoadRenderers(Assembly assembly)
        {
            foreach (Type t in assembly.GetTypes())
            {
                if (t.IsSubclassOf(typeof(ActorRenderer)))
                {
                    ActorRenderer i = (ActorRenderer)Activator.CreateInstance(t);
                    Type itemType = i.RenderType();
                    if (Renderers.ContainsKey(itemType))
                        Renderers[itemType] = t;
                    else
                        Renderers.Add(itemType, t);
                }
            }
        }

        public static ActorRenderer GetRenderer(Actor item)
        {
            Type renderer = typeof(ActorRenderer);

            // walk the tree until we find a type we have a renderer for
            Type t = item.GetType();
            while (true)
            {
                if (t == typeof(Actor))
                    break;
                if (Renderers.ContainsKey(t))
                {
                    renderer = Renderers[t];
                    break;
                }
                t = t.BaseType;
            }

            if (RenderCache.ContainsKey(renderer))
                return RenderCache[renderer];

            ActorRenderer i = (ActorRenderer)Activator.CreateInstance(renderer);
            RenderCache.Add(renderer, i);
            return i;
        }
    }

    public class ActorRenderer
    {
        static Image DefaultImage = new Bitmap(ResourceManager.FindFile("images/default_actor.png"));

        public virtual Type RenderType()
        {
            return typeof(Actor);
        }

        public virtual Image GetImage(Actor item)
        {
            return DefaultImage;
        }
    }

    public class AnimatedCharacter
    {
        public enum AnimSequence
        {
            Standing,
            Moving,
            Attacking,
            Defending,
            Ranged,
            Dying,
            Dead,
        }
    }

    public class AnimatedActorRenderer : ActorRenderer
    {
        public override Type RenderType()
        {
            return typeof(AnimatedActor);
        }

        static Dictionary<string, Image> ImageCache = new Dictionary<string, Image>();

        public override Image GetImage(Actor item)
        {
            AnimatedActor actor = item as AnimatedActor;
            if (actor == null)
                return null;

            return base.GetImage(item);
        }
    }
}
