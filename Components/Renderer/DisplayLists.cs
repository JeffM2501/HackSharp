using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Graphics.OpenGL;

namespace Renderer
{
    public class DisplayList : IDisposable
    {
        public delegate void GenerateListEvent(DisplayList list);

        public static void FlushGL()
        {
            lock(ListCache)
            {
                foreach (DisplayList list in ListCache)
                    list.Invalidate();
            }
        }

        protected static List<DisplayList> ListCache = new List<DisplayList>();

        protected event GenerateListEvent Generate;

        protected static int InvalidList = -1;
        protected int List = InvalidList;

        public DisplayList ( GenerateListEvent e )
        {
            Generate = e;
            lock(ListCache)
            {
                ListCache.Add(this);
            }
        }

        public void Dispose()
        {
            Invalidate();
            lock(ListCache)
            {
                ListCache.Remove(this);
            }
        }

        public void Invalidate ()
        {
            if (List != InvalidList)
                GL.DeleteLists(List, 1);

            List = InvalidList;
        }

        public void Call ()
        {
            if (Generate == null)
                return;

            if (List == InvalidList)
            {
                List = GL.GenLists(1);
                GL.NewList(List, ListMode.CompileAndExecute);

                Generate(this);

                GL.EndList();
            }
            else
                GL.CallList(List);
        }
    }
}
