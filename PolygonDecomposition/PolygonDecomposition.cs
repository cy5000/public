using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib2D
{
    public class Polygon
    {
        //Polygon Decomposition into Convex Subpolygons
        public static List<IList<IXY>> Decomp(IList<IXY> poly)
        {
            foreach (var r in Lib2D.GetReflectVertices(poly))
            {
                var i = poly.IndexOf(r);

                foreach (var v in poly.OrderBy(p => (p.x-r.x)*(p.x-r.x) + (p.y-r.y)*(p.y-r.y) ))
                {
                    if (r.Equals(v)) continue;

                    var j = poly.IndexOf(v);
                    if (!Lib2D.CanSee(i, j, poly)) continue;

                    var left = new List<IXY>();
                    var p = i;
                    do
                    {
                        p %= poly.Count();
                        left.Add(poly[p]);
                    }
                    while (p++ != j);

                    var right = new List<IXY>();
                    p = j;
                    do
                    {
                        p %= poly.Count();
                        right.Add(poly[p]);
                    }
                    while (p++ != i);

                    var tmp = Decomp(left);
                    tmp.AddRange(Decomp(right));

                    return tmp;
                }
            }
            return new List<IList<IXY>>() { poly };
        }
    }
}