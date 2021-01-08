using HotChocolate.Data;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Downstream
{
    public class Query
    {
        [UseFiltering]
        public Parcel GetParcel() => new()
        {
            Polygon = NtsGeometryServices.Instance
            .CreateGeometryFactory(28992)
            .CreatePolygon(new LinearRing(new Coordinate[]
            {

                new Coordinate(
                  240652,
                  452141
                ),
                new Coordinate(
                  240641,
                  452106
                ),
                new Coordinate(
                  240633,
                  452092
                ),
                new Coordinate(
                  240663,
                  452062
                ),
                new Coordinate(
                  240682,
                  452086
                ),
                new Coordinate(
                  240705,
                  452075
                ),
                new Coordinate(
                  240720,
                  452097
                ),
                new Coordinate(
                  240731,
                  452139
                ),
                new Coordinate(
                  240855,
                  452122
                ),
                new Coordinate(
                  240876,
                  452095
                ),
                new Coordinate(
                  240771,
                  451976
                ),
                new Coordinate(
                  240583,
                  452066
                ),
                new Coordinate(
                  240623,
                  452155
                ),
                new Coordinate(
                  240652,
                  452141
                )
            }))
        };
    }

    public class Parcel
    {
        public Polygon Polygon { get; set; }
    }
}