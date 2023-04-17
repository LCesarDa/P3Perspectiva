using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3Perspectiva
{
    public class Instance
    {
        public Model model;
        public Transform transform;
        public Instance(Model m, Transform t)
        {
            model = m;
            transform = t;
        }
    }
}
