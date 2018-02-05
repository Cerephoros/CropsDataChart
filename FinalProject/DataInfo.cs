using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    /**
     * The purpose of this class is to have getters and setters and take the value from the
     * CSV file and insert them into a database.
     * Author: Erik Njolstad
     * Date Modified: 11/10/2017
     */
    public class DataInfo
    {
        private int dataID;
        private int refDate;
        private string geo;
        private string origin;
        private string vector;
        private string coordinate;
        private string value;

        /**
         * Returns the dataID.
         */
        public int GetDataID()
        {
            return dataID;
        }

        /**
         * Sets the refDate.
         */
        public void SetDataID(int dataID)
        {
            this.dataID = dataID;
        }

        /**
         * Returns the refDate.
         */
        public int GetRefDate()
        {
            return refDate;
        }

        /**
         * Sets the refDate.
         */
        public void SetRefDate(int refDate)
        {
            this.refDate = refDate;
        }

        /**
         * Returns the geo.
         */
        public string GetGeo()
        {
            return geo;
        }

        /**
         * Sets the geo.
         */
        public void SetGeo(string geo)
        {
            this.geo = geo;
        }

        /**
         * Returns the origin.
         */
        public string GetOrigin()
        {
            return origin;
        }

        /**
         * Sets the origin.
         */
        public void SetOrigin(string origin)
        {
            this.origin = origin;
        }

        /**
         * Returns the vector.
         */
        public string GetVector()
        {
            return vector;
        }

        /**
         * Sets the vector.
         */
        public void SetVector(string vector)
        {
            this.vector = vector;
        }

        /**
         * Returns the coordinate.
         */
        public string GetCoordinate()
        {
            return coordinate;
        }

        /**
         * Sets the coordinate.
         */
        public void SetCoordinate(string coordinate)
        {
            this.coordinate = coordinate;
        }

        /**
         * Gets the value.
         */
        public string GetValue()
        {
            return value;
        }

        /**
         * Sets the value.
         */
        public void SetValue(string value)
        {
            this.value = value;
        }

        /**
         * Overrided function that returns a string output.
         */
        public override string ToString()
        {
            return "\nRef_Date: " + GetRefDate() + "\nGEO: " + GetGeo() + "\nEST: " + GetOrigin()
                + "\nVector: " + GetVector() + "\nCoordinate: " + GetCoordinate() + "\nValue: " + GetValue()
                + "\n";
        }
    }
}
