
create function [dbo].[F_GREAT_CIRCLE_DISTANCE]
                (
                @Latitude1  float,
                @Longitude1 float,
                @Latitude2  float,
                @Longitude2 float
                )
returns float
as
/*
fUNCTION: F_GREAT_CIRCLE_DISTANCE

                Computes the Great Circle distance in kilometers
                between two points on the Earth using the
                Haversine formula distance calculation.

Input Parameters:
                @Longitude1 - Longitude in degrees of point 1
                @Latitude1  - Latitude  in degrees of point 1
                @Longitude2 - Longitude in degrees of point 2
                @Latitude2  - Latitude  in degrees of point 2

*/
begin
declare @radius float

declare @lon1  float
declare @lon2  float
declare @lat1  float
declare @lat2  float

declare @a float
declare @distance float

-- Sets average radius of Earth in Kilometers
set @radius = 6371.0E

-- Convert degrees to radians
set @lon1 = radians( @Longitude1 )
set @lon2 = radians( @Longitude2 )
set @lat1 = radians( @Latitude1 )
set @lat2 = radians( @Latitude2 )

set @a = sqrt(square(sin((@lat2-@lat1)/2.0E)) + 
                (cos(@lat1) * cos(@lat2) * square(sin((@lon2-@lon1)/2.0E))) )

set @distance =
                @radius * ( 2.0E *asin(case when 1.0E < @a then 1.0E else @a end ))

return @distance

end