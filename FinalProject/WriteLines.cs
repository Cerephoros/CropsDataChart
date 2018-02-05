using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    /**
     * Class cannot be instantiated and takes three functions that expect
     * a derived class to inherit from.
     * Author: Erik Njolstad
     * Date Modified: 11/10/2017
     */
    public abstract class WriteLines
    {
        public abstract string GetColumnHeader();
        public abstract string[] GetLines();
        public abstract List<string> GetList();
    }
}
