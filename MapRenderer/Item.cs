using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;

using Resources;
using Maps;

namespace Renderer
{
    public class ItemRenderFactory
    {
        protected static Dictionary<Type, ItemRenderer> RenderCache = new Dictionary<Type, ItemRenderer>();
        protected static Dictionary<Type, Type> Renderers = new Dictionary<Type, Type>();

        public static void LoadRenderers ( Assembly assembly )
        {
            foreach (Type t in assembly.GetTypes())
            {
                if (t.IsSubclassOf(typeof(ItemRenderer)))
                {
                    ItemRenderer i = (ItemRenderer)Activator.CreateInstance(t);
                    Type itemType = i.RenderType();
                    if (Renderers.ContainsKey(itemType))
                        Renderers[itemType] = t;
                    else
                        Renderers.Add(itemType, t);
                }
            }
        }

        public static ItemRenderer GetRenderer ( ItemType item )
        {
            Type renderer = typeof(ItemRenderer);

            // walk the tree until we find a type we have a renderer for
            Type t = item.GetType();
            while (true)
            {
                if (t == typeof(ItemType))
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

            ItemRenderer i = (ItemRenderer)Activator.CreateInstance(renderer);
            RenderCache.Add(renderer, i);
            return i;
        }
    }

    public class ItemRenderer
    {
        static Image DefaultImage = new Bitmap(ResourceManager.FindFile("images/default_item.png"));

        static Dictionary<string, Image> ImageCache = new Dictionary<string, Image>();

        public virtual Type RenderType()
        {
            return ItemType;
        }

        public virtual Image GetImage (ItemType item)
        {
            if (item.Texture != string.Empty)
            {
                if (ImageCache.ContainsKey(item.Texture))
                    return ImageCache[item.Texture];

                Image img = new Bitmap(ResourceManager.FindFile(item.Texture));
                ImageCache.Add(item.Texture, img);
                return img;
            }
            return DefaultImage;
        }
    }
}
