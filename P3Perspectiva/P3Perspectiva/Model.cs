using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace P3Perspectiva
{
    public class Model
    {
        public List<Vertex> vertex;
        public List<Triangle> triangles;
        public Vertex position;
        Matrix p = new Matrix(new float[,]
                {
                    {1,0,0,0
    },
                    {0,1,0,0
},
                    { 0,0,1,0},
                    { 0,0,0,1}
                });
public Model(List<Vertex> ver, List<Triangle> tris)
        {
            this.vertex = ver;
            this.triangles = tris;

        }
    }
}
