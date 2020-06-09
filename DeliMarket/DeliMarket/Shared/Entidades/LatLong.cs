using System;
using System.Collections.Generic;
using System.Text;

namespace DeliMarket.Shared.Entidades
{
    public class LatLong
    {

        public string AddressName { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        //public static LatLong Interpolate(LatLong start, LatLong end, double proportion)
        //{
        //    // The Earth is flat, right? So no need for spherical interpolation.
        //    return new LatLong(
        //        start.Latitude + (end.Latitude - start.Latitude) * proportion,
        //        start.Longitude + (end.Longitude - start.Longitude) * proportion);
        //}
    }
}
